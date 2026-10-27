namespace EduHub.Ch02;

public class CsvReportStrategy : IReportStrategy
{
    public string Name => "CSV";

    public string Render(IReadOnlyList<ClubStat> stats)
        => "club,members" + Environment.NewLine +
           string.Join(Environment.NewLine, stats.Select(s => $"\"{s.ClubName}\",{s.MemberCount}"));
}