namespace Cau3_QuanLyDiem;

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
        lblLopTitle = new Label();
        lstLop = new ListBox();
        lblTimKiem = new Label();
        txtTimKiem = new TextBox();
        dgvSinhVien = new DataGridView();
        colMaSV = new DataGridViewTextBoxColumn();
        colHoTen = new DataGridViewTextBoxColumn();
        colNgaySinh = new DataGridViewTextBoxColumn();
        colGioiTinh = new DataGridViewTextBoxColumn();
        colMaLop = new DataGridViewTextBoxColumn();

        grpThongTinSV = new GroupBox();
        lblMSSV = new Label();
        txtMSSV = new TextBox();
        lblHoTen = new Label();
        txtHoTen = new TextBox();
        lblNgaySinh = new Label();
        dtpNgaySinh = new DateTimePicker();
        radNam = new RadioButton();
        radNu = new RadioButton();
        lblLopSV = new Label();
        cboLop = new ComboBox();
        btnThemSV = new Button();
        btnSuaSV = new Button();
        btnXoaSV = new Button();

        grpDiemSV = new GroupBox();
        dgvDiem = new DataGridView();
        colMaDiem = new DataGridViewTextBoxColumn();
        colSTTDiem = new DataGridViewTextBoxColumn();
        colMonHoc = new DataGridViewTextBoxColumn();
        colSoTC = new DataGridViewTextBoxColumn();
        colDiem = new DataGridViewTextBoxColumn();
        lblTenMon = new Label();
        txtTenMon = new TextBox();
        lblSoTinChi = new Label();
        numSoTinChi = new NumericUpDown();
        lblDiemVal = new Label();
        numDiem = new NumericUpDown();
        btnThemMon = new Button();
        btnSuaDiem = new Button();
        btnXoaMon = new Button();
        lblTongTinChi = new Label();
        lblDiemTB = new Label();
        lblXepLoai = new Label();

        btnXemTruocPhieu = new Button();
        btnInPhieu = new Button();
        btnCaiDatTrang = new Button();
        btnXuatExcelLop = new Button();

        ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
        grpThongTinSV.SuspendLayout();
        grpDiemSV.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDiem).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSoTinChi).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numDiem).BeginInit();
        SuspendLayout();

        // 
        // Vùng bên trái: ListBox Lớp
        // 
        lblLopTitle.Location = new Point(15, 9);
        lblLopTitle.Text = "Lớp";
        lblLopTitle.Size = new Size(50, 18);
        lstLop.Location = new Point(15, 30);
        lstLop.Size = new Size(210, 240);
        lstLop.TabIndex = 0;
        lstLop.SelectedIndexChanged += lstLop_SelectedIndexChanged;

        // 
        // Vùng ở giữa: Tìm kiếm & DataGridView Sinh viên
        // 
        lblTimKiem.Location = new Point(235, 9);
        lblTimKiem.Text = "Tìm MSSV / họ tên:";
        lblTimKiem.Size = new Size(130, 20);
        txtTimKiem.Location = new Point(370, 6);
        txtTimKiem.PlaceholderText = "Gõ MSSV hoặc tên để tìm...";
        txtTimKiem.Size = new Size(335, 25);
        txtTimKiem.TextChanged += txtTimKiem_TextChanged;

        dgvSinhVien.AllowUserToAddRows = false;
        dgvSinhVien.AllowUserToDeleteRows = false;
        dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSinhVien.Columns.AddRange(new DataGridViewColumn[] {
            colMaSV, colHoTen, colNgaySinh, colGioiTinh, colMaLop
        });
        dgvSinhVien.Location = new Point(235, 35);
        dgvSinhVien.MultiSelect = false;
        dgvSinhVien.ReadOnly = true;
        dgvSinhVien.RowHeadersVisible = false;
        dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSinhVien.Size = new Size(470, 235);
        dgvSinhVien.TabIndex = 1;
        dgvSinhVien.CellClick += dgvSinhVien_CellClick;

        colMaSV.HeaderText = "MSSV";
        colMaSV.Name = "colMaSV";
        colMaSV.Width = 70;

        colHoTen.HeaderText = "Họ tên";
        colHoTen.Name = "colHoTen";
        colHoTen.Width = 150;

        colNgaySinh.HeaderText = "Ngày sinh";
        colNgaySinh.Name = "colNgaySinh";
        colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
        colNgaySinh.Width = 95;

        colGioiTinh.HeaderText = "Giới tính";
        colGioiTinh.Name = "colGioiTinh";
        colGioiTinh.Width = 70;
        colGioiTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colMaLop.HeaderText = "Lớp";
        colMaLop.Name = "colMaLop";
        colMaLop.Width = 75;

        // 
        // Vùng bên phải: GroupBox Thông tin sinh viên
        // 
        grpThongTinSV.Controls.Add(lblMSSV);
        grpThongTinSV.Controls.Add(txtMSSV);
        grpThongTinSV.Controls.Add(lblHoTen);
        grpThongTinSV.Controls.Add(txtHoTen);
        grpThongTinSV.Controls.Add(lblNgaySinh);
        grpThongTinSV.Controls.Add(dtpNgaySinh);
        grpThongTinSV.Controls.Add(radNam);
        grpThongTinSV.Controls.Add(radNu);
        grpThongTinSV.Controls.Add(lblLopSV);
        grpThongTinSV.Controls.Add(cboLop);
        grpThongTinSV.Controls.Add(btnThemSV);
        grpThongTinSV.Controls.Add(btnSuaSV);
        grpThongTinSV.Controls.Add(btnXoaSV);
        grpThongTinSV.Location = new Point(715, 12);
        grpThongTinSV.Name = "grpThongTinSV";
        grpThongTinSV.Size = new Size(355, 258);
        grpThongTinSV.TabIndex = 2;
        grpThongTinSV.TabStop = false;
        grpThongTinSV.Text = "Thông tin sinh viên";

        lblMSSV.Location = new Point(15, 26);
        lblMSSV.Text = "MSSV:";
        lblMSSV.Size = new Size(70, 20);
        txtMSSV.Location = new Point(90, 23);
        txtMSSV.Size = new Size(250, 25);

        lblHoTen.Location = new Point(15, 59);
        lblHoTen.Text = "Họ tên:";
        lblHoTen.Size = new Size(70, 20);
        txtHoTen.Location = new Point(90, 56);
        txtHoTen.Size = new Size(250, 25);

        lblNgaySinh.Location = new Point(15, 92);
        lblNgaySinh.Text = "Ngày sinh:";
        lblNgaySinh.Size = new Size(70, 20);
        dtpNgaySinh.Format = DateTimePickerFormat.Short;
        dtpNgaySinh.Location = new Point(90, 89);
        dtpNgaySinh.Size = new Size(250, 25);

        radNam.Checked = true;
        radNam.Location = new Point(90, 122);
        radNam.Size = new Size(70, 22);
        radNam.Text = "Nam";

        radNu.Location = new Point(180, 122);
        radNu.Size = new Size(70, 22);
        radNu.Text = "Nữ";

        lblLopSV.Location = new Point(15, 155);
        lblLopSV.Text = "Lớp:";
        lblLopSV.Size = new Size(70, 20);
        cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLop.Location = new Point(90, 152);
        cboLop.Size = new Size(250, 25);

        btnThemSV.Location = new Point(35, 200);
        btnThemSV.Size = new Size(85, 32);
        btnThemSV.Text = "Thêm";
        btnThemSV.UseVisualStyleBackColor = true;
        btnThemSV.Click += btnThemSV_Click;

        btnSuaSV.Location = new Point(135, 200);
        btnSuaSV.Size = new Size(85, 32);
        btnSuaSV.Text = "Sửa";
        btnSuaSV.UseVisualStyleBackColor = true;
        btnSuaSV.Click += btnSuaSV_Click;

        btnXoaSV.Location = new Point(235, 200);
        btnXoaSV.Size = new Size(85, 32);
        btnXoaSV.Text = "Xóa";
        btnXoaSV.UseVisualStyleBackColor = true;
        btnXoaSV.Click += btnXoaSV_Click;

        // 
        // Vùng phía dưới: GroupBox Điểm của sinh viên
        // 
        grpDiemSV.Controls.Add(dgvDiem);
        grpDiemSV.Controls.Add(lblTenMon);
        grpDiemSV.Controls.Add(txtTenMon);
        grpDiemSV.Controls.Add(lblSoTinChi);
        grpDiemSV.Controls.Add(numSoTinChi);
        grpDiemSV.Controls.Add(lblDiemVal);
        grpDiemSV.Controls.Add(numDiem);
        grpDiemSV.Controls.Add(btnThemMon);
        grpDiemSV.Controls.Add(btnSuaDiem);
        grpDiemSV.Controls.Add(btnXoaMon);
        grpDiemSV.Controls.Add(lblTongTinChi);
        grpDiemSV.Controls.Add(lblDiemTB);
        grpDiemSV.Controls.Add(lblXepLoai);
        grpDiemSV.Location = new Point(15, 280);
        grpDiemSV.Name = "grpDiemSV";
        grpDiemSV.Size = new Size(1055, 230);
        grpDiemSV.TabIndex = 3;
        grpDiemSV.TabStop = false;
        grpDiemSV.Text = "Điểm của sinh viên";

        dgvDiem.AllowUserToAddRows = false;
        dgvDiem.AllowUserToDeleteRows = false;
        dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDiem.Columns.AddRange(new DataGridViewColumn[] {
            colMaDiem, colSTTDiem, colMonHoc, colSoTC, colDiem
        });
        dgvDiem.Location = new Point(12, 25);
        dgvDiem.MultiSelect = false;
        dgvDiem.ReadOnly = true;
        dgvDiem.RowHeadersVisible = false;
        dgvDiem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDiem.Size = new Size(490, 192);
        dgvDiem.TabIndex = 0;
        dgvDiem.CellClick += dgvDiem_CellClick;

        colMaDiem.Visible = false;
        colMaDiem.Name = "colMaDiem";

        colSTTDiem.HeaderText = "STT";
        colSTTDiem.Name = "colSTTDiem";
        colSTTDiem.Width = 45;
        colSTTDiem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colMonHoc.HeaderText = "Môn học";
        colMonHoc.Name = "colMonHoc";
        colMonHoc.Width = 230;

        colSoTC.HeaderText = "Số TC";
        colSoTC.Name = "colSoTC";
        colSoTC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        colSoTC.Width = 65;

        colDiem.HeaderText = "Điểm";
        colDiem.Name = "colDiem";
        colDiem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colDiem.DefaultCellStyle.Format = "0.0";
        colDiem.Width = 70;

        // Controls nhập điểm
        lblTenMon.Location = new Point(515, 26);
        lblTenMon.Text = "Tên môn:";
        lblTenMon.Size = new Size(65, 20);
        txtTenMon.Location = new Point(585, 23);
        txtTenMon.Size = new Size(215, 25);

        lblSoTinChi.Location = new Point(810, 26);
        lblSoTinChi.Text = "Số TC:";
        lblSoTinChi.Size = new Size(45, 20);
        numSoTinChi.Location = new Point(860, 23);
        numSoTinChi.Minimum = 1;
        numSoTinChi.Maximum = 6;
        numSoTinChi.Value = 3;
        numSoTinChi.Size = new Size(50, 25);

        lblDiemVal.Location = new Point(920, 26);
        lblDiemVal.Text = "Điểm:";
        lblDiemVal.Size = new Size(45, 20);
        numDiem.DecimalPlaces = 1;
        numDiem.Increment = 0.5m;
        numDiem.Minimum = 0;
        numDiem.Maximum = 10;
        numDiem.Location = new Point(970, 23);
        numDiem.Size = new Size(65, 25);

        btnThemMon.Location = new Point(585, 68);
        btnThemMon.Size = new Size(105, 34);
        btnThemMon.Text = "Thêm môn";
        btnThemMon.UseVisualStyleBackColor = true;
        btnThemMon.Click += btnThemMon_Click;

        btnSuaDiem.Location = new Point(705, 68);
        btnSuaDiem.Size = new Size(105, 34);
        btnSuaDiem.Text = "Sửa điểm";
        btnSuaDiem.UseVisualStyleBackColor = true;
        btnSuaDiem.Click += btnSuaDiem_Click;

        btnXoaMon.Location = new Point(825, 68);
        btnXoaMon.Size = new Size(105, 34);
        btnXoaMon.Text = "Xóa môn";
        btnXoaMon.UseVisualStyleBackColor = true;
        btnXoaMon.Click += btnXoaMon_Click;

        // Labels tổng kết
        lblTongTinChi.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTongTinChi.Location = new Point(515, 140);
        lblTongTinChi.Size = new Size(150, 30);
        lblTongTinChi.Text = "Tổng tín chỉ: 0";

        lblDiemTB.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDiemTB.Location = new Point(675, 140);
        lblDiemTB.Size = new Size(180, 30);
        lblDiemTB.Text = "Điểm TB: Chưa có điểm";

        lblXepLoai.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblXepLoai.Location = new Point(865, 140);
        lblXepLoai.Size = new Size(180, 30);
        lblXepLoai.Text = "Xếp loại: Chưa có điểm";

        // 
        // 4 nút dưới cùng
        // 
        btnXemTruocPhieu.Location = new Point(15, 520);
        btnXemTruocPhieu.Size = new Size(135, 34);
        btnXemTruocPhieu.Text = "Xem trước phiếu";
        btnXemTruocPhieu.UseVisualStyleBackColor = true;
        btnXemTruocPhieu.Click += btnXemTruocPhieu_Click;

        btnInPhieu.Location = new Point(160, 520);
        btnInPhieu.Size = new Size(95, 34);
        btnInPhieu.Text = "In…";
        btnInPhieu.UseVisualStyleBackColor = true;
        btnInPhieu.Click += btnInPhieu_Click;

        btnCaiDatTrang.Location = new Point(265, 520);
        btnCaiDatTrang.Size = new Size(120, 34);
        btnCaiDatTrang.Text = "Cài đặt trang…";
        btnCaiDatTrang.UseVisualStyleBackColor = true;
        btnCaiDatTrang.Click += btnCaiDatTrang_Click;

        btnXuatExcelLop.Location = new Point(395, 520);
        btnXuatExcelLop.Size = new Size(160, 34);
        btnXuatExcelLop.Text = "Xuất Excel điểm lớp";
        btnXuatExcelLop.UseVisualStyleBackColor = true;
        btnXuatExcelLop.Click += btnXuatExcelLop_Click;

        // Form
        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(1085, 565);
        Controls.Add(btnXuatExcelLop);
        Controls.Add(btnCaiDatTrang);
        Controls.Add(btnInPhieu);
        Controls.Add(btnXemTruocPhieu);
        Controls.Add(grpDiemSV);
        Controls.Add(grpThongTinSV);
        Controls.Add(dgvSinhVien);
        Controls.Add(txtTimKiem);
        Controls.Add(lblTimKiem);
        Controls.Add(lstLop);
        Controls.Add(lblLopTitle);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý sinh viên và điểm - Khoa CNTT";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
        grpThongTinSV.ResumeLayout(false);
        grpThongTinSV.PerformLayout();
        grpDiemSV.ResumeLayout(false);
        grpDiemSV.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDiem).EndInit();
        ((System.ComponentModel.ISupportInitialize)numSoTinChi).EndInit();
        ((System.ComponentModel.ISupportInitialize)numDiem).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblLopTitle;
    private ListBox lstLop;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private DataGridView dgvSinhVien;
    private DataGridViewTextBoxColumn colMaSV;
    private DataGridViewTextBoxColumn colHoTen;
    private DataGridViewTextBoxColumn colNgaySinh;
    private DataGridViewTextBoxColumn colGioiTinh;
    private DataGridViewTextBoxColumn colMaLop;

    private GroupBox grpThongTinSV;
    private Label lblMSSV;
    private TextBox txtMSSV;
    private Label lblHoTen;
    private TextBox txtHoTen;
    private Label lblNgaySinh;
    private DateTimePicker dtpNgaySinh;
    private RadioButton radNam;
    private RadioButton radNu;
    private Label lblLopSV;
    private ComboBox cboLop;
    private Button btnThemSV;
    private Button btnSuaSV;
    private Button btnXoaSV;

    private GroupBox grpDiemSV;
    private DataGridView dgvDiem;
    private DataGridViewTextBoxColumn colMaDiem;
    private DataGridViewTextBoxColumn colSTTDiem;
    private DataGridViewTextBoxColumn colMonHoc;
    private DataGridViewTextBoxColumn colSoTC;
    private DataGridViewTextBoxColumn colDiem;
    private Label lblTenMon;
    private TextBox txtTenMon;
    private Label lblSoTinChi;
    private NumericUpDown numSoTinChi;
    private Label lblDiemVal;
    private NumericUpDown numDiem;
    private Button btnThemMon;
    private Button btnSuaDiem;
    private Button btnXoaMon;
    private Label lblTongTinChi;
    private Label lblDiemTB;
    private Label lblXepLoai;

    private Button btnXemTruocPhieu;
    private Button btnInPhieu;
    private Button btnCaiDatTrang;
    private Button btnXuatExcelLop;
}
