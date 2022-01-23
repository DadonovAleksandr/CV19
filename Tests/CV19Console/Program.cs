using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CV19Console
{
    class Program
    {
        const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();

            var responce = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await responce.Content.ReadAsStreamAsync();
        }


        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while(!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line) || line.Length < 100) continue;
                yield return line;
            }

        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();




        static void Main(string[] args)
        {
            ////WebClient client = new WebClient();

            //var client = new HttpClient();

            //var responce = client.GetAsync(data_url).Result;
            //var csv_str = responce.Content.ReadAsStringAsync().Result;

            //Console.WriteLine(csv_str);

            //foreach(var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDates();
            Console.WriteLine(string.Join("\r\n", dates));

            Console.ReadLine();
        }
    }
}
