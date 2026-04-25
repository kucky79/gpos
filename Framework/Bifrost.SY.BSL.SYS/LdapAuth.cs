using System;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Security.Principal;
using System.Security;

using CDOEXM;

using CMAX.Framework;

namespace CMAX.CO.BSL.SYS.BLK
{
	/// <summary>
	/// Summary description for LdapAuth
	/// </summary>
	public class LdapAuth : CMAX.Framework.Base
	{
		private string _path;

		private DirectoryEntry GetDirectoryEntry()
		{
			return new DirectoryEntry(_path);
		}

		private DirectoryEntry GetDirectoryEntry(string domain, string userName, string password)
		{
			try
			{
				string domainAndUsername = domain + @"\" + userName;
				DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, password);
				return entry;
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		private DirectoryEntry GetDirectoryEntry(string userName)
		{
			DirectoryEntry entry = null;			

			try
			{
				entry = new DirectoryEntry(_path);
				DirectorySearcher search = new DirectorySearcher(entry, "(SAMAccountName=" + userName + ")");
				SearchResult result = search.FindOne();
				if (result != null)
				{
					entry = new DirectoryEntry(result.Path);
				}
				else
				{
					entry = null;
				}
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			return entry;
		}

		/// <summary>
		/// Method to enable a user account in the AD.
		/// </summary>
		/// <param name="de"></param>
		private void EnableAccount(DirectoryEntry de)
		{
			//UF_DONT_EXPIRE_PASSWD 0x10000
			int exp = (int)de.Properties["userAccountControl"].Value;
			de.Properties["userAccountControl"].Value = exp | 0x0001;
			de.CommitChanges();
			//UF_ACCOUNTDISABLE 0x0002
			int val = (int)de.Properties["userAccountControl"].Value;
			de.Properties["userAccountControl"].Value = val & ~0x0002;
			de.CommitChanges();
		}

		public LdapAuth(string path)
		{
			_path = path;
		}

		/// <summary>
		/// Is Authenticated?
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="username"></param>
		/// <param name="pwd"></param>
		/// <returns></returns>
		public bool IsAuthenticated(string domain, string userName, string password)
		{
			try
			{
				return GetDirectoryEntry(domain, userName, password).Name != null;
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
				return false;
			}
		}

		/// <summary>
		/// User Exist?
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public bool Exists(string adminAccount, string addminPassword, string domain, string userName)
		{
			IntPtr userHandle = IntPtr.Zero;
			WindowsImpersonationContext impersonationContext = null;
			DirectoryEntry entry = null;
			bool bOK = false;
			try
			{
				///
				/// Impersonate
				///
				bool loggedOn = LogonUser(adminAccount, domain, addminPassword,
					LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out userHandle);

				if (!loggedOn)
					throw new Win32Exception(Marshal.GetLastWin32Error());

				impersonationContext = WindowsIdentity.Impersonate(userHandle);

				entry = new DirectoryEntry(_path);
				DirectorySearcher search = new DirectorySearcher(entry, "(SAMAccountName=" + userName + ")");
				SearchResult result = search.FindOne();
				bOK = result != null;
			}
			catch (COMException comEx)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, comEx, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if (entry != null)
					entry.Close();
				if (userHandle != IntPtr.Zero)
					CloseHandle(userHandle);
				if (impersonationContext != null)
					impersonationContext.Undo();
			}
			return bOK;
		}

		/// <summary>
		/// User Exist?
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public bool Exists(string domain, string userName)
		{
			DirectoryEntry entry = null;
			bool bOK = false;
			try
			{
				entry = new DirectoryEntry(_path);
				DirectorySearcher search = new DirectorySearcher(entry, "(SAMAccountName=" + userName + ")");
				SearchResult result = search.FindOne();
				bOK = result != null;
			}
			catch (COMException comEx)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, comEx, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if (entry != null)
					entry.Close();
			}
			return bOK;
		}
		
		/// <summary>
		/// Delete an user in domain
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public bool DeleteUser(string userName)
		{
			DirectoryEntry usr = null;
			bool bOK = false;
			try
			{
				usr = GetDirectoryEntry(userName);
				usr.Parent.Children.Remove(usr);
				bOK = true;
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if (usr != null)
					usr.Close();
			}
			return bOK;
		}

		/// <summary>
		/// Set password to user
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool SetPassword(string domain, string adminAccount, string adminPassword, string userName, string password, string newPassword)
		{
			IntPtr userHandle = IntPtr.Zero;
			WindowsImpersonationContext impersonationContext = null;
			DirectoryEntry usr = null;
			bool bOK = false;
			try
			{
				///
				/// Impersonate
				///
				bool loggedOn = LogonUser(adminAccount, domain, adminPassword,
					LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out userHandle);

				if (!loggedOn)
					throw new Win32Exception(Marshal.GetLastWin32Error());

				impersonationContext = WindowsIdentity.Impersonate(userHandle);

				usr = GetDirectoryEntry(domain, userName, password);
				usr.Invoke("ChangePassword", new object[] { password, newPassword });
				usr.CommitChanges();
				bOK = true;
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if (usr != null)
					usr.Close();
				if (userHandle != IntPtr.Zero)
					CloseHandle(userHandle);
				if (impersonationContext != null)
					impersonationContext.Undo();
			}
			return bOK;
		}

		/// <summary>
		/// Change existing user's password
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <param name="existingPassword"></param>
		/// <param name="newPassword"></param>
		/// <returns></returns>
		public bool ChangePassword(string domain, string userName, string existingPassword, string newPassword)
		{
			DirectoryEntry usr = null;
			bool bOK = false;
			try
			{
				usr = GetDirectoryEntry(domain, userName, existingPassword);
				usr.Invoke("ChangePassword", new object[] { existingPassword, newPassword });
				usr.CommitChanges();
				bOK = true;
			}
			catch(Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if (usr != null)
					usr.Close();
			}
			return bOK;
		}
		/// <summary>
		/// Method that creates a new user account
		/// </summary>
		/// <param name="employeeID"></param>
		/// <param name="name"></param>
		/// <param name="login"></param>
		/// <param name="email"></param>
		/// <param name="group"></param>
		public bool CreateNewUser(string domain, string adminAccount, string addminPassword, 
			string employeeID, string name, string login, string password, string email, string group)
		{
			bool bReturn = false;
			IntPtr userHandle = IntPtr.Zero;
			WindowsImpersonationContext impersonationContext = null;
			DirectoryEntry de = null;
			DirectoryEntry newuser = null;
			try
			{
				///
				/// Impersonate
				///
				bool loggedOn = LogonUser(adminAccount, domain, addminPassword,
					LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out userHandle);

				if (!loggedOn)
					throw new Win32Exception(Marshal.GetLastWin32Error());

				impersonationContext = WindowsIdentity.Impersonate(userHandle);

				/// 0. Getparent
				de = GetDirectoryEntry();

				/// 1. Create user account
				DirectoryEntries users = de.Children;
				newuser = users.Add("CN=" + login, "user");

				/// 2. Set properties
				SetProperty(newuser, "employeeID", employeeID);
				SetProperty(newuser, "givenname", name);
				SetProperty(newuser, "SAMAccountName", login);
				SetProperty(newuser, "userPrincipalName", login);
				//SetProperty(newuser, "mail", email);

				/// 4. Commit
				newuser.CommitChanges();

				/// 4. Enable account            
				EnableAccount(newuser);

				/// 5. Set password
				newuser.Invoke("SetPassword", new object[] { password });

				/// 6. Add user account to groups
				//AddUserToGroup(de, newuser, group);

				/// 7. Commit changes
                newuser.CommitChanges();

				bReturn = true;
			}
			catch (Exception ex)
			{
                if (newuser != null)
                {
                    /// 
                    /// roll back
                    /// 
                    newuser.Parent.Children.Remove(newuser);

                }
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if (newuser != null)
					newuser.Close();
				if (de != null)
					de.Close();
				if (userHandle != IntPtr.Zero)
					CloseHandle(userHandle);
				if (impersonationContext != null)
					impersonationContext.Undo();
			}

			return bReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="adminAccount"></param>
		/// <param name="addminPassword"></param>
		/// <param name="login"></param>
		/// <returns></returns>
		public bool CreateUserMailBox(string domain, string adminAccount, string addminPassword, string login) 
		{
			return true;
			///// 7. Create a mailbox in Microsoft Exchange    
			//CDOEXM.IMailboxStore mailbox;
			//mailbox = (IMailboxStore)newuser.NativeObject;

			//string homeMDB = string.Empty;// "LDAP://MAILDC/CN=사서함 저장소(MAILDC),CN=기본 저장소 그룹,CN=InformationStore,CN=MAILDC,CN=Servers,CN=기본 관리 그룹,CN=CMAXO,CN=Microsoft Exchange,CN=Services,CN=Configuration,DC=cmax,DC=com";

			//homeMDB = "CN=기본 저장소(maildc),CN=기본 저장소 그룹," + "CN=InformationStore,CN=maildc,CN=Servers," +
			//    "CN=Administrators,CN=Administrative Groups," +
			//    "CN=CMAXO,CN=Microsoft Exchange,CN=Services," +
			//    "CN=Configuration,DC=cmax,DC=com";

			//mailbox.CreateMailbox(homeMDB); 		

		}

		/// <summary>
		/// Method that disables a user account in the AD and hides user's email from Exchange address lists.
		/// </summary>
		/// <param name="EmployeeID"></param>
		public void DisableAccount(string userName)
		{
			DirectoryEntry dey = GetDirectoryEntry(userName);

			int val = (int)dey.Properties["userAccountControl"].Value;
			dey.Properties["userAccountControl"].Value = val | 0x0002;
			dey.Properties["msExchHideFromAddressLists"].Value = "TRUE";
			dey.CommitChanges();
			dey.Close();
		}

		/// <summary>
		/// Check if this user is expired
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public bool IsUserExpired(string userName)
		{
			bool bIsExpired = false;
			DirectoryEntry usr = null;
			try
			{
				usr = GetDirectoryEntry(userName);
				object obj = GetProperty(usr, "AccountExpirationDate");
				bIsExpired = obj == null ? false : ((DateTime)obj) <= DateTime.Today;
			}
			catch (Exception ex)
			{
				//CMAXException.HandleFWKException(SubSystemType.FRAMEWORK, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
			}
			finally
			{
				if(usr != null)
					usr.Close();
			}
			return bIsExpired;
		}

		/// <summary>
		/// Method to add a user to a group
		/// </summary>
		/// <param name="de"></param>
		/// <param name="deUser"></param>
		/// <param name="GroupName"></param>
		public void AddUserToGroup(DirectoryEntry de, DirectoryEntry deUser, string GroupName)
		{
			DirectorySearcher deSearch = new DirectorySearcher();
			deSearch.SearchRoot = de;
			deSearch.Filter = "(&(objectClass=group) (cn=" + GroupName + "))";
			SearchResultCollection results = deSearch.FindAll();

			bool isGroupMember = false;

			if (results.Count > 0)
			{
				DirectoryEntry group = GetDirectoryEntry(results[0].Path);

				object members = group.Invoke("Members", null);
				foreach (object member in (IEnumerable)members)
				{
					DirectoryEntry x = new DirectoryEntry(member);
					if (x.Name != deUser.Name)
					{
						isGroupMember = false;
					}
					else
					{
						isGroupMember = true;
						break;
					}
				}

				if (!isGroupMember)
				{
					group.Invoke("Add", new object[] { deUser.Path.ToString() });
				}

				group.Close();
			}
			return;
		}

		/// <summary>
		/// Method that calls and starts a WSHControl.vbs
		/// </summary>
		/// <param name="userAlias"></param>
		public string GenerateMailBox(string adminAccount, string adminPassword, string userAlias)
		{
			StringBuilder mailargs = new StringBuilder();
			mailargs.Append(@"C:\TestRemoteMailbox\WSHControl.vbs");
			mailargs.Append(" ");
			mailargs.Append(userAlias);

			//ProcessStartInfo pinfo = new ProcessStartInfo("Wscript.exe", mailargs.ToString());

			ProcessStartInfo pinfo = new ProcessStartInfo("cmd");
			pinfo.UserName = adminAccount;

			SecureString ss = new SecureString();
			for (int i = 0; i < adminPassword.Length; i++)
			{
				ss.AppendChar(adminPassword[i]);
			}

			pinfo.Password = ss;
			string strOutput = string.Empty;
			StreamReader sr;
			StreamReader err;
			StreamWriter sw;

			Process cmdProcess = new Process();

			cmdProcess.StartInfo = pinfo;

			pinfo.WindowStyle = ProcessWindowStyle.Hidden;
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
			sw.WriteLine(mailargs.ToString());
			sw.Close();
			strOutput = sr.ReadToEnd();
			strOutput += err.ReadToEnd();
			cmdProcess.Close();

			return strOutput;
		}

		/// <summary>
		/// Method to extract the alias from an email account.
		/// </summary>
		/// <param name="mailAddress"></param>
		/// <returns></returns>
		public string GetAlias(string mailAddress)
		{
			if (IsEmail(mailAddress))
			{
				return mailAddress.Substring(0, mailAddress.IndexOf("@"));
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Method that formats a date in the required format
		/// needed (AAAAMMDDMMSSSS.0Z) to compare dates in AD.
		/// </summary>
		/// <param name="date"></param>
		/// <returns>Date in valid format for AD</returns>
		public string ToADDateString(DateTime date)
		{
			string year = date.Year.ToString();
			int month = date.Month;
			int day = date.Day;

			StringBuilder sb = new StringBuilder();
			sb.Append(year);
			if (month < 10)
			{
				sb.Append("0");
			}
			sb.Append(month.ToString());
			if (day < 10)
			{
				sb.Append("0");
			}
			sb.Append(day.ToString());
			sb.Append("000000.0Z");
			return sb.ToString();
		} 


		/// <summary>
		/// Helper method that sets properties for AD users.
		/// </summary>
		/// <param name="de"></param>
		/// <param name="PropertyName"></param>
		/// <param name="PropertyValue"></param>
		public void SetProperty(DirectoryEntry de, string PropertyName, string PropertyValue)
		{
			if (PropertyValue != null)
			{
				if (de.Properties.Contains(PropertyName))
				{
					de.Properties[PropertyName][0] = PropertyValue;
				}
				else
				{
					de.Properties[PropertyName].Add(PropertyValue);
				}
			}
		}

		/// <summary>
		/// Helper method that sets properties for AD users.
		/// </summary>
		/// <param name="de"></param>
		/// <param name="PropertyName"></param>
		/// <param name="PropertyValue"></param>
		public object GetProperty(DirectoryEntry de, string PropertyName)
		{
			if (de.Properties.Contains(PropertyName))
			{
				return de.Properties[PropertyName][0];
			}
			return null;
		}

		/// <summary>
		/// Method that validates if a string has an email pattern.
		/// </summary>
		/// <param name="mail"></param>
		/// <returns></returns>
		public bool IsEmail(string mail)
		{
			Regex mailPattern = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
			return mailPattern.IsMatch(mail);
		}

		#region AD Impersonation

		const int LOGON32_LOGON_INTERACTIVE = 2;
		const int LOGON32_LOGON_NETWORK = 3;
		const int LOGON32_LOGON_BATCH = 4;
		const int LOGON32_LOGON_SERVICE = 5;
		const int LOGON32_LOGON_UNLOCK = 7;
		const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
		const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

		const int LOGON32_PROVIDER_DEFAULT = 0;

		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool LogonUser
		(
			string lpszUsername,
			string lpszDomain,
			string lpszPassword,
			int dwLogonType,
			int dwLogonProvider,
			out IntPtr phToken
		);

		[DllImport("Kernel32")]
		private extern static Boolean CloseHandle(IntPtr handle);

		#endregion

	}
}