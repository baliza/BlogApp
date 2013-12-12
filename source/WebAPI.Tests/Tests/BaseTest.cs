
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Core.Domain;
using Core.Services;
using Core.Services.Impl;
using WebAPI.Tests.Server;
using System.Net.Http;


namespace WebAPI.Tests.Tests
{
    public class BaseTest
    {
        protected readonly IApiServer _server;


        protected BaseTest(IApiServer apiServer)
        {
            _server = apiServer;
        }

        [SetUp]
        public virtual void Setup()
        {
            _server.Start();

        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }

    }

    public class PostBaseTest : BaseTest
    {


        protected PostBaseTest(IApiServer apiServer)
            : base(apiServer)
        {
        }


        [SetUp]
        public override void  Setup()
        {
            base.Setup();
            Type myType = typeof(WebAPI.Controllers.PostController);
        }

        public HttpResponseMessage SendRequest(HttpRequestMessage request)
        {
            using (var client = new HttpClient(_server.ServerHandler))
            {
                return client.SendAsync(request).Result;
            }

        }

    }
}
