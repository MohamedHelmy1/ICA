using BLL.Interface;
using DAL.Database;
using DAL.Entities;
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

        public UserCourseRep(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool AddCouse(int id, string UserId)
        {
            var data=db.UserCourses.Where(x=>x.UserId == UserId&&x.Fk_CouresId==id).FirstOrDefault();
            if (data==null)
            {
                UserCourses obj = new UserCourses();
                obj.UserId = UserId;
                obj.Fk_CouresId = id;
                db.UserCourses.Add(obj);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
