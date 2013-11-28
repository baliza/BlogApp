using System;


using System.Net;
using System.Net.Http;

using NUnit.Framework;
using Core.Domain;
using Core.Services;
using Core.Services.Impl;

using Infrastructure.Repositories;

using WebAPI.Tests.Tests;
using WebAPI.Tests.Server;
using System.Dynamic;



namespace WebAPI.Tests.Tests
{
    public abstract class GetPostByPermalink : PostBaseTest
    {
     

        private const string PostsRelativeUri = "api/post/tomatosoup";

        protected GetPostByPermalink(IApiServer apiServer)
            : base(apiServer)
        { }
      

        [Test]
        public void Given_TomatoSoup_Is_The_Permalink_When_Get_Post_Then_Returns_OK()
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
        public void Given_TomatoSoup_Is_The_Permalink_When_Get_Post_Then_Right_Post()
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
        public void Given_Xmas_Is_The_Permalink_When_Get_Post_Then_Returns_NotFound()
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
