namespace Bifrost.Win
{
    partial class POSFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSFormBase));
            this.panelTitle = new DevExpress.XtraEditors.PanelControl();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.picTitleIcon = new System.Windows.Forms.PictureBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBtmClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnBtmExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBtmPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnBtmDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnBtmSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnBtmNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnBtmSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).BeginInit();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTitle.Controls.Add(this.lblSubTitle);
            this.panelTitle.Controls.Add(this.picTitleIcon);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1280, 40);
            this.panelTitle.TabIndex = 0;
            this.panelTitle.Visible = false;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubTitle.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 12F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(135)))));
            this.lblSubTitle.Location = new System.Drawing.Point(11, 0);
            this.lblSubTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblSubTitle.Size = new System.Drawing.Size(1269, 40);
            this.lblSubTitle.TabIndex = 2;
            this.lblSubTitle.Text = "Menu Title";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picTitleIcon
            // 
            this.picTitleIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picTitleIcon.Image = ((System.Drawing.Image)(resources.GetObject("picTitleIcon.Image")));
            this.picTitleIcon.Location = new System.Drawing.Point(0, 0);
            this.picTitleIcon.Margin = new System.Windows.Forms.Padding(0);
            this.picTitleIcon.Name = "picTitleIcon";
            this.picTitleIcon.Size = new System.Drawing.Size(11, 40);
            this.picTitleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTitleIcon.TabIndex = 1;
            this.picTitleIcon.TabStop = false;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 40);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1280, 800);
            this.pnlContainer.TabIndex = 3;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBottom.Controls.Add(this.flowLayoutPanel1);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 840);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1280, 60);
            this.pnlBottom.TabIndex = 4;
            this.pnlBottom.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnBtmClose);
            this.flowLayoutPanel1.Controls.Add(this.btnBtmExcel);
            this.flowLayoutPanel1.Controls.Add(this.btnBtmPrint);
            this.flowLayoutPanel1.Controls.Add(this.btnBtmDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnBtmSave);
            this.flowLayoutPanel1.Controls.Add(this.btnBtmNew);
            this.flowLayoutPanel1.Controls.Add(this.btnBtmSearch);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1280, 60);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnBtmClose
            // 
            this.btnBtmClose.AllowFocus = false;
            this.btnBtmClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(81)))), ((int)(((byte)(86)))));
            this.btnBtmClose.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmClose.Appearance.Options.UseBackColor = true;
            this.btnBtmClose.Appearance.Options.UseFont = true;
            this.btnBtmClose.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(37)))), ((int)(((byte)(45)))));
            this.btnBtmClose.AppearanceHovered.Options.UseBackColor = true;
            this.btnBtmClose.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(37)))), ((int)(((byte)(45)))));
            this.btnBtmClose.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmClose.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_close_icon_select;
            this.btnBtmClose.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmClose.Location = new System.Drawing.Point(1133, 3);
            this.btnBtmClose.Name = "btnBtmClose";
            this.btnBtmClose.Size = new System.Drawing.Size(144, 54);
            this.btnBtmClose.TabIndex = 12;
            this.btnBtmClose.Text = "닫기";
            // 
            // btnBtmExcel
            // 
            this.btnBtmExcel.AllowFocus = false;
            this.btnBtmExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(114)))), ((int)(((byte)(66)))));
            this.btnBtmExcel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmExcel.Appearance.Options.UseBackColor = true;
            this.btnBtmExcel.Appearance.Options.UseFont = true;
            this.btnBtmExcel.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(85)))), ((int)(((byte)(62)))));
            this.btnBtmExcel.AppearanceHovered.Options.UseBackColor = true;
            this.btnBtmExcel.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(85)))), ((int)(((byte)(62)))));
            this.btnBtmExcel.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmExcel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmExcel.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.excel_icon;
            this.btnBtmExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmExcel.Location = new System.Drawing.Point(983, 3);
            this.btnBtmExcel.Name = "btnBtmExcel";
            this.btnBtmExcel.Size = new System.Drawing.Size(144, 54);
            this.btnBtmExcel.TabIndex = 11;
            this.btnBtmExcel.Text = "엑셀";
            // 
            // btnBtmPrint
            // 
            this.btnBtmPrint.AllowFocus = false;
            this.btnBtmPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.btnBtmPrint.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmPrint.Appearance.Options.UseBackColor = true;
            this.btnBtmPrint.Appearance.Options.UseFont = true;
            this.btnBtmPrint.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.btnBtmPrint.AppearanceHovered.Options.UseBackColor = true;
            this.btnBtmPrint.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.btnBtmPrint.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmPrint.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.print_icon;
            this.btnBtmPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmPrint.Location = new System.Drawing.Point(833, 3);
            this.btnBtmPrint.Name = "btnBtmPrint";
            this.btnBtmPrint.Size = new System.Drawing.Size(144, 54);
            this.btnBtmPrint.TabIndex = 10;
            this.btnBtmPrint.Text = "출력";
            // 
            // btnBtmDelete
            // 
            this.btnBtmDelete.AllowFocus = false;
            this.btnBtmDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(233)))));
            this.btnBtmDelete.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmDelete.Appearance.Options.UseBackColor = true;
            this.btnBtmDelete.Appearance.Options.UseFont = true;
            this.btnBtmDelete.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnBtmDelete.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmDelete.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_trash_icon_normal;
            this.btnBtmDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmDelete.Location = new System.Drawing.Point(683, 3);
            this.btnBtmDelete.Name = "btnBtmDelete";
            this.btnBtmDelete.Size = new System.Drawing.Size(144, 54);
            this.btnBtmDelete.TabIndex = 9;
            this.btnBtmDelete.Text = "삭제";
            // 
            // btnBtmSave
            // 
            this.btnBtmSave.AllowFocus = false;
            this.btnBtmSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(233)))));
            this.btnBtmSave.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmSave.Appearance.Options.UseBackColor = true;
            this.btnBtmSave.Appearance.Options.UseFont = true;
            this.btnBtmSave.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnBtmSave.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmSave.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_save_icon;
            this.btnBtmSave.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmSave.Location = new System.Drawing.Point(533, 3);
            this.btnBtmSave.Name = "btnBtmSave";
            this.btnBtmSave.Size = new System.Drawing.Size(144, 54);
            this.btnBtmSave.TabIndex = 8;
            this.btnBtmSave.Text = "저장";
            // 
            // btnBtmNew
            // 
            this.btnBtmNew.AllowFocus = false;
            this.btnBtmNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmNew.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(233)))));
            this.btnBtmNew.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmNew.Appearance.Options.UseBackColor = true;
            this.btnBtmNew.Appearance.Options.UseFont = true;
            this.btnBtmNew.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnBtmNew.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmNew.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.new_icon_3;
            this.btnBtmNew.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmNew.Location = new System.Drawing.Point(383, 3);
            this.btnBtmNew.Name = "btnBtmNew";
            this.btnBtmNew.Size = new System.Drawing.Size(144, 54);
            this.btnBtmNew.TabIndex = 7;
            this.btnBtmNew.Text = "신규";
            // 
            // btnBtmSearch
            // 
            this.btnBtmSearch.AllowFocus = false;
            this.btnBtmSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBtmSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(156)))), ((int)(((byte)(179)))));
            this.btnBtmSearch.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnBtmSearch.Appearance.Options.UseBackColor = true;
            this.btnBtmSearch.Appearance.Options.UseFont = true;
            this.btnBtmSearch.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnBtmSearch.AppearanceHovered.Options.UseBackColor = true;
            this.btnBtmSearch.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnBtmSearch.AppearancePressed.Options.UseBackColor = true;
            this.btnBtmSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnBtmSearch.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.brightsearch_icon_select;
            this.btnBtmSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBtmSearch.Location = new System.Drawing.Point(233, 3);
            this.btnBtmSearch.Name = "btnBtmSearch";
            this.btnBtmSearch.Size = new System.Drawing.Size(144, 54);
            this.btnBtmSearch.TabIndex = 6;
            this.btnBtmSearch.Text = "조회";
            // 
            // POSFormBase
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 900);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panelTitle);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("POSFormBase.IconOptions.Icon")));
            this.Name = "POSFormBase";
            this.Text = "WinBase";
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).EndInit();
            this.panelTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitleIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelTitle;
        private System.Windows.Forms.PictureBox picTitleIcon;
        private System.Windows.Forms.Label lblSubTitle;
        protected System.Windows.Forms.Panel pnlContainer;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnBtmNew;
        private DevExpress.XtraEditors.SimpleButton btnBtmSave;
        private DevExpress.XtraEditors.SimpleButton btnBtmDelete;
        private DevExpress.XtraEditors.SimpleButton btnBtmExcel;
        private DevExpress.XtraEditors.SimpleButton btnBtmPrint;
        private DevExpress.XtraEditors.SimpleButton btnBtmClose;
        private DevExpress.XtraEditors.SimpleButton btnBtmSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}