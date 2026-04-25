using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppStarter
{
    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int WS_SHOWNORMAL = 1;

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]

        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (IsAdministrator() == false)
            {
                try
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.FileName = Application.ExecutablePath;
                    procInfo.WorkingDirectory = Environment.CurrentDirectory;
                    procInfo.Verb = "runas";
                    Process.Start(procInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                return;
            }

            //Get the running instance.
            Process instance = RunningInstance();
            if (instance == null)
            {
                AppStarter mf = new AppStarter();
                Application.Run(mf);
            }
            else
            {
                //There is another instance of this process.
                HandleRunningInstance(instance.MainWindowHandle);
            }

        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }

        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop through the running processes in with the same name
            foreach (Process process in processes)
            {
                //Ignore the current process
                if (process.Id != current.Id)
                {
                    //Make sure that the process is running from the exe file.
                    string exeFile = Assembly.GetExecutingAssembly().Location.Replace("/", "\\");
                    if (exeFile == current.MainModule.FileName)
                    {
                        //Return the other process instance.
                        return process;
                    }
                }
            }

            //No other instance was found, return null.
            return null;
        }

        public static void HandleRunningInstance(IntPtr handle)
        {
            //Make sure the window is not minimized or maximized
            ShowWindowAsync(handle, WS_SHOWNORMAL);

            //Set the real intance to foreground window
            ActivateWindow(handle);
        }

        public static void ActivateWindow(IntPtr handle)
        {
            //Set the real intance to foreground window
            SetForegroundWindow(handle);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //new MsgDialog(MessageType.Error, e.Exception).ShowDialog();
        }
    }
}
