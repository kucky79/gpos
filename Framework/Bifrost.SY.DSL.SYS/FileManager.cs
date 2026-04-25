using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;
using Bifrost.Framework;

namespace Bifrost.SY.DSL.SYS
{
	public class FileManager : DSLBase
	{

		#region stored procedure 상수

		private const string WP_SY_INSERT_FILE_INFO = "WP_SY_InsertFileInfo";
		private const string WP_SY_DELETE_FILE_INFO = "WP_SY_DeleteFileInfo";
		private const string WP_SY_GET_FILE_INFO = "WP_SY_GetFileInfo";
		private const string WP_SY_GET_FILE_INFO_COUNT = "WP_SY_GetFileInfoCnt";

		private const string WP_SY_DELETE_FILE_INFO_DETAIL = "WP_SY_DeleteFileInfoDetail";
		private const string WP_SY_DELETE_FILE_INFO_DETAILS = "WP_SY_DeleteAllFileInfoDetails";

		private const string WP_SY_GET_FILE_INFO_DETAIL_SEQS = "WP_SY_GetFileInfoDetailSeqs";
		private const string WP_SY_GET_FILE_INFO_DETAIL_NAMES = "WP_SY_GetFileInfoDetailNames";
		private const string WP_SY_GET_FILE_INFO_DETAIL_GUIDS = "WP_SY_GetFileInfoDetailGuidFiles";
		private const string WP_SY_GET_FILE_INFO_DETAIL = "WP_SY_GetFileInfoDetail";


		#endregion

		/// <summary>
		///	기본 생성자
		/// </summary>
		public FileManager() : base()
		{
		}

		/// <summary>
		///	다른 Connection을 얻기 위해서 사용되는 생성자
		/// </summary>
		public FileManager(SubSystemType subSystem) : base(subSystem)
		{
		}

		/// <summary>
		/// Insert T_CO_FileInfoMst,T_CO_FileInfoDetail
		/// </summary>
		/// <param name="subSystemType">서브폴더구분</param>
		/// <param name="intFileNum">파일 Master FileNum</param>
		/// <param name="strGuidFileName">GuidFileName</param>
		/// <param name="strGuidFilePathName">FilePath</param>
		/// <param name="strfullname">파일 Original FileName</param>
		/// <param name="strGuidFilePathName">FilePath</param>
		/// <param name="strFileSize">파일 사이즈</param>
		/// <param name="strFileExt">파일 확장자</param>
		/// <returns></returns>
        public int InsertFileInfo(string SubFolder, int intFileNum, string strGuidFileName,
									string strGuidFilePathName, string strfullname,
									string strFileSize, string strFileExt)
		{

			int intRet = 0;

			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_INSERT_FILE_INFO);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileGuidName", SqlDbType.VarChar, 100, strGuidFileName);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileGuidFullPathName", SqlDbType.VarChar, 200, strGuidFilePathName);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileOrgName", SqlDbType.VarChar, 100, strfullname);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileFileSize", SqlDbType.Int, 0, Convert.ToInt32(strFileSize));
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileFileExt", SqlDbType.VarChar, 20, strFileExt);

			intRet = Convert.ToInt32(ExecuteScalar());
			return intRet;
		}


		/// <summary>
		/// Delete a master file and all detail files
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <returns></returns>
        public int DeleteFileInfoMst(string SubFolder, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_DELETE_FILE_INFO);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);

			return ExecuteNonQuery();
		}

		/// <summary>
		/// Delete detail file by seq
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <param name="intFileDetailSeq"></param>
		/// <returns></returns>
		public int DeleteFileInfoDetail(int intFileDetailSeq)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_DELETE_FILE_INFO_DETAIL);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileDetailSeq", SqlDbType.Int, 0, intFileDetailSeq, ParameterDirection.Input);

			return ExecuteNonQuery();
		}

		/// <summary>
		/// Delete detail file by seq
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <param name="intFileDetailSeq"></param>
		/// <returns></returns>
        public int DeleteAllFileInfoDetails(string SubFolder, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_DELETE_FILE_INFO_DETAILS);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);

			return ExecuteNonQuery();
		}

		/// <summary>
		/// Get list of file detail key by master file number
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum">master file number</param>
		/// <returns></returns>
        public int[] GetFileInfoDetailSeqs(string SubFolder, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_GET_FILE_INFO_DETAIL_SEQS);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);

			ArrayList files = new ArrayList();
			SqlDataReader reader = ExecuteReader();
			while (reader.Read())
			{
				if (reader.IsDBNull(0)) continue;
				files.Add(reader.GetInt32(0));
			}
			reader.Close();
			return (int[])files.ToArray(typeof(int));
		}

		/// <summary>
		/// Get list of file detail original name by master file number
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum">master file number</param>
		/// <returns></returns>
        public string[] GetFileInfoDetailNames(string SubFolder, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_GET_FILE_INFO_DETAIL_NAMES);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);

			ArrayList files = new ArrayList();
			SqlDataReader reader = ExecuteReader();
			while (reader.Read())
			{
				if (reader.IsDBNull(0)) continue;
				files.Add(reader.GetString(0));
			}
			reader.Close();
			return (string[])files.ToArray(typeof(string));
		}

		/// <summary>
		/// Return only full GUID file path of a master file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <returns></returns>
        public string[] GetGUIDFiles(string SubFolder, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_GET_FILE_INFO_DETAIL_GUIDS);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);

			ArrayList files = new ArrayList();
			SqlDataReader reader = ExecuteReader();
			while (reader.Read())
			{
				if (reader.IsDBNull(0)) continue;
				files.Add(reader.GetString(0));
			}
			reader.Close();
			return (string[])files.ToArray(typeof(string));
		}

		/// <summary>
		/// Return only full GUID file path of a master file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <returns></returns>
		public string[] GetGUIDFiles(int[] fileDetailSeqs)
		{
			if (fileDetailSeqs.Length == 0) return new string[0];

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < fileDetailSeqs.Length; i++)
			{
				sb.AppendFormat("{0},", fileDetailSeqs[i]);
			}
			string strSQL = sb.ToString();
			if (strSQL != string.Empty)
				strSQL = strSQL.Substring(0, strSQL.Length - 1);

			strSQL = string.Format("SELECT FileGuidFullPathName + FileGuidName FROM Bifrost.SY_FileInfoDetail WHERE FileDetailSeq IN ({0})", strSQL);
			SetSqlCommand(SqlCommandType.SelectCommand, strSQL, CommandType.Text);

			ArrayList files = new ArrayList();
			SqlDataReader reader = ExecuteReader();
			while (reader.Read())
			{
				if (reader.IsDBNull(0)) continue;
				files.Add(reader.GetString(0));
			}
			reader.Close();
			return (string[])files.ToArray(typeof(string));
		}

		/// <summary>
		/// Return only GUID file name list of a master file
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <returns></returns>
        public string[] GetGUIDNames(string SubFolder, int masterFileNum)
		{
            string strSQL = string.Format("SELECT FileGuidName FROM Bifrost.SY_FileInfoDetail WHERE FileDetailSeq IN (SELECT FileDetailSeq FROM SY_FileInfoMst WHERE FileNum = {0} AND SubFolder = '{1}')", masterFileNum, SubFolder);
			SetSqlCommand(SqlCommandType.SelectCommand, strSQL, CommandType.Text);

			ArrayList files = new ArrayList();
			SqlDataReader reader = ExecuteReader();
			while (reader.Read())
			{
				if (reader.IsDBNull(0)) continue;
				files.Add(reader.GetString(0));
			}
			reader.Close();
			return (string[])files.ToArray(typeof(string));
		}



		/// <summary>
		/// Return DataSet contains File Detail Info
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <returns></returns>
        public DataSet GetFiles(string SubFolder, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_GET_FILE_INFO);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);
			return ExecuteFill();
		}

		/// <summary>
		/// Get single detail file info
		/// </summary>
		/// <param name="intDetailFileSeq"></param>
		/// <returns></returns>
		public DataSet GetFile(int intDetailFileSeq)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_GET_FILE_INFO_DETAIL);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileDetailSeq", SqlDbType.Int, 0, intDetailFileSeq, ParameterDirection.Input);
			return ExecuteFill();
		}

		/// <summary>
		/// FileInfoMst의 카운터
		/// Return newly inserted master file number
		/// </summary>
        public int CreateFileNum(string SubFolder)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, WP_SY_GET_FILE_INFO_COUNT);
            AddSqlParameter(SqlCommandType.SelectCommand, "@SubFolder", SqlDbType.NVarChar, 20, SubFolder, ParameterDirection.Input);
			return (int)ExecuteScalar();
		}
	}
}
