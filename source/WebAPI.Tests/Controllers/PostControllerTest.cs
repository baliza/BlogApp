//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq;
//using System.Collections.Generic;

//using WebAPI.Controllers;

//using Core.Domain;
//using Core.Services;
//using Core.Services.Impl;

//using Moq;
//using System.Web.Http;
//using System.Net.Http;
//using System.Net;
//using System.Web.Http.SelfHost;
//using System.Threading;




//namespace WebAPI.Tests
//{
//    [TestClass]
//    public class PostControllerTest
//    {
        
        
//        Post[] collection;
//        public PostControllerTest()
//        {
//            collection = new Post[] 
//            { 
//                new Post { Body = "Tomato Soup", Author = "Groceries", Permalink="Tomato-Soup", Tags = new string[]{"tomato", "soup"}, Date= DateTime.Now }, 
//                new Post { Body = "Yo-yo the game", Author = "Toys", Permalink = "Yo-yo-the-game", Date= DateTime.Now}, 
//                new Post { Body = "Hammer", Author = "Hardware", Permalink = "Hamemr", Comments= new string[]{"this is a tool", "I like hammers too"}, Date= DateTime.Now}, 
//                new Post(),
//            };          
//        }

//        public void When_Get_All_Is_Requested_Then_3_Post_Should_Return()
//        {
//            var valueServiceMock = new Mock<IPostService>();
//            valueServiceMock.Setup(service => service.GetAll()).Returns(collection);

//            var controller = new PostController(valueServiceMock.Object);
//            var values = controller.GetAll();

//            Assert.AreEqual(values.Count(), 3);
//        }

//        [TestMethod]
//        public void When_Get_All_Is_Requested_Then_All_Permalink_Are_Not_Empty()
//        {

//            var serviceMock = new Mock<IPostService>();
//            serviceMock
//                .Setup(service => 
//                    service.GetAll())
//                .Returns(collection);


//            var controller = new PostController(serviceMock.Object);
//            var values = controller.GetAll();
//            Assert.AreEqual(values.ElementAt(0).Permalink, "Tomato-Soup");
//            Assert.AreEqual(values.ElementAt(1).Permalink, "Yo-yo-the-game");
//            Assert.AreEqual(values.ElementAt(2).Permalink, "Hamemr");
//        }
//        [TestMethod]
//        public void When_We_Receive_A_Post_Request()
//        {

//  //Arrange / Given

//            var request = new Post()
//            {
//                Author = "Edu",
//                Comments = new string[] { "Monday is the worst day of the week Comments", "this is a Tuesday Comment", "I like weekends" },
//                Tags = new string[] { "Monday", "weekdays", "coffee" },
//                Title = "Monday Post ;) Take a rest",
//                Body = "This is valid a Body for a Monday Post but we need coffee ;-) Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel interdum arcu, eu condimentum justo. Quisque varius feugiat tellus a feugiat. Suspendisse non consectetur leo, vel gravida sem. In enim massa, varius quis auctor sit amet, accumsan sed enim. Nulla fringilla tellus eget condimentum mattis. Fusce vulputate, velit sed tempus suscipit, nunc lorem posuere ante, ac iaculis mauris nulla sed sem. Cras euismod velit a facilisis tincidunt. Donec quis diam non sem sodales egestas ac nec quam. Mauris iaculis, ipsum a ultricies adipiscing, lectus risus dapibus ante, ac volutpat ipsum eros at odio. Sed eros libero, vehicula nec felis ut, sagittis commodo purus. Integer risus elit, venenatis eget mi et, consequat pharetra quam. Morbi nibh quam, bibendum at pulvinar in, cursus quis ipsum. Sed sollicitudin nisl leo, ut lacinia velit sagittis vel. Cras at metus eget erat pellentesque ultricies sed ultrices justo. Duis nisl erat, gravida vel dapibus eu, rhoncus sed elit. Mauris laoreet imperdiet augue at aliquet."
//            };
//            Action<Post> addPost = delegate(Post p)
//            {
//                collection.SetValue(p, collection.Length - 1);
//            };
           
//            var serviceMock = new Mock<IPostService>();
//            serviceMock
//             .Setup(service =>
//                 service.InsertEntry(request))
//                 .Callback(addPost); 
                 
          

//            var controller = new PostController(serviceMock.Object);
//            //Act / When
//            var response = controller.SavePost(request);
//            Assert.AreNotEqual(response, null);
//            Assert.AreNotEqual(response.IsSuccessStatusCode, false);
//            //Assert / Verify
//            // - then we add the request to the repository
//            var permalink = ((StringContent)response.Content).ToString();
//            serviceMock
//             .Setup(service =>
//                 service.GetPostByPermalink(request.Permalink))
//             .Returns(collection.FirstOrDefault(p =>
//                 p.Permalink == request.Permalink));

//            var result = controller.GetByPermalink(request.Permalink);
            
//            // - then we receive a count indicating we successfully processed the request
//            Assert.IsNotNull(result);
//            Assert.AreEqual(result.GetType().Name, "OkNegotiatedContentResult`1");
//            var OkFound = (System.Web.Http.Results.OkNegotiatedContentResult<Post>)result;
//            Assert.AreNotEqual(OkFound.Content, null);
//            Assert.AreEqual(request.Permalink, OkFound.Content.Permalink);
//        }
//        [TestMethod]
//        public void When_Get_All_Is_Requested_Then_Only_One_Tags_Is_Not_Empty_And_2_Are_empty()
//        {
//            var valueServiceMock = new Mock<IPostService>();
//            valueServiceMock.Setup(service => service.GetAll()).Returns(collection);

//            var controller = new PostController(valueServiceMock.Object);
//            var values = controller.GetAll();
//            Assert.AreEqual(values.ElementAt(0).Tags.Count(), 2);
//            Assert.AreEqual(values.ElementAt(1).Tags, null);
//            Assert.AreEqual(values.ElementAt(2).Tags, null);
//        }

//        [TestMethod]
//        public void When_GetByTag_Hamemr_OK_Is_The_response()
//        {
//            string permalink = "Hamemr";
//            var valueServiceMock = new Mock<IPostService>();

//            valueServiceMock.Setup(service => service.GetPostByPermalink(permalink))
//               .Returns(collection.FirstOrDefault(p => p.Permalink == permalink));
            

//            var controller = new PostController(valueServiceMock.Object);
//            var result = controller.GetByPermalink(permalink);

//            Assert.IsNotNull(result);
//            Assert.AreEqual(result.GetType().Name, "OkNegotiatedContentResult`1");
//            var Ok = (System.Web.Http.Results.OkNegotiatedContentResult<Post>)result;
//            Assert.AreNotEqual(Ok.Content, null);            
//        }


//        [TestMethod]
//        public void When_GetByTag_Hola_Not_Found_Is_The_response()
//        {
//            string permalink = "Hola";
//            var valueServiceMock = new Mock<IPostService>();

//            valueServiceMock
//                .Setup(service => 
//                    service.GetPostByPermalink(permalink))
//                .Returns(collection.FirstOrDefault(p => 
//                    p.Permalink == permalink));

//            var controller = new PostController(valueServiceMock.Object);
//            var result = controller.GetByPermalink(permalink);
//            Assert.IsNotNull(result);
//            Assert.AreEqual(result.GetType().Name, "NotFoundResult");
//        }

//        //[TestMethod]
//        //public void When_Insert_NewPost_OK_Is_The_response()
//        //{
//        //    Post post = new Post() { 
//        //    Body= "",
//        //    Author="edu",
//        //    Comments = new string[] { "comment 1", "comment 2", "comment 3"},
//        //    Tags = new string[] { "tag 1", "tag 2", "tag 3" },
//        //    Title ="this is the post's title"
//        //    };
//        //    var valueServiceMock = new Mock<IPostService>();

//        //    valueServiceMock
//        //        .Setup(service => 
//        //            service.InsertEntry(post))
//        //        .Callback(collection.SetValue(post, 3));

//        //    var controller = new PostController(valueServiceMock.Object);
//        //    var result = controller.GetByPermalink(permalink);
//        //    Assert.IsNotNull(result);
//        //    Assert.AreEqual(result.GetType().Name, "NotFoundResult");
//        //}
//    }
//}
