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
        bool Edit(UpdateSliderViewModel slider);
        IQueryable<SliderViewModel> GetAll();
        UpdateSliderViewModel GetById(int id);

        bool Delete(int id);
    }
}
