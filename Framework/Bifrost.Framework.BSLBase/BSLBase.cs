using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Collections;

namespace Bifrost.Framework
{
	/// <summary>
	/// BLBase에 대한 요약 설명입니다.
	/// </summary>
	
	public class BSLBase 
	{
		#region 멤버변수들
		/// <summary>
		/// 서브시스템타입
		/// </summary>
		private SubSystemType subType = SubSystemType.FRAMEWORK;
		/// <summary>
		/// 서브팀상태값
		/// </summary>
		private bool bSubStatus;
		/// <summary>
		/// 로그레벨
		/// </summary>
		private string strLogLevel;		
		#endregion

		#region 생성자 - 로그레벨설정
		/// <summary>
		/// 생성자 - 로그레벨설정
		/// </summary>
		public BSLBase()
		{
			if(LogHandler.LogSubSystem)
			{
				string[] typeNamespace = this.GetType().Namespace.Split('.');
				string subtype = string.Empty;

				if (typeNamespace.Length > 1)				
					subtype = typeNamespace[1];				

				strLogLevel = Base.GetConfigString("LogLevel-" + subtype);
				bSubStatus = (strLogLevel == "Y" ? true:false);
			}	
		}
		#endregion


        //#region 공통 리소스, 메시지

        ///// <summary>
        ///// 메시지 가져오기
        ///// 지정팀의 해당메시지아이디 배열을 가져온다.
        ///// 배열[0] SubSystemType
        ///// 배열[1] MessageID
        ///// 배열[2] MessageType
        ///// 배열[3] DisplayMessage
        ///// 배열[4] SummaryMessage
        ///// </summary>
        ///// <param name="subSystemType">서브시스템타입</param>
        ///// <param name="MessageID">메시지아이디</param>
        ///// <returns>메시지배열</returns>
        //protected string[] Msgs(SubSystemType subSystemType, string messageID)
        //{
        //    return Dictionary.Msg(subSystemType, messageID);
        //}

        ///// <summary>
        ///// 메시지 가져오기
        ///// 해당메시지아이디의 배열을 가져온다.
        ///// 배열[0] SubSystemType
        ///// 배열[1] MessageID
        ///// 배열[2] MessageType
        ///// 배열[3] DisplayMessage
        ///// 배열[4] SummaryMessage
        ///// </summary>
        ///// <param name="MessageID">메시지아이디</param>
        ///// <returns>메시지배열</returns>
        //protected string[] Msgs(string messageID)
        //{
        //    return Dictionary.Msg(this.subType, messageID);
        //}

        //#endregion

		#region 로깅셋팅
		/// <summary>
		/// 로깅시작
		/// </summary>
		/// <param name="ts">TimeStamp개체</param>		
		protected void LoggingStart(ref TimeStamp ts)
		{
			try
			{
				if(bSubStatus)
				{
					ts = new TimeStamp();
					ts.TimeStampStart();
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 로깅끝
		/// </summary>
		/// <param name="ts">TimeStamp개체</param>
		/// <param name="target">this</param>
		/// <param name="sServiceName">서비스이름</param>		
		protected void LoggingEnd(TimeStamp ts,object target,string sServiceName)
		{
			//
			try
			{
				if(bSubStatus && ts != null)
				{
					ts.TimeStampEnd(target,sServiceName);
//					if (ts != null)	ts.Dispose();
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
