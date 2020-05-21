using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Utilities
{
    public static class ConfigUtils
    {
        private static readonly string pathToTestData = PathUtil.GetPathToFile(@"Resources\config.json");
        public static string GetValueFromConfig(string key)
        {
            var data = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(pathToTestData));
            return data[key].Value<string>();
        }
    }
}
