using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class frmThongTinTaiKhoan : Form
    {
        public frmThongTinTaiKhoan()
        {
            InitializeComponent();
        }


        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmThayDoiMatKhau tdmk = new frmThayDoiMatKhau();
            tdmk.ShowDialog();
        }

        private void FrmThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            txtBoxHoTen.Text = TaiKhoanBUS.Instance.LayHoTen();
            txtBoxTaiKhoan.Text = TaiKhoanBUS.Instance.LayTenTaiKhoan();
            txtBoxQuyenHan.Text = TaiKhoanBUS.Instance.LayQuyenHan();
        }
    }
}
