namespace EduHub.Ch02;

public record ClubStat(string ClubName, int MemberCount);

public interface IReportStrategy
{
    string Name { get; }
    string Render(IReadOnlyList<ClubStat> stats);
}