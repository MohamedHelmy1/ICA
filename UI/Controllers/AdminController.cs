using BLL.Interface;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly ISliderRep slider;

        public AdminController(ISliderRep slider)
        {
            this.slider = slider;
        }
        public IActionResult Home()
        {
            return View();
        }
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
                return RedirectToAction("AddSlider");
            }
            return View();
        }
        public IActionResult UpdateSlider()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
    }
}
