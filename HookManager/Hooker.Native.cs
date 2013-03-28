using System;
using System.Runtime.InteropServices;

namespace HookManager
{
  public static partial class Hooker
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
      SetLastError = true)]
    public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, int hMod, int dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
      SetLastError = true)]
    public static extern int UnhookWindowsHookEx(int idHook);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern int GetDoubleClickTime();

    [DllImport("user32.dll")]
    public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState,
      byte[] lpwTransKey, int fuState);

    [DllImport("user32.dll")]
    public static extern int GetKeyboardState(byte[] pbKeyState);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern short GetKeyState(int vKey);
  }
}
