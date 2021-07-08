using System.Threading;

namespace DionysosFX.Swan.Threading
{
    public static class PeriodicTask
    {
        public static void Create(TimerCallback callback, int periodSecond, CancellationToken token = default)
        {
            new PeriodicTaskItem(callback, periodSecond, token);
        }
    }
}
