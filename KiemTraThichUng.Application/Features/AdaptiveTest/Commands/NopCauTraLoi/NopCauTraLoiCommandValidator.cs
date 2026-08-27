using FluentValidation;
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.NopCauTraLoi
{
    public class NopCauTraLoiCommandValidator : AbstractValidator<NopCauTraLoiCommand>
    {
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICurrentUserService _currentUserService;


        public NopCauTraLoiCommandValidator(
            IPhienKiemTraRepository phienKiemTraRepository,
            ICurrentUserService currentUserService)
        {
            _phienKiemTraRepository = phienKiemTraRepository;
            _currentUserService = currentUserService;
            RuleFor(x => x.DapAnNguoiDung)
                .NotEmpty().WithMessage("Danh sách đáp án của người dùng không được để trống.");

            RuleForEach(x => x.DapAnNguoiDung)
                .ChildRules(dapAn =>
                {
                    dapAn.RuleFor(x => x.IdCauHoi)
                        .NotEmpty().WithMessage("Id câu hỏi không được để trống.")
                        .GreaterThan(0).WithMessage("Id câu hỏi không được để trống.");
                    dapAn.RuleFor(x => x.Id)
                        .NotEmpty().WithMessage("Id đáp án không được để trống.")
                        .GreaterThan(0).WithMessage("Id đáp án không được để trống.");
                });

            RuleFor(x => x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var idNguoiDung = _currentUserService.UserId;
                    var idsCauHoiDaChon = command.DapAnNguoiDung.Select(x => x.IdCauHoi).ToList();

                    var cauTraLoisThieu = await _phienKiemTraRepository.KiemTraCauTraLoiThieuAsync(
                        idNguoiDung,
                        idsCauHoiDaChon,
                        cancellationToken);

                    if (cauTraLoisThieu.IsThieu)
                    {
                        var cauHoisThieuStr = string.Join(", ", cauTraLoisThieu.CauHoisThieu);
                        context.AddFailure($"Thiếu câu trả lời cho các câu hỏi có Id: {cauHoisThieuStr}.");
                    }
                });
        }
    }
}
