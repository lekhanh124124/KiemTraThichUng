// File: KiemTraThichUng.Domain/ValueObjects/MucDoNhanThuc.cs
using KiemTraThichUng.Domain.Common;

namespace KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects
{
    public sealed class MucDoNhanThuc : Enumeration
    {
        public string Code { get; }
        public string Nhom { get; }

        private MucDoNhanThuc(int id, string code, string name, string nhom)
            : base(id, name)
        {
            Code = code;
            Nhom = nhom;
        }


        public static readonly MucDoNhanThuc KienThuc_Nho =
            new(1, "KT_NHO", "Kiến thức - Nhớ", "Kiến thức");

        public static readonly MucDoNhanThuc KienThuc_Hieu =
            new(2, "KT_HIEU", "Kiến thức - Hiểu", "Kiến thức");

        public static readonly MucDoNhanThuc KienThuc_VanDung =
            new(3, "KT_VAN_DUNG", "Kiến thức - Vận dụng", "Kiến thức");

        public static readonly MucDoNhanThuc KienThuc_PhanTich =
            new(4, "KT_PHAN_TICH", "Kiến thức - Phân tích", "Kiến thức");

        public static readonly MucDoNhanThuc KienThuc_DanhGia =
            new(5, "KT_DANH_GIA", "Kiến thức - Đánh giá", "Kiến thức");

        public static readonly MucDoNhanThuc KienThuc_SangTao =
            new(6, "KT_SANG_TAO", "Kiến thức - Sáng tạo", "Kiến thức");

        public static readonly MucDoNhanThuc KyNang_VanDung =
            new(7, "KN_VAN_DUNG", "Kỹ năng - Vận dụng", "Kỹ năng");

        public static readonly MucDoNhanThuc KyNang_ChinhXac =
            new(8, "KN_CHINH_XAC", "Kỹ năng - Chính xác", "Kỹ năng");

        public static readonly MucDoNhanThuc KyNang_ThanhThao =
            new(9, "KN_THANH_THAO", "Kỹ năng - Thành thạo", "Kỹ năng");

        public static readonly MucDoNhanThuc ThaiDo_TiepNhan =
            new(10, "TD_TIEP_NHAN", "Thái độ - Tiếp nhận", "Thái độ");

        public static readonly MucDoNhanThuc ThaiDo_HoiDap =
            new(11, "TD_HOI_DAP", "Thái độ - Hồi đáp", "Thái độ");

        public static readonly MucDoNhanThuc ThaiDo_DanhGia =
            new(12, "TD_DANH_GIA", "Thái độ - Đánh giá", "Thái độ");

        public static MucDoNhanThuc FromId(int id)
            => FromId<MucDoNhanThuc>(id);

        public static MucDoNhanThuc FromName(string name)
            => FromName<MucDoNhanThuc>(name);

        public static IEnumerable<MucDoNhanThuc> GetByNhom(string nhom)
            => GetAll<MucDoNhanThuc>()
                .Where(x => x.Nhom.Equals(nhom, StringComparison.OrdinalIgnoreCase));
    }
}
