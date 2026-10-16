# EduHub – User Stories (Ch01)

Hệ thống EduHub: quản lý câu lạc bộ (CLB), sự kiện, đăng ký và điểm danh.
Ba vai trò (actor): **Sinh viên**, **Ban CLB**, **Admin**.

---

## US-01 · Sinh viên: hủy đăng ký sự kiện
*Là sinh viên, tôi muốn hủy phiếu đăng ký để nhường chỗ khi không đi được.*

**Tiêu chí nghiệm thu**
1. **Given** tôi có phiếu Pending hoặc Confirmed, **When** tôi hủy, **Then** phiếu chuyển sang `Cancelled`.
2. **Given** phiếu đã `Cancelled`, **When** tôi hủy lần nữa, **Then** hệ thống báo "Phiếu đã bị hủy trước đó".

## US-02 · Sinh viên: rời khỏi một CLB
*Là sinh viên, tôi muốn rời CLB mà tôi không còn tham gia.*

**Tiêu chí nghiệm thu**
1. **Given** tôi đang là thành viên CLB, **When** tôi rời CLB, **Then** tên tôi không còn trong `club.Members`.
2. **Given** tôi chưa là thành viên, **When** tôi rời CLB, **Then** hệ thống báo lỗi "Chưa là thành viên của CLB".

## US-03 · Ban CLB: xác nhận phiếu đăng ký
*Là Ban CLB, tôi muốn xác nhận phiếu đăng ký để chốt danh sách tham dự.*

**Tiêu chí nghiệm thu**
1. **Given** phiếu đang `Pending`, **When** tôi xác nhận, **Then** phiếu chuyển sang `Confirmed`.
2. **Given** phiếu đã `Cancelled`, **When** tôi xác nhận, **Then** hệ thống báo "Không thể xác nhận phiếu đang ở trạng thái Cancelled".

## US-04 · Ban CLB: xem tỷ lệ tham dự
*Là Ban CLB, tôi muốn xem tỷ lệ người đã check-in trên số phiếu đã xác nhận.*

**Tiêu chí nghiệm thu**
1. **Given** có 2 phiếu Confirmed và 1 người đã check-in, **When** tôi xem báo cáo, **Then** tỷ lệ tham dự là 50%.
2. **Given** chưa có phiếu Confirmed nào, **When** tôi xem báo cáo, **Then** tỷ lệ là 0%, không bị lỗi chia cho 0.

## US-05 · Admin: ngừng và kích hoạt lại thành viên
*Là Admin, tôi muốn kích hoạt lại thành viên đã bị ngừng.*

**Tiêu chí nghiệm thu**
1. **Given** thành viên đang bị ngừng (`IsActive()` = false), **When** tôi kích hoạt lại, **Then** `IsActive()` = true.
2. **Given** thành viên đang hoạt động, **When** tôi kích hoạt lại, **Then** hệ thống báo "Thành viên đang hoạt động".

## US-06 · Sinh viên: check-in tại sự kiện bằng phiếu hợp lệ
*Là sinh viên, tôi muốn được điểm danh khi đến sự kiện.*

**Tiêu chí nghiệm thu**
1. **Given** phiếu `Confirmed`, **When** tôi check-in, **Then** hệ thống tạo `CheckIn` gắn với phiếu đó.
2. **Given** phiếu `Pending` hoặc `Cancelled`, **When** tôi check-in, **Then** hệ thống từ chối.

---

| Vai trò | Story | Nội dung |
|---|---|---|
| Sinh viên | US-01, US-02, US-06 | hủy đăng ký, rời CLB, check-in |
| Ban CLB | US-03, US-04 | xác nhận phiếu, xem tỷ lệ tham dự |
| Admin | US-05 | kích hoạt lại thành viên |