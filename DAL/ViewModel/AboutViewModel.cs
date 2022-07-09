using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class AboutViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "الاسم")]
        public string name { get; set; }
            [Required]
            [Display(Name = "معلومات عنا")]
        public string AboutUs { get; set; }
        [Required]
        [Display(Name = "الرسالة")]
        public string message { get; set; }
        [Required]
        [Display(Name = "هدفنا")]
        public string OurGoal { get; set; }
        [Required]
        [Display(Name = "لماذا نحن")]
        public string WhyUs { get; set; }
    }
}
