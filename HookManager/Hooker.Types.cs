using System;
using System.Runtime.InteropServices;

namespace HookManager
{
  public static partial class Hooker
  {
    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
      public int x;
      public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MouseLLHookStruct
    {
      public POINT pt;
      public int mouseData;
      public int flags;
      public int time;
      public int dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class KeyboardHookStruct
    {
      public int vkCode;
      public int scanCode;
      public int flags;
      public int time;
      public int dwExtraInfo;
    }
  }
}
