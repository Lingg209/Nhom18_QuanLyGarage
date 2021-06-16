using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAO
{
    public class BaoCaoTonDAO
    {
        private static BaoCaoTonDAO instance;

        public static BaoCaoTonDAO Instance { get {
                if (instance == null)
                    instance = new BaoCaoTonDAO();
                return instance;
            } private set => instance = value; }

        private BaoCaoTonDAO() { }

        public DataTable KhoiTaoBaoCaoTon()//Khởi tạo báo cáo tồn theo biểu mẫu
        {
            DataTable dt = PhieuSuaChuaDAO.Instance.TaoDataTable(6, new string[] { "STT","Mã", "Vật tư phụ tùng", "Tồn đầu", "Phát sinh", "Tồn cuối" });
            return dt;
        }

        public DataTable ThemHangBaoCaoTon(DataTable a,int ma, string vtpt, int TonDau, int PhatSinh, int TonCuoi)//Thêm nội dung 1 hàng cho báo cáo tồn
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
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from KHO");
            return dt;
        }

        public bool KiemTraThoiDiem(DateTime a)//Kiểm tra thời điểm nhập vào có từ thời điểm báo cáo đầu tiên về sau không
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from BAOCAOTON");
            foreach(DataRow row in dt.Rows)
            {
                int val = DateTime.Compare(a,(DateTime)row[1]);
                if (val >= 0)
                    return true;
            }
            return false;
        }

        public void TaoBaoCaoMoi(DataTable a)//Xóa các hàng của báo cáo
        {
            a.Clear();
        }

        public int SoLuongNhapVao(int MaPhuTung, int Thang, int Nam)//Lấy số lượng nhập vào của vtpt thông qua PHIEUNHAPVTPT theo tháng/năm
        {
            int amount = 0;
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from PHIEUNHAPVTPT where MaPhuTung = " + MaPhuTung + " and MONTH(ThoiDiem) = " + Thang + " and YEAR(ThoiDiem) = " + Nam);
            foreach (DataRow row in dt.Rows)
            {
                amount += int.Parse(row[2].ToString());
            }
            return amount;
        }

        public int SoLuongBanRa(int MaPhuTung, int Thang, int Nam)//Lấy số lượng bán ra của vtpt thông qua các PHIEUSUACHUA và CHITIETPHIEUSUACHUA theo tháng/năm
        {
            int amount = 0;
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from (XE join PHIEUSUACHUA on XE.BienSo = PHIEUSUACHUA.BienSo) join CHITIETPHIEUSUACHUA on PHIEUSUACHUA.MaPhieuSuaChua = CHITIETPHIEUSUACHUA.MaPhieuSuaChua where MaPhuTung = " + MaPhuTung + " and MONTH(NgayTiepNhan) = " + Thang + " and YEAR(NgayTiepNhan) = " + Nam);
            foreach (DataRow row in dt.Rows)
            {
                amount += int.Parse(row["SoLuongPhuTung"].ToString());
            }
            return amount;
        }

        public int SoLuongPhatSinh(int MaPhuTung, int Thang, int Nam)//Lấy số lượng phát sinh của vtpt theo tháng/năm
        {
            return SoLuongNhapVao(MaPhuTung, Thang, Nam) - SoLuongBanRa(MaPhuTung, Thang, Nam);
        }

        public int LuongTonDauThang(int MaPhuTung, int Thang, int Nam)//Lấy lượng tồn đầu tháng của vtpt dựa vào lượng tồn cuối tháng trước
        {
            int TonDau = 0;
            if (Thang == 1)
            {
                Thang = 13;
                Nam -= 1;
            }
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from BAOCAOTON join CT_BAOCAOTON on BAOCAOTON.MaBCT = CT_BAOCAOTON.MaBCT where MaPhuTung = " + MaPhuTung + " and MONTH(ThoiDiemBaoCao) = " + (Thang - 1) + " and YEAR(ThoiDiemBaoCao) = " + Nam);
            if (dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                {
                    TonDau = int.Parse(row["TonCuoi"].ToString());
                }
            else
                TonDau = 0;
            return TonDau;
        }

        public int LuongTonCuoiThang(int MaPhuTung, int Thang, int Nam)//Lấy lượng tồn cuối tháng thông qua lượng tồn đầu và số lượng phát sinh
        {
            return LuongTonDauThang(MaPhuTung, Thang, Nam) + SoLuongPhatSinh(MaPhuTung, Thang, Nam);
        }

        public void NhapBaoCaoTon(DataTable a, DateTime b)//Lưu báo cáo tồn xuống database
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select count(*) from BAOCAOTON");
            int MaBCT = int.Parse(dt.Rows[0][0].ToString());
            int re = DataProvider.Instance.ExecuteNonQuery("Insert into BAOCAOTON values (" + MaBCT + " , '" + b + "')");
            foreach(DataRow row in a.Rows)
            {
                re = DataProvider.Instance.ExecuteNonQuery("Insert into CT_BAOCAOTON values (" + MaBCT + " , " + int.Parse(row[1].ToString()) + " , " + int.Parse(row[3].ToString()) + " , " + int.Parse(row[4].ToString()) + " , " + int.Parse(row[5].ToString()) + ")");
            }
        }

        public DataTable TaoBaoCaoTon(DateTime b)//Tạo và trả về 1 báo cáo tồn từ 1 DataTable trống.
        {
            DataTable a;
            a = KhoiTaoBaoCaoTon();
            DataTable dt = LayDtKHO();
            foreach(DataRow row in dt.Rows)
            {
                a = ThemHangBaoCaoTon(a, int.Parse(row["MaPhuTung"].ToString()), row["TenVatTuPhuTung"].ToString(), LuongTonDauThang(int.Parse(row["MaPhuTung"].ToString()), b.Month, b.Year), SoLuongPhatSinh(int.Parse(row["MaPhuTung"].ToString()), b.Month, b.Year), LuongTonCuoiThang(int.Parse(row["MaPhuTung"].ToString()), b.Month, b.Year));
            }
            return a;
        }

        public DataTable TruyXuatBaoCaoTon(DateTime a)//Truy xuất báo cáo tồn đã được lưu trữ từ Database.
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from (BAOCAOTON join CT_BAOCAOTON on BAOCAOTON.MaBCT = CT_BAOCAOTON.MaBCT) join KHO on CT_BAOCAOTON.MaPhuTung = KHO.MaPhuTung where MONTH(ThoiDiemBaoCao) = " + a.Month + " and YEAR(ThoiDiemBaoCao) = " + a.Year);
            DataTable b = KhoiTaoBaoCaoTon();
            foreach (DataRow row in dt.Rows)
            {
                b = ThemHangBaoCaoTon(b, int.Parse(row["MaPhuTung"].ToString()), row["TenVatTuPhuTung"].ToString(), int.Parse(row["TonDau"].ToString()), int.Parse(row["PhatSinh"].ToString()), int.Parse(row["TonCuoi"].ToString()));
            }
            return b;
        }

        public bool KiemTraDuLieuBaoCao(DateTime a)//Kiểm tra xem đã có báo cáo tồn của thời điểm a hay chưa.
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select count(*) from BAOCAOTON where MONTH(ThoiDiemBaoCao) = " + a.Month + " and YEAR(ThoiDiemBaoCao) = " + a.Year);
            int re =  int.Parse(dt.Rows[0][0].ToString());
            if (re == 1)
                return true;
            return false;
        }

       public bool ThangDauTien()
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select count(*) from BAOCAOTON");
            int re = int.Parse(dt.Rows[0][0].ToString());
            if (re == 0)
                return true;
            return false;
        }

        //public void TaoBaoCaoTonThangDau(DateTime t)
        //{
        //    if(ThangDauTien())
        //    {
        //        DataTable dt = DataProvider.Instance.ExecuteQuery("Select * from KHO");
        //        int re = DataProvider.Instance.ExecuteNonQuery("Insert into BAOCAOTON values (" + 0 + " , " + t + " )");
        //        foreach(DataRow row in dt.Rows)
        //        {

        //        }
        //    }
        //}

    }
}
