namespace DionysosFX.ProjectTemplate.WebSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateStartup();
        }

        private static void CreateStartup()
        {
            DionysosFX.Host.Host host = new DionysosFX.Host.Host();
            var startup = host.CreateStartup<Startup>();
            startup.Configure();
            startup.Build();
        }
    }
}
