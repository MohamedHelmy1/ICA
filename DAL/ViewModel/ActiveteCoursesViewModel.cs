using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class ActiveteCoursesViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "يتم التوثيق من قبل")]
        public string Name { get; set; }
    }
}
