namespace EduHub.Domain;

/// <summary>Đăng ký tham dự sự kiện của một thành viên.</summary>
public class Registration
{
    public int Id { get; private set; }

    /// <summary>Khoá ngoại tới Event.</summary>
    public int EventId { get; private set; }

    /// <summary>Khoá ngoại tới Member.</summary>
    public int MemberId { get; private set; }

    public RegistrationStatus Status { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    private Registration(int id, int eventId, int memberId, DateTime registeredAt)
    {
        Id = id;
        EventId = eventId;
        MemberId = memberId;
        RegisteredAt = registeredAt;
        Status = RegistrationStatus.Pending;
    }

    public static Registration Create(int id, Event evt, Member member, DateTime registeredAt)
    {
        if (id <= 0)
            throw new ArgumentException("Id phải lớn hơn 0.", nameof(id));

        ArgumentNullException.ThrowIfNull(evt);
        ArgumentNullException.ThrowIfNull(member);

        if (member.ClubId != evt.ClubId)
            throw new InvalidOperationException("Chỉ thành viên của câu lạc bộ mới được đăng ký sự kiện này.");

        if (registeredAt == default)
            throw new ArgumentException("Thời gian đăng ký không hợp lệ.", nameof(registeredAt));

        if (registeredAt >= evt.StartTime)
            throw new InvalidOperationException("Không thể đăng ký khi sự kiện đã bắt đầu.");

        return new Registration(id, evt.Id, member.Id, registeredAt);
    }

    /// <summary>Xác nhận đăng ký (chỉ từ trạng thái Pending).</summary>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException("Chỉ đăng ký đang chờ mới được xác nhận.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>Huỷ đăng ký (không huỷ lại được lần hai).</summary>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Đăng ký đã bị huỷ trước đó.");

        Status = RegistrationStatus.Cancelled;
    }
}
