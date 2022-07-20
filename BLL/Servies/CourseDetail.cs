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

        public bool AddcourseTime(CoursesTimeViewModel Courses)
        {
            CourseTime obj = new CourseTime();
            obj.Time = Courses.Time;
            obj.Day = Courses.Day;
            obj.FK_CourseId = Courses.FK_CourseId;
            db.Courset.Add(obj);
            db.SaveChanges();
            return true;
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

        public IEnumerable<CoursesTimeViewModel> GetAll(int id)
        {
            List<CoursesTimeViewModel> list = new List<CoursesTimeViewModel>();
            var date = db.Courset.Where(x => x.FK_CourseId == id).Select(x=>x);
            foreach (var item in date)
            {
                CoursesTimeViewModel obj = new CoursesTimeViewModel();
                obj.Time = item.Time;
                obj.Day= item.Day;
                obj.Id = item.Id;
                list.Add(obj);


            }
            return list;
        }
        public bool DeleteCourseTime(int id)
        {
            var date = db.Courset.Where(x => x.Id == id).FirstOrDefault();
            db.Courset.Remove(date);
            db.SaveChanges();
            return true;  
        }

        public CoursesDetailViewModel GetById(int id)
        {
            var data=db.CoursesDetail.FirstOrDefault(x=>x.Id==id);  
           return mapper.Map<CoursesDetailViewModel>(data);
        }
    }
}
