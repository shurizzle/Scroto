using System;
using Application = System.Windows.Forms.Application;
using Mutex = System.Threading.Mutex;

namespace Scroto
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      bool createdNew = true;
      var m = new Mutex(true, "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", out createdNew);

      if (createdNew)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Scroto());
        GC.KeepAlive(m);
      }
      else
      {
        Process.Instance.Show();
      }
    }
  }
}
