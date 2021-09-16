namespace DionysosFX.Module.OpenApi
{
    /// <summary>
    /// OpenAPI Options
    /// </summary>
    public class OpenApiModuleOptions
    {
        /// <summary>
        /// ApplicationName
        /// </summary>
        public string ApplicationName { get; set; }

        public OpenApiModuleOptions(string ApplicationName)
        {
            this.ApplicationName = ApplicationName;
        }
    }
}
