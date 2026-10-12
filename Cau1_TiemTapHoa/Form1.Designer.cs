namespace Cau1_TiemTapHoa;

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
        grpThongTin = new GroupBox();
        chkConKinhDoanh = new CheckBox();
        dtpNgayNhap = new DateTimePicker();
        numSoLuongTon = new NumericUpDown();
        numGiaBan = new NumericUpDown();
        cboDonVi = new ComboBox();
        cboLoaiHang = new ComboBox();
        txtTenHang = new TextBox();
        lblConKinhDoanh = new Label();
        lblNgayNhap = new Label();
        lblSoLuongTon = new Label();
        lblGiaBan = new Label();
        lblDonVi = new Label();
        lblLoaiHang = new Label();
        lblTenHang = new Label();
        
        grpTimKiem = new GroupBox();
        chkLocConKinhDoanh = new CheckBox();
        cboLocLoaiHang = new ComboBox();
        txtTimKiem = new TextBox();
        
        dgvHangHoa = new DataGridView();
        colMaHang = new DataGridViewTextBoxColumn();
        colTenHang = new DataGridViewTextBoxColumn();
        colLoaiHang = new DataGridViewTextBoxColumn();
        colDonVi = new DataGridViewTextBoxColumn();
        colGiaBan = new DataGridViewTextBoxColumn();
        colSoLuongTon = new DataGridViewTextBoxColumn();
        colNgayNhap = new DataGridViewTextBoxColumn();
        colConKinhDoanh = new DataGridViewCheckBoxColumn();
        
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        btnXemBangGia = new Button();
        btnXuatPdf = new Button();
        btnXuatExcelLuoi = new Button();
        btnXuatExcelTonKho = new Button();
        
        statusStrip1 = new StatusStrip();
        lblTrangThai = new ToolStripStatusLabel();

        grpThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numSoLuongTon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numGiaBan).BeginInit();
        grpTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
        statusStrip1.SuspendLayout();
        SuspendLayout();

        // 
        // grpThongTin
        // 
        grpThongTin.Controls.Add(chkConKinhDoanh);
        grpThongTin.Controls.Add(dtpNgayNhap);
        grpThongTin.Controls.Add(numSoLuongTon);
        grpThongTin.Controls.Add(numGiaBan);
        grpThongTin.Controls.Add(cboDonVi);
        grpThongTin.Controls.Add(cboLoaiHang);
        grpThongTin.Controls.Add(txtTenHang);
        grpThongTin.Controls.Add(lblConKinhDoanh);
        grpThongTin.Controls.Add(lblNgayNhap);
        grpThongTin.Controls.Add(lblSoLuongTon);
        grpThongTin.Controls.Add(lblGiaBan);
        grpThongTin.Controls.Add(lblDonVi);
        grpThongTin.Controls.Add(lblLoaiHang);
        grpThongTin.Controls.Add(lblTenHang);
        grpThongTin.Location = new Point(15, 12);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Size = new Size(620, 160);
        grpThongTin.TabIndex = 0;
        grpThongTin.TabStop = false;
        grpThongTin.Text = "Thông tin hàng hóa";

        // lblTenHang
        lblTenHang.Location = new Point(15, 26);
        lblTenHang.Size = new Size(80, 22);
        lblTenHang.Text = "Tên hàng:";
        // txtTenHang
        txtTenHang.Location = new Point(100, 23);
        txtTenHang.Size = new Size(500, 25);

        // lblLoaiHang
        lblLoaiHang.Location = new Point(15, 59);
        lblLoaiHang.Size = new Size(80, 22);
        lblLoaiHang.Text = "Loại hàng:";
        // cboLoaiHang
        cboLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoaiHang.Location = new Point(100, 56);
        cboLoaiHang.Size = new Size(185, 25);

        // lblDonVi
        lblDonVi.Location = new Point(15, 92);
        lblDonVi.Size = new Size(80, 22);
        lblDonVi.Text = "Đơn vị:";
        // cboDonVi
        cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDonVi.Location = new Point(100, 89);
        cboDonVi.Size = new Size(185, 25);

        // lblGiaBan
        lblGiaBan.Location = new Point(15, 125);
        lblGiaBan.Size = new Size(80, 22);
        lblGiaBan.Text = "Giá bán:";
        // numGiaBan
        numGiaBan.Location = new Point(100, 122);
        numGiaBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numGiaBan.Increment = 500m;
        numGiaBan.ThousandsSeparator = true;
        numGiaBan.Size = new Size(185, 25);

        // lblSoLuongTon
        lblSoLuongTon.Location = new Point(315, 59);
        lblSoLuongTon.Size = new Size(100, 22);
        lblSoLuongTon.Text = "Số lượng tồn:";
        // numSoLuongTon
        numSoLuongTon.Location = new Point(420, 56);
        numSoLuongTon.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        numSoLuongTon.Size = new Size(180, 25);

        // lblNgayNhap
        lblNgayNhap.Location = new Point(315, 92);
        lblNgayNhap.Size = new Size(100, 22);
        lblNgayNhap.Text = "Ngày nhập:";
        // dtpNgayNhap
        dtpNgayNhap.Format = DateTimePickerFormat.Short;
        dtpNgayNhap.Location = new Point(420, 89);
        dtpNgayNhap.Size = new Size(180, 25);

        // chkConKinhDoanh
        chkConKinhDoanh.AutoSize = true;
        chkConKinhDoanh.Checked = true;
        chkConKinhDoanh.CheckState = CheckState.Checked;
        chkConKinhDoanh.Location = new Point(420, 124);
        chkConKinhDoanh.Text = "Còn kinh doanh";

        // 
        // grpTimKiem
        // 
        grpTimKiem.Controls.Add(chkLocConKinhDoanh);
        grpTimKiem.Controls.Add(cboLocLoaiHang);
        grpTimKiem.Controls.Add(txtTimKiem);
        grpTimKiem.Location = new Point(650, 12);
        grpTimKiem.Name = "grpTimKiem";
        grpTimKiem.Size = new Size(335, 160);
        grpTimKiem.TabIndex = 1;
        grpTimKiem.TabStop = false;
        grpTimKiem.Text = "Tìm kiếm";

        // txtTimKiem
        txtTimKiem.Location = new Point(15, 25);
        txtTimKiem.PlaceholderText = "Tìm theo tên hàng...";
        txtTimKiem.Size = new Size(305, 25);
        txtTimKiem.TextChanged += txtTimKiem_TextChanged;

        // cboLocLoaiHang
        cboLocLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocLoaiHang.Location = new Point(15, 65);
        cboLocLoaiHang.Size = new Size(305, 25);
        cboLocLoaiHang.SelectedIndexChanged += cboLocLoaiHang_SelectedIndexChanged;

        // chkLocConKinhDoanh
        chkLocConKinhDoanh.AutoSize = true;
        chkLocConKinhDoanh.Location = new Point(15, 110);
        chkLocConKinhDoanh.Text = "Chỉ hiện hàng còn kinh doanh";
        chkLocConKinhDoanh.CheckedChanged += chkLocConKinhDoanh_CheckedChanged;

        // 
        // dgvHangHoa
        // 
        dgvHangHoa.AllowUserToAddRows = false;
        dgvHangHoa.AllowUserToDeleteRows = false;
        dgvHangHoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] {
            colMaHang, colTenHang, colLoaiHang, colDonVi, colGiaBan, colSoLuongTon, colNgayNhap, colConKinhDoanh
        });
        dgvHangHoa.Location = new Point(15, 185);
        dgvHangHoa.MultiSelect = false;
        dgvHangHoa.Name = "dgvHangHoa";
        dgvHangHoa.ReadOnly = true;
        dgvHangHoa.RowHeadersVisible = true;
        dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHangHoa.Size = new Size(970, 270);
        dgvHangHoa.TabIndex = 2;
        dgvHangHoa.CellClick += dgvHangHoa_CellClick;

        colMaHang.HeaderText = "Mã";
        colMaHang.Name = "colMaHang";
        colMaHang.ReadOnly = true;
        colMaHang.Width = 60;

        colTenHang.HeaderText = "Tên hàng";
        colTenHang.Name = "colTenHang";
        colTenHang.ReadOnly = true;
        colTenHang.Width = 220;

        colLoaiHang.HeaderText = "Loại hàng";
        colLoaiHang.Name = "colLoaiHang";
        colLoaiHang.ReadOnly = true;

        colDonVi.HeaderText = "Đơn vị";
        colDonVi.Name = "colDonVi";
        colDonVi.ReadOnly = true;
        colDonVi.Width = 70;

        colGiaBan.HeaderText = "Giá bán";
        colGiaBan.Name = "colGiaBan";
        colGiaBan.ReadOnly = true;
        colGiaBan.DefaultCellStyle.Format = "N0";
        colGiaBan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        colSoLuongTon.HeaderText = "Tồn";
        colSoLuongTon.Name = "colSoLuongTon";
        colSoLuongTon.ReadOnly = true;
        colSoLuongTon.DefaultCellStyle.Format = "N0";
        colSoLuongTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colSoLuongTon.Width = 65;

        colNgayNhap.HeaderText = "Ngày nhập";
        colNgayNhap.Name = "colNgayNhap";
        colNgayNhap.ReadOnly = true;
        colNgayNhap.DefaultCellStyle.Format = "dd/MM/yyyy";
        colNgayNhap.Width = 100;

        colConKinhDoanh.HeaderText = "Kinh doanh";
        colConKinhDoanh.Name = "colConKinhDoanh";
        colConKinhDoanh.ReadOnly = true;
        colConKinhDoanh.Width = 85;

        // 
        // Buttons
        // 
        btnThem.Location = new Point(15, 470);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(80, 32);
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        btnThem.Click += btnThem_Click;

        btnSua.Location = new Point(105, 470);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(80, 32);
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        btnSua.Click += btnSua_Click;

        btnXoa.Location = new Point(195, 470);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(80, 32);
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        btnXoa.Click += btnXoa_Click;

        btnLamMoi.Location = new Point(285, 470);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(85, 32);
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;

        btnXemBangGia.Location = new Point(445, 470);
        btnXemBangGia.Name = "btnXemBangGia";
        btnXemBangGia.Size = new Size(120, 32);
        btnXemBangGia.Text = "Xem bảng giá";
        btnXemBangGia.UseVisualStyleBackColor = true;
        btnXemBangGia.Click += btnXemBangGia_Click;

        btnXuatPdf.Location = new Point(575, 470);
        btnXuatPdf.Name = "btnXuatPdf";
        btnXuatPdf.Size = new Size(95, 32);
        btnXuatPdf.Text = "Xuất PDF";
        btnXuatPdf.UseVisualStyleBackColor = true;
        btnXuatPdf.Click += btnXuatPdf_Click;

        btnXuatExcelLuoi.Location = new Point(680, 470);
        btnXuatExcelLuoi.Name = "btnXuatExcelLuoi";
        btnXuatExcelLuoi.Size = new Size(140, 32);
        btnXuatExcelLuoi.Text = "Xuất Excel (lưới)";
        btnXuatExcelLuoi.UseVisualStyleBackColor = true;
        btnXuatExcelLuoi.Click += btnXuatExcelLuoi_Click;

        btnXuatExcelTonKho.Location = new Point(830, 470);
        btnXuatExcelTonKho.Name = "btnXuatExcelTonKho";
        btnXuatExcelTonKho.Size = new Size(155, 32);
        btnXuatExcelTonKho.Text = "Xuất Excel tồn kho";
        btnXuatExcelTonKho.UseVisualStyleBackColor = true;
        btnXuatExcelTonKho.Click += btnXuatExcelTonKho_Click;

        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblTrangThai });
        statusStrip1.Location = new Point(0, 515);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1000, 22);
        statusStrip1.TabIndex = 3;

        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(118, 17);
        lblTrangThai.Text = "Tìm thấy 0 mặt hàng";

        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(1000, 540);
        Controls.Add(statusStrip1);
        Controls.Add(btnXuatExcelTonKho);
        Controls.Add(btnXuatExcelLuoi);
        Controls.Add(btnXuatPdf);
        Controls.Add(btnXemBangGia);
        Controls.Add(btnLamMoi);
        Controls.Add(btnXoa);
        Controls.Add(btnSua);
        Controls.Add(btnThem);
        Controls.Add(dgvHangHoa);
        Controls.Add(grpTimKiem);
        Controls.Add(grpThongTin);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý hàng hóa - Tạp hóa UTC2";
        Load += Form1_Load;
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numSoLuongTon).EndInit();
        ((System.ComponentModel.ISupportInitialize)numGiaBan).EndInit();
        grpTimKiem.ResumeLayout(false);
        grpTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private GroupBox grpThongTin;
    private Label lblTenHang;
    private TextBox txtTenHang;
    private Label lblLoaiHang;
    private ComboBox cboLoaiHang;
    private Label lblDonVi;
    private ComboBox cboDonVi;
    private Label lblGiaBan;
    private NumericUpDown numGiaBan;
    private Label lblSoLuongTon;
    private NumericUpDown numSoLuongTon;
    private Label lblNgayNhap;
    private DateTimePicker dtpNgayNhap;
    private Label lblConKinhDoanh;
    private CheckBox chkConKinhDoanh;
    private GroupBox grpTimKiem;
    private TextBox txtTimKiem;
    private ComboBox cboLocLoaiHang;
    private CheckBox chkLocConKinhDoanh;
    private DataGridView dgvHangHoa;
    private DataGridViewTextBoxColumn colMaHang;
    private DataGridViewTextBoxColumn colTenHang;
    private DataGridViewTextBoxColumn colLoaiHang;
    private DataGridViewTextBoxColumn colDonVi;
    private DataGridViewTextBoxColumn colGiaBan;
    private DataGridViewTextBoxColumn colSoLuongTon;
    private DataGridViewTextBoxColumn colNgayNhap;
    private DataGridViewCheckBoxColumn colConKinhDoanh;
    private Button btnThem;
    private Button btnSua;
    private Button btnXoa;
    private Button btnLamMoi;
    private Button btnXemBangGia;
    private Button btnXuatPdf;
    private Button btnXuatExcelLuoi;
    private Button btnXuatExcelTonKho;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel lblTrangThai;
}
