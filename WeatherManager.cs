using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpWeather
{
    public class WeatherManager
    {
        public string app_id { get; set; }
        public List<City> cityList;

        public WeatherManager(string app_ID)
        {
            app_id = app_ID;
            this.buildCityList();
        }

        private int isCityValid(string city)
        {
            return cityList.FindIndex(x =>
            x.Name.ToLower().Equals(city.ToLower()));
        }

        public string getWeatherByCityName(string city)
        {
            if (city == null | city == "") 
            { 
                return "No city entered"; 
            }
            else
            {
                var cityID = isCityValid(city);
                if (cityID > 0)
                {

                    string query = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, app_id);
                    JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));
                    var weatherReport = "Weather for " + city + " on " + DateTime.Today.Date.ToString() + "\n" + parseWeather(response);
                    return weatherReport;
                }
                return "City name invalid";
            }

        }

        public void buildCityList()
        {
            using ( StreamReader r = new StreamReader("../../../city.list.json"))
            {
                Console.WriteLine("Building city list...");
                string json = r.ReadToEnd();
                cityList = JsonConvert.DeserializeObject<List<City>>((json));
            }
        }
        public string parseWeather(JObject locationWeather)
        {
            string weatherMain = locationWeather.SelectToken("weather[0].main").ToString();
            string weatherDesc = locationWeather.SelectToken("weather[0].description").ToString();
            StringBuilder weatherResponse = new StringBuilder();
            weatherResponse.Append("Todays Forecast: " + weatherMain + "\n"
                                   + "Description: " + weatherDesc + "\n");
            return weatherResponse.ToString();
        }
 

    }
}
