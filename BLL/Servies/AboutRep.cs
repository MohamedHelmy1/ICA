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
    public class AboutRep : IAboutRep
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public AboutRep(ApplicationDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        #region About
        public bool About(AboutViewModel About)
        {
            var data = db.About.FirstOrDefault();
            if (data == null)
            {
                try
                {
                    var data1 = mapper.Map<About>(About);

                    db.About.Add(data1);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                try
                {
                    data.AboutUs = About.AboutUs;
                    data.OurGoal = About.OurGoal;
                    data.WhyUs = About.WhyUs;
                    data.name = About.name;
                    data.message = About.message;

                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        public AboutViewModel GetAbout()
        {
            var data = db.About.FirstOrDefault();
            return mapper.Map<AboutViewModel>(data);
        }
        #endregion

        #region Active
        public bool Active(ActiveteCoursesViewModel Active)
        {
            var data = db.ActiveteCourses.FirstOrDefault();
            if (data == null)
            {
                try
                {
                    ActiveteCourses obj = new ActiveteCourses();
                    obj.Name = Active.Name;

                    db.ActiveteCourses.Add(obj);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                try
                {
                    data.Name = Active.Name;

                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
    
        public ActiveteCoursesViewModel GetActive()
        {
            ActiveteCoursesViewModel active=new ActiveteCoursesViewModel();
            var data = db.ActiveteCourses.FirstOrDefault();
            if(data != null)
            {
                active.Name = data.Name;
                active.Id = data.Id;
            }
            
            return active;
        }
        #endregion
    }
}
