using System;

using Core.Domain;
using Core.Repositories;
using Infrastructure.Repositories.MongoDB;

namespace Infrastructure.Repositories
{
    public class UserRepository : MongoDbRepository<Post>
    {

    }
}
