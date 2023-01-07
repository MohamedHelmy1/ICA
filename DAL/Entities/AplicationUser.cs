using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AplicationUser:IdentityUser
    {
        public string Cuntery { get; set; }
        public string Date { get; set; }

        public string NameOfUser{ get; set; }
        public string FK_MarketerId { get; set; }
        [ForeignKey("FK_MarketerId")]
        public virtual AplicationUser User { get; set; }



    }
}
