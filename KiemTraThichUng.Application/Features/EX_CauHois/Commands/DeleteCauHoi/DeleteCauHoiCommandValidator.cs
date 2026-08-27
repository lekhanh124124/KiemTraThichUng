using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;

namespace KiemTraThichUng.Application.Features.EX_CauHois.Commands.DeleteCauHoi
{
    public class DeleteCauHoiCommandValidator : AbstractValidator<DeleteCauHoiCommand>
    {
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        public DeleteCauHoiCommandValidator(INganHangCauHoiRepository nganHangCauHoiRepository)
        {
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Danh sách id không được để trống.");

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
