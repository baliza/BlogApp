/*
 * http://seesharpdeveloper.blogspot.ie/2013/09/c-mongodb-repository-implementation.html
 */
using System;
using System.Collections.Generic;
using System.Linq;


using Core.Domain;
using Core.Repositories;
using System.Linq.Expressions;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Infrastructure.Repositories.MongoDB
{
    /// <summary>
    /// A MongoDB repository. Maps to a collection with the same name
    /// as type TEntity.
    /// </summary>
    /// <typeparam name="T">Entity type for this repository</typeparam>
    public class MongoDbRepository<TEntity> :
     IRepository<TEntity> where
         TEntity : IEntity
    {
        private MongoDatabase database;
        private MongoCollection<TEntity> collection;

        public MongoDbRepository()
        {
            GetDatabase();
            GetCollection();
        }

        public bool Insert(TEntity entity)
        {
            //entity.Id = Guid.NewGuid();
            return collection.Insert(entity).Ok;
        }

        public bool Update(TEntity entity)
        {
            if (entity.Id == string.Empty)
                return Insert(entity);

            return collection
                .Save(entity)
                    .DocumentsAffected > 0;
        }

        public bool Delete(TEntity entity)
        {
            return collection
                .Remove(Query.EQ("_id", entity.Id))
                    .DocumentsAffected > 0;
        }

        public IEnumerable<TEntity>
            SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.FindAll()
                    .Where(predicate.Compile());
        }

        public IEnumerable<TEntity> GetAll()
        {
            return collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(string id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        #region Private Helper Methods
        private void GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            var server = client.GetServer();

            database = server.GetDatabase(GetDatabaseName());
        }

        private string GetConnectionString()
        {
            return "localhost:27017";
            //return ConfigurationManager
            //    .AppSettings
            //        .Get("MongoDbConnectionString")
            //            .Replace("{DB_NAME}", GetDatabaseName());
        }

        private string GetDatabaseName()
        {
            return "blog";
            //return ConfigurationManager
            //    .AppSettings
            //        .Get("MongoDbDatabaseName");
        }

        private void GetCollection()
        {
            collection = database
                .GetCollection<TEntity>(typeof(TEntity).Name);
        }
        #endregion
    }
}