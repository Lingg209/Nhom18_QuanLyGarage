using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;

namespace BUS
{
    public class PhieuSuaChuaBUS
    {
        private static PhieuSuaChuaBUS instance;
        private PhieuSuaChuaBUS() { }
        private DataTable VTPTDangNhap;
        public static PhieuSuaChuaBUS Instance
        {
            get { if (instance == null) instance = new PhieuSuaChuaBUS(); return instance; }
            private set { PhieuSuaChuaBUS.instance = value; }
        }

        public void KhoiTaoDtVTPTDangNhap()//Khởi tạo mới cho dt VTPT Đang Nhập
        {
            VTPTDangNhap = new DataTable();
            VTPTDangNhap = TaoDataTable(3, new string[] { "ma", "slhientai", "slchophep" });
        }

        public DataTable KhoiTaoDtVTPT()
        {
            return TaoDataTable(6, new string[] { "STT", "Vật tư phụ tùng", "Số lượng", "Đơn giá", "Thành tiền", "Mã phụ tùng" });
        }

        public DataTable KhoiTaoDtTienCong()
        {
            return TaoDataTable(4, new string[] { "STT", "Nội dung", "Chi phí", "Mã tiền công" });
        }

        public DataTable TaoDataTable(int a, string[] name)//Tạo dt với số cột và nội dung từng cột
        {
            return DAO.PhieuSuaChuaDAO.Instance.TaoDataTable(a, name);
        }

        public DataTable ThemHangVTPT(DataTable dt, string VTPT, string Soluong, int DonGia, string MaPhuTung)//Thêm 1 hàng cho dt VTPT với các nội dung nhập vào
        {
            int a = dt.Rows.Count + 1;
            int sl = int.Parse(Soluong);
            return DAO.PhieuSuaChuaDAO.Instance.ThemHangVTPT(dt, a, VTPT, sl, DonGia, MaPhuTung);
        }

        public DataTable ThemHangTienCong(DataTable dt, string NoiDung, int ChiPhi, string MaTC)//Thêm 1 hàng tiền công cho dt Tiền công với các nội dung nhập vào
        {
            int a = dt.Rows.Count + 1;
            return DAO.PhieuSuaChuaDAO.Instance.ThemHangTienCong(dt, a, NoiDung, ChiPhi, MaTC);
        }

        public int LayDonGiaVTPT(string maVTPT)//Lấy đơn giá của VTPT theo mã
        {
            return DAO.PhieuSuaChuaDAO.Instance.LayDonGiaVTPT(maVTPT);
        }

        public int LayChiPhiTienCong(string maTienCong)//Lấy chi phí của tiền công theo mã
        {
            return DAO.PhieuSuaChuaDAO.Instance.LayChiPhiTienCong(maTienCong);
        }

        public string LayNoiDungTienCong(string maTienCong)//Lấy chi phí của tiền công theo mã
        {
            return DAO.PhieuSuaChuaDAO.Instance.LayNoiDungTienCong(maTienCong);
        }

        public void LuuPhieuSuaChua(string BienSo, int TienCong, int TienPhuTung, int TongTien, DataTable TC, DataTable VTPT)//Lưu dữ liệu được nhập vào 2 bảng PHIEUSUACHUA và CHITIETPHIEUSUACHUA
        {
            DAO.PhieuSuaChuaDAO.Instance.LuuPhieuSuaChua(BienSo, TienCong, TienCong, TongTien, TC, VTPT);

        }

        public void NhapVTPT(DataTable VTPT)//Thực hiện cập nhật lại số lượng vtpt còn lại trong kho.
        {
            DAO.PhieuSuaChuaDAO.Instance.NhapVTPT(VTPT);
        }


        public bool KiemTraSoLuong(string MaVTPT, int SoLuong)//Kiểm tra số lượng nhập vào có nằm trong khả năng đáp ứng của kho.
        {
            bool TonTai = false;
            int Tong = 0;
            foreach (DataRow row in VTPTDangNhap.Rows)
            {
                if (row["ma"].ToString() == MaVTPT)
                {
                    TonTai = true;
                    Tong = SoLuong + int.Parse(row["slhientai"].ToString());
                    if (Tong > int.Parse(row["slchophep"].ToString()))
                    {
                        return false;
                    }
                    else
                    {
                        row["slhientai"] = Tong.ToString();
                    }
                }
            }
            if (!TonTai)
            {
                return DAO.PhieuSuaChuaDAO.Instance.KiemTraSoLuong(VTPTDangNhap,MaVTPT, SoLuong);
            }
            return true;
        }

        public void XoaDtVTPTDangNhap()//Thực hiện xóa các hàng của dt VTPT Đang nhập
        {
            VTPTDangNhap.Clear();
        }

        public void CapNhatTienNo(string BienSo, int TienSuaChua)//Cập nhật tiền nợ của khách hàng có xe vừa được sửa chữa.
        {
            DAO.PhieuSuaChuaDAO.Instance.CapNhatTienNo(BienSo, TienSuaChua);
        }
    }
}
