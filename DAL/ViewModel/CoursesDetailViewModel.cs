using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class CoursesDetailViewModel
    {
        public int Id { get; set; }
        public int FK_CourseId { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        [Display(Name = "معلومات حول البرنامج")]
        public string Information { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        [Display(Name = "نظام الدراسة")]
        public string Stady { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        [Display(Name = "المقررات")]
        public string Course { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        [Display(Name = "المستندات المطلوبة للاشتراك")]
        public string RequierdDocument { get; set; }
    }
}
