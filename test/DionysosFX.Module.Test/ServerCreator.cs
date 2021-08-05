using DionysosFX.Host;

namespace DionysosFX.Module.Test
{
    public static class ServerCreator
    {
        public static void Create(IStartup startup)
        {
            startup.Configure();
            startup.Build();
        }
    }
}
