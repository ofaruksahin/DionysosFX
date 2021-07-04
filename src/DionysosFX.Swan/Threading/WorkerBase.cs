using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Swan.Threading
{
    public abstract class WorkerBase : IWorker, IDisposable
    {
        public void Dispose()
        {
        }
    }
}
