using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MdInfoGenerator
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class InfoJson
    {
        public string Name;
        public string Author;
        public string Bpm;
        public string Scene;
        public string LevelDesigner;
        public string LevelDesigner1;
        public string LevelDesigner2;
        public string LevelDesigner3;
        public string LevelDesigner4;
        public string Difficulty1;
        public string Difficulty2;
        public string Difficulty3;
        public string Difficulty4;
        public string HideBmsMode;
        public int HideBmsDifficulty;
        public string HideBmsMessage;
    }
}
