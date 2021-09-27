using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi
{
    /// <summary>
    /// OpenAPI Options
    /// </summary>
    public class OpenApiModuleOptions
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public bool EnableBearerToken { get; set; }

        private List<string> _headers;

        public List<string> Headers
        {
            get => _headers ?? (_headers = new List<string>());
        }        
    }
}
