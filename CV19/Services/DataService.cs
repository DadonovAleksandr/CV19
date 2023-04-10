using CV19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Services;

internal class DataService
{
    const string _dataSourceAddress =
    @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

    static async Task<Stream> GetDataStream()
    {
        var client = new HttpClient();

        var responce = await client.GetAsync(_dataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
        return await responce.Content.ReadAsStreamAsync();
    }

    static IEnumerable<string> GetDataLines()
    {
        using var dataStream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
        using var dataReader = new StreamReader(dataStream);

        while (!dataReader.EndOfStream)
        {
            var line = dataReader.ReadLine();
            if (line == null) continue;
            yield return line
                .Replace("Korea,", "Korea -")
                .Replace("Saint Helena,", "Saint Helena -")
                .Replace("Bonaire,", "Bonaire -");
        }
    }

    static DateTime[] GetDates() => GetDataLines()
    .First()
    .Split(",")
    .Skip(4)
    .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
    .ToArray();

    static IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)> GetCountriesData()
    {
        var lines = GetDataLines()
            .Skip(1)
            .Select(line => line.Split(','));

        foreach (var row in lines)
        {
            var province = row[0].Trim() ?? string.Empty;
            var country_name = row[1].Trim(' ', '"') ?? string.Empty;
            var latitude = 0.0;
            var longitude = 0.0;
            double.TryParse(row[2].Trim(), CultureInfo.InvariantCulture, out latitude);
            double.TryParse(row[3].Trim(), CultureInfo.InvariantCulture, out longitude); 
            var counts = row.Skip(4).Select(int.Parse).ToArray();

            yield return new (province, country_name, (latitude, longitude), counts);
        }
    }

    public IEnumerable<CountryInfo> GetData()
    {
        var dates = GetDates();

        var data = GetCountriesData().GroupBy(d => d.Country);

        foreach (var countryInfo in data)
        {
            var country = new CountryInfo
            {
                Name = countryInfo.Key,
                ProvinceCounts = countryInfo.Select(c => new PlaceInfo
                {
                    Name = c.Province,
                    Location = new Point(c.Place.Lat, c.Place.Lon),
                    Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                })
            };
            yield return country;
        }

    }
}

