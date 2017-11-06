using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewLabsTest.Domain;
using NewLabsTest.Domain.Models;
using System.Linq.Dynamic.Core;

namespace NewLabsTest.Services
{
    public class UserService : IUserService
    {
        private readonly NewLabsTestContext _context;

        public UserService(NewLabsTestContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUsers(IEnumerable<User> users)
        {
            await _context.Users.AddRangeAsync(users);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetList(string sortColumn, string sortColumnDirection, int page, int pageSize)
        {
            var query = _context.Users.AsQueryable();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            query = query.Skip(page).Take(pageSize);

            var result = await query.ToListAsync();
            return result;
        }

        public Task<long> LongCount()
        {
            return _context.Users.LongCountAsync();
        }

        public Task<User> GetUserById(int id)
        {
            return _context.Users.SingleAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Update(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
