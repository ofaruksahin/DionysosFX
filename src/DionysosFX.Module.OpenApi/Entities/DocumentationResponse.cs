using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class DocumentationResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ControllerItem> Controllers
        {
            get;
            set;
        } = new List<ControllerItem>();
    }
}
