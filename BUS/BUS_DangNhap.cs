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
    public class BUS_DangNhap
    {
        DAL_DangNhap dalDangNhap = new DAL_DangNhap();
        public bool checkDangNhap(DTO_DangNhap dn)
        {
            DataTable dt = dalDangNhap.sosanhUsernamePassword(dn);
            if (dt.Rows.Count > 0) return true;
            else return false;
        }
    }
}
