using System;
using System.Data;
using Bifrost;

namespace Bifrost
{
	/// <summary>
	/// Converter에 대한 요약 설명입니다.
	/// </summary>
	public class Converter
	{

		/// <summary>
		/// DataSet를 Xml String으로 변환
		/// </summary>
		/// <param name="oDs">DataSet Object</param>
		/// <returns>xml문자열</returns>
		public static string DataSetToXmlString(DataSet oDs)
		{
			string sTableName = string.Empty;
			string sColName = string.Empty;
			System.Text.StringBuilder oSbXml = null;
			try
			{
				oSbXml = new System.Text.StringBuilder(4096);
				oSbXml.Append("<NewDataSet>" + Environment.NewLine);
	
				for (int i=0; i<oDs.Tables.Count; i++)
				{
					for (int j=0; j<oDs.Tables[i].Rows.Count; j++)
					{
						sTableName = oDs.Tables[i].TableName;
						oSbXml.Append("\t<"+ sTableName +">" + Environment.NewLine);
						for(int k=0; k<oDs.Tables[i].Columns.Count; k++)
						{
							sColName = oDs.Tables[i].Columns[k].ColumnName;
							oSbXml.Append("\t\t<" + sColName + "><![CDATA["
								+ Convert.ToString(oDs.Tables[i].Rows[j][k])
								+ "]]></" + sColName + ">" + Environment.NewLine);
						}
						oSbXml.Append("\t</" + sTableName + ">" + Environment.NewLine);
					}
				}
				oSbXml.Append("</NewDataSet>" + Environment.NewLine);
				return oSbXml.ToString();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// DataSet를 Xml String으로 변환
		/// Hashtable의 Key에 변환할 Table이름을
		/// Hashtable의 value에 재정렬한 column 이름을 string[]로 구성
		/// </summary>
		/// <param name="oDs">DataSet Object</param>
		/// <param name="columnOrders">정렬할 column</param>
		/// <returns>xml문자열</returns>
		public static string DataSetToXmlString(DataSet oDs,System.Collections.Hashtable columnOrders)
		{
			string sTableName = string.Empty;
			string sColName = string.Empty;
			System.Text.StringBuilder oSbXml = null;
			string[] strColumns = null;
			try
			{
				oSbXml = new System.Text.StringBuilder(4096);
				oSbXml.Append("<NewDataSet>" + Environment.NewLine);
	
				for (int i=0; i<oDs.Tables.Count; i++)
				{
					for (int j=0; j<oDs.Tables[i].Rows.Count; j++)
					{
						sTableName = oDs.Tables[i].TableName;
						strColumns = (string[])columnOrders[sTableName];
						if (strColumns == null) throw new ArgumentException("Hashtable Structure Error!","columnOrders");
						oSbXml.Append("\t<"+ sTableName +">" + Environment.NewLine);
						for (int k=0; k<strColumns.Length; k++)
						{
							oSbXml.Append("\t\t<" + strColumns[k] + "><![CDATA["
								+ Convert.ToString(oDs.Tables[i].Rows[j][strColumns[k]])
								+ "]]></" + strColumns[k] + ">" + Environment.NewLine);
						}
						oSbXml.Append("\t</" + sTableName + ">" + Environment.NewLine);
					}
				}
				oSbXml.Append("</NewDataSet>" + Environment.NewLine);
				return oSbXml.ToString();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Byte단위의 파일 사이즈를 특정 단위로 문자열 변환
		/// </summary>
		/// <param name="fileLength">file 크기</param>
		/// <param name="filelenUnit">file 크기 단위</param>
		/// <returns>파일 크기 문자열</returns>
		public static string GetFormatFileLength(int fileLength, FileLenUnit filelenUnit)
		{
			string strSize = string.Empty;
			try
			{
				switch (filelenUnit)
				{
					case FileLenUnit.Gigabyte :
						strSize = String.Format("{0:#,##0.##}", fileLength/1024*1024*1024);
						break;
					case FileLenUnit.Megabyte :
						strSize = String.Format("{0:#,##0.##}", fileLength/1024*1024);
						break;
					case FileLenUnit.Killobyte :
						strSize = String.Format("{0:#,##0.##}", fileLength/1024);
						break;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return strSize;
		}

		/// <summary>
		/// DataTime형식을 Formating된 문자열로 변환(날짜)
		/// </summary>
		/// <param name="dateValue">날짜</param>
		/// <returns>formating된 날짜문자열</returns>
		public static string GetFormatDate(DateTime dateValue)
		{			
			string strReplaceDate = null;			
			strReplaceDate = dateValue.ToString("yyyy-MM-dd(ddd)");
			
			return strReplaceDate;
		}

		/// <summary>
		/// DataTime형식을 Formating된 문자열로 변환(날짜+시각)
		/// </summary>
		/// <param name="dateValue">날짜</param>
		/// <returns>formating된 날짜문자열</returns>
		public static string GetFormatDateTime(DateTime dateValue)
		{			
			string strReplaceDate = null;			
			strReplaceDate = dateValue.ToString("yyyy-MM-dd(ddd) HH:mm:ss");
			
			return strReplaceDate;
		}
	}
}
