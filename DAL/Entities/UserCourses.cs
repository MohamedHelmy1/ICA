using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserCourses
    {
        public int id { get; set; }
        public int Fk_CouresId { get; set; }
        [ForeignKey("Fk_CouresId")]
        public Courses Courses { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AplicationUser User { get; set; }
        public string Date { get; set; }
        public int state { get; set; }

    }
}
