using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class CoursesTimeViewModel
    {
        public int Id { get; set; }
        [Required]

        public int FK_CourseId { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]

        public string Time { get; set; }
    }
}
