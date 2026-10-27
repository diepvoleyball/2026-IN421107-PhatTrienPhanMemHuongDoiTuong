using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class LogNotifier : IEventObserver
{
    private readonly IAppLogger _logger;

    public LogNotifier(IAppLogger logger) => _logger = logger;

    public void OnEventCreated(Event ev, Club club)
        => _logger.Info($"[observer] Event '{ev.Title}' created for {club.Name}");
}