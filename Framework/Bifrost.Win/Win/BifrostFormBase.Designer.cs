namespace Bifrost.Win
{
    partial class BifrostFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BifrostFormBase));
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlInBound = new Bifrost.Win.Controls.aPanel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.panelNavigation = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.pnlSubTitle = new Bifrost.Win.Controls.aPanel();
            this.lblSubTitle = new DevExpress.XtraEditors.LabelControl();
            this.picTitleIcon = new DevExpress.XtraEditors.PictureEdit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInBound)).BeginInit();
            this.pnlInBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelNavigation)).BeginInit();
            this.panelNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSubTitle)).BeginInit();
            this.pnlSubTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitleIcon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottom.Controls.Add(this.pnlInBound);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.ForeColor = System.Drawing.Color.Transparent;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(5);
            this.pnlBottom.Size = new System.Drawing.Size(1190, 795);
            this.pnlBottom.TabIndex = 0;
            // 
            // pnlInBound
            // 
            this.pnlInBound.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlInBound.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.pnlInBound.Appearance.Options.UseBackColor = true;
            this.pnlInBound.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlInBound.Controls.Add(this.pnlContainer);
            this.pnlInBound.Controls.Add(this.panelNavigation);
            this.pnlInBound.Controls.Add(this.pnlSubTitle);
            this.pnlInBound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInBound.Location = new System.Drawing.Point(5, 5);
            this.pnlInBound.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInBound.Name = "pnlInBound";
            this.pnlInBound.Padding = new System.Windows.Forms.Padding(1);
            this.pnlInBound.SetPanelType = Bifrost.Win.Controls.aPanel.PanelType.NONE;
            this.pnlInBound.Size = new System.Drawing.Size(1180, 785);
            this.pnlInBound.TabIndex = 1;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.ForeColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Location = new System.Drawing.Point(1, 86);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1178, 698);
            this.pnlContainer.TabIndex = 15;
            // 
            // panelNavigation
            // 
            this.panelNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelNavigation.Controls.Add(this.btnExit);
            this.panelNavigation.Controls.Add(this.btnPrint);
            this.panelNavigation.Controls.Add(this.btnExcel);
            this.panelNavigation.Controls.Add(this.btnSave);
            this.panelNavigation.Controls.Add(this.btnDelete);
            this.panelNavigation.Controls.Add(this.btnNew);
            this.panelNavigation.Controls.Add(this.btnSearch);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavigation.Location = new System.Drawing.Point(1, 31);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(1178, 55);
            this.panelNavigation.TabIndex = 7;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnExit.AllowFocus = false;
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExit.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnExit.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnExit.Appearance.Options.UseBackColor = true;
            this.btnExit.Appearance.Options.UseBorderColor = true;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExit.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.close_icon;
            this.btnExit.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnExit.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnExit.Location = new System.Drawing.Point(727, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnExit.Size = new System.Drawing.Size(55, 55);
            this.btnExit.TabIndex = 14;
            this.btnExit.ToolTip = "닫기 (Ctrl + W)";
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrint.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnPrint.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnPrint.Appearance.Options.UseBackColor = true;
            this.btnPrint.Appearance.Options.UseBorderColor = true;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrint.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.print_icon;
            this.btnPrint.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnPrint.Location = new System.Drawing.Point(672, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnPrint.Size = new System.Drawing.Size(55, 55);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.ToolTip = "프린트";
            // 
            // btnExcel
            // 
            this.btnExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnExcel.AllowFocus = false;
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExcel.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExcel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.excel_icon;
            this.btnExcel.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnExcel.Location = new System.Drawing.Point(617, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnExcel.Size = new System.Drawing.Size(55, 55);
            this.btnExcel.TabIndex = 12;
            this.btnExcel.ToolTip = "엑셀";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnSave.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseBorderColor = true;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSave.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_save_icon;
            this.btnSave.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnSave.Location = new System.Drawing.Point(562, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnSave.Size = new System.Drawing.Size(55, 55);
            this.btnSave.TabIndex = 11;
            this.btnSave.ToolTip = "저장 (Ctrl + S)";
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnDelete.AllowFocus = false;
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnDelete.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnDelete.Appearance.Options.UseBackColor = true;
            this.btnDelete.Appearance.Options.UseBorderColor = true;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDelete.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_trash_icon_normal;
            this.btnDelete.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnDelete.Location = new System.Drawing.Point(507, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnDelete.Size = new System.Drawing.Size(55, 55);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.ToolTip = "삭제";
            // 
            // btnNew
            // 
            this.btnNew.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnNew.AllowFocus = false;
            this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNew.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnNew.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnNew.Appearance.Options.UseBackColor = true;
            this.btnNew.Appearance.Options.UseBorderColor = true;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNew.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.new_icon_4;
            this.btnNew.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnNew.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnNew.Location = new System.Drawing.Point(452, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnNew.Size = new System.Drawing.Size(55, 55);
            this.btnNew.TabIndex = 9;
            this.btnNew.ToolTip = "신규";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnSearch.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnSearch.Appearance.Options.UseBackColor = true;
            this.btnSearch.Appearance.Options.UseBorderColor = true;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSearch.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.brightsearch_icon_select;
            this.btnSearch.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnSearch.Location = new System.Drawing.Point(397, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnSearch.Size = new System.Drawing.Size(55, 55);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.ToolTip = "조회 (F5)";
            // 
            // pnlSubTitle
            // 
            this.pnlSubTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlSubTitle.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.pnlSubTitle.Appearance.Options.UseBackColor = true;
            this.pnlSubTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlSubTitle.Controls.Add(this.lblSubTitle);
            this.pnlSubTitle.Controls.Add(this.picTitleIcon);
            this.pnlSubTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubTitle.Location = new System.Drawing.Point(1, 1);
            this.pnlSubTitle.Name = "pnlSubTitle";
            this.pnlSubTitle.SetPanelType = Bifrost.Win.Controls.aPanel.PanelType.NONE;
            this.pnlSubTitle.Size = new System.Drawing.Size(1178, 30);
            this.pnlSubTitle.TabIndex = 2;
            this.pnlSubTitle.Visible = false;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 13F);
            this.lblSubTitle.Appearance.Options.UseFont = true;
            this.lblSubTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSubTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubTitle.Location = new System.Drawing.Point(22, 0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(1156, 30);
            this.lblSubTitle.TabIndex = 6;
            this.lblSubTitle.Text = "Title";
            // 
            // picTitleIcon
            // 
            this.picTitleIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picTitleIcon.EditValue = global::Bifrost.Win.Properties.Resources.bright_page_next_icon_normal;
            this.picTitleIcon.Location = new System.Drawing.Point(0, 0);
            this.picTitleIcon.Name = "picTitleIcon";
            this.picTitleIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitleIcon.Properties.Appearance.Options.UseBackColor = true;
            this.picTitleIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitleIcon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitleIcon.Properties.SvgImageSize = new System.Drawing.Size(20, 20);
            this.picTitleIcon.Size = new System.Drawing.Size(22, 30);
            this.picTitleIcon.TabIndex = 3;
            // 
            // BifrostFormBase
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1190, 795);
            this.Controls.Add(this.pnlBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BifrostFormBase";
            this.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Hidden;
            this.Text = "Title";
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlInBound)).EndInit();
            this.pnlInBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelNavigation)).EndInit();
            this.panelNavigation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSubTitle)).EndInit();
            this.pnlSubTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitleIcon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private Controls.aPanel pnlInBound;
        protected System.Windows.Forms.Panel pnlContainer;
        private Controls.aPanel pnlSubTitle;
        private DevExpress.XtraEditors.PictureEdit picTitleIcon;
        private DevExpress.XtraEditors.LabelControl lblSubTitle;
        private DevExpress.XtraEditors.PanelControl panelNavigation;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}