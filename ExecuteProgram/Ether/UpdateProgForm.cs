using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Jarvis;
using Bifrost.Win;
using Bifrost.Helper;

namespace Jarvis
{
    public partial class UpdateProgForm : DialogBase
	{
		public UpdateProgForm()
		{
            this.Visible = false;

            InitializeComponent();
            ((MainForm)this.MdiForm).isUpdated = false;
        }

        public void SetUpdateStatus(string statusText)
		{
			label_Main.Text = statusText;
			Application.DoEvents();
		}

		public void SetUpdateProgress(int percentage)
		{
            progressBarMenu.EditValue = percentage;
			Application.DoEvents();
		}

        public void SetUpdateTotalProgress(int percentage)
        {
            progressBarTotal.EditValue = percentage;
            Application.DoEvents();
        }

        void UpdateProgForm_Shown(object sender, System.EventArgs e)
		{
            //LoadData.StartLoading(this, "Update", "Update Menus...");
            DoUpdate();
            //LoadData.EndLoading();
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
                wsFileManager = new Bifrost.FileTransfer.Client.FileManager();

                //서버에 접근가능한지 확인
                if (CheckConnection())
                {
                    this.wsFileManager.Url =  Properties.Settings.Default.AppUpdateUrl;
                    this.wsFileManager.UseDefaultCredentials = true;
                    this.wsFileManager.ProgressChanged +=  WsFileManager_ProgressChanged;
                    this.wsFileManager.StateChanged += WsFileManager_StateChanged;
                    this.wsFileManager.InitializeInstance();

                    this.wsAppUpdater = new Jarvis.IAppUpdater.AppUpdater();
                    this.wsAppUpdater.Url = WebServiceURL;// Properties.Settings.Default.AppUpdateUrl;
                    this.wsAppUpdater.UseDefaultCredentials = true;

                    return true;
                }
                else
                    return false;
			}
			catch
			{
                MessageBox.Show("Update Server is not Access!!");
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
		private void KillAppStarter()
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
            /// Kill AppStarter.exe process
            /// 
            //KillAppStarter();

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
			}
		}


        private int _totalFiles = 0;
		/// <summary>
		/// Update new dll files in Auth Menu dlls
		/// </summary>
		private void UpdateAuthFiles()
		{
			/// 
			/// get list of authorized dll and send to server to get new files list
			/// 
			DataRow[] rows = this.MdiForm.Global.Menus.Select("TP_MENU <> 'MGM' AND CD_MENU_LOCATION <> '' AND CD_MENU_CLASS <> ''", "CD_MENU_LOCATION, CD_MENU_CLASS");
			ArrayList localFileInfos = new ArrayList();
			localFileInfos.Add("AIMS2");
			localFileInfos.Add("UpdateFromERP");

            //폴더생성
            string sDirPath;
            sDirPath = Application.StartupPath + "\\DAT";
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            // each file, get file size and date time
            //SetUpdateStatus("권한 있 file list...");

            ArrayList uniqueList = new ArrayList();
			foreach (DataRow dr in rows)
			{
				string sLocalFileName = dr["CD_MENU_LOCATION"].ToString().Trim();
				if (sLocalFileName.Equals(string.Empty))
				{
					continue;
				}

				if (uniqueList.Contains(sLocalFileName))
				{
					continue;
				}

				/// 
				/// add to list
				/// 
				uniqueList.Add(sLocalFileName);

				string localFileName = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\", sLocalFileName);

				// each file, get file size and date time
				//SetUpdateStatus(string.Format("Building file list {0}...", sLocalFileName));

				FileInfo fi = new FileInfo(localFileName);
				localFileInfos.Add(string.Concat(sLocalFileName, "$", fi.LastWriteTimeUtc.ToString("yyyyMMddHHmm")));
			}

			// checking new version
			//SetUpdateStatus("업데이트 확인 중...");

			string[] dllFiles = wsAppUpdater.GetUpdateFiles((string[])localFileInfos.ToArray(typeof(string)));
			string localDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\";

            _totalFiles = dllFiles.Length;

            if (dllFiles.Length > 1)
			{
                this.Visible = true;
                for (int k = 1; k < dllFiles.Length; k++)
				{
					SetUpdateStatus(string.Format("{0} Downloading....", dllFiles[k]));

					string localFileName = localDir + dllFiles[k];
					string localFileDir = Path.GetDirectoryName(localFileName);

					if (!Directory.Exists(localFileDir))
						Directory.CreateDirectory(localFileDir);

					try
					{
						wsFileManager.StartDownloadSync(dllFiles[0] + dllFiles[k], localFileName, true, true);

                        string[] delFiles = Directory.GetFiles(localDir + "DAT\\", dllFiles[k].Replace("dll", "*"), SearchOption.TopDirectoryOnly);
                        foreach (string delFile in delFiles)
                        {
                            File.Delete(delFile);
                        }
                    }
					catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                    }

                    SetUpdateTotalProgress((int)(k * 100 / _totalFiles));

                }
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
            _totalFiles = newFiles.Length;

            bool chkExecutePrg = false;

            for (int k = 1; k < newFiles.Length; k++)
            {
                string localFileName = Path.Combine(appStarterFolder, newFiles[k]);
                SetUpdateStatus(string.Format("{0} 다운로드 중....", newFiles[k]));

                try
                {
                    wsFileManager.StartDownloadSync(newFiles[0] + newFiles[k], localFileName, true, true);
                    if(newFiles[k] == "Ether_NEW.exe")
                    {
                        chkExecutePrg = true;
                    }
                }
                catch { }

                SetUpdateTotalProgress((int)(k * 100 / _totalFiles));
            }

            if (chkExecutePrg)
            {
                string NewProg = Application.StartupPath + "\\Ether_NEW.exe";
                Process.Start(NewProg);
                KillAppStarter();
            }
        }


    
        /// <summary>
        /// Update new files
        /// </summary>
        private void DoUpdate()
		{
			try
			{
                /// 
                /// Init WebService
                /// 
                if (Initialize())
                {
                    /// 
                    /// UpdateAuth, USER_MENU에 권한 있는 assemblies만 다운로드함
                    /// 
                    UpdateBaseFiles();

                    /// End downloading
                    wsFileManager.KillInstance();
                }
                else
                {
                    ShowMessageBoxA("업데이트 서버에 접속 불가합니다!!", Bifrost.Common.MessageType.Error);
                }
			}
			catch (Exception ex)
			{
                ShowMessageBoxA(ex.Message, Bifrost.Common.MessageType.Error);
			}
			finally
			{
				((MainForm)this.MdiForm).IsAppUpdating = false; 
                ((MainForm)this.MdiForm).isUpdated = true;

                this.Close();
			}
		}


		void WsFileManager_StateChanged(object sender, EventArgs e)
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

        private void WsFileManager_ProgressChanged(object sender, Bifrost.FileTransfer.Client.ProgressChangedEventArgs e)
        {
            SetUpdateProgress((int)(e.Value * 100 / e.Max));
        }

        #endregion

    }
}