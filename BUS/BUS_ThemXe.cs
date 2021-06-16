using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_ThemXe
    {
        DAL_ThemXe dalThemXe = new DAL_ThemXe();
        public bool ThemXe(DTO_Xe xe)
        {
            return dalThemXe.ThemXe(xe);
        }
    }
}
