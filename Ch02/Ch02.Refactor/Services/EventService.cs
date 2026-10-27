using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class EventService
{
    private readonly IClubReader _clubs;
    private readonly IAppLogger _logger;
    private readonly List<IEventObserver> _observers = new();

    public EventService(IClubReader clubs, IAppLogger logger)
    {
        _clubs = clubs;
        _logger = logger;
    }

    public void Subscribe(IEventObserver observer) => _observers.Add(observer);

    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        _logger.Info($"Event created: {title}");

        // Observer: EventService không biết ai nhận tin, chỉ báo cho tất cả người đã đăng ký
        foreach (var observer in _observers.ToList())
            observer.OnEventCreated(ev, club);

        return ev;
    }
}