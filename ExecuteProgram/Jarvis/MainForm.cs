using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Bifrost;
using Bifrost.Common;
using Bifrost.Data;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using DevExpress.XtraTabbedMdi;
using Jarvis.Util;

namespace Jarvis
{
    public partial class MainForm : MDIBase
    {

        //private BackgroundWorker _bw;
        //private SkinManager skinManager1;
        private FormBase _tmpFormBase;

        protected Bitmap _backbuffer;    //Double Buffering에 사용할 버퍼
        public static Global _loginData;

        //public class MyTabbedMdiManager : DevExpress.XtraTabbedMdi.XtraTabbedMdiManager, DevExpress.XtraTab.IXtraTabProperties
        //{
        //    DevExpress.Utils.DefaultBoolean DevExpress.XtraTab.IXtraTabProperties.ShowTabHeader { get { return DevExpress.Utils.DefaultBoolean.False; } }

        //}

        //private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManagerMain;

        private XtraTabbedMdiManager xtraTabbedMdiManagerMain;

        #region Properties
        public bool isCloseFormAnother { get; set; } = false;
        public bool isUpdated { get; set; } = false;
        #endregion

        #region Contructors
        public MainForm()
        {
            try
            {
                this.Hide();
                //Global.UseWebService = Properties.Settings.Default.UseWebService;
                //if (UpdateFileCheck())
                //{
                //    try
                //    {
                //        DoExecuteProgram();

                //        Thread.Sleep(1000);

                //        KillAppMain();
                //        //Application.ExitThread();
                //        //Environment.Exit(0);
                //    }
                //    catch
                //    {
                //        return;
                //    }
                //}

                InitializeComponent();
                //PromptOnClose = true;
                //this.Font = new Font(FontLibrary.Families[0], 9);

                InitForm();

                InitEvent();
                DoInitialize();
                //InitializeGlobalData();

               

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }
        private void InitForm()
        {
            //panelMenu.Visible = false;
            PromptOnClose = false;

            //skin
            RestorePalette();
        }

        private void RestorePalette()
        {

            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            Jarvis.Util.IniFile inifile = new Jarvis.Util.IniFile();

            string SkinName = inifile.IniReadValue("ApplicationSkinName", "Skin", Path);
            string PaletteName = inifile.IniReadValue("ApplicationSkinName", "Palette", Path);

            if (SkinName == string.Empty)
            {
                //default 
                inifile.IniWriteValue("ApplicationSkinName", "Skin", "The Bezier", Path);
                SkinName = inifile.IniReadValue("ApplicationSkinName", "Skin", Path);

                if (SkinName == "The Bezier" && PaletteName != string.Empty)
                {
                    UserLookAndFeel.Default.SetSkinStyle(SkinName, PaletteName);
                }
                else
                    SetSkin(SkinName);

            }
            else if (PaletteName != string.Empty)
            {
                UserLookAndFeel.Default.SetSkinStyle(SkinStyle.Bezier, PaletteName);
            }
        }

        private void SetSkin(string skinName)
        {
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void InitEvent()
        {
            #region Form Moving
            //this.MouseMove += MainForm_MouseMove;
            //this.MouseDown += MainForm_MouseDown;
            //this.MouseClick += MainForm_MouseClick;
            #endregion

            this.FormClosing += MainForm_FormClosing;
            this.FormClosed += OnFormClosed;
            this.Load += OnMainFormLoad;
            //this.Shown += MainForm_Shown;

            xtraTabbedMdiManagerMain.BeginFloating += XtraTabbedMdiManager_Main_BeginFloating;
            xtraTabbedMdiManagerMain.EndDocking += XtraTabbedMdiManager_Main_EndDocking;
            xtraTabbedMdiManagerMain.SelectedPageChanged += XtraTabbedMdiManager_Main_SelectedPageChanged;
            xtraTabbedMdiManagerMain.PageRemoved += xtraTabbedMdiManager1_PageRemoved;
            xtraTabbedMdiManagerMain.PageAdded += XtraTabbedMdiManagerMain_PageAdded;
            //xtraTabbedMdiManagerMain.AppearancePage.HeaderActive.Font = new Font(xtraTabbedMdiManagerMain.AppearancePage.HeaderActive.Font, FontStyle.Regular);

            xtraTabbedMdiManagerMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            xtraTabbedMdiManagerMain.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            mainToolBar.DoubleClick += MainToolBar_DoubleClick;
        }

        private void MainToolBar_DoubleClick(object sender, EventArgs e)
        {
           
        }

        //업데이트서버 웹서비스용
        private Bifrost.FileTransfer.Client.FileManager wsFileManager;
        private IAppUpdater.AppUpdater wsAppUpdater;

        private bool UpdateFileCheck()
        {
            bool result = false;

            if (InitializeUpdateServerInfo())
            {
                string appStarterFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("/", "\\"));

                string[] localFiles = Directory.GetFiles(appStarterFolder, "*", SearchOption.TopDirectoryOnly);
                string[] localFileInfos = new string[localFiles.Length];

                int i = 0;
                // each file, get file size and date time
                foreach (string localFileName in localFiles)
                {
                    // each file, get file size and date time
                    FileInfo fi = new FileInfo(localFileName);
                    localFileInfos[i] = string.Concat(Path.GetFileName(localFileName), "$", fi.LastWriteTimeUtc.ToString("yyyyMMddHHmm"));
                    i++;
                }

                string[] newFiles;
                newFiles = wsAppUpdater.GetAppUpdaterUpdateList(localFileInfos);
                if (newFiles.Length > 1)
                    result = true;

                newFiles = wsAppUpdater.GetBaseUpdateFiles(localFileInfos);
                if (newFiles.Length > 1)
                    result = true;
            }

            return result;
        }

        string WebServiceURL = string.Empty;
        private bool InitializeUpdateServerInfo()
        {
            /// Application Updater
            try
            {
                string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
                Jarvis.Util.IniFile inifile = new Jarvis.Util.IniFile();

                //upgrade server
                string UpgradeServerIP = inifile.IniReadValue("UpgradeServer", "IP", Path);
                if (UpgradeServerIP == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("UpgradeServer", "IP", "localhost", Path);
                    UpgradeServerIP = inifile.IniReadValue("UpgradeServer", "IP", Path);
                }

                Properties.Settings.Default.AppUpdateUrl = @"http://" + UpgradeServerIP + @"/AppUpdaterService/AppUpdater.asmx";
                WebServiceURL = Properties.Settings.Default.AppUpdateUrl;
                wsFileManager = new Bifrost.FileTransfer.Client.FileManager();

                //서버에 접근가능한지 확인
                if (CheckConnection())
                {
                    this.wsFileManager.Url = Properties.Settings.Default.AppUpdateUrl;
                    this.wsFileManager.UseDefaultCredentials = true;
                    this.wsFileManager.InitializeInstance();

                    this.wsAppUpdater = new Jarvis.IAppUpdater.AppUpdater();
                    this.wsAppUpdater.Url = WebServiceURL;// Properties.Settings.Default.AppUpdateUrl;
                    this.wsAppUpdater.UseDefaultCredentials = true;

                    return true;
                }
                else
                    return false;
            }
            catch
            {
                MessageBox.Show("Update Server is not Access!!");
                return false;
            }
        }

        private bool CheckConnection()
        {
            var url = WebServiceURL;

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 3000;

                var response = (HttpWebResponse)myRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        #region XtraTabbedMdiManager 이벤트 모음
        private void XtraTabbedMdiManagerMain_PageAdded(object sender, MdiTabPageEventArgs e)
        {
            foreach (XtraMdiTabPage page in xtraTabbedMdiManagerMain.Pages)
            {
                _tmpFormBase = page.MdiChild as FormBase;
                page.Tooltip = _tmpFormBase.MenuKey;
            }
        }

        private void XtraTabbedMdiManager_Main_SelectedPageChanged(object sender, EventArgs e)
        {
            if (xtraTabbedMdiManagerMain.SelectedPage == null)
                _SeletedForm = null;
            else
            {
                _SeletedForm = xtraTabbedMdiManagerMain.SelectedPage.MdiChild as FormBase;
                mainToolBar.MainTitle = _SeletedForm.Name;
            }
        }

        FormBase _SeletedForm;
        private void XtraTabbedMdiManager_Main_BeginFloating(object sender, FloatingCancelEventArgs e)
        {
            if (xtraTabbedMdiManagerMain.Pages.Count == 1)
                return;

            _SeletedForm = e.ChildForm as FormBase;
            if (_SeletedForm != null)
                _SeletedForm.isFloating = true;
        }

        private void XtraTabbedMdiManager_Main_EndDocking(object sender, FloatingEventArgs e)
        {
            _SeletedForm = e.ChildForm as FormBase;
            _SeletedForm.isFloating = false;
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, MdiTabPageEventArgs e)
        {

            //if (xtraTabbedMdiManagerMain.Pages.Count == 0)
            //{
            //    _ViewMain = true;
            //}

            MdiForm.OnHomeClick();
        }
        #endregion XtraTabbedMdiManager 이벤트 모음

        #region Form Event
        //private void MainForm_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (mainBiz1.VisibleBiz)
        //    {
        //        mainBiz1.VisibleBiz = false;
        //    }
        //}

        //int _PositionX = 0;
        //int _PositionY = 0;
        //private void MainForm_MouseDown(object sender, MouseEventArgs e)
        //{
        //    _PositionX = e.X;
        //    _PositionY = e.Y;
        //    if (mainBiz1.VisibleBiz)
        //    {
        //        mainBiz1.VisibleBiz = false;
        //    }
        //}

        //private void MainForm_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Control subc = FindControlAtCursor(this);
        //}
        #endregion Form Event

        //public static Control FindControlAtPoint(Control container, Point pos)
        //{
        //    Control child;
        //    foreach (Control c in container.Controls)
        //    {
        //        if (c.Visible && c.Bounds.Contains(pos))
        //        {
        //            child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
        //            if (child == null) return c;
        //            else return child;
        //        }
        //    }
        //    return null;
        //}

        //public static Control FindControlAtCursor(Form form)
        //{
        //    Point pos = Cursor.Position;
        //    if (form.Bounds.Contains(pos))
        //        return FindControlAtPoint(form, form.PointToClient(Cursor.Position));
        //    return null;
        //}

        bool eventhandler1 = false;
        private decimal DoExecuteProgram()
        {
            try
            {
                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                ps.StartInfo.FileName = @"AppStarter.exe";
                ps.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                ps.EnableRaisingEvents = true;
                ps.Exited += new EventHandler(cmdProcess_Exited); // 프로그램 종료 이벤트에 cmdProcess_Exited 메소드 추가

                ps.Start();
                int script_run_time = 10000;  // 외부 프로그램의 최대 실행시간 지정
                int elapsedTime = 0;
                const int SLEEP_AMOUNT = 100;
                while (!eventhandler1)
                {
                    elapsedTime += SLEEP_AMOUNT;
                    if (elapsedTime > script_run_time)
                    {
                        break;
                    }
                    Thread.Sleep(SLEEP_AMOUNT);
                }
                return 1;
            }
            catch
            {
                return -1; //"An error occurred running exe program
            }
        }

        private void cmdProcess_Exited(object sender, System.EventArgs e)
        {
            eventhandler1 = true; // 외부 프로그램이 종료되면 eventhandler1 전역변수를 true로 전환
        }

        #endregion

        #region Initialization
        public bool IsAppUpdating = false;
        private LoadingForm progressForm = null;

        //IXtraTabPage SelectedPage = null;

        public delegate void BackgroundTask();
        private BackgroundTask ProcessTask;

        private void DoInitialize()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            // TODO: Add any initialization after the InitComponent call
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            // This supports mouse movement such as the mouse wheel
            this.SetStyle(ControlStyles.UserMouse, true);
            // This allows the control to be transparent
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            // This helps with drawing the control so that it doesn't flicker
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            // This updates the styles
            this.UpdateStyles();


            if (Screen.PrimaryScreen.WorkingArea.Width <= 1024)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.Size = new Size(1280, 984);
            }


            //xtraTabbedMdiManagerMain.ShowToolTips = DevExpress.Utils.DefaultBoolean.True;
            //xtraTabbedMdiManagerMain.TabPageWidth = 120;
            //xtraTabbedMdiManagerMain.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ScreenUtil.SetValidPosition(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PromptOnClose) return;
            
            //CloseConfirmDialog cfd = new CloseConfirmDialog();
            //cfd.OwnerForm = this;
            //cfd.IsLogoutClosing = IsLogoutClosing;

            //bool bCancel = cfd.ShowDialog(this) != System.Windows.Forms.DialogResult.Yes;
            ////IsLogoutClosing
            //if (IsLogoutClosing)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    e.Cancel = bCancel;
            //}

            //// commit closing
            //CloseCommited = !bCancel;

            //// Reset flag
            //IsLogoutClosing = false;

            //XtraMdiTabPage page = xtraTabbedMdiManager1.Pages[sender as Form];
            //int index = xtraTabbedMdiManager1.Pages.IndexOf(page);
            ////int SelectPageIndex = xtraTabbedMdiManager1.Pages.IndexOf(SelectedPage);
            //index--;
            //if (index >= 0)
            //    xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages[index];
        }

        public void InitializeGlobalData(DataRow loggedInUserInfo, string encodedPassword)
        {
            try
            {
                this.Global = new GlobalData(loggedInUserInfo);
                //this.POSGlobal = new POSGlobalData(loggedInUserInfo);
                //this.Global.User = UserData.FromDataRow(loggedInUserInfo);
                //this.Global.User.PassWord = encodedPassword;

                _loginData = new Global(loggedInUserInfo);

                //상단 날짜 표시
                //mainToolBar.SaleDt = dtpSale.Text;


                //가지고 있는 모듈을 전부 가져옴
                //20171120 권한 있는 모듈만 리소스 변환
                //DataTable DtSystem = DBHelper.GetDataTable("SELECT CD_FLAG FROM MAS_CODEL WHERE CD_FIRM = '" + Global.FirmCode + "' AND CD_CLAS = 'SYS001' AND YN_USE = 'Y'");

                //권한 있는 메뉴 로딩
                LoadMenus();

                //SYS별로 RESOURCE 파일 생성
                string _system = string.Empty;
                string _ResourcePath = AppDomain.CurrentDomain.BaseDirectory + @"LanguageResource\";
                //폴더 생성 및 권한주기 
                SetDirectorySecurity(_ResourcePath);

                if (_listAuthSubSystem != null)
                {
                    //20180627 MAS와 SYS가 없으면 추가
                    if (!_listAuthSubSystem.Contains("MAS")) _listAuthSubSystem.Add("MAS");
                    if (!_listAuthSubSystem.Contains("SYS")) _listAuthSubSystem.Add("SYS");

                    for (int i = 0; i < _listAuthSubSystem.Count; i++)
                    {
                        _system = FrmaeUtil.GetString(_listAuthSubSystem[i]);
                        //SaveResourcesToFile(Global.Language, "Resource", _system, _ResourcePath + "Resources_" + _system + "_" + Global.Language + ".xml");
                    }
                }

                //강제로 시간주기
                System.Threading.Thread.Sleep(100);

                //SYS_CONFIG data를 XML로 변경
                SaveConfigData();

                //StartLoadingMenus();
                //LoginData.Menus = MenusReader.GetAllMenus();
                //mainMenu1.SetMenuSeq();

                //폼세팅
                InitializeFormSetting();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void SetDirectorySecurity(string linePath)
        {
            //폴더생성
            DirectoryInfo di = new DirectoryInfo(linePath);
            if (di.Exists == false)
            {
                di.Create();
            }

            //권한주기
            DirectorySecurity dSecurity = Directory.GetAccessControl(linePath);
            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            dSecurity.AddAccessRule(new FileSystemAccessRule(everyone,
                                                                FileSystemRights.FullControl,
                                                                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                                                PropagationFlags.None,
                                                                AccessControlType.Allow));
            Directory.SetAccessControl(linePath, dSecurity);

        }

        //private Bifrost.Helper.SystemConfig cfgBottomBar;
        //private Bifrost.Helper.SystemConfig cfgMainForm;
        //private Bifrost.Helper.SystemConfig cfgHomepage;
        private Bifrost.Helper.SystemConfig cfgAutoLoadform;

        private void InitializeFormSetting()
        {
            #region 메뉴폼 열기
            //menuMain = new Bifrost.Data.MenuData
            //{
            //    MenuID = "9999",
            //    MenuClass = "POS.M_POS_MENU",
            //    MenuLocation = "M_POS_MENU.dll",
            //    MenuName = "메인메뉴"
            //};

            //MainMenuForm = _CreateChildForm(menuMain, false, true, "");

            PromptOnClose = true;

            //panelMenu.Visible = false;
            #endregion 메뉴폼 열기

            #region 설정에 따른 MainForm 띄우기
            //메인폼
            cfgAutoLoadform = ConfigHelper.GetConfig("SYS001");

            //세팅이 없으면 다시 넣어줌
            if (cfgAutoLoadform == null)
            {
                cfgAutoLoadform = new Bifrost.Helper.SystemConfig();
                cfgAutoLoadform.ConfigCode = "SYS001";
                cfgAutoLoadform.ConfigName = "자동 실행화면";
                cfgAutoLoadform.ConfigValue = "N";
                cfgAutoLoadform.ConfigDescript = "POS.M_POS_SALE";
                cfgAutoLoadform.Remark = "판매등록";
                ConfigHelper.SetConfig(cfgAutoLoadform, "SYS");
            }

            switch (cfgAutoLoadform.ConfigValue)
            {
                case "Y": 
                    Bifrost.Data.MenuData menu = new Bifrost.Data.MenuData
                    {
                        MenuID = "123",
                        MenuClass = cfgAutoLoadform.ConfigDescript,
                        MenuLocation = cfgAutoLoadform.ConfigDescript.Split('.')[1] + ".dll",
                        MenuName = cfgAutoLoadform.Remark
                    };

                    _CreateChildForm(menu, false, true, "");
                    break;
                default:
                    //panelMenu.Visible = true;
                    break;
            }
            #endregion
            
            //메뉴 로딩
            InitAccordionControl();
        }

        /// <summary>
        /// 메뉴 설정
        /// </summary>
        private void InitAccordionControl()
        {
            accordionControlMenu.Clear();

            accordionControlMenu.BeginUpdate();

            DataTable dtMenu = DBHelper.GetDataTable("SELECT * FROM SYS_MMENU");
            DataTable dtMenuGroup = DBHelper.GetDataTable("SELECT CD_MODULE FROM SYS_MMENU GROUP BY CD_MODULE");

            AccordionControlElement[] MenuItem = new AccordionControlElement[dtMenu.Rows.Count];

            accordionControlMenu.ElementClick += new ElementClickEventHandler(this.accordionControl1_ElementClick);

            string ModuleCode = string.Empty;
            int GroupCount = 0;

            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                MenuItem[i] = new AccordionControlElement();
                MenuItem[i].Name = A.GetString(dtMenu.Rows[i]["CD_MENU"]);
                MenuItem[i].Tag = A.GetString(dtMenu.Rows[i]["CD_MODULE"]) + "." + A.GetString(dtMenu.Rows[i]["NM_WINDOW"]);
                MenuItem[i].Text = A.GetString(dtMenu.Rows[i]["NM_MENU"]);


                if (dtMenu.Rows[i]["FG_MENU"].ToString() == "GRP")
                {
                    GroupCount = i;

                    //MenuItem[i].Expanded = true;
                    MenuItem[i].ImageOptions.ImageUri.Uri = "Home;Office2013";
                    accordionControlMenu.Elements.Add(MenuItem[i]);

                }
                else
                {
                    MenuItem[i].Style = ElementStyle.Item;
                    MenuItem[GroupCount].Elements.Add(MenuItem[i]);
                }
            }
            
            accordionControlMenu.EndUpdate();
            //accordionControlMenu.OptionsMinimizing.State = AccordionControlState.Minimized;
            accordionControlMenu.ExpandAll();
            //accordionControlMenu.StateChanged += AccordionControlMenu_StateChanged;
        }


        private void accordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
            if (e.Element.Tag == null) return;

            string itemID = e.Element.Tag.ToString();


            Bifrost.Data.MenuData menu = new Bifrost.Data.MenuData
            {
                MenuID = e.Element.Name,
                MenuClass = e.Element.Tag.ToString(),
                MenuLocation = e.Element.Tag.ToString().Split('.')[1] + ".dll",
                MenuName = e.Element.Text
            };

            //_CreateChildForm(menu, false, true, "");


            MdiForm.CreateChildForm(menu, true);
        }


        private void SavePOSConfigData()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SystemConfig.xml";

                FileInfo fileinfo = new FileInfo(path);

                if (fileinfo.Exists)
                {
                    fileinfo.Delete();
                }

                DataSet ds = DBHelper.GetDataSet("USP_POS_CONFIG_XML_S", new object[] { POSGlobal.StoreCode });
                ds.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SaveConfigData()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SystemConfig.xml";

                FileInfo fileinfo = new FileInfo(path);

                if (fileinfo.Exists)
                {
                    fileinfo.Delete();
                }

                DataSet ds = DBHelper.GetDataSet("USP_SYS_CONFIG_XML_S", new object[] { Global.FirmCode});
                ds.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SaveResourcesToFile(string cultureName, string typeText, string systemType, string xmlFile)
        {
            //기존에 있으면 지우기
            if (File.Exists(xmlFile))
            {
                File.Delete(xmlFile);
            }
            File.Create(xmlFile).Close();

            DataSet resData = LoadResources(cultureName, systemType);
            if (resData.Tables[0].Rows.Count == 0) return;

            XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.Unicode);
            writer.Formatting = Formatting.Indented;

            //Write the root element
            writer.WriteStartDocument();
            writer.WriteStartElement(typeText + "s");

            foreach (DataRow dr in resData.Tables[0].Rows)
            {
                writer.WriteStartElement(typeText);
                writer.WriteStartAttribute("id");
                writer.WriteValue(Convert.ToString(dr["CD_DD"]));
                writer.WriteEndAttribute();
                writer.WriteValue(Convert.ToString(dr["NM_DD"]));
                writer.WriteEndElement();

                Application.DoEvents();
            }

            // end the root element
            writer.WriteEndElement();
            writer.WriteEndDocument();

            //Write the XML to file and close the writer
            writer.Close();
        }

        private DataSet LoadResources(string cultureName, string systemType)
        {
            DataSet resourceData = new DataSet();// DBHelper.GetDataSet("USP_SYS_DD_CREATE_S", new object[] { Global.FirmCode, cultureName, systemType });
            return resourceData;
        }


        /// <summary>
		/// Kill AppStarter
		/// </summary>
		private void KillAppStarter()
        {
            /// 
            /// kill appupdater process
            /// 
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName("AppStarter");

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

        private void KillAppMain()
        {
            /// 
            /// kill appupdater process
            /// 
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName("Ether");

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

        private DialogResult ShowLoginForm()
        {
            //frmLogin loginForm = new frmLogin();
            LoginForm loginForm = new LoginForm();
            loginForm.LoggedIn += new LoggedInEventHandler(OnLoggedIn);
            loginForm.Cancelled += new CancelledEventHandler(OnCancelled);

            //KillAppStarter();

            return loginForm.ShowDialog(this);
        }

        internal void StartLoadingMenus()
        {
            //if (this.Global.Menus == null)
            {
                // starting
                OnStartProcessing("Bifrost.Win.MainForm.ProcessTask", true);

                // assign task
                ProcessTask = LoadMenus;
                //backgroundWorker1.RunWorkerAsync();
                //HideProgressForm();
            }
        }

        //DataTable _dtAuthSubSystem;
        List<string> _listAuthSubSystem;
        private void LoadMenus()
        {
            try
            {
                if (this.Global.Menus == null)
                {

                    DataTable menusData = new DataTable();// DBHelper.GetDataTable("USP_SYS_GET_AUTH_MENU_S", new object[] { this.Global.FirmCode, this.Global.Language, this.Global.User.UserID });

                    //if (menusData != null)
                    //{
                    //    DataTable _dtAuthSubSystem = menusData.DefaultView.ToTable(true, new string[] { "TP_SUBSYSTEM" });
                    //    _listAuthSubSystem = _dtAuthSubSystem.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToList();
                    //}
                    ////SystemMgr.GetAuthorizedMenus(this.Global.FirmCode, this.Global.Language, this.Global.User.UserID);
                    //this.Global.Menus = menusData;
                    //this.Global.Menus.PrimaryKey = new DataColumn[] { this.Global.Menus.Columns["CD_MENU"] };
                    //mainMenu1.LoadMenus();
                    //mainMenu1.SetMenuSeq();
                }

                //업데이트
                IsAppUpdating = true;

                //new UpdateProgForm().Show(this);

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            //KillAppStarter();
            //OnLoggedOut(sender, new LoggedOutEventArgs(Global.User.UserID, true));
        }
        #endregion

        #region OnStartProcessing
        private string _ProcessingMethod = string.Empty;
        private void ShowProgressForm(Form activeForm)
        {
            try
            {
                if (progressForm != null)
                {
                    HideProgressForm(activeForm);
                }

                if (activeForm != null)
                {
                    LoadData.StartLoading(this, "메뉴 여는 중", "화면을 로딩하고 있습니다.");
                }
                else
                {
                    LoadData.StartLoading(this, "메뉴 여는 중", "화면을 로딩하고 있습니다.");
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                SplashScreenManager.CloseForm();
            }
        }
        private void HideProgressForm(Form activeForm)
        {
            Form activeChild = activeForm.ActiveMdiChild;

            //팝업이 있는경우 무조건 닫아줌
            if (activeChild != null)
            {
                if (SplashScreenManager.Default != null)
                {
                    SplashScreenManager.CloseForm();
                }
            }
        }

        /// <summary>
        /// Start/stop show progressing bar
        /// </summary>
        /// <param name="blnStart"></param>
        public override void OnStartProcessing(Form activeForm, string processingMethod, bool blnStart)
        {
            if (blnStart)
            {
                this.Cursor = Cursors.WaitCursor;
                ShowProgressForm(activeForm);
            }
            else
            {
                HideProgressForm(activeForm);
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Start/stop show progressing bar
        /// </summary>
        /// <param name="blnStart"></param>
        public override void OnStartProcessing(string processingMethod, bool blnStart)
        {
            OnStartProcessing(this, processingMethod, blnStart);
        }

        /// <summary>
        /// Start/stop show progressing bar
        /// </summary>
        /// <param name="blnStart"></param>
        public override void OnStartProcessing(bool blnStart)
        {
            OnStartProcessing(string.Empty, blnStart);
        }
        #endregion

        #region BackgroundWorker
        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            // This method will run on a thread other than the UI thread.
            // Be sure not to manipulate any Windows Forms controls created
            // on the UI thread from this method.
            if (ProcessTask == null) return;
            if (!InvokeRequired)
            {
                ProcessTask.Invoke();
            }
            else
            {
                this.Invoke(ProcessTask);
            }
        }
        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnStartProcessing("Bifrost.Win.MainForm.ProcessTask", false);
        }
        #endregion

        #region Event handlers

        public override int GetOpenMenuCount()
        {
            return MdiChildren.Length;
        }

        #region Logo 버튼
        public bool _ViewMain = true;
        public override void OnComLogoClick()
        {
            //if (xtraTabbedMdiManager_Main.Pages.Count == 0) return;

            if (_ViewMain)
            {
                _ViewMain = false;
            }
            else
            {
                _ViewMain = true;
            }
            base.OnComLogoClick();
        }
        #endregion Logo 버튼

        #region Help 버튼
        public override void OnHelpClick()
        {
            MessageBox.Show("Not implemented");
        }
        #endregion

        #region 나가기버튼
        public override void OnCloseTab()
        {
            //if (xtraTabbedMdiManager_Main.SelectedPage == null) return;
            //CloseForm(xtraTabbedMdiManager_Main.SelectedPage.MdiChild);
            //xtraTabbedMdiManager_Main.SelectedPage.MdiChild.Close();
        }
        #endregion 나가기버튼

        #region 전체닫기버튼
        public override void OnCloseTabAll()
        {
            if (ShowMessageBoxA("Close all menus?", MessageType.Question) == DialogResult.Yes)
            {
                ArrayList menuNames = new ArrayList();
                foreach (Form childForm in Application.OpenForms)
                {
                    if (childForm is FormBase)
                        if (((FormBase)childForm).MenuData != null)
                            menuNames.Add(((FormBase)childForm).MenuData.MenuName);
                }

                // close every open form
                foreach (string formName in menuNames)
                {
                    if (formName != "MainForm")
                    {
                        FormBase form = FindFormByKey(formName);
                        CloseForm(form);
                    }
                }
            }
        }

        delegate void CloseMethod(Form form);
        static private void CloseForm(Form form)
        {
            if (form == null) return;
            if (!form.IsDisposed)
            {
                if (form.InvokeRequired)
                {
                    CloseMethod method = new CloseMethod(CloseForm);
                    form.Invoke(method, new object[] { form });
                }
                else
                {
                    foreach (Form ChindForm in form.OwnedForms)
                    {
                        ChindForm.Dispose();
                    }

                    form.Close();
                }
            }
        }
        #endregion

        #region DB 재접속
        public override void OnReConnect(string connectionString)
        {
            DBHelper.Reset(connectionString);
        }
        #endregion

        #region 홈버튼
        public override void OnHomeClick()
        {
            //if (Global.StartupMenu != null)
            //{
            //    CreateChildForm(Global.StartupMenu);
            //}

            //xtraTabbedMdiManagerMain.ac = MainMenuForm;
            //if (panelMenu.Visible)
            //    panelMenu.Visible = false;
            //else
            //    panelMenu.Visible = true;
        }
        #endregion 홈버튼

        #region 프로그램종료
        public override void FormClose()
        {
            this.Close();
        }
        #endregion

        #region 로그아웃
        public override void FormLogOut()
        {

            //로그아웃 팝업 보이기
            IsLogoutClosing = true;
            CloseConfirmDialog cfd = new CloseConfirmDialog();
            cfd.OwnerForm = this;
            cfd.IsLogoutClosing = IsLogoutClosing;


            bool bCancel = cfd.ShowDialog(this) != System.Windows.Forms.DialogResult.Yes;
            // commit closing
            CloseCommited = !bCancel;

            // Reset flag
            IsLogoutClosing = false;
            PromptOnClose = false;

            if (CloseCommited)
            {

                //전체 메뉴 닫기
                ArrayList menuNames = new ArrayList();
                foreach (Form childForm in Application.OpenForms)
                {
                    if (childForm is FormBase)
                        if (((FormBase)childForm).MenuData != null)
                            menuNames.Add(((FormBase)childForm).MenuData.MenuName);
                }

                // close every open form
                foreach (string formName in menuNames)
                {
                    if (formName != "MainForm")
                    {
                        FormBase form = FindFormByKey(formName);
                        CloseForm(form);
                    }
                }

                /// hide me
                this.Hide();

                /// Show LoginForm
                if (ShowLoginForm() == DialogResult.OK)
                {
                    CH._cacheCode.Clear();
                    this.Show();
                }
            }
        }
        #endregion 로그아웃

        #region 화면최소화
        public override void FormMin()
        {
            base.FormMin();

            this.WindowState = FormWindowState.Minimized;

        }
        #endregion

        #region 화면최대/최소화
        public override void FormSizeChange()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.MaximizedBounds = new Rectangle(0, 0, MaximizedBounds.Width, MaximizedBounds.Height);
                this.WindowState = FormWindowState.Maximized;
            }
        }

        #endregion

        #region 시스템 날짜 설정
        public override void SetSystemDate(string saleDate)
        {
            this.POSGlobal.SaleDt = saleDate;
            Bifrost.POSGlobal.SaleDt = saleDate;
            mainToolBar.SaleDt = saleDate;

            ////영업일자 가져오기
            //Bifrost.Helper.SystemConfig cfgSaleDt = ConfigHelper.GetConfig("SYS002");

            //cfgSaleDt.ConfigCode = "SYS002";
            //cfgSaleDt.ConfigName = "영업일자";
            //cfgSaleDt.ConfigValue = saleDate;
            //cfgSaleDt.ConfigDescript = "영업일자를 설정합니다.";
            //ConfigHelper.SetConfig(cfgSaleDt, "SYS");

            //dtpSale.DoValidate();
        }
        #endregion 시스템 날짜 설정 

        #region Form Floating
        public override void FormFloat()
        {
            base.FormFloat();

            //if (xtraTabbedMdiManager_Main.Pages.Count == 1)
            //    return;
            //xtraTabbedMdiManager_Main.BeginUpdate();

            //XtraMdiTabPage FloatPage = xtraTabbedMdiManager_Main.SelectedPage;

            //xtraTabbedMdiManager_Main.Float(FloatPage, new Point(this.Location.X + 75, this.Location.Y + 75));
            //xtraTabbedMdiManager_Main.EndUpdate();

        }
        #endregion

        #region Form Docking
        public override void FormDock(Form _FloatingForm)
        {
            base.FormDock(_FloatingForm);

            //xtraTabbedMdiManager_Main.Dock(_FloatingForm, xtraTabbedMdiManager_Main);
        }
        #endregion

        #region 툴바로 마우스 이동
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);

        public override void ToolBar_MouseDown(MouseEventArgs e)
        {
            base.ToolBar_MouseDown(e);
            if (e.Button == MouseButtons.Left)
            {

                _dragging = true;  // _dragging is your variable flag
                _start_point = new Point(e.X, e.Y);
            }
        }

        public override void ToolBar_MouseMove(MouseEventArgs e)
        {
            base.ToolBar_MouseMove(e);

            if (_dragging)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                }

                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        public override void ToolBar_MouseUp(MouseEventArgs e)
        {
            base.ToolBar_MouseUp(e);

            _dragging = false;  // _dragging is your variable flag

        }
        #endregion

        public override void OnUpdateProgress(int percentage, bool showPercentage)
        {
            //ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = 100;
            //ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
            //ultraStatusBar1.Panels[1].ProgressBarInfo.Value = percentage;
            //ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = showPercentage;
        }

        /// <summary>
        /// Display status text in status bar
        /// </summary>
        /// <param name="statusText"></param>
        public override void OnUpdateStatus(string statusText)
        {
            //if (ultraStatusBar1 == null) return;
            //ultraStatusBar1.Panels[0].Text = statusText;
            Application.DoEvents();
        }

        /// <summary>
        /// Load event of MainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMainFormLoad(object sender, EventArgs e)
        {
            ///
            /// hide me
            ///
            this.Hide();

            

            ///
            /// Show LoginForm
            ///
            if (ShowLoginForm() == DialogResult.OK)
            {
                this.Show();
                ///
                /// Create startup form
                ///
                //CreateChildForm(Global.StartupMenu);
            }
        }


        /// <summary>
        /// Called when click Login button in LoginForm, and the user is authenticated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoggedIn(object sender, LoggedInEventArgs e)
        {
            InitializeGlobalData(e.UserDataRow, e.EncodedPassword);
            //((IUserLogInOut)mainToolBar1).OnLoggedIn();
        }

        /// <summary>
        /// Logged out
        /// </summary>
        private void OnLoggedOut()
        {
            if (Global != null)
                //SystemMgr.LogOut(Global.User.UserID);
                this.Global = null;
        }

        /// <summary>
        /// Called when user clicks Cancel buton in login form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelled(object sender, LoginCancelledEventArgs e)
        {
            // this is a real close
            IsLogoutClosing = false;
            this.Close();
            e.Cancel = !CloseCommited;
        }
        #endregion

        #region Override CreateChildForm

        /// <summary>
        /// Loading class safety
        /// </summary>
        /// <param name="dllLocation"></param>
        /// <param name="className"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private Object _SafeClassLoader(string dllLocation, string className)
        {
            return _SafeClassLoader(dllLocation, className, null);
        }

        /// <summary>
        /// Loading class safety
        /// </summary>
        /// <param name="dllLocation"></param>
        /// <param name="className"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private Object _SafeClassLoader(string dllLocation, string className, object[] args)
        {
            return _SafeClassLoader(dllLocation, className, args, string.Empty);
        }

        /// <summary>
        /// Loading class safety
        /// </summary>
        /// <param name="dllLocation"></param>
        /// <param name="className"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private Object _SafeClassLoader(string dllLocation, string className, object[] args, string menuKey)
        {
            string sErrorMsg = string.Empty;
            Object genericInstance = null;
            string path = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar.ToString();
            this.xtraTabbedMdiManagerMain.ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;

            bool bLoadable = true;
            switch (dllLocation)
            {
                case null:
                    path = string.Empty;
                    break;
                case "":
                    path = string.Empty;
                    break;
                default:
                    path = path + dllLocation;
                    bLoadable = File.Exists(path);
                    break;
            }

            if (bLoadable)
            {
                Assembly asmAssemblyContainingForm = string.IsNullOrEmpty(path) ? Assembly.GetCallingAssembly() : Assembly.LoadFrom(path);
                Type typeToLoad = asmAssemblyContainingForm.GetType(className);

                if (typeToLoad == null)
                {
                    sErrorMsg = string.Format(ResReader.GetString("Class {0} Not Found"), className);
                }
                else
                {
                    if (args != null)
                    {
                        genericInstance = Activator.CreateInstance(typeToLoad, new object[] { args });

                    }
                    else
                    {
                        genericInstance = Activator.CreateInstance(typeToLoad);

                    }
                }
            }
            else
            {
                sErrorMsg = string.Format(ResReader.GetString("Assembly {0} Not Found"), dllLocation);
            }

            // Throw exception if sErrorMsg not empty
            if (sErrorMsg != string.Empty)
                throw new Exception(sErrorMsg);

            return genericInstance;
        }

        /// <summary>
        /// Private function to create a child form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        private FormBase _CreateChildForm(MenuData menuData, bool showPopup, bool autoShow)
        {
            return _CreateChildForm(menuData, showPopup, autoShow, string.Empty);
        }

        /// <summary>
        /// Private function to create a child form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        private FormBase _CreateChildForm(MenuData menuData, bool showPopup, bool autoShow, string menukey)
        {
            return _CreateChildForm(menuData, showPopup, autoShow, null, menukey);
        }

        /// <summary>
        /// Private function to create a child form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        private FormBase _CreateChildForm(MenuData menuData, bool showPopup, bool autoShow, object[] args, string menukey)
        {

            OnStartProcessing(true);

            FormBase newForm = null;

            try
            {
                #region 현재 열린폼 확인
                FormBase opentab = null;
                foreach (FormBase f in MdiChildren)
                {
                    //먼저 CLASS 확인
                    if (f.MenuData.MenuID == menuData.MenuID)
                    {
                        //메뉴키가 있는지, 같은지 확인
                        if (f.MenuKey == menukey)
                        {
                            opentab = f;
                            break;
                        }
                    }
                }
                #endregion


                //열린폼이 있으면 폼을 열어줌
                if (opentab != null)
                {
                    opentab.Activate();
                }
                else
                {
                    if (menuData.FormType == MenuFormType.WinForm)
                    {
                        Object genericInstance = _SafeClassLoader(menuData.MenuLocation, menuData.MenuClass, args);

                        if (genericInstance == null) return null;

                        try
                        {
                            newForm = (FormBase)genericInstance;
                            newForm.IsMdiContainer = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            //newForm = (FormBase)genericInstance;
                        }
                    }
                    //else
                    //{
                    //    newForm = new WebFormBase();
                    //}

                    newForm.Name = menuData.MenuName;
                    newForm.MenuData = menuData;

                    if (!showPopup)
                    {
                        newForm.MdiParent = this;
                        //newForm.FormClosing += MainForm_FormClosing;

                        if (autoShow)
                        {
                            _ViewMain = false;
                            newForm.MenuKey = menukey;
                            newForm.Show();
                        }
                    }
                }

                ////메뉴 툴팁 주석처리
                //foreach (XtraMdiTabPage page in xtraTabbedMdiManagerMain.Pages)
                //{
                //    _tmpFormBase = page.MdiChild as FormBase;
                //    page.Tooltip = _tmpFormBase.MenuKey;
                //}
            }
            catch (Exception ex)
            {
                if (SplashScreenManager.Default != null)
                {
                    SplashScreenManager.CloseForm();
                }
                HandleWinException(ex);
            }
            finally
            {
                OnStartProcessing(false);
                if (SplashScreenManager.Default != null)
                {
                    SplashScreenManager.CloseForm();
                }
            }
            return newForm;
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(MenuData menuData)
        {
            return CreateChildForm(menuData, true);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formNamespace, object[] args)
        {
            return CreateChildForm(formNamespace, args, string.Empty);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formNamespace, object[] args, string menuKey)
        {
            DataRow[] forms = Global.Menus.Select(string.Format("CD_MENU_CLASS = '{0}'", formNamespace));

            if (forms.Length == 0)
            {
                //MessageBox.Show("{0}Not Found in current assembly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowMessageBox("You do not have permission on this menu.", MessageType.Error);
                return null;
            }

            return _CreateChildForm(MenuData.FromDataRow(forms[0]), false, true, args, menuKey);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(MenuData menuData, bool autoShow)
        {
            return _CreateChildForm(menuData, false, autoShow);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formNamespace)
        {
            return CreateChildForm(formNamespace, true);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formNamespace, bool autoShow)
        {
            //if (mainBiz1.VisibleBiz)
            //{
            //    mainBiz1.VisibleBiz = false;
            //}
            DataRow[] forms = Global.Menus.Select(string.Format("CD_MENU_CLASS = '{0}'", formNamespace));

            if (forms.Length == 0)
            {
                //MessageBox.Show("{0}Not Found in current assembly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowMessageBox("You do not have permission on this menu.", MessageType.Error);
                return null;
            }

            return CreateChildForm(MenuData.FromDataRow(forms[0]), autoShow);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formAssLocation, string formNamespace)
        {
            return CreateChildForm(formAssLocation, formNamespace, true);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formAssLocation, string formNamespace, bool autoShow)
        {
            //if (mainBiz1.VisibleBiz)
            //{
            //    mainBiz1.VisibleBiz = false;
            //}
            DataRow[] forms = Global.Menus.Select(string.Format("CD_MENU_CLASS = '{0}' AND CD_MENU_LOCATION = '{1}'", formNamespace, formAssLocation));

            if (forms.Length == 0)
            {
                ShowMessageBox("You do not have permission on this menu.", MessageType.Error);
                return null;
            }

            return CreateChildForm(MenuData.FromDataRow(forms[0]), autoShow);
        }

        /// <summary>
        /// Create a child form and add to this MDI form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildForm(string formAssLocation, string formNamespace, string MenuCode)
        {
            //if (mainBiz1.VisibleBiz)
            //{
            //    mainBiz1.VisibleBiz = false;
            //}
            DataRow[] forms = Global.Menus.Select(string.Format("CD_MENU = '{0}' AND CD_MENU_CLASS = '{1}'", formAssLocation, MenuCode));

            if (forms.Length == 0)
            {
                ShowMessageBox("The selected menu does not exist.", Bifrost.Common.MessageType.Error);
                return null;
            }

            return CreateChildForm(MenuData.FromDataRow(forms[0]), true);
        }

        /// <summary>
        /// Create a form
        /// </summary>
        /// <param name="menuData"></param>
        /// <returns></returns>
        public override FormBase CreateChildPopup(MenuData menuData)
        {
            return _CreateChildForm(menuData, true, true);
        }

        /// <summary>
        /// Create a popup form by namespace and in the current executing assembly
        /// </summary>
        /// <param name="formNamespace"></param>
        /// <returns></returns>
        public override FormBase CreateChildPopup(string formNamespace)
        {
            //if (mainBiz1.VisibleBiz)
            //{
            //    mainBiz1.VisibleBiz = false;
            //}
            DataRow[] forms = Global.Menus.Select(string.Format("CD_MENU_CLASS = '{0}'", formNamespace));

            if (forms.Length == 0)
            {
                ShowMessageBox("You do not have permission on this menu.", MessageType.Error);
                return null;
            }

            return CreateChildPopup(MenuData.FromDataRow(forms[0]));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formAssLocation"></param>
        /// <param name="formNamespace"></param>
        /// <returns></returns>
        public override FormBase CreateChildPopup(string formAssLocation, string formNamespace)
        {
            DataRow[] forms = Global.Menus.Select(string.Format("CD_MENU_CLASS = '{0}' AND CD_MENU_LOCATION = '{1}'", formNamespace, formAssLocation));

            if (forms.Length == 0)
            {
                ShowMessageBox("You do not have permission on this menu.", MessageType.Error);
                return null;
            }

            return CreateChildPopup(MenuData.FromDataRow(forms[0]));
        }

        /// <summary>
        /// Active a tab by menukey
        /// </summary>
        /// <param name="menuKey"></param>
        /// <returns></returns>
        public override bool ActivateTabForm(string menuKey)
        {
            XtraForm tab = null;
            foreach (Form f in MdiChildren)
            {
                if (f is Form)
                {
                    tab = f as XtraForm;
                    break;
                }
            }
            if (tab != null)
                tab.Activate();

            return tab != null;
        }


        #endregion

        #region Shortcut Setting (Debugging tool)
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    Keys key = keyData & ~(Keys.Shift | Keys.Control);

        //    switch (key)
        //    {
        //        case Keys.A:
        //            if (keyData == (Keys.A | Keys.Shift | Keys.Control))
        //            {
        //                DebugTool DebugTool = new DebugTool();
        //                DebugTool.ShowDialog();
        //                return true;
        //            }
        //            break;
        //    }

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        #endregion Shortcut Setting

        #region Skin

        public void AddSkinsMenu(Bar bar)
        {
            bar.Manager.ForceInitialize();
            BarSubItem skinsmenu = new BarSubItem(bar.Manager, "Skins");
            SkinContainerCollection skins = SkinManager.Default.Skins;
            foreach (SkinContainer s in skins)
            {
                BarCheckItem chitem = new BarCheckItem();
                chitem.Caption = s.SkinName;
                chitem.ItemClick += new ItemClickEventHandler(skin_ItemClick);
                chitem.GroupIndex = 1;
                if (s.SkinName == DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName)
                {
                    chitem.Checked = true;
                }
                skinsmenu.AddItem(chitem);
            }

            BarSubItem stylesmenu = new BarSubItem(bar.Manager, "Style");
            string[] styles = System.Enum.GetNames(typeof(DevExpress.LookAndFeel.LookAndFeelStyle));
            foreach (string s in styles)
            {
                BarCheckItem chitem = new BarCheckItem();
                chitem.Caption = s;
                chitem.ItemClick += new ItemClickEventHandler(style_ItemClick);
                chitem.GroupIndex = 1;
                if (s == System.Enum.GetName(typeof(DevExpress.LookAndFeel.LookAndFeelStyle), DevExpress.LookAndFeel.UserLookAndFeel.Default.Style))
                {
                    chitem.Checked = true;
                }
                stylesmenu.AddItem(chitem);
            }


            bar.AddItem(skinsmenu);
            bar.AddItem(stylesmenu);

            BarCheckItem xsitem = new BarCheckItem();
            xsitem.Caption = "Skin XtraForm";
            xsitem.ItemClick += new ItemClickEventHandler(xsitem_ItemClick);
            xsitem.Checked = false;
            skinsmenu.AddItem(xsitem).BeginGroup = true;
        }

        void style_ItemClick(object sender, ItemClickEventArgs e)
        {
            LookAndFeelStyle style = (LookAndFeelStyle)System.Enum.Parse(typeof(DevExpress.LookAndFeel.LookAndFeelStyle), e.Item.Caption);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetStyle(style, false, false);
            ((BarCheckItem)e.Item).Checked = true;
        }

        void skin_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(e.Item.Caption);
            ((BarCheckItem)e.Item).Checked = true;
        }

        void xsitem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DevExpress.Skins.SkinManager.AllowFormSkins)
            {
                DevExpress.Skins.SkinManager.DisableFormSkins();
            }
            else
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
            }
            ((BarCheckItem)e.Item).Checked = DevExpress.Skins.SkinManager.AllowFormSkins;
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        }

        #endregion
    }
}
