using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
    public class KhoBUS
    {
        private static KhoBUS instance;
        private KhoBUS() { }
        public DataTable LayKHO()
        {
            return DAO.KhoDAO.Instance.LayKHO();
        }
        public static KhoBUS Instance
        {
            get { if (instance == null) instance = new KhoBUS(); return instance; }
            private set { KhoBUS.instance = value; }
        }
    }
}
