//using System;
//using System.Linq;
//using System.Collections.Generic;


//using Core.Domain;
//using Core.Services;
//using Core.Repositories;
//using Core.Tools;



//namespace Infrastructure.Services
//{
//    internal class UserService : IUserService
//    {        
//        #region Properties


//        /// <summary>
//        /// Gets or sets the user repository.
//        /// </summary>
//        /// <value>The user repository.</value>
//        protected IRepository<User> repository { get; private set; }


//        /// <summary>
//        /// Gets or sets the Logger.
//        /// </summary>
//        /// <value>The logger.</value>
//        private readonly ILogger logger;
//        #endregion


//        #region ctor

//        /// <summary>
//        /// Initializes a new instance of the <see cref="UserService"/> class.
//        /// </summary>
//        /// <param name="UserRepository">The user repository.</param>
//        /// /// <param name="loggerService">The Logger Service.</param>
//        public UserService(IRepository<User> UserRepository,
//                           ILogger loggerService)
//        {

//            repository = UserRepository;
//            logger = loggerService;
//        }


//        #endregion

//        public IList<User> GetAll()
//        {

//            var users = repository.GetAll();
//            return users;
//        }
//        public User GetByName(string name)
//        {
//            var c = new CaseIgnoringStringComparer();
//            var user = repository.SearchFor(
//                (u) =>
//                    c.Equals(u.Name, name));
//            return user.FirstOrDefault();
//        }

//        public User Exists(string name, string password)
//        {
//            //todo add salthash
//            var c = new CaseIgnoringStringComparer();
//            var user = repository.SearchFor(
//                (u) =>
//                    c.Equals(u.Name, name)
//                    && u.Password == password);
//            return user.FirstOrDefault();
//        }

//        public bool Upsert(User user)
//        {
//            try
//            {
//                var c = new CaseIgnoringStringComparer();
//                var result = repository.SearchFor(
//                    (u) =>
//                        c.Equals(u.Name, user.Name));

//                if (result == null)
//                    repository.Insert(user);
//                else
//                    repository.Update(user);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}
