// File: KiemTraThichUng.Application/Features/EX_CauHois/Commands/CreateCauHoi/CreateCauHoiCommandValidator.cs
using FluentValidation;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.CreateCauHoi
{
    public class CreateCauHoiCommandValidator : AbstractValidator<CreateCauHoiCommand>
    {
        public CreateCauHoiCommandValidator()
        {
            RuleFor(x => x.NoiDung)
                .NotEmpty().WithMessage("Nội dung câu hỏi không được để trống.")
                .MaximumLength(1000).WithMessage("Nội dung câu hỏi không được vượt quá 1000 ký tự.")
                .When(x => x.IsCauHoiCha != null && x.IsCauHoiCha == true);
            RuleFor(x => x.IdCauTruc)
                .NotNull().WithMessage("Cấu trúc câu hỏi phải được chọn.");
            RuleFor(x => x.IdLoaiCauHoi)
                .NotNull().WithMessage("Loại câu hỏi phải được chọn.")
                .Must(KiemTraTonTaiLoaiCauHoi)
                .WithMessage("Loại câu hỏi không hợp lệ.");
            RuleFor(x => x.IdMucDoNhanThuc)
                .NotNull().WithMessage("Mức độ nhận thức phải được chọn.")
                .Must(KiemTraTonTaiMucDoNhanThuc)
                .WithMessage("Mức độ nhận thức không hợp lệ.");
            RuleFor(x => x.DoKhoKhoiTao)
                .NotNull().WithMessage("Độ khó khởi tạo phải được chọn.")
                .InclusiveBetween(1, 5).WithMessage("Độ khó khởi tạo phải từ 1 đến 5.")
                .Must(x => x == null || x % 1 == 0).WithMessage("Độ khó khởi tạo phải là số nguyên.")
                .When(x => x.IsCauHoiCha != null && x.IsCauHoiCha == true);
            RuleFor(x => x.CauHois)
                .NotEmpty().WithMessage("Danh sách câu hỏi không được để trống.");

            RuleForEach(x => x.CauHois)
                .ChildRules(cauHois =>
                {
                    cauHois.RuleFor(x => x.NoiDung)
                        .NotEmpty().WithMessage("Nội dung câu hỏi con không được để trống.")
                        .MaximumLength(1000).WithMessage("Nội dung câu hỏi con không được vượt quá 1000 ký tự.");
                    cauHois.RuleFor(x => x.IdCauTruc)
                        .NotNull().WithMessage("Cấu trúc câu hỏi con phải được chọn.");
                    cauHois.RuleFor(x => x.IdLoaiCauHoi)
                        .NotNull().WithMessage("Loại câu hỏi phải được chọn.")
                        .Must(KiemTraTonTaiLoaiCauHoi)
                        .WithMessage("Loại câu hỏi không hợp lệ.");
                    cauHois.RuleFor(x => x.IdMucDoNhanThuc)
                        .NotNull().WithMessage("Mức độ nhận thức phải được chọn.")
                        .Must(KiemTraTonTaiMucDoNhanThuc)
                        .WithMessage("Mức độ nhận thức không hợp lệ.");
                    cauHois.RuleFor(x => x.DoKhoKhoiTao)
                        .NotNull().WithMessage("Độ khó khởi tạo phải được chọn.")
                        .InclusiveBetween(1, 5).WithMessage("Độ khó khởi tạo phải từ 1 đến 5.")
                        .Must(x => x == null || x % 1 == 0).WithMessage("Độ khó khởi tạo phải là số nguyên.");
                    cauHois.RuleFor(x => x.CauTraLois)
                        .NotEmpty().WithMessage("Danh sách câu trả lời không được để trống.")
                        .Must(cauTraLois => cauTraLois != null && cauTraLois.Any(ctl => ctl.IsDung == true)).WithMessage("Phải có ít nhất một câu trả lời đúng.");

                    cauHois.RuleForEach(x => x.CauTraLois)
                        .ChildRules(cauTraLoi =>
                        {
                            cauTraLoi.RuleFor(x => x.NoiDung)
                                .NotEmpty().WithMessage("Nội dung câu trả lời không được để trống.")
                                .MaximumLength(1000).WithMessage("Nội dung câu trả lời không được vượt quá 1000 ký tự.");
                            cauTraLoi.RuleFor(x => x.PhanTramDiem)
                                .GreaterThanOrEqualTo(0).WithMessage("Phần trăm điểm phải lớn hơn hoặc bằng 0.")
                                .LessThanOrEqualTo(1).WithMessage("Phần trăm điểm phải nhỏ hơn hoặc bằng 1.");
                        });
                });
        }

        private bool KiemTraTonTaiLoaiCauHoi(int? idLoaiCauHoi)
        {
            if (!idLoaiCauHoi.HasValue)
                return false;

            return LoaiCauHoi
                .GetAll<LoaiCauHoi>()
                .Any(x => x.Id == idLoaiCauHoi.Value);
        }

        private bool KiemTraTonTaiMucDoNhanThuc(int? idMucDoNhanThuc)
        {
            if (!idMucDoNhanThuc.HasValue)
                return false;

            return MucDoNhanThuc
                .GetAll<MucDoNhanThuc>()
                .Any(x => x.Id == idMucDoNhanThuc.Value);
        }
    }
}
