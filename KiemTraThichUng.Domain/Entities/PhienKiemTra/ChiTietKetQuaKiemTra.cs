// File: KiemTraThichUng.Domain/Entities/PhienKiemTra/ChiTietKetQuaKiemTra.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Enums;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.Entities.PhienKiemTra
{
    public class ChiTietKetQuaKiemTra : BaseEntity
    {
        public int IdKetQuaKiemTra { get; private set; }
        public int IdCauHoi { get; private set; }
        public int? IdCauHoiCha { get; private set; }

        public bool? IsTraLoiDung { get; private set; }
        public double PhanTramDiem { get; private set; }

        public double? DoKhoLucThi { get; private set; }
        public double? DoPhanLoaiLucThi { get; private set; }

        public double? StandardErrorBefore { get; private set; }
        public double? StandardErrorAfter { get; private set; }

        public double? ThetaBefore { get; private set; }
        public double? ThetaAfter { get; private set; }
        public double? ThetaTarget { get; private set; }

        public double? ThongTinCauHoi { get; private set; }
        public double? ThongTinTichLuyBefore { get; private set; }
        public double? ThongTinTichLuyAfter { get; private set; }

        public TrangThaiChiTietKetQua TrangThai { get; private set; }

        protected ChiTietKetQuaKiemTra() { }

        public ChiTietKetQuaKiemTra(
            int idKetQuaKiemTra,
            int idCauHoi,
            int? idCauHoiCha,
            double? doKho,
            double? doPhanLoai)
        {
            IdKetQuaKiemTra = idKetQuaKiemTra;
            IdCauHoi = idCauHoi;
            IdCauHoiCha = idCauHoiCha;

            DoKhoLucThi = doKho;
            DoPhanLoaiLucThi = doPhanLoai;

            TrangThai = TrangThaiChiTietKetQua.DaGiao;
        }

        public void ChamDiem(
            bool? isDung,
            double? phanTramDiem,
            double? seBefore,
            double? seAfter,
            double? thetaBefore,
            double? thetaAfter,
            double? thetaTarget,
            double? currentInfo,
            double? InfoTichLuyBefore,
            double? InfoTichLuyAfter)
        {
            if (TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                throw new DomainValidationException("Câu hỏi đã được trả lời.");

            IsTraLoiDung = isDung ?? IsTraLoiDung;
            PhanTramDiem = phanTramDiem ?? PhanTramDiem;

            StandardErrorBefore = seBefore ?? StandardErrorBefore;
            StandardErrorAfter = seAfter ?? StandardErrorAfter;

            ThetaBefore = thetaBefore ?? ThetaBefore;
            ThetaAfter = thetaAfter ?? ThetaAfter;
            ThetaTarget = thetaTarget ?? ThetaTarget;

            ThongTinCauHoi = currentInfo ?? ThongTinCauHoi;
            ThongTinTichLuyBefore = InfoTichLuyBefore ?? ThongTinTichLuyBefore;
            ThongTinTichLuyAfter = InfoTichLuyAfter ?? ThongTinTichLuyAfter;

            TrangThai = TrangThaiChiTietKetQua.DaTraLoi;
        }

        public void BoQua()
        {
            if (TrangThai == TrangThaiChiTietKetQua.DaTraLoi)
                throw new DomainValidationException("Câu hỏi đã được trả lời.");
            IsTraLoiDung = null;
            PhanTramDiem = 0;
            StandardErrorBefore = null;
            StandardErrorAfter = null;
            ThetaBefore = null;
            ThetaAfter = null;
            ThetaTarget = null;
            ThongTinCauHoi = null;
            ThongTinTichLuyBefore = null;
            ThongTinTichLuyAfter = null;
            TrangThai = TrangThaiChiTietKetQua.DaBoQua;
        }
    }
}
