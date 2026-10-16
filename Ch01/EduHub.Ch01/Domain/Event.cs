namespace EduHub.Domain;

/// <summary>Sự kiện do câu lạc bộ tổ chức.</summary>
public class Event
{
    public int Id { get; private set; }

    /// <summary>Khoá ngoại tới Club.</summary>
    public int ClubId { get; private set; }

    public string Title { get; private set; }
    public DateTime StartTime { get; private set; }
    public string Location { get; private set; }
    public int Capacity { get; private set; }

    private Event(int id, int clubId, string title, DateTime startTime, string location, int capacity)
    {
        Id = id;
        ClubId = clubId;
        Title = title;
        StartTime = startTime;
        Location = location;
        Capacity = capacity;
    }

    public static Event Create(int id, Club club, string title, DateTime startTime, string location, int capacity)
    {
        if (id <= 0)
            throw new ArgumentException("Id phải lớn hơn 0.", nameof(id));

        ArgumentNullException.ThrowIfNull(club);

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tên sự kiện không được để trống.", nameof(title));

        if (startTime == default)
            throw new ArgumentException("Thời gian bắt đầu không hợp lệ.", nameof(startTime));

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Địa điểm không được để trống.", nameof(location));

        if (capacity is < 1 or > 5000)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa phải từ 1 đến 5000.");

        return new Event(id, club.Id, title.Trim(), startTime, location.Trim(), capacity);
    }

    public override string ToString() => $"{Title} - {StartTime:dd/MM/yyyy HH:mm}";
}
