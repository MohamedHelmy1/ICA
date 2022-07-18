using BLL.Interface;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AplicationUser> userManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager, IUserCourseRep userCourseRep, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data2 = await userManager.GetUsersInRoleAsync("Admin");
                if (data2.Count != 0)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                         if (user != null)
                         {
                    
                  


                   
                        var UserRole = await userManager.GetRolesAsync(user);
                        if (UserRole.Count != 0)
                        {
                            if (UserRole[0] == "Admin")
                            {
                                var result = await signInManager.PasswordSignInAsync(model.Email, model.Passwored, true, true);
                                if (result.Succeeded)
                                {
                                    return RedirectToAction("Home", "Admin");
                                }

                            }
                        }
                    }



                }
                else
                {
                    var user1 = new AplicationUser()
                    {
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                    };
                    string password = "123456";
                    var result = await userManager.CreateAsync(user1, password);
                    //Create Role Admin if not found
                    var TestRole = await roleManager.RoleExistsAsync("Admin");
                    var user2 = await userManager.FindByEmailAsync(model.Email);

                    if (!TestRole)
                    {
                        var role = new IdentityRole { Name = "Admin" };
                        await roleManager.CreateAsync(role);
                    }
                    // put LabDoctor in LabDoctor role
                    var result2 = await userManager.AddToRoleAsync(user1, "Admin");
                    ModelState.AddModelError("", "Use This Account => Email : admin@gmail.com, Password : 123456");
                    return View();
                }



                ModelState.AddModelError("", "Email Or Passwored Wrong");
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return RedirectToAction("/Account/Login");
        }
    }
}
