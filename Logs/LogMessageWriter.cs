using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogMessageWriter
{
    public LogType Type { get; set; }
    public DateTime Time { get; set; }
    public string Message { get; set; }

    public LogMessageWriter(LogType type, string message)
    {
        Type = type;
        Message = message;
        Time = DateTime.UtcNow;
    }
}
