using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.IO;
using System.Transactions;
using System.Web;

using Bifrost.Framework;

namespace Bifrost.SY.BSL.SYS
{
	public class FileManager : Bifrost.Framework.BSLBase
	{
		#region 전역변수

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
        public int InsertFileInfoMst(string SubFolder, string[] detailFiles)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			// 변수 선언
			int intFileNum = 0;

			try
			{
                using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
                    string strGuidFilePathName = String.Empty;
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY), System.IO.Path.DirectorySeparatorChar, SubFolder, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
                    /// ex) UploadFileDirectory\SubFolder\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
                        intFileNum = CreateFileNum(SubFolder);
						for (int i = 0; i < detailFiles.Length; i++)
						{
							FileInfo fi = new FileInfo(detailFiles[i]);

							// Move file to target location
							string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", fi.Name);

							// Call DSL
                            dslFileManager.InsertFileInfo(SubFolder, intFileNum, strGuidFileName, strGuidFilePathName, fi.Name, fi.Length.ToString(), fi.Extension);

							// move file
							fi.MoveTo(string.Concat(strGuidFilePathName, strGuidFileName));
						}

						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public int InsertFileInfoMst(string SubFolder, HttpFileCollection oHfc)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			// 변수 선언
			int intFileNum = 0;

			try
			{
                using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = String.Empty;
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
                        System.IO.Path.DirectorySeparatorChar, SubFolder, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
                    // ex) UploadFileDirectory\SubFolder\
					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
                        intFileNum = CreateFileNum(SubFolder);
						for (int i = 0; i < oHfc.Count; i++)
						{


							string strFileName = Base64.Base64Decode(oHfc[i].FileName,  System.Text.Encoding.Default);
							string strFileExt = strFileName.Remove(0, strFileName.LastIndexOf(".") + 1);

							string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", strFileName);
							oHfc[i].SaveAs(strGuidFilePathName + strGuidFileName);

                            dslFileManager.InsertFileInfo(SubFolder, intFileNum, strGuidFileName,
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
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public int InsertFileInfoDetail(string SubFolder, int masterFileNum, string detailFile)
		{

			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			// New File Detail Seq
			int intNewFileDetailSeq = 0;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
                    string strGuidFilePathName = String.Empty; ;
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
                        System.IO.Path.DirectorySeparatorChar, SubFolder, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
                    // ex) UploadFileDirectory\SubFolder\

					if (!(Directory.Exists(strGuidFilePathName)))
					{
						Directory.CreateDirectory(strGuidFilePathName);
					}

					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						FileInfo fi = new FileInfo(detailFile);

						// Move file to target location
						string strGuidFileName = string.Concat(System.Guid.NewGuid().ToString(), "-", fi.Name);						

						// Call DSL
                        intNewFileDetailSeq = dslFileManager.InsertFileInfo(SubFolder, masterFileNum, strGuidFileName,
																	strGuidFilePathName, fi.Name,
																	  fi.Length.ToString(), fi.Extension);

						// move file
						fi.MoveTo(string.Concat(strGuidFilePathName, strGuidFileName));

						// Finalize transaction
						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public int InsertFileInfoDetails(string SubFolder, int masterFileNum, string[] detailFiles)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
                        System.IO.Path.DirectorySeparatorChar, SubFolder, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
                    // ex) UploadFileDirectory\SubFolder\

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

							// Call DSL
                            dslFileManager.InsertFileInfo(SubFolder, masterFileNum, strGuidFileName,
																	  strGuidFilePathName, fi.Name,
																	  fi.Length.ToString(), fi.Extension);

							// move file
							fi.MoveTo(string.Concat(strGuidFilePathName, strGuidFileName));
						}

						transScope.Complete();
						transScope.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public int InsertFileInfoDetails(string SubFolder, int masterFileNum, HttpFileCollection oHfc)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					///
					/// These varisables will be deleted if going out of this Using scope
					/// 
					string strGuidFilePathName = DateTime.Now.ToString("yyyyMM");
					strGuidFilePathName = string.Concat(Base.GetConfigString(UPLOAD_DIRECTORY_CONFIG_KEY),
                        System.IO.Path.DirectorySeparatorChar, SubFolder, System.IO.Path.DirectorySeparatorChar);

					///
					/// Create directory for each month, per subsystemtype
                    // ex) UploadFileDirectory\SubFolder\

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

                            dslFileManager.InsertFileInfo(SubFolder, masterFileNum, strGuidFileName,
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
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public int DeleteFileInfoMst(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			// 변수 선언
			int intRet = 0;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager())
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get list of full file path
                        string[] fullFiles = dslFileManager.GetGUIDFiles(SubFolder, masterFileNum);

                        intRet = dslFileManager.DeleteFileInfoMst(SubFolder, masterFileNum);

						// Delete files
						DeleteFileDetailInfos(fullFiles);

						// Comit transaction
						transScope.Complete();
						transScope.Dispose();

					}

				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
		public int DeleteFileInfoDetail(string SubFolder, int masterFileNum, int fileDetailSeq)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			// 변수 선언
			int intRet = 0;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get list of full file path
                        string[] fullFiles = dslFileManager.GetGUIDFiles(SubFolder, masterFileNum);

						// Delete in db
						intRet = dslFileManager.DeleteFileInfoDetail(fileDetailSeq);

						// Delete file
						DeleteFileDetailInfos(fullFiles);

						// Comit transaction
						transScope.Complete();
						transScope.Dispose();
					}

				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
		/// <param name="fileDetailSeq">상세파일번호</param>
		/// <returns></returns>
        public int DeleteFileInfoDetails(string SubFolder, int masterFileNum, int[] fileDetailSeqs)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get list of full file path
                        string[] fullFiles = dslFileManager.GetGUIDFiles(fileDetailSeqs);

						for (int i = 0; i < fileDetailSeqs.Length; i++)
						{
							dslFileManager.DeleteFileInfoDetail(fileDetailSeqs[i]);
						}

						// Delete files
						DeleteFileDetailInfos( fullFiles);

						// Comit transaction
						transScope.Complete();
					//	transScope.Dispose();
					}

				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
		/// <param name="fileDetailSeq">상세파일번호</param>
		/// <returns></returns>
        public int DeleteAllFileInfoDetails(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
					using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required))
					{
						// Get file paths
                        string[] fullFiles = dslFileManager.GetGUIDFiles(SubFolder, masterFileNum);

						// remove db records
                        dslFileManager.DeleteAllFileInfoDetails(SubFolder, masterFileNum);

						// Delete files
						DeleteFileDetailInfos(fullFiles);

						// Comit transaction
						transScope.Complete();
						transScope.Dispose();
					}

				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public string[] GetGUIDFiles(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] arrRet = null;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
                    arrRet = dslFileManager.GetGUIDFiles(SubFolder, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public string[] GetGUIDFiles(string SubFolder, int[] fileDetailSeqs)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] arrRet = null;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
                    arrRet = dslFileManager.GetGUIDFiles(fileDetailSeqs);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return arrRet;
		}

		/// <summary>
		/// Return only list of GUID file name
		/// </summary>
		/// <param name="masterFileNum">FileInfoMst - FileNum</param>
		/// <param name="subSystemType">systemSubType</param>
		/// <returns></returns>
        public string[] GetGUIDNames(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] arrRet = null;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
                    arrRet = dslFileManager.GetGUIDNames(SubFolder, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public int[] GetFileInfoDetailSeqs(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			int[] arrRet = null;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
                    arrRet = dslFileManager.GetFileInfoDetailSeqs(SubFolder, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public DataSet GetFiles(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			DataSet dsReturn = null;
			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager())
				{
                    dsReturn = dslFileManager.GetFiles(SubFolder, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
        public string[] GetFileInfoDetailNames(string SubFolder, int masterFileNum)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			string[] fileNames = null;
			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager())
				{
                    fileNames = dslFileManager.GetFileInfoDetailNames(SubFolder, masterFileNum);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			DataSet dsReturn = null;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager())
				{
					dsReturn = dslFileManager.GetFile(detailFileSeq);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
		/// FileInfoMst의 카운터
		/// </summary>
		/// <returns></returns>
        private int CreateFileNum(string SubFolder)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			int intRet = 0;

			try
			{
				using (Bifrost.SY.DSL.SYS.FileManager dslFileManager = new Bifrost.SY.DSL.SYS.FileManager(SubSystemType.FRAMEWORK))
				{
                    intRet = dslFileManager.CreateFileNum(SubFolder);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(_subType, ex, this.GetType());
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
		private void DeleteFileDetailInfos(string[] filePaths)
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
