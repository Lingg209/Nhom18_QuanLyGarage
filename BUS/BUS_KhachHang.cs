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
    public class BUS_KhachHang
    {
        DAL_KhachHang dalKhachHang = new DAL_KhachHang();
        public bool ThemKhachHang(DTO_KhachHang kh)
        {
            return dalKhachHang.ThemKhachHang(kh);
        }
    }
}
