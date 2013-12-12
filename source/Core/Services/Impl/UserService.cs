using System;
using System.Linq;
using System.Collections.Generic;


using Core.Domain;
using Core.Services;
using Core.Repositories;
using Core.Tools;



namespace Core.Services.Impl
{
    internal class UserService : IUserService
    {        
        #region Properties


        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        /// <value>The user repository.</value>
        protected IRepository<User> repository { get; private set; }


        /// <summary>
        /// Gets or sets the Logger.
        /// </summary>
        /// <value>The logger.</value>
        private readonly ILogger logger;
        #endregion


        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="UserRepository">The user repository.</param>
        /// /// <param name="loggerService">The Logger Service.</param>
        public UserService(IRepository<User> UserRepository,
                           ILogger loggerService)
        {

            repository = UserRepository;
            logger = loggerService;
        }


        #endregion

        public IList<User> GetAll()
        {

            var users = repository.GetAll();
            return users.ToList();
        }
        public User Get(string name, string password)
        {
            var c = new CaseIgnoringStringComparer();
            var user = repository.SearchFor(
                (u) =>
                    c.Equals(u.Name, name)
                    && u.Password == password);
            return user.FirstOrDefault();
        }

        public bool Exists(string name)
        {
            //todo add salthash
            var c = new CaseIgnoringStringComparer();
            var user = repository.SearchFor(
                (u) =>
                    c.Equals(u.Name, name));
            return user.Any();
        }

        public bool Insert(User user)
        {
            if (Exists(user.Name)) return false;
            repository.Update(user);
            return true;
        }

        public bool Update(User user)
        {
            if (string.IsNullOrEmpty(user.Id)) return false;
            if (!!!Exists(user.Name)) return false;

            repository.Update(user);
            return true;

        }
    }
}
