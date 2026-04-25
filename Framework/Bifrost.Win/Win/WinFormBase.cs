using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

using Bifrost.Common;
using Bifrost.Win.Controls;
using Bifrost.Win.Util;
using Bifrost.Data;

namespace Bifrost.Win
{
    #region WinFormBase

    public partial class WinFormBase : FormBase
    {
        #region Public properties

        /// <summary>
        /// Editing behavior, default: EditAction.Edit
        /// Form is in Editing Mode
        /// </summary>
        [Browsable(false)]
        public EditAction EditAction { get; set; } = EditAction.Edit;


        /// <summary>
        /// Form Title
        /// </summary>
        [Browsable(false)]
        public string SubTitle
        {
            get { return lblSubTitle.Text; }
            set { this.Text = lblSubTitle.Text = value; }
        }

        ///// <summary>
        ///// Set this On to validate when form is loading
        ///// </summary>
        //public bool ValidateOnLoad
        //{
        //    set { 
        //        value; }
        //}


        #endregion Public

        #region Initialization & Privates
        //private bool GridSettingPath_Top = false;
        private bool[] _ButtonValidations;
        private string GridSettingPath = string.Empty;
        private Panel pnlInBound;
        private Panel pnlSubTitle;
        private Label lblMenuPath;
        private Label lblSubTitle;
        private PictureBox picTitleIcon;
        private string GridSettingClass = string.Empty;
        /// <summary>
        /// Validation setting of each button in toolbar
        /// </summary>
        protected bool[] ButtonValidations
        {
            get { return _ButtonValidations; }
            set { _ButtonValidations = value; }
        }

        public WinFormBase()
        {
            InitializeComponent();

            ///
            /// Initialization
            ///
            FormInitialize();
        }

        private void FormInitialize()
        {
            ///
            /// Toolbar button init
            /// 
            //tsButtons.CausesValidation = true;
            //tbbtnView.Text = tbbtnView.ToolTipText = this.ResReader.GetString("R00006"); //"조회[F2]"
            //tbbtnInsert.Text = tbbtnInsert.ToolTipText = this.ResReader.GetString("R00004");//"신규[F3]"
            //tbbtnDelete.Text = tbbtnDelete.ToolTipText = this.ResReader.GetString("R00007"); //"삭제[F4]"
            //tbbtnSave.Text = tbbtnSave.ToolTipText = this.ResReader.GetString("R00005"); //"저장[F5]"            
            //tbbtnAddRow.Text = tbbtnAddRow.ToolTipText = this.ResReader.GetString("R00008"); //"행 추가[F6]"
            //tbbtnDeleteRow.Text = tbbtnDeleteRow.ToolTipText = this.ResReader.GetString("R00009"); //"행삭제[F7]"
            //tbbtnCancel.Text = tbbtnCancel.ToolTipText = this.ResReader.GetString("R00010"); //"취소[F8]"
            //tbbtnExcelExport.Text = tbbtnExcelExport.ToolTipText = this.ResReader.GetString("R00011"); //"엑셀변환[F9]"
            //tbbtnPrintPreview.Text = tbbtnPrintPreview.ToolTipText = this.ResReader.GetString("R00012"); //"미리보기[F10]"
            //tbbtnPrint.Text = tbbtnPrint.ToolTipText = this.ResReader.GetString("R00013"); //"출력[F11]"
            //tbbtnClose.Text = tbbtnClose.ToolTipText = this.ResReader.GetString("R00014"); //"닫기"

            /// 
            /// Validation
            /// 
            //_ButtonValidations = new bool[tsButtons.Items.Count];
            //_ButtonValidations[3] = true;

            /// 
            /// MenuData
            /// 
            this.MenuData = new MenuData();


        }

        /// <summary>
        /// Apply Styles to controls on form
        /// </summary>
        /// <param name="c"></param>
        private void ApplyNewSubSystemType(Control c)
        {
            //foreach (Control ctrl in c.Controls)
            //{

            //	if (ctrl is IXControl)
            //	{                    
            //		((IXControl)ctrl).OnSubSystemTypeChanged(this.SubSystemType,false);					
            //	}
            //             if (ctrl.GetType().Name == "XPanel")
            //             {
            //                 if (((XPanel)ctrl).PanelStyle == XPanel.PanelStyles.Header)
            //                     ApplyNewHeaderSubSystemType(ctrl);
            //                 else
            //                     ApplyNewSubSystemType(ctrl);
            //             }
            //             else
            //             {
            //                 ApplyNewSubSystemType(ctrl);
            //             }
            //}
        }

        private void ApplyNewHeaderSubSystemType(Control c)
        {
            //foreach (Control ctrl in c.Controls)
            //{

            //    if (ctrl is IXControl)
            //    {
            //        ((IXControl)ctrl).OnSubSystemTypeChanged(this.SubSystemType,true);
            //    }

            //    ApplyNewHeaderSubSystemType(ctrl);
            //}
        }

        /// <summary>
        /// Change SubSystemType
        /// </summary>
        protected override void OnSubSystemTypeChanged()
        {
            // Right background image & background color for each type of subsystem
            //tsButtons.Tag = formSettings.ToolBarBackColor;

            //tsButtons.Renderer = new ToolBarRenderer();
            pnlBottom.BackColor = formSettings.FormBackColor;
            pnlInBound.BackColor = formSettings.WorkAreaBackColor;
            //pnlInBound.BorderColor = formSettings.BorderColor;

            //switch (this.SubSystemType)
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
            ApplyNewSubSystemType(this);

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
            //             new MsgDialog(System.Windows.FormsType.Warning, new UIException("M04436", null)).ShowDialog(this);
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
            /// CRUD - Toolbar permissions
            /// 
            //tbbtnInsert.Enabled = tbbtnAddRow.Enabled = this.MenuData.Permissions.AllowCreate;
            //tbbtnView.Enabled = this.MenuData.Permissions.AllowRead;
            //tbbtnSave.Enabled = this.MenuData.Permissions.AllowUpdate;
            //tbbtnDelete.Enabled = this.MenuData.Permissions.AllowDelete;
            //tbbtnExcelExport.Enabled = this.MenuData.Permissions.AllowExportExcel;
            //tbbtnPrint.Enabled = tbbtnPrintPreview.Enabled = this.MenuData.Permissions.AllowPrint;

            ///
            /// Apply title
            /// 
            SubTitle = MenuData.MenuName;
            lblMenuPath.Text = MenuData.MenuPath;
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
            if (this.MdiForm == null) return;
            this.MdiForm.OnUpdateStatus(statusText);
        }

        /// <summary>
        /// Support function for excel exporting
        /// </summary>
        private void _OnExcelExport()
        {
            OnExcelExport(string.Empty);
        }

        #endregion Initialization & Privates

        #region Event Handler

        private void WinFormBase_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.F2:
                //    if (tbbtnView.Enabled) tbbtnView_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F3:
                //    if (tbbtnInsert.Enabled) tbbtnInsert_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F4:
                //    if (tbbtnDelete.Enabled) tbbtnDelete_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F5:
                //    if (tbbtnSave.Enabled) tbbtnSave_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F6:
                //    if (tbbtnAddRow.Enabled) tbbtnAddRow_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F7:
                //    if (tbbtnDeleteRow.Enabled) tbbtnDeleteRow_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F8:
                //    if (tbbtnCancel.Enabled) tbbtnCancel_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F9:
                //    if (tbbtnExcelExport.Enabled) tbbtnExcelExport_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F10:
                //    if (tbbtnPrintPreview.Enabled) tbbtnPrintPreview_Click(this, EventArgs.Empty);
                //    break;
                //case Keys.F11:
                //    if (tbbtnPrint.Enabled) tbbtnPrint_Click(this, EventArgs.Empty);
                //    break;
                //default:
                //    break;
            }
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
                //case Keys.F2:
                //    if (tbbtnView.Enabled) tbbtnView_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F3:
                //    if (tbbtnInsert.Enabled) tbbtnInsert_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F4:
                //    if (tbbtnDelete.Enabled) tbbtnDelete_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F5:
                //    if (tbbtnSave.Enabled) tbbtnSave_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F6:
                //    if (tbbtnAddRow.Enabled) tbbtnAddRow_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F7:
                //    if (tbbtnDeleteRow.Enabled) tbbtnDeleteRow_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F8:
                //    if (tbbtnExcelExport.Enabled) tbbtnExcelExport_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F9:
                //    if (tbbtnPrintPreview.Enabled) tbbtnPrintPreview_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F10:
                //    if (tbbtnPrint.Enabled) tbbtnPrint_Click(this, EventArgs.Empty);
                //    return true;
                //case Keys.F11:
                //    tbbtnClose_Click(this, EventArgs.Empty);
                //    return true;
                default:
                    return base.ProcessDialogKey(keyData);
            }
        }

        private void tbbtnClose_Click(object sender, EventArgs e)
        {
            if (!DoValidate(10))
            {
                return;
            }

            this.Close();
        }

        /// <summary>
        /// Enable/Disable a toolbar button
        /// </summary>
        /// <param name="buttonIndex">Button sequence. Starting index is 0</param>
        /// <param name="enable">indicate enable/disable</param>
        protected void EnableToolBarButton(int buttonIndex, bool enable)
        {
            //tsButtons.Items[buttonIndex].Enabled = enable;
        }

        private void WinFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetGridSetting((Control)sender);
        }
        private void SetGridSetting(Control pTarget)
        {
            //if (GridSettingPath == string.Empty)
            //{
            //    GridSettingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(pTarget.GetType()).Location) + "\\DAT\\" ;
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

            //        ((Infragistics.Win.UltraWinGrid.UltraGrid)pTarget).DisplayLayout.SaveAsXml(string.Concat(GridSettingPath,GridSettingClass, pTarget.Name, ".dat"), Infragistics.Win.UltraWinGrid.PropertyCategories.All);
            //    }
            //}
        }

        #endregion Event Handlers

        #region Must Override Methods

        /// <summary>
        /// Occur on clicking Insert button in toolbar
        /// </summary>
        protected virtual void OnInsert() { }
        /// <summary>
        /// Occur on clicking Save button in toolbar
        /// </summary>
        protected virtual void OnSave() { }
        /// <summary>
        /// Occur on clicking Delete button in toolbar
        /// </summary>
        protected virtual void OnDelete() { }
        /// <summary>
        /// Occur on clicking View button in toolbar
        /// </summary>
        protected virtual void OnView() { }
        /// <summary>
        /// Occur on clicking AddRow button in toolbar
        /// </summary>
        protected virtual void OnAddRow() { }
        /// <summary>
        /// Occur on clicking DeleteRow button in toolbar
        /// </summary>
        protected virtual void OnDeleteRow() { }
        /// <summary>
        /// Occur on clicking Cancel button in toolbar
        /// </summary>
        protected virtual void OnCancel() { }
        /// <summary>
        /// Occur on clicking Excel export button in toolbar
        /// </summary>
        /// <param name="saveFileName">Filename has beed chosen in the File Save Dialog</param>
        [Description("saveFileName Not Use")]
        protected virtual void OnExcelExport(string saveFileName) { }

        /// <summary>
        /// Occur on clicking Preview button in toolbar
        /// </summary>
        protected virtual void OnPrintPreview() { }
        /// <summary>
        /// Occur on clicking Print button in toolbar
        /// </summary>
        protected virtual void OnPrint() { }

        protected void CloseForm()
        {
            //tbbtnClose_Click(tbbtnClose, EventArgs.Empty);
        }

        #endregion

        

       
        #region Utility

        private void LogMessage(string msg)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                string logFile = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\Tracking.log");
                if (!System.IO.File.Exists(logFile))
                    System.IO.File.Create(logFile).Close();

                sw = new System.IO.StreamWriter(logFile, true);
                sw.WriteLine(string.Format("[{0:yyyy-MM-dd} {0:HH:mm:ss:ms}] ", DateTime.Now) + msg);
                sw.Flush();
            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }

            }
        }

        #endregion

    }

    #endregion WinFormBase

    #region ToolBarRenderer

    public class ToolBarRenderer : System.Windows.Forms.ToolStripRenderer
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

    #region EditAction

    public enum EditAction
    {
        Edit, Insert
    }

    #endregion EditAction


}
