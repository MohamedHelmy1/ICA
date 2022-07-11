using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICoursesRep
    {
        int Add(CoursesViewModel Courses);
        bool Edit(UpdateCoursesViewModel Courses);
        IQueryable<CoursesViewModel> GetAll();
        UpdateCoursesViewModel GetById(int id);

        bool Delete(int id);
    }
}
