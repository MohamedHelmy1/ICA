using BLL.Interface;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseDetail courseDetail;
        private readonly UserManager<AplicationUser> userManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly IUserCourseRep userCourseRep;

        public HomeController(ILogger<HomeController> logger,ICourseDetail courseDetail,UserManager<AplicationUser> userManager,SignInManager<AplicationUser> signInManager, IUserCourseRep userCourseRep)
        {
            _logger = logger;
            this.courseDetail = courseDetail;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userCourseRep = userCourseRep;
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
        #region Register And Login
        public IActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Form(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AplicationUser()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Cuntery = model.Countery,
                    PhoneNumber = model.phone
                };
               var result=await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, true);
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var d = userCourseRep.AddCouse(model.CourseId, userId);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                    return View(model);
                }

            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               var result=await signInManager.PasswordSignInAsync(model.Email, model.Passwored, true, true);
                if (result.Succeeded)
                {
                  return  RedirectToAction("Index");
                }
            }
            return View(model);
        }
        
        public async Task <IActionResult> LoginOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
