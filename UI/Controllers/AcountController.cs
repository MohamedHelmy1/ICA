using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AcountController : Controller
    {
        [HttpPost]
        public IActionResult Regsister(RegisterViewModel model)
        {
            return View();
        }
    }
}
