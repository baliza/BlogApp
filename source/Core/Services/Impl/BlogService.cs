using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Impl
{
    internal class BlogService : IBlogService 
    {        
        #region Properties

        /// <summary>
        /// Gets or sets the post repository.
        /// </summary>
        /// <value>The post repository.</value>
        protected IRepository<User> userRepos { get; private set; }

        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        /// <value>The user repository.</value>
        protected IRepository<Post> postsRepos { get; private set; }


        /// <summary>
        /// Gets or sets the Logger.
        /// </summary>
        /// <value>The logger.</value>
        private readonly ILogger logger;
        #endregion


        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService"/> class.
        /// </summary>
        /// <param name="postRepository">The post repository.</param>
        /// <param name="userRepository">The user trial repository.</param>
        /// /// <param name="loggerService">The Logger Service.</param>
        public BlogService(IRepository<Post> postRepository,
                           IRepository<User> userRepository, 
                            ILogger loggerService)
        {
            userRepos = userRepository;
            postsRepos = postRepository;
            logger = loggerService;
        }


        public IList<Post> GetAllPost()
        {
            return postsRepos.GetAll().ToList();
        }

        public IList<Post> GetPostByTag(string tag)
        {
            var result = postsRepos.GetAll().Where(
                (p) =>
                    (p.Tags != null) && p.Tags.Contains(tag, new Core.Tools.CaseIgnoringStringComparer())
                    );
            return result.ToList();
        }

        public Post GetPostByPermalink(string tag)
        {
            var result = postsRepos.GetAll().FirstOrDefault(
                (p) =>
                    (p.Tags != null) && p.Tags.Contains(tag, new Core.Tools.CaseIgnoringStringComparer())
                    );
            return result;
        }
        #endregion
    }
}
