namespace EduHub.Ch02;

// Giữ hành vi cũ: ghi vào club-log.txt (file này đã nằm trong .gitignore)
public class FileAppLogger : IAppLogger
{
    private readonly string _path;

    public FileAppLogger(string path = "club-log.txt") => _path = path;

    public void Info(string message)
        => File.AppendAllText(_path, $"[{DateTime.Now}] {message}\n");
}