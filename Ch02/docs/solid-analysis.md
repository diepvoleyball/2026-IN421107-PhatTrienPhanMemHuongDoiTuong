# Phân tích vi phạm SOLID trong ClubManager

Nguồn: `Ch02.Refactor/ClubManager.cs` (bản starter).

| # | Dòng | Nguyên lý | Vấn đề | Hậu quả | Cách sửa |
|---|------|-----------|--------|---------|----------|
| 1 | 35-64 | SRP | `CreateClub` vừa kiểm tra tên, vừa lưu, ghi log, vừa gửi thông báo | Đổi một việc phải sửa cả method; khó test riêng từng việc | Đưa luật kiểm tra vào `Club.Create`; tách `ClubService` |
| 2 | 48, 79, 94, 111 | DIP | Gọi thẳng `File.AppendAllText` ở 4 chỗ | Không test được nếu không ghi file thật; không đổi được nơi ghi log | Interface `IAppLogger`, nhận qua constructor |
| 3 | 52-63 | OCP | Chuỗi `if gmail / else if yahoo / else` chọn cách gửi | Thêm kênh SMS/Slack phải sửa code đã chạy ổn | `INotificationService` + Factory |
| 4 | 82-83 | OCP, SRP | `AddMemberToClub` cứng việc gửi email và Slack | Thêm hoặc bỏ kênh phải sửa logic nghiệp vụ | Gọi qua `INotificationService` |
| 5 | 72 | SRP | Số 50 viết cứng ở service, trong khi `Club.MaxMembers` đã có | Luật nghiệp vụ nằm sai chỗ, dễ lệch nhau | `club.HasRoom(count)` |
| 6 | 96-97 | SRP, OCP | `CreateEvent` tự gửi mail cho từng thành viên | Muốn thêm kiểu thông báo phải sửa `CreateEvent` | Observer (`IEventObserver`) |
| 7 | 103-113 | SRP, OCP | `PrintClubReport` vừa tính, vừa in, vừa ghi log; chỉ in console | Muốn xuất CSV phải sửa method | Strategy (`IReportStrategy`) |
| 8 | 30-31, 116 | DIP, ISP | Dữ liệu là `List` nằm trong class; `GetAllClubs()` trả danh sách gốc | Bên ngoài sửa được dữ liệu; không thay được nơi lưu | `IClubRepository` / `IClubReader`, trả `IReadOnlyList` |