namespace EduHub.Domain;

/// <summary>Bản ghi điểm danh (check-in) tại sự kiện.</summary>
public class CheckIn
{
    public int Id { get; private set; }

    /// <summary>Khoá ngoại tới Registration.</summary>
    public int RegistrationId { get; private set; }

    public DateTime CheckedInAt { get; private set; }

    private CheckIn(int id, int registrationId, DateTime checkedInAt)
    {
        Id = id;
        RegistrationId = registrationId;
        CheckedInAt = checkedInAt;
    }

    public static CheckIn Create(int id, Registration registration, DateTime checkedInAt)
    {
        if (id <= 0)
            throw new ArgumentException("Id phải lớn hơn 0.", nameof(id));

        ArgumentNullException.ThrowIfNull(registration);

        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException("Chỉ đăng ký đã xác nhận mới được check-in.");

        if (checkedInAt == default)
            throw new ArgumentException("Thời gian check-in không hợp lệ.", nameof(checkedInAt));

        if (checkedInAt < registration.RegisteredAt)
            throw new ArgumentException("Check-in không được trước thời điểm đăng ký.", nameof(checkedInAt));

        return new CheckIn(id, registration.Id, checkedInAt);
    }
}
