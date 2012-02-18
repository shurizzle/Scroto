using System;
using System.Runtime.InteropServices;

namespace Scroto
{
  internal class NativeMethods
  {
    public struct RECT
    {
      public int Left;    // Specifies the x-coordinate of the upper-left corner of the rectangle.
      public int Top;        // Specifies the y-coordinate of the upper-left corner of the rectangle.
      public int Right;    // Specifies the x-coordinate of the lower-right corner of the rectangle.
      public int Bottom;    // Specifies the y-coordinate of the lower-right corner of the rectangle.

    }

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

    public delegate bool EnumWindowsProc(int hWnd, int lParam);

    public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
    public static readonly int SW_SHOW = 5;

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
  }
}
