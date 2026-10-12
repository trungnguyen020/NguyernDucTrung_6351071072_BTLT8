using System.Data;
using System.Drawing.Printing;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Cau3_QuanLyDiem.Models;

namespace Cau3_QuanLyDiem;

public partial class Form1 : Form
{
    private readonly PhieuDiemPrinter _printer = new();
    private List<Lop> _allLop = new();
    private List<SinhVien> _allSinhVien = new();
    private string? _selectedMaSV = null;
    private int? _selectedMaDiem = null;

    public Form1()
    {
        InitializeComponent();
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            await LoadLopAsync();
            await LoadSinhVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi nạp dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadLopAsync()
    {
        using var db = new QuanLyDiemDbContext();
        _allLop = await db.Lops.AsNoTracking().ToListAsync();

        // Nạp ListBox Lớp
        lstLop.Items.Clear();
        lstLop.Items.Add("Tất cả");
        foreach (var l in _allLop)
        {
            lstLop.Items.Add($"{l.MaLop} - {l.TenLop}");
        }
        lstLop.SelectedIndex = 0;

        // Nạp ComboBox Lớp (ở GroupBox thông tin)
        cboLop.DataSource = _allLop.ToList();
        cboLop.DisplayMember = "MaLop";
        cboLop.ValueMember = "MaLop";
    }

    private async Task LoadSinhVienAsync()
    {
        using var db = new QuanLyDiemDbContext();
        _allSinhVien = await db.SinhViens
            .AsNoTracking()
            .Include(s => s.MaLopNavigation)
            .Include(s => s.DiemMons)
            .ToListAsync();

        ApplyFilterSinhVien();
    }

    private void ApplyFilterSinhVien()
    {
        var query = _allSinhVien.AsEnumerable();

        // Lọc theo Lớp
        if (lstLop.SelectedIndex > 0)
        {
            string selectedText = lstLop.SelectedItem?.ToString() ?? "";
            string maLop = selectedText.Split(" - ")[0].Trim();
            query = query.Where(s => s.MaLop == maLop);
        }

        // Lọc theo tìm kiếm MSSV hoặc họ tên (không phân biệt hoa thường)
        string search = txtTimKiem.Text.Trim();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.MaSv.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                     s.HoTen.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        var list = query.OrderBy(s => s.MaSv).ToList();

        dgvSinhVien.Rows.Clear();
        foreach (var sv in list)
        {
            dgvSinhVien.Rows.Add(
                sv.MaSv,
                sv.HoTen,
                sv.NgaySinh,
                sv.GioiTinh,
                sv.MaLop
            );
        }

        if (dgvSinhVien.Rows.Count > 0)
        {
            if (_selectedMaSV == null || !list.Any(x => x.MaSv == _selectedMaSV))
            {
                dgvSinhVien.Rows[0].Selected = true;
                SelectSinhVien(list[0].MaSv);
            }
            else
            {
                SelectSinhVien(_selectedMaSV);
            }
        }
        else
        {
            ClearThongTinSinhVien();
            ClearDiemSinhVien();
        }
    }

    private void SelectSinhVien(string maSV)
    {
        _selectedMaSV = maSV;
        var sv = _allSinhVien.FirstOrDefault(s => s.MaSv == maSV);
        if (sv == null) return;

        // Điền vào GroupBox Thông tin sinh viên
        txtMSSV.Text = sv.MaSv;
        txtHoTen.Text = sv.HoTen;
        dtpNgaySinh.Value = sv.NgaySinh;
        if (sv.GioiTinh == "Nữ") radNu.Checked = true;
        else radNam.Checked = true;
        cboLop.SelectedValue = sv.MaLop;

        // Load bảng điểm của sinh viên này
        LoadBangDiemSinhVien(sv);
    }

    private void LoadBangDiemSinhVien(SinhVien sv)
    {
        dgvDiem.Rows.Clear();
        _selectedMaDiem = null;

        var diemList = sv.DiemMons.OrderBy(d => d.MaDiem).ToList();
        int stt = 1;
        int tongTC = 0;
        decimal tongTich = 0;

        foreach (var d in diemList)
        {
            int tc = d.SoTinChi ?? 0;
            decimal diem = d.Diem ?? 0;

            tongTC += tc;
            tongTich += diem * tc;

            dgvDiem.Rows.Add(
                d.MaDiem,
                stt++,
                d.TenMon,
                tc,
                diem
            );
        }

        // Tính điểm trung bình và xếp loại
        if (tongTC > 0)
        {
            decimal dtb = Math.Round(tongTich / tongTC, 2);
            string xepLoai = "";
            if (dtb >= 8.0m) xepLoai = "Giỏi";
            else if (dtb >= 7.0m) xepLoai = "Khá";
            else if (dtb >= 5.0m) xepLoai = "Trung bình";
            else xepLoai = "Yếu";

            lblTongTinChi.Text = $"Tổng tín chỉ: {tongTC}";
            lblDiemTB.Text = $"Điểm TB: {dtb:0.00}";
            lblXepLoai.Text = $"Xếp loại: {xepLoai}";
        }
        else
        {
            lblTongTinChi.Text = "Tổng tín chỉ: 0";
            lblDiemTB.Text = "Điểm TB: Chưa có điểm";
            lblXepLoai.Text = "Xếp loại: Chưa có điểm";
        }
    }

    private void ClearThongTinSinhVien()
    {
        _selectedMaSV = null;
        txtMSSV.Text = string.Empty;
        txtHoTen.Text = string.Empty;
        dtpNgaySinh.Value = DateTime.Now;
        radNam.Checked = true;
        cboLop.SelectedIndex = -1;
    }

    private void ClearDiemSinhVien()
    {
        _selectedMaDiem = null;
        dgvDiem.Rows.Clear();
        txtTenMon.Text = string.Empty;
        numSoTinChi.Value = 3;
        numDiem.Value = 0;
        lblTongTinChi.Text = "Tổng tín chỉ: 0";
        lblDiemTB.Text = "Điểm TB: Chưa có điểm";
        lblXepLoai.Text = "Xếp loại: Chưa có điểm";
    }

    private void lstLop_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyFilterSinhVien();
    }

    private void txtTimKiem_TextChanged(object sender, EventArgs e)
    {
        ApplyFilterSinhVien();
    }

    private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvSinhVien.Rows.Count) return;
        var row = dgvSinhVien.Rows[e.RowIndex];
        string? maSV = row.Cells["colMaSV"].Value?.ToString();
        if (!string.IsNullOrEmpty(maSV))
        {
            SelectSinhVien(maSV);
        }
    }

    private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvDiem.Rows.Count) return;
        var row = dgvDiem.Rows[e.RowIndex];
        if (row.Cells["colMaDiem"].Value == null) return;

        _selectedMaDiem = Convert.ToInt32(row.Cells["colMaDiem"].Value);
        txtTenMon.Text = row.Cells["colMonHoc"].Value?.ToString() ?? "";
        numSoTinChi.Value = Convert.ToInt32(row.Cells["colSoTC"].Value ?? 3);
        numDiem.Value = Convert.ToDecimal(row.Cells["colDiem"].Value ?? 0);
    }

    #region Thao tác Sinh viên (Thêm, Sửa, Xóa)
    private async void btnThemSV_Click(object sender, EventArgs e)
    {
        try
        {
            string maSV = txtMSSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();

            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("MSSV không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMSSV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (cboLop.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new QuanLyDiemDbContext();
            // Kiểm tra trùng MSSV
            var exists = await db.SinhViens.AnyAsync(s => s.MaSv == maSV);
            if (exists)
            {
                MessageBox.Show($"MSSV '{maSV}' đã tồn tại trong hệ thống!", "Cảnh báo trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMSSV.Focus();
                return;
            }

            var sv = new SinhVien
            {
                MaSv = maSV,
                HoTen = hoTen,
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = radNu.Checked ? "Nữ" : "Nam",
                MaLop = cboLop.SelectedValue.ToString()!
            };

            await db.SinhViens.AddAsync(sv);
            await db.SaveChangesAsync();

            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _selectedMaSV = maSV;
            await LoadSinhVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi thêm sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSuaSV_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(_selectedMaSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            using var db = new QuanLyDiemDbContext();
            var sv = await db.SinhViens.FindAsync(_selectedMaSV);
            if (sv == null) return;

            sv.HoTen = hoTen;
            sv.NgaySinh = dtpNgaySinh.Value.Date;
            sv.GioiTinh = radNu.Checked ? "Nữ" : "Nam";
            sv.MaLop = cboLop.SelectedValue?.ToString() ?? sv.MaLop;

            await db.SaveChangesAsync();
            MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadSinhVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi sửa sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoaSV_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(_selectedMaSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên '{txtHoTen.Text}' ({_selectedMaSV}) cùng toàn bộ bảng điểm không?",
                "Xác nhận xóa sinh viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog != DialogResult.Yes) return;

            using var db = new QuanLyDiemDbContext();
            var sv = await db.SinhViens.Include(s => s.DiemMons).FirstOrDefaultAsync(s => s.MaSv == _selectedMaSV);
            if (sv != null)
            {
                // Xóa các dòng điểm của sinh viên đó trước
                db.DiemMons.RemoveRange(sv.DiemMons);
                db.SinhViens.Remove(sv);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedMaSV = null;
                await LoadSinhVienAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xóa sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region Thao tác Điểm môn (Thêm môn, Sửa điểm, Xóa môn)
    private async void btnThemMon_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(_selectedMaSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước khi thêm điểm môn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên môn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            using var db = new QuanLyDiemDbContext();
            var dm = new DiemMon
            {
                MaSv = _selectedMaSV,
                TenMon = tenMon,
                SoTinChi = (int)numSoTinChi.Value,
                Diem = numDiem.Value
            };

            await db.DiemMons.AddAsync(dm);
            await db.SaveChangesAsync();

            MessageBox.Show("Thêm môn học và điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtTenMon.Text = string.Empty;
            await LoadSinhVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi thêm môn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSuaDiem_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaDiem == null)
            {
                MessageBox.Show("Vui lòng chọn môn cần sửa trong bảng điểm phía dưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên môn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            using var db = new QuanLyDiemDbContext();
            var dm = await db.DiemMons.FindAsync(_selectedMaDiem.Value);
            if (dm == null) return;

            dm.TenMon = tenMon;
            dm.SoTinChi = (int)numSoTinChi.Value;
            dm.Diem = numDiem.Value;

            await db.SaveChangesAsync();
            MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadSinhVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi sửa điểm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoaMon_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaDiem == null)
            {
                MessageBox.Show("Vui lòng chọn môn cần xóa trong bảng điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa môn '{txtTenMon.Text}' khỏi bảng điểm?",
                "Xác nhận xóa môn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog != DialogResult.Yes) return;

            using var db = new QuanLyDiemDbContext();
            var dm = await db.DiemMons.FindAsync(_selectedMaDiem.Value);
            if (dm != null)
            {
                db.DiemMons.Remove(dm);
                await db.SaveChangesAsync();
                MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedMaDiem = null;
                txtTenMon.Text = string.Empty;
                await LoadSinhVienAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xóa môn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region In Phiếu Báo Điểm bằng PrintDocument
    private bool PreparePhieuDiem()
    {
        if (string.IsNullOrEmpty(_selectedMaSV))
        {
            MessageBox.Show("Vui lòng chọn sinh viên để in phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        var sv = _allSinhVien.FirstOrDefault(s => s.MaSv == _selectedMaSV);
        if (sv == null || sv.DiemMons.Count == 0)
        {
            MessageBox.Show("Sinh viên chưa có điểm! Không thể in phiếu báo điểm trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        var diemList = sv.DiemMons.OrderBy(d => d.MaDiem).ToList();
        int tongTC = 0;
        decimal tongTich = 0;

        foreach (var d in diemList)
        {
            int tc = d.SoTinChi ?? 0;
            decimal diem = d.Diem ?? 0;
            tongTC += tc;
            tongTich += diem * tc;
        }

        decimal dtb = tongTC > 0 ? Math.Round(tongTich / tongTC, 2) : 0;
        string xepLoai = "";
        if (dtb >= 8.0m) xepLoai = "Giỏi";
        else if (dtb >= 7.0m) xepLoai = "Khá";
        else if (dtb >= 5.0m) xepLoai = "Trung bình";
        else xepLoai = "Yếu";

        _printer.SetupData(sv, diemList, dtb, xepLoai, tongTC);
        return true;
    }

    private void btnXemTruocPhieu_Click(object sender, EventArgs e)
    {
        try
        {
            if (!PreparePhieuDiem()) return;

            using var preview = new PrintPreviewDialog
            {
                Document = _printer.Document,
                Width = 900,
                Height = 700,
                StartPosition = FormStartPosition.CenterScreen
            };
            preview.ShowDialog();
        }
        catch (InvalidPrinterException)
        {
            MessageBox.Show("Không tìm thấy máy in nào được cài đặt trên hệ thống! Vui lòng cài đặt một máy in (ví dụ: Microsoft Print to PDF) để tiếp tục.",
                "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xem trước phiếu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnInPhieu_Click(object sender, EventArgs e)
    {
        try
        {
            if (!PreparePhieuDiem()) return;

            using var printDlg = new PrintDialog
            {
                Document = _printer.Document,
                UseEXDialog = true
            };

            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                _printer.Document.Print();
            }
        }
        catch (InvalidPrinterException)
        {
            MessageBox.Show("Không tìm thấy máy in nào được cài đặt trên hệ thống!", "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi in phiếu: {ex.Message}", "Lỗi in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCaiDatTrang_Click(object sender, EventArgs e)
    {
        try
        {
            using var pageSetup = new PageSetupDialog
            {
                Document = _printer.Document,
                EnableMetric = true
            };

            pageSetup.ShowDialog();
        }
        catch (InvalidPrinterException)
        {
            MessageBox.Show("Không tìm thấy máy in nào được cài đặt trên hệ thống để cài đặt trang!", "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi cài đặt trang: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region Xuất Excel điểm theo lớp
    private async void btnXuatExcelLop_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstLop.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp cụ thể từ danh sách bên trái để xuất điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedText = lstLop.SelectedItem?.ToString() ?? "";
            string maLop = selectedText.Split(" - ")[0].Trim();

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"BangDiemLop_{maLop}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using var db = new QuanLyDiemDbContext();
            // Truy vấn nối 3 bảng bằng EF Core cho lớp đang chọn
            var data = await (from dm in db.DiemMons
                              join sv in db.SinhViens on dm.MaSv equals sv.MaSv
                              join l in db.Lops on sv.MaLop equals l.MaLop
                              where l.MaLop == maLop
                              orderby sv.MaSv, dm.MaDiem
                              select new
                              {
                                  sv.MaSv,
                                  sv.HoTen,
                                  sv.MaLop,
                                  dm.TenMon,
                                  dm.SoTinChi,
                                  dm.Diem
                              }).ToListAsync();

            if (data.Count == 0)
            {
                MessageBox.Show($"Lớp {maLop} chưa có dữ liệu điểm để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add($"Diem_{maLop}");

            // Tiêu đề
            ws.Cell(1, 1).Value = "MSSV";
            ws.Cell(1, 2).Value = "Họ tên";
            ws.Cell(1, 3).Value = "Lớp";
            ws.Cell(1, 4).Value = "Môn học";
            ws.Cell(1, 5).Value = "Số TC";
            ws.Cell(1, 6).Value = "Điểm";

            int rowIndex = 2;
            foreach (var item in data)
            {
                ws.Cell(rowIndex, 1).Value = item.MaSv;
                ws.Cell(rowIndex, 2).Value = item.HoTen;
                ws.Cell(rowIndex, 3).Value = item.MaLop;
                ws.Cell(rowIndex, 4).Value = item.TenMon;
                
                ws.Cell(rowIndex, 5).Value = item.SoTinChi ?? 0;
                ws.Cell(rowIndex, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(rowIndex, 6).Value = Convert.ToDouble(item.Diem ?? 0);
                ws.Cell(rowIndex, 6).Style.NumberFormat.Format = "0.00";
                ws.Cell(rowIndex, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                rowIndex++;
            }

            // Dòng Tổng số tín chỉ: SUM cột Số TC
            int sumRow = rowIndex;
            ws.Cell(sumRow, 4).Value = "Tổng số tín chỉ";
            ws.Cell(sumRow, 4).Style.Font.Bold = true;
            ws.Cell(sumRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(sumRow, 5).FormulaA1 = $"SUM(E2:E{sumRow - 1})";
            ws.Cell(sumRow, 5).Style.Font.Bold = true;
            ws.Cell(sumRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Dòng Điểm trung bình: AVERAGE cột Điểm
            int avgRow = rowIndex + 1;
            ws.Cell(avgRow, 4).Value = "Điểm trung bình";
            ws.Cell(avgRow, 4).Style.Font.Bold = true;
            ws.Cell(avgRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(avgRow, 6).FormulaA1 = $"AVERAGE(F2:F{sumRow - 1})";
            ws.Cell(avgRow, 6).Style.Font.Bold = true;
            ws.Cell(avgRow, 6).Style.NumberFormat.Format = "0.00";
            ws.Cell(avgRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            // Format Header
            var header = ws.Range(1, 1, 1, 6);
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.FromHtml("#BDD7EE");

            ws.Columns().AdjustToContents();

            try
            {
                wb.SaveAs(sfd.FileName);
                var hoiMo = MessageBox.Show("Xuất file Excel điểm lớp thành công! Bạn có muốn mở file không?",
                    "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (hoiMo == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (IOException)
            {
                MessageBox.Show("File Excel đang được mở bởi một chương trình khác. Hãy đóng file rồi thử lại!",
                    "Trùng file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion
}
