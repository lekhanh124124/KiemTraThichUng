// File: KiemTraThichUng.Domain/Entities/NganHangCauHoi/CauHoi.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Exceptions;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;
using KiemTraThichUng.Domain.ValueObjects;

namespace KiemTraThichUng.Domain.Entities.NganHangCauHoi
{
    public class CauHoi : ApprovalEntity
    {
        public Guid CauHoiGuid { get; private set; }

        public string MaCauHoi { get; private set; } = string.Empty;
        public string? NoiDung { get; private set; }
        public string? TieuDeVeTrai { get; private set; }
        public string? TieuDeVePhai { get; private set; }
        public string? MediaUrl { get; private set; }
        public string? GiaiThich { get; private set; }

        public int IdCauTruc { get; private set; }

        public int IdLoaiCauHoi { get; private set; }
        public LoaiCauHoi LoaiCauHoi => LoaiCauHoi.FromId(IdLoaiCauHoi);

        public int? IdMucDoNhanThuc { get; private set; }
        public MucDoNhanThuc? MucDoNhanThuc =>
            IdMucDoNhanThuc.HasValue
                ? MucDoNhanThuc.FromId(IdMucDoNhanThuc.Value)
                : null;

        public bool IsCauHoiCha { get; private set; }
        public int? IdCauHoiCha { get; private set; }

        public bool IsKhongDao { get; private set; }

        public double? DoKho { get; private set; }
        public double? DoKhoKhoiTao { get; private set; }
        public double? DoPhanLoai { get; private set; }
        public double? DoPhanLoaiKhoiTao { get; private set; }

        public int SoLuotLam { get; private set; }
        public int SoLuotDung { get; private set; }

        protected CauHoi() { }

        public CauHoi(
            string? noiDung,
            string? tieuDeVeTrai,
            string? tieuDeVePhai,
            string? mediaUrl,
            string? giaiThich,
            int idCauTruc,
            LoaiCauHoi loaiCauHoi,
            MucDoNhanThuc? mucDoNhanThuc,
            bool? isCauHoiCha,
            int? idCauHoiCha,
            bool? isKhongDao,
            double? doKhoKhoiTao,
            double? doPhanLoaiKhoiTao,
            int? stt,
            bool? isVisible)
        {
            CauHoiGuid = Guid.NewGuid();
            NoiDung = noiDung ?? string.Empty;

            TieuDeVeTrai = tieuDeVeTrai ?? string.Empty;
            TieuDeVePhai = tieuDeVePhai ?? string.Empty;

            MediaUrl = mediaUrl ?? string.Empty;
            GiaiThich = giaiThich ?? string.Empty;

            IdCauTruc = idCauTruc;
            IdLoaiCauHoi = loaiCauHoi.Id;
            IdMucDoNhanThuc = mucDoNhanThuc?.Id;

            IsCauHoiCha = isCauHoiCha ?? false;
            IdCauHoiCha = idCauHoiCha;

            IsKhongDao = isKhongDao ?? false;

            var irt = IrtParameter.Create(doKhoKhoiTao ?? -3, doPhanLoaiKhoiTao ?? 1);

            DoKho = null;
            DoKhoKhoiTao = irt.DoKho;
            DoPhanLoai = null;
            DoPhanLoaiKhoiTao = irt.DoPhanLoai;

            SoLuotLam = 0;
            SoLuotDung = 0;

            Initialize(stt, isVisible);
        }

        public void CapNhatThongTin(
            string? noiDung,
            string? tieuDeVeTrai,
            string? tieuDeVePhai,
            string? mediaUrl,
            string? giaiThich,
            bool? isKhongDao,
            double? doKhoKhoiTao,
            double? doPhanLoaiKhoiTao,
            int? stt,
            bool? isVisible)
        {
            NoiDung = noiDung ?? NoiDung;
            TieuDeVeTrai = tieuDeVeTrai ?? TieuDeVeTrai;
            TieuDeVePhai = tieuDeVePhai ?? TieuDeVePhai;
            MediaUrl = mediaUrl ?? MediaUrl;
            GiaiThich = giaiThich ?? GiaiThich;
            IsKhongDao = isKhongDao ?? IsKhongDao;
            DoKhoKhoiTao = doKhoKhoiTao ?? DoKhoKhoiTao;
            DoPhanLoaiKhoiTao = doPhanLoaiKhoiTao ?? DoPhanLoaiKhoiTao;

            UpdateDisplay(stt, isVisible);
        }

        public void ThemCauHoiCon(
            bool? isCauHoiCha,
            int? idCauHoiCha)
        {
            if (isCauHoiCha == null || isCauHoiCha == true)
                throw new DomainValidationException("IsCauHoiCha phải false khi thêm câu hỏi con.");
            if (idCauHoiCha == null)
                throw new DomainValidationException("Câu hỏi con phải có IdCauHoiCha.");
            IsCauHoiCha = isCauHoiCha ?? false;
            IdCauHoiCha = idCauHoiCha;
        }
        public void CapNhatThongKe(int soLuotLam, int soLuotDung)
        {
            if (soLuotLam < 0 || soLuotDung < 0)
                throw new DomainValidationException("Thống kê không hợp lệ.");

            SoLuotLam += soLuotLam;
            SoLuotDung += soLuotDung;
        }
    }
}
