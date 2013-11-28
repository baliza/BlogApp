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
using System.Collections.Generic;
using System.Linq;



namespace WebAPI.Tests.Tests
{
    public abstract class GetPostsByTag : PostBaseTest
    {
       

        private const string PostsRelativeUri = "api/post/tag/";

        protected GetPostsByTag(IApiServer apiServer)
            : base(apiServer)
        {
            
        }
      

        [Test]
        public void Given_stuff_Is_The_Tag_When_Get_Post_Then_Returns_OK()
        {
            var tag = "stuff";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri,tag));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public void Given_stuff_Is_The_Tag_When_Get_Post_Then_Returns_2_Posts()
        
        {
            var tag = "stuff";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri, tag));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<IList<Post>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Count(result), Is.EqualTo(2));
            }
        }


        [Test]
        public void Given_food_Is_The_Tag_When_Get_Post_Then_Returns_OK()
        {
            var tag = "food";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri, tag));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
        [Test]
        public void Given_Food_Is_The_Tag_When_Get_Post_Then_Returns_1_Post()
        {
            var tag = "food";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri, tag));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<IList<Post>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Count(result), Is.EqualTo(1));
            }
        }
        [Test]
        [Description("This test attempts to get and unexiting tag.")]
        public void Given_Xmas_Is_The_Tag_When_Get_Post_Then_Returns_NotFound()
        {
            var tag = "Xmas";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri, tag));            
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.IsFalse(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            }
        }
       
    }
}
