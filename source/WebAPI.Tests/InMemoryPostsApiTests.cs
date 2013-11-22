
using WebAPI.Tests.Server;
using WebAPI.Tests.Test;
using NUnit.Framework;

namespace WebAPI.Tests
{
    [TestFixture]
    public class InMemoryPostsApiTests : PostsApiTests
    {
        public InMemoryPostsApiTests()
            : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryPostApiTests : PostApiTests
    {
        public InMemoryPostApiTests()
            : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryAddPostApiTests : AddPostApiTests
    {
        public InMemoryAddPostApiTests()
            : base(new InMemoryApiServer())
        {
        }
    }

    [TestFixture]
    public class InMemoryAddCommentApiTests : AddCommentApiTests
    {
        public InMemoryAddCommentApiTests()
            : base(new InMemoryApiServer())
        {
        }
    }
    [TestFixture]
    public class InMemoryLikeCommentApiTests : LikeCommentApiTests
    {
        public InMemoryLikeCommentApiTests()
            : base(new InMemoryApiServer())
        {
        }
    }
   
}
