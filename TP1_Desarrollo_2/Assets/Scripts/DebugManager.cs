using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private static DebugManager instance;
    private List<string> enabledTags = new List<string>();
    private bool logEnabled = true;
    private bool logErrorEnabled = true;
    private bool logWarningEnabled = true;
    private bool drawLineEnabled = true;
    private bool drawRayEnabled = true;

    private DebugManager() { }

    public static DebugManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DebugManager();
            }
            return instance;
        }
    }

    public void Log(string message, string tag)
    {
        if (!logEnabled || !enabledTags.Contains(tag))
        {
            return;
        }
        Debug.Log(tag + ": " + message);
    }

    public void LogError(string message, string tag)
    {
        if (!logErrorEnabled || !enabledTags.Contains(tag))
        {
            return;
        }
        Debug.LogError("<color=red>" + tag + ": " + message + "</color>");
    }

    public void LogWarning(string message, string tag)
    {
        if (!logWarningEnabled || !enabledTags.Contains(tag))
        {
            return;
        }
        Debug.LogWarning("<color=yellow>" + tag + ": " + message + "</color>");
    }

    public void DrawLine(Vector3 start, Vector3 end, Color color, string tag)
    {
        if (!drawLineEnabled || !enabledTags.Contains(tag))
        {
            return;
        }
        Debug.DrawLine(start, end, color);
    }

    public void DrawRay(Vector3 start, Vector3 direction, Color color, string tag)
    {
        if (!drawRayEnabled || !enabledTags.Contains(tag))
        {
            return;
        }
        Debug.DrawRay(start, direction, color);
    }

    public void AddToWhiteList(string tag)
    {
        if (!enabledTags.Contains(tag))
        {
            enabledTags.Add(tag);
        }
    }

    public void RemoveFromWhiteList(string tag)
    {
        enabledTags.Remove(tag);
    }

    public void EnableLog(bool enabled)
    {
        logEnabled = enabled;
    }

    public void EnableLogError(bool enabled)
    {
        logErrorEnabled = enabled;
    }

    public void EnableLogWarning(bool enabled)
    {
        logWarningEnabled = enabled;
    }

    public void EnableDrawLine(bool enabled)
    {
        drawLineEnabled = enabled;
    }

    public void EnableDrawRay(bool enabled)
    {
        drawRayEnabled = enabled;
    }

    // Implementación de la interfaz ILogger de UnityEngine

    public void Log(LogType logType, object message)
    {
        switch (logType)
        {
            case LogType.Log:
                Log((string)message, "Unity");
                break;
            case LogType.Warning:
                LogWarning((string)message, "Unity");
                break;
            case LogType.Error:
            case LogType.Exception:
                LogError((string)message, "Unity");
                break;
        }
    }

    public void Log(LogType logType, object message, UnityEngine.Object context)
    {
        Log(logType, message);
    }

    public void LogFormat(LogType logType, string format, params object[] args)
    {
        string message = string.Format(format, args);
        Log(logType, message);
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        LogFormat(logType, format, args);
    }
}
