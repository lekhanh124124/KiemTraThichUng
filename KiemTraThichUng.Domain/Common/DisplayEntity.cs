// File: KiemTraThichUng.Domain/Common/DisplayEntity.cs
namespace KiemTraThichUng.Domain.Common
{
    public abstract class DisplayEntity : AuditableEntity
    {
        public int? Stt { get; protected set; } = 0;
        public bool IsVisible { get; protected set; } = true;

        public void Initialize(int? stt, bool? isVisible)
        {
            Stt = stt ?? 0;
            IsVisible = isVisible ?? true;
        }

        public void UpdateDisplay(int? stt, bool? isVisible)
        {
            Stt = stt ?? Stt;
            IsVisible = isVisible ?? IsVisible;
        }

        public void Hide()
        {
            IsVisible = false;
        }

        public void Show()
        {
            IsVisible = true;
        }
    }
}
