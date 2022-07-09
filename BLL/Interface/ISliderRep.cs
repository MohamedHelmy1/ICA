using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ISliderRep
    {
        bool Add(SliderViewModel slider);
        bool Edit(SliderViewModel slider);
        IQueryable<SliderViewModel> GetAll();
        SliderViewModel GetById(int id);

        bool Delete(int id);
    }
}
