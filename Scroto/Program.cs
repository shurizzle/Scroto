using System;
using Application = System.Windows.Forms.Application;

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
      if (Process.OnlyInstance)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Scroto());
        GC.KeepAlive(Process.Mutex);
      }
      else
      {
        Process.ShowInstance();
      }
    }
  }
}
