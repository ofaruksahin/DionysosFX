namespace DionysosFX.Module.OpenApi
{
    public class OpenApiOptions
    {
        public string ApplicationName { get; set; }

        public OpenApiOptions(string ApplicationName)
        {
            this.ApplicationName = ApplicationName;
        }
    }
}
