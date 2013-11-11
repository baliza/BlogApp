using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Services
{
    public interface IBlogService        
    {
        IList<Post> GetAllPost();
        IList<Post> GetPostByTag(string tag);
        Post GetPostByPermalink(string tag);
        
    }
}
