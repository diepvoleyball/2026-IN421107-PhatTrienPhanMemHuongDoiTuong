using EduHub.Ch02;

// ── Composition root: NƠI DUY NHẤT được "new" class cụ thể ───────
IAppLogger logger = new FileAppLogger();
IClubRepository clubRepo = new LoggingClubRepository(new InMemoryClubRepository(), logger); // Decorator
IMemberRepository memberRepo = new InMemoryMemberRepository();
INotificationServiceFactory factory = new NotificationServiceFactory();

var clubService = new ClubService(clubRepo, memberRepo, factory, logger);
var eventService = new EventService(clubRepo, logger);
var reportService = new ReportService(clubRepo, memberRepo, logger);

eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(memberRepo, factory.Create(NotificationChannel.Email)));

// ── 1. Tạo CLB và thêm thành viên ───────────────────────────────
Console.WriteLine("=== 1. Create club and member ===");
var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
clubService.AddMember(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");

// ── 2. Observer ─────────────────────────────────────────────────
Console.WriteLine("\n=== 2. Observer: create event ===");
eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

// ── 3. Strategy ─────────────────────────────────────────────────
Console.WriteLine("\n=== 3. Strategy: report ===");
Console.WriteLine(reportService.Generate(new ConsoleReportStrategy()));
Console.WriteLine(reportService.Generate(new CsvReportStrategy()));

// ── 4. Factory ──────────────────────────────────────────────────
Console.WriteLine("\n=== 4. Factory: notification channels ===");
foreach (var channel in Enum.GetValues<NotificationChannel>())
    factory.Create(channel).Send("demo@example.com", $"hello via {channel}");

// ── 5. Dữ liệu sai ──────────────────────────────────────────────
Console.WriteLine("\n=== 5. Invalid data ===");
try { clubService.CreateClub("", "x", "p@gmail.com"); }
catch (ArgumentException ex) { Console.WriteLine($"Rejected: {ex.Message}"); }