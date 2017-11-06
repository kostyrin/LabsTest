using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NewLabsTest.Services
{
    public interface IImportService
    {
        Task<bool> ImportDataAsync(StreamReader content, int batchSize);
    }
}
