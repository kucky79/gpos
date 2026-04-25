using Bifrost.Common;
using Bifrost.Data;
using Bifrost.Helper;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Win
{
    public partial class POSFormBase : FormBase
    {
        #region Public properties
        /// <summary> Editing behavior, default: EditAction.Edit Form is in Editing Mode</summary>
        [Browsable(false)]
        public EditAction EditAction { get; set; } = EditAction.Edit;


        /// <summary>Form Title</summary>
        [Browsable(false)]
        public string SubTitle
        {
            get { return lblSubTitle.Text; }
            set { Text = lblSubTitle.Text = value; }
        }

        private bool _isSubTitle = false;
        [Browsable(true), DefaultValue(typeof(Boolean), "false")]

        public bool IsSubTitle
        {
            get { return _isSubTitle; }
            set 
            { 
                _isSubTitle = value;
                panelTitle.Visible = value;
            }
        }

        private bool _isBottomVisible = false;
        [Browsable(true), DefaultValue(typeof(Boolean), "false")]

        public bool IsBottomVisible
        {
            get { return _isBottomVisible; }
            set
            {
                _isBottomVisible = value;
                pnlBottom.Visible = value;
            }
        }


        [Browsable(false)]
        public bool isSearchData { get; set; } = false;

        //조회
        private bool _visibleBtnSearch = true;
        [Browsable(false)]
        public bool VisibleBtnSearch 
        { 
            get { return _visibleBtnSearch;  }
            set 
            { 
                _visibleBtnSearch = value; 
                btnBtmSearch.Visible = value;
            }
        }

        //신규
        private bool _visibleBtnNew = true;
        [Browsable(false)]
        public bool VisibleBtnNew
        {
            get { return _visibleBtnNew; }
            set
            {
                _visibleBtnNew = value;
                btnBtmNew.Visible = value;
            }
        }

        //저장
        private bool _visibleBtnSave = true;
        [Browsable(false)]
        public bool VisibleBtnSave
        {
            get { return _visibleBtnSave; }
            set
            {
                _visibleBtnSave = value;
                btnBtmSave.Visible = value;
            }
        }

        //삭제
        private bool _visibleBtnDelete = true;
        [Browsable(false)]
        public bool VisibleBtnDelete
        {
            get { return _visibleBtnDelete; }
            set
            {
                _visibleBtnDelete = value;
                btnBtmDelete.Visible = value;
            }
        }


        //자동조회
        [Browsable(false)]
        public bool AutoSearch 
        {
            get;
            set;
        } = false;


        /// <summary>Form Title</summary>
        [Browsable(false)]
        public string PrintPort { get; set; }

        #endregion Public properties

        #region Initialization & Privates

        //private FormValidator formValidator1 = new FormValidator();
        private string GridSettingPath = string.Empty;
        private string GridSettingClass = string.Empty;
        /// <summary>
        /// Validation setting of each button in toolbar
        /// </summary>
        protected bool[] ButtonValidations { get; set; }

        public POSFormBase()
        {
            InitializeComponent();
            FormInitialize();
            EventInitialize();

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
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }


        private void EventInitialize()
        {
            #region FormMove
            lblSubTitle.MouseMove += panelTop_MouseMove;
            lblSubTitle.MouseUp += panelTop_MouseUp;
            lblSubTitle.MouseDown += panelTop_MouseDown;
            lblSubTitle.DoubleClick += PanelTop_DoubleClick;
            #endregion FormMove

            TextChanged += FormBase_TextChanged;
            FormClosed += FormBase_FormClosed;
            Shown += FormBase_Shown; //그리드 사이즈  저장 이벤트
            Activated += POSFormBase_Activated;

            btnBtmClose.Click += BtnBtmClose_Click;
            btnBtmNew.Click += BtnBtmNew_Click;
            btnBtmSave.Click += BtnBtmSave_Click;
            btnBtmExcel.Click += BtnBtmExcel_Click;
            btnBtmDelete.Click += BtnBtmDelete_Click;
            btnBtmPrint.Click += BtnBtmPrint_Click;
            btnBtmSearch.Click += BtnBtmSearch_Click;

            Font FontDefault = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            Color ColorFontDefault = Color.White;

            SetButtonApperance(btnBtmSearch, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault, Color.FromArgb(169, 169, 169), Color.FromArgb(77, 198, 227));
            SetButtonApperance(btnBtmNew, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault,    Color.FromArgb(169, 169, 169), Color.FromArgb(86, 37, 45));
            SetButtonApperance(btnBtmSave, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault,   Color.FromArgb(169, 169, 169), Color.FromArgb(86, 37, 45));
            SetButtonApperance(btnBtmDelete, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault, Color.FromArgb(169, 169, 169), Color.FromArgb(86, 37, 45));
            SetButtonApperance(btnBtmPrint, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault, Color.FromArgb(100, 93, 198), Color.FromArgb(63, 0, 153));
            SetButtonApperance(btnBtmExcel, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault, Color.FromArgb(24, 114, 66), Color.FromArgb(17, 85, 62));
            SetButtonApperance(btnBtmClose, FontDefault, FontDefault, ColorFontDefault, ColorFontDefault, Color.FromArgb(238, 81, 86), Color.FromArgb(86, 37, 45));
        }

        private void POSFormBase_Activated(object sender, EventArgs e)
        {
            //프린터포트
            POSConfig configPrintPort = POSConfigHelper.GetConfig("PRT001");
            PrintPort = configPrintPort.ConfigValue;
        }

        public static void SetButtonApperance(SimpleButton btn, Font defaultFont, Font pressFont, Color defaultFontColor, Color pressFontColor, Color defaultColor, Color pressColor)
        {

            btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            btn.LookAndFeel.UseDefaultLookAndFeel = false;
            btn.Appearance.BackColor = defaultColor;
            btn.Appearance.BorderColor = defaultColor;

            btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;

            btn.AllowFocus = false;
            btn.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;

            //btn.AllowFocus = false;

            //btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            //btn.LookAndFeel.UseDefaultLookAndFeel = false;
            //btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //btn.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;

            //btn.Appearance.Options.UseBorderColor = true;
            //btn.Appearance.BorderColor = Color.AliceBlue;

            //btn.Appearance.BackColor = defaultColor;
            //btn.Appearance.Options.UseBackColor = true;

            btn.Appearance.Options.UseFont = true;
            btn.Appearance.Font = defaultFont;
            btn.Appearance.ForeColor = defaultFontColor;

            //btn.AppearancePressed.BackColor = pressColor;
            //btn.AppearancePressed.Font = pressFont;
            //btn.AppearancePressed.ForeColor = pressFontColor;
            //btn.AppearancePressed.Options.UseBackColor = true;
            //btn.AppearancePressed.Options.UseFont = true;
            //btn.AppearancePressed.Options.UseForeColor = true;
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
            set { _isFloating = value; }
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

            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("카이겐고딕 KR Regular", 9F);
            
            this.Text = string.Empty;
        }

        /// <summary>
        /// Call to validate and display the error dialog
        /// </summary>
        /// <param name="btnIdx"></param>
        /// <returns></returns>
        private bool DoValidate(int btnIdx)
        {
            if (!ButtonValidations[btnIdx])
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
            ButtonValidations[toolbarBtnIndex] = required;
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

        private void FormBase_TextChanged(object sender, EventArgs e)
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

        private void BtnBtmSearch_Click(object sender, EventArgs e)
        {
            OnView();

        }

        private void BtnBtmPrint_Click(object sender, EventArgs e)
        {
            OnPrint();

        }

        private void BtnBtmDelete_Click(object sender, EventArgs e)
        {
            OnDelete();

        }

        private void BtnBtmExcel_Click(object sender, EventArgs e)
        {
            OnExcelExport();
        }

        private void BtnBtmSave_Click(object sender, EventArgs e)
        {
            OnSave();

        }

        private void BtnBtmNew_Click(object sender, EventArgs e)
        {
            OnInsert();

        }

        private void BtnBtmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region 단축키 설정
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    OnHelpMenual();
                    return true;
                case Keys.F2:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //if (btnNew.Enabled) OnMoveInsert();
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
                    return true;
                case Keys.Alt | Keys.D2:
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
                case Keys.Control | Keys.S:
                    OnSave();
                    return true;
                #endregion

                #endregion
                #endregion


                #endregion

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

        }

        #endregion

        private void FormBase_Shown(object sender, EventArgs e)
        {
            panelTitle.Visible = IsSubTitle;

            pnlBottom.Visible = IsBottomVisible;
            //if (GridSettingPath == string.Empty)
            //{
            //    GridSettingPath = Application.StartupPath + @"\GridLayout\";
            //}

            ////20171124 프로그램이 실행될때만 적용되게, 개발 디자인 화면에서는 적용되지 않게 하려고
            //Process[] precessList = Process.GetProcessesByName("AIMS2TEST");
            //if (precessList.Length > 0)
            //    ApplyGridSetting((Control)sender, GridSettingPath);

            if (AutoSearch)
                OnView();
        }

        private void FormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            //20171220 그리드 컬럼 세팅 일단 주석
            //if (GridSettingPath == string.Empty)
            //{
            //    GridSettingPath = Application.StartupPath + @"\GridLayout\";
            //}

            //SetGridSetting((Control)sender);
        }

        /// <summary>
        /// 설정한 폴더에 권한주기
        /// </summary>
        /// <param name="linePath"></param>
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

        /// <summary>
        /// 컨테이너 안에 있는 컨트롤 찾기
        /// </summary>
        /// <param name="containerControl"></param>
        /// <returns></returns>
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

        private void SetGridSetting(Control pTarget)
        {

            //if (GridSettingPath == string.Empty)
            //{
            //    GridSettingPath = Application.StartupPath + @"\GridLayout\";
            //}

            //if (!System.IO.Directory.Exists(GridSettingPath))
            //    System.IO.Directory.CreateDirectory(GridSettingPath);

            //if (pTarget.HasChildren)
            //{
            //    if (pTarget.GetType().FullName == "Bifrost.Grid.aGrid")
            //    {
            //        GridSettingClass = this.GetType().FullName + "_" + pTarget.Name + ".xml";
            //        ((DevExpress.XtraGrid.GridControl)pTarget).MainView.SaveLayoutToXml(GridSettingPath + GridSettingClass);
            //    }

            //    foreach (Control ctl in pTarget.Controls)
            //    {
            //        SetGridSetting(ctl);
            //    }
            //}
        }


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
        /// Occur on clicking Print button in toolbar
        /// </summary>
        public virtual void OnPrint() { }

        public virtual void OnExcelExport() { }

        /// <summary>
        /// [F1] Menual
        /// </summary>
        public virtual void OnHelpMenual() { }

        public virtual void OnHomeClick() { }


        protected override void OnPaint(PaintEventArgs e)
        {

          

            base.OnPaint(e);
        }



        #endregion


        public void CloseForm()
        {
            //tbbtnClose_Click(tbbtnClose, EventArgs.Empty);
        }

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
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }
        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        #endregion
    }
}
