

using System;

namespace Weather
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1 || args[0] == "-h")
            {
                Console.WriteLine("Please provide zip code of the location to get the weather data back");
                Console.WriteLine("with comma seperation format eg: 94511,95632");
            }
            else
            {
                var zipCodes = args[0].Split(',');

                // var currentDate = DateTime.Now;
                try
                {
                    GetWeatherData.GetWeatherAsyncAll(zipCodes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                }
            }
        }
    }
}
