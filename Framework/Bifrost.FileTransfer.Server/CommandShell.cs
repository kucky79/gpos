using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Bifrost.FileTransfer.Server
{
	class CommandShell : IDisposable
	{
		private Process cmdProcess = null;

		public CommandShell()
		{
		}

		#region Disposing
        /// <summary>
		///     Dispose of this object's resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		}

		/// <summary>
		///		Free the instance variables of this object.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return; 
            //threadOutput.
		}

		#endregion

		public string RegGAC(string sCommand)
		{
			ProcessStartInfo pinfo = new ProcessStartInfo("cmd");
			string strOutput = string.Empty;
			StreamReader sr;
			StreamReader err;
			StreamWriter sw;

            cmdProcess = new Process
            {
                StartInfo = pinfo
            };

            pinfo.UseShellExecute = false;
			pinfo.CreateNoWindow = true;
			pinfo.RedirectStandardError = true;
			pinfo.RedirectStandardInput = true;
			pinfo.RedirectStandardOutput = true;

			cmdProcess.Start();

			sw = cmdProcess.StandardInput;
			sr = cmdProcess.StandardOutput;
			err = cmdProcess.StandardError;

			sw.AutoFlush = true;
			sw.WriteLine(sCommand);

			sw.Close();

			strOutput = sr.ReadToEnd();
			strOutput += err.ReadToEnd();

			cmdProcess.Close();

			return strOutput;
		}

		private void ReadError()
		{
			char[] buffer = new char[1024];
			int len = 0;
			string sError=string.Empty;

			while (!cmdProcess.HasExited)
			{
				len = cmdProcess.StandardError.Read(buffer, 0, 1024);

				if (len > 0)
				{
					sError = new String(buffer, 0, len);
				}
			}
		}

		private void ReadOutput()
		{
			char[] buffer = new char[1024];
			int len = 0;
			string output;

			while (!cmdProcess.HasExited)
			{
				len = cmdProcess.StandardOutput.Read(buffer, 0, 1024);
				if (len > 0)
				{
					output = new String(buffer, 0, len);
				}
			}
		}

		public string ParsingError(string strError)
		{
			int nErr = 0;
			string strErrMsg = string.Empty;
			string strReturn = string.Empty;

			strReturn = "Failure";
			strErrMsg = "Failure";
			nErr = strError.IndexOf(strErrMsg);
			if (nErr < 0)
			{
				strReturn = "OK";
			}
			else
			{
				strErrMsg = "strong";
				nErr = strError.IndexOf(strErrMsg);
				if (nErr < 0)
				{
					strReturn = "FAILURE";
				}
				else
				{
					strReturn = "STRONG";
				}
			}
			return strReturn;
		}
	}
}
