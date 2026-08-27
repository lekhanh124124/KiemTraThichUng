using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.UpdateCautruc
{
    public class UpdateCauTrucCommandValidator : AbstractValidator<UpdateCauTrucCommand>
    {
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public UpdateCauTrucCommandValidator(ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id không được để trống")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var (exists, _) = await _cauHinhDanhMucRepository.KiemTraTonTaiCauTrucByIdsAsync(new List<int> { id }, cancellationToken);
                    return exists;
                }).WithMessage("Cấu trúc với Id này không tồn tại.");

            RuleFor(x => x.MaCauTruc)
                .MaximumLength(50).WithMessage("Mã cấu trúc không được vượt quá 50 ký tự.");

            RuleFor(x => x.TenCauTruc)
                .MaximumLength(255).WithMessage("Tên cấu trúc không được vượt quá 255 ký tự.");

            RuleFor(v => v)
                .MustAsync(async (request, cancellationToken) =>
                {
                    if (string.IsNullOrEmpty(request.MaCauTruc)) return true; 
                    return !await _cauHinhDanhMucRepository.KiemTraTonTaiCauTrucByMaCauTrucNgoaiIdAsync(request.Id, request.MaCauTruc, cancellationToken);
                }).WithMessage("Mã cấu trúc đã tồn tại.");
        }
    }
}
