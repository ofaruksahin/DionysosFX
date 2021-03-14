using System.Runtime.InteropServices;

namespace DionysosFX.Shared
{
    public static class DionysosFXRuntime
    {
        public static bool IsWindows
        {
            get => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinux
        {
            get => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static bool IsOSX
        {
            get => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        public static bool IsFreeBSD
        {
            get => RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);
        }
    }
}
