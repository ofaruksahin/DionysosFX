namespace DionysosFX.Host
{
    /// <summary>
    /// DionysosFX App Instance startup object interface
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// DionysosFX Module, Dependency Injection Configuration Method
        /// </summary>
        void Configure();

        /// <summary>
        /// DionysosFX Module, Dependency Injection Builded and Accessiable defined.
        /// </summary>
        void Build();
    }
}
