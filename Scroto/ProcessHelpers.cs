using System;

namespace Scroto
{
  public static class ProcessHelpers
  {
    class _GetWindow
    {
      public IntPtr Result { get; set; }
      private uint _id = 0;

      public _GetWindow(uint id)
      {
        _id = id;

        NativeMethods.EnumWindowsProc ewp = new NativeMethods.EnumWindowsProc(Check);
        NativeMethods.EnumWindows(ewp, 0);
      }

      private bool Check(int hWnd, int lParam)
      {
        uint pid = 0;
        NativeMethods.GetWindowThreadProcessId(new IntPtr(hWnd), out pid);

        if (pid == _id)
        {
          Result = new IntPtr(hWnd);
          return false;
        }

        return true;
      }
    }

    static public IntPtr GetWindowHandle(this System.Diagnostics.Process proc)
    {
      return (new _GetWindow((uint)proc.Id)).Result;
    }

    static public bool IsVisible(this System.Diagnostics.Process proc)
    {
      NativeMethods.WINDOWINFO pwi = new NativeMethods.WINDOWINFO();
      IntPtr handle = proc.MainWindowHandle;
      return NativeMethods.GetWindowInfo(handle, ref pwi);
    }

    static public void Show(this System.Diagnostics.Process proc)
    {
      if (!proc.IsVisible())
        NativeMethods.ShowWindow(proc.GetWindowHandle(), NativeMethods.SW_SHOW);

      NativeMethods.PostMessage(
        proc.MainWindowHandle,
        NativeMethods.WM_SHOWME,
        IntPtr.Zero,
        IntPtr.Zero);

      NativeMethods.SetForegroundWindow(proc.MainWindowHandle);
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
