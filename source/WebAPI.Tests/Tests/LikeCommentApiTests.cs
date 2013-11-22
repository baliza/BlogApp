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
    public abstract class LikeCommentApiTests
    {
        private readonly IApiServer _server;

        private const string PostsRelativeUri = "api/post/tomatosoup";

        protected LikeCommentApiTests(IApiServer apiServer)
        {
            _server = apiServer;
        }

        [SetUp]
        public void Setup()
        {
            _server.Start();
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
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                Assert.That(httpResponseMessage.IsSuccessStatusCode);
                Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public void CanSuccessfullyGetOneBook()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(valuesUri).Result;
                dynamic result = httpResponseMessage.Content.ReadAsAsync<ExpandoObject>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(1));
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
    }
}
