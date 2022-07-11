using AutoMapper;
using BLL.Interface;
using DAL.Database;
using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servies
{
    public class CourseDetail : ICourseDetail
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CourseDetail(ApplicationDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public bool Add(CoursesDetailViewModel Courses)
        {
            try
            {
                var data = mapper.Map<CoursesDetail>(Courses);
                db.CoursesDetail.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           

        }

        public bool Edit(CoursesDetailViewModel Courses)
        {
            try
            {
                var data = mapper.Map<CoursesDetail>(Courses);
                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
           
        }

       

        public CoursesDetailViewModel GetById(int id)
        {
            var data=db.CoursesDetail.FirstOrDefault(x=>x.Id==id);  
           return mapper.Map<CoursesDetailViewModel>(data);
        }
    }
}
