using RestSharp;

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
    }
}
