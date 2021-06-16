using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO;

namespace GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region Parameters
        private bool btnHoanTatClicked = false;
        private bool textBoxTenVTPTMoi_TextChanged = false;
        private bool textBoxGiaVTPT_TextChanged = false;
        #endregion

        #region Methods
        void DoiDateTimePickerFormat(DateTimePicker dtp) //Ham thuc hien chuyen format DateTimePicker sang MM/yyyy.
        {
                dtp.CustomFormat = "MM/yyyy";
                dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                dtp.ShowUpDown = true;
        }

        string LayNgayThangNamHienTai() //Ham thuc hien lay ngay/thang/nam thoi diem hien tai.
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
        void DatThoiDiemHienTai(TextBox tb) //Ham dat noi dung textbox la thoi diem hien tai.
        {
            tb.Text = LayNgayThangNamHienTai();
        }
        void DatLaiDateTimePicker(DateTimePicker dtp) //Dat lai gia tri DatTimePicker thanh hom nay.
        {
            dtp.Value = DateTime.Now;
        }
        void DatVisibleChoControl(Control ctrl, bool result) //Dat thuoc tinh Visible cho Control.
        {
            ctrl.Visible = result;
        }

        void KhoiTaoDataGridviewVTPT()
        {
            DataTable dt = PhieuSuaChuaDAO.Instance.TaoDataTable(6, new string[] { "STT", "Vật tư phụ tùng", "Số lượng", "Đơn giá", "Thành tiền", "Mã phụ tùng" });
            dataGridViewVTPTPhieuSuaChua.DataSource = dt;
            dataGridViewVTPTPhieuSuaChua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewVTPTPhieuSuaChua.AutoResizeColumns();

            //dataGridViewVTPTPhieuSuaChua.Columns["Mã phụ tùng"].Visible = false;
        }

        void KhoiTaoDataGridviewTienCong()
        {
            dataGridViewTienCongPhieuSuaChua.DataSource = PhieuSuaChuaDAO.Instance.TaoDataTable(4, new string[] { "STT", "Nội dung", "Chi phí" ,"Mã tiền công"});
            dataGridViewTienCongPhieuSuaChua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTienCongPhieuSuaChua.AutoResizeColumns();

            //dataGridViewVTPTPhieuSuaChua.Columns["Mã tiền công"].Visible = false;
        }

        void NhapVTPTChoPhieuSuaChua(int DonGia)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataGridViewVTPTPhieuSuaChua.DataSource;
            PhieuSuaChuaDAO.Instance.ThemHangVTPT(dt, dt.Rows.Count + 1, comboBoxVTPTPhieuSuaChua.Text, int.Parse(textBoxSoLuongVTPTPhieuSuaChua.Text), DonGia, comboBoxVTPTPhieuSuaChua.SelectedValue.ToString());
        }

        void NhapTienCongChoPhieuSuaChua(int ChiPhi)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataGridViewTienCongPhieuSuaChua.DataSource;
            PhieuSuaChuaDAO.Instance.ThemHangTienCong(dt, dt.Rows.Count + 1, PhieuSuaChuaDAO.Instance.LayNoiDungTienCong(comboBoxTienCongPhieuSuaChua.SelectedValue.ToString()), ChiPhi, comboBoxTienCongPhieuSuaChua.SelectedValue.ToString());
        }

        int TinhTongThanhTien()
        {
            int TongThanhTien = 0;
            foreach (DataGridViewRow row in dataGridViewVTPTPhieuSuaChua.Rows)
            {
                TongThanhTien += int.Parse(row.Cells["Thành tiền"].Value.ToString());
            }
            return TongThanhTien;
        }

        int TinhTongChiPhi()
        {
            int TongChiPhi = 0;
            foreach (DataGridViewRow row in dataGridViewTienCongPhieuSuaChua.Rows)
            {
                TongChiPhi += int.Parse(row.Cells["Chi phí"].Value.ToString());
            }
            return TongChiPhi;
        }

        void KiemTraDuLieuBaoCaoKhiLoad(DateTime a)
        {
            if (!BaoCaoTonDAO.Instance.KiemTraDuLieuBaoCao(a))
            {
                DataTable dt = new DataTable();
                BaoCaoTonDAO.Instance.NhapBaoCaoTon(BaoCaoTonDAO.Instance.TaoBaoCaoTon(a), a);
            }
        }

        bool QuyenHanAdmin()//Kiểm tra tài khoản hiện tại có phải là admin không
        {
            if (TaiKhoanDAO.Instance.LayQuyenHan().ToUpper() == "ADMIN")
                return true;
            return false;
        }

        void GioiHanQuyenHan()//Thực hiện giới hạn quyền hạn lên các tài khoản không phải là admin.
        {
            if(!QuyenHanAdmin())
            {
                tCtrlChinh.TabPages.Remove(tCtrlChinh.TabPages[2]);
                tCtrlChucNang.TabPages.Remove(tCtrlChucNang.TabPages[3]);
                tCtrlChinh.TabPages.Remove(tCtrlChinh.TabPages[1]);
            }
        }

        void DatMacDinhChoComboBox(ComboBox a)//Thực hiện đặt giá trị mặc định cho comboBox để tránh lỗi.
        {
            a.SelectedItem = null;
            a.Text = "--select--";
        }
        #endregion

        #region Events
        private void ThôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTinTaiKhoan tttk = new frmThongTinTaiKhoan();
            tttk.ShowDialog();
        }


        private void ĐăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void LabelTenKhachHang_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.TIENCONG' table. You can move, or remove it, as needed.
            this.tIENCONGTableAdapter.Fill(this.quanLyGarageDataSet.TIENCONG);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.KHO' table. You can move, or remove it, as needed.
            this.kHOTableAdapter.Fill(this.quanLyGarageDataSet.KHO);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.XE' table. You can move, or remove it, as needed.
            this.xETableAdapter.Fill(this.quanLyGarageDataSet.XE);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.HIEUXE' table. You can move, or remove it, as needed.
            this.hIEUXETableAdapter.Fill(this.quanLyGarageDataSet.HIEUXE);
            // Lấy dữ liệu các xe đã tiếp nhận
            string query = "SELECT BienSo, TenHieuXe, TenKH, DienThoai, DiaChi FROM XE, HIEUXE as HX, KHACHHANG as KH WHERE XE.MaKH = KH.MaKH and XE.MaHX = HX.MaHX and XE.TrangThai = 1";
            dataGridViewXeDaTiepNhan.DataSource = DataProvider.Instance.ExecuteQuery(query);
            dataGridViewXeDaTiepNhan.Show();
            // Lấy thông tin cho progressbar số xe đã tiếp nhận 1 ngày
            DateTime now = DateTime.Now;
            textBoxNgayThuTien.Text = now.ToString("dd-MM-yyyy");
            DataTable iSoXeDaTiepNhanTrongNgay;
            query = "SELECT GiaTri FROM THAMSO WHERE MaThamSo = 'TS2'";
            iSoXeDaTiepNhanTrongNgay = DataProvider.Instance.ExecuteQuery(query);
            progressBarSoXeDaThem.Maximum = int.Parse(iSoXeDaTiepNhanTrongNgay.Rows[0][0].ToString());
            query = "SELECT COUNT(BienSo) FROM XE WHERE day(NgayTiepNhan) = " + now.Day + " and month(NgayTiepNhan) = " + now.Month + " and year(NgayTiepNhan) = " + now.Year;
            iSoXeDaTiepNhanTrongNgay = DataProvider.Instance.ExecuteQuery(query);
            progressBarSoXeDaThem.Value = int.Parse(iSoXeDaTiepNhanTrongNgay.Rows[0][0].ToString());

            // Điển ngày thu tiền
            textBoxNgayThuTien.Text = now.ToString("dd-MM-yyyy");
            //Khởi tạo Datagridview Phiếu sửa chữa và tiền công
            KhoiTaoDataGridviewTienCong();
            KhoiTaoDataGridviewVTPT();
            //Khởi tạo 1 dt để lưu lại các mã vtpt đã nhập và kiểm tra, so sánh số lượng nhập vào.
            PhieuSuaChuaDAO.Instance.KhoiTaoDtVTPTDangNhap();
            KiemTraDuLieuBaoCaoKhiLoad(DateTime.Now);
            dataGridViewBaoCaoTon.DataSource = BaoCaoTonDAO.Instance.KhoiTaoBaoCaoTon();
            dataGridViewBaoCaoTon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBaoCaoTon.AutoResizeColumns();
            DatThoiDiemHienTai(txtBoxNgaySuaChua);
            //Lấy quy định hiện hành
            dataGridViewQuyDinhHienHanh.DataSource = DataProvider.Instance.ExecuteQuery("Select * from THAMSO");
            dataGridViewQuyDinhHienHanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewQuyDinhHienHanh.AutoResizeColumns();
            GioiHanQuyenHan();
            DatMacDinhChoComboBox(comboBoxBienSoXe1);
            dateTimePickerChonThoiDiemBaoCaoTon.CustomFormat = "MM/yyyy";
            dateTimePickerChonThoiDiemBaoCaoTon.ShowUpDown = true;
        }


        private void BtnInPhieuSuaChua_Click(object sender, EventArgs e)
        {
            printDialogPSC.ShowDialog();
        }

        private void BtnDatLaiTraCuu_Click(object sender, EventArgs e)
        {
            textBoxTraCuuChinh.Text = "";
            txtBoxBienSoTraCuu.Text = "";
            comboBoxHieuXeTraCuu.Text = "";
        }

        private void RadioButtonTimTuongDoi_CheckedChanged(object sender, EventArgs e)
        {
            DatVisibleChoControl(flowLayoutPanelTimChinhXac, false);
            lblTraCuuChinh.Visible = true;
            textBoxTraCuuChinh.Visible = true;
        }

        private void RadioButtonTimChinhXac_CheckedChanged(object sender, EventArgs e)
        {
            DatVisibleChoControl(flowLayoutPanelTimChinhXac, true);
            lblTraCuuChinh.Visible = false;
            textBoxTraCuuChinh.Visible = false;
        }

        private void BtnLapBaoCaoDoanhSo_Click(object sender, EventArgs e)
        {
            DataTable BaoCaoDS;
            string query = "BaoCaoDoanhThu @Thang , @Nam";
            BaoCaoDS = DataProvider.Instance.ExecuteQuery(query, new object[] { int.Parse(textBoxThangBaoCao.Text), int.Parse(textBoxNamBaoCao.Text) });
            dataGridViewBaoCaoDoanhSo.DataSource = BaoCaoDS;
            dataGridViewBaoCaoDoanhSo.Show();
            query = "TongTienDoanhThu @Thang , @Nam";
            BaoCaoDS = DataProvider.Instance.ExecuteQuery(query, new object[] { int.Parse(textBoxThangBaoCao.Text), int.Parse(textBoxNamBaoCao.Text) });
            textBoxTongDoanhThu.Text = BaoCaoDS.Rows[0][0].ToString();
        }

        private void BtnLapBaoCaoTon_Click(object sender, EventArgs e)
        {
           // if (BaoCaoTonDAO.Instance.KiemTraThoiDiem(dateTimePickerChonThoiDiemBaoCaoTon.Value))
            //{
                lblThangBaoCaoTon.Text = "Tháng " + dateTimePickerChonThoiDiemBaoCaoTon.Value.ToString("MM/yyyy");
                dataGridViewBaoCaoTon.DataSource=BaoCaoTonDAO.Instance.TaoBaoCaoTon(dateTimePickerChonThoiDiemBaoCaoTon.Value);
            //}
        }

        private void BtnBaoCaoTonMoi_Click(object sender, EventArgs e)
        {
            DatLaiDateTimePicker(dateTimePickerChonThoiDiemBaoCaoTon);
            lblThangBaoCaoTon.Text = "Tháng";
            BaoCaoTonDAO.Instance.TaoBaoCaoMoi((DataTable)dataGridViewBaoCaoTon.DataSource);
            dateTimePickerChonThoiDiemBaoCaoTon.CustomFormat = "MM/yyyy";
            dateTimePickerChonThoiDiemBaoCaoTon.ShowUpDown = true;
        }

        private void BtnBaoCaoDoanhSoMoi_Click(object sender, EventArgs e)
        {
            textBoxThangBaoCao.Clear();
            textBoxNamBaoCao.Clear();
            textBoxTongDoanhThu.Clear();
        }

        private void HướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("D:/Git/Garage/README.md") ;
        }

        private void LiênHệToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/AProperName/QuanLyGarage");
        }

        private void ButtonPhieuThuTienMoiPTT_Click(object sender, EventArgs e)
        {
            textBoxDienThoaiPTT.Clear();
            textBoxDiaChiPTT.Clear();
            textBoxHoTenChuXePTT.Clear();
            textBoxSoTienThuPTT.Clear();
            DateTime now = DateTime.Now;
            textBoxNgayThuTien.Text = now.ToString("dd-MM-yyyy");
        }

        private void ButtonInPhieuThuTienPTT_Click(object sender, EventArgs e)
        {
            printDialogPTT.ShowDialog();
        }

        private void textBoxSoLuongPhieuNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ButtonInPhieuNhapVTPT_Click(object sender, EventArgs e)
        {
            printDialogPhieuNhapVTPT.ShowDialog();
        }

        private void ButtonPhieuNhapVTPTMoi_Click(object sender, EventArgs e)
        {
            textBoxTenVTPTMoi.Clear();
            textBoxSoLuongVTPT.Clear();
            textBoxGiaVTPT.Clear();
            textBoxTenVTPTMoi.Enabled = true;
            textBoxGiaVTPT.Enabled = true;
            textBoxTenVTPTMoi_TextChanged = false;
            textBoxGiaVTPT_TextChanged = false;
            buttonLapPhieuNhapVTPT.Visible = true;
            buttonTaoMoiVTPT.Visible = true;
        }


        //private void TPQuyDinh_Enter(object sender, EventArgs e)
        //{
        //    this.dataGridViewGiaTriHienTai.DataSource = DataProvider.Instance.ExecuteQuery("select * from THAMSO");
        //}

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaiKhoanDAO.Instance.XoaThongTinNguoiDungGanNhat();
        }

        private void buttonThemXe_Click(object sender, EventArgs e)
        {
            if (txtBoxTenKH.Text.Length == 0)
                MessageBox.Show("Vui lòng nhập tên khách hàng!");
            else
            {
                if (txtBoxDienThoai.Text.Length == 0)
                    MessageBox.Show("Vui lòng nhập điện thoại của khách hàng!");
                else
                {
                    if (txtBoxDiaChi.Text.Length == 0)
                        MessageBox.Show("Vui lòng nhập địa chỉ khách hàng!");
                    else
                    {
                        if (txtBoxBienSo.Text.Length == 0)
                            MessageBox.Show("Vui lòng nhập biển số xe !");
                    }
                }
            }
            string query1 = "ThemKhachHang @TenKH , @DienThoai , @DiaChi , @TienNo";
            int test = 0;
            DataTable tMaKH;
            int iMaKH;
            DateTime now = DateTime.Now;
            test = DataProvider.Instance.ExecuteNonQuery(query1, new object[] { txtBoxTenKH.Text, txtBoxDienThoai.Text, txtBoxDiaChi.Text, 0 });
            string query2 = "SELECT MaKH FROM KHACHHANG WHERE TenKH = '" + txtBoxTenKH.Text + "' and DienThoai = '" + txtBoxDienThoai.Text + "'";
            tMaKH = DataProvider.Instance.ExecuteQuery(query2);
            iMaKH = int.Parse(tMaKH.Rows[0][0].ToString());
            query2 = "ThemXe @BienSo , @HieuXe , @MaKH , @NgayTiepNhan";
            test = DataProvider.Instance.ExecuteNonQuery(query2, new object[] { txtBoxBienSo.Text, comBoxHieuXe.SelectedValue, iMaKH, now });
            if (test != 0)
            {
                MessageBox.Show("Thêm xe thành công!");
                progressBarSoXeDaThem.Value = progressBarSoXeDaThem.Value + 1;
                this.xETableAdapter.Fill(this.quanLyGarageDataSet.XE);
            }
            if (progressBarSoXeDaThem.Value == progressBarSoXeDaThem.Maximum)
            {
                txtBoxTenKH.Clear();
                txtBoxDienThoai.Clear();
                txtBoxDiaChi.Clear();
                txtBoxBienSo.Clear();
                txtBoxTenKH.Visible = false;
                txtBoxDienThoai.Visible = false;
                txtBoxDiaChi.Visible = false;
                txtBoxBienSo.Visible = false;
                buttonThemXe.Enabled = false;
                buttonClear.Enabled = false;
            }
            else
            {
                txtBoxTenKH.Clear();
                txtBoxDienThoai.Clear();
                txtBoxDiaChi.Clear();
                txtBoxBienSo.Clear();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtBoxTenKH.Clear();
            txtBoxDienThoai.Clear();
            txtBoxDiaChi.Clear();
            txtBoxBienSo.Clear();
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            string query = "SELECT TenHieuXe, BienSo, TenKH, DienThoai, DiaChi FROM XE, HIEUXE as HX, KHACHHANG as KH WHERE Xe.MaKH = KH.MaKH and Xe.MaHX = HX.MaHX and XE.TrangThai = 1";
            dataGridViewXeDaTiepNhan.DataSource = DataProvider.Instance.ExecuteQuery(query);
            dataGridViewXeDaTiepNhan.Show();
        }

        private void buttonLapPhieuThuTienPTT_Click(object sender, EventArgs e)
        {
            if (textBoxSoTienThuPTT.Text != "")
            {
                int iTienNo = 0;
                DataTable TienNo;
                string query1 = "SELECT TienNo from KHACHHANG as KH, XE WHERE XE.BienSo = '" + comboBienSoXe2.Text + "' and XE.MaKH = KH.MaKH";
                TienNo = DataProvider.Instance.ExecuteQuery(query1);
                iTienNo = int.Parse(TienNo.Rows[0][0].ToString());
                if (iTienNo < int.Parse(textBoxSoTienThuPTT.Text))
                    MessageBox.Show("Vui lòng nhập lại tiền thu!");
                else
                {
                    string query = " ThemPhieuThuTien @BienSo , @TienThu , @NgayThuTien";
                    int test = 0;
                    DateTime now = DateTime.Now;
                    test = DataProvider.Instance.ExecuteNonQuery(query, new object[] { comboBienSoXe2.Text, int.Parse(textBoxSoTienThuPTT.Text), now });
                    if (test != 0)
                        MessageBox.Show("Thêm Phiếu Thu Tiền thành công!");

                }
            }
            else
                MessageBox.Show("Vui lòng nhập số tiền thu !");
        }

        private void comboBienSoXe2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string query = "SELECT TenKH, DienThoai, DiaChi FROM KHACHHANG as KH, XE WHERE KH.MaKH = XE.MaKH and XE.BienSo = '" + comboBienSoXe2.SelectedValue + "'";
            DataTable TTKhachHangLPTT;
            TTKhachHangLPTT = DataProvider.Instance.ExecuteQuery(query);
            textBoxHoTenChuXePTT.Text = TTKhachHangLPTT.Rows[0][0].ToString();
            textBoxDienThoaiPTT.Text = TTKhachHangLPTT.Rows[0][1].ToString();
            textBoxDiaChiPTT.Text = TTKhachHangLPTT.Rows[0][2].ToString();
        }

        private void buttonLapPhieuNhapVTPT_Click(object sender, EventArgs e) //chỗ này chỉnh lại database này
        {
            if (textBoxSoLuongVTPT.Text == "")
                MessageBox.Show("Vui lòng nhập số lượng vật tư trước khi thêm mới phiếu nhập !");
            else
            {
                DateTime now = DateTime.Now;
                string query = "NhapVTPT @MaPhuTung , @SoLuong , @ThoiDiem";
                int test = 0;
                test = DataProvider.Instance.ExecuteNonQuery(query, new object[] { comboBoxTenVTPT.SelectedValue, int.Parse(textBoxSoLuongVTPT.Text), now });
                if (test > 0)
                    MessageBox.Show("Nhập vật thêm tư phụ tùng thành công!");
            }
        }

        private void buttonTaoMoiVTPT_Click(object sender, EventArgs e) //chỗ này thiếu thêm thời gian, để tối về anh kiểm tra thử database có thêm chưa, nếu chưa thêm thì anh gởi lệnh SQL qua và sửa vào trong này
        {
            DateTime now = DateTime.Now;
            if (textBoxSoLuongVTPT.Text == "")
                MessageBox.Show("Vui lòng nhập số lượng vật tư trước khi thêm mới vật tư vào kho !");
            else
            {
                string query = "NhapMoiVTPT @TenPhuTung , @SoLuong , @DonGia , @ThoiDiem";
                int test = 0;
                test = DataProvider.Instance.ExecuteNonQuery(query, new object[] { textBoxTenVTPTMoi.Text, int.Parse(textBoxSoLuongVTPT.Text), int.Parse(textBoxGiaVTPT.Text), now });
                if (test > 0)
                {
                    MessageBox.Show("Nhập mới vật tư phụ tùng thành công");
                    this.kHOTableAdapter.Fill(this.quanLyGarageDataSet.KHO);
                }
            }
        }

        private void btnTimKiemTraCuu_Click(object sender, EventArgs e)
        {
            if (radioButtonTimTuongDoi.Checked == true)
            {
                if (textBoxTraCuuChinh.Text == "")
                    MessageBox.Show("Nhập từ khóa tìm kiếm !");
                else
                {
                    string query = "TimKiemTuongDoi @DuLieu";
                    DataTable Find;
                    Find = DataProvider.Instance.ExecuteQuery(query, new object[] { textBoxTraCuuChinh.Text });
                    dataGridViewTraCuu.DataSource = Find;
                    dataGridViewTraCuu.Show();
                    //dataGridViewTraCuu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //dataGridViewTraCuu.AutoResizeColumns();
                }
            }
            else
            {
                if (txtBoxBienSoTraCuu.Text == "")
                    MessageBox.Show("Nhập từ khóa tìm kiếm !");
                else
                {
                    string query = "TimKiemChinhXac @BienSo , @HieuXe";
                    DataTable Find;
                    Find = DataProvider.Instance.ExecuteQuery(query, new object[] { txtBoxBienSoTraCuu.Text, comboBoxHieuXeTraCuu.Text });
                    dataGridViewTraCuu.DataSource = Find;
                    dataGridViewTraCuu.Show();
                    //dataGridViewTraCuu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //dataGridViewTraCuu.AutoResizeColumns();
                }
            }
        }

        private void btnCapNhatSoHieuXe_Click(object sender, EventArgs e)
        {
            string query = "UPDATE THAMSO SET GiaTri =" + int.Parse(txtBoxSoHieuXe.Text) + " WHERE MaThamSo = 'TS1'";
            int test = 0;
            test = DataProvider.Instance.ExecuteNonQuery(query);
            if (test != 0)
            {
                MessageBox.Show("Thay đổi số hiệu xe thành công !");
                txtBoxSoHieuXe.Clear();
            }
        }

        private void btnCapNhatSoXeSuaToiDa_Click(object sender, EventArgs e)
        {
            string query = "UPDATE THAMSO SET GiaTri =" + int.Parse(txtBoxSoXeSuaChuaToiDa.Text) + " WHERE MaThamSo = 'TS2'";
            int test = 0;
            test = DataProvider.Instance.ExecuteNonQuery(query);
            if (test != 0)
            {
                MessageBox.Show("Thay đổi số xe sữa chữa tối đa mỗi ngày thành công !");
                txtBoxSoXeSuaChuaToiDa.Clear();
            }
        }

        private void btnCapNhatSoLoaiVatTu_Click(object sender, EventArgs e)
        {
            string query = "UPDATE THAMSO SET GiaTri =" + int.Parse(txtBoxSoLoaiVatTu.Text) + " WHERE MaThamSo = 'TS3'";
            int test = 0;
            test = DataProvider.Instance.ExecuteNonQuery(query);
            if (test != 0)
            {
                MessageBox.Show("Thay đổi số loại vật tư thành công !");
                txtBoxSoLoaiVatTu.Clear();
            }
        }

        private void btnCapNhatSoLoaiTienCong_Click(object sender, EventArgs e)
        {
            string query = "UPDATE THAMSO SET GiaTri =" + int.Parse(txtBoxSoLoaiTienCong.Text) + " WHERE MaThamSo = 'TS4'";
            int test = 0;
            test = DataProvider.Instance.ExecuteNonQuery(query);
            if (test != 0)
            {
                MessageBox.Show("Thay đổi số loại tiền công thành công !");
                txtBoxSoLoaiTienCong.Clear();
            }
        }

        private void TextBoxSoLuongVTPTPhieuSuaChua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ButtonNhapVTPTPhieuSuaChua_Click(object sender, EventArgs e)
        {
            if (PhieuSuaChuaDAO.Instance.KiemTraSoLuong(comboBoxVTPTPhieuSuaChua.SelectedValue.ToString(), int.Parse(textBoxSoLuongVTPTPhieuSuaChua.Text)))
            {
                NhapVTPTChoPhieuSuaChua(PhieuSuaChuaDAO.Instance.LayDonGiaVTPT(comboBoxVTPTPhieuSuaChua.SelectedValue.ToString()));
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại số lượng vật tư phụ tùng!", "Kho không đủ đáp ứng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonNhapTienCongPhieuSuaChua_Click(object sender, EventArgs e)
        {
            NhapTienCongChoPhieuSuaChua(PhieuSuaChuaDAO.Instance.LayChiPhiTienCong(comboBoxTienCongPhieuSuaChua.SelectedValue.ToString()));
        }

        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            int TongTien;
            TongTien = TinhTongThanhTien() + TinhTongChiPhi();
            textBoxTongTienPhieuSuaChua.Text = TongTien.ToString();
            btnHoanTatClicked = true;
        }

        private void BtnTaoMoiPCS_Click(object sender, EventArgs e)
        {
            comboBoxBienSoXe1.Text = "";
            comboBoxVTPTPhieuSuaChua.Text = "";
            comboBoxTienCongPhieuSuaChua.Text = "";
            textBoxSoLuongVTPTPhieuSuaChua.Text = "";
            textBoxTongTienPhieuSuaChua.Text = "";
            KhoiTaoDataGridviewVTPT();
            KhoiTaoDataGridviewTienCong();
            PhieuSuaChuaDAO.Instance.XoaDtVTPTDangNhap();
            btnHoanTatClicked = false;
            DatMacDinhChoComboBox(comboBoxBienSoXe1);
        }

        private void BtnLuuPSC_Click(object sender, EventArgs e)
        {
            if (btnHoanTatClicked)
            {
                PhieuSuaChuaDAO.Instance.LuuPhieuSuaChua(comboBoxBienSoXe1.Text, TinhTongChiPhi(), TinhTongThanhTien(), TinhTongThanhTien() + TinhTongChiPhi(), (DataTable)dataGridViewTienCongPhieuSuaChua.DataSource, (DataTable)dataGridViewVTPTPhieuSuaChua.DataSource);
                PhieuSuaChuaDAO.Instance.CapNhatTienNo(comboBoxBienSoXe1.Text, int.Parse(textBoxTongTienPhieuSuaChua.Text));
                MessageBox.Show("Đã lưu phiếu sửa chữa!", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PhieuSuaChuaDAO.Instance.NhapVTPT((DataTable)dataGridViewVTPTPhieuSuaChua.DataSource);
                this.kHOTableAdapter.Fill(this.quanLyGarageDataSet.KHO);// update lai KHO cho phieusuachua lan sau 
            }
            else
            {
                MessageBox.Show("Xin vui lòng nhấn Hoàn tất trước khi lưu phiếu sửa chữa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ComboBoxVTPTPhieuSuaChua_SelectionChangeCommitted(object sender, EventArgs e)
        {
            textBoxSoLuongVTPTPhieuSuaChua.Text = "";
        }


        private void TextBoxTenVTPTMoi_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGiaVTPT_TextChanged)
                buttonLapPhieuNhapVTPT.Visible = false;
            textBoxTenVTPTMoi_TextChanged = true;
        }

        private void TextBoxGiaVTPT_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTenVTPTMoi_TextChanged)
                buttonLapPhieuNhapVTPT.Visible = false;
            textBoxGiaVTPT_TextChanged = true;
        }


        private void ComboBoxTenVTPT_SelectionChangeCommitted(object sender, EventArgs e)
        {
            textBoxTenVTPTMoi.Enabled = false;
            textBoxGiaVTPT.Enabled = false;
            buttonTaoMoiVTPT.Visible = false;
        }


        private void ButtonLamMoiQuyDinh_Click(object sender, EventArgs e)
        {
            dataGridViewQuyDinhHienHanh.DataSource = DataProvider.Instance.ExecuteQuery("Select * from THAMSO");
            dataGridViewQuyDinhHienHanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewQuyDinhHienHanh.AutoResizeColumns();
        }

        private void TextBoxThangBaoCao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxNamBaoCao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoxSoHieuXe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoxSoXeSuaChuaToiDa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoxSoLoaiVatTu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoxSoLoaiTienCong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

    }
}
