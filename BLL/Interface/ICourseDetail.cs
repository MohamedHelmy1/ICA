using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICourseDetail
    {
        bool Add(CoursesDetailViewModel Courses);
        bool Edit(CoursesDetailViewModel Courses);
        CoursesDetailViewModel GetById(int id);
        bool AddcourseTime(CoursesTimeViewModel Courses);
        IEnumerable<CoursesTimeViewModel> GetAll(int id);


    }
}
