using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseDetail courseDetail;

        public HomeController(ILogger<HomeController> logger,ICourseDetail courseDetail)
        {
            _logger = logger;
            this.courseDetail = courseDetail;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult CourseDetail(int id)
        {
            ViewBag.id = id;
            var data=courseDetail.GetById(id);
            return View(data);
        }
        public IActionResult Form()
        {
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
