using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.IO;
using System.Transactions;
using System.Web;

using CMAX.Framework;

namespace CMAX.CO.BSL.SYS.FileManager
{
	public class FileManager : CMAX.Framework.BSLBase
	{
		#region 

		private const string UPLOAD_DIRECTORY_CONFIG_KEY = "UploadFileDirectory";
		private SubSystemType _subType = SubSystemType.FRAMEWORK;

		#endregion

		#region Master(Insert)

		// 
		/// <summary>
		/// Insert a new master file with collection of full detail file paths
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="detailFiles"></param>
		/// <returns></returns>
		public int InsertFileInfoMst(string subSystemType, string[] detailFiles)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			//  
			int intFileNum = 0;

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
						System.IO.Path.DirectorySeparatorChar, strGuidFilePathName, 
						System.IO.Path.DirectorySeparatorChar, subSystemType, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
					// ex) UploadFileDirectory\200508\CO\
					//	   UploadFileDirectory\200508\SM\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						intFileNum = CreateFileNum();
						for (int i = 0; i < detailFiles.Length; i++)
						{
							FileInfo fi = new FileInfo(detailFiles[i]);

							// Move file to target location
							string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", fi.Name);
							fi.MoveTo(string.Concat(strGuidFilePathName, strGuidFileName));

							// Call DSL
							dslFileManager.InsertFileInfo(subSystemType, intFileNum, strGuidFileName,
																	  strGuidFilePathName, fi.Name, 
																	  fi.Length.ToString(), fi.Extension);
						}

						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return intFileNum;
		}
		// 
		/// <summary>
		/// Insert a new master file with httpFileCollection
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="oHfc"></param>
		/// <returns></returns>
		public int InsertFileInfoMst(string subSystemType, HttpFileCollection oHfc)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			//  
			int intFileNum = 0;

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
						System.IO.Path.DirectorySeparatorChar, strGuidFilePathName,
						System.IO.Path.DirectorySeparatorChar, subSystemType, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
					// ex) UploadFileDirectory\200508\CO\
					//	   UploadFileDirectory\200508\SM\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						intFileNum = CreateFileNum();
						for (int i = 0; i < oHfc.Count; i++)
						{
							string strFileName = Base64.Base64Decode(oHfc[i].FileName, System.Text.Encoding.Default);
							string strFileExt = strFileName.Remove(0, strFileName.LastIndexOf(".") + 1);

							string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", strFileName);
							oHfc[i].SaveAs(strGuidFilePathName + strGuidFileName);

							dslFileManager.InsertFileInfo(subSystemType, intFileNum, strGuidFileName,
																	  strGuidFilePathName,
																	  strFileName,
																	  oHfc[i].ContentLength.ToString(), 
																	  strFileExt);
						}

						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return intFileNum;			
		}

		#endregion

		#region Detail(Insert)
		// 
		/// <summary>
		/// Master exists, insert detail file only
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="detailFile"></param>
		/// <returns></returns>
		public int InsertFileInfoDetail(string subSystemType, int masterFileNum, string detailFile)
		{

			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			// New File Detail Seq
			int intNewFileDetailSeq = 0;

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
						System.IO.Path.DirectorySeparatorChar, strGuidFilePathName,
						System.IO.Path.DirectorySeparatorChar, subSystemType, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
					// ex) UploadFileDirectory\200508\CO\
					//	   UploadFileDirectory\200508\SM\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						FileInfo fi = new FileInfo(detailFile);

						// Move file to target location
						string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", fi.Name);
						fi.MoveTo(string.Concat(strGuidFilePathName, strGuidFileName));

						// Call DSL
						intNewFileDetailSeq = dslFileManager.InsertFileInfo(subSystemType, masterFileNum, strGuidFileName,
																	strGuidFilePathName, fi.Name,
																	  fi.Length.ToString(), fi.Extension);

						// Finalize transaction
						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return intNewFileDetailSeq;		
		}

		// 
		/// <summary>
		/// Master exists, insert detail files only
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="detailFile"></param>
		/// <returns></returns>
		public int InsertFileInfoDetails(string subSystemType, int masterFileNum, string[] detailFiles)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
						System.IO.Path.DirectorySeparatorChar, strGuidFilePathName,
						System.IO.Path.DirectorySeparatorChar, subSystemType, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
					// ex) UploadFileDirectory\200508\CO\
					//	   UploadFileDirectory\200508\SM\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						for (int i = 0; i < detailFiles.Length; i++)
						{
							FileInfo fi = new FileInfo(detailFiles[i]);

							// Move file to target location
							string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", fi.Name);
							fi.MoveTo(string.Concat(strGuidFilePathName, strGuidFileName));

							// Call DSL
							dslFileManager.InsertFileInfo(subSystemType, masterFileNum, strGuidFileName,
																	  strGuidFilePathName, fi.Name,
																	  fi.Length.ToString(), fi.Extension);
						}

						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return masterFileNum;
		}

		// 
		/// <summary>
		/// Master exists, insert detail files only by HttpFileCollection
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="masterFileNum"></param>
		/// <param name="oHfc"></param>
		/// <returns></returns>
		public int InsertFileInfoDetails(string subSystemType, int masterFileNum, HttpFileCollection oHfc)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
						System.IO.Path.DirectorySeparatorChar, strGuidFilePathName,
						System.IO.Path.DirectorySeparatorChar, subSystemType, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
					// ex) UploadFileDirectory\200508\CO\
					//	   UploadFileDirectory\200508\SM\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						for (int i = 0; i < oHfc.Count; i++)
						{
							string strFileName = Base64.Base64Decode(oHfc[i].FileName, System.Text.Encoding.Default);
							string strFileExt = strFileName.Remove(0, strFileName.LastIndexOf(".") + 1);

							string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", strFileName);
							oHfc[i].SaveAs(strGuidFilePathName + strGuidFileName);

							dslFileManager.InsertFileInfo(subSystemType, masterFileNum, strGuidFileName,
																	  strGuidFilePathName,
																	  strFileName,
																	  oHfc[i].ContentLength.ToString(),
																	  strFileExt);
						}

						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return masterFileNum;		
		}

		#endregion

		#region Master(Delete)
		/// <summary>
		/// Delete master file info and all related detail files
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public int DeleteFileInfoMst(string subSystemType, int masterFileNum)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			//  
			int intRet = 0;

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager())
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get list of full file path
						string[] fullFiles = dslFileManager.GetGUIDFiles(subSystemType, masterFileNum);

						intRet = dslFileManager.DeleteFileInfoMst(subSystemType, masterFileNum);

						// Delete files
						DeleteFileDetailInfos(subSystemType, fullFiles);

						// Comit transaction
						transScope.Complete();
						transScope.Dispose();

					}

				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return intRet;

		}
		#endregion
		
		#region Detail(Delete)
		/// <summary>
		/// Delete 1 file detail
		/// </summary>
		/// <param name="masterFileNum"></param>
		/// <param name="subSystemType"></param>
		/// <param name="fileDetailSeq"></param>
		/// <returns></returns>
		public int DeleteFileInfoDetail(string subSystemType, int masterFileNum, int fileDetailSeq)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			//  
			int intRet = 0;			

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get list of full file path
						string[] fullFiles = dslFileManager.GetGUIDFiles(subSystemType, masterFileNum);

						// Delete in db
						intRet = dslFileManager.DeleteFileInfoDetail(subSystemType, fileDetailSeq);
						
						// Delete file
						DeleteFileDetailInfos(subSystemType, fullFiles);

						// Comit transaction
						transScope.Complete();
						transScope.Dispose();
					}

				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return intRet;
		}


	
		/// <summary>
		/// Delete multiple file details
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <param name="fileDetailSeq">Ϲȣ</param>
		/// <returns></returns>
		public int DeleteFileInfoDetails(string subSystemType, int masterFileNum, int[] fileDetailSeqs)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);
			
			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get list of full file path
						string[] fullFiles = dslFileManager.GetGUIDFiles(subSystemType, fileDetailSeqs);

						for (int i = 0; i < fileDetailSeqs.Length; i++)
						{
							dslFileManager.DeleteFileInfoDetail(subSystemType, fileDetailSeqs[i]);
						}

						// Delete files
						DeleteFileDetailInfos(subSystemType, fullFiles);

						// Comit transaction
						transScope.Complete();
						transScope.Dispose();
					}

				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return masterFileNum;
		}


		/// <summary>
		/// Delete multiple file details
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <param name="fileDetailSeq">Ϲȣ</param>
		/// <returns></returns>
		public int DeleteAllFileInfoDetails(string subSystemType, int masterFileNum)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);
			
			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get file paths
						string[] fullFiles = dslFileManager.GetGUIDFiles(subSystemType, masterFileNum);

						// remove db records
						dslFileManager.DeleteAllFileInfoDetails(subSystemType, masterFileNum);

						// Delete files
						DeleteFileDetailInfos(subSystemType, fullFiles);
						
						// Comit transaction
						transScope.Complete();
						transScope.Dispose();
					}

				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return masterFileNum;
		}



		#endregion

		#region detail(select)

		/// <summary>
		/// Return only list of GUID file (include path and guid name)
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public string[] GetGUIDFiles(string subSystemType,int masterFileNum)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] arrRet = null;			
			
			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					arrRet = dslFileManager.GetGUIDFiles(subSystemType, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return arrRet;
		}

		/// <summary>
		/// Return only list of GUID file (include path and guid name)
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public string[] GetGUIDFiles(string subSystemType, int[] fileDetailSeqs)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] arrRet = null;

			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					arrRet = dslFileManager.GetGUIDFiles(subSystemType, fileDetailSeqs);
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return arrRet;
		}

		/// <summary>
		/// Return only list of GUID file (include path and guid name)
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public int[] GetFileInfoDetailSeqs(string subSystemType, int masterFileNum)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);
			int[] arrRet = null;
			
			try
			{
				using(CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					arrRet = dslFileManager.GetFileInfoDetailSeqs(subSystemType, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return arrRet;
		}

		/// <summary>
		/// Return T_CO_FileInfoDetail table's fields 
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public DataSet GetFiles(string subSystemType,int masterFileNum)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			DataSet dsReturn = null;			
			try
			{
				using(CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager())
				{
					dsReturn = dslFileManager.GetFiles(subSystemType,masterFileNum);
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return dsReturn;
		}

		/// <summary>
		/// Get real file names only
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public string[] GetFileInfoDetailNames(string subSystemType, int masterFileNum)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] fileNames = null;			
			try
			{
				using(CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager())
				{
					fileNames = dslFileManager.GetFileInfoDetailNames(subSystemType, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return fileNames;
		}		

		/// <summary>
		/// Return T_CO_FileInfoDetail table's fields
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
		public DataSet GetFile(int detailFileSeq)
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);

			DataSet dsReturn = null;
			
			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager())
				{
					dsReturn = dslFileManager.GetFile(detailFileSeq);
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return dsReturn;
		}

		#endregion

		#region Privates

		/// <summary>
		/// FileInfoMst ī
		/// </summary>
		/// <returns></returns>
		private int CreateFileNum()
		{
			//α
			TimeStamp ts = null;
			LoggingStart(ref ts);
			int intRet = 0;
			
			try
			{
				using (CMAX.CO.DSL.SYS.FileManager.FileManager dslFileManager = new CMAX.CO.DSL.SYS.FileManager.FileManager(SubSystemType.FRAMEWORK))
				{
					intRet = dslFileManager.CreateFileNum();
				}
			}
			catch (Exception ex)
			{
				CMAXException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return intRet;
		}

		/// <summary>
		/// Delete physical files
		/// </summary>
		/// <param name="intFileNum"></param>
		/// <returns></returns>
		private void DeleteFileDetailInfos(string subSystemType, string[] filePaths)
		{
			for (int i = 0; i < filePaths.Length; i++)
			{
				try
				{
					System.IO.File.Delete(filePaths[i]);
				}
				catch { }
			}
		}

		#endregion Privates
	}
}
