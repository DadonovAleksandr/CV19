using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;


const string dataUrl = 
    @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

//var client = new HttpClient();
//var responce= client.GetAsync(dataUrl).Result;
//var csvStr = responce.Content.ReadAsStringAsync().Result;
//Console.WriteLine(csvStr);

//foreach(var line in GetDataLines())
//{
//    Console.WriteLine(line);
//}

var russia_data = GetData()
    .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));
Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date} - {count}")));

Console.ReadLine();



static async Task<Stream> GetDataStream()
{
    var client = new HttpClient();

    var responce = await client.GetAsync(dataUrl, HttpCompletionOption.ResponseHeadersRead);
    return await responce.Content.ReadAsStreamAsync();

}

static IEnumerable<string> GetDataLines()
{
    using var dataStream = GetDataStream().Result;
    using var dataReader = new StreamReader(dataStream);

    while(!dataReader.EndOfStream)
    {
        var line = dataReader.ReadLine();
        if (line == null) continue; 
        yield return line.Replace("Korea,", "Korea -").Replace("Bonaire,", "Bonaire -");
    }
}

static DateTime[] GetDates() => GetDataLines()
    .First()
    .Split(",")
    .Skip(4)
    .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
    .ToArray();

static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
{
    var lines = GetDataLines()
        .Skip(1)
        .Select(line => line.Split(','));

    foreach(var row in lines)
    {
        var province = row[0].Trim() ?? string.Empty;
        var country_name = row[1].Trim(' ', '"') ?? string.Empty;
        var counts = row.Skip(4).Select(int.Parse).ToArray();

        yield return (country_name, province, counts);
    }
}

