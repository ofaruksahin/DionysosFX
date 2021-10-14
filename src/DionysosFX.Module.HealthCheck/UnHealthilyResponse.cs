using Newtonsoft.Json;
using System;

namespace DionysosFX.Module.HealthCheck
{
    public class UnHealthilyResponse : HealthCheckResponse
    {
        private DateTime _startDate;

        [JsonProperty("start_date")]
        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        private DateTime _endDate;

        [JsonProperty("end_date")]
        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value;
        }

        [JsonProperty("total_millisecond")]
        public double TotalMilliSecond
        {
            get =>Math.Abs((EndDate - StartDate).TotalMilliseconds);
        }

        private bool _isHealthily = false;

        [JsonProperty("is_healthily")]
        public bool IsHealthily
        {
            get => _isHealthily;
        }

        private string _message;

        [JsonProperty("message")]
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public UnHealthilyResponse(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public UnHealthilyResponse(DateTime startDate, DateTime endDate, string message)
        {
            _startDate = startDate;
            _endDate = endDate;
            _message = message;
        }
    }
}
