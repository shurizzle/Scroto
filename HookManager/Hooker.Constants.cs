using System;

namespace HookManager
{
  public static partial class Hooker
  {
    public const int WH_KEYBOARD = 2;
    public const int WH_MOUSE = 7;
    public const int WH_KEYBOARD_LL = 13;
    public const int WH_MOUSE_LL = 14;
    public const int VK_SHIFT = 0x10;
    public const int VK_CAPITAL = 0x14;
    public const int VK_NUMLOCK = 0x90;
    public const int WM_KEYDOWN = 0x100;
    public const int WM_KEYUP = 0x101;
    public const int WM_SYSKEYDOWN = 0x104;
    public const int WM_SYSKEYUP = 0x105;
    public const int WM_MOUSEMOVE = 0x200;
    public const int WM_LBUTTONDOWN = 0x201;
    public const int WM_LBUTTONUP = 0x202;
    public const int WM_LBUTTONDBLCLK = 0x203;
    public const int WM_RBUTTONDOWN = 0x204;
    public const int WM_RBUTTONUP = 0x205;
    public const int WM_RBUTTONDBLCLK = 0x206;
    public const int WM_MBUTTONDOWN = 0x207;
    public const int WM_MBUTTONUP = 0x208;
    public const int WM_MBUTTONDBLCLK = 0x209;
    public const int WM_MOUSEWHEEL = 0x20A;
  }
}
