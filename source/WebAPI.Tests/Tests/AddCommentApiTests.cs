using System;


using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using NUnit.Framework;
using Core.Domain;
using Core.Services;
using Core.Services.Impl;

using Infrastructure.Repositories;

using WebAPI.Tests.Test;
using WebAPI.Tests.Server;
using System.Dynamic;
using System.Web.Http.SelfHost;



namespace WebAPI.Tests.Test
{
    public abstract class AddCommentApiTests
    {
        private readonly IApiServer _server;

        private const string PostsRelativeUri = "api/post/tomatosoup";
       
        protected AddCommentApiTests(IApiServer apiServer)
        {
            _server = apiServer;
            
        
        }

        [SetUp]
        public void Setup()
        {
            _server.Start();
            Type myType = typeof(WebAPI.Controllers.PostController);
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }

        [Test]
        public void GetOneBookReturnsSuccessfulStatusCode()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = valuesUri;
                HttpResponseMessage response = client.SendAsync(request).Result;
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.IsSuccessStatusCode);
            }
        }

        [Test]
        public void CanSuccessfullyGetOneBook()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);

            using (var client = new HttpClient(_server.ServerHandler))
            {
                var request = new HttpRequestMessage();

                request.RequestUri = valuesUri;
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                request.Method = HttpMethod.Get;
                HttpResponseMessage response = client.SendAsync(request).Result;
                dynamic result = response.Content.ReadAsAsync<ExpandoObject>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Permalink, Is.EqualTo("TomatoSoup"));
            }
        }

        [Test]
        public void GetNonExistantBookReturnsNotFound()
        {
            var valuesUri = new Uri(_server.BaseAddress, "api/books/203");
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

 //        [Test]
 //          public void Post_Drink_Log_With_No_Drink_Failure()
 //{
 //var baseAddress = "http://localhost:8080/";

 //var selfHostConfig = new HttpSelfHostConfiguration(baseAddress);

 ////selfHostConfig.Filters.Add(new ValidationActionFilter());
 //selfHostConfig.Routes.MapHttpRoute(
 //name: "DefaultApi",
 //routeTemplate: "api/{controller}/{id}",
 //defaults: new { id = RouteParameter.Optional }
 //);

 //selfHostConfig.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

 //var container = IoC.Initialize();
 //var resolver = new SmDependencyResolver(container);

 //selfHostConfig.ServiceResolver.SetResolver(resolver.GetService, resolver.GetServices);

 //var server = new HttpSelfHostServer(selfHostConfig);
 //var client = new HttpClient();
 //server.OpenAsync().Wait();

 //client.BaseAddress = new Uri(baseAddress);

 //var newLog = new DrinkLog { PersonId = 1, LogDate = DateTime.UtcNow };

 //var postData = new StringContent(JsonConvert.SerializeObject(newLog), Encoding.UTF8, "application/json");

 //var r = client.PostAsync("api/drinklogs";, postData);

 //Assert.That(r.Result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

 //}

 //}
 //}
    }
}
