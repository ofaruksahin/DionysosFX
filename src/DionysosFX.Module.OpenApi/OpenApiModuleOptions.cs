namespace DionysosFX.Module.OpenApi
{
    public class OpenApiModuleOptions
    {
        public string ApplicationName { get; set; }

        public OpenApiModuleOptions(string ApplicationName)
        {
            this.ApplicationName = ApplicationName;
        }
    }
}
