using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewLabsTest.Domain.Models;
using NewLabsTest.Services;
using NewLabsTest.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewLabsTest.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

            // Skip number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();

            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();

            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            // Search Value from (Search box)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            long recordsTotal = await _userService.LongCount();

            //Paging   
            var list = await _userService.GetList(sortColumn, sortColumnDirection, skip, pageSize);
            var data = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(list);

            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserById(id);
            var model = _mapper.Map<User, UserViewModel>(user);
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = _mapper.Map<UserViewModel, User>(model);
            var result = await _userService.UpdateUser(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);
            var result = await _userService.DeleteUser(user);
            return RedirectToAction("Index");
        }
    }
}
