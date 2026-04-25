using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Text;


using NF.Framework.Common;
using NF.A2P.Data;
using NF.Framework.Win.Controls;
using NF.Framework.Win.Validation;
using NF.Framework.Win.Util;
using NF.Framework.Win.Data;
using NF.A2P.Grid;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.InteropServices;
using NF.A2P;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;

namespace NF.Framework.Win
{
    public partial class MessengerForm : FormBase
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

        private bool _isSearchData = false;
        [Browsable(false)]
        public bool isSearchData
        {
            get { return _isSearchData; }
            set { _isSearchData = value; }
        }
        #endregion Public

        #region Initialization & Privates

        private FormValidator formValidator1 = new FormValidator();
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

        public MessengerForm()
        {
            //강제 ime 영문
            this.ImeMode = ImeMode.Off;
            InitializeComponent();
            FormInitialize();
            EventInitialize();
        }

        

        private void EventInitialize()
        {
            lblTitle.MouseDown += Form1_MouseDown;
            lblTitle.MouseMove += Form1_MouseMove;
            lblTitle.MouseUp += panelTop_MouseUp;

            btnMin.Click += BtnMin_Click;
            btnClose.Click += BtnClose_Click;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        static Control[] GetAllControlsUsingRecursive(Control containerControl)
        {
            List<Control> allControls = new List<Control>();

            foreach (Control control in containerControl.Controls)
            {
                //자식 컨트롤을 컬렉션에 추가한다
                allControls.Add(control);
                //만일 자식 컨트롤이 또 다른 자식 컨트롤을 가지고 있다면…
                if (control.Controls.Count > 0)
                {
                    //자신을 재귀적으로 호출한다
                    allControls.AddRange(GetAllControlsUsingRecursive(control));
                }
            }
            //모든 컨트롤을 반환한다
            return allControls.ToArray();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        private void FormInitialize()
        {
            /// 
            /// MenuData
            /// 
            MenuData = new MenuData();


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

            DevExpress.Utils.AppearanceObject.DefaultFont = new  System.Drawing.Font("Arial", 9F);


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
            //    if (ctrl.GetType().Name == "XPanel")
            //    {
            //        if (((XPanel)ctrl).PanelStyle == XPanel.PanelStyles.Header)
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
            formValidator1.Validate();

            if (!formValidator1.IsValid || !IsValidated)
            {
                new MsgDialog(MessageType.Warning, new UIException("M04436", null)).ShowDialog(this);
            }

            return formValidator1.IsValid && IsValidated;
        }

        /// <summary>
        /// On Change MenuData
        /// </summary>
        protected override void OnMenuDataChanged()
        {
            base.OnMenuDataChanged();

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

        #endregion Initialization & Privates

        #region Event Handler

        #region Button Click Event
        private void tbbtnInsert_Click(object sender, EventArgs e)
        {
            if (!DoValidate(1))
            {
                return;
            }

            try
            {
                
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnInsert();
            }
            catch { }
            finally
            {
                // End progressing
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnSave_Click(object sender, EventArgs e)
        {
            if (!DoValidate(3))
            {
                return;
            }

            try
            {
                
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnSave();
            }
            catch { }
            finally
            {
                // End progressing
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnView_Click(object sender, EventArgs e)
        {
            if (!DoValidate(0))
            {
                return;
            }

            try
            {
                
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnView();
            }
            catch { }
            finally
            {
                // End progressing
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnDelete_Click(object sender, EventArgs e)
        {
            if (!DoValidate(2))
            {
                return;
            }

            try
            {
                
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnDelete();
            }
            catch { }
            finally
            {
                // End progressing
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnAddRow_Click(object sender, EventArgs e)
        {
            OnAddRow();
        }

        private void tbbtnDeleteRow_Click(object sender, EventArgs e)
        {
            OnDeleteRow();
        }

        private void tbbtnCancel_Click(object sender, EventArgs e)
        {
            if (!DoValidate(6))
            {
                return;
            }

            try
            {
                
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnCancel();

            }
            catch { }
            finally
            {
                // End progressing
                MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                
                //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                // DoTask
                OnExcelExport(string.Empty);
            }
            catch { }
            finally
            {
                // End progressing
                //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnPrintPreview_Click(object sender, EventArgs e)
        {
            if (!DoValidate(8))
            {
                return;
            }

            try
            {
                
                //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnPrintPreview();

            }
            catch { }
            finally
            {
                // End progressing
                //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        private void tbbtnPrint_Click(object sender, EventArgs e)
        {
            if (!DoValidate(9))
            {
                return;
            }

            try
            {
                
                //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);

                
                OnPrint();
            }
            catch { }
            finally
            {
                // End progressing
                //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, false);
            }
        }

        #region 단축키 설정
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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
                    OnMoveInsert();
                    return true;
                case Keys.F3:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    OnMoveModify();
                    return true;
                case Keys.F4:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveViewOnly();
                    OnMoveCopy();
                    return true;
                case Keys.F5:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    OnView();
                    return true;
                case Keys.F6:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveCopy();
                    OnMoveOrderSearch();
                    return true;
                case Keys.F7:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveAccount();
                    OnMovePartner();
                    return true;
                case Keys.F8:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnMoveView();
                    OnMoveMasterSearch();
                    return true;
                case Keys.F9:
                    OnMoveAccount();
                    return true;
                case Keys.F10:
                    OnModuleFile();
                    return true;
                case Keys.F11:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    //OnDelete();
                    OnModuleMemo();
                    return true;
                case Keys.F12:
                    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                    OnSave();
                    return true;

                //case Keys.Alt | Keys.D1:
                //    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                //    OnModuleMemo();
                //    return true;
                //case Keys.Alt | Keys.D2:
                //    //MdiForm.OnStartProcessing(MethodInfo.GetCurrentMethod().Name, true);
                //    OnModuleFile();
                //    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

        }

        #endregion

        private void tbbtnClose_Click(object sender, EventArgs e)
        {
            if (!DoValidate(10))
            {
                return;
            }

            Close();
        }

        #endregion

        private void WinFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetGridSetting((Control)sender);
        }
        private void SetGridSetting(Control pTarget)
        {
            
            //if (GridSettingPath == string.Empty)
            //{
            //    GridSettingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(pTarget.GetType()).Location) + "\\DAT\\";
            //    GridSettingClass = pTarget.GetType().FullName + ".";
            //}
            //if (pTarget.HasChildren)
            //{
            //    foreach (Control ctl in pTarget.Controls)
            //    {
            //        SetGridSetting(ctl);
            //    }
            //}
            //else
            //{
            //    if (pTarget.GetType().FullName == "Infragistics.Win.UltraWinGrid.UltraGrid")
            //    {
            //        if (!System.IO.Directory.Exists(GridSettingPath))
            //            System.IO.Directory.CreateDirectory(GridSettingPath);

            //        ((Infragistics.Win.UltraWinGrid.UltraGrid)pTarget).DisplayLayout.SaveAsXml(string.Concat(GridSettingPath, GridSettingClass, pTarget.Name, ".dat"), Infragistics.Win.UltraWinGrid.PropertyCategories.All);
            //    }
            //}
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

        #region borderless Form
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            //SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(255, 72, 57, 149));

            //e.Graphics.FillRectangle(brush, Top);
            //e.Graphics.FillRectangle(brush, Left);
            //e.Graphics.FillRectangle(brush, Right);
            //e.Graphics.FillRectangle(brush, Bottom);
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 1; // you can rename this variable if you like

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
        #endregion

    }
}
