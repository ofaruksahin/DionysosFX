using System;
using System.Threading;

namespace DionysosFX.Swan.Threading
{
    public static class PeriodicTask
    {
        public static void Create(TimerCallback callback, int periodSecond, CancellationToken token = default)
        {
            //new PeriodicTaskItem(callback, periodSecond, token);
        }

        public static void Create(Action callback, int periodSecond, CancellationToken token = default)
        {
            new PeriodicTaskItem(callback, periodSecond, token);
        }
    }
}
