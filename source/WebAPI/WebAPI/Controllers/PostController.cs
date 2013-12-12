using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Domain;
using Core.Services;




using System.Data;



using System.Web;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{

    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {


        IPostService postsService;


        public PostController(IPostService PostService)
        {
            postsService = PostService;
        }

        // Get api/Post/
        public async Task<HttpResponseMessage> GetAll()
        {
            var listOfPosts = await postsService.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, listOfPosts);
        }
        
        
        // Get api/Post/Tag/{value}
        [Route("tag/{value}")]
        public async Task<HttpResponseMessage> GetByTag(string value)
        {
            var listOfPosts = await postsService.GetPostByTag(value);
            if (listOfPosts == null
                   || listOfPosts.Count() == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, listOfPosts);
        }

        // Get api/Post/Author/{value}
        [Route("Author/{value}")]
        public async Task<HttpResponseMessage> GetByAuthor(string value)
        {
            var listOfPosts = await postsService.GetPostByAuthor(value);
            if (listOfPosts == null 
                || listOfPosts.Count() == 0)            
                return Request.CreateResponse(HttpStatusCode.NotFound);
            
            return Request.CreateResponse(HttpStatusCode.OK, listOfPosts);
        }

        // Get api/Post/{permalink}
        [Route("{permalink}")]
        public async Task<HttpResponseMessage> GetByPermalink(string permalink)
        {
            var post = await postsService.GetPostByPermalinkAsync(permalink);
            if (post == null)            
                return Request.CreateResponse(HttpStatusCode.NotFound);
            
            return Request.CreateResponse(HttpStatusCode.OK, post);
        }


        // Get api/Post/{permalink}/Comments
        [Route("{permalink}/Comments")]
        public async Task<HttpResponseMessage> GetComments(System.String permalink)
        {
            var post = await postsService.GetPostByPermalinkAsync(permalink);
            if (post == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, post.Comments);            
        }

        // Post api/Post/
        public async Task<HttpResponseMessage> NewPost(Post newPost)
        {
          
            if (!!!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            if ((newPost == null)
                || (string.IsNullOrEmpty(newPost.Title))
                || (string.IsNullOrEmpty(newPost.Author))
                || (string.IsNullOrEmpty(newPost.Body)))            
                 return Request.CreateResponse(HttpStatusCode.BadRequest);
                
            

            var chars = newPost.Title
                .ToCharArray()
                .Where(c =>
                    Char.IsLetterOrDigit(c) ||
                    Char.IsWhiteSpace(c))
                .ToArray();

            var permalink = new string(chars);
            newPost.Permalink = permalink.ToLower()
                            .Replace(' ', '_')
                            .Replace("_a_", "_")
                            .Replace("_of_", "_")
                            .Replace("_and_", "_")
                            .Replace("_the_", "_")
                            .Replace("__", "_");

            await postsService.InsertPostAsync(newPost);

            return Request.CreateResponse(HttpStatusCode.Created, newPost.Permalink);

        }

        
        
        // Post api/Post/{permalink}/Comment/
        [Route("{permalink}/Comments")]
        public async Task<HttpResponseMessage> AddComment(string permalink, [FromBody] Comment newComment)
        {
            if (!!!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
                                              
            if (string.IsNullOrEmpty(newComment.Name)) newComment.Name = "Anonymus";            

            var result = await postsService.InsertCommentAsync(permalink, newComment);
            if (result)
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

     
        // Put api/Post/{permalink}/Comment/Like
        [HttpPut]
        [Route("{permalink}/Comments/{id}/Like")]
        public async Task<HttpResponseMessage> IncreaseLike(string permalink, int id)
        {
            try
            {
                var likes = await postsService.LikeComment(permalink, id);
                if (likes < 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, likes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
