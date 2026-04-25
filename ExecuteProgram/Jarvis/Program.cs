using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bifrost.Common;
using Bifrost.Win;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;



namespace Jarvis
{
    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int WS_SHOWNORMAL = 1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //숫자형식, 날짜형식 디폴트 고정
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("ko-KR");

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.Skins.SkinManager.EnableFormSkins();
            //DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Caramel");
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.Bezier.MilkSnake);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2016 Light");

            //Assembly asm = typeof(DevExpress.UserSkins.AIMS).Assembly;
            //DevExpress.Skins.SkinManager.Default.RegisterAssembly(asm);
            //SplashScreenManager.RegisterUserSkins(asm);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "AIMS";


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
            //AppearanceObject.DefaultFont = new Font("카이겐고딕 KR Regular", 9);
            //DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("D2Coding ligature", 15);
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("D2Coding ligature", 15);
            SkinElement element = SkinManager.GetSkinElement(SkinProductId.Docking, DevExpress.LookAndFeel.UserLookAndFeel.Default, "DocumentGroupTabPane");
            element.ContentMargins.All = 0;

            //DevExpress.XtraEditors.WindowsFormsSettings.TouchUIMode = TouchUIMode.True;// = DevExpress.XtraEditors.ScrollUIMode.Touch;


            //if (IsAdministrator() == false)
            //{
            //    try
            //    {
            //        ProcessStartInfo procInfo = new ProcessStartInfo();
            //        procInfo.UseShellExecute = true;
            //        procInfo.FileName = Application.ExecutablePath;
            //        procInfo.WorkingDirectory = Environment.CurrentDirectory;
            //        procInfo.Verb = "runas";
            //        Process.Start(procInfo);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }

            //    return;
            //}

            //Get the running instance. 
            //Process instance = RunningInstance();
            //if (instance == null)
            //{
            MainForm mf = new MainForm();
            Application.Run(mf);
            //}
            //else
            //{
            //    //There is another instance of this process.
            //    HandleRunningInstance(instance.MainWindowHandle);
            //}
        }

        //public class SkinRegistration : Component
        //{
        //    public SkinRegistration()
        //    {
        //        //DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.AIMS).Assembly);
        //    }
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            //GlobalVariable.hasError = true;
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.CloseForm();
            }
            new MsgDialog(e.Exception.ToString(), MessageType.Error).ShowDialog();
        }
    }
}
