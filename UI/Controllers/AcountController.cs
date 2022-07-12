using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AcountController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }
    }
}
