using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;

namespace BUS
{
    public class QuyDinhBUS
    {
        private static QuyDinhBUS instance;
        public static QuyDinhBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuyDinhBUS();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        private QuyDinhBUS() { }
        public DataTable LayTatCaQuyDinh()
        {
            return DAO.QuyDinhDAO.Instance.LayTatCaQuyDinh();
        }
        public int LaySoXeSuaToiDa()
        {
            DataTable dt = QuyDinhDAO.Instance.LaySoXeSuaToiDa();
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public int CapNhatSoXeSuaToiDa(string GiaTriMoi)
        {
            int gtm = int.Parse(GiaTriMoi);
            return DAO.QuyDinhDAO.Instance.CapNhatSoXeSuaToiDa(gtm);
        }
        
    }
}
