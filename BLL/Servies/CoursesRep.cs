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
using System.Globalization;
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
            
            List<int> Add = new List<int>();
            var a = "";
            var x=db.Courset.Where(x=>x.FK_CourseId==id);
            DateTime t1 = DateTime.Now;
            var time9 = "";
            var Day1 = "";
            var d = "";
            var ax = 0;
            switch (Day)
            {
                  case "Saturday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Sunday")
                        {
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Monday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Friday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                    ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it < ax)
                        {
                            ax = it;
                        }
                    }

                    if (ax == 1)
                    {
                        d = "Sunday";
                    }
                    else if (ax == 2)
                    {
                        d = "Monday";
                    }
                    else if (ax == 3)
                    {
                        d = "Tuesday";
                    }
                    else if (ax == 4)
                    {
                        d = "Wednesday";
                    }
                    else if (ax == 5)
                    {
                        d = "Thursday";
                    }
                    else if (ax == 6)
                    {
                        d = "Friday";
                    }

                   
                    // code block
                    break;
                case "Sunday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Monday")
                        {
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Saturday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                    ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it < ax)
                        {
                            ax = it;
                        }
                    }

                    if (ax == 1)
                    {
                        d = "Monday";
                    }
                    else if (ax == 2)
                    {
                        d = "Tuesday";
                    }
                    else if (ax == 3)
                    {
                        d = "Wednesday";
                    }
                    else if (ax == 4)
                    {
                        d = "Thursday";
                    }
                    else if (ax == 5)
                    {
                        d = "Friday";
                    }
                    else if (ax == 6)
                    {
                        d = "Saturday";
                    }

                    // code block
                    break;
                case "Monday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Tuesday")
                        {
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Sunday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Monday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                    ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it < ax)
                        {
                            ax = it;
                        }
                    }

                    if (ax == 1)
                    {
                        d = "Tuesday";
                    }
                    else if (ax == 2)
                    {
                        d = "Wednesday";
                    }
                    else if (ax == 3)
                    {
                        d = "Thursday";
                    }
                    else if (ax == 4)
                    {
                        d = "Friday";
                    }
                    else if (ax == 5)
                    {
                        d = "Saturday";
                    }
                    else if (ax == 6)
                    {
                        d = "Sunday";
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
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Monday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                      ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it<ax)
                        {
                            ax = it;
                        }
                    }
                    
                    if (ax==1)
                    {
                        d = "Wednesday";
                    }else if (ax == 2)
                    {
                        d = "Thursday";
                    }
                    else if (ax == 3)
                    {
                        d = "Friday";
                    }
                    else if (ax == 4)
                    {
                        d = "Saturday";
                    }
                    else if (ax == 5)
                    {
                        d = "Sunday";
                    }
                    else if (ax == 6)
                    {
                        d = "Monday";
                    }
                   
                    // code block
                    break;
                case "Wednesday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Thursday")
                        {
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Monday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Tuesday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                    ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it < ax)
                        {
                            ax = it;
                        }
                    }

                    if (ax == 1)
                    {
                        d = "Thursday";
                    }
                    else if (ax == 2)
                    {
                        d = "Friday";
                    }
                    else if (ax == 3)
                    {
                        d = "Saturday";
                    }
                    else if (ax == 4)
                    {
                        d = "Sunday";
                    }
                    else if (ax == 5)
                    {
                        d = "Monday";
                    }
                    else if (ax == 6)
                    {
                        d = "Tuesday";
                    }

                   
                    // code block
                    break;
                case "Thursday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Friday")
                        {
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Saturday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Monday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Wednesday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Thursday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                    ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it < ax)
                        {
                            ax = it;
                        }
                    }

                    if (ax == 1)
                    {
                        d = "Friday";
                    }
                    else if (ax == 2)
                    {
                        d = "Saturday";
                    }
                    else if (ax == 3)
                    {
                        d = "Sunday";
                    }
                    else if (ax == 4)
                    {
                        d = "Monday";
                    }
                    else if (ax == 5)
                    {
                        d = "Tuesday";
                    }
                    else if (ax == 6)
                    {
                        d = "Wednesday";
                    }


                   
                    // code block
                    break;
                case "Friday":
                    foreach (var c in x)
                    {


                        var dataj1 = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        if (DateTime.Compare(t1, Convert.ToDateTime(c.Time)) > 1)
                        {
                            var dataj = DateTime.Compare(t1, Convert.ToDateTime(c.Time));

                        }

                        if (c.Day == "Saturday")
                        {
                            Add.Add(1);
                            a = c.Day;
                        }
                        else if (c.Day == "Sunday")
                        {
                            Add.Add(2);

                            a = c.Day;
                        }
                        else if (c.Day == "Monday")
                        {
                            Add.Add(3);

                            a = c.Day;
                        }
                        else if (c.Day == "Tuesday")
                        {
                            Add.Add(4);

                            a = c.Day;
                        }
                        else if (c.Day == "Wednesday")
                        {
                            Add.Add(5);

                            a = c.Day;

                        }
                        else if (c.Day == "Thursday")
                        {
                            Add.Add(6);

                            a = c.Day;
                        }
                        else if (c.Day == "Friday")
                        {
                            Add.Add(7);

                            a = c.Day;
                        }

                    }
                    ax = Add[0];
                    foreach (var it in Add)
                    {
                        if (it < ax)
                        {
                            ax = it;
                        }
                    }

                    if (ax == 1)
                    {
                        d = "Saturday";
                    }
                    else if (ax == 2)
                    {
                        d = "Sunday";
                    }
                    else if (ax == 3)
                    {
                        d = "Monday";
                    }
                    else if (ax == 4)
                    {
                        d = "Tuesday";
                    }
                    else if (ax == 5)
                    {
                        d = "Wednesday";
                    }
                    else if (ax == 6)
                    {
                        d = "Thursday";
                    }



                   
                    // code block
                    break;
            }
            //////////////////////////

            var time8 = db.Courset.Where(x => x.Day == Day && x.FK_CourseId == id).Select(x => x.Time).ToList();

            if (time8.Count() != 1 && time8.Count() != 0)
            {
                var t = time8[0];
                foreach (var item in time8)
                {
                    var dataj = DateTime.Compare(t1, Convert.ToDateTime(item));


                    if (DateTime.Compare(t1, Convert.ToDateTime(item)) < 0)
                    {
                        var dataj1 = Convert.ToDateTime(t);


                        if (DateTime.Compare(dataj1, Convert.ToDateTime(item)) > 0)
                        {
                            time9 = item;
                            t = item;
                        }


                    }
                }
                Day1 = Day;

            }

            var time10 = db.Courset.Where(x => x.Day == d && x.FK_CourseId == id).Select(x => x.Time).ToList();
            if (time9 == "")
            {
                if (time10.Count() == 1)
                {
                    time9 = time10[0];
                }
                else
                {
                    time9 = time10[0];
                    foreach (var item in time10)
                    {

                        var dataj = Convert.ToDateTime(time9);



                        if (DateTime.Compare(dataj, Convert.ToDateTime(item)) > 0)
                        {
                            time9 = item;

                        }


                    }
                }

                Day1 = d;

            }
            if (time9 == "")
            {
                time9 = time8[0];
                Day1 = Day;
            }

            ///////////////////////////
            data.Link = null;
            data.NextLeather = Day1  +"-"+ time9 ; 
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
