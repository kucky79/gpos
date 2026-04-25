using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;
using System.ComponentModel;

using System.Security.Principal;
using System.Security;
using CDOEXM;
using ActiveDs;

//추가 using
using System.DirectoryServices;
using System.EnterpriseServices;

[assembly: ApplicationName("MailBox.Administration")]
[assembly: ApplicationActivation(ActivationOption.Library)]

namespace MailBoxAdmin
{
	/// <summary>
	/// Epmployee Administration Object
	/// </summary>
	/// <summary>
    /// <b>■ ActiveDirctory </b><br/>
    /// - 작  성  자 : 인터데브 김성득<br/>
    /// - 최초 작성일 : 2005년 11월 29일<br/>
    /// - 최종 수정자 : Korea Hinet Truong Cong Loc<br/>
    /// - 최종 수정일 : 2005년 12월 23일<br/>
    /// - 주요 변경 로그<br/>
    ///	1.
    ///	2.
    ///	3.
    /// </summary>
    
	[Transaction(TransactionOption.RequiresNew)]
	[ObjectPooling(true, 5, 10)]
	[ComVisible(true)]
	public class ActiveDirectory : ServicedComponent, IDisposable
	{
		private const int ADS_UF_ACCOUNTDISABLE = 2;
		private const string DOMAIN_NAME = "CMAX.COM";
		private const string NETBIOS_DOMAIN_NAME = "CMAX";

		private const string USERS_LDAP_PATH = "OU=Users,OU=CMAX,DC=CMAX,DC=COM";
		private const string ADMIN_GROUP_LDAP_PATH = "Administrators";
		private const string DOMAIN_LDAP_PATH = "DC=CMAX,DC=COM";

		private const string ADMIN_ACCOUNT = "Administrator";
		private const string ADMIN_ACCOUNT_PASSWORD = "YOUR_ADMIN_PASSWORD";

		private IntPtr userHandle = IntPtr.Zero;
		private WindowsImpersonationContext impersonationContext = null;

		public string MailBoxHomeDBUrl = "MAILDC/CN=사서함 저장소(MAILDC),CN=기본 저장소 그룹,CN=InformationStore,CN=MAILDC,CN=Servers,CN=기본 관리 그룹,CN=Administrative Groups,CN=CMAXO,CN=Microsoft Exchange,CN=Services,CN=Configuration,DC=cmax,DC=com";

		public ActiveDirectory()
		{
			///
			/// Impersonate
			///
			bool loggedOn = LogonUser(ADMIN_ACCOUNT, NETBIOS_DOMAIN_NAME, ADMIN_ACCOUNT_PASSWORD,
				LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out userHandle);

			if (!loggedOn)
				throw new Win32Exception(Marshal.GetLastWin32Error());

			impersonationContext = WindowsIdentity.Impersonate(userHandle);
		}

		/// <summary>
		/// Add new user
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool Add(string cn, string employeeID, string fullName, string password, bool bIsAdministrator)
		{
			DirectoryEntry ent = null;
			DirectoryEntry usr = null;
			bool bSuccess = false;
			try
			{

				ent = new DirectoryEntry(string.Concat("LDAP://", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				usr = ent.Children.Add(string.Concat("CN=", cn), "user");
				usr.Properties["userPrincipalName"].Value = string.Concat(cn, "@", DOMAIN_NAME.ToLower());
				usr.Properties["sAMAccountName"].Value = cn;
				usr.Properties["employeeID"].Value = employeeID;
				usr.Properties["displayName"].Value = fullName;
				usr.Properties["description"].Value = fullName;
				usr.Properties["mail"].Value = string.Concat(cn, "@", DOMAIN_NAME.ToLower());
				usr.CommitChanges();

				int iVal = (int)usr.Properties["userAccountControl"].Value;
				usr.Properties["userAccountControl"].Value = iVal & ~ADS_UF_ACCOUNTDISABLE;
				usr.Invoke("SetPassword", password);
				usr.CommitChanges();

				if (bIsAdministrator)
				{
					AddToAdminGroup(usr);
				}

				bSuccess = true;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
				if (ent != null)
				{
					ent.Close();
				}
			}

			return bSuccess;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool CreateUserMailBox(string cn)
		{
			DirectoryEntry usr = null;
			bool bSuccess = false;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				bSuccess = CreateUserMailBox(usr);
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
			}

			return bSuccess;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool CreateUserMailBox(DirectoryEntry usr)
		{
			CDOEXM.IMailboxStore mailbox = null;
			bool bSuccess = false;
			try
			{				
				mailbox = (IMailboxStore)usr.NativeObject;
				mailbox.CreateMailbox(MailBoxHomeDBUrl);

				/// 
				/// SetFullMailPermission
				/// 
				SetFullMailPermission(usr);

				bSuccess = true;
			}
			catch (Exception ex)
			{				

			}
			finally
			{
				if (mailbox != null)
				{
					mailbox = null;
				}
			}

			return bSuccess;
		}

		/// <summary>
		/// Delete an user
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool Remove(string cn)
		{
			DirectoryEntry ent = null;
			DirectoryEntry usr = null;
			bool bSuccess = false;
			try
			{
				ent = new DirectoryEntry(string.Concat("LDAP://", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				ent.Children.Remove(usr);
				bSuccess = true;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
				if (ent != null)
				{
					ent.Close();
				}
			}
			return bSuccess;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="groupPath">ie: OU=Administrators,OU=Builtin | OU=SMT,OU=CMAX</param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool AddUserToGroup(string cn, string groupPath)
		{
			DirectoryEntry usr = null;
			bool bSuccess = false;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				bSuccess = AddUserToGroup(usr, groupPath);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (usr != null)
					usr.Close();
			}
			return bSuccess;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="usr"></param>
		/// <param name="groupPath">ie: Administrators</param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool AddUserToGroup(DirectoryEntry usr, string groupName)
		{
			bool bSuccess = false;
			DirectorySearcher deSearch = null;
			DirectoryEntry group = null;
			try
			{
				deSearch = new DirectorySearcher();
				deSearch.SearchRoot = new DirectoryEntry(string.Concat("LDAP://", DOMAIN_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD); ;
				deSearch.Filter = "(&(objectClass=group) (cn=" + groupName + "))";
				SearchResultCollection results = deSearch.FindAll();

				bool isGroupMember = false;

				if (results.Count > 0)
				{
					group = new DirectoryEntry(results[0].Path, ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
					object members = group.Invoke("Members", null);

					foreach (object member in (IEnumerable)members)
					{
						DirectoryEntry x = new DirectoryEntry(member);
						if (x.Name != usr.Name)
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
						group.Invoke("Add", new object[] { usr.Path.ToString() });
						bSuccess = true;
					}
				}


			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (group != null)
				{
					group.Close();
					group.Dispose();
				}
				if (deSearch != null)
				{
					deSearch.Dispose();
				}
			}
			return bSuccess;
		}

		/// <summary>
		/// Add user to administrator group
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool AddToAdminGroup(DirectoryEntry usr)
		{
			return AddUserToGroup(usr, ADMIN_GROUP_LDAP_PATH);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		public bool Enable(string cn)
		{
			bool bSuccess = false;
			DirectoryEntry usr = null;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);

				int iVal = (int)usr.Properties["userAccountControl"].Value;
				usr.Properties["userAccountControl"].Value = iVal & ~ADS_UF_ACCOUNTDISABLE;
				usr.CommitChanges();

				bSuccess = true;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
			}
			return bSuccess;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		public bool Disable(string cn)
		{
			bool bSuccess = false;
			DirectoryEntry usr = null;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);

				int iVal = (int)usr.Properties["userAccountControl"].Value;
				usr.Properties["userAccountControl"].Value = iVal | ADS_UF_ACCOUNTDISABLE;
				usr.CommitChanges();
				bSuccess = true;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
			}
			return bSuccess;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="password"></param>
		[AutoComplete(true)]
		public bool SetPassword(string cn, string password)
		{
			bool bSuccess = false;
			DirectoryEntry usr = null;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				usr.Invoke("SetPassword", password);
				bSuccess = true;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
			}
			return bSuccess;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		[AutoComplete(true)]
		public bool Exists(string cn)
		{
			bool bSuccess = false;
			DirectoryEntry usr = null;
			DirectorySearcher search = null;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://CN=", cn, ",", USERS_LDAP_PATH), ADMIN_ACCOUNT, ADMIN_ACCOUNT_PASSWORD);
				search = new DirectorySearcher(usr, "(SAMAccountName=" + cn + ")");
				SearchResult result = search.FindOne();
				bSuccess = result != null;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
			}
			return bSuccess;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[AutoComplete(true)]
		public bool IsAuthenticated(string cn, string password)
		{
			bool bSuccess = false;
			DirectoryEntry usr = null;
			try
			{
				usr = new DirectoryEntry(string.Concat("LDAP://", USERS_LDAP_PATH), cn, password);
				bSuccess = usr.Name != null;
			}
			catch
			{
			}
			finally
			{
				if (usr != null)
				{
					usr.Close();
				}
			}
			return bSuccess;
		}

		/// <summary>
		/// 
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

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			if (userHandle != IntPtr.Zero)
				CloseHandle(userHandle);
			if (impersonationContext != null)
				impersonationContext.Undo();

		}

		#endregion

		#region Exchange  사서함 권한

		private const int  ADS_ACEFLAG_INHERIT_ACE = 2;
		private const int  ADS_RIGHT_DS_CREATE_CHILD = 1;
		private const int  RIGHT_DS_MAILBOX_OWNER = 1;
		private const int  RIGHT_DS_PRIMARY_OWNER = 1;
		private const int  ADS_ACETYPE_ACCESS_ALLOWED = 0;
		private const int  ADS_ACETYPE_ACCESS_DENIED = 1;
		private const int  ADS_ACETYPE_SYSTEM_AUDIT = 2;
		private const int  ADS_ACETYPE_ACCESS_ALLOWED_OBJECT = 5;
		private const int  ADS_ACETYPE_ACCESS_DENIED_OBJECT = 6;
		private const int  ADS_ACETYPE_SYSTEM_AUDIT_OBJECT = 7;
		private const int ADS_ACETYPE_SYSTEM_ALARM_OBJECT = 8;


		private void SetFullMailPermission(DirectoryEntry usr)
		{
			ActiveDs.IADsUser objUser;
			CDOEXM.IExchangeMailbox objUserMailbox;
			CDOEXM.Mailbox objUserMail;

			ActiveDs.IADsUser objMgr;
			ActiveDs.SecurityDescriptor oSecurityDescriptor;
			ActiveDs.AccessControlList oDacl;
			ActiveDs.AccessControlEntry oAce;
			string sUserADsPath; string sTrustee;

			// 'Get directory user object.
			objUser = (ActiveDs.IADsUser)usr.NativeObject;
			objUserMailbox = (CDOEXM.IExchangeMailbox)usr.NativeObject;

			// ' Get the Mailbox security descriptor (SD).
			// 'oSecurityDescriptor = objUser.Get("msExchMailboxSecurityDescriptor")
			oSecurityDescriptor = (ActiveDs.SecurityDescriptor)objUserMailbox.MailboxRights;

			// ' Extract the discretionary access control list (ACL) by using the IADsSecurityDescriptor.
			// ' Interface
			oDacl = (ActiveDs.AccessControlList)oSecurityDescriptor.DiscretionaryAcl;

			/*
				''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				' The following block of code demonstrates how to add a new ACE to the
				' DACL for the Exchange 2000 mailbox with the Trustee specified in
				' sTrustee, which permits "Full Control" over this mailbox.
				' This is the same task that is performed by ADUnC when you follow these
				' steps to modify the properties of a user: on the Exchange Advanced tab,
				' under Mailbox Rights, click Add, select the Trustee, and then select the
				' Full Mailbox Access Rights check box.
				' Similarly, you can also remove ACEs from this ACL by using the IADsAccessControlEntry interfaces.
				''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
			*/
			//AddAce(dacl, sTrustee, 131075, ADS_ACETYPE_ACCESS_ALLOWED, ADS_ACEFLAG_INHERIT_ACE, 0, 0, 0);
			AddAce(oDacl, @"NT AUTHORITY\SELF", 131075, ADS_ACETYPE_ACCESS_ALLOWED, ADS_ACEFLAG_INHERIT_ACE, 0, "0", "0");
			//AddAce(oDacl, @"NT AUTHORITY\SELF", ADS_RIGHT_DS_CREATE_CHILD, 0, 0, RIGHT_DS_PRIMARY_OWNER, "0", "0"); // 'This doesnot work
			//AddAce(oDacl, @"NT AUTHORITY\SELF", ADS_RIGHT_DS_CREATE_CHILD, 0, 0, RIGHT_DS_PRIMARY_OWNER, "0", "0"); // 'This doesnot work

			// ' Add the modified DACL to the security descriptor.
			oSecurityDescriptor.DiscretionaryAcl = oDacl;

			// ' Save new SD onto the user.
			objUserMailbox.MailboxRights = oSecurityDescriptor;

			// ' Commit changes from the property cache to the information store.
			// 'objUser.Put("msExchMailboxSecurityDescriptor", oSecurityDescriptor)
			objUser.SetInfo();
		}

		private void AddAce(ActiveDs.AccessControlList dacl, string trusteeName, 
			int gAccessMask, int gAceType, int gAceFlags, int gFlags, string gObjectType, 
			string gInheritedObjectType)
		{
			ActiveDs.IADsAccessControlEntry ace1;
			ace1 = new AccessControlEntryClass();
			ace1.AccessMask = gAccessMask;
			ace1.AceType = gAceType;
			ace1.AceFlags = gAceFlags;
			ace1.Flags = gFlags;
			ace1.Trustee = trusteeName;

			// 'See whether ObjectType must be set.
			if (!string.IsNullOrEmpty(gObjectType))
			{
				ace1.ObjectType = gObjectType;
			}
				
			// 'See whether InheritedObjectType must be set.
			if (!string.IsNullOrEmpty(gInheritedObjectType))
			{
				ace1.InheritedObjectType = gInheritedObjectType;
			}
			
			dacl.AddAce(ace1);
			ace1 = null;
		}

		#endregion
	}
}
