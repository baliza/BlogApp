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
using System.Linq;
using System.Collections.Generic;



namespace WebAPI.Tests.Tests
{
    public abstract class GetComments : PostBaseTest
    {


        private const string PostsRelativeUri = "api/post/{permalink}/Comments";

        protected GetComments(IApiServer apiServer)
            : base(apiServer)
        {
            
        }

        [Test]
        public void Given_TomatoSoup_When_GetComments_Then_Returns_OK()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public void Given_Tomatosoup_When_Get_Then_Returns_3_Post()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<IList<Comment>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Count(result), Is.EqualTo(3));

            }
        }


        [Test]

        public void Given_Xmas_When_GetComments_Then_Returns_OK()
        {
            string permalink = "Xmas";
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.IsFalse(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }
}
