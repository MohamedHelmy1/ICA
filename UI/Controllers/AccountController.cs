using BLL.Helper.SendMail;
using BLL.Interface;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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

        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        // Generate Token
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        // Get Reset Password Link
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                        MailSender.SendMail(new MailViewModel()
                        {

                            Email = model.Email,
                            Title = "Reset Password",
                            Message = passwordResetLink
                        });

                        return RedirectToAction("ConfirmForgetPassword");
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }

        }

        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string Email, string Token)
        {
            if (Email != null && Token != null)
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        public IActionResult ConfirmResetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("/Home/Login");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                    return RedirectToAction("/Home/Login");


                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }

        }

        #endregion

    }
}
