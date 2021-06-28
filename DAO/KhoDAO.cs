using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class KhoDAO
    {
        private static KhoDAO instance;
        private KhoDAO() { }
        public DataTable LayKHO()
        {
            DataTable dt = new DataTable();
            String query = "Select * from KHO";
            dt = DataProvider.Instance.ExecuteQuery(query);
            return dt;
        }
        public static KhoDAO Instance
        {
            get { if (instance == null) instance = new KhoDAO(); return instance; }
            private set { KhoDAO.instance = value; }
        }
    }
}
