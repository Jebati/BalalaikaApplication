using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace LoggerLibrary.Configurator
{
    public static class Configurator
    {
        public static async Task<Config> GetConfigAsync()
        {
            Config config = new Config();

            await Task.Run(() =>
            {
                string json = GetJsonOnFile();
                config = DeserializeJson(json);
            });

            return config;
        }

        private static Config DeserializeJson(string json)
        {
            return JsonConvert.DeserializeObject<Config>(json);
        }

        private static string GetJsonOnFile(string path = @"Config.json")
        {
            return File.ReadAllText(path);
        }
    }
}
