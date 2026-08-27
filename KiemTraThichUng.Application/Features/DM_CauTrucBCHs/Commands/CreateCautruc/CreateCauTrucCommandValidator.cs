using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;
using System.Threading;

namespace KiemTraThichUng.Application.Features.DM_CauTrucBCHs.Commands.CreateCautruc
{
    public class CreateCauTrucCommandValidator : AbstractValidator<CreateCauTrucCommand>
    {
        private readonly ICauHinhDanhMucRepository _cauHinhDanhMucRepository;
        public CreateCauTrucCommandValidator(ICauHinhDanhMucRepository cauHinhDanhMucRepository)
        {
            _cauHinhDanhMucRepository = cauHinhDanhMucRepository;
            RuleFor(x => x.TenCauTruc)
                .NotEmpty().WithMessage("Tên cấu trúc không được để trống.")
                .MaximumLength(255).WithMessage("Tên cấu trúc không được vượt quá 255 ký tự.");
            RuleFor(x => x.MaCauTruc)
                .NotEmpty().WithMessage("Mã cấu trúc không được để trống.")
                .MaximumLength(100).WithMessage("Mã cấu trúc không được vượt quá 100 ký tự.")
                .MustAsync(async (maCauTruc, cancellationToken) =>
                {
                    return !await _cauHinhDanhMucRepository.KiemTraTonTaiCauTrucByMaCauTrucAsync(maCauTruc, cancellationToken);
                })
                .WithMessage("Mã cấu trúc đã tồn tại.");
            RuleFor(x => x.IdBoCauHoi)
                .MustAsync(async (idBoCauHoi, cancellationToken) =>
                {
                    var (exists, _) = await _cauHinhDanhMucRepository.KiemTraTonTaiBoCauHoiByIdsAsync(new List<int> { idBoCauHoi }, cancellationToken);
                    return exists;
                })
                .WithMessage("Bộ câu hỏi không tồn tại");
        }
    }
}
