using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "الاميل مطلوب")]
        [EmailAddress(ErrorMessage = "ادخل اميل صحيح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "كلمه المرور مطلوبه")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "اقل عدد 6 حروف")]
        public string Password { get; set; }
        [Required(ErrorMessage = "تاكيد كلمه المرور مطلوبه ")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "اقل عدد 6 حروف")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "غير متطابق")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "ادخل الدوله")]

        public string Countery { get; set; }
        [Required(ErrorMessage = "ادخل رقم الواتس")]

        public string phone { get; set; }
        [Required(ErrorMessage = "ادخل الاسم")]

        public string Name { get; set; }
        [Required(ErrorMessage = "اختر االماجستير او الدكتراه")]
        public int CourseId { get; set; }
        public string FK_MarketerId { get; set; }





    }
}
