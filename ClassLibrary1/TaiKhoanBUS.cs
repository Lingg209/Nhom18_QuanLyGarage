using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;
namespace BUS
{
    public class TaiKhoanBUS
    {
        private static TaiKhoanBUS instance;
        private string User = "";
        private string Pass = "";

        private TaiKhoanBUS() { }

        public static TaiKhoanBUS Instance
        {
            get { if (instance == null) instance = new TaiKhoanBUS(); return instance; }
            private set { TaiKhoanBUS.instance = value; }
        }

        void LuuThongTinNguoiDungHienTai(string user, string pass)
        {
            User = user;
            Pass = pass;
        }

        public void XoaThongTinNguoiDungGanNhat()
        {
            User = "";
            Pass = "";
        }
        public bool DangNhap(string TaiKhoan, string MatKhau)
        {

            DataTable result = TaiKhoanDAO.Instance.DangNhap(TaiKhoan, MatKhau);

            if (result.Rows.Count > 0)
            {
                LuuThongTinNguoiDungHienTai(TaiKhoan, MatKhau);
                return true;
            }
            return false;
        }
        public string LayQuyenHan()
        {
            return TaiKhoanDAO.Instance.LayQuyenHan(User, Pass);
        }

        public string LayHoTen()
        {
            return TaiKhoanDAO.Instance.LayHoTen(User, Pass);
        }

        public int LayMaTaiKhoan()
        {
            return TaiKhoanDAO.Instance.LayMaTaiKhoan(User, Pass);
        }

        public string LayTenTaiKhoan()
        {
            return User;
        }

        public string LayMatKhau()
        {
            return Pass;
        }

        public void CapNhatMatKhau(string matkhau, int id)
        {
            TaiKhoanDAO.Instance.CapNhatMatKhau(matkhau, id);
        }

    }
}
