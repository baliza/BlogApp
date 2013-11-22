using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.SelfHost;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Threading;

using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace UnitTestProject1
{
    [TestClass]
    public class WebApiIntegrationTests : IDisposable
    {
        private HttpServer _server;
        private string _url = "http://localhost/";

        public WebApiIntegrationTests()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });
            //config.Routes.MapHttpRoute(name: "Action", routeTemplate: "api/{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            //config.MessageHandlers.Add(new WebApiKeyHandler());

            _server = new HttpServer(config);
        }

        [TestMethod]
        public void GetAll()
        {
            var client = new HttpClient(_server);
            var request = createRequest("api/Post", "application/json", HttpMethod.Get);


            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.AreNotEqual(response.Content, null);
            //    Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
            //    Assert.Equal(3, response.Content.ReadAsAsync<IQueryable<Url>>().Result.Count());
            //    Assert.Equal(60, response.Headers.CacheControl.MaxAge.Value.TotalSeconds);
            //    Assert.Equal(true, response.Headers.CacheControl.MustRevalidate);
            //    Assert.Equal(expectedJson, response.Content.ReadAsStringAsync().Result);
            }

            request.Dispose();
        }

      
        [TestMethod]
        public void GetTomatoSoup()
        {
            var client = new HttpClient(_server);
            var request = createRequest("api/post/tomatosoup", "application/json", HttpMethod.Get);

            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.AreNotEqual(response.Content, null);
                //Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
                //Assert.Equal("You can't use the API without the key.", response.Content.ReadAsStringAsync().Result);
            }

            request.Dispose();
        }
       

        private HttpRequestMessage createRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(_url + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

        private HttpRequestMessage createRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = createRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        public void Dispose()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
        }
    }
}