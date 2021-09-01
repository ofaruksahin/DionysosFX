using Newtonsoft.Json;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class VersionItem
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("deprecated")]
        public bool Deprecated { get; set; }
    }
}
