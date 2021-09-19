namespace DionysosFX.Module.WebSocket.Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateStartup();
        }


        private static void CreateStartup()
        {
            Host.Host host = new Host.Host();
            var startup = host.CreateStartup<Startup>();
            startup.Configure();
            startup.Build();
        }
    }
}
