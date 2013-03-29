using System;
using System.Runtime.InteropServices;

namespace Scroto
{
  public static class ProcessHelpers
  {
    static public IntPtr GetWindowHandle(this System.Diagnostics.Process proc)
    {
      IntPtr res = IntPtr.Zero;
      Native.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
      {
        uint pid = 0;
        Native.GetWindowThreadProcessId(hWnd, out pid);

        if (pid == proc.Id)
        {
          res = hWnd;
          return false;
        }

        return true;
      }, IntPtr.Zero);

      return res;
    }

    static public bool IsVisible(this System.Diagnostics.Process proc)
    {
      Native.WINDOWINFO pwi = new Native.WINDOWINFO();
      IntPtr handle = proc.MainWindowHandle;
      return Native.GetWindowInfo(handle, ref pwi);
    }

    static public void Show(this System.Diagnostics.Process proc)
    {
      if (!proc.IsVisible())
        Native.ShowWindow(proc.GetWindowHandle(), Native.ShowWindowCommands.Show);

      Native.PostMessage(
        new HandleRef(proc, proc.MainWindowHandle),
        Native.WM_SHOWME,
        IntPtr.Zero,
        IntPtr.Zero);

      Native.SetForegroundWindow(proc.MainWindowHandle);
    }
  }

  public static class Process
  {
    static public System.Diagnostics.Process Instance
    {
      get
      {
        var current = System.Diagnostics.Process.GetCurrentProcess();
        foreach (var process in System.Diagnostics.Process.GetProcessesByName(current.ProcessName))
        {
          if (process.Id != current.Id)
          {
            current = process;
            break;
          }
        }

        return current;
      }
    }
  }
}
