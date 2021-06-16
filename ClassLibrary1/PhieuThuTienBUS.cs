using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;
using DTO;

namespace BUS
{
    public class PhieuThuTienBUS
    {
        private static PhieuThuTienBUS instance;
        public static PhieuThuTienBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new PhieuThuTienBUS();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        private PhieuThuTienBUS() { }
        public int LayTienNoKH(string BienSo)
        {
            DataTable dt = PhieuThuTienDAO.Instance.LayTienNoKH(BienSo);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int ThemPhieuThuTien(string BienSo, string SoTienThu, DateTime now)
        {
            return PhieuThuTienDAO.Instance.ThemPhieuThuTien(BienSo, int.Parse(SoTienThu), now);
        }

        public string[] LayThongTinKH(string BienSo)
        {
            DataTable dt = PhieuThuTienDAO.Instance.LayThongTinKH(BienSo);
            DTO_KhachHang KH = new DTO_KhachHang();
            string[] info = { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString() , dt.Rows[0][2].ToString() };
            return info;
        }
    }
}
