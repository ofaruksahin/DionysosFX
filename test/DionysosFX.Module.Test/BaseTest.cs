using DionysosFX.Swan.Extensions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DionysosFX.Module.Test
{
    public class BaseTest
    {
        public string Url => "http://localhost:1923";
        public Dictionary<string,string> Headers = new Dictionary<string, string>();

        public IRestResponse Get(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            foreach (var header in Headers)
                request.AddHeader(header.Key, header.Value);
            return client.Execute(request);
        }

        public IRestResponse Post(string url, object data)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            foreach (KeyValuePair<string, string> header in Headers)
                request.AddHeader(header.Key, header.Value);
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
            foreach (KeyValuePair<string, string> header in Headers)
                request.AddHeader(header.Key, header.Value);
            var body = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return client.Execute(request);
        }

        public IRestResponse PatchWithFormData(string url, object data)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.PATCH);
            request.AlwaysMultipartFormData = true;
            var formData = data.ToFormData();
            foreach (var item in formData)
                request.AddParameter(item.Item1, item.Item2);
            foreach (KeyValuePair<string, string> header in Headers)
                request.AddHeader(header.Key, header.Value);
            return client.Execute(request);
        }

        public IRestResponse Delete(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            foreach (KeyValuePair<string, string> header in Headers)
                request.AddHeader(header.Key, header.Value);
            return client.Execute(request);
        }
    }
}
