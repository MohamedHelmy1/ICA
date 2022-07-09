using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class SliderViewModel
    {
        public int Id { get; set; }
        [Required]
       
        public string Name { get; set; }
        [Required]
        [Display(Name = "الوصف")]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        [Display(Name = "اختار الصورة")]
        public IFormFile Images { get; set; }

    }
}
