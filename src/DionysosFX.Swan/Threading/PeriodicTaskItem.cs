using System;
using System.Threading;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Threading
{
    /// <summary>
    /// 
    /// </summary>
    internal class PeriodicTaskItem
    {
        /// <summary>
        /// 
        /// </summary>
        private Action Action
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        private CancellationToken CancellationToken
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        private int PeriodSecond
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="periodSecond"></param>
        /// <param name="cancellationToken"></param>
        public PeriodicTaskItem(Action action, int periodSecond, CancellationToken cancellationToken = default)
        {
            Action = action;
            PeriodSecond = periodSecond;

            if (cancellationToken == default)
                cancellationToken = new CancellationToken();
            else
                CancellationToken = cancellationToken;

            DoWork();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void DoWork()
        {
            try
            {
                while (!CancellationToken.IsCancellationRequested)
                {
                    Action();
                    await Task.Delay((int)TimeSpan.FromSeconds(PeriodSecond).TotalMilliseconds, CancellationToken);
                }
            }
            catch (Exception)
            {
                
            }           
        }
    }
}
