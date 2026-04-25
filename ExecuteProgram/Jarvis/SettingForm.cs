using Bifrost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jarvis
{
    public partial class SettingForm : Form
    {
        #region variable set
        string FileServerIP = string.Empty;
        string FileServerID = string.Empty;
        string FileServerPW = string.Empty;
        string FileServerPort = string.Empty;
        string UpgradeServerIP = string.Empty;
        string ConnectionString = string.Empty;
        string ConnectionCatalog = string.Empty;
        string ReportPath = string.Empty;
        string ReportServer = string.Empty;
        string DownloadPath = string.Empty;
        string StoreCode = string.Empty;
        string StoreName = string.Empty;
        string UserID = string.Empty;
        string UserName = string.Empty;
        string Language = string.Empty;
        string DBID = string.Empty;
        string DBPassword = string.Empty;

        #endregion

        public SettingForm()
        {
            InitializeComponent();
            InitSetting();
            InitEvent();
            //SetDBConnection();

            char[] delimiterChars = { ';', '=' };
            string[] words = ConnectionString.Split(delimiterChars);

            txtDBServer.Text = words[1].ToString();
            txtStoreCode.Text = StoreCode;
            txtStoreName.Text = StoreName;
            txtUserID01.Text = UserID;
            txtUserId.Text = UserID;
            txtUserName.Text = UserName;
            txtUpgradeServer.Text = UpgradeServerIP;
            ddlLang.SelectedValue = Language;

        }

        private void InitEvent()
        {
            //lblTitle.MouseDown += Form1_MouseDown;
            //lblTitle.MouseMove += Form1_MouseMove;

            btnOK.Click += btnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            txtStoreCode.KeyDown += txtStoreCode_KeyDown;
            txtUserId.KeyDown += TxtUserId_KeyDown;
        }

        private void TxtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtStoreCode.Text == string.Empty)
                {
                    MessageBox.Show("회사코드를 먼저 확인해주세요.");
                    txtUserId.Text = string.Empty;
                    txtStoreCode.Focus();
                    return;
                }
                object UserName = DBHelper.ExecuteScalar("SELECT NM_EMP FROM POS_EMP WHERE CD_STORE = '" + txtStoreCode.Text + "' AND CD_USER = '" + txtUserId.Text + "'");

                if (UserName != null)
                {
                    txtUserName.Text = UserName as string;
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitSetting()
        {
            //ControlDataBinding(this.ddlLang, DBHelper.GetDataTable("SELECT CD_FLAG, NM_FLAG FROM MAS_CODEL WHERE CD_Store = '0000' AND CD_CLAS = 'SYS004' ORDER BY DC_REF1"), "NM_FLAG", "CD_FLAG");

            ComboBoxDataBinding(this.ddlLang, new string[] { "KO", "EN", "JP", "CN" }, new string[] { "KOREAN", "ENGLISH", "JAPANESE", "CHINESE" });


            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            Jarvis.Util.IniFile inifile = new Jarvis.Util.IniFile();

            //upgrade server
            UpgradeServerIP = inifile.IniReadValue("UpgradeServer", "IP", Path);
            if (UpgradeServerIP == string.Empty)
            {
                //default 
                inifile.IniWriteValue("UpgradeServer", "IP", "localhost", Path);
                UpgradeServerIP = inifile.IniReadValue("UpgradeServer", "IP", Path);
            }

            
            //Connection
            ConnectionString = inifile.IniReadValue("DB", "ConnectionString", Path);

            if (ConnectionString == string.Empty)
            {
                //default 
                inifile.IniWriteValue("DB", "ConnectionString", "Data Source=localhost; initial Catalog=GPOS;uid=YOUR_USER;password=YOUR_PASSWORD;", Path);
                ConnectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
            }

            char[] delimiterChars = { ';', '=' };
            string[] words = ConnectionString.Split(delimiterChars);
            ConnectionCatalog = words[1];

            DBID = words[5];
            DBPassword = words[7];

            //StoreCode
            StoreCode = inifile.IniReadValue("StoreInfo", "StoreCode", Path);

            if (StoreCode == string.Empty)
            {
                //default 
                inifile.IniWriteValue("StoreInfo", "StoreCode", "9000", Path);
                StoreCode = inifile.IniReadValue("StoreInfo", "StoreCode", Path);
            }

            //StoreName
            StoreName = inifile.IniReadValue("StoreInfo", "StoreName", Path);

            if (StoreName == string.Empty)
            {
                //default 
                inifile.IniWriteValue("StoreInfo", "StoreName", "GPOS", Path);
                StoreName = inifile.IniReadValue("StoreInfo", "StoreName", Path);
            }

            //UserID
            UserID = inifile.IniReadValue("StoreInfo", "UserID", Path);

            if (UserID == string.Empty)
            {
                //default 
                inifile.IniWriteValue("StoreInfo", "UserID", string.Empty, Path);
                UserID = inifile.IniReadValue("StoreInfo", "UserID", Path);
            }

            //UserName
            UserName = inifile.IniReadValue("StoreInfo", "UserName", Path);

            if (UserName == string.Empty)
            {
                //default 
                inifile.IniWriteValue("StoreInfo", "UserName", string.Empty, Path);
                UserName = inifile.IniReadValue("StoreInfo", "UserName", Path);
            }

            //Language
            Language = inifile.IniReadValue("StoreInfo", "Language", Path);

            if (Language == string.Empty)
            {
                //default 
                inifile.IniWriteValue("StoreInfo", "Language", "KO", Path);
                Language = inifile.IniReadValue("StoreInfo", "Language", Path);
            }

        }

        public static void ComboBoxDataBinding(ComboBox pTargetCbx, string[] pValue, string[] pName)
        {
            DataTable tempDT = new DataTable();
            DataRow row = null;
            tempDT.Columns.Add(new DataColumn("Code"));
            tempDT.Columns.Add(new DataColumn("Name"));

            for (int i = 0; i < pValue.Length; i++)
            {
                row = tempDT.NewRow();
                row["Code"] = pValue[i];
                row["Name"] = pName[i];
                tempDT.Rows.Add(row);
            }
            // DataBinding
            pTargetCbx.DataSource = tempDT;
            pTargetCbx.DisplayMember = "Name";
            pTargetCbx.ValueMember = "Code";
        }

        private void txtStoreCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                object StoreName = DBHelper.ExecuteScalar("SELECT NM_STORE FROM POS_STORE WHERE CD_STORE = '" + txtStoreCode.Text + "'");

                if (StoreName != null)
                {
                    txtStoreName.Text = StoreName as string;
                }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //패스워드체인지
            if (tabSetting.SelectedTab == tabSetting.TabPages[0])
            {
                if (txtPWDCurrent.Text == string.Empty)
                    return;

                if (!ComparePassword())
                {
                    MessageBox.Show("기존 패스워드와 일치하지 않습니다.");
                    return;
                }

                if (!CheckCurrentPassword())
                {
                    MessageBox.Show("패스워드가 맞지 않습니다..");
                    return;
                }

                UpdatePassword();
                WriteINI();

                if (MessageBox.Show("패스워드가 정상 수정되었습니다.", "", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            //User 세팅
            else if (tabSetting.SelectedTab == tabSetting.TabPages[1])
            {
                WriteINI();
                DBHelper.Reset();
            }
            //Server 세팅
            else if (tabSetting.SelectedTab == tabSetting.TabPages[2])
            {

                WriteINI();
                DBHelper.Reset();
            }

            WriteINI();
            DBHelper.Reset();
            this.Close();

            #region 프로그램 재실행
            //string batchContent = "/c \"@ECHO OFF & timeout /t 6 > nul & start \"\" \"$[APPPATH]$\" & exit\"";
            //batchContent = batchContent.Replace("$[APPPATH]$", Application.ExecutablePath);

            //ProcessStartInfo start = new ProcessStartInfo();
            //start.FileName = @"cmd.exe";
            //start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;   // 윈도우 속성을  windows hidden  으로 지정  
            //start.CreateNoWindow = true;
            //start.Arguments = batchContent;

            //Process.Start(start);
            //Application.Exit();


            //string processName = Process.GetCurrentProcess().ProcessName;
            //Process.Start(Environment.CurrentDirectory + "\\Restart.exe", processName);
            #endregion 
        }

        private void WriteINI()
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

            Jarvis.Util.IniFile inifile = new Jarvis.Util.IniFile();

            inifile.IniWriteValue("DB", "ConnectionString", "Data Source=" + txtDBServer.Text + ";initial Catalog=GPOS;uid=" + DBID + ";password=" + DBPassword + ";", Path);

            inifile.IniWriteValue("UpgradeServer", "IP", txtUpgradeServer.Text, Path);
            inifile.IniWriteValue("StoreInfo", "StoreCode", txtStoreCode.Text, Path);
            inifile.IniWriteValue("StoreInfo", "StoreName", txtStoreName.Text, Path);
            inifile.IniWriteValue("StoreInfo", "UserID", txtUserId.Text, Path);
            inifile.IniWriteValue("StoreInfo", "UserName", txtUserName.Text, Path);
            inifile.IniWriteValue("StoreInfo", "Language", ddlLang.SelectedValue.ToString(), Path);

        }


        private bool CheckCurrentPassword()
        {
            bool result = false;

            object return_obj = DBHelper.ExecuteScalar("USP_POS_PWD_CHANGE_S", new object[] { txtStoreCode.Text, txtUserId.Text, txtPWDCurrent.Text });

            if (return_obj.ToString() == "1")
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        private bool ComparePassword()
        {
            bool result = false;
            if (txtPWDNew.Text == txtPWDReenter.Text)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        private void UpdatePassword()
        {
            string query = "UPDATE POS_EMP "
                        + "SET NO_PWD = PwdEncrypt('" + txtPWDNew.Text + "') "
                        + "WHERE CD_STORE = '" + txtStoreCode.Text + "'"
                        + "AND CD_USER = '" + txtUserId.Text + "'";
            DBHelper.ExecuteNonQuery(query);
        }
    }
}
