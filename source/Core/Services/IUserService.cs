using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Services
{
    public interface IUserService        
    {
        IList<User> GetAll();
        User Get(string name, string password);
        bool Exists(string name);        
        bool Insert(User user);
        bool Update(User user);
    }
}
