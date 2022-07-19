using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interface;
using DAL.Database;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servies
{
    public class UserCourseRep: IUserCourseRep
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<AplicationUser> userManager;
        private readonly IMapper mapper;

        public UserCourseRep(ApplicationDbContext db,UserManager<AplicationUser> userManager,IMapper mapper)
        {
            this.db = db;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public bool AddCouse(int id, string UserId)
        {
            var data=db.UserCourses.Where(x=>x.UserId == UserId&&x.Fk_CouresId==id&&x.state==1).FirstOrDefault();
            if (data==null)
            {
                UserCourses obj = new UserCourses();
                obj.UserId = UserId; 
                obj.Fk_CouresId = id;
                obj.state = 0;
                obj.Date = DateTime.Now.ToString();
                db.UserCourses.Add(obj);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<UserCoursesViewModel>> GetAllCoursetoAdminAcsept()
        {
            
            List<UserCoursesViewModel> userCourses = new List<UserCoursesViewModel>();
            var data = db.UserCourses.Where(x => x.state == 0);
            foreach (var item in data)
            {
                UserCoursesViewModel obj = new UserCoursesViewModel();
                obj.CourseName = db.Courses.Where(c => c.Id == item.Fk_CouresId&&c.Delete==false).Select(c => c.Name).FirstOrDefault();
                obj.id = item.id;
                obj.UserName = (await userManager.FindByIdAsync(item.UserId)).NameOfUser;
                obj.Date = item.Date;
                obj.cuntery= (await userManager.FindByIdAsync(item.UserId)).Cuntery;
                obj.Email = (await userManager.FindByIdAsync(item.UserId)).Email;
                obj.Phone = (await userManager.FindByIdAsync(item.UserId)).PhoneNumber;


                userCourses.Add(obj);
            }
            return userCourses;
               
            
        }
        public bool AdminAcceptuser(int id)
        {
            var data = db.UserCourses.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                data.state = 1;
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public bool AdminNotAcceptuser(int id)
        {
            var data = db.UserCourses.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                data.state = -1;// اترفض
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<CoursesViewModel> GetUserCourses( string userId)
        {
            List<CoursesViewModel> courses = new List<CoursesViewModel>();
            var data = db.UserCourses.Where(x => x.UserId == userId && x.state != 0);
            foreach (var item in data)
            {
                var course = db.Courses.Where(x => x.Id == item.Fk_CouresId).FirstOrDefault();
                courses.Add(new CoursesViewModel
                {
                    Id=course.Id,
                    Name=course.Name,   
                    Description=course.Description, 
                    Image=course.Image,
                    Link=course.Link,
                    NextLeather=course.NextLeather,
                    state=item.state,
                    

                });

            }

            return courses;

        }
        public async Task <IEnumerable<UserViewModel>> GetUserinCourses(int CourseId)
        {
            List<UserViewModel> Users = new List<UserViewModel>();
            var data = db.UserCourses.Where(x => x.Fk_CouresId == CourseId && x.state == 1);
            foreach (var item in data)
            {
                UserViewModel obj = new UserViewModel();
               var user= await userManager.FindByIdAsync(item.UserId);
                obj.Email = user.Email;
                obj.name = user.NameOfUser;
                obj.Date = item.Date;
                obj.UerId = item.UserId;
                obj.Cuntery = user.Cuntery;
                obj.phone = user.PhoneNumber;
                obj.id = item.id;

                Users.Add(obj);
            }

            return Users;

        }

        public bool RemoveUser(int id)
        {
            var data=db.UserCourses.Where(x=>x.id==id).FirstOrDefault();
            data.state = -1;
            db.SaveChanges();
            return true;
        }

        public bool Fishcourse(int id)
        {
            var data = db.UserCourses.Where(x => x.Fk_CouresId == id&&x.state==1).FirstOrDefault();
            data.state = 2;
            db.SaveChanges();
            return true;

        }
    }
}
