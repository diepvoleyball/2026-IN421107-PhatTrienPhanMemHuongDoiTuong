using System.Text;
using EduHub.Domain;

Console.OutputEncoding = Encoding.UTF8;

// ===== 1. Dữ liệu mẫu =====
var club = Club.Create(1, "CLB Lập trình", "Câu lạc bộ học và chia sẻ lập trình", new DateTime(2023, 9, 1));

var members = new List<Member>
{
    Member.Create(1, "Nguyễn Văn An", "an@student.dthu.edu.vn", "0024410001", club),
    Member.Create(2, "Trần Thị Bình", "binh@student.dthu.edu.vn", "0024410002", club),
    Member.Create(3, "Lê Hoàng Cường", "cuong@student.dthu.edu.vn", "0024410003", club),
    Member.Create(4, "Phạm Minh Dũng", "dung@student.dthu.edu.vn", "0024410004", club)
};

var eventStart = new DateTime(2026, 11, 20, 18, 0, 0);
var evt = Event.Create(1, club, "Workshop Git và GitHub", eventStart, "Phòng B.201", 3);

// ===== 2. Đăng ký và xác nhận (kiểm tra sức chứa) =====
var registrations = new List<Registration>();
var nextRegId = 1;

foreach (var m in members)
{
    var reg = Registration.Create(nextRegId++, evt, m, new DateTime(2026, 11, 10, 9, 0, 0));
    registrations.Add(reg);

    var confirmedCount = registrations.Count(r => r.Status == RegistrationStatus.Confirmed);
    if (confirmedCount < evt.Capacity)
    {
        reg.Confirm();
    }
    else
    {
        reg.Cancel();
        Console.WriteLine($"[!] Sự kiện đã đủ {evt.Capacity} chỗ, huỷ đăng ký của {m.FullName}.");
    }
}

// ===== 3. Check-in tại sự kiện =====
var checkIns = new List<CheckIn>();
var nextCheckInId = 1;

// Chỉ 2 trong 3 người đã xác nhận đến dự
foreach (var reg in registrations.Where(r => r.Status == RegistrationStatus.Confirmed).Take(2))
{
    checkIns.Add(CheckIn.Create(nextCheckInId++, reg, eventStart.AddMinutes(-10)));
}

// ===== 4. Thử một vi phạm nghiệp vụ =====
try
{
    var cancelled = registrations.First(r => r.Status == RegistrationStatus.Cancelled);
    CheckIn.Create(nextCheckInId, cancelled, eventStart);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"[Lỗi nghiệp vụ mong đợi] {ex.Message}");
}

// ===== 5. Báo cáo tham dự =====
Console.WriteLine();
Console.WriteLine("========== BÁO CÁO THAM DỰ ==========");
Console.WriteLine($"CLB      : {club.Name}");
Console.WriteLine($"Sự kiện  : {evt.Title}");
Console.WriteLine($"Thời gian: {evt.StartTime:dd/MM/yyyy HH:mm} tại {evt.Location}");
Console.WriteLine($"Sức chứa : {evt.Capacity}");
Console.WriteLine(new string('-', 70));
Console.WriteLine($"{"Thành viên",-28}{"MSSV",-14}{"Đăng ký",-12}{"Check-in",-10}");
Console.WriteLine(new string('-', 70));

foreach (var reg in registrations)
{
    var member = members.First(m => m.Id == reg.MemberId);
    var attended = checkIns.Any(c => c.RegistrationId == reg.Id);
    Console.WriteLine($"{member.FullName,-28}{member.StudentCode,-14}{reg.Status,-12}{(attended ? "Có mặt" : "-"),-10}");
}

var confirmed = registrations.Count(r => r.Status == RegistrationStatus.Confirmed);
var cancelledTotal = registrations.Count(r => r.Status == RegistrationStatus.Cancelled);
var rate = confirmed == 0 ? 0 : (double)checkIns.Count / confirmed * 100;

Console.WriteLine(new string('-', 70));
Console.WriteLine($"Tổng đăng ký : {registrations.Count}");
Console.WriteLine($"Đã xác nhận  : {confirmed}");
Console.WriteLine($"Đã huỷ       : {cancelledTotal}");
Console.WriteLine($"Có mặt       : {checkIns.Count}");
Console.WriteLine($"Tỉ lệ tham dự: {rate:0.#}%");
