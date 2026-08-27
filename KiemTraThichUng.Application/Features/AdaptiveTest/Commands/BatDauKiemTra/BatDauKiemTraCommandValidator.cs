using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.BatDauKiemTra
{
    public class BatDauKiemTraCommandValidator : AbstractValidator<BatDauKiemTraCommand>
    {
        private readonly ICauHinhDeKiemTraRepository _cauHinhDeKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        public BatDauKiemTraCommandValidator(
            ICauHinhDeKiemTraRepository cauHinhDeKiemTraRepository,
            IPhienKiemTraRepository phienKiemTraRepository,
            ICurrentUserService currentUserService)
        {
            _cauHinhDeKiemTraRepository = cauHinhDeKiemTraRepository;
            _currentUserService = currentUserService;
            _phienKiemTraRepository = phienKiemTraRepository;

            RuleFor(x => x.IdCauHinhDeKiemTra)
                .NotEmpty().WithMessage("Cấu hình đề kiểm tra không được để trống.");

            RuleFor(x => x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var id = new List<int> { command.IdCauHinhDeKiemTra };
                    var (exists, _) =
                        await _cauHinhDeKiemTraRepository
                            .KiemTraTonTaiCauHinhDeKiemTraByIdsAsync(id, cancellationToken);

                    if (!exists)
                    {
                        context.AddFailure($"Không tìm thấy id cấu hình để kiểm tra '{command.IdCauHinhDeKiemTra}'");
                    }

                    var idNguoiDung = _currentUserService.UserId;

                    var (isHoanThanh, cauHinh) = await _phienKiemTraRepository.KiemTraDaHoanThanhAsync(idNguoiDung, cancellationToken);
                    if (!isHoanThanh && cauHinh!.Id != command.IdCauHinhDeKiemTra)
                    {
                        context.AddFailure($"Bạn chưa hoàn thành bài kiểm tra '{cauHinh?.TenCauHinhDeKiemTra}'. Vui lòng hoàn thành bài kiểm tra trước khi bắt đầu bài kiểm tra mới.");
                    }
                });
            _phienKiemTraRepository = phienKiemTraRepository;
        }

    }
}
