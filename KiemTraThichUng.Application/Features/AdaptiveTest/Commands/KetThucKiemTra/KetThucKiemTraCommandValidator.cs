using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.KetThucKiemTra
{
    public class KetThucKiemTraCommandValidator : AbstractValidator<KetThucKiemTraCommand>
    {
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;
        public KetThucKiemTraCommandValidator
            (IPhienKiemTraRepository phienKiemTraRepository,
            ICurrentUserService currentUserService)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _currentUserService = currentUserService;
            RuleFor(x=>x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var idNguoiDung = _currentUserService.UserId;
                    var (isHoanThanh, _) = await _phienKiemTraRepository.KiemTraDaHoanThanhAsync(idNguoiDung, cancellationToken);
                    if (isHoanThanh)
                    {
                        context.AddFailure("Không tìm thấy bài kiểm tra chưa hoàn thành.");
                    }
                });
        }
    }
}
