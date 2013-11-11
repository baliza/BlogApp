using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public interface IDataContext<T> where T : IEntity
    {
        T Get(string Id);
        T[] GetAll();
        void Save(T entity);
        void Delete(string id);
                
    }
}
