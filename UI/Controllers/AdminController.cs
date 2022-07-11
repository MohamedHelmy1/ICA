using BLL.Interface;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly ISliderRep slider;
        private readonly ICoursesRep courses;
        private readonly ICourseDetail courseDetail;
        private readonly IAboutRep about;

        public AdminController(ISliderRep slider,ICoursesRep courses,ICourseDetail courseDetail,IAboutRep about)
        {
            this.slider = slider;
            this.courses = courses;
            this.courseDetail = courseDetail;
            this.about = about;
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
        #region About and التوثيق
       
       
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
        
    }
}
