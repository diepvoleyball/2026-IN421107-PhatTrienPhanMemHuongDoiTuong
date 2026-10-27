using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class EventService
{
    private readonly IClubReader _clubs;
    private readonly IAppLogger _logger;

    public EventService(IClubReader clubs, IAppLogger logger)
    {
        _clubs = clubs;
        _logger = logger;
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        _ = _clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        _logger.Info($"Event created: {title}");
        return ev;
    }
}