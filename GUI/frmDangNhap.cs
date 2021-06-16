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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }


        private void buttonDangNhap_Click(object sender, EventArgs e)
        {
            if (textDangNhap.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tài khoản!");
            }
            else if (textMatKhau.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
            }
            else
            {
                string TaiKhoan = textDangNhap.Text;
                string MatKhau = textMatKhau.Text;
                if (DangNhap(TaiKhoan, MatKhau))
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông Báo");
                    frmMain f = new frmMain();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    textDangNhap.Clear();
                    textMatKhau.Clear();
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại tên hoặc mật khẩu!");
                }

            }
        }
        bool DangNhap(string TaiKhoan, string MatKhau)
        {
            return TaiKhoanBUS.Instance.DangNhap(TaiKhoan,MatKhau);
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
