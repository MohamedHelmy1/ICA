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
        public IActionResult About()
        {
            return View();
        }
       
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
        public IActionResult AddCourses(SliderViewModel sliders)
        {
            if (ModelState.IsValid)
            {
                var data = slider.Add(sliders);
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
        public IActionResult DeleteCourses(int id)
        {
            var data = slider.Delete(id);
            return Json(data);
        }
        public IActionResult UpdateCourses(int id)
        {
            var data = slider.GetById(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("UpdateCourses")]
        public IActionResult UpdateCourses(UpdateSliderViewModel sliders)
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
    }
}
