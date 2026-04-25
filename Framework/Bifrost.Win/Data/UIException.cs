using System;
using System.Collections.Generic;
using System.Text;

using Bifrost.Common;
using Bifrost.Win.Util;

namespace Bifrost.Win.Data
{
	public class UIException : ApplicationException
	{
		/// <summary>
		/// Parse Exception to get detail error message
		/// </summary>
		/// <param name="innerException"></param>
		/// <returns></returns>
		public static string ParseException(Exception innerException, out int msgType)
		{
			string msg = innerException.Message;
			int nIdx = msg.IndexOf("[SqlException]");
			string customMsg = string.Empty;
			
			// SqlException
			if (nIdx != -1)
			{
				nIdx = msg.IndexOf("\nNumber: ", nIdx);
				if (nIdx != -1)
				{
					int mIdx = msg.IndexOf("\n", nIdx + 1);
					if (mIdx != -1)
					{
						int errorNo = 0;
						Int32.TryParse(msg.Substring(nIdx + 9, mIdx - nIdx - 9), out errorNo);
						if (errorNo != 0)
						{
							customMsg = new ResManager().GetString(new SqlErrorMapping().GetMessageID(errorNo.ToString())).Trim();
						}
					}
				}
			}

			/// 
			/// msgType
			/// 0: ´Ù±¹¾î Msg
			/// 1: Inner Exception Msg
			/// 2: Detail Msg
			/// 
			/// 
			int mType = 2;

			if (customMsg == string.Empty)
			{	
				customMsg = msg;
				nIdx = msg.IndexOf("{");
				if (nIdx != -1)
				{
					int aIdx = msg.IndexOf("}", nIdx);
					if (aIdx != -1)
					{
						mType = 1;
						customMsg = msg.Substring(nIdx + 1, aIdx - nIdx - 1);
					}
				}
				else
				{
					mType = 2;
					nIdx = msg.IndexOf("--- End of inner exception stack trace ---");
					if (nIdx != -1)
					{						
						customMsg = msg.Substring(0, nIdx);
					}
					else
					{
						customMsg = msg;
					}
				}
			}
			else
			{
				mType = 0;
			}

			msgType = mType;
			return customMsg;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sysType">SubSystemType</param>
		/// <param name="subType">2-level system id like: EA, SM...</param>
		/// <param name="exceptionID">Exception Code</param>
		/// <param name="innerException"></param>
		public UIException(string exceptionID, Exception innerException) : base(string.Empty, innerException)
		{
			if (innerException != null)
			{
				_Message = innerException.Message;
			}
			else
			{
				_Message = new ResManager().GetString(exceptionID);
			}

			//_Message = ParseException(innerException);
			//if(string.IsNullOrEmpty(_Message))
			//{							
			//    _Message = new ResManager(ResourceType.Message).GetString("0000");
			//}
			//_Message = new ResManager(ResourceType.Message).GetString(exceptionID);
		}

        public UIException(Exception innerException) : base(string.Empty, innerException)
        {
            if (innerException != null)
            {
				string[] message = innerException.Message.Split(new string[] { "\r\n" }, StringSplitOptions.None );

				if (message.Length > 0)
					_Message = message[0];
				else
					_Message = innerException.Message;

			}
        }

        private string _Message = string.Empty;

		/// <summary>
		/// User-Friendly error message
		/// </summary>
		public override string Message
		{
			get
			{
				return _Message;
			}
		}

		
	}
}
