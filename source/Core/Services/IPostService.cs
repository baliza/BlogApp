using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<Post>> GetPostByTag(string tag);
        Task<IEnumerable<Post>> GetPostByAuthor(string author);
        Task<Post> GetPostByPermalinkAsync(string permalink);
        Task InsertPostAsync(Post post);
        Task<bool> InsertCommentAsync(string permalink, Comment comment);
        Task<int> LikeComment(string permalink, int comment_ordinal);
    }
}
