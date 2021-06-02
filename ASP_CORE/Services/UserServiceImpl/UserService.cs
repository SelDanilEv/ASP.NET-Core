using ASP_CORE.Model;
using ASP_CORE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.Services.UserService
{
    public class UserService : IUserServices<User>
    {

        private IUserRepository<User> repository;

        public UserService(DBcontext dBcontext)
        {
            repository = new UserRepository(dBcontext);
        }
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public User AddUser(User item)
        {
            return repository.AddUser(item);
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User DeleteUserById(int id)
        {
           return repository.DeleteUserById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }

        public bool GetUserByEmail(string email)
        {
            return repository.GetUserByEmail(email);
        }

        public User GetUserById(int id)
        {
            return repository.GetUserById(id);
        }

        public User UpdateUserById(User item)
        {
            return repository.UpdateUserById(item);
        }
    }
}
