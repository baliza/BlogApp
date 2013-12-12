using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Domain;
using Core.Services;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {

         IUserService repository;        


        public UserController(IUserService UserService)
        {
            repository = UserService;            
        }


        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }

       
        [HttpGet]
        [ActionName("Exists")]
        public IHttpActionResult Exists(string value)
        {
            var result = repository.Exists(value);
            //if (result == null)
            //{
            //    return NotFound();
            //}
            return Ok(result);
        }


        [HttpPost]
        [ActionName("Insert")]
        public bool Insert(User user)
        {
            var result = repository.Insert(user);            
            return result;
        }

        [HttpPost]
        [ActionName("Update")]
        public bool Update(User user)
        {            
            var result = repository.Update(user);
            return result;
        }

        // POST api/Post
        public HttpResponseMessage Post(User user)
        {
            var userResource = repository.Insert(user);
            var httpResponseMessage = Request.CreateResponse(HttpStatusCode.Created, userResource);
            httpResponseMessage.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));
            return httpResponseMessage;
        }
    }
}
