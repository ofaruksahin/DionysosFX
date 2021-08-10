using Newtonsoft.Json;
using RestSharp;
using System;

namespace DionysosFX.Module.Test
{
    public class BaseTest
    {
        public string Url => "http://localhost:1923";

        public IRestResponse Get(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            return client.Execute(request);
        }

        public IRestResponse Post(string url, object data)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return client.Execute(request);
        }

        public IRestResponse Patch(string url, object data)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return client.Execute(request);
        }
    }
}
