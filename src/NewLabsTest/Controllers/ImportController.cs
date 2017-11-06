using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewLabsTest.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewLabsTest.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportService _importService;
        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [Route("import")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("import")]
        public async Task<JsonResult> UploadFiles(IFormFile file, int batchSize)
        {
            bool result;
            using (StreamReader sr = new StreamReader(file.OpenReadStream()))
            {
                result = await _importService.ImportDataAsync(sr, batchSize);
            }
            
            return Json(new { result, message = "the files are uploaded" });
        }
    }
}
