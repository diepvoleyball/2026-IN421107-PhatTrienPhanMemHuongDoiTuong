using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        const string name = "Tech Club";

        // Act
        var club = Club.Create(name, "Câu lạc bộ công nghệ");

        // Assert
        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Club_Create_WithBlankName_ThrowsArgumentException(string name)
    {
        // Arrange (tham số name do InlineData cấp)

        // Act
        var act = () => Club.Create(name, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_NameLongerThanMax_ThrowsArgumentException()
    {
        // Arrange
        var tooLong = new string('a', Club.MaxNameLength + 1);

        // Act
        var act = () => Club.Create(tooLong, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_NameExactlyMax_Succeeds()
    {
        // Arrange
        var name = new string('a', Club.MaxNameLength);

        // Act
        var club = Club.Create(name, "mô tả");

        // Assert
        Assert.Equal(name, club.Name);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(49, true)]
    [InlineData(50, false)]
    public void Club_HasRoom_DependsOnMemberCount(int count, bool expected)
    {
        // Arrange
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        var result = club.HasRoom(count);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };

        // Act
        var result = ev.Register(Guid.NewGuid());

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var second = ev.Register(memberId);

        // Assert
        Assert.False(second);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };

        // Act
        var act = () => ev.CheckIn(Guid.NewGuid());

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Event_CheckIn_RegisteredMember_AddsToCheckedIn()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        ev.CheckIn(memberId);

        // Assert
        Assert.Contains(memberId, ev.CheckedInMemberIds);
    }
}