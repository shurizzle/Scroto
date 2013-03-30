using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Reflection;
using HookManager;
using System.ComponentModel;
using System.Collections.Generic;

namespace Scroto
{
  public partial class Scroto : Form
  {
    static private string apiKey = "54bf7d24bb8f12f638f40c41abcd65ee";
    static private Regex ImageRe = new Regex("<original_image>(.*)</original_image>");

    bool forceClose = false;

    private Pair<bool, FormWindowState> prev = new Pair<bool, FormWindowState>();

    public Scroto()
    {
      InitializeComponent();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);

      if (e.CloseReason == CloseReason.WindowsShutDown ||
        forceClose) return;

      Hide();
      e.Cancel = true;
    }

    protected void ForceClose()
    {
      forceClose = true;
      Close();
    }

    private void Scroto_Resize(object sender, EventArgs e)
    {
      if (WindowState == FormWindowState.Minimized)
        Hide();
    }

    private void esciToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ForceClose();
    }

    private void shootToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShootAll();
    }

    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != System.Windows.Forms.MouseButtons.Left) return;

      if (Visible)
        Hide();
      else
        Show();
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == Process.FirstInstanceMessage)
      {
        Show();
      }
      base.WndProc(ref m);
    }

    public void Show()
    {
      if (!Visible)
        base.Show();

      if (WindowState != FormWindowState.Normal)
        WindowState = FormWindowState.Normal;

      bool topMost = TopMost;
      TopMost = true;
      TopMost = topMost;
      ForceForegroundWindow(Handle);
    }

    private static string GetDefaultBrowserPath()
    {
      string key = @"htmlfile\shell\open\command";
      RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);
      return ((string)registryKey.GetValue(null, null)).Split('"')[1];
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
      }
      catch (Exception e1)
      {
        if (e1.GetType().ToString() != "System.ComponentModel.Win32Exception")
        {
          try
          {
            System.Diagnostics.Process.Start(GetDefaultBrowserPath(), e.Link.LinkData.ToString());
          }
          catch { }
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      ShootAll();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      GrabAll();
    }

    private void GrabAll()
    {
      try
      {
        Hooker.KeyUp += globalEventProvider_KeyUp;
        Hooker.KeyDown += globalEventProvider_KeyDown;
        Hooker.MouseDownEx += globalEventProvider_MouseDownEx;
        Hooker.MouseUpEx += globalEventProvider_MouseUpEx;
        Hooker.MouseWheelEx += globalEventProvider_MouseWheelEx;
        Hooker.MouseDoubleClickEx += globalEventProvider_MouseDoubleClickEx;
        PreShoot();
      }
      catch (Win32Exception)
      {
        UngrabAll();
        GrabFailure();
      }
    }

    private void GrabFailure()
    {
      MessageBox.Show("Impossibile prendere il controllo del desktop", "Error");
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

      f(() => Hooker.KeyUp -= globalEventProvider_KeyUp);
      f(() => Hooker.KeyDown -= globalEventProvider_KeyDown);
      f(() => Hooker.MouseDownEx -= globalEventProvider_MouseDownEx);
      f(() => Hooker.MouseUpEx -= globalEventProvider_MouseUpEx);
      f(() => Hooker.MouseWheelEx -= globalEventProvider_MouseWheelEx);
      f(() => Hooker.MouseDoubleClickEx -= globalEventProvider_MouseDoubleClickEx);
    }

    private void globalEventProvider_KeyDown(object sender, KeyEventArgs e)
    {
      UngrabAll();
      e.Handled = true;
    }

    private void globalEventProvider_KeyUp(object sender, KeyEventArgs e)
    {
      e.Handled = true;
    }

    private void globalEventProvider_MouseDownEx(object sender, MouseEventExArgs e)
    {
      e.Handled = true;
    }

    private void globalEventProvider_MouseUpEx(object sender, MouseEventExArgs e)
    {
      OnMouseClick(e);
    }

    private void globalEventProvider_MouseDoubleClickEx(object sender, MouseEventExArgs e)
    {
      OnMouseClick(e);
    }

    private void globalEventProvider_MouseWheelEx(object sender, MouseEventExArgs e)
    {
      e.Handled = true;
    }

    private void OnMouseClick(MouseEventExArgs e)
    {
      UngrabAll();
      e.Handled = true;

      if (e.Button == MouseButtons.Left)
      {
        var win = ZOrderVisibleWindows().Select<IntPtr, Pair<IntPtr, Rectangle>>(delegate(IntPtr hwnd, int idx)
        {
          Native.WINDOWINFO wi = new Native.WINDOWINFO();
          Native.GetWindowInfo(hwnd, ref wi);
          return new Pair<IntPtr, Rectangle>(hwnd, wi.rcWindow);
        }).First(x => x.Second.Contains(e.X, e.Y));

        ForceForegroundWindow(win.First);

        Size sz = new Size(Screen.AllScreens.Select(x => x.Bounds.X + x.Bounds.Width).Max(),
          Screen.AllScreens.Select(x => x.Bounds.Y + x.Bounds.Height).Max());

        int X = win.Second.X < 0 ? 0 : win.Second.X;
        int Y = win.Second.Y < 0 ? 0 : win.Second.Y;
        int width = win.Second.Right > sz.Width ? sz.Width - X : win.Second.Width;
        int height = win.Second.Bottom > sz.Height ? sz.Height - Y : win.Second.Height;
        Rectangle crop = new Rectangle(X, Y, width, height);

        Shooting(delegate { return UploadToImgur(TakeScreenshot(crop)); }, false);
      }
      else
      {
        InputFailure();
      }
    }

    private void InputFailure()
    {
      MessageBox.Show("Tasto non valido", "Error");
    }

    private IEnumerable<IntPtr> ZOrder()
    {
      IntPtr cur = Native.GetTopWindow(IntPtr.Zero);

      if (cur != IntPtr.Zero)
      {
        yield return cur;

        while ((cur = Native.GetWindow(cur, Native.GetWindow_Cmd.GW_HWNDNEXT)) != IntPtr.Zero)
        {
          yield return cur;
        }
      }
    }

    private IEnumerable<IntPtr> ZOrderVisibleWindows()
    {
      List<IntPtr> visibleWindows = new List<IntPtr>();

      Native.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
      {
        if (Native.IsWindowVisible(hWnd))
        {
          visibleWindows.Add(hWnd);
        }
        return true;
      }, IntPtr.Zero);

      foreach (var hWnd in ZOrder())
      {
        if (visibleWindows.Contains(hWnd))
        {
          yield return hWnd;
        }
      }
    }

    private void ForceForegroundWindow(IntPtr hWnd)
    {
      uint x;
      uint foreThread = Native.GetWindowThreadProcessId(Native.GetForegroundWindow(), out x);
      uint appThread = (uint)System.Threading.Thread.CurrentThread.ManagedThreadId;

      if (foreThread != appThread)
      {
        Native.AttachThreadInput(foreThread, appThread, true);
        Native.BringWindowToTop(hWnd);
        Native.ShowWindow(hWnd, Native.ShowWindowCommands.Show);
        Native.AttachThreadInput(foreThread, appThread, false);
      }
      else
      {
        Native.BringWindowToTop(hWnd);
        Native.ShowWindow(hWnd, Native.ShowWindowCommands.Show);
      }
    }

    private void ShootAll()
    {
      Shooting(delegate { return UploadToImgur(TakeScreenshot()); });
    }

    private string UploadToImgur(byte[] screen)
    {
      NameValueCollection values = new NameValueCollection
      {
        {"key", apiKey},
        {"image", Convert.ToBase64String(screen)}
      };

      byte[] response = new WebClient().UploadValues("http://imgur.com/api/upload.xml", values);
      string strResponse = System.Text.Encoding.ASCII.GetString(response);
      return ImageRe.Match(strResponse).Groups[1].Value;
    }

    private void Shooting(Func<string> f)
    {
      Shooting(f, true);
    }

    private void PreShoot()
    {
      prev.First = Visible;
      prev.Second = WindowState;

      Hide();
      linkLabel1.Hide();
    }

    private void Shooting(Func<string> f, bool hide)
    {
      if (hide)
      {
        PreShoot();
      }

      try
      {
        string imgUrl = f();

        linkLabel1.Text = imgUrl;
        linkLabel1.Links.Clear();
        linkLabel1.Links.Add(0, imgUrl.Length, imgUrl);
        linkLabel1.Show();
        Clipboard.SetText(imgUrl);

        if (prev.First)
        {
          Show();
          WindowState = prev.Second;
        }
        else
        {
          notifyIcon1.ShowBalloonTip(5000, "Scroto",
            "Screenshot at " + imgUrl, ToolTipIcon.Info);
        }
      }
      catch (OutOfMemoryException)
      {
        Show();
        MessageBox.Show("Errore durante l'acquisizione dell'immagine",
          "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      catch (ArgumentException)
      {
        Show();
        MessageBox.Show("Errore durante l'acquisizione dell'immagine",
          "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      catch (Exception)
      {
        Show();
        MessageBox.Show("Errore durante l'upload dell'immagine",
          "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    private byte[] TakeScreenshot(Rectangle crop)
    {
      Size sz = new Size(Screen.AllScreens.Select(x => x.Bounds.X + x.Bounds.Width).Max(),
        Screen.AllScreens.Select(x => x.Bounds.Y + x.Bounds.Height).Max());
      IntPtr hDesk = Native.GetDesktopWindow();
      IntPtr hSrce = Native.GetWindowDC(hDesk);
      IntPtr hDest = Native.CreateCompatibleDC(hSrce);
      IntPtr hBmp = Native.CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
      IntPtr hOldBmp = Native.SelectObject(hDest, hBmp);
      Native.BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
      Bitmap img = Bitmap.FromHbitmap(hBmp);
      Native.SelectObject(hDest, hOldBmp);
      Native.DeleteObject(hBmp);
      Native.DeleteDC(hDest);
      Native.ReleaseDC(hDesk, hSrce);

      if (!(crop.X == 0 && crop.Y == 0 && crop.Height == sz.Height && crop.Width == sz.Width))
      {
        Bitmap cropped = img.Clone(crop, img.PixelFormat);
        img.Dispose();
        img = cropped;
      }

      MemoryStream ms = new MemoryStream();
      img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
      img.Dispose();

      return ms.ToArray();
    }

    private byte[] TakeScreenshot(Point start, Size size)
    {
      return TakeScreenshot(new Rectangle(start.X, start.Y, size.Width, size.Height));
    }

    private byte[] TakeScreenshot()
    {
      return TakeScreenshot(new Rectangle(0, 0, Screen.AllScreens.Select(x => x.Bounds.X + x.Bounds.Width).Max(),
        Screen.AllScreens.Select(x => x.Bounds.Y + x.Bounds.Height).Max()));
    }

    private byte[] TakeScreenshot(int screen)
    {
      return TakeScreenshot(Screen.AllScreens[screen].WorkingArea);
    }

    private void windowToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GrabAll();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Crop();
    }

    private void cropToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Crop();
    }

    private void Crop()
    {
      PreShoot();
      Cropper cropper = new Cropper();
      cropper.OnSuccess += delegate(Rectangle r)
      {
        Shooting(delegate { return UploadToImgur(TakeScreenshot(r)); }, false);
      };
      cropper.OnHookFailure += GrabFailure;
      cropper.OnInputFailure += InputFailure;
      cropper.Run();
    }
  }
}
