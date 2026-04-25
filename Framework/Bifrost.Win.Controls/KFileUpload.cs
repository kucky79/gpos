#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using NF.Framework.FileTransfer.Client;
#endregion Using directives

namespace NF.Framework.Win.Controls
{
	/// <summary>
	/// Single-file upload control
	/// </summary>
	public partial class KFileUpload : UserControl, IDisposable
	{
		public KFileUpload()
		{
			InitializeComponent();
		}

		#region Properties

		/// <summary>
		/// Start Uploading Time
		/// </summary>
		private DateTime _StartTime;

		/// <summary>
		/// Upload engine, connect to webservice to post
		/// </summary>
		private NF.Framework.FileTransfer.Client.FileManager wsFileManager;

		/// 
		/// SubSystemType
		/// 
        private SubFolder _subFolder = SubFolder.Board;
		public SubFolder subFolder
		{
			get
			{
                return _subFolder;
			}
			set
			{
                _subFolder = value;
			}
		}

		/// <summary>
		/// Show/Hide Browse button
		/// </summary>
		[Description("Show/Hide Browse button")]
		public bool ShowBrowse
		{
			get { return btnBrowse.Visible; }
			set { btnBrowse.Visible = value; btnBrowse.Width = value ? 74 : 0; }
		}


		/// <summary>
		/// Max file size
		/// </summary>
		private int MaxFileSize = 0;

		/// <summary>
		/// 
		/// </summary>
		public event ProcessStateChangedEventHandler ProcessStateChanged;
		private void RaiseProcessStateChangedEvent(ProcessStateChangedEventArgs e)
		{
			if (ProcessStateChanged != null)
				ProcessStateChanged(this, e);
		}

		/// <summary>
		/// Upload webservice url
		/// </summary>
		private string _FileManagerWebService = "http://localhost/FileService/FMService.asmx";
		[Description("File Upload WebService Url")]
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

		/// <summary>
		/// Master file key, each master file contains multiple uploaded files
		/// </summary>
		private int _MasterFileNum = 0;
		public int MasterFileNum
		{
			get { return this._MasterFileNum; }
			set
			{
				this._MasterFileNum = value;
				if (value != 0)
				{
					if (wsFileManager.InstanceId == string.Empty)
						wsFileManager.InitializeInstance();
                    string[] fileNames = wsFileManager.GetFileInfoDetailNames(wsFileManager.InstanceId, _subFolder.ToString(), value);
					this.Text = fileNames.Length != 0 ? fileNames[0] : string.Empty;
				}
				else
					this.Text = string.Empty;
			}
		}

		/// <summary>
		/// File path/name is displayed on the control
		/// </summary>
		[Description("Current string displayed in control, \r\nThis could be an uploaded filename or \r\na full path of a file to be uploaded")]
		public override string Text
		{
			get { return this.txtFilePath.Text; }
			set
			{
				this.txtFilePath.Text = value;
			}
		}

		private TimeSpan _Duration;

		/// <summary>
		/// Total upload time
		/// </summary>
		[Description("Upload/Download Time")]
		public TimeSpan Duration
		{
			get { return _Duration; }
			set { _Duration = value; }
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
		/// Process Upload action
		/// </summary>
		/// <returns></returns>
		private void _Upload()
		{
			try
			{
				// Start uploading
				this.PreUpload(UploadState.Transfer);

				if (!File.Exists(this.Text))
				{
					this.PreUpload(UploadState.Stop);
					return;
				}
				//throw new Exception("File Not Foundc");

				///
				/// Call Upload control to upload, then 
				/// get the MasterFileNum
				/// if succeeds.
				/// else throw a exception
				///
				if (this._UseAsyncProcessing)
                    this.wsFileManager.StartUpload(this.Text, SubFolder.Board);
				else
                    this.wsFileManager.StartUploadSync(this.Text, SubFolder.Board);
			}
			catch 
			{
				//MessageBox.Show(ex.Message);
				this.PreUpload(UploadState.Stop);
			}
		}

		/// <summary>
		/// Change the upload file with the same master file number
		/// </summary>
		/// <returns></returns>
		public void Upload()
		{
			_Upload();
		}

		/// <summary>
		/// Upload new file
		/// </summary>
		/// <param name="subSystemType"></param>
		public void ReUpload()
		{
			this._MasterFileNum = 0;
			_Upload();
		}

		/// <summary>
		/// Upload file to server
		/// </summary>
		/// <param name="fileName">full file path</param>
		/// <param name="targetServerFolder">target folder under the UploadFileDirectory settings</param>
		/// <param name="subPath">sub folder with file name</param>
		public bool UploadFileOnly(string fileName, string targetServerFolder, string subPath)
		{
			return wsFileManager.StartUpload(fileName, targetServerFolder, subPath);
		}

		/// <summary>
		/// Delete folder at server
		/// </summary>
		/// <param name="targetServerFolder">like: 2006\01\01, this folder is under CMAX FileSTored directory</param>
		/// <returns></returns>
		public bool DeleteFolder(string targetServerFolder)
		{
			return wsFileManager.DeleteFolder(targetServerFolder);
		}

		/// <summary>
		/// Delete folder at server
		/// </summary>
		/// <param name="fullFiles">array like { "2006\01\01\abd.txt", "..." } </param>
		public bool DeleteFiles(string[] fullFiles)
		{
			return wsFileManager.DeleteFiles(fullFiles);
		}

		/// <summary>
		/// Delete master file
		/// </summary>
		public void DeleteMasterFile()
		{
            wsFileManager.DeleteFileInfoMst(wsFileManager.InstanceId, _subFolder.ToString(), this.MasterFileNum);
			this.MasterFileNum = 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool ChooseFile()
		{
			string text = this.Text;
			btnBrowse_Click(this, EventArgs.Empty);
			return text != this.Text;
		}

		#endregion

		#region Privates

		/// <summary>
		/// 
		/// </summary>
		private void InitializeFX()
		{
			try
			{
				///
				/// FileUpload Webservice object
				/// 
				this.wsFileManager = new NF.Framework.FileTransfer.Client.FileManager();
				this.wsFileManager.ProgressChanged += new NF.Framework.FileTransfer.Client.ProgressChangedEventHandler(this.OnUpload_ProgressChanged);
				this.wsFileManager.StateChanged += new EventHandler(this.OnUpload_StateChanged);
				this.wsFileManager.Url = _FileManagerWebService;
				this.wsFileManager.InitializeInstance();

				// Get max file size allowing to upload
				this.MaxFileSize = this.wsFileManager.GetUploadSizeLimmit();
			}
			catch { }
		}


		/// <summary>
		/// Enable/disable buttons
		/// Preparing before, after upload
		/// </summary>
		/// <param name="start"></param>
		private void PreUpload(UploadState state)
		{
			///
			/// status
			/// 
			switch (state)
			{
				case UploadState.Transfer:
					pnlProgress.BringToFront();
					pnlProgress.Dock = DockStyle.Fill;
					pnlProgress.Visible = true;
					_StartTime = DateTime.Now;
					break;
				case UploadState.Stop:
					pnlProgress.SendToBack();
					pnlProgress.Dock = DockStyle.None;
					pnlProgress.Visible = false;
					this._Duration = DateTime.Now - this._StartTime;
					break;
				default:
					break;
			}

			Application.DoEvents();
		}

		/// <summary>
		/// Check if the file size is over max file size limit
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		private bool IsOverSize(string fileName)
		{
			if (MaxFileSize == 0) return false;

			FileInfo fi = new FileInfo(fileName);
			if (fi.Length > MaxFileSize)
				MessageBox.Show(string.Format("Max File Size: {0} MB", MaxFileSize / 1048576));

			return fi.Length > MaxFileSize;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Browse button's click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.FileName = txtFilePath.Text;
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				if (File.Exists(this.openFileDialog1.FileName))
				{
					if (!IsOverSize(this.openFileDialog1.FileName))
						this.Text = this.openFileDialog1.FileName;
				}
		}

		/// <summary>
		/// Event occurs when the process of uploading changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnUpload_ProgressChanged(object sender, NF.Framework.FileTransfer.Client.ProgressChangedEventArgs e)
		{
			this.ppBarFileUpload.Maximum = (int)e.Max;
			this.ppBarFileUpload.Value = (int)e.Value;
			Application.DoEvents();
		}

		/// <summary>
		/// Upload state changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnUpload_StateChanged(object sender, EventArgs e)
		{
			if (this.wsFileManager.State == NF.Framework.FileTransfer.Client.ProcessState.Completed)
			{
				if (this._MasterFileNum == 0)
				{
					this._MasterFileNum = this.wsFileManager.Save(wsFileManager.InstanceId);
				}
				else
				{
                    this.wsFileManager.DeleteAllFileInfoDetails(this.wsFileManager.InstanceId, this._subFolder.ToString(), this._MasterFileNum);
					this._MasterFileNum = this.wsFileManager.SaveAllWithFileInfoMst(this.wsFileManager.InstanceId, this._MasterFileNum);
				}

				// Kill the current instance
				this.wsFileManager.KillInstance();

				///
				/// After upload
				/// Change status
				/// 
				this.Text = Path.GetFileName(this.Text);

				// Stop uploading
				this.PreUpload(UploadState.Stop);

			}
			else if (this.wsFileManager.State == NF.Framework.FileTransfer.Client.ProcessState.Failed)
			{
				MessageBox.Show("Upload failed.");

				// Stop uploading
				this.PreUpload(UploadState.Stop);

			}

			RaiseProcessStateChangedEvent(new ProcessStateChangedEventArgs(this.wsFileManager.State, DateTime.Now - this._StartTime));
		}

		#endregion Event Handlers


	}

	internal enum UploadState
	{
		Transfer, FileSave, Stop
	}

	public delegate void ProcessStateChangedEventHandler(object sender, ProcessStateChangedEventArgs e);
	public class ProcessStateChangedEventArgs : System.EventArgs
	{
		private ProcessState _CurrentState;

		public ProcessState CurrentState
		{
			get { return _CurrentState; }
			set { _CurrentState = value; }
		}

		private TimeSpan _ElapsedTime;

		public TimeSpan ElapsedTime
		{
			get { return _ElapsedTime; }
			set { _ElapsedTime = value; }
		}

		public ProcessStateChangedEventArgs(ProcessState state, TimeSpan elapsedTime)
		{
			this._CurrentState = state;
			this._ElapsedTime = elapsedTime;
		}
	}
}
