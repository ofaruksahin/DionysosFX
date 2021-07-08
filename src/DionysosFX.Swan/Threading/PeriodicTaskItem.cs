using System;
using System.Threading;

namespace DionysosFX.Swan.Threading
{
    internal class PeriodicTaskItem
    {
        private object SyncLock
        {
            get;
            set;
        }

        private TimerCallback Callback
        {
            get;
            set;
        }

        private int PeriodSecond
        {
            get;
            set;
        }

        private CancellationToken CancellationToken
        {
            get;
            set;
        }

        private Timer Timer
        {
            get;
            set;
        }

        public PeriodicTaskItem(TimerCallback callback,int periodSecond, CancellationToken cancellationToken)
        {
            SyncLock = new();
            Callback = callback;
            PeriodSecond = periodSecond;
            CancellationToken = cancellationToken;

            Timer = new Timer(DoWork, null, 0, (int)TimeSpan.FromSeconds(periodSecond).TotalMilliseconds);
        }

        private void DoWork(object state)
        {
            if (!Monitor.TryEnter(SyncLock))
                return;

            try
            {
                CancellationToken.ThrowIfCancellationRequested();
                Callback(SyncLock);
            }
            catch (Exception)
            {
                Timer.Dispose();
                Timer = null;
            }

            Monitor.Exit(SyncLock);
        }
    }
}
