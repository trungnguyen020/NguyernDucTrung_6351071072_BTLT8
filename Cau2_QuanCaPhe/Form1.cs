using System.Data;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;
using Cau2_QuanCaPhe.Models;

namespace Cau2_QuanCaPhe;

public partial class Form1 : Form
{
    private List<Mon> _allMon = new();
    private int? _selectedMaHD = null;
    private int? _selectedMaCT = null;

    public Form1()
    {
        InitializeComponent();
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            // Cài đặt ComboBox Số bàn nhập liệu 1-10
            for (int i = 1; i <= 10; i++)
            {
                cboSoBan.Items.Add(i);
            }
            cboSoBan.SelectedIndex = 0;

            // ComboBox lọc số bàn: Tất cả, 1..10
            cboLocSoBan.Items.Add("Tất cả");
            for (int i = 1; i <= 10; i++)
            {
                cboLocSoBan.Items.Add(i.ToString());
            }
            cboLocSoBan.SelectedIndex = 0;

            // Hình thức lọc
            cboLocHinhThuc.Items.AddRange(new object[] { "Tất cả", "Tiền mặt", "Chuyển khoản" });
            cboLocHinhThuc.SelectedIndex = 0;

            // Khoảng ngày mặc định
            dtpTuNgay.Value = DateTime.Today.AddDays(-7);
            dtpDenNgay.Value = DateTime.Today.AddDays(1);

            dtpNgayLap.Value = DateTime.Now;

            // Tải danh mục món
            await LoadDanhMucMonAsync();

            // Tải danh sách hóa đơn
            await LoadDanhSachHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadDanhMucMonAsync()
    {
        using var db = new QuanCaPheDbContext();
        _allMon = await db.Mons.AsNoTracking().OrderBy(m => m.MaMon).ToListAsync();

        cboMon.DisplayMember = "DisplayText";
        cboMon.ValueMember = "MaMon";
        cboMon.DataSource = _allMon.Select(m => new
        {
            m.MaMon,
            DisplayText = $"{m.TenMon} ({m.DonGia:N0} đ)"
        }).ToList();
    }

    private async Task LoadDanhSachHoaDonAsync()
    {
        try
        {
            using var db = new QuanCaPheDbContext();

            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            var query = db.HoaDons
                .AsNoTracking()
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.MaMonNavigation)
                .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay);

            // Lọc theo số bàn
            if (cboLocSoBan.SelectedIndex > 0)
            {
                if (int.TryParse(cboLocSoBan.SelectedItem?.ToString(), out int ban))
                {
                    query = query.Where(h => h.SoBan == ban);
                }
            }

            // Lọc hình thức thanh toán
            if (cboLocHinhThuc.SelectedIndex > 0)
            {
                string ht = cboLocHinhThuc.SelectedItem?.ToString() ?? "";
                query = query.Where(h => h.HinhThucTt == ht);
            }

            // Lọc theo tên món
            string keywordMon = txtLocTenMon.Text.Trim();
            if (!string.IsNullOrEmpty(keywordMon))
            {
                query = query.Where(h => h.ChiTietHoaDons.Any(ct => ct.MaMonNavigation.TenMon.Contains(keywordMon)));
            }

            var list = await query
                .OrderByDescending(h => h.NgayLap)
                .Select(h => new
                {
                    h.MaHd,
                    MaHDFormatted = $"HD{h.MaHd:D6}",
                    h.NgayLap,
                    h.SoBan,
                    h.HinhThucTt,
                    h.GhiChu,
                    TongTien = h.ChiTietHoaDons.Sum(ct => ct.SoLuong * ct.DonGia)
                })
                .ToListAsync();

            dgvHoaDon.Rows.Clear();
            decimal tongDoanhThu = 0;

            foreach (var item in list)
            {
                tongDoanhThu += item.TongTien;
                dgvHoaDon.Rows.Add(
                    item.MaHd,
                    item.MaHDFormatted,
                    item.NgayLap,
                    item.SoBan,
                    item.HinhThucTt,
                    item.TongTien,
                    item.GhiChu
                );
            }

            lblTongDoanhThu.Text = $"Tổng doanh thu đang hiển thị: {tongDoanhThu:N0} đ";

            // Chọn hóa đơn đầu tiên nếu có
            if (dgvHoaDon.Rows.Count > 0)
            {
                if (_selectedMaHD == null || !list.Any(x => x.MaHd == _selectedMaHD.Value))
                {
                    dgvHoaDon.Rows[0].Selected = true;
                    await LoadChiTietHoaDonAsync(list[0].MaHd);
                }
                else
                {
                    await LoadChiTietHoaDonAsync(_selectedMaHD.Value);
                }
            }
            else
            {
                ClearChiTiet();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadChiTietHoaDonAsync(int maHD)
    {
        try
        {
            _selectedMaHD = maHD;
            grpChiTiet.Text = $"Chi tiết hóa đơn HD{maHD:D6}";

            using var db = new QuanCaPheDbContext();
            var hd = await db.HoaDons
                .AsNoTracking()
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.MaMonNavigation)
                .FirstOrDefaultAsync(h => h.MaHd == maHD);

            if (hd == null) return;

            // Đổ lên controls hóa đơn
            dtpNgayLap.Value = hd.NgayLap;
            cboSoBan.SelectedItem = hd.SoBan;
            if (hd.HinhThucTt == "Chuyển khoản")
                radChuyenKhoan.Checked = true;
            else
                radTienMat.Checked = true;
            txtGhiChu.Text = hd.GhiChu ?? "";

            // Đổ lên lưới chi tiết
            dgvChiTiet.Rows.Clear();
            int stt = 1;
            decimal tongCong = 0;

            foreach (var ct in hd.ChiTietHoaDons.OrderBy(x => x.MaCt))
            {
                decimal thanhTien = ct.SoLuong * ct.DonGia;
                tongCong += thanhTien;
                dgvChiTiet.Rows.Add(
                    ct.MaCt,
                    stt++,
                    ct.MaMonNavigation?.TenMon ?? "",
                    ct.SoLuong,
                    ct.DonGia,
                    thanhTien,
                    ct.MaMon
                );
            }

            lblTongCong.Text = $"Tổng cộng: {tongCong:N0} đ";
            _selectedMaCT = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearChiTiet()
    {
        _selectedMaHD = null;
        _selectedMaCT = null;
        grpChiTiet.Text = "Chi tiết hóa đơn";
        dgvChiTiet.Rows.Clear();
        lblTongCong.Text = "Tổng cộng: 0 đ";
        txtGhiChu.Text = string.Empty;
    }

    private async void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvHoaDon.Rows.Count) return;
        var row = dgvHoaDon.Rows[e.RowIndex];
        if (row.Cells["colMaHD"].Value == null) return;

        int maHD = Convert.ToInt32(row.Cells["colMaHD"].Value);
        await LoadChiTietHoaDonAsync(maHD);
    }

    private void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvChiTiet.Rows.Count) return;
        var row = dgvChiTiet.Rows[e.RowIndex];
        if (row.Cells["colMaCT"].Value == null) return;

        _selectedMaCT = Convert.ToInt32(row.Cells["colMaCT"].Value);
        numSoLuong.Value = Convert.ToInt32(row.Cells["colSL"].Value ?? 1);

        int maMon = Convert.ToInt32(row.Cells["colMaMonVal"].Value ?? 0);
        cboMon.SelectedValue = maMon;
    }

    private async void btnTimKiem_Click(object sender, EventArgs e)
    {
        await LoadDanhSachHoaDonAsync();
    }

    #region Thao tác Hóa đơn
    private async void btnHoaDonMoi_Click(object sender, EventArgs e)
    {
        try
        {
            int soBan = cboSoBan.SelectedItem != null ? Convert.ToInt32(cboSoBan.SelectedItem) : 1;
            string hinhThuc = radChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
            string ghiChu = txtGhiChu.Text.Trim();

            var hd = new HoaDon
            {
                NgayLap = dtpNgayLap.Value,
                SoBan = soBan,
                HinhThucTt = hinhThuc,
                GhiChu = string.IsNullOrEmpty(ghiChu) ? null : ghiChu
            };

            using var db = new QuanCaPheDbContext();
            await db.HoaDons.AddAsync(hd);
            await db.SaveChangesAsync();

            _selectedMaHD = hd.MaHd;
            MessageBox.Show($"Đã tạo mới hóa đơn HD{hd.MaHd:D6}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            await LoadDanhSachHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tạo hóa đơn mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSuaHoaDon_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaHD == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var db = new QuanCaPheDbContext();
            var hd = await db.HoaDons.FindAsync(_selectedMaHD.Value);
            if (hd == null) return;

            hd.NgayLap = dtpNgayLap.Value;
            hd.SoBan = cboSoBan.SelectedItem != null ? Convert.ToInt32(cboSoBan.SelectedItem) : 1;
            hd.HinhThucTt = radChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
            hd.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

            await db.SaveChangesAsync();
            MessageBox.Show($"Cập nhật thông tin hóa đơn HD{hd.MaHd:D6} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            await LoadDanhSachHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi sửa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoaHoaDon_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaHD == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa hóa đơn HD{_selectedMaHD.Value:D6} cùng toàn bộ các món chi tiết của nó không?",
                "Xác nhận xóa hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog != DialogResult.Yes) return;

            using var db = new QuanCaPheDbContext();
            var hd = await db.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .FirstOrDefaultAsync(h => h.MaHd == _selectedMaHD.Value);

            if (hd != null)
            {
                // Xóa các dòng chi tiết trước
                db.ChiTietHoaDons.RemoveRange(hd.ChiTietHoaDons);
                db.HoaDons.Remove(hd);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedMaHD = null;
                await LoadDanhSachHoaDonAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region Thao tác Món trong hóa đơn
    private async void btnThemMon_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaHD == null)
            {
                MessageBox.Show("Vui lòng chọn hoặc tạo hóa đơn trước khi thêm món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboMon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMon = Convert.ToInt32(cboMon.SelectedValue);
            int sl = (int)numSoLuong.Value;

            using var db = new QuanCaPheDbContext();
            var mon = await db.Mons.FindAsync(maMon);
            if (mon == null) return;

            // Kiểm tra xem món này đã có trong hóa đơn chưa
            var existing = await db.ChiTietHoaDons
                .FirstOrDefaultAsync(ct => ct.MaHd == _selectedMaHD.Value && ct.MaMon == maMon);

            if (existing != null)
            {
                existing.SoLuong += sl;
            }
            else
            {
                var ct = new ChiTietHoaDon
                {
                    MaHd = _selectedMaHD.Value,
                    MaMon = maMon,
                    SoLuong = sl,
                    DonGia = mon.DonGia
                };
                await db.ChiTietHoaDons.AddAsync(ct);
            }

            await db.SaveChangesAsync();
            await LoadChiTietHoaDonAsync(_selectedMaHD.Value);
            await LoadDanhSachHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi thêm món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSuaSL_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaCT == null)
            {
                MessageBox.Show("Vui lòng chọn món trong bảng chi tiết để sửa số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var db = new QuanCaPheDbContext();
            var ct = await db.ChiTietHoaDons.FindAsync(_selectedMaCT.Value);
            if (ct == null) return;

            ct.SoLuong = (int)numSoLuong.Value;
            await db.SaveChangesAsync();

            await LoadChiTietHoaDonAsync(ct.MaHd);
            await LoadDanhSachHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi cập nhật số lượng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoaMon_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedMaCT == null)
            {
                MessageBox.Show("Vui lòng chọn món trong bảng chi tiết để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var db = new QuanCaPheDbContext();
            var ct = await db.ChiTietHoaDons.FindAsync(_selectedMaCT.Value);
            if (ct == null) return;

            int maHD = ct.MaHd;
            db.ChiTietHoaDons.Remove(ct);
            await db.SaveChangesAsync();

            await LoadChiTietHoaDonAsync(maHD);
            await LoadDanhSachHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xóa món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region FastReport Hóa đơn
    private async Task<Report?> PrepareReportHoaDonAsync()
    {
        if (_selectedMaHD == null)
        {
            MessageBox.Show("Chưa chọn hóa đơn để xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        using var db = new QuanCaPheDbContext();
        var hd = await db.HoaDons
            .AsNoTracking()
            .Include(h => h.ChiTietHoaDons)
            .ThenInclude(ct => ct.MaMonNavigation)
            .FirstOrDefaultAsync(h => h.MaHd == _selectedMaHD.Value);

        if (hd == null || hd.ChiTietHoaDons.Count == 0)
        {
            MessageBox.Show("Hóa đơn chưa có món nào! Không thể in trang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        var listChiTiet = new List<ChiTietIn>();
        int stt = 1;
        foreach (var ct in hd.ChiTietHoaDons.OrderBy(x => x.MaCt))
        {
            listChiTiet.Add(new ChiTietIn
            {
                STT = stt++,
                TenMon = ct.MaMonNavigation?.TenMon ?? "",
                SoLuong = ct.SoLuong,
                DonGia = ct.DonGia,
                ThanhTien = ct.SoLuong * ct.DonGia
            });
        }

        string reportPath = Path.Combine(Application.StartupPath, "Reports", "HoaDon.frx");
        if (!File.Exists(reportPath))
        {
            reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Reports\HoaDon.frx");
            reportPath = Path.GetFullPath(reportPath);
        }

        if (!File.Exists(reportPath))
        {
            MessageBox.Show($"Không tìm thấy file mẫu report: {reportPath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        var report = new Report();
        report.Load(reportPath);
        report.RegisterData(listChiTiet, "ChiTiet");
        report.GetDataSource("ChiTiet").Enabled = true;

        // Truyền các tham số
        report.SetParameterValue("TenQuan", "QUÁN CÀ PHÊ GÓC PHỐ");
        report.SetParameterValue("SoHoaDon", $"HD{hd.MaHd:D6}");
        report.SetParameterValue("NgayLap", hd.NgayLap.ToString("dd/MM/yyyy HH:mm"));
        report.SetParameterValue("SoBan", hd.SoBan.ToString());
        report.SetParameterValue("HinhThucTT", hd.HinhThucTt);

        return report;
    }

    private async void btnXemHoaDon_Click(object sender, EventArgs e)
    {
        try
        {
            var report = await PrepareReportHoaDonAsync();
            if (report != null)
            {
                report.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi hiển thị hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXuatPdf_Click(object sender, EventArgs e)
    {
        try
        {
            var report = await PrepareReportHoaDonAsync();
            if (report == null) return;

            string defaultFileName = $"HoaDon_HD{_selectedMaHD!.Value:D6}.pdf";
            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = defaultFileName
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

    private async void btnIn_Click(object sender, EventArgs e)
    {
        try
        {
            var report = await PrepareReportHoaDonAsync();
            if (report == null) return;

            report.Print();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi in hóa đơn: {ex.Message}", "Lỗi in", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion
}
