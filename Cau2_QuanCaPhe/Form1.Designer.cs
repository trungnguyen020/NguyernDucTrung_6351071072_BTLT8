namespace Cau2_QuanCaPhe;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpTimKiem = new GroupBox();
        lblTuNgay = new Label();
        dtpTuNgay = new DateTimePicker();
        lblDenNgay = new Label();
        dtpDenNgay = new DateTimePicker();
        lblLocSoBan = new Label();
        cboLocSoBan = new ComboBox();
        lblLocHinhThuc = new Label();
        cboLocHinhThuc = new ComboBox();
        lblLocTenMon = new Label();
        txtLocTenMon = new TextBox();
        btnTimKiem = new Button();

        grpDanhSach = new GroupBox();
        dgvHoaDon = new DataGridView();
        colMaHD = new DataGridViewTextBoxColumn();
        colMaHDFormatted = new DataGridViewTextBoxColumn();
        colNgayLap = new DataGridViewTextBoxColumn();
        colSoBan = new DataGridViewTextBoxColumn();
        colHinhThucTT = new DataGridViewTextBoxColumn();
        colTongTien = new DataGridViewTextBoxColumn();
        colGhiChu = new DataGridViewTextBoxColumn();

        grpChiTiet = new GroupBox();
        lblNgayLap = new Label();
        dtpNgayLap = new DateTimePicker();
        lblSoBan = new Label();
        cboSoBan = new ComboBox();
        radTienMat = new RadioButton();
        radChuyenKhoan = new RadioButton();
        lblGhiChu = new Label();
        txtGhiChu = new TextBox();
        lblMon = new Label();
        cboMon = new ComboBox();
        lblSoLuong = new Label();
        numSoLuong = new NumericUpDown();
        btnThemMon = new Button();
        dgvChiTiet = new DataGridView();
        colMaCT = new DataGridViewTextBoxColumn();
        colSTT = new DataGridViewTextBoxColumn();
        colTenMon = new DataGridViewTextBoxColumn();
        colSL = new DataGridViewTextBoxColumn();
        colDonGia = new DataGridViewTextBoxColumn();
        colThanhTien = new DataGridViewTextBoxColumn();
        colMaMonVal = new DataGridViewTextBoxColumn();

        lblTongDoanhThu = new Label();
        lblTongCong = new Label();

        btnHoaDonMoi = new Button();
        btnSuaHoaDon = new Button();
        btnXoaHoaDon = new Button();
        btnSuaSL = new Button();
        btnXoaMon = new Button();
        btnXemHoaDon = new Button();
        btnXuatPdf = new Button();
        btnIn = new Button();

        grpTimKiem.SuspendLayout();
        grpDanhSach.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
        grpChiTiet.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
        SuspendLayout();

        // 
        // grpTimKiem
        // 
        grpTimKiem.Controls.Add(lblTuNgay);
        grpTimKiem.Controls.Add(dtpTuNgay);
        grpTimKiem.Controls.Add(lblDenNgay);
        grpTimKiem.Controls.Add(dtpDenNgay);
        grpTimKiem.Controls.Add(lblLocSoBan);
        grpTimKiem.Controls.Add(cboLocSoBan);
        grpTimKiem.Controls.Add(lblLocHinhThuc);
        grpTimKiem.Controls.Add(cboLocHinhThuc);
        grpTimKiem.Controls.Add(lblLocTenMon);
        grpTimKiem.Controls.Add(txtLocTenMon);
        grpTimKiem.Controls.Add(btnTimKiem);
        grpTimKiem.Location = new Point(15, 8);
        grpTimKiem.Name = "grpTimKiem";
        grpTimKiem.Size = new Size(1060, 65);
        grpTimKiem.TabIndex = 0;
        grpTimKiem.TabStop = false;
        grpTimKiem.Text = "Tìm kiếm hóa đơn";

        // lblTuNgay
        lblTuNgay.Location = new Point(10, 26);
        lblTuNgay.Text = "Từ ngày";
        lblTuNgay.Size = new Size(55, 20);
        dtpTuNgay.Format = DateTimePickerFormat.Short;
        dtpTuNgay.Location = new Point(70, 23);
        dtpTuNgay.Size = new Size(110, 25);

        // lblDenNgay
        lblDenNgay.Location = new Point(190, 26);
        lblDenNgay.Text = "Đến ngày";
        lblDenNgay.Size = new Size(60, 20);
        dtpDenNgay.Format = DateTimePickerFormat.Short;
        dtpDenNgay.Location = new Point(255, 23);
        dtpDenNgay.Size = new Size(110, 25);

        // lblLocSoBan
        lblLocSoBan.Location = new Point(375, 26);
        lblLocSoBan.Text = "Số bàn";
        lblLocSoBan.Size = new Size(45, 20);
        cboLocSoBan.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocSoBan.Location = new Point(425, 23);
        cboLocSoBan.Size = new Size(80, 25);

        // lblLocHinhThuc
        lblLocHinhThuc.Location = new Point(515, 26);
        lblLocHinhThuc.Text = "Hình thức";
        lblLocHinhThuc.Size = new Size(65, 20);
        cboLocHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocHinhThuc.Location = new Point(585, 23);
        cboLocHinhThuc.Size = new Size(110, 25);

        // lblLocTenMon
        lblLocTenMon.Location = new Point(705, 26);
        lblLocTenMon.Text = "Tên món";
        lblLocTenMon.Size = new Size(60, 20);
        txtLocTenMon.Location = new Point(770, 23);
        txtLocTenMon.Size = new Size(185, 25);

        // btnTimKiem
        btnTimKiem.Location = new Point(965, 22);
        btnTimKiem.Size = new Size(80, 28);
        btnTimKiem.Text = "Tìm";
        btnTimKiem.UseVisualStyleBackColor = true;
        btnTimKiem.Click += btnTimKiem_Click;

        // 
        // grpDanhSach (Vùng trái)
        // 
        grpDanhSach.Controls.Add(dgvHoaDon);
        grpDanhSach.Location = new Point(15, 80);
        grpDanhSach.Name = "grpDanhSach";
        grpDanhSach.Size = new Size(515, 340);
        grpDanhSach.TabIndex = 1;
        grpDanhSach.TabStop = false;
        grpDanhSach.Text = "Danh sách hóa đơn";

        dgvHoaDon.AllowUserToAddRows = false;
        dgvHoaDon.AllowUserToDeleteRows = false;
        dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHoaDon.Columns.AddRange(new DataGridViewColumn[] {
            colMaHD, colMaHDFormatted, colNgayLap, colSoBan, colHinhThucTT, colTongTien, colGhiChu
        });
        dgvHoaDon.Dock = DockStyle.Fill;
        dgvHoaDon.MultiSelect = false;
        dgvHoaDon.ReadOnly = true;
        dgvHoaDon.RowHeadersVisible = false;
        dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHoaDon.CellClick += dgvHoaDon_CellClick;

        colMaHD.Visible = false;
        colMaHD.Name = "colMaHD";

        colMaHDFormatted.HeaderText = "Mã HD";
        colMaHDFormatted.Name = "colMaHDFormatted";
        colMaHDFormatted.Width = 90;

        colNgayLap.HeaderText = "Ngày lập";
        colNgayLap.Name = "colNgayLap";
        colNgayLap.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        colNgayLap.Width = 125;

        colSoBan.HeaderText = "Số bàn";
        colSoBan.Name = "colSoBan";
        colSoBan.Width = 60;
        colSoBan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colHinhThucTT.HeaderText = "Hình thức TT";
        colHinhThucTT.Name = "colHinhThucTT";
        colHinhThucTT.Width = 110;

        colTongTien.HeaderText = "Tổng tiền";
        colTongTien.Name = "colTongTien";
        colTongTien.DefaultCellStyle.Format = "N0";
        colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colTongTien.Width = 100;

        colGhiChu.Visible = false;
        colGhiChu.Name = "colGhiChu";

        // 
        // grpChiTiet (Vùng phải)
        // 
        grpChiTiet.Controls.Add(lblNgayLap);
        grpChiTiet.Controls.Add(dtpNgayLap);
        grpChiTiet.Controls.Add(lblSoBan);
        grpChiTiet.Controls.Add(cboSoBan);
        grpChiTiet.Controls.Add(radTienMat);
        grpChiTiet.Controls.Add(radChuyenKhoan);
        grpChiTiet.Controls.Add(lblGhiChu);
        grpChiTiet.Controls.Add(txtGhiChu);
        grpChiTiet.Controls.Add(lblMon);
        grpChiTiet.Controls.Add(cboMon);
        grpChiTiet.Controls.Add(lblSoLuong);
        grpChiTiet.Controls.Add(numSoLuong);
        grpChiTiet.Controls.Add(btnThemMon);
        grpChiTiet.Controls.Add(dgvChiTiet);
        grpChiTiet.Location = new Point(540, 80);
        grpChiTiet.Name = "grpChiTiet";
        grpChiTiet.Size = new Size(535, 340);
        grpChiTiet.TabIndex = 2;
        grpChiTiet.TabStop = false;
        grpChiTiet.Text = "Chi tiết hóa đơn";

        lblNgayLap.Location = new Point(12, 25);
        lblNgayLap.Text = "Ngày lập:";
        lblNgayLap.Size = new Size(65, 20);
        dtpNgayLap.CustomFormat = "dd/MM/yyyy HH:mm";
        dtpNgayLap.Format = DateTimePickerFormat.Custom;
        dtpNgayLap.Location = new Point(80, 22);
        dtpNgayLap.Size = new Size(145, 25);

        lblSoBan.Location = new Point(235, 25);
        lblSoBan.Text = "Số bàn:";
        lblSoBan.Size = new Size(50, 20);
        cboSoBan.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSoBan.Location = new Point(290, 22);
        cboSoBan.Size = new Size(55, 25);

        radTienMat.Location = new Point(360, 24);
        radTienMat.Size = new Size(75, 22);
        radTienMat.Text = "Tiền mặt";
        radTienMat.Checked = true;

        radChuyenKhoan.Location = new Point(440, 24);
        radChuyenKhoan.Size = new Size(85, 22);
        radChuyenKhoan.Text = "C/khoản";

        lblGhiChu.Location = new Point(12, 58);
        lblGhiChu.Text = "Ghi chú:";
        lblGhiChu.Size = new Size(65, 20);
        txtGhiChu.Location = new Point(80, 55);
        txtGhiChu.Size = new Size(445, 25);

        lblMon.Location = new Point(12, 91);
        lblMon.Text = "Chọn món:";
        lblMon.Size = new Size(70, 20);
        cboMon.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMon.Location = new Point(85, 88);
        cboMon.Size = new Size(205, 25);

        lblSoLuong.Location = new Point(300, 91);
        lblSoLuong.Text = "SL:";
        lblSoLuong.Size = new Size(25, 20);
        numSoLuong.Location = new Point(330, 88);
        numSoLuong.Minimum = 1;
        numSoLuong.Maximum = 50;
        numSoLuong.Value = 1;
        numSoLuong.Size = new Size(60, 25);

        btnThemMon.Location = new Point(405, 86);
        btnThemMon.Size = new Size(120, 28);
        btnThemMon.Text = "Thêm món";
        btnThemMon.UseVisualStyleBackColor = true;
        btnThemMon.Click += btnThemMon_Click;

        dgvChiTiet.AllowUserToAddRows = false;
        dgvChiTiet.AllowUserToDeleteRows = false;
        dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] {
            colMaCT, colSTT, colTenMon, colSL, colDonGia, colThanhTien, colMaMonVal
        });
        dgvChiTiet.Location = new Point(12, 122);
        dgvChiTiet.MultiSelect = false;
        dgvChiTiet.ReadOnly = true;
        dgvChiTiet.RowHeadersVisible = false;
        dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvChiTiet.Size = new Size(513, 208);
        dgvChiTiet.CellClick += dgvChiTiet_CellClick;

        colMaCT.Visible = false;
        colMaCT.Name = "colMaCT";

        colSTT.HeaderText = "STT";
        colSTT.Name = "colSTT";
        colSTT.Width = 45;
        colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colTenMon.HeaderText = "Tên món";
        colTenMon.Name = "colTenMon";
        colTenMon.Width = 170;

        colSL.HeaderText = "SL";
        colSL.Name = "colSL";
        colSL.Width = 45;
        colSL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colDonGia.HeaderText = "Đơn giá";
        colDonGia.Name = "colDonGia";
        colDonGia.DefaultCellStyle.Format = "N0";
        colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colDonGia.Width = 90;

        colThanhTien.HeaderText = "Thành tiền";
        colThanhTien.Name = "colThanhTien";
        colThanhTien.DefaultCellStyle.Format = "N0";
        colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colThanhTien.Width = 100;

        colMaMonVal.Visible = false;
        colMaMonVal.Name = "colMaMonVal";

        // 
        // Labels tổng doanh thu và tổng cộng
        // 
        lblTongDoanhThu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTongDoanhThu.Location = new Point(15, 428);
        lblTongDoanhThu.Size = new Size(515, 30);
        lblTongDoanhThu.Text = "Tổng doanh thu đang hiển thị: 0 đ";
        lblTongDoanhThu.TextAlign = ContentAlignment.MiddleLeft;

        lblTongCong.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTongCong.Location = new Point(540, 428);
        lblTongCong.Size = new Size(535, 30);
        lblTongCong.Text = "Tổng cộng: 0 đ";
        lblTongCong.TextAlign = ContentAlignment.MiddleRight;

        // 
        // Buttons dưới
        // 
        btnHoaDonMoi.Location = new Point(15, 468);
        btnHoaDonMoi.Size = new Size(115, 34);
        btnHoaDonMoi.Text = "Hóa đơn mới";
        btnHoaDonMoi.UseVisualStyleBackColor = true;
        btnHoaDonMoi.Click += btnHoaDonMoi_Click;

        btnSuaHoaDon.Location = new Point(140, 468);
        btnSuaHoaDon.Size = new Size(115, 34);
        btnSuaHoaDon.Text = "Sửa hóa đơn";
        btnSuaHoaDon.UseVisualStyleBackColor = true;
        btnSuaHoaDon.Click += btnSuaHoaDon_Click;

        btnXoaHoaDon.Location = new Point(265, 468);
        btnXoaHoaDon.Size = new Size(115, 34);
        btnXoaHoaDon.Text = "Xóa hóa đơn";
        btnXoaHoaDon.UseVisualStyleBackColor = true;
        btnXoaHoaDon.Click += btnXoaHoaDon_Click;

        btnSuaSL.Location = new Point(540, 468);
        btnSuaSL.Size = new Size(100, 34);
        btnSuaSL.Text = "Sửa SL";
        btnSuaSL.UseVisualStyleBackColor = true;
        btnSuaSL.Click += btnSuaSL_Click;

        btnXoaMon.Location = new Point(650, 468);
        btnXoaMon.Size = new Size(100, 34);
        btnXoaMon.Text = "Xóa món";
        btnXoaMon.UseVisualStyleBackColor = true;
        btnXoaMon.Click += btnXoaMon_Click;

        btnXemHoaDon.Location = new Point(760, 468);
        btnXemHoaDon.Size = new Size(110, 34);
        btnXemHoaDon.Text = "Xem hóa đơn";
        btnXemHoaDon.UseVisualStyleBackColor = true;
        btnXemHoaDon.Click += btnXemHoaDon_Click;

        btnXuatPdf.Location = new Point(880, 468);
        btnXuatPdf.Size = new Size(100, 34);
        btnXuatPdf.Text = "Xuất PDF";
        btnXuatPdf.UseVisualStyleBackColor = true;
        btnXuatPdf.Click += btnXuatPdf_Click;

        btnIn.Location = new Point(990, 468);
        btnIn.Size = new Size(85, 34);
        btnIn.Text = "In";
        btnIn.UseVisualStyleBackColor = true;
        btnIn.Click += btnIn_Click;

        // Form
        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(1090, 515);
        Controls.Add(btnIn);
        Controls.Add(btnXuatPdf);
        Controls.Add(btnXemHoaDon);
        Controls.Add(btnXoaMon);
        Controls.Add(btnSuaSL);
        Controls.Add(btnXoaHoaDon);
        Controls.Add(btnSuaHoaDon);
        Controls.Add(btnHoaDonMoi);
        Controls.Add(lblTongCong);
        Controls.Add(lblTongDoanhThu);
        Controls.Add(grpChiTiet);
        Controls.Add(grpDanhSach);
        Controls.Add(grpTimKiem);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quán cà phê Góc Phố - Quản lý hóa đơn";
        Load += Form1_Load;
        grpTimKiem.ResumeLayout(false);
        grpTimKiem.PerformLayout();
        grpDanhSach.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
        grpChiTiet.ResumeLayout(false);
        grpChiTiet.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numSoLuong).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
        ResumeLayout(false);
    }

    private GroupBox grpTimKiem;
    private Label lblTuNgay;
    private DateTimePicker dtpTuNgay;
    private Label lblDenNgay;
    private DateTimePicker dtpDenNgay;
    private Label lblLocSoBan;
    private ComboBox cboLocSoBan;
    private Label lblLocHinhThuc;
    private ComboBox cboLocHinhThuc;
    private Label lblLocTenMon;
    private TextBox txtLocTenMon;
    private Button btnTimKiem;

    private GroupBox grpDanhSach;
    private DataGridView dgvHoaDon;
    private DataGridViewTextBoxColumn colMaHD;
    private DataGridViewTextBoxColumn colMaHDFormatted;
    private DataGridViewTextBoxColumn colNgayLap;
    private DataGridViewTextBoxColumn colSoBan;
    private DataGridViewTextBoxColumn colHinhThucTT;
    private DataGridViewTextBoxColumn colTongTien;
    private DataGridViewTextBoxColumn colGhiChu;

    private GroupBox grpChiTiet;
    private Label lblNgayLap;
    private DateTimePicker dtpNgayLap;
    private Label lblSoBan;
    private ComboBox cboSoBan;
    private RadioButton radTienMat;
    private RadioButton radChuyenKhoan;
    private Label lblGhiChu;
    private TextBox txtGhiChu;
    private Label lblMon;
    private ComboBox cboMon;
    private Label lblSoLuong;
    private NumericUpDown numSoLuong;
    private Button btnThemMon;
    private DataGridView dgvChiTiet;
    private DataGridViewTextBoxColumn colMaCT;
    private DataGridViewTextBoxColumn colSTT;
    private DataGridViewTextBoxColumn colTenMon;
    private DataGridViewTextBoxColumn colSL;
    private DataGridViewTextBoxColumn colDonGia;
    private DataGridViewTextBoxColumn colThanhTien;
    private DataGridViewTextBoxColumn colMaMonVal;

    private Label lblTongDoanhThu;
    private Label lblTongCong;

    private Button btnHoaDonMoi;
    private Button btnSuaHoaDon;
    private Button btnXoaHoaDon;
    private Button btnSuaSL;
    private Button btnXoaMon;
    private Button btnXemHoaDon;
    private Button btnXuatPdf;
    private Button btnIn;
}
