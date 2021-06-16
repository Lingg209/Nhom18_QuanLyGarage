using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;

namespace BUS
{
    public class BaoCaoTonBUS
    {
        private static BaoCaoTonBUS instance;
        private BaoCaoTonBUS() { }
        public static BaoCaoTonBUS Instance
        {
            get { if (instance == null) instance = new BaoCaoTonBUS(); return instance; }
            private set { BaoCaoTonBUS.instance = value; }
        }
        public DataTable KhoiTaoBaoCaoTon()//Khởi tạo báo cáo tồn theo biểu mẫu
        {
            DataTable dt = PhieuSuaChuaDAO.Instance.TaoDataTable(6, new string[] { "STT", "Mã", "Vật tư phụ tùng", "Tồn đầu", "Phát sinh", "Tồn cuối" });
            return dt;
        }

        public DataTable ThemHangBaoCaoTon(DataTable a, int ma, string vtpt, int TonDau, int PhatSinh, int TonCuoi)//Thêm nội dung 1 hàng cho báo cáo tồn
        {
            DataRow dr = a.NewRow();
            dr["STT"] = a.Rows.Count;
            dr["Mã"] = ma;
            dr["Vật tư phụ tùng"] = vtpt;
            dr["Tồn đầu"] = TonDau;
            dr["Phát sinh"] = PhatSinh;
            dr["Tồn cuối"] = TonCuoi;
            a.Rows.Add(dr);
            return a;
        }

        public DataTable LayDtKHO()//Lấy DataTable từ KHO
        {
            return BaoCaoTonDAO.Instance.LayDtKHO();
        }

        public bool KiemTraThoiDiem(DateTime a)//Kiểm tra thời điểm nhập vào có từ thời điểm báo cáo đầu tiên về sau không
        {
            return BaoCaoTonDAO.Instance.KiemTraThoiDiem(a);
        }

        public void TaoBaoCaoMoi(DataTable a)//Xóa các hàng của báo cáo
        {
            a.Clear();
        }

        public int SoLuongNhapVao(int MaPhuTung, int Thang, int Nam)//Lấy số lượng nhập vào của vtpt thông qua PHIEUNHAPVTPT theo tháng/năm
        {
            return BaoCaoTonDAO.Instance.SoLuongNhapVao(MaPhuTung, Thang, Nam);
        }

        public int SoLuongBanRa(int MaPhuTung, int Thang, int Nam)//Lấy số lượng bán ra của vtpt thông qua các PHIEUSUACHUA và CHITIETPHIEUSUACHUA theo tháng/năm
        {
            return BaoCaoTonDAO.Instance.SoLuongBanRa(MaPhuTung, Thang, Nam);
        }

        public int SoLuongPhatSinh(int MaPhuTung, int Thang, int Nam)//Lấy số lượng phát sinh của vtpt theo tháng/năm
        {
            return SoLuongNhapVao(MaPhuTung, Thang, Nam) - SoLuongBanRa(MaPhuTung, Thang, Nam);
        }

        public int LuongTonDauThang(int MaPhuTung, int Thang, int Nam)//Lấy lượng tồn đầu tháng của vtpt dựa vào lượng tồn cuối tháng trước
        {
            return BaoCaoTonDAO.Instance.LuongTonDauThang(MaPhuTung, Thang, Nam);
        }

        public int LuongTonCuoiThang(int MaPhuTung, int Thang, int Nam)//Lấy lượng tồn cuối tháng thông qua lượng tồn đầu và số lượng phát sinh
        {
            return LuongTonDauThang(MaPhuTung, Thang, Nam) + SoLuongPhatSinh(MaPhuTung, Thang, Nam);
        }

        public void NhapBaoCaoTon(DataTable a, DateTime b)//Lưu báo cáo tồn xuống database
        {
            BaoCaoTonDAO.Instance.NhapBaoCaoTon(a, b);
        }

        public DataTable TaoBaoCaoTon(DateTime b)//Tạo và trả về 1 báo cáo tồn từ 1 DataTable trống.
        {
            DataTable a;
            a = KhoiTaoBaoCaoTon();
            DataTable dt = LayDtKHO();
            foreach (DataRow row in dt.Rows)
            {
                a = ThemHangBaoCaoTon(a, int.Parse(row["MaPhuTung"].ToString()), row["TenVatTuPhuTung"].ToString(), LuongTonDauThang(int.Parse(row["MaPhuTung"].ToString()), b.Month, b.Year), SoLuongPhatSinh(int.Parse(row["MaPhuTung"].ToString()), b.Month, b.Year), LuongTonCuoiThang(int.Parse(row["MaPhuTung"].ToString()), b.Month, b.Year));
            }
            return a;
        }

        public DataTable TruyXuatBaoCaoTon(DateTime a)//Truy xuất báo cáo tồn đã được lưu trữ từ Database.
        {
            DataTable dt = BaoCaoTonDAO.Instance.TruyXuatBaoCaoTon(a);
            DataTable b = KhoiTaoBaoCaoTon();
            foreach (DataRow row in dt.Rows)
            {
                b = ThemHangBaoCaoTon(b, int.Parse(row["MaPhuTung"].ToString()), row["TenVatTuPhuTung"].ToString(), int.Parse(row["TonDau"].ToString()), int.Parse(row["PhatSinh"].ToString()), int.Parse(row["TonCuoi"].ToString()));
            }
            return b;
        }

        public bool KiemTraDuLieuBaoCao(DateTime a)//Kiểm tra xem đã có báo cáo tồn của thời điểm a hay chưa.
        {
            return BaoCaoTonDAO.Instance.KiemTraDuLieuBaoCao(a);
        }

        public bool ThangDauTien()
        {
            return BaoCaoTonDAO.Instance.ThangDauTien();
        }
    }
}
