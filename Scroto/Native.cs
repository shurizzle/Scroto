using System;
using System.Runtime.InteropServices;

namespace Scroto
{
  internal class Native
  {
    #region Structures
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;

      public static implicit operator System.Drawing.Rectangle(RECT self)
      {
        return new System.Drawing.Rectangle(self.Left, self.Top,
          self.Right - self.Left, self.Bottom - self.Top);
      }

      public static implicit operator RECT(System.Drawing.Rectangle self)
      {
        RECT res = new RECT();
        res.Left = self.Left;
        res.Top = self.Top;
        res.Right = self.Right;
        res.Bottom = self.Bottom;
        return res;
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
      public uint cbSize;
      public RECT rcWindow; //holds the coords of the window
      public RECT rcClient;
      public uint dwStyle;
      public uint dwExStyle;
      public uint dwWindowStatus;
      public uint cxWindowBorders;
      public uint cyWindowBorders;
      public ushort atomWindowType;
      public ushort wCreatorVersion;
    }
    #endregion

    #region Delegates
    public delegate bool EnumWindowsProc(int hWnd, int lParam);
    #endregion

    #region Constants
    public static readonly uint GW_HWNDFIRST = 0;
    public static readonly uint GW_HWNDLAST = 1;
    public static readonly uint GW_HWNDNEXT = 2;
    public static readonly uint GW_HWNDPREV = 3;
    public static readonly uint GW_OWNER = 4;
    public static readonly uint GW_CHILD = 5;
    public static readonly uint GW_ENABLEDPOPUP = 6;

    public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
    public static readonly int SW_SHOW = 5;
    #endregion

    #region Functions
    [DllImport("user32.dll")]
    public static extern IntPtr GetTopWindow(IntPtr hwnd);
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindow(IntPtr hwnd, uint uCmd);
    [DllImport("user32.dll")]
    public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll")]
    public static extern int EnumWindows(EnumWindowsProc ewp, int lParam);
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow); 
    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
    [DllImport("user32.dll")]
    public static extern int RegisterWindowMessage(string message);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool BringWindowToTop(IntPtr hWnd);
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    public static extern uint GetCurrentThreadId();
    [DllImport("user32.dll")]
    public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);
    #endregion
  }
}
