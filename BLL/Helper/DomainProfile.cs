using AutoMapper;
using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            #region courses
            CreateMap<Courses, CoursesViewModel>();
            CreateMap<CoursesViewModel, Courses>();
            CreateMap<UpdateCoursesViewModel, Courses>();
            CreateMap<Courses, UpdateCoursesViewModel>();
            #endregion

        }
    }
}
