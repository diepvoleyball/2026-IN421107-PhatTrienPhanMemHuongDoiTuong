using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

// ISP: tách phần ĐỌC khỏi phần GHI, nơi chỉ cần đọc (ReportService) chỉ phụ thuộc IClubReader
public interface IClubReader
{
    IReadOnlyList<Club> GetAll();
    Club? GetById(Guid id);
}

public interface IClubRepository : IClubReader
{
    void Add(Club club);
}