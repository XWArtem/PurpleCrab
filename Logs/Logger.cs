using UnityEngine;
using System.IO;

public class Logger : MonoBehaviour
{
    private string _workDirectory;
    private FileWriter _fileWriter;

    private void Awake()
    {
        _workDirectory = $"{ Application.dataPath}/Internal/Logs";
        if (!Directory.Exists(_workDirectory))
        {
            Directory.CreateDirectory(_workDirectory);
        }
        Application.logMessageReceived += OnLogMessageReceived;
        _fileWriter = new FileWriter(_workDirectory);
    }

    private void OnLogMessageReceived(string condition, string stackTrace, LogType type)
    {
        _fileWriter.Write(new LogMessageWriter(type, condition));
    }
}
