// File: KiemTraThichUng.Domain/ValueObjects/LoaiCauHoi.cs
using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.Exceptions;

namespace KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects
{
    public sealed class LoaiCauHoi : Enumeration
    {
        public string Code { get; }

        private LoaiCauHoi(int id, string code, string name)
            : base(id, name)
        {
            Code = code;
        }

        public static readonly LoaiCauHoi TracNghiemDon =
            new(1, "TN_DON", "Trắc nghiệm đơn");

        public static readonly LoaiCauHoi TracNghiemNhom =
            new(2, "TN_NHOM", "Trắc nghiệm nhóm");

        public static readonly LoaiCauHoi DungSai =
            new(3, "DS_DON", "Đúng / Sai");

        public static readonly LoaiCauHoi DungSaiNhom =
            new(4, "DS_NHOM", "Đúng / Sai nhóm");

        public static readonly LoaiCauHoi NoiCheo =
            new(5, "NOI_CHEO", "Nối chéo");

        public static readonly LoaiCauHoi TuLuan =
            new(6, "TL_DON", "Tự luận");

        public static readonly LoaiCauHoi TuLuanNhom =
            new(7, "TL_NHOM", "Tự luận nhóm");

        public static readonly LoaiCauHoi DienTu =
            new(8, "DIEN_TU", "Điền từ hoàn thành câu");

        public static readonly LoaiCauHoi SapXepDoan =
            new(9, "SAP_XEP", "Sắp xếp thành đoạn văn");

        public static LoaiCauHoi FromId(int id)
        {
            return FromId<LoaiCauHoi>(
                id,
                invalidId => new DomainValidationException(
                    $"Loại câu hỏi với Id = {invalidId} không tồn tại trong hệ thống."));
        }

        public static LoaiCauHoi FromName(string name)
            => FromName<LoaiCauHoi>(name);
    }
}
