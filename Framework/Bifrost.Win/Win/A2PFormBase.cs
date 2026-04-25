using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Text;
using System.Diagnostics;

using Bifrost.Common;
using Bifrost.Data;
using System.IO;
using System.Security.AccessControl;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;

namespace Bifrost.Win
{
    public partial class A2PFormBase : FormBase
    {
        #region Public properties

        private EditAction _EditAction = EditAction.Edit;

        /// <summary>
        /// Editing behavior, default: EditAction.Edit
        /// Form is in Editing Mode
        /// </summary>
        [Browsable(false)]
        public EditAction EditAction
        {
            get { return _EditAction; }
            set { _EditAction = value; }
        }


        /// <summary>
        /// Form Title
        /// </summary>
        [Browsable(false)]
        public string SubTitle
        {
            get { return lblSubTitle.Text; }
            set { Text = lblSubTitle.Text = value; }
        }

        private bool _isSearchData = false;
        [Browsable(false)]
        public bool isSearchData
        {
            get { return _isSearchData; }
            set { _isSearchData = value; }
        }
        #endregion Public

        #region Initialization & Privates

        //private FormValidator formValidator1 = new FormValidator();
        private bool[] _ButtonValidations;
        private string GridSettingPath = string.Empty;
        private string GridSettingClass = string.Empty;
        /// <summary>
        /// Validation setting of each button in toolbar
        /// </summary>
        protected bool[] ButtonValidations
        {
            get { return _ButtonValidations; }
            set { _ButtonValidations = value; }
        }

        public A2PFormBase()
        {
            this.AutoScaleMode = AutoScaleMode.Inherit;
            // TODO: Add any initialization after the InitComponent call
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            // This supports mouse movement such as the mouse wheel
            this.SetStyle(ControlStyles.UserMouse, true);

            // This allows the control to be transparent
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // This helps with drawing the control so that it doesn't flicker
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            // This updates the styles
            this.UpdateStyles();

            InitializeComponent();
            FormInitialize();
            EventInitialize();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);

                #region 상단 버튼 권한 세팅
                //20180626 조회는 무조건 다 조회
                //btnSearch.Enabled = this.MenuData.Permissions.AllowRead;
                btnSearch.Enabled = true;
                btnNew.Enabled = this.MenuData.Permissions.AllowCreate;
                btnSave.Enabled = this.MenuData.Permissions.AllowUpdate;
                btnDelete.Enabled = this.MenuData.Permissions.AllowDelete;
                btnExcel.Enabled = this.MenuData.Permissions.AllowExportExcel;
                btnPrint.Enabled = this.MenuData.Permissions.AllowPrint;
                #endregion 상단 버튼 세팅
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }


        private void EventInitialize()
        {
            #region FormMove
            panelTop.MouseMove += panelTop_MouseMove;
            panelTop.MouseUp += panelTop_MouseUp;
            panelTop.MouseDown += panelTop_MouseDown;
            panelTop.DoubleClick += PanelTop_DoubleClick;
            //lblSubTitle.MouseMove += panelTop_MouseMove;
            //lblSubTitle.MouseUp += panelTop_MouseUp;
            //lblSubTitle.MouseDown += panelTop_MouseDown;

            //pnlBottom.MouseMove += panelTop_MouseMove;
            //pnlBottom.MouseUp += panelTop_MouseUp;
            //pnlBottom.MouseDown += panelTop_MouseDown;
            #endregion

            btnClose.Click += BtnClose_Click;
            btnResize.Click += BtnResize_Click;
            btnMin.Click += BtnMin_Click;
            btnReturn.Click += BtnReturn_Click;

            this.TextChanged += A2PFormBase_TextChanged;
            this.FormClosed += A2PFormBase_FormClosed;
            this.Shown += A2PFormBase_Shown;

            btnSearch.Click += BtnSearch_Click;
            btnNew.Click += BtnNew_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnExcel.Click += BtnExcel_Click;
            btnPrint.Click += BtnPrint_Click;
            btnExit.Click += BtnClose_Click;
        }

        private void PanelTop_DoubleClick(object sender, EventArgs e)
        {
            FormResize();
        }

        #region Window Form Resize
      

        private void FormResize()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                //this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
            }
        }
       
        #endregion

        #region Properties
        //string _MenuKey = string.Empty;
        //public string MenuKey
        //{
        //    get { return _MenuKey; }
        //    set { _MenuKey = value; }
        //}


        private bool _isFloating = false;

        public override bool isFloating
        {
            get { return _isFloating; }
            set
            {
                _isFloating = value;
                panelTop.Visible = value;
            }
        }
        #endregion Properties

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        private void FormInitialize()
        {
            /// 
            /// MenuData
            /// 
            MenuData = new MenuData();
            isFloating = false;

            //PrivateFontCollection privateFonts = new PrivateFontCollection();
            ////폰트명이 아닌 폰트의 파일명을 적음
            //privateFonts.AddFontFile("D2CODING.TTC");
            ////24f 는 출력될 폰트 사이즈
            //Font inboundFont = new Font(privateFonts.Families[0], 9f);

            //DevExpress.Utils.AppearanceObject.DefaultFont = inboundFont;// new Font("D2Coding", 9f);
            //DevExpress.Utils.AppearanceObject.DefaultMenuFont = inboundFont;//new Font("D2Coding", 9f);

            //string FontName = GetFontNameFromFile("AritaSans-Medium.otf");
            //DevExpress.Utils.AppearanceObject.DefaultFont = new Font(FontName, 9f);
            //DevExpress.Utils.AppearanceObject.DefaultMenuFont = new Font(FontName, 9f);
            //DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font(FontName, 9f);

            DevExpress.Utils.AppearanceObject.DefaultFont = new  System.Drawing.Font("카이겐고딕 KR Regular", 9F);


            //this.pnlInBound.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.panelTop.Visible = false;
            this.Text = string.Empty;

           
        }

        private static string GetFontNameFromFile(string filename)
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();

            //string _strFile = "D:\\Fonts\\AritaSans-Medium.ttf"; //파일경로
            //FileInfo _finfo = new FileInfo(_strFile);


            // Add three font files to the private collection.
            //fontCollection.AddFontFile(_finfo.FullName);
            //fontCollection.AddFontFile("D:\\Fonts\\AritaSans-Medium.otf");

            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Font\";

            fontCollection.AddFontFile(Path + filename);
            return fontCollection.Families[0].Name;
        }

        /// <summary>
        /// Apply Styles to controls on form
        /// </summary>
        /// <param name="c"></param>
        private void ApplyNewSubSystemType(Control c)
        {
            //foreach (Control ctrl in c.Controls)
            //{

            //    if (ctrl is IXControl)
            //    {
            //        ((IXControl)ctrl).OnSubSystemTypeChanged(SubSystemType, false);
            //    }
            //    if (ctrl.GetType().Name == "aPanel")
            //    {
            //        if (((aPanel)ctrl).PanelStyle == aPanel.PanelStyles.Header)
            //            ApplyNewHeaderSubSystemType(ctrl);
            //        else
            //            ApplyNewSubSystemType(ctrl);
            //    }
            //    else
            //    {
            //        ApplyNewSubSystemType(ctrl);
            //    }
            //}
        }

        private void ApplyNewHeaderSubSystemType(Control c)
        {
            //foreach (Control ctrl in c.Controls)
            //{

            //    if (ctrl is IXControl)
            //    {
            //        ((IXControl)ctrl).OnSubSystemTypeChanged(SubSystemType, true);
            //    }

            //    ApplyNewHeaderSubSystemType(ctrl);
            //}
        }

        /// <summary>
        /// Change SubSystemType
        /// </summary>
        protected override void OnSubSystemTypeChanged()
        {
            //pnlBottom.BackColor = formSettings.FormBackColor;
            //pnlInBound.BackColor = formSettings.WorkAreaBackColor;
            //pnlInBound.BorderColor = formSettings.BorderColor;

            //switch (SubSystemType)
            //{
            //    case SubSystemType.PP:
            //        tbimgRightBG.Image = Toolbar.tbbg_1_topright;
            //        break;
            //    case SubSystemType.SM:
            //        tbimgRightBG.Image = Toolbar.tbbg_2_topright;
            //        break;
            //    case SubSystemType.QA:
            //        tbimgRightBG.Image = Toolbar.tbbg_3_topright;
            //        break;
            //    case SubSystemType.AW:
            //        tbimgRightBG.Image = Toolbar.tbbg_4_topright;
            //        break;
            //    case SubSystemType.PM:
            //        tbimgRightBG.Image = Toolbar.tbbg_5_topright;
            //        break;
            //    case SubSystemType.FC:
            //        tbimgRightBG.Image = Toolbar.tbbg_6_topright;
            //        break;
            //    case SubSystemType.HR:
            //        tbimgRightBG.Image = Toolbar.tbbg_7_topright;
            //        break;
            //    case SubSystemType.CO:
            //        tbimgRightBG.Image = Toolbar.tbbg_8_topright;
            //        break;
            //    default:
            //        break;
            //}

            ///
            /// Call control IXControl.OnSubSysTypeChanged function
            /// 
            //ApplyNewSubSystemType(this);

        }

        /// <summary>
        /// Call to validate and display the error dialog
        /// </summary>
        /// <param name="btnIdx"></param>
        /// <returns></returns>
        private bool DoValidate(int btnIdx)
        {
            if (!_ButtonValidations[btnIdx])
                return true;

            StartValidate();
            //formValidator1.Validate();

            //if (!formValidator1.IsValid || !IsValidated)
            //{
            //    new MsgDialog(MessageType.Warning, new UIException("M04436", null)).ShowDialog(this);
            //}

            return IsValidated;
        }

        /// <summary>
        /// On Change MenuData
        /// </summary>
        protected override void OnMenuDataChanged()
        {
            base.OnMenuDataChanged();

            ///
            /// Apply title
            /// 
            SubTitle = MenuData.MenuName;
            //lblMenuPath.Text = MenuData.MenuPath;
        }

        /// <summary>
        /// Enable/Disable Required Validation feature of each button in toolbar
        /// </summary>
        /// <param name="toolbarBtnIndex"></param>
        /// <param name="required"></param>
        protected void SetRequireValidation(int toolbarBtnIndex, bool required)
        {
            _ButtonValidations[toolbarBtnIndex] = required;
        }

        /// <summary>
        /// Support function of Update status text in MainForm
        /// </summary>
        /// <param name="statusText"></param>
        private void OnUpdateStatus(string statusText)
        {
            if (MdiForm == null) return;
            MdiForm.OnUpdateStatus(statusText);
        }

        /// <summary>
        /// Support function for excel exporting
        /// </summary>
        private void _OnExcelExport()
        {
            OnExcelExport(string.Empty);
        }

      

        private void A2PFormBase_TextChanged(object sender, EventArgs e)
        {
            lblSubTitle.Text = this.Text;
        }

        #endregion Initialization & Privates

        #region Event Handler

        #region Button Click Event

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            MdiForm.FormDock(this);
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnResize_Click(object sender, EventArgs e)
        {
            FormResize();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            OnExcelExport(string.Empty);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            OnInsert();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OnView();
        }

        #endregion

        #region 단축키 설정
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control | Keys.Alt);

            switch (keyData)
            {
                case Keys.F1:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    OnHelpMenual();
                    return true;
                case Keys.F2:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    if(btnNew.Enabled) OnMoveInsert();
                    return true;
                case Keys.F3:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveModify();
                    return true;
                case Keys.F4:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveViewOnly();
                    //OnMoveCopy();
                    return true;
                case Keys.F5:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    OnView();
                    //201806026 조회는 무조건 조회
                    //if (btnSearch.Enabled) OnView();
                    return true;
                case Keys.F6:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveCopy();
                    //OnMoveOrderSearch();
                    return true;
                case Keys.F7:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveAccount();
                    //OnMovePartner();
                    return true;
                case Keys.F8:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveView();
                    //OnMoveMasterSearch();
                    return true;
                case Keys.F9:
                    //OnMoveAccount();
                    return true;
                case Keys.F10:
                    //OnModuleFile();
                    return true;
                case Keys.F11:
                    //OnModuleMemo();
                    return true;
                case Keys.F12:
                    //OnSave();
                    return true;

                #region 조합키 영역
                #region ALT 키를 조합한 바로 가기 키
                case Keys.Alt | Keys.D1:
                    OnModuleMemo();
                    return true;
                case Keys.Alt | Keys.D2:
                    OnModuleFile();
                    return true;
                #endregion

                #region CONTROL 키를 조합한 바로 가기 키
                #region 독립
                case Keys.Control | Keys.P:
                    OnPrint();
                    return true;
                case Keys.Control | Keys.W:
                    Close();
                    return true;
                case Keys.Control | Keys.Left:
                    MdiForm.OnLeftClick();
                    return true;
                case Keys.Control | Keys.Right:
                    MdiForm.OnRightClick();
                    return true;
                #endregion

                #region 상단 아이콘
                //case Keys.Control | Keys.N://F2
                //    OnMoveInsert();
                //    return true;
                //case Keys.Control | Keys.Space://F3
                //    OnMoveModify();
                //    return true;
                //case Keys.Control | Keys.R://F5
                //    OnView();
                //    return true;
                //case Keys.Control | Keys.S://F12
                //    OnSave();
                //    return true;
                #endregion
                #endregion


                #endregion

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

        }

        #endregion

        private void A2PFormBase_Shown(object sender, EventArgs e)
        {
            if (GridSettingPath == string.Empty)
            {
                GridSettingPath = Application.StartupPath + @"\GridLayout\";
            }

            //20171220 그리드 컬럼 세팅 일단 주석
            //20171124 프로그램이 실행될때만 적용되게, 개발 디자인 화면에서는 적용되지 않게 하려고
            Process[] precessList = Process.GetProcessesByName("AIMS2TEST");
            if (precessList.Length > 0)
                ApplyGridSetting((Control)sender, GridSettingPath);
        }

        private void A2PFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            //20171220 그리드 컬럼 세팅 일단 주석
            if (GridSettingPath == string.Empty)
            {
                GridSettingPath = Application.StartupPath + @"\GridLayout\";
            }

            SetGridSetting((Control)sender);
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
            dSecurity.AddAccessRule(new FileSystemAccessRule("Users",
                                                                FileSystemRights.FullControl,
                                                                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                                                PropagationFlags.None,
                                                                AccessControlType.Allow));
            Directory.SetAccessControl(linePath, dSecurity);
        }

        private void ApplyGridSetting(Control pTarget, string GridSettingPath)
        {
            Control[] FormControl;
            FormControl = GetAllControls(pTarget);
            SetDirectorySecurity(GridSettingPath);

            foreach (var control in FormControl)
            {
                if (control.GetType().FullName == "Bifrost.Grid.aGrid")
                {
                    if (((Bifrost.Grid.aGrid)control).isSaveLayout)
                    {
                        GridSettingClass = this.GetType().FullName + "_" + control.Name + ".xml";
                        System.IO.FileInfo fi = new System.IO.FileInfo(GridSettingPath + GridSettingClass);
                        if (fi.Exists)
                        {
                            ((DevExpress.XtraGrid.GridControl)control).MainView.BeforeLoadLayout += MainView_BeforeLoadLayout;
                            ((DevExpress.XtraGrid.GridControl)control).MainView.RestoreLayoutFromXml(GridSettingPath + GridSettingClass);
                        }
                    }
                }
            }
        }

        private static Control[] GetAllControls(Control containerControl)
        {
            List<Control> allControls = new List<Control>();

            Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
            queue.Enqueue(containerControl.Controls);

            while (queue.Count > 0)
            {
                Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();
                if (controls == null || controls.Count == 0) continue;

                foreach (Control control in controls)
                {
                    allControls.Add(control);
                    queue.Enqueue(control.Controls);
                }
            }

            return allControls.ToArray();
        }

        private void MainView_BeforeLoadLayout(object sender, DevExpress.Utils.LayoutAllowEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.OptionsLayout.LayoutVersion != e.PreviousVersion)
            {
                view.OptionsLayout.Columns.StoreLayout = false;
            }
            else
            {
                view.OptionsLayout.Columns.StoreLayout = true;
            }
        }

        private void SetGridSetting(Control pTarget)
        {

            if (GridSettingPath == string.Empty)
            {
                GridSettingPath = Application.StartupPath + @"\GridLayout\";
            }

            if (!System.IO.Directory.Exists(GridSettingPath))
                System.IO.Directory.CreateDirectory(GridSettingPath);

            if (pTarget.HasChildren)
            {
                if (pTarget.GetType().FullName == "Bifrost.Grid.aGrid")
                {
                    GridSettingClass = this.GetType().FullName + "_" + pTarget.Name + ".xml";
                    ((DevExpress.XtraGrid.GridControl)pTarget).MainView.SaveLayoutToXml(GridSettingPath + GridSettingClass);
                }

                foreach (Control ctl in pTarget.Controls)
                {
                    SetGridSetting(ctl);
                }
            }
        }

        #endregion Event Handlers

        #region Must Override Methods

        /// <summary>
        /// Occur on clicking Insert button in toolbar
        /// </summary>
        public virtual void OnInsert() { }
        /// <summary>
        /// [F12] Occur on clicking Save button in toolbar
        /// </summary>
        public virtual void OnSave() { }
        /// <summary>
        /// Occur on clicking Delete button in toolbar
        /// </summary>
        public virtual void OnDelete() { }
        /// <summary>
        /// [F5] Occur on clicking View button in toolbar
        /// </summary>
        public virtual void OnView() { }
        /// <summary>
        /// Occur on clicking AddRow button in toolbar
        /// </summary>
        public virtual void OnAddRow() { }
        /// <summary>
        /// Occur on clicking DeleteRow button in toolbar
        /// </summary>
        public virtual void OnDeleteRow() { }
        /// <summary>
        /// Occur on clicking Cancel button in toolbar
        /// </summary>
        public virtual void OnCancel() { }
        /// <summary>
        /// Occur on clicking Excel export button in toolbar
        /// </summary>
        /// <param name="saveFileName">Filename has beed chosen in the File Save Dialog</param>
        [Description("saveFileName Not Use")]
        public virtual void OnExcelExport(string saveFileName) { }

        /// <summary>
        /// Occur on clicking Preview button in toolbar
        /// </summary>
        public virtual void OnPrintPreview() { }
        /// <summary>
        /// Occur on clicking Print button in toolbar
        /// </summary>
        public virtual void OnPrint() { }

        #region 20161008 추가
        /// <summary>
        /// [F1] Menual
        /// </summary>
        public virtual void OnHelpMenual() { }
        /// <summary>
        /// [F2] 해당 바운드의 ENTRY로 이동
        /// </summary>
        public virtual void OnMoveInsert() { }
        /// <summary>
        /// [F3] 해당 바운드의 ENTRY로 이동 후 선택한 ROW 항목 조회
        /// </summary>
        public virtual void OnMoveModify() { }
        /// <summary>
        /// 해당 바운드의 ENTRY로 이동 후 선택한 ROW 항목 조회, 수정불가 - 버튼 컨트롤
        /// </summary>
        public virtual void OnMoveViewOnly() { }
        /// <summary>
        /// [F4] 해당 바운드의 ENTRY로 이동 후 선택한 ROW 항목 COPY용 조회
        /// </summary>
        public virtual void OnMoveCopy() { }
        /// <summary>
        /// [F9] INVOICE 현황으로 이동 후 선택한 ROW의 INVOICE 조회
        ///(MASTER 번호가 없더라도 MASTER에 생성한 INVOICE만 조회 되어야 함)
        /// </summary>
        public virtual void OnMoveAccount() { }
        /// <summary>
        /// 현황 이동 조회
        /// </summary>
        public virtual void OnMoveView() { }

        #endregion

        #region 20161029 추가
        /// <summary>
        /// [F10] File upload/download 모듈 호출
        /// </summary>
        public virtual void OnModuleFile() { }
        /// <summary>
        /// [F11] Memo 모듈 호출
        /// </summary>
        public virtual void OnModuleMemo() { }
        #endregion

        #region 20161114 추가

        /// <summary>
        /// [F6] MOVE TO ORDER (AE, AI, OE, OI의 HOUSE/MASTER ENTRY, SEARCH에서만 동작)
        /// </summary>
        public virtual void OnMoveOrderSearch() { }
        /// <summary>
        /// [F7] MOVE TO PARTNER 
        /// </summary>
        public virtual void OnMovePartner() { }
        /// <summary>
        /// [F8] MOVE TO MASTER (AE, AI, OE, OI의HOUSE/ORDER ENTRY, SEARCH에서만 동작) 
        /// </summary>
        public virtual void OnMoveMasterSearch() { }

        #endregion


        public void CloseForm()
        {
            //tbbtnClose_Click(tbbtnClose, EventArgs.Empty);
        }

        #endregion

        #region ToolBarRenderer

        public class ToolBarRenderer : ToolStripRenderer
        {
            private object GetControlPropertyValue(Control control, string propertyName)
            {
                try
                {
                    object controlValue = null;
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    if (pi != null && pi.CanRead)
                    {
                        controlValue = pi.GetValue(control, null);
                    }

                    return controlValue;
                }
                catch
                {
                    return null;
                }
            }

            private FormSettings GetFormSettings(Control control)
            {
                Form f = control.FindForm();
                if (f == null) return new FormSettings(SubSystemType.FRAMEWORK);

                try
                {
                    return (FormSettings)GetControlPropertyValue(f, "formSettings");
                }
                catch (Exception)
                {
                    return new FormSettings(SubSystemType.FRAMEWORK);
                }

            }

            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
            {
                base.OnRenderButtonBackground(e);

                FormSettings formsettings = GetFormSettings(e.Item.Owner);
                Color borderColor = formsettings.ButtonBorderColor;
                Color hoverColor = formsettings.ButtonHoverColor;

                Graphics g = e.Graphics;

                //if the mouse is over the given buttin
                if (e.Item.Selected)
                {
                    //if the mouse has been pressed on the button, 
                    //we want to make the colors a little
                    //darker so the user knows something happened.
                    using (SolidBrush b = new SolidBrush(hoverColor))
                    {
                        g.FillRectangle(b, new Rectangle(3, 3, e.Item.Bounds.Width - 6, e.Item.Bounds.Height - 6));
                    }

                    using (Pen p = new Pen(borderColor, 1))
                    {
                        g.DrawRectangle(p, new Rectangle(2, 2, e.Item.Bounds.Width - 5, e.Item.Bounds.Height - 5));
                    }
                }
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                SolidBrush sb = new SolidBrush((Color)e.ToolStrip.Tag);
                e.Graphics.FillRectangle(sb, e.AffectedBounds);
                base.OnRenderToolStripBackground(e);
            }
        }

        #endregion ToolBarRenderer

        #region FormMove
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (isFloating)
            {
                _dragging = true;  // _dragging is your variable flag
                _start_point = new Point(e.X, e.Y);
            }
        }
        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFloating)
            {
                if (_dragging)
                {
                    Point p = PointToScreen(e.Location);
                    Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
                }
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        #endregion

    }
}
