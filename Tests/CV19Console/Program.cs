using System;
using System.Net;
using System.Net.Http;

namespace CV19Console
{
    class Program
    {
        const string data_url = @"https://github.com/CSSEGISandData/COVID-19/blob/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";


        static void Main(string[] args)
        {
            //WebClient client = new WebClient();

            var client = new HttpClient();

            var responce = client.GetAsync(data_url).Result;
            var csv_str = responce.Content.ReadAsStringAsync().Result;

            Console.WriteLine(csv_str);
            Console.ReadLine();
        }
    }
}
