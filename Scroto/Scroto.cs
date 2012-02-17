using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Scroto
{
  public partial class Scroto : Form
  {
    static private string apiKey = "54bf7d24bb8f12f638f40c41abcd65ee";
    static private Regex ImageRe = new Regex("<original_image>(.*)</original_image>");

    public Scroto()
    {
      InitializeComponent();
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == NativeMethods.WM_SHOWME)
        ShowMe();
      base.WndProc(ref m);
    }

    private void ShowMe()
    {
      if (WindowState == FormWindowState.Minimized)
        WindowState = FormWindowState.Normal;

      bool top = TopMost;
      TopMost = true;
      TopMost = top;
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
    }

    private void button1_Click(object sender, EventArgs e)
    {
      WindowState = FormWindowState.Minimized;
      linkLabel1.Hide();
      try
      {
        UploadToImgur();
      }
      catch (Exception)
      {
        WindowState = FormWindowState.Normal;
        MessageBox.Show("Errore durante l'upload dell'immagine",
          "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    private void UploadToImgur()
    {
      NameValueCollection values = new NameValueCollection
      {
        {"key", apiKey},
        {"image", Convert.ToBase64String(TakeScreenshot())}
      };

      string imgUrl;
      {
        byte[] response = new WebClient().UploadValues("http://imgur.com/api/upload.xml", values);
        string strResponse = System.Text.Encoding.ASCII.GetString(response);
        imgUrl = ImageRe.Match(strResponse).Groups[1].Value;
      }

      linkLabel1.Text = imgUrl;
      linkLabel1.Links.Clear();
      linkLabel1.Links.Add(0, imgUrl.Length, imgUrl);
      linkLabel1.Show();
      Clipboard.SetText(imgUrl);
      WindowState = FormWindowState.Normal;
    }

    private byte[] TakeScreenshot()
    {
      Size sz = Screen.PrimaryScreen.Bounds.Size;
      IntPtr hDesk = GetDesktopWindow();
      IntPtr hSrce = GetWindowDC(hDesk);
      IntPtr hDest = CreateCompatibleDC(hSrce);
      IntPtr hBmp = CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
      IntPtr hOldBmp = SelectObject(hDest, hBmp);
      BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
      Bitmap img = Bitmap.FromHbitmap(hBmp);
      SelectObject(hDest, hOldBmp);
      DeleteObject(hBmp);
      DeleteDC(hDest);
      ReleaseDC(hDesk, hSrce);

      MemoryStream ms = new MemoryStream();
      img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
      img.Dispose();

      return ms.ToArray();
    }

    [DllImport("gdi32.dll")]
    static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int
    wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);
    [DllImport("user32.dll")]
    static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);
    [DllImport("gdi32.dll")]
    static extern IntPtr DeleteDC(IntPtr hDc);
    [DllImport("gdi32.dll")]
    static extern IntPtr DeleteObject(IntPtr hDc);
    [DllImport("gdi32.dll")]
    static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
    [DllImport("gdi32.dll")]
    static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")]
    static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr ptr);
  }
}
