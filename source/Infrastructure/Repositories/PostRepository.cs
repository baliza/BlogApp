using System;

using Core.Domain;
using Infrastructure.Repositories.MongoDB;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class PostRepository : MongoDbRepository<Post>
    {

        
    }
}
