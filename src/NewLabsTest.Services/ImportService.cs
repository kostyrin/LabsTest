using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NewLabsTest.Domain.Models;

namespace NewLabsTest.Services
{
    public class ImportService : IImportService
    {
        private readonly IUserService _userService;

        public ImportService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<bool> ImportData(StreamReader sr)
        {
            var users = GetUsers(sr);
            return _userService.CreateUsers(users);
        }

        private IEnumerable<User> GetUsers(StreamReader sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                yield return ParseUser(line);
            }
        }

        private User ParseUser(string content)
        {
            var fields = content.Split('\t');
            return new User
            {
                Name = fields[0],
                Birthday = DateTime.ParseExact(fields[1], "dd.MM.yy", CultureInfo.InvariantCulture),
                Email = fields[2],
                Phone = fields[3]
            };
        }
    }
}
