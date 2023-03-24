
using System.Net;

const string dataUrl = 
    @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

HttpClient client = new HttpClient();
var responce= client.GetAsync(dataUrl).Result;
var csvStr = responce.Content.ReadAsStringAsync().Result;
Console.WriteLine(csvStr);
Console.ReadLine();
