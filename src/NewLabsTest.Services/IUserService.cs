using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLabsTest.Domain.Models;

namespace NewLabsTest.Services
{
    public interface IUserService
    {
        Task<bool> CreateUsers(IEnumerable<User> users);
        Task<bool> CreateUser(User user);

        Task<IEnumerable<User>> GetList(string sortColumn, string sortColumnDirection, int page, int pageSize);
        Task<long> LongCount();

        Task<User> GetUserById(int id);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
    }
}
