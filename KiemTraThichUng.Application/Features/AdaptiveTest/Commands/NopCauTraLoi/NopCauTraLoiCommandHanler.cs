using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Application.Features.AdaptiveTest.DTOs;
using KiemTraThichUng.Domain.Entities.NganHangCauHoi;
using KiemTraThichUng.Domain.Entities.PhienKiemTra;
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;
using MediatR;

namespace KiemTraThichUng.Application.Features.AdaptiveTest.Commands.NopCauTraLoi
{
    public class NopCauTraLoiCommandHanler : IRequestHandler<NopCauTraLoiCommand, ApiResponse<NopCauTraLoiResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhienKiemTraRepository _phienKiemTraRepository;
        private readonly ICauHinhDeKiemTraRepository _cauHinhDeKiemTraRepository;
        private readonly INganHangCauHoiRepository _nganHangCauHoiRepository;
        private readonly IAnswerEvaluationService _answerEvaluationService;
        private readonly IUpdateThetaService _updateThetaService;
        private readonly IUnitOfWork _unitOfWork;

        public NopCauTraLoiCommandHanler(
            ICurrentUserService currentUserService,
            IPhienKiemTraRepository phienKiemTraRepository, 
            ICauHinhDeKiemTraRepository cauHinhDeKiemTraRepository,
            INganHangCauHoiRepository nganHangCauHoiRepository,
            IAnswerEvaluationService answerEvaluationService,
            IUpdateThetaService thetaUpdateService,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _phienKiemTraRepository = phienKiemTraRepository;
            _cauHinhDeKiemTraRepository = cauHinhDeKiemTraRepository;
            _nganHangCauHoiRepository = nganHangCauHoiRepository;
            _answerEvaluationService = answerEvaluationService;
            _updateThetaService = thetaUpdateService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<NopCauTraLoiResponse>> Handle(NopCauTraLoiCommand request, CancellationToken cancellationToken)
        {
            var idNguoiDung = _currentUserService.UserId;
            var phienKiemTra = await _phienKiemTraRepository.LayPhienKiemTraByIdNguoiDungAsync(idNguoiDung, cancellationToken);

            if (phienKiemTra.KetQua == null)
            {
                throw new ValidationException(["Phiên kiểm tra không tồn tại hoặc đã kết thúc."]);
            }

            var cauHoiData = await _nganHangCauHoiRepository.LayCauHoiByIdsAsync(request.DapAnNguoiDung.Select(x => x.IdCauHoi).ToList(), cancellationToken);

            var cauHinhDeKiemTra = await _cauHinhDeKiemTraRepository.LayCauHinhDeKiemTraByIdAsync(phienKiemTra.KetQua.IdCauHinhDeKiemTra, cancellationToken);

            var ketQuaDanhGiaDapAns =
            (
                from dapAn in request.DapAnNguoiDung
                join chiTietKetQua in phienKiemTra.ChiTietKetQuas
                    on dapAn.IdCauHoi equals chiTietKetQua.IdCauHoi
                where chiTietKetQua.TrangThai == TrangThaiChiTietKetQua.DaGiao
                orderby dapAn.IdCauHoi
                select new KetQuaDanhGiaDapAn
                {
                    IdCauHoi = dapAn.IdCauHoi,
                    IdCauHoiCha = chiTietKetQua.IdCauHoiCha,
                    IdCauTraLoi = dapAn.Id,
                    NoiDungCauTraLoi = dapAn.NoiDung,
                    IsCorrect = null,
                    ScoreRatio = null,
                    CurrentDifficulty = chiTietKetQua.DoKhoLucThi,
                    ThetaBefore = null,
                    ThetaAfter = null,
                    TargetTheta = null,
                    StandardErrorBefore = null,
                    StandardErrorAfter = null,
                    CurrentItemInformation = null,
                    CumulativeInformationBefore = null,
                    CumulativeInformationAfter = null
                }
            ).ToList();

            var IdLoaiCauHoi = cauHoiData.First().CauHoi.IdLoaiCauHoi;

            var loaiCauHoi = LoaiCauHoi.FromId(IdLoaiCauHoi);

            _answerEvaluationService.Evaluate(
                loaiCauHoi.Id,
                cauHoiData
                    .SelectMany(c => c.CauTraLois)
                    .ToList(),
                ketQuaDanhGiaDapAns);

            var ketQuaDam = phienKiemTra.ChiTietKetQuas
                .Where(ctkq => ctkq.TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                .OrderBy(ctkq => ctkq.Id)
                .Select(ctkq => new KetQuaDanhGiaDapAn
                {
                    IdCauHoi = ctkq.IdCauHoi,
                    IdCauHoiCha = ctkq.IdCauHoiCha,
                    IdCauTraLoi = null,
                    NoiDungCauTraLoi = null,
                    IsCorrect = ctkq.IsTraLoiDung,
                    ScoreRatio = ctkq.PhanTramDiem,
                    CurrentDifficulty = ctkq.DoKhoLucThi,
                    ThetaBefore = ctkq.ThetaBefore,
                    ThetaAfter = ctkq.ThetaAfter,
                    TargetTheta = ctkq.ThetaTarget,
                    StandardErrorBefore = ctkq.StandardErrorBefore,
                    StandardErrorAfter = ctkq.StandardErrorAfter,
                    CurrentItemInformation = ctkq.ThongTinCauHoi,
                    CumulativeInformationBefore = ctkq.ThongTinTichLuyBefore,
                    CumulativeInformationAfter = ctkq.ThongTinTichLuyAfter
                }).ToList();

            _updateThetaService.UpdateThetaWithMAP(
                ketQuaDam,
                ketQuaDanhGiaDapAns,
                cauHinhDeKiemTra.CauHinh.ThetaMax,
                cauHinhDeKiemTra.CauHinh.ThetaMin,
                cauHinhDeKiemTra.CauHinh.ThetaKhoiTao,
                cauHinhDeKiemTra.CauHinh.PriorMean,
                cauHinhDeKiemTra.CauHinh.PriorVariance,
                cauHinhDeKiemTra.CauHinh.StandardErrorInitial);

            foreach (var ketQua in ketQuaDanhGiaDapAns)
            {
                var chiTietKetQua = phienKiemTra.ChiTietKetQuas.First(ctkq => ctkq.IdCauHoi == ketQua.IdCauHoi);
                chiTietKetQua.ChamDiem(
                    ketQua.IsCorrect,
                    ketQua.ScoreRatio,
                    ketQua.StandardErrorBefore,
                    ketQua.StandardErrorAfter,
                    ketQua.ThetaBefore,
                    ketQua.ThetaAfter,
                    ketQua.TargetTheta,
                    ketQua.CurrentItemInformation,
                    ketQua.CumulativeInformationBefore,
                    ketQua.CumulativeInformationAfter);
            }

            var chiTietLuaChonCauTraLois =
            (
                from answer in request.DapAnNguoiDung
                join dapAn in cauHoiData
                    .SelectMany(ch => ch.CauTraLois)
                    on answer.Id equals dapAn.Id
                let chiTietKetQua = phienKiemTra.ChiTietKetQuas.First(ctkq => ctkq.IdCauHoi == dapAn.IdCauHoi)
                select new ChiTietLuaChon(
                    chiTietKetQua.Id,
                    answer.Id!.Value,
                    answer.NoiDung,
                    chiTietKetQua.IsTraLoiDung ?? false,
                    chiTietKetQua.PhanTramDiem)
            ).ToList();

            await _phienKiemTraRepository.TaoChiTietLuaChonDapAnAsync(chiTietLuaChonCauTraLois, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            var response = new NopCauTraLoiResponse
            {
                ThetaBefore = ketQuaDanhGiaDapAns.Last().ThetaBefore,
                ThetaAfter = ketQuaDanhGiaDapAns.Last().ThetaAfter,
                StandardError = ketQuaDanhGiaDapAns.Last().StandardErrorAfter,
                AnswerResults = ketQuaDanhGiaDapAns
            };

            return  ApiResponse<NopCauTraLoiResponse>.Success(response);
        }
    }
}
