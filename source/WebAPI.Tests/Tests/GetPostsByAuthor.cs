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
    public abstract class GetPostsByAuthor : PostBaseTest
    {
       

        private const string PostsRelativeUri = "api/post/Author/";

        protected GetPostsByAuthor(IApiServer apiServer)
            : base(apiServer)
        {
            
        }
      

        [Test]
        public void Given_Toystory_Is_The_Author_When_Get_Post_Then_Returns_OK()
        {
            var Author = "Toystory";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri,Author));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public void Given_Toystory_Is_The_Author_When_Get_Post_Then_Returns_1_Posts()
        
        {
            var Author = "Toystory";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri, Author));
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                dynamic result = response.Content.ReadAsAsync<IList<Post>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Count(result), Is.EqualTo(1));
            }
        }

            
        [Test]
        [Description("This test attempts to get and unexiting Author.")]
        public void Given_Camila_Is_The_Author_When_Get_Post_Then_Returns_NotFound()
        {
            var Author = "Camila";
            var valuesUri = new Uri(_server.BaseAddress, string.Concat(PostsRelativeUri, Author));            
            using (var client = new HttpClient(_server.ServerHandler))
            {
                HttpResponseMessage response = client.GetAsync(valuesUri).Result;
                Assert.IsFalse(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            }
        }
       
    }
}
