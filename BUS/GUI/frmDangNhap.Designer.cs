namespace GUI
{
    partial class frmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.account = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.textDangNhap = new System.Windows.Forms.TextBox();
            this.textMatKhau = new System.Windows.Forms.TextBox();
            this.buttonDangNhap = new System.Windows.Forms.Button();
            this.buttonThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // account
            // 
            this.account.AutoSize = true;
            this.account.BackColor = System.Drawing.Color.Transparent;
            this.account.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.account.Location = new System.Drawing.Point(32, 31);
            this.account.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.account.Name = "account";
            this.account.Size = new System.Drawing.Size(144, 22);
            this.account.TabIndex = 0;
            this.account.Text = "Tên Đăng Nhập: ";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.BackColor = System.Drawing.Color.Transparent;
            this.password.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(32, 65);
            this.password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(98, 22);
            this.password.TabIndex = 1;
            this.password.Text = "Mật Khẩu: ";
            // 
            // textDangNhap
            // 
            this.textDangNhap.Location = new System.Drawing.Point(175, 32);
            this.textDangNhap.Margin = new System.Windows.Forms.Padding(4);
            this.textDangNhap.MaxLength = 30;
            this.textDangNhap.Name = "textDangNhap";
            this.textDangNhap.Size = new System.Drawing.Size(227, 22);
            this.textDangNhap.TabIndex = 2;
            // 
            // textMatKhau
            // 
            this.textMatKhau.Location = new System.Drawing.Point(175, 66);
            this.textMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.textMatKhau.MaxLength = 30;
            this.textMatKhau.Name = "textMatKhau";
            this.textMatKhau.Size = new System.Drawing.Size(227, 22);
            this.textMatKhau.TabIndex = 3;
            this.textMatKhau.UseSystemPasswordChar = true;
            // 
            // buttonDangNhap
            // 
            this.buttonDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.buttonDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDangNhap.Location = new System.Drawing.Point(175, 121);
            this.buttonDangNhap.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDangNhap.Name = "buttonDangNhap";
            this.buttonDangNhap.Size = new System.Drawing.Size(100, 28);
            this.buttonDangNhap.TabIndex = 4;
            this.buttonDangNhap.Text = "Đăng nhập";
            this.buttonDangNhap.UseVisualStyleBackColor = false;
            this.buttonDangNhap.Click += new System.EventHandler(this.buttonDangNhap_Click);
            // 
            // buttonThoat
            // 
            this.buttonThoat.BackColor = System.Drawing.Color.Transparent;
            this.buttonThoat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonThoat.Location = new System.Drawing.Point(302, 121);
            this.buttonThoat.Margin = new System.Windows.Forms.Padding(4);
            this.buttonThoat.Name = "buttonThoat";
            this.buttonThoat.Size = new System.Drawing.Size(100, 28);
            this.buttonThoat.TabIndex = 5;
            this.buttonThoat.Text = "Thoát";
            this.buttonThoat.UseVisualStyleBackColor = false;
            this.buttonThoat.Click += new System.EventHandler(this.buttonThoat_Click);
            // 
            // frmDangNhap
            // 
            this.AcceptButton = this.buttonDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(448, 177);
            this.Controls.Add(this.buttonThoat);
            this.Controls.Add(this.buttonDangNhap);
            this.Controls.Add(this.textMatKhau);
            this.Controls.Add(this.textDangNhap);
            this.Controls.Add(this.password);
            this.Controls.Add(this.account);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            this.Enter += new System.EventHandler(this.buttonDangNhap_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label account;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.TextBox textDangNhap;
        private System.Windows.Forms.TextBox textMatKhau;
        private System.Windows.Forms.Button buttonDangNhap;
        private System.Windows.Forms.Button buttonThoat;
    }
}