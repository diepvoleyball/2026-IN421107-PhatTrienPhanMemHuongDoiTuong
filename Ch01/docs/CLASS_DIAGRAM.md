# Sơ đồ lớp EduHub (Mermaid)

```mermaid
classDiagram
    direction LR

    class Club {
        +int Id
        +string Name
        +IReadOnlyList~Member~ Members
        +Create(id, name) Club$
        +AddMember(member)
        +RemoveMember(member)
    }

    class Member {
        +int Id
        +string FullName
        +string Email
        +bool Active
        +Create(id, fullName, email) Member$
        +IsActive() bool
        +Deactivate()
        +Reactivate()
    }

    class Event {
        +int Id
        +int ClubId  «FK»
        +string Title
        +int Capacity
        +DateTime StartTime
        +bool Cancelled
        +Create(id, clubId, title, capacity, startTime) Event$
        +Cancel(now)
    }

    class Registration {
        +int Id
        +int EventId  «FK»
        +int MemberId  «FK»
        +RegistrationStatus Status
        +Create(id, eventId, memberId) Registration$
        +Confirm()
        +Cancel()
    }

    class CheckIn {
        +int Id
        +int RegistrationId  «FK»
        +DateTime CheckedInAt
        +Create(id, registration, checkedInAt) CheckIn$
    }

    class RegistrationStatus {
        <<enumeration>>
        Pending
        Confirmed
        Cancelled
    }

    Club "1" o-- "0..*" Member : có thành viên
    Club "1" --> "0..*" Event : tổ chức (Event.ClubId)
    Event "1" --> "0..*" Registration : nhận (Registration.EventId)
    Member "1" --> "0..*" Registration : đăng ký (Registration.MemberId)
    Registration "1" --> "0..1" CheckIn : điểm danh (CheckIn.RegistrationId)
    Registration ..> RegistrationStatus : dùng
```

## Giải thích quan hệ và khoá ngoại

| Quan hệ | Bội số | Khoá ngoại (phía "nhiều") |
|---|---|---|
| Club – Member | 1 – nhiều (danh sách `Members`) | không có FK, Club giữ danh sách |
| Club – Event | 1 – nhiều | `Event.ClubId` |
| Event – Registration | 1 – nhiều | `Registration.EventId` |
| Member – Registration | 1 – nhiều | `Registration.MemberId` |
| Registration – CheckIn | 1 – 0..1 | `CheckIn.RegistrationId` |
