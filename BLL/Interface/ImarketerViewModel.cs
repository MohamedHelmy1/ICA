﻿using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ImarketerViewModel
    {

        IEnumerable<UserViewModel> GetMarketerUser(string id);

    }
}
