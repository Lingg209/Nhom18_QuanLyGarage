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
    public partial class frmMain : Form
    {
        DateTime now = DateTime.Now;
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
            dataGridViewVTPTPhieuSuaChua.DataSource = PhieuSuaChuaBUS.Instance.KhoiTaoDtVTPT();
            dataGridViewVTPTPhieuSuaChua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewVTPTPhieuSuaChua.AutoResizeColumns();

            //dataGridViewVTPTPhieuSuaChua.Columns["Mã phụ tùng"].Visible = false;
        }

        void KhoiTaoDataGridviewTienCong()
        {
            dataGridViewTienCongPhieuSuaChua.DataSource = PhieuSuaChuaBUS.Instance.KhoiTaoDtTienCong();
            dataGridViewTienCongPhieuSuaChua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTienCongPhieuSuaChua.AutoResizeColumns();

            //dataGridViewVTPTPhieuSuaChua.Columns["Mã tiền công"].Visible = false;
        }

        void NhapVTPTChoPhieuSuaChua(int DonGia)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataGridViewVTPTPhieuSuaChua.DataSource;
            PhieuSuaChuaBUS.Instance.ThemHangVTPT(dt, comboBoxVTPTPhieuSuaChua.Text, textBoxSoLuongVTPTPhieuSuaChua.Text, DonGia, comboBoxVTPTPhieuSuaChua.SelectedValue.ToString());
        }

        void NhapTienCongChoPhieuSuaChua(int ChiPhi)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dataGridViewTienCongPhieuSuaChua.DataSource;
            PhieuSuaChuaBUS.Instance.ThemHangTienCong(dt, PhieuSuaChuaBUS.Instance.LayNoiDungTienCong(comboBoxTienCongPhieuSuaChua.SelectedValue.ToString()), ChiPhi, comboBoxTienCongPhieuSuaChua.SelectedValue.ToString());
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
            if (!BaoCaoTonBUS.Instance.KiemTraDuLieuBaoCao(a))
            {
                DataTable dt = new DataTable();
                BaoCaoTonBUS.Instance.NhapBaoCaoTon(BaoCaoTonBUS.Instance.TaoBaoCaoTon(a), a);
            }
        }

        bool QuyenHanAdmin()//Kiểm tra tài khoản hiện tại có phải là admin không
        {
            if (TaiKhoanBUS.Instance.LayQuyenHan().ToUpper() == "ADMIN")
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
            // TODO: This line of code loads data into the 'quanLyGarageDataSet1.PHIEUTHUTIEN' table. You can move, or remove it, as needed.
            this.pHIEUTHUTIENTableAdapter.Fill(this.quanLyGarageDataSet1.PHIEUTHUTIEN);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.PHIEUSUACHUA' table. You can move, or remove it, as needed.
            this.pHIEUSUACHUATableAdapter.Fill(this.quanLyGarageDataSet.PHIEUSUACHUA);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.PHIEUNHAPVTPT' table. You can move, or remove it, as needed.
            this.pHIEUNHAPVTPTTableAdapter.Fill(this.quanLyGarageDataSet.PHIEUNHAPVTPT);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.TIENCONG' table. You can move, or remove it, as needed.
            this.tIENCONGTableAdapter.Fill(this.quanLyGarageDataSet.TIENCONG);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.KHO' table. You can move, or remove it, as needed.
            this.kHOTableAdapter.Fill(this.quanLyGarageDataSet.KHO);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.XE' table. You can move, or remove it, as needed.
            this.xETableAdapter.Fill(this.quanLyGarageDataSet.XE);
            // TODO: This line of code loads data into the 'quanLyGarageDataSet.HIEUXE' table. You can move, or remove it, as needed.
            this.hIEUXETableAdapter.Fill(this.quanLyGarageDataSet.HIEUXE);
            // Lấy dữ liệu các xe đã tiếp nhận
            dataGridViewXeDaTiepNhan.DataSource = XeBUS.Instance.CacXeDaTiepNhan(now);
            dataGridViewXeDaTiepNhan.Show();
            // Lấy thông tin cho progressbar số xe đã tiếp nhận 1 ngày
            textBoxNgayThuTien.Text = now.ToString("dd-MM-yyyy");
            progressBarSoXeDaThem.Maximum = QuyDinhBUS.Instance.LaySoXeSuaToiDa();
            progressBarSoXeDaThem.Value = XeBUS.Instance.SoXeTiepNhanTrongNgay(now);

            // Điển ngày thu tiền
            textBoxNgayThuTien.Text = now.ToString("dd-MM-yyyy");
            //Khởi tạo Datagridview Phiếu sửa chữa và tiền công
            KhoiTaoDataGridviewTienCong();
            KhoiTaoDataGridviewVTPT();
            //Khởi tạo 1 dt để lưu lại các mã vtpt đã nhập và kiểm tra, so sánh số lượng nhập vào.
            PhieuSuaChuaBUS.Instance.KhoiTaoDtVTPTDangNhap();
            KiemTraDuLieuBaoCaoKhiLoad(DateTime.Now);
            dataGridViewBaoCaoTon.DataSource = BaoCaoTonBUS.Instance.KhoiTaoBaoCaoTon();
            dataGridViewBaoCaoTon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBaoCaoTon.AutoResizeColumns();
            DatThoiDiemHienTai(txtBoxNgaySuaChua);
            //Lấy quy định hiện hành
            dataGridViewQuyDinhHienHanh.DataSource = QuyDinhBUS.Instance.LayTatCaQuyDinh();
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
            dataGridViewBaoCaoDoanhSo.DataSource = BaoCaoDoanhThuBUS.Instance.BaoCaoDoanhThu(textBoxThangBaoCao.Text, textBoxNamBaoCao.Text);
            dataGridViewBaoCaoDoanhSo.Show();
            textBoxTongDoanhThu.Text = BaoCaoDoanhThuBUS.Instance.TongTienDoanhThu(textBoxThangBaoCao.Text, textBoxNamBaoCao.Text);
        }

        private void BtnLapBaoCaoTon_Click(object sender, EventArgs e)
        {
           // if (BaoCaoTonDAO.Instance.KiemTraThoiDiem(dateTimePickerChonThoiDiemBaoCaoTon.Value))
            //{
                lblThangBaoCaoTon.Text = "Tháng " + dateTimePickerChonThoiDiemBaoCaoTon.Value.ToString("MM/yyyy");
                dataGridViewBaoCaoTon.DataSource=BaoCaoTonBUS.Instance.TaoBaoCaoTon(dateTimePickerChonThoiDiemBaoCaoTon.Value);
            //}
        }

        private void BtnBaoCaoTonMoi_Click(object sender, EventArgs e)
        {
            DatLaiDateTimePicker(dateTimePickerChonThoiDiemBaoCaoTon);
            lblThangBaoCaoTon.Text = "Tháng";
            BaoCaoTonBUS.Instance.TaoBaoCaoMoi((DataTable)dataGridViewBaoCaoTon.DataSource);
            dateTimePickerChonThoiDiemBaoCaoTon.CustomFormat = "MM/yyyy";
            dateTimePickerChonThoiDiemBaoCaoTon.ShowUpDown = true;
        }

        private void BtnBaoCaoDoanhSoMoi_Click(object sender, EventArgs e)
        {
            textBoxThangBaoCao.Clear();
            textBoxNamBaoCao.Clear();
            textBoxTongDoanhThu.Clear();
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

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaiKhoanBUS.Instance.XoaThongTinNguoiDungGanNhat();
        }

        private void buttonThemXe_Click(object sender, EventArgs e)
        {
            if (txtBoxTenKH.Text.Length == 0)
                MessageBox.Show("Vui lòng nhập lại tên khách hàng!");
            else
            {
                if (txtBoxDienThoai.Text.Length < 10)
                    MessageBox.Show("Vui lòng nhập lại số điện thoại của khách hàng!");
                else
                {
                    if (txtBoxDiaChi.Text.Length == 0)
                        MessageBox.Show("Vui lòng nhập lại địa chỉ khách hàng!");
                    else
                    {
                        if (txtBoxBienSo.Text.Length == 0)
                            MessageBox.Show("Vui lòng nhập lại biển số xe !");                     
                        else
                        {
                            int test = 0;
                            test = KhachHangBUS.Instance.ThemKhachHang(txtBoxTenKH.Text, txtBoxDienThoai.Text, txtBoxDiaChi.Text);      //thuc hien them khach hang moi
                            test = XeBUS.Instance.ThemXe(txtBoxBienSo.Text, comBoxHieuXe.SelectedValue.ToString(), KhachHangBUS.Instance.LayMaKH(txtBoxTenKH.Text, txtBoxDienThoai.Text), now);
                            if (test == 0)
                            {
                                MessageBox.Show("Thêm xe  k thành công!");
                            }
                            else {
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
                    }
                }
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
            dataGridViewXeDaTiepNhan.DataSource = XeBUS.Instance.LamMoiDanhSachXe(now);
            dataGridViewXeDaTiepNhan.Show();
        }

        private void buttonLapPhieuThuTienPTT_Click(object sender, EventArgs e)
        {
            if(textBoxHoTenChuXePTT.Text == null || textBoxHoTenChuXePTT.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn biển số!");
            }
            else
            if (textBoxSoTienThuPTT.Text != "" & textBoxSoTienThuPTT.Text != "0")
            {
                if (PhieuThuTienBUS.Instance.LayTienNoKH(comboBienSoXe2.Text) < int.Parse(textBoxSoTienThuPTT.Text))
                    MessageBox.Show("Vui lòng nhập lại số tiền! Số tiền đã nhập lớn hơn số tiền đang nợ. (Để xem số tiền nợ vui lòng sử dụng trang tra cứu bên phải.)");
                else
                {
                    int test = 0;
                    test = PhieuThuTienBUS.Instance.ThemPhieuThuTien(comboBienSoXe2.Text, textBoxSoTienThuPTT.Text, now);
                    if (test != 0)
                        MessageBox.Show("Thêm Phiếu Thu Tiền thành công!");

                }
            }
            else
                MessageBox.Show("Vui lòng nhập số tiền thu!");
        }

        private void comboBienSoXe2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string[] info = PhieuThuTienBUS.Instance.LayThongTinKH(comboBienSoXe2.Text);        //du lieu tra ve: { ten, dien thoai, dia chi}
            textBoxHoTenChuXePTT.Text = info[0];
            textBoxDienThoaiPTT.Text = info[1];
            textBoxDiaChiPTT.Text = info[2];
        }

        private void buttonLapPhieuNhapVTPT_Click(object sender, EventArgs e) //chỗ này chỉnh lại database này
        {
            if (textBoxSoLuongVTPT.Text == "0" || textBoxSoLuongVTPT.Text.Length == 0)
                MessageBox.Show("Vui lòng nhập số lượng vật tư trước khi thêm mới phiếu nhập !");
            else
            {
                int test = 0;
                test = PhieuNhapVTPTBUS.Instance.NhapVTPT(comboBoxTenVTPT.SelectedValue.ToString(), textBoxSoLuongVTPT.Text, now);
                if (test > 0)
                    MessageBox.Show("Nhập vật tư phụ tùng thành công!");
            }
        }

        private void buttonTaoMoiVTPT_Click(object sender, EventArgs e) //chỗ này thiếu thêm thời gian, để tối về anh kiểm tra thử database có thêm chưa, nếu chưa thêm thì anh gởi lệnh SQL qua và sửa vào trong này
        {
            DateTime now = DateTime.Now;
            if (textBoxTenVTPTMoi.Text == "0" || textBoxTenVTPTMoi.Text.Length == 0)
            { MessageBox.Show("Vui lòng nhập Tên vật tư trước khi thêm  vật tư vào kho !"); }
            else
            if (textBoxSoLuongVTPT.Text == "0" || textBoxSoLuongVTPT.Text.Length == 0)
            { MessageBox.Show("Vui lòng nhập số lượng vật tư trước khi thêm  vật tư vào kho !"); }
            else
            {
                if (textBoxGiaVTPT.Text == "0" || textBoxGiaVTPT.Text.Length == 0)
                { MessageBox.Show("Vui lòng nhập Giá vật tư trước khi thêm mới vật tư vào kho !"); }
                else
                {
                    int test = 0;
                    test = PhieuNhapVTPTBUS.Instance.NhapMoiVTPT(textBoxTenVTPTMoi.Text, textBoxSoLuongVTPT.Text, textBoxGiaVTPT.Text, now);
                    if (test > 0)
                    {
                        MessageBox.Show("Nhập mới vật tư phụ tùng thành công");
                        this.kHOTableAdapter.Fill(this.quanLyGarageDataSet.KHO);
                    }
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
                    dataGridViewTraCuu.DataSource = XeBUS.Instance.TimKiemTuongDoi(textBoxTraCuuChinh.Text);
                    dataGridViewTraCuu.Show();
                }
            }
            else
            {
                if (txtBoxBienSoTraCuu.Text == "")
                    MessageBox.Show("Nhập từ khóa tìm kiếm !");
                else
                {
                    dataGridViewTraCuu.DataSource = XeBUS.Instance.TimKiemChinhXac(txtBoxBienSoTraCuu.Text, comboBoxHieuXeTraCuu.Text);
                    dataGridViewTraCuu.Show();
                }
            }
        }

        private void btnCapNhatSoHieuXe_Click(object sender, EventArgs e)
        {
            if (txtBoxSoHieuXe.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số hiệu xe !");
            }else
            {
                int test = BUS.QuyDinhBUS.Instance.CapNhatSoHieuXe(txtBoxSoHieuXe.Text);
                if (test != 0)
                {
                    MessageBox.Show("Thay đổi số hiệu xe thành công !");
                    txtBoxSoHieuXe.Clear();
                }
            }                
        }

        private void btnCapNhatSoXeSuaToiDa_Click(object sender, EventArgs e)
        {
            if (txtBoxSoXeSuaChuaToiDa.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số xe sửa tối đa !");
            }
            else
            {
                int test = BUS.QuyDinhBUS.Instance.CapNhatSoXeSuaToiDa(txtBoxSoXeSuaChuaToiDa.Text);
                if (test != 0)
                {
                    MessageBox.Show("Thay đổi số xe sửa tối đa thành công !");
                    txtBoxSoXeSuaChuaToiDa.Clear();
                }
            }
        }

        private void btnCapNhatSoLoaiVatTu_Click(object sender, EventArgs e)
        {
            if (txtBoxSoLoaiVatTu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số loại vật tư !");
            }
            else
            {
                int test = BUS.QuyDinhBUS.Instance.CapNhatSoLoaiVatTu(txtBoxSoLoaiVatTu.Text);
                if (test != 0)
                {
                    MessageBox.Show("Thay đổi số loại vật tư thành công !");
                    txtBoxSoLoaiVatTu.Clear();
                }
            }           
        }

        private void btnCapNhatSoLoaiTienCong_Click(object sender, EventArgs e)
        {
            if (txtBoxSoLoaiTienCong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số loại vật tư !");
            }
            else
            {

                int test = BUS.QuyDinhBUS.Instance.CapNhatSoLoaiTienCong(txtBoxSoLoaiTienCong.Text);
                if (test != 0)
                {
                    MessageBox.Show("Thay đổi số loại tiền công thành công !");
                    txtBoxSoLoaiTienCong.Clear();
                }
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
            if (comboBoxBienSoXe1.Text == "--select--")
            {
                MessageBox.Show("Vui lòng chọn một biển số xe!");

            }else
            {
                if (comboBoxVTPTPhieuSuaChua.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng chọn vật tư phụ tùng!");
                }else
                if (textBoxSoLuongVTPTPhieuSuaChua.Text.Length == 0 || textBoxSoLuongVTPTPhieuSuaChua.Text == "0")
                {
                    MessageBox.Show("Vui lòng kiểm tra lại số lượng vật tư phụ tùng!");
                }
                else
                {
                    if (PhieuSuaChuaBUS.Instance.KiemTraSoLuong(comboBoxVTPTPhieuSuaChua.SelectedValue.ToString(), int.Parse(textBoxSoLuongVTPTPhieuSuaChua.Text)))
                    {
                        NhapVTPTChoPhieuSuaChua(PhieuSuaChuaBUS.Instance.LayDonGiaVTPT(comboBoxVTPTPhieuSuaChua.SelectedValue.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại số lượng vật tư phụ tùng!", "Kho không đủ đáp ứng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }            
        }

        private void ButtonNhapTienCongPhieuSuaChua_Click(object sender, EventArgs e)
        {
            if (comboBoxBienSoXe1.Text == "--select--")
            {
                MessageBox.Show("Vui lòng chọn một biển số xe!");

            }
            else
            {
                if (comboBoxTienCongPhieuSuaChua.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng chọn tiền công!");
                }
                else
                {
                    NhapTienCongChoPhieuSuaChua(PhieuSuaChuaBUS.Instance.LayChiPhiTienCong(comboBoxTienCongPhieuSuaChua.SelectedValue.ToString()));

                }
            }
        }

        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            
            int TongTien;
            TongTien = TinhTongThanhTien() + TinhTongChiPhi();
            if (TongTien == 0 )
            {
                MessageBox.Show("Phiếu sửa chữa trống ! Vui lòng thêm dịch vụ");

            }
            else
            {
                textBoxTongTienPhieuSuaChua.Text = TongTien.ToString();
                btnHoanTatClicked = true;
            }
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
            PhieuSuaChuaBUS.Instance.XoaDtVTPTDangNhap();
            btnHoanTatClicked = false;
            DatMacDinhChoComboBox(comboBoxBienSoXe1);
        }

        private void BtnLuuPSC_Click(object sender, EventArgs e)
        {
            if (btnHoanTatClicked)
            {
                PhieuSuaChuaBUS.Instance.LuuPhieuSuaChua(comboBoxBienSoXe1.Text, TinhTongChiPhi(), TinhTongThanhTien(), TinhTongThanhTien() + TinhTongChiPhi(), (DataTable)dataGridViewTienCongPhieuSuaChua.DataSource, (DataTable)dataGridViewVTPTPhieuSuaChua.DataSource);
                PhieuSuaChuaBUS.Instance.CapNhatTienNo(comboBoxBienSoXe1.Text, int.Parse(textBoxTongTienPhieuSuaChua.Text));
                MessageBox.Show("Đã lưu phiếu sửa chữa!", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PhieuSuaChuaBUS.Instance.NhapVTPT((DataTable)dataGridViewVTPTPhieuSuaChua.DataSource);
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
            dataGridViewQuyDinhHienHanh.DataSource = BUS.QuyDinhBUS.Instance.LayTatCaQuyDinh();
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

        private void tPPSC_Click(object sender, EventArgs e)
        {

        }

        private void tPChucNang_Click(object sender, EventArgs e)
        {

        }

        private void lblDanhSachTiepNhan_Click(object sender, EventArgs e)
        {

        }

        private void pnChonThoiDiem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewTraCuu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel31_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnPNVTPT_Click(object sender, EventArgs e)
        {

            dataGridViewPnVT.DataSource = BUS.PhieuNhapVTPTBUS.Instance.LayTatCaPN();
            dataGridViewPnVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPnVT.AutoResizeColumns();
            

        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            dataGridViewKho.DataSource = BUS.KhoBUS.Instance.LayKHO();
            dataGridViewKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewKho.AutoResizeColumns();

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridViewPSC.DataSource = BUS.PhieuSuaChuaBUS.Instance.LayPSC();
            dataGridViewPSC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPSC.AutoResizeColumns();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridViewPTT.DataSource = BUS.PhieuThuTienBUS.Instance.LayPTT();
            dataGridViewPTT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPTT.AutoResizeColumns();
        }

        private void txtBoxTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxSoHieuXe_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxSoXeSuaChuaToiDa_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSoLuongVTPTPhieuSuaChua_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBienSoXe1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {  // Xác thực rằng phím vừa nhấn không phải CTRL hoặc không phải dạng số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;

            }
        }

        private void textBoxSoTienThuPTT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBoxSoLuongVTPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBoxGiaVTPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
