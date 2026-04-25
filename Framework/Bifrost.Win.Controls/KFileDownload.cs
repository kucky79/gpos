using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NF.Framework.FileTransfer.Client;

namespace NF.Framework.Win.Controls
{
	public partial class KFileDownload : Component
	{
		#region Win32 API

		// Performs an operation on a specified file.
		[DllImport("shell32.dll")]
		internal static extern IntPtr ShellExecute(
			IntPtr hwnd,			// Handle to a parent window.
			[MarshalAs(UnmanagedType.LPStr)]
			String lpOperation,		// Pointer to a null-terminated string, referred to in this case as a verb, 
			// that specifies the action to be performed.
			[MarshalAs(UnmanagedType.LPStr)]
			String lpFile,			// Pointer to a null-terminated string that specifies the file or object on which 
			// to execute the specified verb.
			[MarshalAs(UnmanagedType.LPStr)]
			String lpParameters,	// If the lpFile parameter specifies an executable file, lpParameters is a pointer 
			// to a null-terminated string that specifies the parameters to be passed 
			// to the application.
			[MarshalAs(UnmanagedType.LPStr)]
			String lpDirectory,		// Pointer to a null-terminated string that specifies the default directory. 
			Int32 nShowCmd);		// Flags that specify how an application is to be displayed when it is opened.


		#endregion

		#region Privates

		/// <summary>
		/// File Manager webservice
		/// </summary>
		private NF.Framework.FileTransfer.Client.FileManager wsFileManager;

		/// <summary>
		/// Progress Form
		/// </summary>
		private PrgForm progressForm;

		private string[] fileInfoDetailGuids;
		private string[] fileInfoDetailNames;

		private void InitializeFX()
		{
			try
			{
				wsFileManager = new NF.Framework.FileTransfer.Client.FileManager();
				this.wsFileManager.ProgressChanged += new NF.Framework.FileTransfer.Client.ProgressChangedEventHandler(wsFileManager_ProgressChanged);
				this.wsFileManager.StateChanged += new EventHandler(wsFileManager_StateChanged);
				this.wsFileManager.Url = _FileManagerWebService;
				this.wsFileManager.UseDefaultCredentials = true;
				this.wsFileManager.InitializeInstance();
			}
			catch { }
		}

		void wsFileManager_StateChanged(object sender, EventArgs e)
		{
			if (wsFileManager.State == NF.Framework.FileTransfer.Client.ProcessState.Completed ||
				wsFileManager.State == NF.Framework.FileTransfer.Client.ProcessState.Failed)
			{
				progressForm.Close();
				this.wsFileManager.KillInstance();
			}
		}

		void wsFileManager_ProgressChanged(object sender, NF.Framework.FileTransfer.Client.ProgressChangedEventArgs e)
		{
			this.progressForm.UpdateProgress((int)e.Value, (int)e.Max, fileInfoDetailNames[0]);
		}

		/// <summary>
		/// Retrieve detail file list
		/// </summary>
        private bool GetDetailFiles(NF.Framework.SubFolder subFolder, int masterFileNum)
		{
            string _subFolder = subFolder.ToString();
            fileInfoDetailGuids = wsFileManager.GetFileInfoDetailGuids(this.wsFileManager.InstanceId, _subFolder, masterFileNum);
            fileInfoDetailNames = wsFileManager.GetFileInfoDetailNames(this.wsFileManager.InstanceId, _subFolder, masterFileNum);
			if (fileInfoDetailGuids.Length == 0 || fileInfoDetailNames.Length == 0)
			{
				//System.Windows.Forms.MessageBox.Show("File Not Found");
				return false;
			}

			return true;
		}

		#endregion

		#region Constructors

		public KFileDownload()
		{
			InitializeComponent();
		}

		public KFileDownload(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		/// Upload webservice url
		/// </summary>
		private string _FileManagerWebService = "http://localhost/FileService/FMService.asmx";
		[Description("File Upload WebService Url")]
		[Browsable(false)]
		public string FileManagerWebService
		{
			get
			{
				return _FileManagerWebService;
			}
			set
			{
				_FileManagerWebService = value;
				InitializeFX();
			}
		}

		private bool _UseAsyncProcessing = false;

		/// <summary>
		/// Use thread to process upload/download
		/// </summary>
		[Description("UseAsyncProcessing")]
		public bool UseAsyncProcessing
		{
			get { return _UseAsyncProcessing; }
			set
			{
				_UseAsyncProcessing = value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Get file from server to temporary file and load to an 
		/// Image object
		/// </summary>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public Image GetImage(NF.Framework.SubFolder subFolder, int masterFileNum)
		{
			Image retImg = null;

			try
			{
				this.wsFileManager.InitializeInstance();
                if (!GetDetailFiles(subFolder, masterFileNum)) return null;

				string tempFileName = System.IO.Path.GetTempFileName();
				tempFileName = Path.Combine(Path.GetDirectoryName(tempFileName),
					Path.GetFileNameWithoutExtension(tempFileName) + Path.GetExtension(fileInfoDetailGuids[0]));

                if (SaveFile( masterFileNum, tempFileName))
				{
					retImg = Image.FromFile(tempFileName);
				}
			}
			catch
			{
				
			}

			return retImg;
		}

		/// <summary>
		/// Get url of server image
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		public string GetImageUrl(string subSystemType, int masterFileNum)
		{
			this.wsFileManager.InitializeInstance();
			string url = wsFileManager.GetFileInfoUrl(wsFileManager.InstanceId, subSystemType, masterFileNum);
			int result = 0;
			if (string.IsNullOrEmpty(url) || (int.TryParse(url, out result) && result < 0)) return string.Empty;

			return url;
		}

		/// <summary>
		/// Get url of server image
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		public string GetImageUrl(int fileDetailInfoSeq)
		{
			this.wsFileManager.InitializeInstance();
			string url = wsFileManager.GetFileInfoDetailUrl(wsFileManager.InstanceId, fileDetailInfoSeq);
			int result = 0;
			if (string.IsNullOrEmpty(url) || (int.TryParse(url, out result) && result < 0)) return string.Empty;

			return url;
		}

		/// <summary>
		/// Prompt to choose a target location to save file from server
		/// to local pc
		/// </summary>
		/// <param name="masterFileNum"></param>
        public bool SaveFile(NF.Framework.SubFolder subFolder, int masterFileNum)
		{
			this.wsFileManager.InitializeInstance();
            if (!GetDetailFiles(subFolder, masterFileNum)) return false;

			string ext = System.IO.Path.GetExtension(fileInfoDetailGuids[0]);
			saveFileDialog1.Filter = string.Format("{0} files (*{0})|*{0}|All files (*.*)|*.*", ext);
			saveFileDialog1.FileName = fileInfoDetailNames[0];
			if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				return SaveFile(masterFileNum, saveFileDialog1.FileName);
			}
			return false;
		}

		/// <summary>
		/// Prompt to choose a target location to save file from server
		/// to local pc
		/// </summary>
		/// <param name="masterFileNum"></param>
        public bool SaveFileAs(NF.Framework.SubFolder subFolder, int masterFileNum, string localFilePath)
		{
			this.wsFileManager.InitializeInstance();
            if (!GetDetailFiles(subFolder, masterFileNum)) return false;

			return SaveFile( masterFileNum, localFilePath);
		}

		/// <summary>
		/// Save to a local file
		/// </summary>
		/// <param name="masterFileNum"></param>
		/// <param name="localFilePath"></param>
        public bool SaveFile( int masterFileNum, string localFilePath)
		{
			// Start downloading
			progressForm = new PrgForm();
			progressForm.Show();

			// Call File Manager webservice to download			
			if(this._UseAsyncProcessing)
				wsFileManager.StartDownload(fileInfoDetailGuids[0], localFilePath, true);
			else
				wsFileManager.StartDownloadSync(fileInfoDetailGuids[0], localFilePath, true);

			return true;
		}

		/// <summary>
		/// Get Real file name by master file number
		/// </summary>
		/// <param name="masterFileKey"></param>
		/// <returns></returns>
        public string GetFileName(NF.Framework.SubFolder subFolder, int masterFileNum)
		{
			string fileName = string.Empty;

			try
			{
				this.wsFileManager.InitializeInstance();
                string[] fileNames = wsFileManager.GetFileInfoDetailNames(wsFileManager.InstanceId, subFolder.ToString(), masterFileNum);
				if (fileNames != null && fileNames.Length != 0)
					fileName = fileNames[0];
			}
			catch 
			{
				//System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
				this.wsFileManager.KillInstance();
			}

			return fileName;
		}

		/// <summary>
		/// Open file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
        public void OpenFile(NF.Framework.SubFolder subFolder, int masterFileNum)
		{
			try
			{
				string tempFileName = System.IO.Path.GetTempFileName();
				this.wsFileManager.InitializeInstance();
                if (!GetDetailFiles(subFolder, masterFileNum)) return;

				if (SaveFile(masterFileNum, tempFileName))
				{
                    string newFile = Path.GetDirectoryName(tempFileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(tempFileName) 
                        + Path.GetExtension(fileInfoDetailNames[0]);

					/// 
					/// Change to original filename
					/// 
					File.Move(tempFileName, newFile);

					/// 
					/// Shell execute
					/// 					
					ShellExecute(IntPtr.Zero, "open", newFile, "", "", 1);										
				}
			}
			catch 
			{
				//System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}


		#endregion
	}

	
}
