using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Domain;
using Core.Tools;
using Core.Repositories;



namespace Infrastructure.Repositories
{
    public class UserMockRepository : IRepository<User>
    {
        static List<User> collection = null;
        CaseIgnoringStringComparer comparer = new CaseIgnoringStringComparer();

        static UserMockRepository()
        {

            collection = new User[] 
            { 
                new User { Name ="Edu", Password= "password1" }, 
                new User { Name ="TheCook", Password= "password1" }, 
                new User { Name ="Toystory", Password= "password1" }, 
                new User { Name ="Hardman", Password= "password1" }, 
            }.ToList();
        }

        public bool Insert(User entity)
        {
            collection.Add(entity);
            return true;
        }

        public bool Update(User entity)
        {
            var idx = collection.FindIndex(
                (item) => 
                    comparer.Equals(item.Name, entity.Name));
            if (idx < 0) 
                return false;
            
            collection[idx] = entity;
            return true;
        }

        public bool Delete(User entity)
        {

            var idx = collection.FindIndex(
                (item) =>
                    comparer.Equals(item.Name, entity.Name)
                    && item.Password == entity.Password);
            if (idx < 0) 
                return false;
            
            collection.RemoveAt(idx);
            return true;
        }

        public IList<User> SearchFor(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {                      
            return collection.AsQueryable<User>()
                        .Where(predicate)
                        .ToList();
        }

        public IList<User> GetAll()
        {
            return collection;
        }

        public User GetById(string id)
        {
            return collection
                .FirstOrDefault((item) => item.Id == id);
        }
    }
}
