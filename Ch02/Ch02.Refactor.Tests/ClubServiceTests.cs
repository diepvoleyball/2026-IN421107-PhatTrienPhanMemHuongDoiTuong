using EduHub.Ch02;

namespace Ch02.Refactor.Tests;

public class ClubServiceTests
{
    // Bản giả: chứng minh nhờ DIP mà test không cần ghi file hay gửi mail thật
    private class FakeLogger : IAppLogger
    {
        public List<string> Messages { get; } = new();
        public void Info(string message) => Messages.Add(message);
    }

    private class FakeNotification : INotificationService
    {
        public int Sent { get; private set; }
        public void Send(string to, string message) => Sent++;
    }

    private class FakeFactory : INotificationServiceFactory
    {
        public FakeNotification Notifier { get; } = new();
        public INotificationService Create(NotificationChannel channel) => Notifier;
    }

    private static (ClubService Service, FakeLogger Logger, FakeFactory Factory) Build()
    {
        var logger = new FakeLogger();
        var factory = new FakeFactory();
        var service = new ClubService(new InMemoryClubRepository(),
                                      new InMemoryMemberRepository(), factory, logger);
        return (service, logger, factory);
    }

    [Fact]
    public void CreateClub_Valid_LogsAndNotifies()
    {
        // Arrange
        var (service, logger, factory) = Build();

        // Act
        service.CreateClub("Tech Club", "mô tả", "p@gmail.com");

        // Assert
        Assert.Contains(logger.Messages, m => m.Contains("Club created"));
        Assert.Equal(1, factory.Notifier.Sent);
    }

    [Fact]
    public void CreateClub_EmptyName_ThrowsArgumentException()
    {
        // Arrange
        var (service, _, _) = Build();

        // Act
        var act = () => service.CreateClub("", "mô tả", "p@gmail.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void AddMember_UnknownClub_ThrowsInvalidOperationException()
    {
        // Arrange
        var (service, _, _) = Build();

        // Act
        var act = () => service.AddMember(Guid.NewGuid(), "A", "a@x.com");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}