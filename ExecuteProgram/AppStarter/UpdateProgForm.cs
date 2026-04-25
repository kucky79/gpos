using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;

namespace AppStarter
{
    public partial class UpdateProgForm : DialogBase
	{

        ProcessStartInfo startInfo = new ProcessStartInfo(@"Ether.exe");


        public UpdateProgForm()
		{
            InitializeComponent();	
		}

		public void SetUpdateStatus(string statusText)
		{
			lblUpdate.Text = statusText;
			Application.DoEvents();
		}

		public void SetUpdateProgress(int percentage)
		{
			statusBarDetail.Value = percentage;
            Application.DoEvents();
		}

        public void SetTotalProgress(int percentage)
        {
            statusBarTotal.Value = percentage;
            Application.DoEvents();
        }

        void UpdateProgForm_Shown(object sender, System.EventArgs e)
		{

            //KillApplication();

            if (DoUpdate())
            {
                //DoChangeProgram();
            }
            DoExecuteProgram();

            Application.Exit();
            // Wait one second.
            Thread.Sleep(1000);
            // End notepad.
            //CloseStartedProcesses();

            Process.GetCurrentProcess().Kill();

        }

        private static bool FileExistCheck(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                return true;
            else
                return false;
        }

        private static bool FileRename(string filePath, string oldFile, string newFile)
        {
            oldFile = filePath + "\\" + oldFile;
            newFile = filePath + "\\" + newFile;

            if(FileExistCheck(oldFile))
            {
                System.IO.File.Move(oldFile, newFile);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DoChangeProgram()
        {
            string newFilePath = AppDomain.CurrentDomain.BaseDirectory + "Ether_NEW.exe";
            string orgFilePath = AppDomain.CurrentDomain.BaseDirectory + "Ether.exe";

            //받은 파일중에 실행파일이 존재하면 지우고 이름을 바꿔줌
            if (FileExistCheck(newFilePath))
            {
                File.SetAttributes(orgFilePath, FileAttributes.Normal);
                System.IO.File.Delete(orgFilePath);
                FileRename(AppDomain.CurrentDomain.BaseDirectory, "Ether_NEW.exe", "Ether.exe");
            }

        }


        private void DoExecuteProgram()
        {
            try
            {

                ProcessStartInfo startInfo = new ProcessStartInfo(@"Ether.exe");
                Process.Start(startInfo);

                //Process ps = new Process();
                //ps.StartInfo.FileName = "Ether.exe";
                //ps.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                //ps.StartInfo.UseShellExecute = true; //This should not block your program

                ////startInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ////startInfo.UseShellExecute = true; //This should not block your program

                ////Process.Start(ps);
                //using (ps)
                //{
                //    ps.Start();
                //    ps.WaitForExit();
                //    //_startedProcesses.Push(ps);
                //}
            }
            catch
            {
                //Cursor.Current = Cursors.Default;
                //MessageBox.Show("Analysis  error! Themodulere is no excution module to run!", "Fatal Error!");
                return;
            }

        }


        #region AppStarter.exe Update

        private Bifrost.FileTransfer.Client.FileManager wsFileManager;
		private IAppUpdater.AppUpdater wsAppUpdater;

        string WebServiceURL = string.Empty;

        private bool Initialize()
		{
			/// Application Updater
			try
			{
                string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
                IniFile inifile = new IniFile();

                //upgrade server
                string UpgradeServerIP = inifile.IniReadValue("UpgradeServer", "IP", Path);
                if (UpgradeServerIP == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("UpgradeServer", "IP", "localhost", Path);
                    UpgradeServerIP = inifile.IniReadValue("UpgradeServer", "IP", Path);
                }

                Properties.Settings.Default.AppUpdateUrl = @"http://" + UpgradeServerIP + @"/AppUpdaterService/AppUpdater.asmx";
                WebServiceURL = Properties.Settings.Default.AppUpdateUrl;

                //서버에 접근가능한지 확인
                if (CheckConnection())
                {

                    wsFileManager = new Bifrost.FileTransfer.Client.FileManager();
                    wsFileManager.Url = Properties.Settings.Default.AppUpdateUrl;
                    wsFileManager.UseDefaultCredentials = true;
                    wsFileManager.ProgressChanged += new Bifrost.FileTransfer.Client.ProgressChangedEventHandler(wsFileManager_ProgressChanged);
                    wsFileManager.StateChanged += new EventHandler(wsFileManager_StateChanged);
                    wsFileManager.InitializeInstance();

                    wsAppUpdater = new IAppUpdater.AppUpdater();
                    wsAppUpdater.Url = Properties.Settings.Default.AppUpdateUrl;
                    wsAppUpdater.UseDefaultCredentials = true;
                    return true;
                }
                else
                {
                    return false;
                }
			}
			catch
			{
				return false;
			}
		}

        private bool CheckConnection()
        {
            var url = WebServiceURL;

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 3000;
                //NetworkCredential networkCredential = new NetworkCredential("UserName", "password", "domain");
                // Associate the 'NetworkCredential' object with the 'WebRequest' object.
                //myRequest.Credentials = networkCredential;
                var response = (HttpWebResponse)myRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// Kill AppStarter
        /// </summary>
        private void KillApplication()
        {
            /// 
            /// kill appupdater process
            /// 
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName("Ether");

                while (processes.Length != 0)
                {
                    //Loop through the running processes in with the same name
                    foreach (Process process in processes)
                    {
                        try
                        {
                            process.Kill();
                            break;
                        }
                        catch
                        {
                        }
                    }

                    //Application.DoEvents();
                    //processes = Process.GetProcessesByName("AppStarter");
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Update appStarter.exe and dll files
        /// </summary>
        private void UpdateAppStarter()
		{
            /// 
            /// Get list of update files
            /// 
            string appStarterFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("/", "\\"));
            //if (Directory.Exists(appStarterFolder))
            //{
            //    appStarterFolder = new DirectoryInfo(appStarterFolder).Parent.FullName;
            //}

            /// 
            /// get list of local files info
            /// 
            string[] localFiles = Directory.GetFiles(appStarterFolder, "*", SearchOption.TopDirectoryOnly);
			string[] localFileInfos = new string[localFiles.Length];

			int i = 0;
			// each file, get file size and date time
			foreach (string localFileName in localFiles)
			{
				// each file, get file size and date time
				FileInfo fi = new FileInfo(localFileName);
				localFileInfos[i] = string.Concat(Path.GetFileName(localFileName), "$", fi.LastWriteTimeUtc.ToString("yyyyMMddHHmm"));
				i++;
			}

			/// 
			/// connect to server, get list of new files
			/// 
			string[] newFiles = wsAppUpdater.GetAppUpdaterUpdateList(localFileInfos);

            for (int k = 1; k < newFiles.Length; k++)
			{
				string localFileName = Path.Combine(appStarterFolder, newFiles[k]);
				SetUpdateStatus(string.Format("{0} Downloading....", newFiles[k]));

				try
				{
					wsFileManager.StartDownloadSync(newFiles[0] + newFiles[k], localFileName, true, true);
				}
				catch { }

                SetTotalProgress((int)(k * 100 / newFiles.Length));
            }
        }

        /// <summary>
		/// Update new dll files
		/// </summary>
		private void UpdateBaseFiles()
        {
            /// 
			/// Get list of update files
			/// 
			string appStarterFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("/", "\\"));

            /// 
            /// get list of local files info
            /// 
            string[] localFiles = Directory.GetFiles(appStarterFolder, "*", SearchOption.TopDirectoryOnly);
            string[] localFileInfos = new string[localFiles.Length];

            int i = 0;
            // each file, get file size and date time
            foreach (string localFileName in localFiles)
            {
                // each file, get file size and date time
                FileInfo fi = new FileInfo(localFileName);
                localFileInfos[i] = string.Concat(Path.GetFileName(localFileName), "$", fi.LastWriteTimeUtc.ToString("yyyyMMddHHmm"));
                i++;
            }

            /// 
            /// connect to server, get list of new files
            /// 
            string[] newFiles = wsAppUpdater.GetBaseUpdateFiles(localFileInfos);

            for (int k = 1; k < newFiles.Length; k++)
            {
                string localFileName = Path.Combine(appStarterFolder, newFiles[k]);
                SetUpdateStatus(string.Format("{0} 다운로드 중....", newFiles[k]));

                try
                {
                    wsFileManager.StartDownloadSync(newFiles[0] + newFiles[k], localFileName, true, true);
                }
                catch { }

                SetTotalProgress((int)(k * 100 / newFiles.Length));
            }
        }

        

        /// <summary>
        /// Update new files
        /// </summary>
        private bool DoUpdate()
        {
            bool result = false;

            try
            {
                /// 
                /// Init WebService
                /// 
                if (Initialize())
                {
                    /// 
                    /// Update AppStarter.exe and TCL....dll
                    /// 
                    UpdateAppStarter();
                    UpdateBaseFiles();

                    /// End downloading
                    wsFileManager.KillInstance();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "업데이트중 오류가 발생했습니다.");
                return result;
            }
			finally
			{
                this.Close();
			}

        }


		void wsFileManager_StateChanged(object sender, EventArgs e)
		{
			if (wsFileManager.State == Bifrost.FileTransfer.Client.ProcessState.Completed)
			{
			}
			else if (wsFileManager.State == Bifrost.FileTransfer.Client.ProcessState.Failed)
			{
				this.wsFileManager.KillInstance();
				Close();
			}
		}

		void wsFileManager_ProgressChanged(object sender, Bifrost.FileTransfer.Client.ProgressChangedEventArgs e)
		{
			SetUpdateProgress((int)(e.Value * 100 / e.Max));
		}


		#endregion

		
	}
}