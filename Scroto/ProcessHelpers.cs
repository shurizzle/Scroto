using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Scroto
{
  public static class Process
  {
    private static string m_AssemblyGuid = null;

    public static string AssemblyGuid
    {
      get
      {
        if (m_AssemblyGuid == null)
        {
          object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
          if (attributes.Length == 0)
          {
            m_AssemblyGuid = String.Empty;
          }
          m_AssemblyGuid = ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
        }

        return m_AssemblyGuid;
      }
    }

    private static string m_FirstInstanceMessageName;

    public static string FirstInstanceMessageName
    {
      get
      {
        if (m_FirstInstanceMessageName == null)
        {
          m_FirstInstanceMessageName = String.Format("WM_SHOWFIRSTINSTANCE|{0}", AssemblyGuid);
        }

        return m_FirstInstanceMessageName;
      }
    }

    private static uint m_FirstInstanceMessage = 0;

    public static uint FirstInstanceMessage
    {
      get
      {
        if (m_FirstInstanceMessage == 0)
        {
          m_FirstInstanceMessage = Native.RegisterWindowMessage(FirstInstanceMessageName);
        }

        return m_FirstInstanceMessage;
      }
    }

    private static string m_MutexName = null;

    public static string MutexName
    {
      get
      {
        if (m_MutexName == null)
        {
          m_MutexName = String.Format(@"Local\{0}", AssemblyGuid);
        }

        return m_MutexName;
      }
    }

    private static Mutex m_Mutex = null;
    private static bool m_OnlyInstance;

    public static Mutex Mutex
    {
      get
      {
        if (m_Mutex == null)
        {
          m_Mutex = new Mutex(true, MutexName, out m_OnlyInstance);
        }

        return m_Mutex;
      }
    }

    public static bool OnlyInstance
    {
      get
      {
        if (m_Mutex == null)
        {
          m_Mutex = new Mutex(true, MutexName, out m_OnlyInstance);
        }

        return m_OnlyInstance;
      }
    }

    public static readonly IntPtr HWND_Broadcast = new IntPtr(0xffff);
    public static readonly HandleRef HBroadcast = new HandleRef(null, HWND_Broadcast);

    public static void ShowInstance()
    {
      if (!OnlyInstance)
      {
        Native.PostMessage(HBroadcast, FirstInstanceMessage,
          IntPtr.Zero, IntPtr.Zero);
      }
    }
  }
}
