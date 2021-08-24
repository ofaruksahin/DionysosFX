using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class ControllerItem
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
        public List<EndpointItem> Endpoints { get; set; } = new List<EndpointItem>();
    }
}
