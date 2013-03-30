using System;
using Application = System.Windows.Forms.Application;
using System.Linq;

namespace Scroto
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      bool quiet = args.Any(x => x == "/q" || x == "/quiet");

      if (Process.OnlyInstance)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        new Scroto(quiet);
        Application.Run();
        GC.KeepAlive(Process.Mutex);
      }
      else
      {
        if (!quiet)
        {
          Process.ShowInstance();
        }
      }
    }
  }
}
