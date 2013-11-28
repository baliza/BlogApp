using System;


using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using NUnit.Framework;
using Core.Domain;
using Core.Services;
using Core.Services.Impl;

using Infrastructure.Repositories;

using WebAPI.Tests.Tests;
using WebAPI.Tests.Server;
using System.Dynamic;
using System.Web.Http.SelfHost;
using System.Net.Http.Formatting;
using System.Collections.Generic;
using System.Linq;



namespace WebAPI.Tests.Tests
{
    public abstract class AddComment : PostBaseTest
    {
        protected AddComment(IApiServer apiServer)
            : base(apiServer)
        { }

        private const string PostsRelativeUri = "api/post/{permalink}/Comments";
      

        [Test]
        public void Given_Post_TomatoSoup_And_NewComment_When_AddComment_Then_Returns_Created()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));

            var newComment = new Comment()
            {
                Email = "edu.lahoz@vodafone.com",
                Body = "this post rocks",
                Name = "Edu the rock"
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.That(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Given_Post_Hammer_And_NewComment_When_AddComment_Then_Post_Has_4_Comments()
        {
            string permalink = "hammer";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));

            using (var client = new HttpClient(_server.ServerHandler))
            {
                var newComment = new Comment()
                {
                    Email = "edu.lahoz@vodafone.com",
                    Body = "this post rocks",
                };
                var request = new HttpRequestMessage();

                request.RequestUri = valuesUri;
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Method = HttpMethod.Post;
                request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

                HttpResponseMessage postResponse = client.SendAsync(request).Result;
                Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

                HttpResponseMessage getResponse = client.GetAsync(valuesUri.ToString()).Result;
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                dynamic result = getResponse.Content.ReadAsAsync<IList<Comment>>().Result;
                Assert.That(result, Is.Not.Null);
                Assert.That(Enumerable.Count(result), Is.EqualTo(4));
            }
        }

        [Test]
        public void Given_Post_TomatoSoup_And_NewPost_With_No_Email_When_AddComment_Then_Returns_BadRequest()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));

       
            var newComment = new Comment()
            {
                Body = "this post rocks",
                Name = "Edu the rock"
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }
        [Test]
        public void Given_Post_TomatoSoup_And_NewPost_With_No_Body_When_AddComment_Then_Returns_BadRequest()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));

            var newComment = new Comment()
            {
                Email= "edu.lahoz@vodafone.com",                
                Name = "Edu the rock"
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);


            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }
        [Test]
        public void Given_Post_TomatoSoup_And_NewPost_With_No_Name_When_AddComment_Then_Returns_Created()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));
       
            var newComment = new Comment()
            {
                Email = "edu.lahoz@vodafone.com",
                Body = "this post rocks",
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.That(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }
   
        [Test]
        public void Given_Post_TomatoSoup_And_NewPost_With_No_Name_When_AddComment_Then_Returns_Name_As_Anonymus()
        {
            string permalink = "tomatosoup";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));

            using (var client = new HttpClient(_server.ServerHandler))
            {
                var newComment = new Comment()
                {
                    Email = "edu.lahoz@vodafone.com",
                    Body = "this post rocks",
                };
                var request = new HttpRequestMessage();

                request.RequestUri = valuesUri;
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Method = HttpMethod.Post;
                request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

                HttpResponseMessage postResponse = client.SendAsync(request).Result;
                Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

                HttpResponseMessage getResponse = client.GetAsync(valuesUri.ToString()).Result;
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                dynamic result = getResponse.Content.ReadAsAsync<IList<Comment>>().Result;
                Assert.That(result, Is.Not.Null);                
                Assert.That(result[Enumerable.Count(result) - 1].Name.ToLower(), Is.EqualTo("anonymus"));
            }
        }
      
        [Test]
        public void Given_Post_Xmas_When_Add_NewComment_Then_Returns_NotFound()
        {
            string permalink = "xmas";

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri.Replace("{permalink}", permalink));

       
            var newComment = new Comment()
            {
                Email = "edu.lahoz@vodafone.com",
                Body = "this post rocks",
                Name = "Edu the rock"
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Comment>(newComment, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }


    }
}
