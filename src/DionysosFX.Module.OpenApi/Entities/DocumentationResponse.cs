using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class DocumentationResponse
    {
        [JsonProperty("application_name")]
        ///
        public string ApplicationName { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("schema")]
        public List<SchemaItem> Schemas
        {
            get;
            set;
        } = new List<SchemaItem>();
    }
}
