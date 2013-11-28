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
using System.Net.Http.Formatting;
using System.Net.Http.Headers;



namespace WebAPI.Tests.Tests
{
    public abstract class AddPost : PostBaseTest
    {
        protected AddPost(IApiServer apiServer)
            : base(apiServer)
        { }
      
        private const string PostsRelativeUri = "api/post/";


        [Test]
        public void Given_NewPost_When_Add_Then_Returns_Created()
        {
            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);

            var newPost = new Post()
            {
                Author = "Edu",
                Tags = new string[] { "Monday", "weekdays", "coffee" },
                Title = "Monday Post ;) Take a rest",
                Body = "This is valid a Body for a Monday Post but we need coffee ;-) Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel interdum arcu, eu condimentum justo. Quisque varius feugiat tellus a feugiat. Suspendisse non consectetur leo, vel gravida sem. In enim massa, varius quis auctor sit amet, accumsan sed enim. Nulla fringilla tellus eget condimentum mattis. Fusce vulputate, velit sed tempus suscipit, nunc lorem posuere ante, ac iaculis mauris nulla sed sem. Cras euismod velit a facilisis tincidunt. Donec quis diam non sem sodales egestas ac nec quam. Mauris iaculis, ipsum a ultricies adipiscing, lectus risus dapibus ante, ac volutpat ipsum eros at odio. Sed eros libero, vehicula nec felis ut, sagittis commodo purus. Integer risus elit, venenatis eget mi et, consequat pharetra quam. Morbi nibh quam, bibendum at pulvinar in, cursus quis ipsum. Sed sollicitudin nisl leo, ut lacinia velit sagittis vel. Cras at metus eget erat pellentesque ultricies sed ultrices justo. Duis nisl erat, gravida vel dapibus eu, rhoncus sed elit. Mauris laoreet imperdiet augue at aliquet."
            };

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Post>(newPost, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.That(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));


        }
        [Test]
        public void Given_NewPost_When_Add_Then_Permalink_Is_monday_post_take_rest()
        {

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);

            var newPost = new Post()
            {
                Author = "Edu",
                Tags = new string[] { "Monday", "weekdays", "coffee" },
                Title = "Monday Post ;) Take a rest",
                Body = "This is valid a Body for a Monday Post but we need coffee ;-) Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel interdum arcu, eu condimentum justo. Quisque varius feugiat tellus a feugiat. Suspendisse non consectetur leo, vel gravida sem. In enim massa, varius quis auctor sit amet, accumsan sed enim. Nulla fringilla tellus eget condimentum mattis. Fusce vulputate, velit sed tempus suscipit, nunc lorem posuere ante, ac iaculis mauris nulla sed sem. Cras euismod velit a facilisis tincidunt. Donec quis diam non sem sodales egestas ac nec quam. Mauris iaculis, ipsum a ultricies adipiscing, lectus risus dapibus ante, ac volutpat ipsum eros at odio. Sed eros libero, vehicula nec felis ut, sagittis commodo purus. Integer risus elit, venenatis eget mi et, consequat pharetra quam. Morbi nibh quam, bibendum at pulvinar in, cursus quis ipsum. Sed sollicitudin nisl leo, ut lacinia velit sagittis vel. Cras at metus eget erat pellentesque ultricies sed ultrices justo. Duis nisl erat, gravida vel dapibus eu, rhoncus sed elit. Mauris laoreet imperdiet augue at aliquet."
            };

            var request = new HttpRequestMessage();
            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Post>(newPost, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.That(response.IsSuccessStatusCode);
            dynamic result = response.Content.ReadAsAsync<string>().Result;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ToLower(), Is.EqualTo("monday_post_take_rest"));

        }

        [Test]
        public void Given_Null_Post_When_Add_Then_Returns_BadRequest()
        {

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);

            var request = new HttpRequestMessage();

            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Post>(null, new JsonMediaTypeFormatter());
            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void Given_Post_With_Empty_Body_When_Add_Then_Returns_BadRequest()
        {

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);
            var newPost = new Post()
           {
               Author = "Edu",
               Tags = new string[] { "Monday", "weekdays", "coffee" },
               Title = "Monday Post ;) Take a rest"
           };

            var request = new HttpRequestMessage();
            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Post>(newPost, new JsonMediaTypeFormatter());


            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        [Test]
        public void Given_Post_With_Empty_Author_When_Add_Then_Returns_BadRequest()
        {

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);

            var newPost = new Post()
           {
               Body = "This is valid a Body for a Monday Post but we need coffee ;-) Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel interdum arcu, eu condimentum justo. Quisque varius feugiat tellus a feugiat. Suspendisse non consectetur leo, vel gravida sem. In enim massa, varius quis auctor sit amet, accumsan sed enim. Nulla fringilla tellus eget condimentum mattis. Fusce vulputate, velit sed tempus suscipit, nunc lorem posuere ante, ac iaculis mauris nulla sed sem. Cras euismod velit a facilisis tincidunt. Donec quis diam non sem sodales egestas ac nec quam. Mauris iaculis, ipsum a ultricies adipiscing, lectus risus dapibus ante, ac volutpat ipsum eros at odio. Sed eros libero, vehicula nec felis ut, sagittis commodo purus. Integer risus elit, venenatis eget mi et, consequat pharetra quam. Morbi nibh quam, bibendum at pulvinar in, cursus quis ipsum. Sed sollicitudin nisl leo, ut lacinia velit sagittis vel. Cras at metus eget erat pellentesque ultricies sed ultrices justo. Duis nisl erat, gravida vel dapibus eu, rhoncus sed elit. Mauris laoreet imperdiet augue at aliquet.",
               Tags = new string[] { "Monday", "weekdays", "coffee" },
               Title = "Monday Post ;) Take a rest",
           };

            var request = new HttpRequestMessage();
            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Post>(newPost, new JsonMediaTypeFormatter());
            
            HttpResponseMessage response = this.SendRequest(request);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));


        }
        [Test]
        public void Given_Post_With_Empty_Title_When_Add_Then_Returns_BadRequest()
        {

            var valuesUri = new Uri(_server.BaseAddress, PostsRelativeUri);

            var newPost = new Post()
            {
                Author = "Edu",
                Body = "This is valid a Body for a Monday Post but we need coffee ;-) Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel interdum arcu, eu condimentum justo. Quisque varius feugiat tellus a feugiat. Suspendisse non consectetur leo, vel gravida sem. In enim massa, varius quis auctor sit amet, accumsan sed enim. Nulla fringilla tellus eget condimentum mattis. Fusce vulputate, velit sed tempus suscipit, nunc lorem posuere ante, ac iaculis mauris nulla sed sem. Cras euismod velit a facilisis tincidunt. Donec quis diam non sem sodales egestas ac nec quam. Mauris iaculis, ipsum a ultricies adipiscing, lectus risus dapibus ante, ac volutpat ipsum eros at odio. Sed eros libero, vehicula nec felis ut, sagittis commodo purus. Integer risus elit, venenatis eget mi et, consequat pharetra quam. Morbi nibh quam, bibendum at pulvinar in, cursus quis ipsum. Sed sollicitudin nisl leo, ut lacinia velit sagittis vel. Cras at metus eget erat pellentesque ultricies sed ultrices justo. Duis nisl erat, gravida vel dapibus eu, rhoncus sed elit. Mauris laoreet imperdiet augue at aliquet.",
                Tags = new string[] { "Monday", "weekdays", "coffee" },
            };

            var request = new HttpRequestMessage();
            request.RequestUri = valuesUri;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = HttpMethod.Post;
            request.Content = new ObjectContent<Post>(newPost, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.SendRequest(request);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }
    }
}
