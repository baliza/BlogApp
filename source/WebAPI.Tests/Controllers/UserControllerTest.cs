//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq;
//using System.Collections.Generic;

//using WebAPI.Controllers;

//using Core.Domain;
//using Core.Services;
//using Core.Services.Impl;

//using Infrastructure.Repositories;

//using Moq;


//namespace WebAPI.Tests
//{
//    [TestClass]
//    public class UserControllerTest
//    {
//        IUserService service;
//        ILogger logger;
//        public UserControllerTest()
//        {
//            logger = new Infrastructure.Services.Log4NetLogger();            
//        }
          
//            public void WhenGetALLIsRequestedAllComeback()
//            {
//                // Arrange
//                var  controller = new UserController(service);

//                // Act
//                IEnumerable<User> result = controller.GetAll();

//                // Assert
//                Assert.IsNotNull(result);
//                Assert.AreEqual(3,  result.Count());
//                Assert.AreEqual("value1", result.ElementAt(0).Id);
//                Assert.AreEqual("value2", result.ElementAt(1).Id);
//            }

//            [TestMethod]
//  public void WhenWeReceiveAPostRequest(){
////http://stackoverflow.com/questions/16842910/having-difficulties-setting-up-a-mock-to-unit-test-webapi-post
//    //Arrange / Given
//    var repository = new Mock<UserMockRepository>();
//    var request = new Mock<User>();
//    request.Setup ( rq => rq.ToString() )
//           .Returns ( "This is valid json ;-)" );
//    request.Setup ( rq => rq.Id )
//           .Returns ( "user5");
//    request.Setup ( rq => rq.Password )
//           .Returns ( "password5");
//  service = new Core.Services.Impl.UserService(repository.Object, logger);
//   var controller = new UserController( service );



//   //Act / When
//   int actual = controller.Insert( request.Object );


//   //Assert / Verify
//   // - then we add the request to the repository
//   repository.Verify( 
//     repo => repo.AddRequest( request, Times.Once() );
//   // - then we add the two days (from above setup) in the request to the repository
//   repository.Verify( 
//     repo => repo.AddRequestDays( It.IsAny<IDay>(), Times.Exactly( 2 ));
//   // - then we receive a count indicating we successfully processed the request
//   Assert.NotEqual( -1, actual );
//            }

//            [TestMethod]
//            public void GetByTag()
//            {
//                // Arrange
//                var controller = new PostController(service);

//                // Act
//                var result = controller.GetByTag("Soup");

//                // Assert
//                Assert.AreEqual("value", result);
//            }
        
//        }
//    }
