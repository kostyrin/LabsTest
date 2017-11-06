using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using NewLabsTest.Domain.Models;
using NewLabsTest.Services.Extensions;

namespace NewLabsTest.Services
{
    public class ImportService : IImportService
    {
        private readonly IUserService _userService;

        public ImportService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<bool> ImportDataAsync(StreamReader sr, int batchSize)
        {
            return Task.Run(async () =>
            {
                var users = GetUsers(sr);
                //batches are better for performance CPU and SQL then whole list  
                foreach (IEnumerable<User> batch in users.Batch(batchSize))
                {
                    await _userService.CreateUsers(batch);
                }
                return true;
            });
            
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
                Email = fields[2].ToLower(),
                Phone = fields[3] 
            };
        }
    }
}
