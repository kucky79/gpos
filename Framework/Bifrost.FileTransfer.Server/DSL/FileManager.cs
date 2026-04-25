using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using CMAX.Framework;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;

namespace CMAX.CO.DSL.SYS.FileManager
{
	public class FileManager : DSLBase
	{

		#region stored procedure ?ÅÏàò

		private const string UP_CO_INSERT_FILE_INFO = "dbo.UP_CO_InsertFileInfo";
		private const string UP_CO_DELETE_FILE_INFO = "dbo.UP_CO_DeleteFileInfo";
		private const string UP_CO_GET_FILE_INFO = "dbo.UP_CO_GetFileInfo";
		private const string UP_CO_GET_FILE_INFO_COUNT = "dbo.UP_CO_GetFileInfoCnt";

		private const string UP_CO_DELETE_FILE_INFO_DETAIL = "dbo.UP_CO_DeleteFileInfoDetail";
		private const string UP_CO_DELETE_FILE_INFO_DETAILS = "dbo.UP_CO_DeleteAllFileInfoDetails";

		private const string UP_CO_GET_FILE_INFO_DETAIL_SEQS = "dbo.UP_CO_GetFileInfoDetailSeqs";
		private const string UP_CO_GET_FILE_INFO_DETAIL_NAMES = "dbo.UP_CO_GetFileInfoDetailNames";
		private const string UP_CO_GET_FILE_INFO_DETAIL_GUIDS = "dbo.UP_CO_GetFileInfoDetailGuidFiles";
		private const string UP_CO_GET_FILE_INFO_DETAIL = "dbo.UP_CO_GetFileInfoDetail";


		#endregion

		/// <summary>
		///	Í∏∞Î≥∏ ?ùÏÑ±??
		/// </summary>
		public FileManager()
			: base()
		{
		}

		/// <summary>
		///	?§Î•∏ Connection???ªÍ∏∞ ?ÑÌï¥???¨Ïö©?òÎäî ?ùÏÑ±??
		/// </summary>
		public FileManager(SubSystemType subSystem)
			: base(subSystem)
		{
		}

		/// <summary>
		/// Insert T_CO_FileInfoMst,T_CO_FileInfoDetail
		/// </summary>
		/// <param name="subSystemType">?úÎ∏å?úÏä§???Ä??/param>
		/// <param name="intFileNum">?åÏùº Master FileNum</param>
		/// <param name="strGuidFileName">GuidFileName</param>
		/// <param name="strGuidFilePathName">FilePath</param>
		/// <param name="strfullname">?åÏùº Original FileName</param>
		/// <param name="strGuidFilePathName">FilePath</param>
		/// <param name="strFileSize">?åÏùº ?¨Ïù¥Ï¶?/param>
		/// <param name="strFileExt">?åÏùº ?ïÏû•??/param>
		/// <returns></returns>
		public int InsertFileInfo(string subSystemType, int intFileNum, string strGuidFileName,
									string strGuidFilePathName,string strfullname,
									string strFileSize, string strFileExt)
		{

			int intRet = 0;

			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_INSERT_FILE_INFO);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType);
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
		public int DeleteFileInfoMst(string subSystemType, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_DELETE_FILE_INFO);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType, ParameterDirection.Input);
			
			return ExecuteNonQuery();
		}

		/// <summary>
		/// Delete detail file by seq
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum"></param>
		/// <param name="intFileDetailSeq"></param>
		/// <returns></returns>
		public int DeleteFileInfoDetail(string subSystemType, int intFileDetailSeq)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_DELETE_FILE_INFO_DETAIL);
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
		public int DeleteAllFileInfoDetails(string subSystemType, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_DELETE_FILE_INFO_DETAILS);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType, ParameterDirection.Input);

			return ExecuteNonQuery();		
		}		

		/// <summary>
		/// Get list of file detail key by master file number
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="intFileNum">master file number</param>
		/// <returns></returns>
		public int[] GetFileInfoDetailSeqs(string subSystemType, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_GET_FILE_INFO_DETAIL_SEQS);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType, ParameterDirection.Input);

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
		public string[] GetFileInfoDetailNames(string subSystemType, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_GET_FILE_INFO_DETAIL_NAMES);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType, ParameterDirection.Input);

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
		public string[] GetGUIDFiles(string subSystemType, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_GET_FILE_INFO_DETAIL_GUIDS);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType, ParameterDirection.Input);

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
		public string[] GetGUIDFiles(string subSystemType, int[] fileDetailSeqs)
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

			strSQL = string.Format("SELECT FileGuidFullPathName + FileGuidName FROM dbo.T_CO_FileInfoDetail WHERE FileDetailSeq IN ({0})", strSQL);
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
		public DataSet GetFiles(string subSystemType, int intFileNum)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_GET_FILE_INFO);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileNum", SqlDbType.Int, 0, intFileNum, ParameterDirection.Input);
			AddSqlParameter(SqlCommandType.SelectCommand, "@systemSubType", SqlDbType.Char, 2, subSystemType, ParameterDirection.Input);
			return ExecuteFill();			
		}
		
		/// <summary>
		/// Get single detail file info
		/// </summary>
		/// <param name="intDetailFileSeq"></param>
		/// <returns></returns>
		public DataSet GetFile( int intDetailFileSeq)
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_GET_FILE_INFO_DETAIL);
			AddSqlParameter(SqlCommandType.SelectCommand, "@fileDetailSeq", SqlDbType.Int, 0, intDetailFileSeq, ParameterDirection.Input);
			return ExecuteFill();
		}
				
		/// <summary>
		/// FileInfoMst??Ïπ¥Ïö¥??
		/// Return newly inserted master file number
		/// </summary>
		public int CreateFileNum()
		{
			SetSqlCommand(SqlCommandType.SelectCommand, UP_CO_GET_FILE_INFO_COUNT);
			return (int)ExecuteScalar();			
		}		
	}
}
