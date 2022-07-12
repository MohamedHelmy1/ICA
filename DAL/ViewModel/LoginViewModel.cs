using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "الاميل مطلوب")]
        [EmailAddress(ErrorMessage = "ادخل اميل صحيح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "كلمه المرور مطلوبه")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "اقل عدد 6 حروف")]
        public string Passwored { get; set; }

    }
}
