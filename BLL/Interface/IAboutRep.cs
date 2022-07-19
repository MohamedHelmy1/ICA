using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IAboutRep
    {
        bool About(AboutViewModel About);
        bool Contant(ContactViewmodel contant);

        ContactViewmodel Getcontact();

        AboutViewModel GetAbout();
        bool Active(ActiveteCoursesViewModel Active);
        ActiveteCoursesViewModel GetActive();
    }
}
