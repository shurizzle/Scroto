using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HookManager
{
  public static partial class Hooker
  {
    public delegate IntPtr HookProc(int nCode, WM wParam, IntPtr lParam);

    #region Mouse things
    private static HookProc s_MouseDelegate;
    private static IntPtr s_MouseHookHandle;

    private static int m_OldX;
    private static int m_OldY;

    private static IntPtr MouseHookProc(int nCode, WM wParam, IntPtr lParam)
    {
      if (nCode >= 0)
      {
        MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)
          Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

        MouseButtons button = MouseButtons.None;

        short mouseDelta = 0;
        int clickCount = 0;
        bool mouseDown = false;
        bool mouseUp = false;

        switch (wParam)
        {
          case WM.LBUTTONDOWN:
            mouseDown = true;
            button = MouseButtons.Left;
            clickCount = 1;
            break;
          case WM.LBUTTONUP:
            mouseUp = true;
            button = MouseButtons.Left;
            clickCount = 1;
            break;
          case WM.MBUTTONDBLCLK:
            button = MouseButtons.Left;
            clickCount = 2;
            break;
          case WM.RBUTTONDOWN:
            mouseDown = true;
            button = MouseButtons.Right;
            clickCount = 1;
            break;
          case WM.RBUTTONUP:
            mouseUp = true;
            button = MouseButtons.Right;
            clickCount = 1;
            break;
          case WM.RBUTTONDBLCLK:
            button = MouseButtons.Right;
            clickCount = 2;
            break;
          case WM.MOUSEWHEEL:
            mouseDelta = (short) ((mouseHookStruct.mouseData >> 16) & 0xffff);
            break;
        }

        MouseEventExArgs e = new MouseEventExArgs(button, clickCount,
          mouseHookStruct.pt.x, mouseHookStruct.pt.y, mouseDelta);

        if (clickCount == 2)
        {
          if (s_MouseDoubleClick != null)
          {
            s_MouseDoubleClick.Invoke(null, e);
          }

          if (s_MouseDoubleClickEx != null)
          {
            s_MouseDoubleClickEx.Invoke(null, e);
          }
        }

        if (mouseUp)
        {
          if (s_MouseUp != null)
          {
            s_MouseUp.Invoke(null, e);
          }

          if (s_MouseUpEx != null)
          {
            s_MouseUpEx.Invoke(null, e);
          }
        }

        if (mouseDown)
        {
          if (s_MouseDown != null)
          {
            s_MouseDown.Invoke(null, e);
          }

          if (s_MouseDownEx != null)
          {
            s_MouseDownEx.Invoke(null, e);
          }
        }

        if (mouseDelta != 0)
        {
          if (s_MouseWheel != null)
          {
            s_MouseWheel.Invoke(null, e);
          }

          if (s_MouseWheelEx != null)
          {
            s_MouseWheelEx.Invoke(null, e);
          }
        }

        if (m_OldX != mouseHookStruct.pt.x || m_OldY != mouseHookStruct.pt.y)
        {
          m_OldX = mouseHookStruct.pt.x;
          m_OldY = mouseHookStruct.pt.y;

          if (s_MouseMove != null)
          {
            s_MouseMove.Invoke(null, e);
          }

          if (s_MouseMoveEx != null)
          {
            s_MouseMoveEx.Invoke(null, e);
          }
        }

        if (e.Handled)
        {
          return new IntPtr(-1);
        }
      }

      return CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
    }

    private static void SubscribeGlobalMouseEvents()
    {
      if (s_MouseHookHandle == IntPtr.Zero)
      {
        s_MouseDelegate = MouseHookProc;
        s_MouseHookHandle = SetWindowsHookEx(HookType.WH_MOUSE_LL, s_MouseDelegate,
          System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress,
          0);

        if (s_MouseHookHandle == IntPtr.Zero)
        {
          int errCode = Marshal.GetLastWin32Error();
          throw new Win32Exception(errCode);
        }
      }
    }

    private static void UnsubscribeGlobalMouseEvents()
    {
      if (s_MouseDoubleClick == null &&
        s_MouseDoubleClickEx == null &&
        s_MouseDown == null &&
        s_MouseDownEx == null &&
        s_MouseUp == null &&
        s_MouseUpEx == null &&
        s_MouseWheel == null &&
        s_MouseWheelEx == null &&
        s_MouseMove == null &&
        s_MouseMoveEx == null &&
        s_MouseHookHandle != IntPtr.Zero)
      {
        bool result = UnhookWindowsHookEx(s_MouseHookHandle);
        s_MouseHookHandle = IntPtr.Zero;
        s_MouseDelegate = null;

        if (!result)
        {
          int errCode = Marshal.GetLastWin32Error();
          throw new Win32Exception(errCode);
        }
      }
    }
    #endregion

    #region Keyboard things
    private static HookProc s_KeyboardDelegate;
    private static IntPtr s_KeyboardHookHandle;

    private static IntPtr KeyboardHookProc(int nCode, WM wParam, IntPtr lParam)
    {
      bool handled = false;

      if (nCode >= 0)
      {
        KeyboardHookStruct kbHookStruct = (KeyboardHookStruct)
          Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
        Keys keyData = (Keys)kbHookStruct.vkCode;
        KeyEventArgs e = new KeyEventArgs(keyData);

        if (s_KeyDown != null && (wParam == WM.KEYDOWN ||
            wParam == WM.SYSKEYDOWN))
        {
          s_KeyDown.Invoke(null, e);
          handled = e.Handled;
        }

        if (s_KeyUp != null && (wParam == WM.KEYUP ||
            wParam == WM.SYSKEYUP))
        {
          s_KeyUp.Invoke(null, e);
          handled = e.Handled;
        }

        if (handled)
        {
          return new IntPtr(-1);
        }
      }

      return CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
    }

    private static void SubscribeGlobalKeyboardEvents()
    {
      if (s_KeyboardHookHandle == IntPtr.Zero)
      {
        s_KeyboardDelegate = KeyboardHookProc;
        s_KeyboardHookHandle = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, s_KeyboardDelegate,
          System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress,
          0);

        if (s_KeyboardHookHandle == IntPtr.Zero)
        {
          int errCode = Marshal.GetLastWin32Error();
          throw new Win32Exception(errCode);
        }
      }
    }

    private static void UnsubscribeGlobalKeyboardEvents()
    {
      if (s_KeyDown == null &&
        s_KeyUp == null &&
        s_KeyboardHookHandle != IntPtr.Zero)
      {
        bool res = UnhookWindowsHookEx(s_KeyboardHookHandle);
        s_KeyboardHookHandle = IntPtr.Zero;
        s_KeyboardDelegate = null;

        if (!res)
        {
          int errCode = Marshal.GetLastWin32Error();
          throw new Win32Exception(errCode);
        }
      }
    }
    #endregion
  }
}
