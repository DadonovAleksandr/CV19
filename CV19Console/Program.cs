using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;


const string dataUrl = 
    @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

//var client = new HttpClient();
//var responce= client.GetAsync(dataUrl).Result;
//var csvStr = responce.Content.ReadAsStringAsync().Result;
//Console.WriteLine(csvStr);

foreach(var line in GetDataLines())
{
    Console.WriteLine(line);
}

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
        yield return line;
    }
}

static DateTime[] GetDates() => GetDataLines()
    .First()
    .Split(",")
    .Skip(4)
    .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
    .ToArray();

