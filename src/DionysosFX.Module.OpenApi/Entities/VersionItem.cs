using Newtonsoft.Json;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class VersionItem
    {
        /// <summary>
        /// Version Name
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Version is deprecated
        /// </summary>
        [JsonProperty("deprecated")]
        public bool Deprecated { get; set; }
    }
}
