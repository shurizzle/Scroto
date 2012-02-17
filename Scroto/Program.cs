using System;
using Application = System.Windows.Forms.Application;
using Mutex = System.Threading.Mutex;
using Process = System.Diagnostics.Process;

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
      using (var mutex = new Mutex(true, "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", out createdNew))
      {
        if (createdNew)
        {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run(new Scroto());
        }
        else
        {
          var current = Process.GetCurrentProcess();
          foreach (var process in Process.GetProcessesByName(current.ProcessName))
          {
            if (process.Id != current.Id)
            {
              NativeMethods.PostMessage(
                      (IntPtr)NativeMethods.HWND_BROADCAST,
                      NativeMethods.WM_SHOWME,
                      IntPtr.Zero,
                      IntPtr.Zero);

              NativeMethods.SetForegroundWindow(process.MainWindowHandle);
              break;
            }
          }
        }
      }
    }
  }
}
