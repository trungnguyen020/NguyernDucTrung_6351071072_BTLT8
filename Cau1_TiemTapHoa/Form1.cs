using System.Data;
using ClosedXML.Excel;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;
using Cau1_TiemTapHoa.Models;

namespace Cau1_TiemTapHoa;

public partial class Form1 : Form
{
    private readonly System.Windows.Forms.Timer _searchTimer = new();
    private List<HangHoa> _allHangHoa = new();
    private int? _selectedMaHang = null;

    public Form1()
    {
        InitializeComponent();
        SetupTimer();
    }

    private void SetupTimer()
    {
        _searchTimer.Interval = 350;
        _searchTimer.Tick += (s, e) =>
        {
            _searchTimer.Stop();
            ApplyFilter();
        };
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            // Cài đặt danh sách Loại hàng & Đơn vị
            cboLoaiHang.Items.AddRange(new object[] { "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" });
            cboDonVi.Items.AddRange(new object[] { "Chai", "Gói", "Hộp", "Túi" });

            cboLocLoaiHang.Items.Add("Tất cả loại");
            cboLocLoaiHang.Items.AddRange(new object[] { "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" });
            cboLocLoaiHang.SelectedIndex = 0;

            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu ban đầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadDataAsync()
    {
        try
        {
            using var db = new TiemTapHoaDbContext();
            _allHangHoa = await db.HangHoas.AsNoTracking().ToListAsync();
            ApplyFilter();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi truy vấn CSDL: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyFilter()
    {
        try
        {
            var query = _allHangHoa.AsEnumerable();

            string keyword = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(h => h.TenHang.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (cboLocLoaiHang.SelectedIndex > 0)
            {
                string loai = cboLocLoaiHang.SelectedItem?.ToString() ?? "";
                query = query.Where(h => h.LoaiHang == loai);
            }

            if (chkLocConKinhDoanh.Checked)
            {
                query = query.Where(h => h.ConKinhDoanh);
            }

            var filteredList = query.ToList();

            // Hiển thị lên DataGridView
            dgvHangHoa.Rows.Clear();
            foreach (var item in filteredList)
            {
                dgvHangHoa.Rows.Add(
                    item.MaHang,
                    item.TenHang,
                    item.LoaiHang,
                    item.DonVi,
                    item.GiaBan,
                    item.SoLuongTon,
                    item.NgayNhap,
                    item.ConKinhDoanh
                );
            }

            lblTrangThai.Text = $"Tìm thấy {filteredList.Count} mặt hàng";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void txtTimKiem_TextChanged(object sender, EventArgs e)
    {
        _searchTimer.Stop();
        _searchTimer.Start();
    }

    private void cboLocLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyFilter();
    }

    private void chkLocConKinhDoanh_CheckedChanged(object sender, EventArgs e)
    {
        ApplyFilter();
    }

    private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvHangHoa.Rows.Count) return;

        var row = dgvHangHoa.Rows[e.RowIndex];
        if (row.Cells["colMaHang"].Value == null) return;

        _selectedMaHang = Convert.ToInt32(row.Cells["colMaHang"].Value);
        txtTenHang.Text = row.Cells["colTenHang"].Value?.ToString() ?? "";
        cboLoaiHang.Text = row.Cells["colLoaiHang"].Value?.ToString() ?? "";
        cboDonVi.Text = row.Cells["colDonVi"].Value?.ToString() ?? "";
        
        if (decimal.TryParse(row.Cells["colGiaBan"].Value?.ToString(), out decimal giaBan))
            numGiaBan.Value = giaBan;
        else
            numGiaBan.Value = 0;

        if (int.TryParse(row.Cells["colSoLuongTon"].Value?.ToString(), out int sl))
            numSoLuongTon.Value = sl;
        else
            numSoLuongTon.Value = 0;

        if (DateTime.TryParse(row.Cells["colNgayNhap"].Value?.ToString(), out DateTime ngayNhap))
            dtpNgayNhap.Value = ngayNhap;
        else
            dtpNgayNhap.Value = DateTime.Now;

        chkConKinhDoanh.Checked = Convert.ToBoolean(row.Cells["colConKinhDoanh"].Value ?? false);
    }

    private void LamMoiForm()
    {
        _selectedMaHang = null;
        txtTenHang.Text = string.Empty;
        cboLoaiHang.SelectedIndex = -1;
        cboDonVi.SelectedIndex = -1;
        numGiaBan.Value = 0;
        numSoLuongTon.Value = 0;
        dtpNgayNhap.Value = DateTime.Now;
        chkConKinhDoanh.Checked = true;
        txtTenHang.Focus();
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        LamMoiForm();
    }

    private async void btnThem_Click(object sender, EventArgs e)
    {
        try
        {
            string tenHang = txtTenHang.Text.Trim();
            if (string.IsNullOrEmpty(tenHang))
            {
                MessageBox.Show("Tên hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHang.Focus();
                return;
            }

            if (numGiaBan.Value <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaBan.Focus();
                return;
            }

            var hang = new HangHoa
            {
                TenHang = tenHang,
                LoaiHang = cboLoaiHang.Text.Trim(),
                DonVi = cboDonVi.Text.Trim(),
                GiaBan = numGiaBan.Value,
                SoLuongTon = (int)numSoLuongTon.Value,
                NgayNhap = dtpNgayNhap.Value.Date,
                ConKinhDoanh = chkConKinhDoanh.Checked
            };

            using var db = new TiemTapHoaDbContext();
            await db.HangHoas.AddAsync(hang);
            await db.SaveChangesAsync();

            MessageBox.Show("Thêm hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi thêm hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSua_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaHang == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng hóa để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenHang = txtTenHang.Text.Trim();
            if (string.IsNullOrEmpty(tenHang))
            {
                MessageBox.Show("Tên hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHang.Focus();
                return;
            }

            if (numGiaBan.Value <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaBan.Focus();
                return;
            }

            using var db = new TiemTapHoaDbContext();
            var hang = await db.HangHoas.FindAsync(_selectedMaHang.Value);
            if (hang == null)
            {
                MessageBox.Show("Không tìm thấy hàng hóa cần sửa trong CSDL!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hang.TenHang = tenHang;
            hang.LoaiHang = cboLoaiHang.Text.Trim();
            hang.DonVi = cboDonVi.Text.Trim();
            hang.GiaBan = numGiaBan.Value;
            hang.SoLuongTon = (int)numSoLuongTon.Value;
            hang.NgayNhap = dtpNgayNhap.Value.Date;
            hang.ConKinhDoanh = chkConKinhDoanh.Checked;

            await db.SaveChangesAsync();

            MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi sửa hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaHang == null)
            {
                MessageBox.Show("Vui lòng chọn một hàng hóa để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa hàng hóa '{txtTenHang.Text}' không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult != DialogResult.Yes) return;

            using var db = new TiemTapHoaDbContext();
            var hang = await db.HangHoas.FindAsync(_selectedMaHang.Value);
            if (hang != null)
            {
                db.HangHoas.Remove(hang);
                await db.SaveChangesAsync();
                MessageBox.Show("Xóa hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                await LoadDataAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xóa hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #region FastReport Bảng giá
    private async Task<Report?> PrepareReportBangGiaAsync()
    {
        using var db = new TiemTapHoaDbContext();
        var data = await db.HangHoas
            .AsNoTracking()
            .Where(h => h.ConKinhDoanh)
            .OrderBy(h => h.MaHang)
            .ToListAsync();

        var dt = new DataTable("HangHoa");
        dt.Columns.Add("TenHang", typeof(string));
        dt.Columns.Add("DonVi", typeof(string));
        dt.Columns.Add("GiaBan", typeof(decimal));

        foreach (var item in data)
        {
            dt.Rows.Add(item.TenHang, item.DonVi ?? "", item.GiaBan ?? 0);
        }

        string reportPath = Path.Combine(Application.StartupPath, "Reports", "BangGia.frx");
        if (!File.Exists(reportPath))
        {
            // Thử tìm trong thư mục gốc dự án
            reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Reports\BangGia.frx");
            reportPath = Path.GetFullPath(reportPath);
        }

        if (!File.Exists(reportPath))
        {
            MessageBox.Show($"Không tìm thấy file mẫu report: {reportPath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        var report = new Report();
        report.Load(reportPath);
        report.RegisterData(dt, "HangHoa");
        report.GetDataSource("HangHoa").Enabled = true;
        report.SetParameterValue("NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
        return report;
    }

    private async void btnXemBangGia_Click(object sender, EventArgs e)
    {
        try
        {
            var report = await PrepareReportBangGiaAsync();
            if (report != null)
            {
                report.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi hiển thị bảng giá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXuatPdf_Click(object sender, EventArgs e)
    {
        try
        {
            var report = await PrepareReportBangGiaAsync();
            if (report == null) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"BangGia_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                report.Prepare();
                var pdfExport = new PDFExport();
                report.Export(pdfExport, sfd.FileName);

                var hoiMo = MessageBox.Show("Xuất file PDF thành công! Bạn có muốn mở file không?",
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
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region Xuất Excel
    private void btnXuatExcelLuoi_Click(object sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"DanhSachHangHoa_Luoi_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("HangHoa");

            // Tiêu đề cột
            ws.Cell(1, 1).Value = "Mã";
            ws.Cell(1, 2).Value = "Tên hàng";
            ws.Cell(1, 3).Value = "Loại hàng";
            ws.Cell(1, 4).Value = "Đơn vị";
            ws.Cell(1, 5).Value = "Giá bán";
            ws.Cell(1, 6).Value = "Tồn";
            ws.Cell(1, 7).Value = "Ngày nhập";
            ws.Cell(1, 8).Value = "Kinh doanh";

            int rowIndex = 2;
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (row.IsNewRow) continue;

                ws.Cell(rowIndex, 1).Value = Convert.ToInt32(row.Cells["colMaHang"].Value);
                ws.Cell(rowIndex, 2).Value = row.Cells["colTenHang"].Value?.ToString() ?? "";
                ws.Cell(rowIndex, 3).Value = row.Cells["colLoaiHang"].Value?.ToString() ?? "";
                ws.Cell(rowIndex, 4).Value = row.Cells["colDonVi"].Value?.ToString() ?? "";
                
                // Số phải là số
                ws.Cell(rowIndex, 5).Value = Convert.ToDouble(row.Cells["colGiaBan"].Value ?? 0);
                ws.Cell(rowIndex, 5).Style.NumberFormat.Format = "#,##0";

                ws.Cell(rowIndex, 6).Value = Convert.ToInt32(row.Cells["colSoLuongTon"].Value ?? 0);
                ws.Cell(rowIndex, 6).Style.NumberFormat.Format = "#,##0";

                if (DateTime.TryParse(row.Cells["colNgayNhap"].Value?.ToString(), out DateTime dt))
                {
                    ws.Cell(rowIndex, 7).Value = dt;
                    ws.Cell(rowIndex, 7).Style.DateFormat.Format = "dd/MM/yyyy";
                }

                ws.Cell(rowIndex, 8).Value = Convert.ToBoolean(row.Cells["colConKinhDoanh"].Value ?? false) ? "Còn bán" : "Ngừng bán";

                rowIndex++;
            }

            var headerRange = ws.Range(1, 1, 1, 8);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E2EFDA");
            ws.Columns().AdjustToContents();

            SaveWorkbookSafely(wb, sfd.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXuatExcelTonKho_Click(object sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"BaoCaoTonKho_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using var db = new TiemTapHoaDbContext();
            var listTonKho = await db.HangHoas
                .AsNoTracking()
                .Where(h => h.ConKinhDoanh)
                .OrderBy(h => h.MaHang)
                .ToListAsync();

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("TonKho");

            // Tiêu đề
            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Tên hàng";
            ws.Cell(1, 3).Value = "Đơn vị";
            ws.Cell(1, 4).Value = "Giá bán";
            ws.Cell(1, 5).Value = "Số lượng tồn";
            ws.Cell(1, 6).Value = "Giá trị tồn";

            int rowIndex = 2;
            int stt = 1;
            foreach (var item in listTonKho)
            {
                ws.Cell(rowIndex, 1).Value = stt++;
                ws.Cell(rowIndex, 2).Value = item.TenHang;
                ws.Cell(rowIndex, 3).Value = item.DonVi ?? "";

                ws.Cell(rowIndex, 4).Value = Convert.ToDouble(item.GiaBan ?? 0);
                ws.Cell(rowIndex, 4).Style.NumberFormat.Format = "#,##0";
                ws.Cell(rowIndex, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Cell(rowIndex, 5).Value = Convert.ToInt32(item.SoLuongTon ?? 0);
                ws.Cell(rowIndex, 5).Style.NumberFormat.Format = "#,##0";
                ws.Cell(rowIndex, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                // Công thức Giá trị tồn = Giá bán * Số lượng tồn
                ws.Cell(rowIndex, 6).FormulaA1 = $"D{rowIndex}*E{rowIndex}";
                ws.Cell(rowIndex, 6).Style.NumberFormat.Format = "#,##0";
                ws.Cell(rowIndex, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                rowIndex++;
            }

            // Dòng Tổng
            ws.Cell(rowIndex, 1).Value = "Tổng";
            ws.Range(rowIndex, 1, rowIndex, 5).Merge();
            ws.Cell(rowIndex, 1).Style.Font.Bold = true;
            ws.Cell(rowIndex, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Cell(rowIndex, 6).FormulaA1 = $"SUM(F2:F{rowIndex - 1})";
            ws.Cell(rowIndex, 6).Style.Font.Bold = true;
            ws.Cell(rowIndex, 6).Style.NumberFormat.Format = "#,##0";
            ws.Cell(rowIndex, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            // Định dạng Header: In đậm, AutoFilter, Freeze
            var headerRange = ws.Range(1, 1, 1, 6);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E1F2");
            ws.Range(1, 1, rowIndex - 1, 6).SetAutoFilter();
            ws.SheetView.FreezeRows(1);

            ws.Columns().AdjustToContents();

            SaveWorkbookSafely(wb, sfd.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất Excel tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveWorkbookSafely(XLWorkbook wb, string filePath)
    {
        try
        {
            wb.SaveAs(filePath);
            var hoiMo = MessageBox.Show("Xuất file Excel thành công! Bạn có muốn mở file ngay không?",
                "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (hoiMo == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
        }
        catch (IOException)
        {
            MessageBox.Show("File Excel đang được mở bởi một ứng dụng khác. Vui lòng đóng file rồi thử lại!",
                "Thông báo trùng file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    #endregion
}
