using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class EmailNotifier : IEventObserver
{
    private readonly IMemberRepository _members;
    private readonly INotificationService _email;

    public EmailNotifier(IMemberRepository members, INotificationService email)
    {
        _members = members;
        _email = email;
    }

    public void OnEventCreated(Event ev, Club club)
    {
        foreach (var m in _members.GetByClub(club.Id))
            _email.Send(m.Email, $"New event '{ev.Title}' at {club.Name}");
    }
}