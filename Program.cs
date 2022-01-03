using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpWeather
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Open Weather Map API Key goes here.
            const string APP_ID = "----openweathermapAPIkey-----";

            var manager = new WeatherManager(APP_ID);
                        
            var done = false;
            while (done == false)
            {
                Console.WriteLine("Please enter city name (Example: knoxville / miami / Etc..): ");
                string city = Console.ReadLine();
                if (city == "done")
                {
                    done = true;
                    return;
                }
                else
                {
                    Console.WriteLine(manager.getWeatherByCityName(city));
                }
            }

        }
    }
}
