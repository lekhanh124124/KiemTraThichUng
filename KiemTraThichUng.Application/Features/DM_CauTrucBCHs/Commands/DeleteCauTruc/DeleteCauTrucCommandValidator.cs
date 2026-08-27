using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.DeleteCauTruc
{
    public class DeleteCauTrucCommandValidator : AbstractValidator<DeleteCauTrucCommand>
    {
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public DeleteCauTrucCommandValidator(ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage("Vui lòng cung cấp ít nhất một Id để xóa.");

            RuleFor(x => x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var (exists, missingIds) =
                        await _cauHinhDanhMucRepository
                            .KiemTraTonTaiCauTrucByIdsAsync(command.Ids!, cancellationToken);

                    if (!exists)
                    {
                        context.AddFailure(
                            nameof(command.Ids),
                            $"Các Id sau không tồn tại: {string.Join(", ", missingIds!)}");
                    }
                });
        }
    }
}
