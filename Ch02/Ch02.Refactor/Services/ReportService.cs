namespace EduHub.Ch02;

public class ReportService
{
    private readonly IClubReader _clubs;
    private readonly IMemberRepository _members;
    private readonly IAppLogger _logger;

    public ReportService(IClubReader clubs, IMemberRepository members, IAppLogger logger)
    {
        _clubs = clubs;
        _members = members;
        _logger = logger;
    }

    public string Generate(IReportStrategy strategy)
    {
        var stats = _clubs.GetAll()
            .Select(c => new ClubStat(c.Name, _members.CountByClub(c.Id)))
            .ToList();

        _logger.Info($"Report generated ({strategy.Name})");
        return strategy.Render(stats);   // cách trình bày do strategy quyết định
    }
}