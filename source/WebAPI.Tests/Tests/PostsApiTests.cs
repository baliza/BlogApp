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
using System.Linq;
using System.Collections.Generic;



namespace WebAPI.Tests.Test
{
    public abstract class PostsApiTests
    {
        private readonly IApiServer _server;

        private const string PostsRelativeUri = "api/post/";

        protected PostsApiTests(IApiServer apiServer)
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
        public void When_GetAll_Returns_Successful_StatusCode()
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
        public void When_GetAll_Returns_3_Post()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<IList<Post>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Count(result), Is.EqualTo(3));
                
            }
        }


        [Test]
        public void When_GetAll_The_2nd_Is_Yoyo_the_game()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<IList<Post>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(result[1].Permalink, Is.EqualTo("Yoyo_the_game"));
            }
        }
    }
}
