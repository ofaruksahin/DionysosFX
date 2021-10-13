using System;

namespace DionysosFX.Module.HealthCheck
{
    public interface HealthCheckResponse
    {
        DateTime StartDate
        {
            get;
            set;
        }

        DateTime EndDate
        {
            get;
            set;
        }

        bool IsHealthily
        {
            get;
        }

        double TotalMilliSecond
        {
            get;
        }

        string Message
        {
            get;
            set;
        }
    }
}
