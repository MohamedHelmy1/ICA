using BLL.Interface;
using DAL.Database;
using DAL.ViewModel;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Helper;

namespace BLL.Servies
{
    public class SliderRep : ISliderRep
    {
        private readonly ApplicationDbContext db;

        public SliderRep(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool Add(SliderViewModel slider)
        {
            try
            {
                Slider obj = new Slider();
                obj.Name = slider.Name;
                obj.Description = slider.Description;
                if (slider.Images != null)
                {
                    obj.Image = UploodImage.SaveFile(slider.Images, "Image");
                }
                db.Slider.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public bool Delete(int id)
        {
            try
            {
                var data = db.Slider.Where(x => x.Id == id).FirstOrDefault();
                db.Slider.Remove(data);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }

        }

        public bool Edit(UpdateSliderViewModel slider)
        {
            try
            {
                var data = db.Slider.Where(x => x.Id == slider.Id).FirstOrDefault();
               data.Name=slider.Name;
                data.Description=slider.Description;
                if (slider.Images != null)
                {
                    UploodImage.RemoveFile(data.Image, "Image");
                    data.Image = UploodImage.SaveFile(slider.Images, "Image");
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public IQueryable<SliderViewModel> GetAll()
        {
            return db.Slider.Select(x => new SliderViewModel
            {
                Id=x.Id,
                Name=x.Name,
                Description=x.Description,
                Image=x.Image,
            });
        }

        public UpdateSliderViewModel GetById(int id)
        {
            return db.Slider.Where(z=>z.Id==id).Select(x => new UpdateSliderViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Image = x.Image,
            }).FirstOrDefault();
        }
    }
}
