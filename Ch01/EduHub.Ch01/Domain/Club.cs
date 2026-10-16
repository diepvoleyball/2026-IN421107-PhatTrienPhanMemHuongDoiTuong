namespace EduHub.Domain;

/// <summary>Câu lạc bộ tổ chức các sự kiện.</summary>
public class Club
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime FoundedDate { get; private set; }

    private Club(int id, string name, string description, DateTime foundedDate)
    {
        Id = id;
        Name = name;
        Description = description;
        FoundedDate = foundedDate;
    }

    public static Club Create(int id, string name, string description, DateTime foundedDate)
    {
        if (id <= 0)
            throw new ArgumentException("Id phải lớn hơn 0.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(name));

        if (name.Trim().Length > 100)
            throw new ArgumentException("Tên câu lạc bộ tối đa 100 ký tự.", nameof(name));

        if (foundedDate == default || foundedDate > DateTime.Today)
            throw new ArgumentException("Ngày thành lập không hợp lệ.", nameof(foundedDate));

        return new Club(id, name.Trim(), description?.Trim() ?? string.Empty, foundedDate);
    }

    public override string ToString() => Name;
}
