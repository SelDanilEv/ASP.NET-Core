using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.Services
{
    public interface IUserServices<T>
    {
        IEnumerable<T> GetAll();
        T AddUser(T item);
        T UpdateUserById(T item);
        T GetUserById(int id);
        T DeleteUserById(int id);
        bool GetUserByEmail(string email);
    }
}
