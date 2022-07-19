using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class UserCoursesViewModel
    {
        public int id { get; set; }

        public int Fk_CouresId { get; set; }
        public string CourseName { get; set; }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string cuntery { get; set; }


        public string Date { get; set; }
        public int state { get; set; }
    }
}
