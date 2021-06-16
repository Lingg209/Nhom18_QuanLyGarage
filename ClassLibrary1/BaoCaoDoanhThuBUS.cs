using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;

namespace BUS
{
    public class BaoCaoDoanhThuBUS
    {
        private static BaoCaoDoanhThuBUS instance;
        public static BaoCaoDoanhThuBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaoCaoDoanhThuBUS();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        private BaoCaoDoanhThuBUS() { }

        public DataTable BaoCaoDoanhThu(string Thang, string Nam)
        {
            return BaoCaoDoanhThuDAO.Instance.BaoCaoDoanhThu(int.Parse(Thang), int.Parse(Nam));
        }
        public string TongTienDoanhThu(string Thang, string Nam)
        {
            DataTable dt = BaoCaoDoanhThuDAO.Instance.TongTienDoanhThu(int.Parse(Thang), int.Parse(Nam));
            return dt.Rows[0][0].ToString();
        }
    }
}
