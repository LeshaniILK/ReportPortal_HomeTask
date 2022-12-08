using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ReportPortal_HomeTask
{
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        public RestRequest restRequest;
        public RestResponse restResponse;
        public RestClient restClient;

        [SetUp]
        public void Setup()
        {
            restClient = new RestClient("https://jsonplaceholder.typicode.com/");
            restRequest = new RestRequest("users", Method.Get);
            Assert.That("" + restRequest.Method + "", Is.EqualTo("Get"));
            restResponse = restClient.Execute(restRequest);
        }

        [Test, Order(1)]
        public void VerifyStatusCode()
        {
            Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Console.WriteLine("Status Code => " + restResponse.StatusCode);
        }

        [Test, Order(2)]
        public void VerifyResponseHeader()
        {
            Assert.That(restResponse.ContentType, Is.EqualTo("application/json"));
            Console.WriteLine("Response Header => " + restResponse.ContentType);
        }

        [Test, Order(3)]
        public void VerifyResponseBody()
        {
            JArray jasonObject = (JArray)JsonConvert.DeserializeObject(restResponse.Content.ToString());
            int Count = jasonObject.Count;
            Assert.That(Count, Is.EqualTo(10));
        }

    }
}