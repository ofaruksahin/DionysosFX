using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class DocumentationResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("controllers")]
        public List<ControllerItem> Controllers
        {
            get;
            set;
        } = new List<ControllerItem>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("versions")]
        public List<string> Versions
        {
            get;
            set;
        } = new List<string>();
    }
}
