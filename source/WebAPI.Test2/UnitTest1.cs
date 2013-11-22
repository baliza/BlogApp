using System;


using System.Net;
using System.Net.Http;

using NUnit.Framework;

using System.Dynamic;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Threading;



namespace WebAPI.Test2
{

    public class BaseTest
    {
        protected HttpSelfHostServer Server { get; set; }

        [SetUp]
        public void SetUp()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            Server = new HttpSelfHostServer(config);
            Server.OpenAsync().Wait();
        }

        [TearDown]
        public void TearDown()
        {
            Server.CloseAsync();
            Server.Dispose();
        }
    }
    [TestFixture]
    public class WhenWorkingWithProducts : BaseTest
    {
        [Test]
        public void ThenShouldGetAllProductsSuccessfully()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, @"http://localhost:8080/api/post/");
            CancellationToken cancellationToken = new CancellationToken();

            using (var client = new HttpClient(Server))
            {
                var response = client.SendAsync(request, cancellationToken);
                response.Wait();
                var result = response.Result;
                Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            }
        }
    }
}