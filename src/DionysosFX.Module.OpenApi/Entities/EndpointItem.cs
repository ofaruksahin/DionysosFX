using DionysosFX.Swan.Routing;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class EndpointItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpVerb Verb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Versions { get; set; } = new List<string>();

    }
}
