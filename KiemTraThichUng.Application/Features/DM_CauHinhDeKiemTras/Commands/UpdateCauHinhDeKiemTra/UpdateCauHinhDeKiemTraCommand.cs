using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.Entities.CauHinhDeKiemTra;
using MediatR;
using FluentValidation;
using KiemTraThichUng.Application.Common.Exceptions;

namespace KiemTraThichUng.Application.Features.DM_CauHinhDeKiemTras.Commands.UpdateCauHinhDeKiemTra
{
    public class UpdateCauHinhDeKiemTraCommand : IRequest<ApiResponse<int>>
    {
        public int Id { get; set; }
        public string MaCauHinhDeKiemTra { get; set; } = string.Empty;
        public string? TenCauHinhDeKiemTra { get; set; }
        public int IdCauTruc { get; set; }
        public int ThoiGianLamBaiGiay { get; set; }
        
        public double ThetaMin { get; set; }
        public double ThetaMax { get; set; }
        public double ThetaKhoiTao { get; set; }
        public double ThetaDat { get; set; }
        public double PriorMean { get; set; }
        public double PriorVariance { get; set; }
        public double StandardErrorInitial { get; set; }

        public int? Stt { get; set; }
        public bool IsVisible { get; set; }

        public List<UpdateChiTietCauHinhDeKiemTraDto> ChiTietCauHinhDeKiemTras { get; set; } = new();
    }

    public class UpdateChiTietCauHinhDeKiemTraDto
    {
        public int? IdLoaiCauHoi { get; set; }
        public int? IdMucDoNhanThuc { get; set; }
        public int SoLuongCauHoi { get; set; }
        public int? Stt { get; set; }
        public bool IsVisible { get; set; }
    }

    public class UpdateCauHinhDeKiemTraCommandValidator : AbstractValidator<UpdateCauHinhDeKiemTraCommand>
    {
        private readonly ICauHinhDeKiemTraRepository _repository;

        public UpdateCauHinhDeKiemTraCommandValidator(ICauHinhDeKiemTraRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.MaCauHinhDeKiemTra)
                .NotEmpty().WithMessage("Mã cấu hình không được để trống.")
                .MustAsync(async (model, ma, token) => !await _repository.KiemTraTonTaiMaAsync(ma, model.Id, token))
                .WithMessage("Mã cấu hình đã tồn tại.");

            RuleFor(x => x.TenCauHinhDeKiemTra)
                .NotEmpty().WithMessage("Tên cấu hình không được để trống.");

            RuleFor(x => x.ThetaMax)
                .GreaterThan(x => x.ThetaMin).WithMessage("ThetaMax phải lớn hơn ThetaMin.");

            RuleFor(x => x.ThetaDat)
                .Must((model, thetaDat) => thetaDat >= model.ThetaMin && thetaDat <= model.ThetaMax)
                .WithMessage("ThetaDat phải nằm trong khoảng ThetaMin và ThetaMax.");

            RuleFor(x => x.ThetaKhoiTao)
                .Must((model, thetaKhoiTao) => thetaKhoiTao >= model.ThetaMin && thetaKhoiTao <= model.ThetaMax)
                .WithMessage("ThetaKhoiTao phải nằm trong khoảng ThetaMin và ThetaMax.");
        }
    }

    public class UpdateCauHinhDeKiemTraCommandHandler : IRequestHandler<UpdateCauHinhDeKiemTraCommand, ApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICauHinhDeKiemTraRepository _repository;

        public UpdateCauHinhDeKiemTraCommandHandler(IUnitOfWork unitOfWork, ICauHinhDeKiemTraRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ApiResponse<int>> Handle(UpdateCauHinhDeKiemTraCommand request, CancellationToken cancellationToken)
        {
            var (entity, _) = await _repository.GetByIdAdminAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(CauHinhDeKiemTra), request.Id);
            }

            entity.CapNhatThongTin(
                request.MaCauHinhDeKiemTra,
                request.TenCauHinhDeKiemTra,
                request.IdCauTruc,
                request.ThoiGianLamBaiGiay,
                request.ThetaMin,
                request.ThetaMax,
                request.ThetaKhoiTao,
                request.ThetaDat,
                request.PriorMean,
                request.PriorVariance,
                request.StandardErrorInitial,
                request.Stt,
                request.IsVisible);

            await _repository.UpdateCauHinhDeKiemTraAsync(entity, cancellationToken);
            
            // Handle details: Replace all
            await _repository.ClearChiTietCauHinhDeKiemTraAsync(entity.Id, cancellationToken);
            
            if (request.ChiTietCauHinhDeKiemTras.Any())
            {
                var details = request.ChiTietCauHinhDeKiemTras.Select(ct => new ChiTietCauHinhDeKiemTra(
                    entity.Id,
                    ct.IdLoaiCauHoi.HasValue ? Domain.NganHangCauHoi.ValueObjects.LoaiCauHoi.FromId(ct.IdLoaiCauHoi.Value) : null,
                    ct.IdMucDoNhanThuc.HasValue ? Domain.NganHangCauHoi.ValueObjects.MucDoNhanThuc.FromId(ct.IdMucDoNhanThuc.Value) : null,
                    ct.SoLuongCauHoi,
                    ct.Stt,
                    ct.IsVisible)).ToList();

                await _repository.AddChiTietCauHinhDeKiemTraRangeAsync(details, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<int>.Success(entity.Id);
        }
    }
}
