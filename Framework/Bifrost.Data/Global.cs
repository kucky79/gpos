using NF.A2P.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Web.Services.Protocols;

namespace NF.A2P.Data
{
    public class Global : IDisposable
    {
        #region Global variable 
        private static string _Language = "KO";	            // 접속언어
        private static string _FirmCode;                    // 회사코드
        private static string _FirmName;                    // 회사명
        private static string _BizCode;                     // 접속본부코드
        private static string _BizName;                     // 접속본부명
        private static string _ExchangeCode;	            // 적용환율
        private static string _UserHostIP;                  // 접속 아이피
        private static string _UserHostMAC;                 // 접속 아이피
        private static string _SocketIP;                    // 소켓 IP
        private static string _SocketPort;                  // 소켓 Port
        private static string _GroupCode;                   // 그룹코드
        private static string _GroupName;                   // 그룹명
        private static string _PhotoName;                   // 사원사진경로
        private static int _AwayTime;                       // 자리비움시간
        private static string _MessagerDescript;            // 메신저상태
        #endregion Global variable 

        #region Login Info variable
        private static string _DeptCode;	                // 부서코드
        private static string _DeptName;	                // 부서명
        private static string _EmpCode;	                    // 사원코드
        private static string _EmpName;	                    // 사원명
        private static string _UserID;		                // 접속ID
        private static string _UserName;	                // 접속 사용자명
        private static string _PassWord;		            // 접속 암호
        private static string _Email = string.Empty;        // Email

        private static string _RoleType;	                // 직책코드 
        private static string _RoleName;	                // 직책명
        private static string _PositionType;	            // 직위코드
        private static string _PositionName;	            // 직위명
        private static string _GradeCode;                   // 직급코드
        #endregion Login Info variable

        public Global()
        {
            _User = new UserData(string.Empty, string.Empty, string.Empty, string.Empty);
        }
        public Global(DataRow gbDataRow)
        {
            Language = Convert.ToString(gbDataRow["CD_LANG"]).Trim();
            FirmCode = Convert.ToString(gbDataRow["CD_FIRM"]).Trim();
            FirmName = Convert.ToString(gbDataRow["NM_FIRM"]).Trim();
            BizCode = Convert.ToString(gbDataRow["CD_BIZ"]).Trim();
            BizName = Convert.ToString(gbDataRow["NM_BIZ"]).Trim();
            ExchangeCode = Convert.ToString(gbDataRow["CD_EXCHANGE"]).Trim();
            UserHostIP = Convert.ToString(gbDataRow["DC_IP"]).Trim();
            UserHostMAC = Convert.ToString(gbDataRow["DC_MAC"]).Trim();
            string[] socketInfo = new string[2];

            if (Convert.ToString(gbDataRow["DC_SOCKET"]).Trim() != string.Empty)
            {
                if (Convert.ToString(gbDataRow["DC_SOCKET"]).Trim().Split(',').Length == 2)
                {
                    socketInfo[0] = Convert.ToString(gbDataRow["DC_SOCKET"]).Trim().Split(',')[0];
                    socketInfo[1] = Convert.ToString(gbDataRow["DC_SOCKET"]).Trim().Split(',')[1];
                }

            }

            SocketIP = socketInfo[0];
            SocketPort = socketInfo[1];

            //20170418 메신저 관련 항목 추가
            GroupCode = Convert.ToString(gbDataRow["CD_GP"]).Trim();
            GroupName = Convert.ToString(gbDataRow["NM_GP"]).Trim();
            PhotoName = Convert.ToString(gbDataRow["NM_PHOTO"]).Trim();
            AwayTime = Convert.ToInt16(gbDataRow["TM_AWAY"]);
            MessagerDescript = Convert.ToString(gbDataRow["DC_MESSAGE"]).Trim();

            _User = new UserData(string.Empty, Convert.ToString(gbDataRow["CD_USER"]).Trim(), Convert.ToString(gbDataRow["NM_USER"]).Trim(), Convert.ToString(gbDataRow["NO_PWD"]).Trim());

        }
        private UserData _User;

        public static UserData FromDataRow(DataRow userDataRow)
        {
            UserData ud = new UserData();

            ud.DeptCode = Convert.ToString(userDataRow["CD_DEPT"]).Trim();
            ud.DeptName = Convert.ToString(userDataRow["NM_DEPT"]).Trim();
            ud.EmpCode = Convert.ToString(userDataRow["CD_EMP"]).Trim();
            ud.EmpName = Convert.ToString(userDataRow["NM_EMP"]).Trim();
            ud.UserID = Convert.ToString(userDataRow["CD_USER"]).Trim();
            ud.UserName = Convert.ToString(userDataRow["NM_USER"]).Trim();
            ud.PassWord = Convert.ToString(userDataRow["NO_PWD"]).Trim();
            ud.Email = Convert.ToString(userDataRow["DC_EMAIL"]).Trim();
            ud.RoleType = Convert.ToString(userDataRow["FG_ROLE"]).Trim();
            ud.RoleName = Convert.ToString(userDataRow["NM_FG_ROLE"]).Trim();
            ud.PositionType = Convert.ToString(userDataRow["FG_POS"]).Trim();
            ud.PositionName = Convert.ToString(userDataRow["NM_FG_POS"]).Trim();
            ud.GradeCode = Convert.ToString(userDataRow["CD_GRA"]).Trim();

            return ud;
        }


        #region GlobalData Properties
        /// <summary>
        /// 사용언어  
        /// </summary>
        /// <remarks>사용언어</remarks>
        public static string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }
        /// <summary>
        /// 회사코드
        /// </summary>        
        public static string FirmCode
        {
            get { return _FirmCode; }
            set { _FirmCode = value; }
        }
        /// <summary>
        /// 회사명
        /// </summary>
        public static string FirmName
        {
            get { return _FirmName; }
            set { _FirmName = value; }
        }

        /// <summary>
        /// 접속본부코드
        /// </summary>
        public static string BizCode
        {
            get { return _BizCode; }
            set { _BizCode = value; }
        }
        /// <summary>
        /// 접속본부명
        /// </summary>
        public static string BizName
        {
            get { return _BizName; }
            set { _BizName = value; }
        }
        /// <summary>
        /// 접속 아이피
        /// </summary>
        public static string UserHostIP
        {
            get { return _UserHostIP; }
            set { _UserHostIP = value; }
        }
        /// <summary>
        /// 접속 MAC
        /// </summary>
        public static string UserHostMAC
        {
            get { return _UserHostMAC; }
            set { _UserHostMAC = value; }
        }
        /// <summary>
        /// 적용환율
        /// </summary>
        public static string ExchangeCode
        {
            get { return _ExchangeCode; }
            set { _ExchangeCode = value; }
        }
        #endregion

        #region Login Info Properties
        /// <summary>
        /// 부서코드
        /// </summary>
        public static string DeptCode
        {
            get { return _DeptCode; }
            set { _DeptCode = value; }
        }
        /// <summary>
        /// 부서명
        /// </summary>
        public static string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }
        /// <summary>
        /// 사원코드
        /// </summary>
        public static string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }
        /// <summary>
        /// 사원명
        /// </summary>
        public static string EmpName
        {
            get { return _EmpName; }
            set { _EmpName = value; }
        }
        /// <summary>
        /// 접속ID
        /// </summary>
        public static string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        /// <summary>
        /// 접속 사용자명
        /// </summary>
        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        /// <summary>
        /// 접속 암호
        /// </summary>
        public static string PassWord
        {
            get { return _PassWord; }
            set { _PassWord = value; }
        }
        /// <summary>
        /// 사용Email
        /// </summary>
        public static string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        /// <summary>
        /// 소켓IP
        /// </summary>
        public static string SocketIP
        {
            get { return _SocketIP; }
            set { _SocketIP = value; }
        }

        /// <summary>
        /// 소켓Port
        /// </summary>
        public static string SocketPort
        {
            get { return _SocketPort; }
            set { _SocketPort = value; }
        }

        /// <summary>
        /// 직책코드
        /// </summary>
        public static string RoleType
        {
            get { return _RoleType; }
            set { _RoleType = value; }
        }

        /// <summary>
        /// 직책명
        /// </summary>
        public static string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }


        /// <summary>
        /// 직위코드
        /// </summary>
        public static string PositionType
        {
            get { return _PositionType; }
            set { _PositionType = value; }
        }

        /// <summary>
        /// 직위명
        /// </summary>
        public static string PositionName
        {
            get { return _PositionName; }
            set { _PositionName = value; }
        }

        /// <summary>
        /// 직급코드
        /// </summary>
        public static string GradeCode
        {
            get { return _GradeCode; }
            set { _GradeCode = value; }
        }

        /// <summary>
        /// 그룹코드
        /// </summary>
        public static string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }

        /// <summary>
        /// 그룹명
        /// </summary>
        public static string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        /// <summary>
        /// 사원사진
        /// </summary>
        public static string PhotoName
        {
            get { return _PhotoName; }
            set { _PhotoName = value; }
        }

        /// <summary>
        /// 자리비움시간
        /// </summary>
        public static int AwayTime
        {
            get { return _AwayTime; }
            set { _AwayTime = value; }
        }

        /// <summary>
        /// 메신저 상태
        /// </summary>
        public static string MessagerDescript
        {
            get { return _MessagerDescript; }
            set { _MessagerDescript = value; }
        }
        #endregion

        private DataTable _Menus;
        public DataTable Menus
        {
            get { return _Menus; }
            set { _Menus = value; }
        }

        private DataTable _MyMenus;
        public DataTable MyMenus
        {
            get { return _MyMenus; }
            set { _MyMenus = value; }
        }

        private MenuData _StartupMenu;
        public MenuData StartupMenu
        {
            get { return _StartupMenu; }
            set { _StartupMenu = value; }
        }

        private CookieContainer _CookieContainer;
        public CookieContainer CookieContainer
        {
            get { return _CookieContainer; }
            set { _CookieContainer = value; }
        }

        private AuthHeader _AuthHeader;
        public AuthHeader AuthHeader
        {
            get { return _AuthHeader; }
            set { _AuthHeader = value; }
        }

        private Hashtable _SharedData = new Hashtable();
        [Description("Shared Data")]
        public Hashtable SharedData
        {
            get { return _SharedData; }
            set { _SharedData = value; }
        }

        

        #region IDisposable Members

        public void Dispose() { }

        #endregion
    }

}
