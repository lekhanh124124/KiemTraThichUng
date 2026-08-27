using KiemTraThichUng.Domain.Common;
using KiemTraThichUng.Domain.NganHangCauHoi.ValueObjects;

namespace KiemTraThichUng.Domain.ValueObjects
{
    public sealed class StopReason : Enumeration
    {
        public string Code { get; }

        private StopReason(int id, string code, string name)
            : base(id, name)
        {
            Code = code;
        }

        public static readonly StopReason TimeExpired =
            new(1, "TimeExpired", "Hết thời gian làm bài");

        public static readonly StopReason BlueprintCompleted =
            new(2, "BlueprintCompleted", "Hoàn thành theo kế hoạch đề");

        public static readonly StopReason NoEligibleQuestion =
            new(3, "NoEligibleQuestion", "Không còn câu hỏi phù hợp");

        public static readonly StopReason SessionClosed =
            new(4, "SessionClosed", "Phiên kiểm tra đã đóng");

        public static readonly StopReason UserSubmitted =
            new(5, "UserSubmitted", "Người dùng kết thúc kiểm tra");

        public static readonly StopReason BlueprintUnderMaintenance =
            new(6, "BlueprintUnderMaintenance", "Kế hoạch đề đang bảo trì");

        public static StopReason FromId(int id)
            => FromId<StopReason>(id);

        public static StopReason FromName(string name)
            => FromName<StopReason>(name);
    }
}
