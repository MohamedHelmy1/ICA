using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Helper;
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
    public class CoursesRep : ICoursesRep
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CoursesRep(ApplicationDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public int Add(CoursesViewModel Courses)
        {
            var data = mapper.Map<Courses>(Courses);
            if (Courses.Images!=null)
            {
                data.Image = UploodImage.SaveFile(Courses.Images, "Image");

            }
            db.Courses.Add(data);
            db.SaveChanges();
            return data.Id;

        }

        public int count()
        {
            return db.Courses.Count();
          

        }

        public bool Delete(int id)
        {
            try
            {
                var data = db.Courses.FirstOrDefault(x => x.Id == id);
                data.Delete =true;
                db.SaveChanges();
                return true;


            }
            catch (Exception)
            {

               return false;
            }
        }

        public bool Edit(UpdateCoursesViewModel Courses)
        {
            var data=mapper.Map<Courses>(Courses);
            if (Courses.Images != null)
            {
                var data1 = db.Courses.Where(x => x.Id == Courses.Id).Select(x=>x.Image).FirstOrDefault();
                if (data1 != null)
                {
                    UploodImage.RemoveFile("Image", data1);

                }
                data.Image = UploodImage.SaveFile(Courses.Images, "Image");

            }
            db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool EditLink(int id, string link)
        {
            var data = db.Courses.Where(x => x.Id == id).FirstOrDefault();
            data.Link=link;
            db.SaveChanges();
            return true;
        }
        public bool RemoveLink(int id)
        {
            var data = db.Courses.Where(x => x.Id == id).FirstOrDefault();
            data.Link = null;
            db.SaveChanges();
            return true;
        }

        public IQueryable<CoursesViewModel> GetAll()
        {
            return db.Courses.Where(x=>x.Delete==false).ProjectTo<CoursesViewModel>(mapper.ConfigurationProvider);
        }

        public UpdateCoursesViewModel GetById(int id)
        {
            var data = db.Courses.Where(x => x.Id==id).FirstOrDefault();
            var data1= mapper.Map<UpdateCoursesViewModel>(data);
            data1.Image = data.Image;
            return data1;

        }
       
    }
}
