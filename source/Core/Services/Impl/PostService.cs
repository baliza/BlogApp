using System;
using System.Linq;
using System.Collections.Generic;


using Core.Domain;
using Core.Services;
using Core.Repositories;
using Core.Tools;
using System.Threading.Tasks;





namespace Core.Services.Impl
{
    internal class PostService : IPostService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the post repository.
        /// </summary>
        /// <value>The user repository.</value>
        protected IRepository<Post> repository { get; private set; }


        /// <summary>
        /// Gets or sets the Logger.
        /// </summary>
        /// <value>The logger.</value>
        private readonly ILogger logger;


        /// <summary>
        /// Gets or sets the Logger.
        /// </summary>
        /// <value>The logger.</value>
        private readonly CaseIgnoringStringComparer comparer;
        #endregion


        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="PostRepository">The post repository.</param>
        /// /// <param name="loggerService">The Logger Service.</param>
        public PostService(IRepository<Post> PostRepository,
                           ILogger loggerService)
        {
            repository = PostRepository;
            logger = loggerService;
            comparer = new CaseIgnoringStringComparer();
        }


        public Task<IEnumerable<Post>> GetAll()
        {
            var result = repository
                .GetAll()
                .OrderByDescending(p => p.CreatedOn)
                .Take(20);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Post>> GetPostByTag(string tag)
        {
            var result = repository.SearchFor(
                (p) =>
                    (p.Tags != null)
                    && p.Tags.Contains(tag, comparer));
            return Task.FromResult(result);
        }
        public Task<IEnumerable<Post>> GetPostByAuthor(string author)
        {
            var result = repository.SearchFor(
                (p) =>
                    comparer.Equals(p.Author, author));
            return Task.FromResult(result);
        }

        public Task<Post> GetPostByPermalinkAsync(string permalink)
        {
            var result = repository.SearchFor(
                (p) =>
                    comparer.Equals(p.Permalink, permalink))
                    .FirstOrDefault();
            return Task.FromResult(result);
        }

        public Task InsertPostAsync(Post post)
        {

            var result = repository.SearchFor(
                (p) =>
                    comparer.Equals(p.Permalink, post.Permalink)
                    ).FirstOrDefault();

            if (result == null)
            {
                post.CreatedOn = DateTime.Now;
                repository.Insert(post);
            }
            else
            {
                post.UpdatedOn = DateTime.Now;
                repository.Update(post);
            }
            var id = repository.SearchFor(
                (p) =>
                    comparer.Equals(p.Permalink, post.Permalink))
                    .FirstOrDefault()
                    .Id;
            return Task.FromResult(id);
        }
        #endregion



        public Task<bool> InsertCommentAsync(string permalink, Comment comment)
        {
            var post = GetPostByPermalinkAsync(permalink)
                        .Result;

            if (post == null)
                return Task.FromResult(false);

            var listOfComments = (post.Comments ?? new Comment[0]).ToList();
            listOfComments.Add(comment);
            post.Comments = listOfComments.ToArray();
            repository.Update(post);

            return Task.FromResult(true);
        }

        public Task<int> LikeComment(string permalink, int comment_ordinal)
        {
            var post = GetPostByPermalinkAsync(permalink)
                .Result;
            if (post == null
                || post.Comments == null
                || post.Comments.Count() < comment_ordinal)
                return Task.FromResult(-1);

            post.Comments[comment_ordinal].Likes += 1;
            repository.Update(post);
            return Task.FromResult(post.Comments[comment_ordinal].Likes);
        }
    }
}
