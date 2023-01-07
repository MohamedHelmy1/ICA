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
    public class marketerViewModel : ImarketerViewModel
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<AplicationUser> userManger;

        public marketerViewModel( ApplicationDbContext db,UserManager <AplicationUser> userManger)
        {
            this.db = db;
            this.userManger = userManger;
        }

        public IEnumerable<UserViewModel> GetMarketerUser(string id)
        {
            var data=db.Users.Where(u => u.FK_MarketerId == id).Select(x=> new UserViewModel
            {
                FK_MarketerId = x.FK_MarketerId,
                UerId=x.Id,
                name=x.NameOfUser,
                Email=x.Email,
                Cuntery=x.Cuntery,
                phone=x.PhoneNumber,
                Date=x.Date
                


            }).ToList();
            return data;
        }
    }
}
