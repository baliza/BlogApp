
using WebAPI.Tests.Server;
using WebAPI.Tests.Tests;
using NUnit.Framework;

namespace WebAPI.Tests
{
    [TestFixture]
    public class InMemoryGetAllTests : GetAllPost
    {
        public InMemoryGetAllTests()
            : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryGetByPermalinkTests : GetPostByPermalink
    {
        public InMemoryGetByPermalinkTests()
            : base(new InMemoryApiServer())
        {
        }
    }
    [TestFixture]
    public class InMemoryPostGetByTagTests : GetPostsByTag
    {
        public InMemoryPostGetByTagTests()
            : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryPostGetByAuthorTests : GetPostsByAuthor
    {
        public InMemoryPostGetByAuthorTests()
            : base(new InMemoryApiServer())
        {
        }
    }
    [TestFixture]
    public class InMemoryPostAddTests : AddPost
    {
        public InMemoryPostAddTests()
            : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryPostCommentAddTests : AddComment
    {
        public InMemoryPostCommentAddTests()
            : base(new InMemoryApiServer())
        {
        }
    }
    [TestFixture]
    public class InMemoryPostCommentLikeTests : LikeComment
    {
        public InMemoryPostCommentLikeTests()
            : base(new InMemoryApiServer())
        {
        }
    }
    [TestFixture]
    public class InMemoryGetCommentsTests : GetComments
    {
        public InMemoryGetCommentsTests()
            : base(new InMemoryApiServer())
        {
        }
    }
  
   
}
