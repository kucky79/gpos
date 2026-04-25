using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace Bifrost.FileTransfer.Server
{
	/// <summary>
	/// Ultility class
	/// </summary>
	public class Instance : IDisposable
	{
		#region Privates 

		private string _tempFilename = string.Empty;
		private string _filename = string.Empty;
        private string _SubFolder = "";
		private string _FileDbConnectString = string.Empty;
		private NameValueCollection _uploadedFiles = new NameValueCollection();

		/// <summary>
		/// Log error message to log file
		/// </summary>
		/// <param name="ex"></param>
		private static void LogMessage(string message)
		{		
			string logFile = System.Web.HttpContext.Current.Server.MapPath("~") + "\\error.log";
			if(!System.IO.File.Exists(logFile))
				System.IO.File.Create(logFile).Close();
			System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile, true);
			sw.WriteLine(string.Format("[{0} {1}] ", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()) + message);
			sw.Close();
		}

        #endregion

        #region Properties

        /// <summary>
        /// Unique identifier for this instance
        /// </summary>
        public string InstanceId { get; set; } = string.Empty;
        public long BytesReceived { get; set; } = 0;
        public string TargetFolder { get; } = string.Empty;

        /// <summary>
        /// Return list of uploaded files
        /// </summary>
        public NameValueCollection UploadedFiles
		{
			get 
			{ 
				return _uploadedFiles; 
			}
		}
	
		#endregion

		#region Constructors, Statics
		
		public Instance (string targetFolder, string fileDbConnectString) 
		{
			
			this.TargetFolder = targetFolder;
			this._FileDbConnectString = fileDbConnectString;
			this.InstanceId = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Remove this instance from Session
		/// </summary>
		public void Kill()
		{
			if (Instance.GetInstanceById(this.InstanceId) != null) 
			{ 
				System.Web.HttpContext.Current.Session.Remove(this.InstanceId); 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="InstanceId"></param>
		/// <returns></returns>
		public static Instance GetInstanceById(string instanceId)
		{
			if (System.Web.HttpContext.Current.Session[instanceId] != null)
			{
				return (Instance)System.Web.HttpContext.Current.Session[instanceId];
			}
			else {
				Instance instance = new Instance(string.Empty, string.Empty);
				System.Web.HttpContext.Current.Session[instanceId] = instance;
				return instance; 
			}
		}

		#endregion

		#region File Upload

		/// <summary>
		/// Start upload process
		/// </summary>
		/// <returns></returns>
        public int BeginUpload(string targetFileName, string SubFolder)
		{
			this._filename = targetFileName;
            this._SubFolder = SubFolder;

			UploadReturnCodes result = UploadReturnCodes.Success;
			try
			{
				this._tempFilename = System.IO.Path.GetTempFileName();
				if (System.IO.File.Exists(_tempFilename))
					System.IO.File.Delete(_tempFilename);
				System.IO.File.Create(this._tempFilename).Close();
				
			}
			catch(Exception ex)
			{
				result = UploadReturnCodes.IOError;
				LogMessage(ex.ToString());
			}
			return (int)result;
		}

		/// <summary>
		/// Append bytes to temporary file
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public int AppendChunk(byte[] buffer, int offset, int count)
		{
			try
			{
				if (!System.IO.File.Exists(this._tempFilename))
				{
					LogMessage("File Not Foud: " + this._tempFilename);
					return (int)UploadReturnCodes.CannotFindTempFile; 
				}

				System.IO.FileStream FileStream = new System.IO.FileStream(this._tempFilename, System.IO.FileMode.Append);
				FileStream.Write(buffer, 0, count);
				FileStream.Close();

				return (int)UploadReturnCodes.Success;				
			}
			catch (Exception ex)
			{
				LogMessage(ex.ToString());
				return (int)UploadReturnCodes.IOError;
			}
		}


		/// <summary>
		/// Save temporary file path to list
		/// </summary>
		/// <returns></returns>
		public int EndUpload()
		{
			_uploadedFiles.Add(this._tempFilename, this._filename);
			return (int)UploadReturnCodes.Success;
		}

		/// <summary>
		/// Save to real file
		/// </summary>
		/// <returns></returns>
		public int Save()
		{
			///
			/// Change the tempFileName's name to _fileName
			/// and alll InsertFileInfoMst
			/// 
			string tempPath = System.IO.Path.GetDirectoryName(this._tempFilename);
			System.IO.FileInfo fi = new System.IO.FileInfo(this._tempFilename);
			if (!fi.Exists) return 0;

			string realFile = string.Concat(tempPath, System.IO.Path.DirectorySeparatorChar, this._filename);
			if (System.IO.File.Exists(realFile)) System.IO.File.Delete(realFile);
				
			fi.MoveTo(realFile);
			return InsertFileInfoMst(_SubFolder, new string[] { realFile });
		}

		/// <summary>
		/// Save all uploaded files to target folder 
		/// and create db records 
		/// </summary>
		/// <returns></returns>
		public int SaveAll()
		{
			string[] fullPathFiles = new string[this._uploadedFiles.Count];

			for (int i = 0; i < this._uploadedFiles.Keys.Count; i++)
			{
				string tempFilePath = this._uploadedFiles.Keys[i];
				System.IO.FileInfo fi = new System.IO.FileInfo(tempFilePath);
				if (!fi.Exists) continue;

				string realFilePath = string.Concat(System.IO.Path.GetDirectoryName(tempFilePath),
					System.IO.Path.DirectorySeparatorChar, this._uploadedFiles.GetValues(tempFilePath)[0]);

				if (System.IO.File.Exists(realFilePath)) System.IO.File.Delete(realFilePath);

				fi.MoveTo(realFilePath);
				fullPathFiles[i] = realFilePath;
			}

            return InsertFileInfoMst(_SubFolder, fullPathFiles);
		}

		/// <summary>
		/// Insert list of files uploaded to existing master file
		/// </summary>
		/// <param name="masterFileNum"></param>
		public int SaveAllWithFileInfoMst(int masterFileNum)
		{
			string[] fullPathFiles = new string[this._uploadedFiles.Count];

			for (int i = 0; i < this._uploadedFiles.Keys.Count; i++)
			{
				string tempFilePath = this._uploadedFiles.Keys[i];
				System.IO.FileInfo fi = new System.IO.FileInfo(tempFilePath);
				if (!fi.Exists) continue;

				string realFilePath = string.Concat(System.IO.Path.GetDirectoryName(tempFilePath),
					System.IO.Path.DirectorySeparatorChar, this._uploadedFiles.GetValues(tempFilePath)[0]);

				if (System.IO.File.Exists(realFilePath)) System.IO.File.Delete(realFilePath);
				fi.MoveTo(realFilePath);
				fullPathFiles[i] = realFilePath;
			}

            return InsertFileInfoDetails(_SubFolder, masterFileNum, fullPathFiles);
		}

		/// <summary>
		/// Save uploaded file to target folder and registe GAC (.net assembly dll)
		/// </summary>
		/// <param name="targetFolder"></param>
		/// <param name="gacRegister"></param>
		/// <returns>00: success, 1: copy ok, gac:failed, 2: failed  </returns>
		public int SaveFileWithShellExecute(string targetFolder, bool gacRegister)
		{
			///
			/// Change the tempFileName's name to _fileName
			/// 
			System.IO.FileInfo fi = new System.IO.FileInfo(this._tempFilename);
			if (!fi.Exists) return (int)UploadReturnCodes.CannotFindTempFile;
			if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

			string realFile = string.Concat(targetFolder, System.IO.Path.DirectorySeparatorChar, this._filename);
			if (System.IO.File.Exists(realFile)) System.IO.File.Delete(realFile);

			/// 
			/// move to target
			/// 
			fi.MoveTo(realFile);

			int intReturn=0;

			/// 
			/// register GAC
			/// 
			if(gacRegister)
			{
				string strRemoveCommand = @"gacutil -u " + Path.GetFileNameWithoutExtension(realFile);
				string strRegisterCommand = @"gacutil -i " + realFile;

				string strGACReturn = string.Empty;
				using (CommandShell cmdShell = new CommandShell())
				{
					string strRemoveOut = cmdShell.RegGAC(strRemoveCommand);
					string strRegisterOut = cmdShell.RegGAC(strRegisterCommand);
					strGACReturn = cmdShell.ParsingError(strRegisterOut);
				}
				

				// sError= ParsingError(strRemoveOut);		
				if (strGACReturn == "OK")
				{
					intReturn = 0;
				}
				else if (strGACReturn == "FAILURE")
				{
					intReturn = 1;
				}
				else
				{
					intReturn = 2;
				}
			}
			return intReturn;
		}

		#endregion

		#region File Download

		/// <summary>
		/// Begin downloadin file from targetFolder
		/// </summary>
		/// <param name="fileName">GUID File Name, target folder is the folder in file server, for example</param>
		public string[] BeginDownload(string fileName)
		{
            //Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
			this._tempFilename = fileName; //this._TargetFolder + fileName;
			System.IO.FileInfo fi = new System.IO.FileInfo(this._tempFilename);

			return new string[] { fi.Length.ToString(), fi.CreationTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
				fi.LastWriteTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"), fi.LastAccessTime.ToString("yyyy-MM-dd HH:mm:ss")};
		}

		/// <summary>
		/// Read a segment from file
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public int GetChunk(int offset, int count, int fileSize, out byte[] buffer)
		{
			//Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
			if (!System.IO.File.Exists(this._tempFilename))
			{
				buffer = null;
				return (int)DownloadReturnCodes.CannotFindFile;
			}

			int tempFilesize = (int)new System.IO.FileInfo(this._tempFilename).Length;
			if (fileSize != tempFilesize)
			{
				buffer = null;
				return (int)DownloadReturnCodes.IOError;
			}

			if (tempFilesize > offset)
			{
				byte[] buff = new byte[count];

				System.IO.FileStream FileStream = new System.IO.FileStream(this._tempFilename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				FileStream.Seek(offset, System.IO.SeekOrigin.Begin);

				int bytesRead = FileStream.Read(buff, 0, count);

				FileStream.Close();
				buffer = buff;
				return bytesRead;
			}
			else
			{
				buffer = new byte[0];
				return (int)DownloadReturnCodes.Success;
			}
		}

		/// <summary>
		/// End downloading process
		/// </summary>
		public int EndDownload()
		{
			return (int)UploadReturnCodes.Success;
		}

		#endregion

		#region Master File Handling

		/// <summary>
		/// Create a master file from list of physical files
		/// Call BSL to 
		///		- create a master file
		///		- create list of detail files linked to newly inserted master key
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="detailFiles"></param>
		/// <returns>Return master file number</returns>
        private int InsertFileInfoMst(string SubFolder, string[] detailFiles)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.InsertFileInfoMst(SubFolder, detailFiles);
		}

		/// <summary>
		/// Delete master file and all detail files in DB also 
		/// delete physical files
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns>Return 1 if succeed, 0 fails, < 0</returns>
        public int DeleteFileInfoMst(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.DeleteFileInfoMst(SubFolder, masterFileNum);
		}

		/// <summary>
		/// Get list of seq file from mastet table
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public int[] GetFileInfoDetailSeqs(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.GetFileInfoDetailSeqs(SubFolder, masterFileNum);
		}

		/// <summary>
		/// Return a dataset containing both master and detail file info tables
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public DataSet GetFiles(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.GetFiles(SubFolder, masterFileNum);			
		}

		/// <summary>
		/// Return a list of real files name belonged to this master filer
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public string[] GetFileInfoDetailNames(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.GetFileInfoDetailNames(SubFolder, masterFileNum);
		}

		/// <summary>
		/// Return a list of guid files name belonged to this master filer
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public string[] GetFileInfoDetailGuidNames(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.GetGUIDNames(SubFolder, masterFileNum);		
		}

		/// <summary>
		/// Return a list of guid files name belonged to this master filer
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public string[] GetFileInfoDetailGuids(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.GetGUIDFiles(SubFolder, masterFileNum);
		}

		/// <summary>
		/// Get Url of file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public string GetFileInfoUrl(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            DataSet dsFiles = fm.GetFiles(SubFolder, masterFileNum);

			if (dsFiles.Tables[0].Rows.Count == 0)
			{
				return string.Empty;
			}

			string fileUrl = dsFiles.Tables[0].Rows[0]["FileGuidFullPathName"].ToString().Trim();
			fileUrl = string.Concat("http:", fileUrl.Replace("\\", "/"));
			if (fileUrl[fileUrl.Length - 1] != '/')
			{
				fileUrl += "/";
			}

			fileUrl = string.Concat(fileUrl, dsFiles.Tables[0].Rows[0]["FileGuidName"].ToString().Trim());
			return fileUrl;
		}

		/// <summary>
		/// Get url of file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="fileInfoDetailSeq"></param>
		/// <returns></returns>
		public string GetFileInfoUrl(int fileInfoDetailSeq)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
			DataSet dsFiles = fm.GetFile(fileInfoDetailSeq);

			if (dsFiles.Tables[0].Rows.Count == 0)
			{
				return string.Empty;
			}

			string fileUrl = dsFiles.Tables[0].Rows[0]["FileGuidFullPathName"].ToString().Trim();
			fileUrl = string.Concat("http:", fileUrl.Replace("\\", "/"));
			if (fileUrl[fileUrl.Length - 1] != '/')
			{
				fileUrl += "/";
			}

			fileUrl = string.Concat(fileUrl, dsFiles.Tables[0].Rows[0]["FileGuidName"].ToString().Trim());
			return fileUrl;
		}

		#endregion

		#region Detail Files Handling

		/// <summary>
		/// Insert a list of physical files to an existing master file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="detailFiles"></param>
		/// <returns></returns>
        private int InsertFileInfoDetails(string SubFolder, int masterFileNum, string[] detailFiles)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.InsertFileInfoDetails(SubFolder, masterFileNum, detailFiles);
		}

		/// <summary>
		/// Delete multiple detail files both in db and file system
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="fileDetailSeq"></param>
		/// <returns>0: fail, 1: success</returns>
        public int DeleteFileInfoDetails(string SubFolder, int masterFileNum, int[] fileDetailSeqs)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.DeleteFileInfoDetails(SubFolder, masterFileNum, fileDetailSeqs);
		}

		/// <summary>
		/// Delete multiple detail files both in db and file system
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="fileDetailSeq"></param>
		/// <returns>0: fail, 1: success</returns>
        public int DeleteAllFileInfoDetails(string SubFolder, int masterFileNum)
		{
			Bifrost.SY.BSL.SYS.FileManager fm = new Bifrost.SY.BSL.SYS.FileManager();
            return fm.DeleteAllFileInfoDetails(SubFolder, masterFileNum);
		}		

		#endregion

		#region IDisposable Members

		void IDisposable.Dispose()
		{			
		}

		#endregion
	}
}
