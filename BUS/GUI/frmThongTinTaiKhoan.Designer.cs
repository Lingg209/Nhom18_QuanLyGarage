namespace GUI
{
    partial class frmThongTinTaiKhoan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongTinTaiKhoan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.txtBoxQuyenHan = new System.Windows.Forms.TextBox();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.txtBoxTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblQuyenHan = new System.Windows.Forms.Label();
            this.txtBoxHoTen = new System.Windows.Forms.TextBox();
            this.lblHoten = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.btnDoiMatKhau);
            this.panel1.Controls.Add(this.txtBoxQuyenHan);
            this.panel1.Controls.Add(this.lblTaiKhoan);
            this.panel1.Controls.Add(this.txtBoxTaiKhoan);
            this.panel1.Controls.Add(this.lblQuyenHan);
            this.panel1.Controls.Add(this.txtBoxHoTen);
            this.panel1.Controls.Add(this.lblHoten);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 194);
            this.panel1.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Transparent;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnThoat.Location = new System.Drawing.Point(444, 130);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(216, 49);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.BtnThoat_Click);
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.btnDoiMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDoiMatKhau.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(444, 69);
            this.btnDoiMatKhau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(216, 50);
            this.btnDoiMatKhau.TabIndex = 9;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = false;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.BtnDoiMatKhau_Click);
            // 
            // txtBoxQuyenHan
            // 
            this.txtBoxQuyenHan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxQuyenHan.Location = new System.Drawing.Point(101, 139);
            this.txtBoxQuyenHan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxQuyenHan.Name = "txtBoxQuyenHan";
            this.txtBoxQuyenHan.ReadOnly = true;
            this.txtBoxQuyenHan.Size = new System.Drawing.Size(272, 30);
            this.txtBoxQuyenHan.TabIndex = 5;
            this.txtBoxQuyenHan.TextChanged += new System.EventHandler(this.TxtBoxTaiKhoan_TextChanged);
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaiKhoan.Location = new System.Drawing.Point(3, 81);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(94, 22);
            this.lblTaiKhoan.TabIndex = 4;
            this.lblTaiKhoan.Text = "Tài khoản:";
            this.lblTaiKhoan.Click += new System.EventHandler(this.LblTaiKhoan_Click);
            // 
            // txtBoxTaiKhoan
            // 
            this.txtBoxTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxTaiKhoan.Location = new System.Drawing.Point(101, 78);
            this.txtBoxTaiKhoan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxTaiKhoan.Name = "txtBoxTaiKhoan";
            this.txtBoxTaiKhoan.ReadOnly = true;
            this.txtBoxTaiKhoan.Size = new System.Drawing.Size(272, 30);
            this.txtBoxTaiKhoan.TabIndex = 3;
            // 
            // lblQuyenHan
            // 
            this.lblQuyenHan.AutoSize = true;
            this.lblQuyenHan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuyenHan.Location = new System.Drawing.Point(3, 143);
            this.lblQuyenHan.Name = "lblQuyenHan";
            this.lblQuyenHan.Size = new System.Drawing.Size(103, 22);
            this.lblQuyenHan.TabIndex = 2;
            this.lblQuyenHan.Text = "Quyền hạn: ";
            // 
            // txtBoxHoTen
            // 
            this.txtBoxHoTen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxHoTen.Location = new System.Drawing.Point(101, 18);
            this.txtBoxHoTen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxHoTen.Name = "txtBoxHoTen";
            this.txtBoxHoTen.ReadOnly = true;
            this.txtBoxHoTen.Size = new System.Drawing.Size(415, 30);
            this.txtBoxHoTen.TabIndex = 1;
            this.txtBoxHoTen.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // lblHoten
            // 
            this.lblHoten.AutoSize = true;
            this.lblHoten.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoten.Location = new System.Drawing.Point(4, 22);
            this.lblHoten.Name = "lblHoten";
            this.lblHoten.Size = new System.Drawing.Size(92, 22);
            this.lblHoten.TabIndex = 0;
            this.lblHoten.Text = "Họ và tên:";
            // 
            // frmThongTinTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(687, 214);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmThongTinTaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin tài khoản";
            this.Load += new System.EventHandler(this.FrmThongTinTaiKhoan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBoxHoTen;
        private System.Windows.Forms.Label lblHoten;
        private System.Windows.Forms.TextBox txtBoxQuyenHan;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.TextBox txtBoxTaiKhoan;
        private System.Windows.Forms.Label lblQuyenHan;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnDoiMatKhau;
    }
}