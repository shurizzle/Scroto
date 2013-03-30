using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HookManager;

namespace Scroto
{
  public partial class Cropper : Form
  {
    private Point start;
    private Point current;
    private bool Capturing;
    public event Action<Rectangle> OnSuccess;
    public event Action OnHookFailure;
    public event Action OnInputFailure;
    private Rectangle CapturingRectangle
    {
      get
      {
        int fx, sx, fy, sy;

        if (start.X > current.X)
        {
          fx = current.X;
          sx = start.X;
        }
        else
        {
          fx = start.X;
          sx = current.X;
        }

        if (start.Y > current.Y)
        {
          fy = current.Y;
          sy = start.Y;
        }
        else
        {
          fy = start.Y;
          sy = current.Y;
        }

        return new Rectangle(fx, fy, sx - fx, sy - fy);
      }
    }

    Form back;

    public Cropper() {}

    public void Run()
    {
      back = new Form();
      back.BackColor = back.TransparencyKey = Color.Fuchsia;
      back.ControlBox = back.ShowIcon = back.ShowInTaskbar = false;
      back.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      back.Opacity = 255;
      back.Paint += back_Paint;
      back.Show();

      InitializeComponent();
      back.Top = Top = Screen.AllScreens.Select(x => x.Bounds.X).Min();
      back.Left = Left = Screen.AllScreens.Select(x => x.Bounds.Y).Min();
      back.Height = Height = Screen.AllScreens.Select(x => x.Bounds.Bottom).Max() - Top;
      back.Width = Width = Screen.AllScreens.Select(x => x.Bounds.Right).Max() - Left;
      Capturing = false;

      GrabAll();
      Show();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        UngrabAll();
        if (components != null)
        {
          components.Dispose();
        }
        back.Dispose();
      }
      base.Dispose(disposing);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
      base.OnFormClosed(e);
      back.Close();
    }

    private void GrabAll()
    {
      try
      {
        Hooker.KeyUp += Cropper_KeyUp;
        Hooker.KeyDown += Cropper_KeyDown;
        Hooker.MouseDownEx += Cropper_MouseDownEx;
        Hooker.MouseUpEx += Cropper_MouseUpEx;
        Hooker.MouseMove += Cropper_MouseMove;
        Hooker.MouseWheelEx += Cropper_MouseWheelEx;
        Hooker.MouseDoubleClickEx += Cropper_MouseDoubleClickEx;
      }
      catch (Win32Exception)
      {
        UngrabAll();
        Close();
        if (OnHookFailure != null)
        {
          OnHookFailure.Invoke();
        }
      }
    }

    private void UngrabAll()
    {
      Action<Action> f = delegate(Action ff)
      {
        try
        {
          ff();
        }
        catch (Win32Exception) {}
      };

      f(() => Hooker.KeyUp -= Cropper_KeyUp);
      f(() => Hooker.KeyDown -= Cropper_KeyDown);
      f(() => Hooker.MouseDownEx -= Cropper_MouseDownEx);
      f(() => Hooker.MouseUpEx -= Cropper_MouseUpEx);
      f(() => Hooker.MouseMove -= Cropper_MouseMove);
      f(() => Hooker.MouseWheelEx -= Cropper_MouseWheelEx);
      f(() => Hooker.MouseDoubleClickEx -= Cropper_MouseDoubleClickEx);
    }

    private void back_Paint(object sender, PaintEventArgs e)
    {
      Rectangle dr = new Rectangle(DisplayRectangle.X, DisplayRectangle.Y,
        DisplayRectangle.Width, DisplayRectangle.Height);

      e.Graphics.FillRectangle(new SolidBrush(Color.Fuchsia), dr);

      if (Capturing)
      {
        e.Graphics.DrawRectangle(new Pen(Color.Black, 1), CapturingRectangle);
      }
    }

    private void Cropper_MouseDoubleClickEx(object sender, MouseEventExArgs e)
    {
      e.Handled = true;
      Close();
      if (OnInputFailure != null)
      {
        OnInputFailure.Invoke();
      }
    }

    private void Cropper_KeyUp(object sender, KeyEventArgs e)
    {
      e.Handled = true;
      Close();
      if (OnInputFailure != null)
      {
        OnInputFailure.Invoke();
      }
    }

    private void Cropper_KeyDown(object sender, KeyEventArgs e)
    {
      e.Handled = true;
    }

    private void Cropper_MouseWheelEx(object sender, MouseEventExArgs e)
    {
      e.Handled = true;
    }

    private void Cropper_MouseUpEx(object sender, MouseEventExArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        UngrabAll();
        e.Handled = true;
        Close();
        if (OnSuccess != null)
        {
          OnSuccess.Invoke(CapturingRectangle);
        }
      }
    }

    private void Cropper_MouseDownEx(object sender, MouseEventExArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        start = new Point(e.X, e.Y);
        current = new Point(e.X, e.Y);
        Capturing = true;
        back.Invalidate();
      }
      else
      {
        Close();
        if (OnInputFailure != null)
        {
          OnInputFailure.Invoke();
        }
      }
      e.Handled = true;
    }

    private void Cropper_MouseMove(object sender, MouseEventArgs e)
    {
      if (Capturing)
      {
        current.X = e.X;
        current.Y = e.Y;

        back.Invalidate();
      }
    }
  }
}
