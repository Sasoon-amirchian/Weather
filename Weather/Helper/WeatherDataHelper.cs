using System.Collections.Generic;
using System.Linq;

namespace Weather.Helper
{
    public static class WeatherDataHelper
    {
        public static object convertWeatherData(RootData[] dataList, string zipCode)
        {
            var QueryMinutely =
                    from data in dataList
                    select data.minutely;

            var avgPrecip = QueryMinutely.Select((args) => from arg in args select arg.precip).First().Average();
            var avgTempF = QueryMinutely.Select((args) => from arg in args select arg.temp).First().Average();
            var startTime = QueryMinutely.Select((args) => from arg in args select arg.timestamp_local).First().First().TimeOfDay;
            var endTime = QueryMinutely.Select((args) => from arg in args select arg.timestamp_local).First().Last().TimeOfDay;

            List<string> result = new();
            result.Add(zipCode);
            result.Add(dataList[0].data[0].timezone.ToString());
            result.Add(dataList[0].data[0].city_name);
            result.Add(dataList[0].data[0].state_code);
            result.Add(dataList[0].data[0].weather.description);
            result.Add(dataList[0].data[0].precip.ToString());
            result.Add(dataList[0].data[0].temp.ToString());
            result.Add(avgPrecip.ToString());
            result.Add(avgTempF.ToString());
            result.Add(startTime.ToString());
            result.Add(endTime.ToString());
            return string.Join("\t", result);
        }
    }
}
