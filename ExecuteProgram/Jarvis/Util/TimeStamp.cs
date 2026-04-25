using System;
using System.Net;
using System.IO;
//using System.EnterpriseServices;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bifrost
{
	/// <summary>
	/// TimeStamp에 대한 요약 설명입니다.
	/// </summary>
	public class TimeStamp : Base
	{
		/// <summary>
		/// 생성자
		/// </summary>
		public TimeStamp()
		{
		}

		private DateTime _startTime;

		#region TimeStampStart
		/// <summary>
		/// 시작시간담기
		/// </summary>
		public void TimeStampStart()
		{
			_startTime = DateTime.Now;
		}	
		#endregion

		#region TimeStampEnd
		/// <summary>
		/// 사용자,네임스페이스,서비스네임등등 종료로그찍기
		/// </summary>
		/// <param name="target">오브젝트</param>
		/// <param name="sServiceName">서비스네임</param>
		public void TimeStampEnd(object target,string sServiceName)
		{
			string sLogDir = GetConfigString("TIMELOGPATH");
			DateTime endTime = DateTime.Now;
			TimeSpan timeSpan = new TimeSpan(endTime.Ticks - _startTime.Ticks);
			string sFullPath = null;
			string sSubSystemName = null;
			FileStream fs = null;
			StreamWriter sWriter = null;
			string strNamespace = null;			
			//string logLevel = null;

			try
			{	
				strNamespace = target.GetType().Namespace;				
				sSubSystemName = target.GetType().Namespace.Split('.')[2];
				
				sFullPath = 
					string.Format(@"{0}\{1}-{2}.txt", 
					sLogDir, DateTime.Now.ToString("yyyyMMdd"), sSubSystemName);
				string strUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;	
				
				string sLogInfo = _startTime.ToString("yyyy-MM-dd HH:mm:ss") + "." + 
					_startTime.Millisecond.ToString() + " | " + 
					strUserName + "|" + strNamespace + " | " + target.GetType().Name + " | " + sServiceName + " | " +
					timeSpan.TotalMilliseconds.ToString();
				if (!Directory.Exists(sLogDir))
					Directory.CreateDirectory(sLogDir);
				
				fs = new FileStream(sFullPath,FileMode.Append,FileAccess.Write,FileShare.Write);		
				sWriter = new StreamWriter(fs,System.Text.Encoding.Default);
				sWriter.WriteLine(sLogInfo);
			}
			catch//(Exception ex)
			{
				//System.Diagnostics.EventLog.WriteEntry("TimeStamp",ex.ToString(),System.Diagnostics.EventLogEntryType.Error);
			} 	
			finally
			{
				if (sWriter != null) sWriter.Close();
				if (fs != null) fs.Close();
			}
		}

		/// <summary>
		/// 사용자,네임스페이스,서비스네임등등 종료로그찍기
		/// </summary>
		/// <param name="target">오브젝트</param>
		/// <param name="sServiceName">서비스네임</param>
		/// <param name="strSPName">SP네임</param>
		/// <param name="pack">SQL파라미터</param>
		public void TimeStampEnd(object target,string sServiceName,string strSPName,System.Data.SqlClient.SqlParameter[] pack)
		{
			string sLogDir = GetConfigString("TIMELOGPATH");
			DateTime endTime = DateTime.Now;
			TimeSpan timeSpan = new TimeSpan(endTime.Ticks - _startTime.Ticks);
			//string sFullPath = sLogDir + "\\"+ DateTime.Now.ToShortDateString() + "TimeLog.txt";	
			string sFullPath = string.Format(@"{0}\{1} TimeLog.txt",sLogDir,DateTime.Now.ToString("yyyy-MM-dd"));
	
			FileStream fs = null;
			StreamWriter sWriter = null;
			string strParameters = "";
			System.Text.StringBuilder sbParameters = null;

			try
			{	
				sbParameters = new System.Text.StringBuilder(1024);

				foreach (System.Data.SqlClient.SqlParameter p in pack)
				{					
					if (p.Value != null)
					{
						sbParameters.Append(p.ParameterName);
						sbParameters.Append("=");
						sbParameters.Append(p.Value.ToString());
						sbParameters.Append(",");
					}
					else
					{
						sbParameters.Append(p.ParameterName);
						sbParameters.Append("=");
						sbParameters.Append(",");
					}
				}

				strParameters = sbParameters.ToString();

				if (strParameters.Length > 0)
					strParameters = strParameters.Remove(strParameters.Length-1,1);
				string strUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;	
				string strNamespace = target.GetType().Namespace;

				if (String.Compare(strNamespace,"ASP",true)==0)				
					strNamespace = target.GetType().BaseType.ToString(); //웹인경우에는 BaseType에서 Namespace를 뽑는다.
				
				string sLogInfo = _startTime.ToString() + "." + 
					_startTime.Millisecond.ToString() + " | " + 
					strUserName + "|" + strNamespace + " | " + target.GetType().Name + " | " + sServiceName + " | " + 
					strSPName + " | " + strParameters + " | " + timeSpan.TotalMilliseconds.ToString();

				
				if (!Directory.Exists(sLogDir))
					Directory.CreateDirectory(sLogDir);
				
				fs = new FileStream(sFullPath,FileMode.Append,FileAccess.Write,FileShare.Write);		
				sWriter = new StreamWriter(fs,System.Text.Encoding.Default);

				sWriter.WriteLine(sLogInfo);
				
			}
			catch//(Exception ex)
			{
				//System.Diagnostics.EventLog.WriteEntry("TimeStamp",ex.ToString(),System.Diagnostics.EventLogEntryType.Error);
			} 	
			finally
			{
				if (sWriter != null)
					sWriter.Close();
				if (fs != null)
					fs.Close();
			}
		}		
		#endregion
	}
}
