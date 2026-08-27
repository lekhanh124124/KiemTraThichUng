using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Domain.Enums;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.UpdateTrangThaiCauHoi
{
    public class CapNhatTrangThaiCauHoiCommandValidator : AbstractValidator<CapNhatTrangThaiCauHoiCommand>
    {
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        public CapNhatTrangThaiCauHoiCommandValidator(INganHangCauHoiRepository nganHangCauHoiRepository)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;

            RuleFor(x => x.Ids)
                .NotNull().WithMessage("Danh sách Ids không được để trống.");

            RuleFor(x => x.IdTrangThai)
                .NotNull().WithMessage("Trạng thái không được để trống.")
                .Must(id => Enum.IsDefined(typeof(TrangThaiDuyet), id)).WithMessage("Trạng thái không hợp lệ.");

            RuleFor(x => x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var (exists, missingIds) =
                        await _nganHangCauHoiRepository
                            .KiemTraTonTaiCauHoiByIdsAsync(command.Ids, cancellationToken);

                    if (!exists)
                    {
                        context.AddFailure(
                            nameof(command.Ids),
                            $"Không tìm thấy id: {string.Join(", ", missingIds!)}");
                    }
                });
        }
    }
}
