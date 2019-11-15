using Newtonsoft.Json;
using ButlerBot.Entities;
using System.IO;

namespace ButlerBot.Services
{
    public class ConfigService
    {
        public Config GetConfig()
        {
            var file = "config.json";
            var data = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<Config>(data);
        }
    }
}