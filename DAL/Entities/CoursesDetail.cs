using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CoursesDetail
    {
        public int Id { get; set; }
        public int FK_CourseId { get; set; }
        [ForeignKey("FK_CourseId")]
        public Courses Courses { get; set; }
        public string Information { get; set; }
        public string Stady { get; set; }
        public string Course { get; set; }
        public string RequierdDocument { get; set; }


    }
}
