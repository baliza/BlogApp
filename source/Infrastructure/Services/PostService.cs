//using System;
//using System.Linq;
//using System.Collections.Generic;


//using Core.Domain;
//using Core.Services;
//using Core.Repositories;




//namespace Infrastructure.Services
//{
//    internal class PostService : IBlogService
//    {        
//        #region Properties
        
//        /// <summary>
//        /// Gets or sets the post repository.
//        /// </summary>
//        /// <value>The user repository.</value>
//        protected IRepository<Post> repository { get; private set; }


//        /// <summary>
//        /// Gets or sets the Logger.
//        /// </summary>
//        /// <value>The logger.</value>
//        private readonly ILogger logger;
//        #endregion


//        #region ctor

//        /// <summary>
//        /// Initializes a new instance of the <see cref="PostService"/> class.
//        /// </summary>
//        /// <param name="PostRepository">The post repository.</param>
//        /// /// <param name="loggerService">The Logger Service.</param>
//        public PostService(IRepository<Post> PostRepository,                           
//                           ILogger loggerService)
//        {
//            repository = PostRepository;
//            logger = loggerService;
//        }


//        public IList<Post> GetAllPost()
//        {
//            return repository
//                .GetAll()
//                .OrderByDescending(p => p.Date)
//                 .Take(20)
//                .ToList();
//        }

//        public IList<Post> GetPostByTag(string tag)
//        {
//            var result = repository.SearchFor(
//                (p) =>
//                    (p.Tags != null) 
//                    && p.Tags.Contains(tag, new Core.Tools.CaseIgnoringStringComparer()));
//            return result.ToList();
//        }

//        public Post GetPostByPermalink(string permalink)
//        {
//            var result = repository.SearchFor(
//                (p) =>
//                    (p.Tags != null) 
//                    && p.Tags.Contains(permalink, new Core.Tools.CaseIgnoringStringComparer())
//                    ).FirstOrDefault();
//            return result;
//        }
//        #endregion
//    }
//}
