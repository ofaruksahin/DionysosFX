using Newtonsoft.Json;

namespace DionysosFX.Module.OpenApi
{
    internal class OpenApiUrlItem
    {
        [JsonProperty("url")]
        public string Url
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        public OpenApiUrlItem(string Url,string Name)
        {
            this.Url = Url;
            this.Name = Name;
        }
    }
}
