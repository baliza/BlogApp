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
using System.Net.Http.Headers;



namespace WebAPI.Tests.Tests
{
    public abstract class LikeComment: PostBaseTest
    {


        private const string PostsRelativeUri = "api/post/{permalink}/Comments/{comment_ordinal}/Like";

        protected LikeComment(IApiServer apiServer)
            : base(apiServer)
        { }

        [Test]
        public void Given_Post_TomatoSoup_Comment_1_When_Like_Then_Returns_Created()
        {
            string permalink = "tomatosoup";
            string comment_ordinal= "1";
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink).Replace("{comment_ordinal}", comment_ordinal));
          
            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Put;            
            HttpResponseMessage response = this.SendRequest(request);

            Assert.That(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Given_Post_TomatoSoup_Comment_0_When_Like_Then_Returns_The_Likes_1()
        {
            string permalink = "tomatosoup";
            string comment_ordinal = "0";
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink).Replace("{comment_ordinal}", comment_ordinal));
            
            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Put;            

            HttpResponseMessage response = this.SendRequest(request);

            Assert.That(response.IsSuccessStatusCode);
            dynamic result = response.Content.ReadAsAsync<int>().Result;
            Assert.That(result, Is.Not.Null);            
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_Post_Xmas_Comment_0_When_Like_Then_Returns_NotFound()
        {
            string permalink = "xmas";
            string comment_ordinal = "0";
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink).Replace("{comment_ordinal}", comment_ordinal));
        

            var newComment = new Comment()
            {
                Email = "edu.lahoz@vodafone.com",
                Body = "this post rocks",
                Name = "Edu the rock"
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Put;

            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public void Given_Post_TomatoSoup_Comment_30_When_Like_Then_Returns_Notfound()
        {
            string permalink = "tomatosoup";
            string comment_ordinal = "30";
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink).Replace("{comment_ordinal}", comment_ordinal));
        
            var newComment = new Comment()
            {
                Email = "edu.lahoz@vodafone.com",
                Body = "this post rocks",
                Name = "Edu the rock"
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Put;

            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
