using System;


using System.Net;
using System.Net.Http;

using NUnit.Framework;
using Core.Domain;
using Core.Services;
using Core.Services.Impl;

using Infrastructure.Repositories;

using WebAPI.Tests.Test;
using WebAPI.Tests.Server;
using System.Dynamic;



namespace WebAPI.Tests.Test
{
    public abstract class PostApiTests
    {
        private readonly IApiServer _server;

        private const string PostsRelativeUri = "api/post/tomatosoup";

        protected PostApiTests(IApiServer apiServer)
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
        public void When_Get_Existing_Post_Returns_OK()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public void When_GetByPermalink_Returns_the_Correct_Post()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<ExpandoObject>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Permalink, Is.EqualTo("TomatoSoup"));
            }
        }

        [Test]
        public void When_Get_NonExistant_Post_Returns_NotFound()
        {
            var valuesUri = new Uri(_server.BaseAddress, "api/Post/Xmas_day");
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }
}
