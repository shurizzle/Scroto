using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HookManager
{
    public static partial class Hooker
    {
      #region Mouse events
      private static event MouseEventHandler s_MouseMove;

      public static event MouseEventHandler MouseMove
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseMove += value;
        }

        remove
        {
          s_MouseMove -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventExHandler s_MouseMoveEx;

      public static event MouseEventExHandler MouseMoveEx
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseMoveEx += value;
        }

        remove
        {
          s_MouseMoveEx -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventHandler s_MouseDown;

      public static event MouseEventHandler MouseDown
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseDown += value;
        }

        remove
        {
          s_MouseDown -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventExHandler s_MouseDownEx;

      public static event MouseEventExHandler MouseDownEx
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseDownEx += value;
        }

        remove
        {
          s_MouseDownEx -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventHandler s_MouseUp;

      public static event MouseEventHandler MouseUp
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseUp += value;
        }

        remove
        {
          s_MouseUp -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventExHandler s_MouseUpEx;

      public static event MouseEventExHandler MouseUpEx
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseUpEx += value;
        }

        remove
        {
          s_MouseUpEx -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventHandler s_MouseWheel;

      public static event MouseEventHandler MouseWheel
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseWheel += value;
        }

        remove
        {
          s_MouseWheel -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventExHandler s_MouseWheelEx;

      public static event MouseEventExHandler MouseWheelEx
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseWheelEx += value;
        }

        remove
        {
          s_MouseWheelEx -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventHandler s_MouseDoubleClick;

      public static event MouseEventHandler MouseDoubleClick
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseDoubleClick += value;
        }

        remove
        {
          s_MouseDoubleClick -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }

      private static event MouseEventExHandler s_MouseDoubleClickEx;

      public static event MouseEventExHandler MouseDoubleClickEx
      {
        add
        {
          SubscribeGlobalMouseEvents();
          s_MouseDoubleClickEx += value;
        }

        remove
        {
          s_MouseDoubleClickEx -= value;
          UnsubscribeGlobalMouseEvents();
        }
      }
      #endregion

      #region Keyboard events
      private static event KeyEventHandler s_KeyDown;

      public static event KeyEventHandler KeyDown
      {
        add
        {
          SubscribeGlobalKeyboardEvents();
          s_KeyDown += value;
        }

        remove
        {
          s_KeyDown -= value;
          UnsubscribeGlobalKeyboardEvents();
        }
      }

      private static event KeyEventHandler s_KeyUp;

      public static event KeyEventHandler KeyUp
      {
        add
        {
          SubscribeGlobalKeyboardEvents();
          s_KeyUp += value;
        }

        remove
        {
          s_KeyUp -= value;
          UnsubscribeGlobalKeyboardEvents();
        }
      }
      #endregion
    }
}
