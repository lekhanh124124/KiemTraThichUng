// File: KiemTraThichUng.Application/Features/AdaptiveTest/DTOs/KetQuaDanhGiaDapAn.cs
namespace KiemTraThichUng.Application.Features.AdaptiveTest.DTOs
{
    public class KetQuaDanhGiaDapAn
    {
        public int? IdCauHoi { get; set; }
        public int? IdCauHoiCha { get; set; }
        public int? IdCauTraLoi { get; set; }
        public string? NoiDungCauTraLoi { get; set; }
        public bool? IsCorrect { get; set; }
        public double? ScoreRatio { get; set; } // 0 or 1
        public double? CurrentDifficulty { get; set; } // Độ khó hiện tại của câu hỏi [-3.0; +3.0]
        public double? ThetaBefore { get; set; } // Theta đầu vào [-3.0; +3.0]
        public double? ThetaAfter { get; set; } // Theta sau khi cập nhật [-3.0; +3.0], giá trị cần tính
        public double? TargetTheta { get; set; } // Theta mục tiêu (dùng để chọn câu hỏi tiếp theo), giá trị cần tính
        public double? StandardErrorBefore { get; set; } // Sai số ước lượng của Theta [0.0 = +inf)   
        public double? StandardErrorAfter { get; set; } // Sai số ước lượng của Theta [0.0 = +inf), giá trị cần tính
        public double? CurrentItemInformation { get; set; } // Thông tin của câu hỏi [0.0 - 0.25], giá trị cần tính
        public double? CumulativeInformationBefore { get; set; } // Tổng thông tin tích luỹ đến câu hỏi trước
        public double? CumulativeInformationAfter { get; set; } // Tổng thông tin tích luỹ đến câu hỏi này, giá trị cần tính
    }
}
