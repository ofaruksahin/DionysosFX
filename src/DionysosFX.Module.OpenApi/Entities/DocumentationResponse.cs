using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class DocumentationResponse
    {
        /// <summary>
        /// Application Name
        /// </summary>
        [JsonProperty("application_name")]        
        public string ApplicationName { get; set; }

        /// <summary>
        /// Controllers
        /// </summary>
        [JsonProperty("controllers")]
        public List<ControllerItem> Controllers
        {
            get;
            set;
        } = new List<ControllerItem>();

        /// <summary>
        /// Versions
        /// </summary>
        [JsonProperty("versions")]
        public List<VersionItem> Versions
        {
            get;
            set;
        } = new List<VersionItem>();

        /// <summary>
        /// Schemas
        /// </summary>
        [JsonProperty("schema")]
        public List<SchemaItem> Schemas
        {
            get;
            set;
        } = new List<SchemaItem>();
    }
}
