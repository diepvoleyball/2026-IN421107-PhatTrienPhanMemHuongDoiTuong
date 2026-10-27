using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class InMemoryClubRepository : IClubRepository
{
    private readonly List<Club> _clubs = new();

    public void Add(Club club) => _clubs.Add(club);

    // Trả bản sao: bên ngoài không sửa được danh sách bên trong
    public IReadOnlyList<Club> GetAll() => _clubs.ToList();

    public Club? GetById(Guid id) => _clubs.FirstOrDefault(c => c.Id == id);
}