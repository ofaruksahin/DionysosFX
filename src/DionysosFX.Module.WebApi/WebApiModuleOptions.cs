namespace DionysosFX.Module.WebApi
{
    public class WebApiModuleOptions
    {
        public ResponseType ResponseType
        {
            get;
            set;
        } = ResponseType.Json;

        public WebApiModuleOptions(ResponseType responseType = ResponseType.Json)
        {
            ResponseType = responseType; 
        }
    }
}
