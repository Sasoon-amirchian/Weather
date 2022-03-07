using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using Weather.Helper;

namespace Weather
{
    public static class GetWeatherData
    {
        public static void GetWeatherAsyncAll(string[] zipCodes)
        {
            for (int i = 0; i < zipCodes.Length; i++)
            {
                var data = Task.WhenAll(GetWeatherAsync(zipCodes[i]));
                var dataList = data.Result;
                writeFile(dataList, zipCodes[i]);
            }
        }

        private static async Task<RootData> GetWeatherAsync(string zipCode)
        {
            string URL = "https://api.weatherbit.io/v2.0/current";
            HttpClient client = new();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Clear();
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string urlParameters = $"?postal_code={zipCode}&include=minutely&units=I&key=24d6f2a2e84849e2aaa77f8e372ba681";

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync();
                RootData rootData = JsonConvert.DeserializeObject<RootData>(dataObjects);

                return rootData;
            }
            else
            {
                throw new Exception(message: "Server call was unsuccessful");
            }
        }

        private static void writeFile(RootData[] dataList, string zipCode)
        {
            string tab = "\t";
            string[] headerList = { "ZipCode", "TimeZone", "City", "State", "WeatherDescription", "CurrentPrecipPercent", "CurrentTempInFahrenheit", "AveragePrecipPercent", "AverageTempInFahrenheit", "StartTime", "EndTime", "CountDataPoints" };
            string header = "";

            var currentTime = DateTime.Now.ToLongTimeString();
            string fileName = $"weather-{currentTime}.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    header = string.Join(tab, headerList);

                    writer.WriteLine(header); //WriteLine - header
                    writer.WriteLine(WeatherDataHelper.convertWeatherData(dataList, zipCode));
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }

    }
}