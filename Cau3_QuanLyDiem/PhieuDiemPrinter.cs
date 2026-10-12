using System.Drawing.Printing;
using Cau3_QuanLyDiem.Models;

namespace Cau3_QuanLyDiem;

public class PhieuDiemPrinter
{
    public PrintDocument Document { get; }
    private SinhVien? _sinhVien;
    private List<DiemMon> _diemMons = new();
    private decimal _diemTB = 0;
    private string _xepLoai = "";
    private int _tongTinChi = 0;

    private int _currentIndex = 0;
    private int _currentPage = 0;
    private int _totalPages = 1;

    public PhieuDiemPrinter()
    {
        Document = new PrintDocument();
        Document.BeginPrint += Document_BeginPrint;
        Document.PrintPage += Document_PrintPage;
    }

    public void SetupData(SinhVien sv, List<DiemMon> diemList, decimal diemTB, string xepLoai, int tongTinChi)
    {
        _sinhVien = sv;
        _diemMons = diemList ?? new List<DiemMon>();
        _diemTB = diemTB;
        _xepLoai = xepLoai;
        _tongTinChi = tongTinChi;
    }

    private void Document_BeginPrint(object sender, PrintEventArgs e)
    {
        _currentIndex = 0;
        _currentPage = 0;

        // Tính trước tổng số trang dự kiến
        // Giả sử trang 1 chứa khoảng 18 dòng (do có phần header lớn), các trang tiếp theo chứa khoảng 25 dòng
        int remaining = _diemMons.Count;
        if (remaining <= 18)
        {
            _totalPages = 1;
        }
        else
        {
            remaining -= 18;
            _totalPages = 1 + (int)Math.Ceiling(remaining / 25.0);
        }
    }

    private void Document_PrintPage(object sender, PrintPageEventArgs e)
    {
        _currentPage++;
        var g = e.Graphics!;
        var bounds = e.MarginBounds; // Tọa độ tính theo MarginBounds (không hardcode)

        using var fontTieuDeNho = new Font("Arial", 11, FontStyle.Bold);
        using var fontTieuDeLon = new Font("Arial", 16, FontStyle.Bold);
        using var fontInfo = new Font("Arial", 10, FontStyle.Regular);
        using var fontInfoBold = new Font("Arial", 10, FontStyle.Bold);
        using var fontTableH = new Font("Arial", 10, FontStyle.Bold);
        using var fontTableB = new Font("Arial", 10, FontStyle.Regular);
        using var fontItalic = new Font("Arial", 9.5f, FontStyle.Italic);
        using var pen = new Pen(Color.Black, 1);
        using var brush = new SolidBrush(Color.Black);

        float currentY = bounds.Top;

        // Header trên đầu trang (ở tất cả các trang hoặc trang 1)
        if (_currentPage == 1)
        {
            // Tên khoa
            string tenKhoa = "KHOA CÔNG NGHỆ THÔNG TIN";
            var sizeKhoa = g.MeasureString(tenKhoa, fontTieuDeNho);
            g.DrawString(tenKhoa, fontTieuDeNho, brush, bounds.Left + (bounds.Width - sizeKhoa.Width) / 2, currentY);
            currentY += sizeKhoa.Height + 10;

            // Tiêu đề: PHIẾU BÁO ĐIỂM
            string tieuDe = "PHIẾU BÁO ĐIỂM";
            var sizeTieuDe = g.MeasureString(tieuDe, fontTieuDeLon);
            g.DrawString(tieuDe, fontTieuDeLon, brush, bounds.Left + (bounds.Width - sizeTieuDe.Width) / 2, currentY);
            currentY += sizeTieuDe.Height + 15;

            // Thông tin sinh viên
            g.DrawString($"MSSV: {_sinhVien?.MaSv}", fontInfoBold, brush, bounds.Left + 20, currentY);
            currentY += 22;
            g.DrawString($"Họ tên: {_sinhVien?.HoTen}", fontInfo, brush, bounds.Left + 20, currentY);
            currentY += 22;
            g.DrawString($"Ngày sinh: {_sinhVien?.NgaySinh:dd/MM/yyyy}", fontInfo, brush, bounds.Left + 20, currentY);
            currentY += 22;
            g.DrawString($"Lớp: {_sinhVien?.MaLop}", fontInfo, brush, bounds.Left + 20, currentY);
            currentY += 30;
        }
        else
        {
            string tieuDeTiep = $"PHIẾU BÁO ĐIỂM - {_sinhVien?.HoTen} ({_sinhVien?.MaSv}) (Tiếp theo)";
            g.DrawString(tieuDeTiep, fontInfoBold, brush, bounds.Left + 20, currentY);
            currentY += 30;
        }

        // Kẻ bảng điểm
        float colSTTWidth = 50;
        float colTinChiWidth = 70;
        float colDiemWidth = 70;
        float colMonWidth = bounds.Width - colSTTWidth - colTinChiWidth - colDiemWidth;

        float tableLeft = bounds.Left;
        float rowHeight = 26;

        // Header bảng
        g.DrawRectangle(pen, tableLeft, currentY, bounds.Width, rowHeight);
        g.DrawLine(pen, tableLeft + colSTTWidth, currentY, tableLeft + colSTTWidth, currentY + rowHeight);
        g.DrawLine(pen, tableLeft + colSTTWidth + colMonWidth, currentY, tableLeft + colSTTWidth + colMonWidth, currentY + rowHeight);
        g.DrawLine(pen, tableLeft + colSTTWidth + colMonWidth + colTinChiWidth, currentY, tableLeft + colSTTWidth + colMonWidth + colTinChiWidth, currentY + rowHeight);

        using var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        using var sfLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        using var sfRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        g.DrawString("STT", fontTableH, brush, new RectangleF(tableLeft, currentY, colSTTWidth, rowHeight), sfCenter);
        g.DrawString("Môn học", fontTableH, brush, new RectangleF(tableLeft + colSTTWidth + 5, currentY, colMonWidth - 10, rowHeight), sfLeft);
        g.DrawString("Số TC", fontTableH, brush, new RectangleF(tableLeft + colSTTWidth + colMonWidth, currentY, colTinChiWidth, rowHeight), sfCenter);
        g.DrawString("Điểm", fontTableH, brush, new RectangleF(tableLeft + colSTTWidth + colMonWidth + colTinChiWidth, currentY, colDiemWidth - 5, rowHeight), sfRight);

        currentY += rowHeight;

        // Vẽ các dòng dữ liệu
        float footerReserve = 160; // Khoảng trống dự trữ cho Tổng kết & Chữ ký trang cuối
        bool isLastPage = false;

        while (_currentIndex < _diemMons.Count)
        {
            // Kiểm tra xem có đủ chỗ vẽ dòng này không
            // Nếu đây là trang cuối, cần chừa thêm footerReserve
            bool needSpaceForFooter = (_currentIndex == _diemMons.Count - 1);
            float spaceNeeded = rowHeight + (needSpaceForFooter ? footerReserve : 30);

            if (currentY + spaceNeeded > bounds.Bottom)
            {
                // Hết chỗ trên trang hiện tại -> sang trang
                break;
            }

            var item = _diemMons[_currentIndex];
            int stt = _currentIndex + 1;

            g.DrawRectangle(pen, tableLeft, currentY, bounds.Width, rowHeight);
            g.DrawLine(pen, tableLeft + colSTTWidth, currentY, tableLeft + colSTTWidth, currentY + rowHeight);
            g.DrawLine(pen, tableLeft + colSTTWidth + colMonWidth, currentY, tableLeft + colSTTWidth + colMonWidth, currentY + rowHeight);
            g.DrawLine(pen, tableLeft + colSTTWidth + colMonWidth + colTinChiWidth, currentY, tableLeft + colSTTWidth + colMonWidth + colTinChiWidth, currentY + rowHeight);

            g.DrawString(stt.ToString(), fontTableB, brush, new RectangleF(tableLeft, currentY, colSTTWidth, rowHeight), sfCenter);
            g.DrawString(item.TenMon, fontTableB, brush, new RectangleF(tableLeft + colSTTWidth + 5, currentY, colMonWidth - 10, rowHeight), sfLeft);
            g.DrawString(item.SoTinChi?.ToString() ?? "", fontTableB, brush, new RectangleF(tableLeft + colSTTWidth + colMonWidth, currentY, colTinChiWidth, rowHeight), sfCenter);
            g.DrawString(item.Diem.HasValue ? item.Diem.Value.ToString("0.0") : "", fontTableB, brush, new RectangleF(tableLeft + colSTTWidth + colMonWidth + colTinChiWidth, currentY, colDiemWidth - 5, rowHeight), sfRight);

            currentY += rowHeight;
            _currentIndex++;
        }

        if (_currentIndex >= _diemMons.Count)
        {
            isLastPage = true;
        }

        // Vẽ phần tổng kết & chữ ký chỉ ở trang cuối
        if (isLastPage)
        {
            currentY += 15;
            string strTongKet = $"Điểm trung bình: {_diemTB:0.00}   –   Xếp loại: {_xepLoai}";
            if (_diemMons.Count == 0) strTongKet = "Chưa có điểm môn nào";

            g.DrawString(strTongKet, fontInfoBold, brush, bounds.Left + 20, currentY);
            currentY += 35;

            // Hai chỗ ký tên
            float colSignWidth = bounds.Width / 2;
            g.DrawString("Giáo vụ khoa", fontInfoBold, brush, new RectangleF(bounds.Left, currentY, colSignWidth, 20), sfCenter);
            g.DrawString("Sinh viên", fontInfoBold, brush, new RectangleF(bounds.Left + colSignWidth, currentY, colSignWidth, 20), sfCenter);

            g.DrawString("(Ký, ghi rõ họ tên)", fontItalic, brush, new RectangleF(bounds.Left, currentY + 20, colSignWidth, 20), sfCenter);
            g.DrawString("(Ký, ghi rõ họ tên)", fontItalic, brush, new RectangleF(bounds.Left + colSignWidth, currentY + 20, colSignWidth, 20), sfCenter);
        }

        // Chân trang: Trang x/y
        int displayTotal = Math.Max(_totalPages, _currentPage);
        string strPage = $"Trang {_currentPage}/{displayTotal}";
        var sizePage = g.MeasureString(strPage, fontTableB);
        g.DrawString(strPage, fontTableB, brush, bounds.Right - sizePage.Width - 10, bounds.Bottom - sizePage.Height);

        // Quyết định HasMorePages
        if (!isLastPage)
        {
            e.HasMorePages = true;
        }
        else
        {
            e.HasMorePages = false;
        }
    }
}
