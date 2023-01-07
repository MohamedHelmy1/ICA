using BLL.Helper.SendMail;
using BLL.Interface;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Controllers
{


    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ImarketerViewModel marketer;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly UserManager<AplicationUser> userManger;
        private readonly ISliderRep slider;
        private readonly ICoursesRep courses;
        private readonly ICourseDetail courseDetail;
        private readonly IAboutRep about;
        private readonly IUserCourseRep userCourse;

        public AdminController(ImarketerViewModel marketer,RoleManager<IdentityRole> roleManager, SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManger,ISliderRep slider,ICoursesRep courses,ICourseDetail courseDetail,IAboutRep about,IUserCourseRep userCourse)
        {
            this.marketer = marketer;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManger = userManger;
            this.slider = slider;
            this.courses = courses;
            this.courseDetail = courseDetail;
            this.about = about;
            this.userCourse = userCourse;
        }
        public async Task<IActionResult> Home()

        {
            ViewBag.id = courses.GetAll();
            ViewBag.courseCount = courses.count();
            ViewBag.user=  await userManger.GetUsersInRoleAsync("User");
            var a = await userManger.GetUsersInRoleAsync("User");
            

            return View();
        }
        #region Slider
        public IActionResult Slider()
        {
            return View();
        }
        public IActionResult AddSlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSlider(SliderViewModel sliders)
        {
            if (ModelState.IsValid)
            {
                var data=slider.Add(sliders);
                if (data == true)
                {
                    return RedirectToAction("Slider");

                }
                else
                {
                    return View(sliders);
                }
            }
            return View(sliders);
        }
        [HttpPost]
        public IActionResult DeleteSlider(int id)
        {
           var data=slider.Delete(id);
            return Json(data);
        }
        public IActionResult UpdateSlider(int id)
        {
            var data=slider.GetById(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("UpdateSlider")]
        public IActionResult UpdateSliders(UpdateSliderViewModel sliders)
        {
            if (ModelState.IsValid)
            {
                var data = slider.Edit(sliders);
                if (data == true)
                {
                    return RedirectToAction("Slider");

                }
                else
                {
                    return View(sliders);
                }
            }
            return View(sliders);
        }
        #endregion

        #region About and التوثيق Contact

        public IActionResult Contact()
        {
            var data = about.Getcontact();

            return View(data);
           
        }
        [HttpPost]
        public IActionResult Contact(ContactViewmodel contact)
        {
            var data = about.Contant(contact);
            if (data == true)
            {
                return RedirectToAction("Home");

            }
            return View(contact);
        }
        public IActionResult About()
        {
            var data = about.GetAbout();

            return View(data);
        }
        [HttpPost]
        public IActionResult About(AboutViewModel About)
        {
            var data=about.About(About);
            if(data == true)
            {
                return RedirectToAction("Home");

            }
            return View(About);
        }
        //التوثيق فى نفس AboutRep
        public IActionResult Active()
        {
            var data = about.GetActive();

            return View(data);
        }
        [HttpPost]
        public IActionResult Active(ActiveteCoursesViewModel activete)
        {
            var data = about.Active(activete);
            if (data == true)
            {
                return RedirectToAction("Home");

            }
            return View(activete);
        }
        #endregion
        #region Courses
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult AddCourses()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCourses(CoursesViewModel course)
        {
            if (ModelState.IsValid)
            {
                var data = courses.Add(course);
                if (data != 0)
                {
                    return RedirectToAction("AddCoursesDetail", new { id = data});

                }
                else
                {
                    return View(course);
                }
            }
            return View(course);
        }
        [HttpPost]
        public IActionResult DeleteCourses(int id)
        {
            var data = courses.Delete(id);
            return Json(data);
        }
        public IActionResult UpdateCourses(int id)
        {
            var data = courses.GetById(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("UpdateCourses")]
        public IActionResult UpdateCourses(UpdateCoursesViewModel course)
        {
            if (ModelState.IsValid)
            {
                var data = courses.Edit(course);
                if (data == true)
                {
                    return RedirectToAction("Courses");

                }
                else
                {
                    return View(course);
                }
            }
            return View(course);
        }
        #endregion
        #region Courses Detail
        public IActionResult AddCoursesTime(int id)
        {
            ViewBag.id=id;
            return View();
        }
        [HttpPost]
        public IActionResult AddCoursesTime(CoursesTimeViewModel courses)
        {
            var data = courseDetail.AddcourseTime(courses);

            return RedirectToAction("AddCoursesTime", new {id=courses.FK_CourseId});
        }
        [HttpPost]
        public IActionResult DeleteCourseTime (int id)
        {
            var data = courseDetail.DeleteCourseTime(id);
            return Json(data);
        }
        [HttpPost]
        
        public async Task <IActionResult> AddCoursesLink (int id, string link)
        {
            var data=await courses.EditLink(id,link);
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCoursesLink(int id)
        {
            var data =await courses.RemoveLink(id);
            return Json(data);
        }


        public IActionResult AddCoursesDetail(int id)
        {
            ViewBag.id = id;
            var data = courseDetail.GetById(id);
            if (data != null)
            {
                return RedirectToAction("Courses");

            }

            return View();
        }
        [HttpPost]
        public IActionResult AddCoursesDetail(CoursesDetailViewModel coursesDetail)
        {
            if (ModelState.IsValid)
            {
                coursesDetail.FK_CourseId= coursesDetail.Id;
                coursesDetail.Id = 0;
                var data = courseDetail.Add(coursesDetail);
                if (data == true)
                {
                    return RedirectToAction("Courses");

                }
                else
                {
                    return View(coursesDetail);
                }
            }
            return View(coursesDetail);
        }
       
        public IActionResult UpdateCoursesDetail(int id)
        {
            var data = courseDetail.GetById(id);
            return View(data);
        }
        public IActionResult AllCourses()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult UpdateCoursesDetail(CoursesDetailViewModel coursesDetail)
        {
            if (ModelState.IsValid)
            {
                var data = courseDetail.Edit(coursesDetail);
                if (data == true)
                {
                    return RedirectToAction("Courses");

                }
                else
                {
                    return View(coursesDetail);
                }
            }
            return View(coursesDetail);
        }
        #endregion
        #region Accept User in course
        public IActionResult AcceptUser()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> AcceptCourses(int id)
        {
            var url = Url.Action("Index", "Home", "", Request.Scheme);

            var data =await userCourse.AdminAcceptuser(id,url);
            return Json(data);
        }
        [HttpPost]
        public IActionResult NotAcceptCourses(int id)
        {
            var data = userCourse.AdminNotAcceptuser(id);
            return Json(data);
        }



        #endregion
        #region UpdateAdminprofile
        public async Task <IActionResult> UpdateAdmin()
        {
            AdminviewModel data = new AdminviewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            data.Name = (await userManger.FindByIdAsync(userId)).NameOfUser;
            data.Email = (await userManger.FindByIdAsync(userId)).Email;
            data.Phone= (await userManger.FindByIdAsync(userId)).PhoneNumber;
            data.UserId = userId;


            return View(data);
        }
        public async Task<IActionResult> Changepasswored()
        {
            AdminviewModel data = new AdminviewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            data.UserId = userId;


            return View(data);
        }
        
        //update password
        [HttpPost]
        public async Task<IActionResult> Changepasswored(AdminviewModel model)
        {
            var user = await userManger.FindByIdAsync(model.UserId);

            // change username and email
           
            // Persiste the changes
            var result = await userManger.ChangePasswordAsync(user,model.OldPassword,model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("LoginOut","Home");

            }
            else
            {
                ModelState.AddModelError("", "Current password in not Vaild");
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Updateinfo(AdminviewModel model)
        {
            // get user object from the storage
            var user = await userManger.FindByIdAsync(model.UserId);

            // change username and email
            user.NameOfUser = model.Name;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.Phone;

            // Persiste the changes
           var result= await userManger.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UpdateAdmin");

            }
            else
            {
                return View(model);
            }

        }
        
            
        #endregion
        public IActionResult GetUserInCourse(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteUserFromCourse(int id)
        {
            var data = userCourse.RemoveUser(id);
            return Json(data);
        }
        [HttpPost]
        public IActionResult finshCourses(int id)
        {
            var data = userCourse.RemoveUser(id);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAdminInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userManger.FindByIdAsync(userId);

            return Json(user.NameOfUser);
        }
        public async Task <IActionResult> GetAllMarketer()
        {
            ViewBag.user = await userManger.GetUsersInRoleAsync("Marketer");
            ViewBag.user1 = await userManger.GetUsersInRoleAsync("DeleteMarketer");


            return View();
        }
        public IActionResult AddMarketer()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> AddMarketer(RegisterViewModel model)
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
                var result = await userManger.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                   

                    var TestRole = await roleManager.RoleExistsAsync("Marketer");
                    var user2 = await userManger.FindByEmailAsync(model.Email);

                    var userId = user2.Id;
                    if (!TestRole)
                    {
                        var role = new IdentityRole { Name = "Marketer" };
                        await roleManager.CreateAsync(role);
                    }
                    // put LabDoctor in LabDoctor role
                    var result2 = await userManger.AddToRoleAsync(user2, "Marketer");
                    //send massage to Admin Acount
                    //var Email = await userManger.GetUsersInRoleAsync("Admin");
                    //var passwordResetLink = Url.Action("AcceptUser", "Admin", "", Request.Scheme);
                    //StringBuilder body = new StringBuilder();
                    //body.AppendLine("International Concept Academy");
                    //body.AppendFormat("User '{0}' Hass Ask to join Course", user.NameOfUser);
                    //body.AppendFormat("clik the Link to Accept it    '{0}'", passwordResetLink);

                    //foreach (var item in Email)
                    //{
                    //    MailSender.SendMail(new MailViewModel()
                    //    {

                    //        Email = item.Email,
                    //        Title = "International Concept Academy",
                    //        Message = body.ToString()
                    //    });
                    //}
                    return RedirectToAction("AddMarketer");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }

            }
            return View();
           
        }
        public async Task <IActionResult> MarketerProfile(string Id)
        {
            ViewBag.user = await userManger.FindByIdAsync(Id);
            ViewBag.data = marketer.GetMarketerUser(Id);

            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> Delete(string userId)
        {

            var user = await userManger.FindByIdAsync(userId);
            var result = await userManger.RemoveFromRoleAsync(user, "Marketer");

            var TestRole = await roleManager.RoleExistsAsync("DeleteMarketer");

            if (!TestRole)
            {
                var role = new IdentityRole { Name = "DeleteMarketer" };
                await roleManager.CreateAsync(role);
            }
            // put LabDoctor in LabDoctor role
            var result2 = await userManger.AddToRoleAsync(user, "DeleteMarketer");

            return Json("");
        }
        public async Task<IActionResult> Recover(string userId)
        {

            var user = await userManger.FindByIdAsync(userId);
            var result = await userManger.RemoveFromRoleAsync(user, "DeleteMarketer");

            var TestRole = await roleManager.RoleExistsAsync("Marketer");

            if (!TestRole)
            {
                var role = new IdentityRole { Name = "Marketer" };
                await roleManager.CreateAsync(role);
            }
            // put LabDoctor in LabDoctor role
            var result2 = await userManger.AddToRoleAsync(user, "Marketer");

            return Json("");
        }

    }
}
