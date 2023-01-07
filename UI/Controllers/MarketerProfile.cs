using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Authorize(Roles = "Marketer")]
    public class MarketerProfile : Controller
    {
        private readonly UserManager<AplicationUser> userManger;
        private readonly RoleManager<IdentityRole> roleManager;

        public MarketerProfile( UserManager<AplicationUser> userManger, RoleManager<IdentityRole> roleManager)
        {
            this.userManger = userManger;
            this.roleManager = roleManager;
        }
        public async Task <IActionResult> Profile()
        {
            var url = Url.Action("form", "Home", "", Request.Scheme);
            ViewBag.url = url;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.user = await userManger.FindByIdAsync(userId);

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
    }
}
