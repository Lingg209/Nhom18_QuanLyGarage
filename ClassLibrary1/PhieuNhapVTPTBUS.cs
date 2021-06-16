using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
    public class PhieuNhapVTPTBUS
    {
        private static PhieuNhapVTPTBUS instance;
        private PhieuNhapVTPTBUS() { }
        public static PhieuNhapVTPTBUS Instance
        {
            get { if (instance == null) instance = new PhieuNhapVTPTBUS(); return instance; }
            private set { PhieuNhapVTPTBUS.instance = value; }
        }
        public int NhapVTPT(string ten, string soluong, DateTime now)
        {
            return PhieuNhapVTPTDAO.Instance.NhapVTPT(ten, int.Parse(soluong), now);
        }
        public int NhapMoiVTPT(string ten, string soluong, string gia, DateTime now)
        {
            return PhieuNhapVTPTDAO.Instance.NhapMoiVTPT(ten, int.Parse(soluong), int.Parse(gia), now);
        }
    }
}
