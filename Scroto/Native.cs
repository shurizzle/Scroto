using System;
using System.Drawing;
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
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
      public uint cbSize;
      public RECT rcWindow;
      public RECT rcClient;
      public uint dwStyle;
      public uint dwExStyle;
      public uint dwWindowStatus;
      public uint cxWindowBorders;
      public uint cyWindowBorders;
      public ushort atomWindowType;
      public ushort wCreatorVersion;

      public WINDOWINFO(Boolean? filler)
        : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
      {
        cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
      }
    }
    #endregion

    #region Delegates
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    #endregion

    #region Constants
    public static readonly uint WM_SHOWME = RegisterWindowMessage("WM_SHOWME");

    public enum ShowWindowCommands : int
    {
      Hide = 0,
      Normal = 1,
      ShowMinimized = 2,
      Maximize = 3,
      ShowMaximized = 3,
      ShowNoActivate = 4,
      Show = 5,
      Minimize = 6,
      ShowMinNoActive = 7,
      ShowNA = 8,
      Restore = 9,
      ShowDefault = 10,
      ForceMinimize = 11
    }

    public enum GetWindow_Cmd : uint
    {
      GW_HWNDFIRST = 0,
      GW_HWNDLAST = 1,
      GW_HWNDNEXT = 2,
      GW_HWNDPREV = 3,
      GW_OWNER = 4,
      GW_CHILD = 5,
      GW_ENABLEDPOPUP = 6
    }

    #endregion

    #region Functions
    [DllImport("user32.dll")]
    public static extern IntPtr GetTopWindow(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
    public static extern uint RegisterWindowMessage(string lpString);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
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

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    [DllImport("gdi32.dll")]
    public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest,
      int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);
    [DllImport("user32.dll")]
    public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);
    [DllImport("gdi32.dll")]
    public static extern IntPtr DeleteDC(IntPtr hDc);
    [DllImport("gdi32.dll")]
    public static extern IntPtr DeleteObject(IntPtr hDc);
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr ptr);

    #endregion
  }
}
