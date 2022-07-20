using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class CoursesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public IFormFile Images { get; set; }
        public string Link { get; set; }
        public string NextLeather { get; set; }
        [Required]
        public string StartDate { get; set; }

        public int state { get; set; }




    }
}
