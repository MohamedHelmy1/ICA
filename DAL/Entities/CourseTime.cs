using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CourseTime
    {
        public int Id { get; set; }
        public int FK_CourseId { get; set; }
        [ForeignKey("FK_CourseId")]
        public Courses Courses { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }
}
