using BLL.Helper.SendMail;
using BLL.Interface;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger,ICourseDetail courseDetail,UserManager<AplicationUser> userManager,SignInManager<AplicationUser> signInManager, IUserCourseRep userCourseRep,RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.courseDetail = courseDetail;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userCourseRep = userCourseRep;
            this.roleManager = roleManager;
        }

        public async Task <IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (userId != null)
            {
                var UserRole = await userManager.GetRolesAsync(user);
                if (UserRole.Count != 0)
                {
                    if (UserRole[0] == "Admin")
                    {
                return RedirectToAction("LoginOut");


                    }
                }

            }
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
       
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
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
                    UserName = model.Email,
                    Email = model.Email,
                    Cuntery = model.Countery,
                    PhoneNumber = model.phone,
                    NameOfUser = model.Name
                };
               var result=await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    await signInManager.SignInAsync(user, true);
                    
                    var TestRole = await roleManager.RoleExistsAsync("User");
                    var user2 = await userManager.FindByEmailAsync(model.Email);
                    
                    var userId = user2.Id;
                    var d = userCourseRep.AddCouse(model.CourseId, userId);

                    if (!TestRole)
                    {
                        var role = new IdentityRole { Name = "User" };
                        await roleManager.CreateAsync(role);
                    }
                    // put LabDoctor in LabDoctor role
                    var result2 = await userManager.AddToRoleAsync(user2, "User");
                    //send massage to Admin Acount
                    var Email = await userManager.GetUsersInRoleAsync("Admin");
                    var passwordResetLink = Url.Action("AcceptUser", "Admin", "", Request.Scheme);
                    StringBuilder body = new StringBuilder();
                    body.AppendLine("International Concept Academy");
                    body.AppendFormat("User '{0}' Hass Ask to join Course", user.NameOfUser);
                    body.AppendFormat("clik the Link to Accept it<a href='{0}'> click </a>", passwordResetLink);

                    foreach (var item in Email)
                    {
                        MailSender.SendMail(new MailViewModel()
                        {

                            Email = item.Email,
                            Title = "International Concept Academy",
                            Message = body.ToString()
                        });
                    }
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
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                        var UserRole = await userManager.GetRolesAsync(user);
                        if (UserRole.Count != 0)
                        {
                            if (UserRole[0] == "User")
                            {
                                var result = await signInManager.PasswordSignInAsync(model.Email, model.Passwored, true, true);
                                if (result.Succeeded)
                                {
                                    return RedirectToAction("Index");
                                }

                            }
                        }

                    
                }
                
               
                ModelState.AddModelError("", "Email Or Passwored Wrong");
            }
            return View(model);
        }
        
        public async Task <IActionResult> LoginOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        #endregion
        [HttpPost]
        public async Task <IActionResult> AddUserCourse(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var d = userCourseRep.AddCouse(id, userId);
           

            if (d == true) {
                var user = await userManager.FindByIdAsync(userId);
                var Email = await userManager.GetUsersInRoleAsync("Admin");
                var passwordResetLink = Url.Action("AcceptUser", "Admin", "", Request.Scheme);
                StringBuilder body = new StringBuilder();
                body.AppendLine("International Concept Academy");
                body.AppendFormat("User '{0}' Hass Ask to join Course" , user.NameOfUser);
                body.AppendFormat("clik the Link to Accept it<a href='{0}'> click </a>", passwordResetLink);
            foreach (var item in Email)
            {
                MailSender.SendMail(new MailViewModel()
                {

                    Email = item.Email,
                    Title = "International Concept Academy",
                    Message = body.ToString()
                });
            }

            }
            return Json(d);
        }
       
        public IActionResult UserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = userCourseRep.GetUserCourses(userId);
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
