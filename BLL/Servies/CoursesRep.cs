using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Helper;
using BLL.Hubs;
using BLL.Interface;
using DAL.Database;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ReailTime> hub;

        public CoursesRep(ApplicationDbContext db,IMapper mapper,IHubContext<ReailTime> hub)
        {
            this.db = db;
            this.mapper = mapper;
            this.hub = hub;
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

        public async Task<bool> EditLink(int id, string link)
        {
            var data = db.Courses.Where(x => x.Id == id).FirstOrDefault();
            data.Link=link;
            db.SaveChanges();
            var userId = db.UserCourses.Where(x => x.Fk_CouresId == id && x.state == 1).Select(z => z.UserId);
            if (userId.Any())
            {
                await hub.Clients.Users(userId.ToList()).SendAsync("AddLink");

            }
            return true;
        }
        public async Task<bool> RemoveLink(int id)
        {
            var data = db.Courses.Where(x => x.Id == id).FirstOrDefault();
            var Day= DateTime.Now.DayOfWeek.ToString();

            var a = "";
            var x=db.Courset.Where(x=>x.FK_CourseId==id);
            DateTime t1 = DateTime.Now;
            switch (Day)
            {
                case "Saturday":
                    foreach (var c in x) {
                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) == 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                     }
                    if (c.Day == "Sunday")
                    {
                            a = c.Day;

                    }
                    else if (c.Day == "Monday")
                    {
                            a = c.Day;
                        }
                    else if (c.Day == "Tuesday")
                    {
                            a = c.Day;
                        }
                    else if (c.Day == "Wednesday")
                    {
                            a = c.Day;
                        }
                    else if (c.Day == "Thursday")
                    {
                            a = c.Day;

                        }
                    else if (c.Day == "Friday")
                    {
                            a = c.Day;
                        }
                    else if (c.Day == "Saturday")
                    {
                            a = c.Day;
                        }
                   
                     }
                    // code block
                    break;
                case "Sunday":
                    foreach (var c in x)
                    {

                       
                         if (c.Day == "Monday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            a = c.Day;

                        }

                    }
                    // code block
                    break;
                case "Monday":
                    foreach (var c in x)
                    {


                       
                        if (c.Day == "Tuesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            a = c.Day;

                        }
                        else if (c.Day == "Monday")
                        {
                            a = c.Day;
                        }

                    }
                    // code block
                    break;
                case "Tuesday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Wednesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            a = c.Day;

                        }
                        else if (c.Day == "Monday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            a = c.Day;
                        }

                    }
                    // code block
                    break;
                case "Wednesday":
                    foreach (var c in x)
                    {


                       
                        if (c.Day == "Thursday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            a = c.Day;

                        }
                        else if (c.Day == "Monday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            a = c.Day;
                        }

                    }
                    // code block
                    break;
                case "Thursday":
                    foreach (var c in x)
                    {



                       
                         if (c.Day == "Friday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            a =c.Day ;
                        }
                        else if (c.Day == "Sunday")
                        {
                            a =c.Day ;

                        }
                        else if (c.Day == "Monday")
                        {
                            a =c.Day ;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            a =c.Day ;
                        }
                        else if (c.Day == "Wednesday")
                        {

                        }
                        else if (c.Day == "Thursday")
                        {
                            a =c.Day ;
                        }
                    }
                    // code block
                    break;
                case "Friday":
                    foreach (var c in x)
                    {

                        
                        if (c.Day == "Saturday")
                        {
                            a =c.Day ;
                        }
                        else if (c.Day == "Sunday")
                        {
                            a =c.Day ;

                        }
                        else if (c.Day == "Monday")
                        {
                            a =c.Day ;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            a =c.Day ;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            a =c.Day ;
                        }
                        else if(c.Day == "Friday")
                        {
                            a =c.Day ;
                        }
                    }
                    // code block
                    break;
            }
            data.Link = null;
            var time = db.Courset.Where(x => x.Day == a && x.FK_CourseId == id).Select(x => x.Time).FirstOrDefault();
            data.NextLeather = a+"-"+time;
            db.SaveChanges();
            var userId = db.UserCourses.Where(x => x.Fk_CouresId == id && x.state == 1).Select(z => z.UserId);
            if (userId.Any())
            {
                await hub.Clients.Users(userId.ToList()).SendAsync("AddLink");

            }
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
