using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Configuration;

namespace Bifrost.FileTransfer.Server
{
	/// <summary>
	/// FileManager handles
	///		- File Upload
	///		- File Download
	///		- File reading, saving to file system and database
	/// </summary>
	public class FileManager : System.Web.Services.WebService
	{
		#region Overridable, Settings : UploadFileDirectory & File DB connection string

		/// <summary>
		/// This method could be overrides by inheriter to change 
		/// upload folder location
		/// </summary>
		/// <returns></returns>
		protected virtual string GetUploadDirectory()
		{
			return Server.MapPath("~/Uploads/");
		}

        /// <summary>
		/// This method could be overrides by inheriter to change 
		/// upload folder location
		/// </summary>
		/// <returns></returns>
		protected virtual string GetReportsDirectory()
        {
            return Server.MapPath("~/Reports/");
        }

        /// <summary>
        /// Connection String to the database which contains 2 tables
        /// Master & Detail
        /// </summary>
        /// <returns></returns>
        protected virtual string GetFileMgtConnectString()
		{
			return string.Empty;
		}

		/// <summary>
		/// Return list of newly updated file names
		/// </summary>
		/// <param name="clientTimeStamp"></param>
		/// <returns></returns>
		[WebMethod]
		public virtual string[] GetUpdateFiles(string[] localFileInfos)
		{
			return new string[0];
		}

		/// <summary>
		/// Return list of framework newly updated file names
		/// </summary>
		/// <param name="clientTimeStamp"></param>
		/// <returns></returns>
		[WebMethod]
		public virtual string[] GetBaseUpdateFiles(string[] localFileInfos)
		{
			return new string[0];
		}

        /// <summary>
		/// Return list of framework newly updated file names
		/// </summary>
		/// <param name="clientTimeStamp"></param>
		/// <returns></returns>
		[WebMethod]
        public virtual string[] GetReportsUpdateFiles(string[] localFileInfos)
        {
            return new string[0];
        }

        /// <summary>
		/// Return list of framework newly updated file names
		/// </summary>
		/// <param name="clientTimeStamp"></param>
		/// <returns></returns>
		[WebMethod]
        public virtual string[] GetSubReportsUpdateFiles(string[] localFileInfos)
        {
            return new string[0];
        }

        [WebMethod]
		public virtual string NeedReloadAllFiles(string serverFolder)
		{
			return string.Empty;
		}

		/// <summary>
		/// To use with Application Deployment
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[WebMethod]
		public int SaveFileWithShellExecute(string instanceId, string targetFolder, bool gacRegister)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				int result = instance.SaveFileWithShellExecute(targetFolder, gacRegister);
				if (result == 0)
				{
					instance.Kill();
				}
				return result;
			}
			else 
			{ 
				return (int)UploadReturnCodes.InstanceNotFound; 
			}
		}

		#endregion

		#region File Upload

		/// <summary>
		/// This method initializes a buffered upload. It creates a temporary file on the
		/// server's temp directory and a session object that references the temp file. It returns
		/// a string value representing the InstanceId of the buffered upload. This id should be 
		/// passed to the proceding calls to AppendChunck(), Save(), and Close() methods.
		/// </summary>
		/// <param name="Filesize">The total amount of bytes in the file to be uploaded.</param>
		[WebMethod(EnableSession = true)]
		public string Initialization() 
		{
			Instance instance = new Instance(GetUploadDirectory(), GetFileMgtConnectString());
			Session.Add(instance.InstanceId, instance);
			return instance.InstanceId;
		}

		/// <summary>
		/// Begin upload file
		/// </summary>
		/// <param name="targetFileName"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
        public int BeginUpload(string instanceId, string targetFileName, string SubFolder)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.BeginUpload(targetFileName, SubFolder);
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }
		}

		/// <summary>
		/// This method appends a chunk of data to an upload in progress.
		/// </summary>
		/// <param name="InstanceId">The string returned by Initialize() method.</param>
		/// <param name="buffer">An array of bytes to be appended to the upload in progress.</param>
		/// <param name="offset">The offset where to start writting.</param>
		/// <param name="length">The size of the buffer.</param>
		/// <returns>
		/// An integer value that if greater than zero represents a new offset to reset the
		/// transfer to, otherwise the return value can be casted against UploadHanlder.ReportCodes
		/// to translate the return code.
		/// </returns>
		[WebMethod(EnableSession = true)]
		public int AppendChunk(string instanceId, byte[] buffer, int offset, int count)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				return instance.AppendChunk(buffer, offset, count);
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }
		}

		/// <summary>
		/// Begin upload file
		/// </summary>
		/// <param name="targetFileName"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public int EndUpload(string instanceId)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				return instance.EndUpload();
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }
		}

		/// <summary>
		/// This method saves the uploaded file as a permanent file to the filesystem.
		/// Return a master file key
		/// </summary>
		/// <param name="param name="instanceId">The string returned by Initialize() method.</param>
		[WebMethod(EnableSession = true)]
		public int Save(string instanceId)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				int result = instance.Save();
				if (result > 0)
				{
					instance.Kill();
				}
				return result;
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }			
		}

		/// <summary>
		/// Save all uploaded files to target folder
		/// and create db records (1 master & multi files)
		/// </summary>
		/// <param name="instanceId"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public int SaveAll(string instanceId)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				int result = instance.SaveAll();
				if (result > 0)
				{
					instance.Kill();
				}
				return result;
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }			
		}

		/// <summary>
		/// Save all uploaded files to target folder
		/// with provided masterFileNum
		/// </summary>
		/// <param name="instanceId"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public int SaveAllWithFileInfoMst(string instanceId, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				int result = instance.SaveAllWithFileInfoMst(masterFileNum);
				if (result > 0)
				{
					instance.Kill();
				}
				return result;
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }
		}

		/// <summary>
		/// Save all uploaded files to target folder
		/// with provided masterFileNum
		/// </summary>
		/// <param name="instanceId"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public virtual bool UploadFileOnly(string targetServerFolder, string subPath, byte[] buffer)
		{
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="instanceId"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public virtual bool DeleteFolder(string targetServerFolder)
		{
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fullFiles"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public virtual bool DeleteFiles(string[] fullFiles)
		{
			return true;
		}

		#endregion

		#region File Download
		
		/// <summary>
		/// Return filesize or error
		/// </summary>
		/// <param name="fileName"></param>
		[WebMethod(EnableSession=true)]
		public string[] BeginDownload(string instanceId, string fileName)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				return instance.BeginDownload(fileName);
			}
			else { return new string[] { Convert.ToString((int)UploadReturnCodes.InstanceNotFound) }; }
		}

		/// <summary>
		/// Read a byte array from file
		/// </summary>
		/// <param name="instanceId"></param>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <param name="fileSize"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
		public int GetChunk(string instanceId, int offset, int count, int fileSize, out byte[] buffer)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				byte[] buff;
				int result = instance.GetChunk(offset, count, fileSize, out buff);
				buffer = buff;
				return result;
			}
			else {
				buffer = null;
				return (int)UploadReturnCodes.InstanceNotFound; 
			}
		}

		/// <summary>
		/// End download process
		/// </summary>
		/// <param name="instanceId"></param>
		[WebMethod(EnableSession = true)]
		public int EndDownload(string instanceId)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				return instance.EndDownload();
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }
		}

		#endregion		

		#region File Management

		/// <summary>
		/// File size limitation
		/// </summary>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
		public virtual int GetUploadSizeLimmit()
		{
			return 10485760; // 10mb
		}

		/// <summary>
		/// Delete a master file and all sub physical files
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
        public int DeleteFileInfoMst(string instanceId, string SubFolder, int masterFileNum)
		{						
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.DeleteFileInfoMst(SubFolder, masterFileNum);
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }			
		}

		/// <summary>
		/// Get list of seq file from mastet table
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
        public int[] GetFileInfoDetailSeqs(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.GetFileInfoDetailSeqs(SubFolder, masterFileNum);
			}
			else { return new int[0]; }
		}

		/// <summary>
		/// Return a dataset containing both master and detail file info tables
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
        public DataSet GetFiles(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.GetFiles(SubFolder, masterFileNum);
			}
			else { return null; }			
		}

		/// <summary>
		/// Return a list of real files name belonged to this master filer
		/// </summary>
		/// <param name="instanceId"></param>
        /// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
        public string[] GetFileInfoDetailNames(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.GetFileInfoDetailNames(SubFolder, masterFileNum);
			}
			else { return new string[0]; }
		}

		/// <summary>
		/// Return a list of real files name belonged to this master filer
		/// </summary>
		/// <param name="instanceId"></param>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
        public string[] GetFileInfoDetailGuidNames(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.GetFileInfoDetailGuidNames(SubFolder, masterFileNum);
			}
			else { return new string[0]; }
		}

		/// <summary>
		/// Return a list of real files name belonged to this master filer
		/// </summary>
		/// <param name="instanceId"></param>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
        public string[] GetFileInfoDetailGuids(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.GetFileInfoDetailGuids(SubFolder, masterFileNum);
			}
			else { return new string[0]; }
		}	

		/// <summary>
		/// Delete multiple detail files both in db and file system
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="fileDetailSeq"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
        public int DeleteFileInfoDetails(string instanceId, string SubFolder, int masterFileNum, int[] fileDetailSeqs)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.DeleteFileInfoDetails(SubFolder, masterFileNum, fileDetailSeqs);
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }	
		}

		/// <summary>
		/// Delete a all detail file & physical files in master file
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
        public int DeleteAllFileInfoDetails(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
                return instance.DeleteAllFileInfoDetails(SubFolder, masterFileNum);
			}
			else { return (int)UploadReturnCodes.InstanceNotFound; }
		}

		/// <summary>
		/// Get Url of file
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="masterFileNum"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
        public string GetFileInfoUrl(string instanceId, string SubFolder, int masterFileNum)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				return instance.GetFileInfoUrl(SubFolder, masterFileNum);
			}
			else { return ((int)UploadReturnCodes.InstanceNotFound).ToString(); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="SubFolder"></param>
		/// <param name="fileInfoDetailSeq"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true)]
		public string GetFileInfoDetailUrl(string instanceId, int fileInfoDetailSeq)
		{
			Instance instance = Instance.GetInstanceById(instanceId);
			if (instance != null)
			{
				return instance.GetFileInfoUrl(fileInfoDetailSeq);
			}
			else { return ((int)UploadReturnCodes.InstanceNotFound).ToString(); }
		}

		#endregion
	}
}
