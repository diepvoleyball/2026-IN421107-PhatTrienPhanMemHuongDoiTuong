using System.Net.Mail;

namespace EduHub.Domain;

/// <summary>Thành viên của một câu lạc bộ.</summary>
public class Member
{
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string StudentCode { get; private set; }

    /// <summary>Khoá ngoại tới Club.</summary>
    public int ClubId { get; private set; }

    private Member(int id, string fullName, string email, string studentCode, int clubId)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        StudentCode = studentCode;
        ClubId = clubId;
    }

    public static Member Create(int id, string fullName, string email, string studentCode, Club club)
    {
        if (id <= 0)
            throw new ArgumentException("Id phải lớn hơn 0.", nameof(id));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên không được để trống.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            throw new ArgumentException("Email không hợp lệ.", nameof(email));

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã sinh viên không được để trống.", nameof(studentCode));

        ArgumentNullException.ThrowIfNull(club);

        return new Member(id, fullName.Trim(), email.Trim().ToLowerInvariant(), studentCode.Trim(), club.Id);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var address = new MailAddress(email.Trim());
            return address.Address == email.Trim();
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public override string ToString() => $"{FullName} ({StudentCode})";
}
