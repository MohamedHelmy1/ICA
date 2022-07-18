using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserCourseRep
    {
        bool AddCouse(int id,string UserId);
        Task<IEnumerable<UserCoursesViewModel>> GetAllCoursetoAdminAcsept();
        bool AdminAcceptuser(int id);
        bool AdminNotAcceptuser(int id);
        IEnumerable<CoursesViewModel> GetUserCourses(string userId);



    }
}
