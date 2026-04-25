using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bifrost;
using Bifrost.Common;
using Bifrost.Win;
using DevExpress.XtraEditors;

namespace Jarvis
{
    public partial class LoginForm : Form
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
        IniFile inifile = new IniFile();


        readonly Font FontDefault = new Font("카이겐고딕 KR Regular", 28F, FontStyle.Regular, GraphicsUnit.Pixel, 0);

        //기본 폰트 색상
        readonly Color ColorFontDefault = Color.FromArgb(28, 29, 30);
        readonly Color ColorFontPress = Color.White;

        readonly Color ColorMain = Color.FromArgb(170, 203, 239);
        readonly Color ColorSub = Color.FromArgb(199, 225, 239);
        readonly Color ColorPress = Color.FromArgb(31, 85, 153);

        public LoginForm()
        {

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();
            InitControl();

            label1.Parent = panelMain;
            label2.Parent = panelMain;
            label1.BringToFront();
            label2.BringToFront();

            //EnableTransparency(txtUserID);
            //EnableTransparency(txtPassword);

            this.TopLevel = true;

            InitEvent();

        }

        public void EnableTransparency(Control c)
        {
            MethodInfo method = c.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(c, new object[] { ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true });
            c.BackColor = Color.Transparent;
        }


        private void InitEvent()
        {
            btnLogin.Click += BtnLogin_Click;

            //btnA.Click += BtnAlphabet_Click;
            //btnB.Click += BtnAlphabet_Click;
            //btnC.Click += BtnAlphabet_Click;
            //btnD.Click += BtnAlphabet_Click;
            //btnE.Click += BtnAlphabet_Click;
            //btnF.Click += BtnAlphabet_Click;
            //btnG.Click += BtnAlphabet_Click;
            //btnH.Click += BtnAlphabet_Click;
            //btnI.Click += BtnAlphabet_Click;
            //btnJ.Click += BtnAlphabet_Click;
            //btnK.Click += BtnAlphabet_Click;
            //btnL.Click += BtnAlphabet_Click;
            //btnM.Click += BtnAlphabet_Click;
            //btnN.Click += BtnAlphabet_Click;
            //btnO.Click += BtnAlphabet_Click;
            //btnP.Click += BtnAlphabet_Click;
            //btnQ.Click += BtnAlphabet_Click;
            //btnR.Click += BtnAlphabet_Click;
            //btnS.Click += BtnAlphabet_Click;
            //btnT.Click += BtnAlphabet_Click;
            //btnU.Click += BtnAlphabet_Click;
            //btnV.Click += BtnAlphabet_Click;
            //btnW.Click += BtnAlphabet_Click;
            //btnX.Click += BtnAlphabet_Click;
            //btnY.Click += BtnAlphabet_Click;
            //btnZ.Click += BtnAlphabet_Click;

            //btn00.Click += BtnAlphabet_Click;
            //btn0.Click += BtnAlphabet_Click;
            //btn1.Click += BtnAlphabet_Click;
            //btn2.Click += BtnAlphabet_Click;
            //btn3.Click += BtnAlphabet_Click;
            //btn4.Click += BtnAlphabet_Click;
            //btn5.Click += BtnAlphabet_Click;
            //btn6.Click += BtnAlphabet_Click;
            //btn7.Click += BtnAlphabet_Click;
            //btn8.Click += BtnAlphabet_Click;
            //btn9.Click += BtnAlphabet_Click;

            //btnBackSpace.Click += BtnBackSpace_Click;
            //btnClear.Click += BtnClear_Click;
            //btnEnter.Click += BtnEnter_Click;

            btnSetting.Click += BtnSetting_Click;
            btnExit.Click += BtnExit_Click;
            this.panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseDown);
            this.Activated += new System.EventHandler(this.LoginForm_Activated);
            this.FormClosing += LoginForm_FormClosing;
        }

        private void InitControl()
        {
            txtUserID.BackColor = panelMain.BackColor;
            txtPassword.BackColor = panelMain.BackColor;
            
            //SetButtonApperance(btnBackSpace, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
            //SetButtonApperance(btnClear, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);
            //SetButtonApperance(btnEnter, FontDefault, ColorFontDefault, ColorFontPress, ColorSub, ColorPress);

            //SetButtonApperance(btn00, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn0, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn1, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn2, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn3, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn4, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn5, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn6, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn7, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn8, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btn9, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);

            //SetButtonApperance(btnA, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnB, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnC, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnD, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnE, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnF, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnG, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnH, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnI, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnJ, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnK, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnL, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnM, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnN, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnO, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnP, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnQ, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnR, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnS, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnT, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnU, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnV, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnW, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnX, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnY, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
            //SetButtonApperance(btnZ, FontDefault, ColorFontDefault, ColorFontPress, ColorMain, ColorPress);
        }

        private void SetButtonApperance(SimpleButton btn, Font defaultFont, Color defaultFontColor, Color pressFontColor, Color defaultColor, Color pressColor)
        {
            btn.AllowFocus = false;
            btn.Appearance.BackColor = defaultColor;
            btn.Appearance.Font = defaultFont;
            btn.Appearance.ForeColor = defaultFontColor;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.BorderColor = Color.FromArgb(232, 232, 232);
            btn.Appearance.Options.UseBorderColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.AppearancePressed.BackColor = pressColor;
            btn.AppearancePressed.Font = defaultFont;
            btn.AppearancePressed.ForeColor = pressFontColor;
            btn.AppearancePressed.Options.UseBackColor = true;
            btn.AppearancePressed.Options.UseFont = true;
            btn.AppearancePressed.Options.UseForeColor = true;
            btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            btn.LookAndFeel.UseDefaultLookAndFeel = false;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            CloseConfirmDialog cfd = new CloseConfirmDialog();
            cfd.OwnerForm = this;

            if (cfd.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
            {
                KillAppMain();
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            //if (loginStatus == UserLoginStatus.UnknownError) return;
            if (loginStatus != UserLoginStatus.LogInSuccess)
            {
                CloseConfirmDialog cfd = new CloseConfirmDialog();
                cfd.OwnerForm = this;

                if (cfd.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
                {
                    KillAppMain();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void KillAppMain()
        {
            /// 
            /// kill appupdater process
            /// 
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName("Jarvis");

                while (processes.Length != 0)
                {
                    //Loop through the running processes in with the same name
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }

                    //Application.DoEvents();
                    //processes = Process.GetProcessesByName("AppStarter");
                }
            }
            catch
            {
            }
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            SettingForm SettingForm = new SettingForm();
            SettingForm.StartPosition = FormStartPosition.CenterParent;

            SettingForm.ShowDialog();
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            if(txtUserID.IsEditorActive)
            {
                txtPassword.Select();
            }
            else if (txtPassword.IsEditorActive)
            {
                DoLogin(sender);
            }
        }

        public static Control FindFocusedControl(Control container)
        {
            foreach (Control childControl in container.Controls)
            {
                if (childControl.Focused)
                {
                    return childControl;
                }
            }

            foreach (Control childControl in container.Controls)
            {
                Control maybeFocusedControl = FindFocusedControl(childControl);
                if (maybeFocusedControl != null)
                {
                    return maybeFocusedControl;
                }
            }

            return null; 
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            Control selectedControl = FindFocusedControl(panelMain);
            selectedControl.Text = "";
            //selectedControl.GetType().GetProperty("Text").SetValue(selectedControl, "");
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            if (txtUserID.IsEditorActive)
            {
                DeleteStringBackside(txtUserID);
            }
            else if (txtPassword.IsEditorActive)
            {
                DeleteStringBackside(txtPassword);
            }

        }

        private void DeleteStringBackside(TextEdit textedit)
        {
            int selstart = textedit.SelectionStart;
            if (selstart == 0) return;
            textedit.Text = textedit.Text.Remove(selstart - 1, 1);
            textedit.SelectionStart = selstart - 1;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
            {
                ShowMessageBox("아이디를 입력해주세요.", MessageType.Warning);
                txtUserID.Focus();
                return;
            }
            else if (txtPassword.Text == "")
            {
                ShowMessageBox("암호를 입력해주세요.", MessageType.Warning);
                txtPassword.Focus();
                return;
            }
            else
            {
                DoLogin(sender);
            }
        }

        private void BtnAlphabet_Click(object sender, EventArgs e)
        {
            if (txtUserID.IsEditorActive)
            {
                int selstart = txtUserID.SelectionStart;

                txtUserID.Text = txtUserID.Text.Insert(selstart, ((DevExpress.XtraEditors.SimpleButton)sender).Text);
                txtUserID.SelectionStart = selstart + 1;
            }
            else if (txtPassword.IsEditorActive)
            {
                int selstart = txtPassword.SelectionStart;

                txtPassword.Text = txtPassword.Text.Insert(selstart, ((DevExpress.XtraEditors.SimpleButton)sender).Text);
                txtPassword.SelectionStart = selstart + 1;
            }


        }

        #region Movable
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void LoginForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Events

        public event LoggedInEventHandler LoggedIn;
        private void OnLoggedIn(object sender, LoggedInEventArgs e)
        {
            LoggedIn?.Invoke(sender, e);
        }

        public event CancelledEventHandler Cancelled;
        private void OnCancelled(object sender, LoginCancelledEventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm SettingForm = new SettingForm();
            SettingForm.StartPosition = FormStartPosition.CenterParent;

            SettingForm.ShowDialog();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt)    // ALT 키 조합
            {
                if (e.KeyCode == Keys.F4)
                {
                    LoginCancelledEventArgs LoninCancel = new LoginCancelledEventArgs();

                    OnCancelled(sender, LoninCancel);
                    DialogResult = LoninCancel.Cancel ? DialogResult.None : DialogResult.Cancel;

                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
        }

        #endregion Events

        #region Handling

        /// <summary>
        /// Form Init
        /// </summary>
        private void FormInitialize()
        {
            this.ImeMode = ImeMode.NoControl;
            txtPassword.Focus();
        }

        public string getIPv4Addr
        {
            get
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                string clientIP = string.Empty;
                for (int i = 0; i < host.AddressList.Length; i++)
                {
                    // AddressFamily.InterNetworkV6 - IPv6
                    if (host.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        clientIP = host.AddressList[i].ToString();
                    }
                }
                return clientIP;
            }
        }
        public string getIPv6Addr
        {
            get
            {
                // Get local ip address
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                string clientIP = Dns.GetHostName();

                if (host.AddressList.Length != 0)
                {
                    clientIP = host.AddressList[0].ToString();
                }
                return clientIP;
            }
        }
        public string getMACAddr
        {
            get
            {
                return NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
            }
        }


        public string getExtIPAddr
        {
            get
            {
                string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim(); 

                if (String.IsNullOrWhiteSpace(externalip))
                {
                    externalip = getIPv4Addr;//null경우 Get Internal IP를 가져오게 한다.
                }

                return externalip;
            }
        }

        /// <summary>
        /// Handling login
        /// </summary>
        ///                 UserLoginStatus loginStatus = UserLoginStatus.UnknownError;

        UserLoginStatus loginStatus = UserLoginStatus.UnknownError;

        private void DoLogin(object sender)
        {
            this.Cursor = Cursors.WaitCursor;

            SystemMgr SystemMgr = new SystemMgr();

            try
            {
                DataSet loginInfo = null;

                string sIPAddr = getExtIPAddr;
                string sMACAdr = getMACAddr;



                string _FirmCode = inifile.IniReadValue("FirmInfo", "FirmCode", _path);
                if (_FirmCode == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("FirmInfo", "FirmCode", "1000", _path);
                    _FirmCode = inifile.IniReadValue("FirmInfo", "FirmCode", _path);
                }

                string _LanguageCode = inifile.IniReadValue("FirmInfo", "Language", _path);
                if (_LanguageCode == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("FirmInfo", "Language", "KO", _path);
                    _LanguageCode = inifile.IniReadValue("FirmInfo", "Language", _path);
                }

                string encodedUserID = Base.BifrostEncrypt(txtUserID.Text);
                string encodedPassword = Base.BifrostEncrypt(txtPassword.Text);

                loginInfo = SystemMgr.Authenticate(_FirmCode, _LanguageCode, encodedUserID, encodedPassword, sIPAddr, sMACAdr);

                if (loginInfo != null && loginInfo.Tables["LoginStatus"] != null && loginInfo.Tables["LoginStatus"].Rows.Count != 0)
                {
                    loginStatus = (UserLoginStatus)(int)loginInfo.Tables["LoginStatus"].Rows[0]["LoginStatus"];
                }

                switch (loginStatus)
                {
                    case UserLoginStatus.InvalidCredentials:
                        ShowMessageBox("사용자 아이디를 확인하세요.", MessageType.Error);//사용자ID를 확인하세요
                        txtUserID.SelectAll();
                        break;
                    case UserLoginStatus.WrongPassword:
                        ShowMessageBox("암호가 틀립니다.\n암호를 확인해주세요.", MessageType.Error);//Password가 틀립니다
                        txtPassword.SelectAll();
                        break;
                    case UserLoginStatus.LogInSuccess:

                        if (loginInfo.Tables["UserInfo"].Rows.Count == 0)
                        {
                            ShowMessageBox("등록된 사용자가 존재하지 않습니다.\n아이디를 확인해주세요", MessageType.Error);//사용자의 정보가 없습니다
                            break;
                        }
                        inifile.IniWriteValue("FirmInfo", "UserID", txtUserID.Text, _path);

                        this.Hide();

                        /// 
                        /// raise loggedin event
                        /// 
                        LoggedInEventArgs ev = new LoggedInEventArgs(txtUserID.Text, txtPassword.Text, loginInfo.Tables["UserInfo"].Rows[0]);
                        OnLoggedIn(this, ev);

                        ///
                        /// Show main form
                        /// 
                        if (ev.NeedRestart)
                            DialogResult = DialogResult.Cancel;
                        else
                            DialogResult = DialogResult.OK;

                        this.ShowInTaskbar = false;
                        break;
                    case UserLoginStatus.UsageExpired:
                        ShowMessageBox("권한이 없거나 기간이 만료 되었습니다.", MessageType.Error);//권한이 없거나 기간이 만료 되었습니다
                        break;
                    case UserLoginStatus.UnknownError:
                    default:
                        ShowMessageBox("오류가 있습니다.", MessageType.Error);//프로그램에 오류가 있습니다.
                        break;
                }
            }
            catch (Exception ex)
            {
                new MsgDialog(MessageType.Error, ex).ShowDialog(this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Show messagebox by msgCode (resource id)
        /// </summary>
        /// <param name="msgCode">ٱ- MessageCode</param>
        /// <param name="msgType"></param>
        private DialogResult ShowMessageBox(string msgCode, MessageType msgType)
        {
            return new MsgDialog(msgCode, msgType).ShowDialog(this);
        }

        #endregion Handling

        #region Event Handlers

        //void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //	if (e.CloseReason == CloseReason.UserClosing)
        //	{
        //		LoginCancelledEventArgs le = new LoginCancelledEventArgs();
        //		OnCancelled(sender, le);
        //		DialogResult = le.Cancel ? DialogResult.None : DialogResult.Cancel;
        //	}
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
            {
                txtUserID.Focus();
                return;
            }
            else if (txtPassword.Text == "")
            {
                txtPassword.Focus();
                return;
            }
            else
            {
                DoLogin(sender);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoginCancelledEventArgs le = new LoginCancelledEventArgs();

            OnCancelled(sender, le);
            DialogResult = le.Cancel ? DialogResult.None : DialogResult.Cancel;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        /// <summary>
        /// Processing shortcut key
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    btnLogin_Click(this, EventArgs.Empty);
                    break;
                case Keys.Escape:
                    btnCancel_Click(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            FormInitialize();
        }

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private const int WS_SHOWNORMAL = 1;
        private void ActivateWindow(IntPtr handle)
        {
            //Set the real intance to foreground window
            SetForegroundWindow(handle);
        }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
            txtUserID.Text = inifile.IniReadValue("FirmInfo", "UserID", _path);

            if (txtUserID.Text == string.Empty)
            {
                txtUserID.Focus();
            }
            else
            {
                txtPassword.Focus();
            }
        }

        #endregion Event Handlers		
    }
}
