using System;
using System.IO;
using System.Text;

public class FileWriter
{
    private string _folder;
    private string _filePath;
    
    private const string DATE_FORMAT = "yyyy-MM-dd";
    private const string MessageFormat = "{0:dd/MM/yyyy HH:mm:ss:ffff} [{1}]: {2}\r";

    public FileWriter(string folder)
    {
        _folder = folder;
        ManagePath();
    }
    private void ManagePath()
    {
        _filePath = $"{_folder}/{DateTime.UtcNow.ToString(DATE_FORMAT)}.log";
    }
    public void Write(LogMessageWriter message)
    {
        string messageToRecord = string.Format(MessageFormat, message.Time, message.Type, message.Message);
        using (FileStream fs = File.Open(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
        {
            var bytes = Encoding.UTF8.GetBytes(messageToRecord);
            fs.Write(bytes, 0, bytes.Length);
        }
    }
}
