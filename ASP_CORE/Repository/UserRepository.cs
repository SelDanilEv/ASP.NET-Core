using ASP_CORE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly DBcontext _DBcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(DBcontext context)
        {
            this._DBcontext = context;
        }
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public User AddUser(User item)
        {
            _DBcontext.Users.Add(item);
            _DBcontext.SaveChanges();
            return item;
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User DeleteUserById(int id)
        {
            var deleteUser = _DBcontext.Users.FirstOrDefault(r => r.Id == id);
            _DBcontext.Users.Remove(deleteUser);
            _DBcontext.SaveChanges();
            return deleteUser;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            try
            {
                return _DBcontext.Users.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return _DBcontext.Users.FirstOrDefault(r => r.Id == id);
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public User UpdateUserById(User item)
        {
            _DBcontext.Users.Update(item);

            _DBcontext.SaveChanges();
            return item;
        }
        /// <summary>
        /// Get by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool GetUserByEmail(string email)
        {
            if(_DBcontext.Users.FirstOrDefault(r => r.Email == email) != null)
            {
                return false;
            }
            return true;
        }
    }
}
