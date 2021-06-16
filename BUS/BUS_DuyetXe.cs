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
    public class BUS_DuyetXe
    {
        DAL_DuyetXe dalDuyetXe = new DAL_DuyetXe();

        public DataTable getXe()
        {
            return dalDuyetXe.getXe();
        }
    }
}
