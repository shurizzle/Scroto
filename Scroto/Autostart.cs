using System;
using Microsoft.Win32;
using System.Reflection;
using System.Windows.Forms;

namespace Scroto
{
  class Autostart
  {
    public static readonly string RunLocation = @"Software\Microsoft\Windows\CurrentVersion\Run";
    public static readonly string Key = Application.ProductName;

    private static string m_ExePath;
    public static string ExePath
    {
      get
      {
        if (m_ExePath == null)
        {
          m_ExePath = Assembly.GetExecutingAssembly().Location;
        }

        return m_ExePath;
      }
    }

    private static string m_Command;
    public static string Command
    {
      get
      {
        if (m_Command == null)
        {
          m_Command = String.Format("{0} /autostart", ExePath);
        }

        return m_Command;
      }
    }

    public static bool IsEnabled
    {
      get
      {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(RunLocation);
        if (key == null)
        {
          return false;
        }

        string value = (string)key.GetValue(Key);
        if (value == null)
          return false;

        return (value == Command);
      }
    }

    public static void Enable()
    {
      RegistryKey key = Registry.CurrentUser.CreateSubKey(RunLocation);
      key.SetValue(Key, Command);
    }

    public static void Disable()
    {
      RegistryKey key = Registry.CurrentUser.CreateSubKey(RunLocation);
      key.DeleteValue(Key);
    }
  }
}
