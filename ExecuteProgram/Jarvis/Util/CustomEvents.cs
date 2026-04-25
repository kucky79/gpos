using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Bifrost.Win
{
	public class LoggedInEventArgs : EventArgs
	{
        public bool NeedRestart { get; set; } = false;

        public DataRow UserDataRow { get; set; } = null;

        public string EncodedUserID { get; set; } = string.Empty;

        public string EncodedPassword { get; set; }

        public LoggedInEventArgs()
		{
		}

		public LoggedInEventArgs(string encodedUserID, string encodedPassword, DataRow userInfoDR)
		{
			EncodedUserID = encodedUserID;
			UserDataRow = userInfoDR;
			EncodedPassword = encodedPassword;
		}
	}

	public delegate void LoggedInEventHandler(object sender, LoggedInEventArgs e);

	public class LoggedOutEventArgs : EventArgs
	{
        public string LoginId { get; set; }

        public bool ExitApp { get; set; } = false;


        public LoggedOutEventArgs()
		{

		}

		public LoggedOutEventArgs(string loginId)
		{
			this.LoginId = loginId;
		}

		public LoggedOutEventArgs(string loginId, bool exitApp)
		{
			this.LoginId = loginId;
			this.ExitApp = exitApp;
		}

	}

	public delegate void LoggedOutEventHandler(object sender, LoggedOutEventArgs e);

	public class LoginCancelledEventArgs : EventArgs
	{
        public bool Cancel { get; set; } = false;

        public LoginCancelledEventArgs()
		{
		}
	}

	public delegate void CancelledEventHandler(object sender, LoginCancelledEventArgs e);
}
