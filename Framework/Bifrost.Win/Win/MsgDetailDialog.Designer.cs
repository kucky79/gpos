namespace Bifrost.Win
{
    partial class MsgDetailDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgDetailDialog));
            this.txtDetail = new Bifrost.Win.Controls.aMemoEdit();
            this.panelMaion = new DevExpress.XtraEditors.PanelControl();
            this.llblShowDetails = new System.Windows.Forms.LinkLabel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblMessage = new Bifrost.Win.Controls.aLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOKYes = new DevExpress.XtraEditors.SimpleButton();
            this.btnYesNo = new DevExpress.XtraEditors.SimpleButton();
            this.btnNoCancel = new DevExpress.XtraEditors.SimpleButton();
            this.MsgImage = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMaion)).BeginInit();
            this.panelMaion.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MsgImage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDetail
            // 
            this.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetail.isUpper = true;
            this.txtDetail.Location = new System.Drawing.Point(1, 1);
            this.txtDetail.Modified = false;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDetail.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtDetail.Properties.Appearance.Options.UseBackColor = true;
            this.txtDetail.Properties.Appearance.Options.UseBorderColor = true;
            this.txtDetail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDetail.Properties.ReadOnly = true;
            this.txtDetail.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetail.Properties.WordWrap = false;
            this.txtDetail.SetScrollbar = System.Windows.Forms.ScrollBars.Both;
            this.txtDetail.Size = new System.Drawing.Size(448, 448);
            this.txtDetail.TabIndex = 12;
            this.txtDetail.Visible = false;
            // 
            // panelMaion
            // 
            this.panelMaion.Controls.Add(this.MsgImage);
            this.panelMaion.Controls.Add(this.btnOKYes);
            this.panelMaion.Controls.Add(this.btnYesNo);
            this.panelMaion.Controls.Add(this.btnNoCancel);
            this.panelMaion.Controls.Add(this.llblShowDetails);
            this.panelMaion.Controls.Add(this.lblTitle);
            this.panelMaion.Controls.Add(this.btnClose);
            this.panelMaion.Controls.Add(this.lblMessage);
            this.panelMaion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaion.Location = new System.Drawing.Point(1, 1);
            this.panelMaion.Name = "panelMaion";
            this.panelMaion.Size = new System.Drawing.Size(448, 448);
            this.panelMaion.TabIndex = 1;
            // 
            // llblShowDetails
            // 
            this.llblShowDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llblShowDetails.AutoSize = true;
            this.llblShowDetails.Location = new System.Drawing.Point(7, 428);
            this.llblShowDetails.Name = "llblShowDetails";
            this.llblShowDetails.Size = new System.Drawing.Size(75, 17);
            this.llblShowDetails.TabIndex = 11;
            this.llblShowDetails.TabStop = true;
            this.llblShowDetails.Text = "자세히 보기...";
            this.llblShowDetails.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Location = new System.Drawing.Point(-1, 210);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(450, 34);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Subject";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_close_icon_normal;
            this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(17, 17);
            this.btnClose.Location = new System.Drawing.Point(383, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClose.Size = new System.Drawing.Size(54, 54);
            this.btnClose.TabIndex = 2;
            // 
            // lblMessage
            // 
            this.lblMessage.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Appearance.Options.UseTextOptions = true;
            this.lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMessage.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.lblMessage.Location = new System.Drawing.Point(-1, 250);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(450, 114);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "MSG";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelMaion);
            this.panel1.Controls.Add(this.txtDetail);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(450, 450);
            this.panel1.TabIndex = 0;
            // 
            // btnOKYes
            // 
            this.btnOKYes.AllowFocus = false;
            this.btnOKYes.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnOKYes.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnOKYes.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.btnOKYes.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnOKYes.Appearance.Options.UseBackColor = true;
            this.btnOKYes.Appearance.Options.UseFont = true;
            this.btnOKYes.Appearance.Options.UseForeColor = true;
            this.btnOKYes.Appearance.Options.UseTextOptions = true;
            this.btnOKYes.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOKYes.Location = new System.Drawing.Point(53, 370);
            this.btnOKYes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnOKYes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOKYes.Name = "btnOKYes";
            this.btnOKYes.Size = new System.Drawing.Size(110, 55);
            this.btnOKYes.TabIndex = 12;
            this.btnOKYes.Tag = "&OK";
            this.btnOKYes.Text = "예";
            // 
            // btnYesNo
            // 
            this.btnYesNo.AllowFocus = false;
            this.btnYesNo.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnYesNo.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnYesNo.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.btnYesNo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnYesNo.Appearance.Options.UseBackColor = true;
            this.btnYesNo.Appearance.Options.UseFont = true;
            this.btnYesNo.Appearance.Options.UseForeColor = true;
            this.btnYesNo.Appearance.Options.UseTextOptions = true;
            this.btnYesNo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnYesNo.Location = new System.Drawing.Point(169, 370);
            this.btnYesNo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnYesNo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnYesNo.Name = "btnYesNo";
            this.btnYesNo.Size = new System.Drawing.Size(110, 55);
            this.btnYesNo.TabIndex = 13;
            this.btnYesNo.Tag = "&No";
            this.btnYesNo.Text = "아니오";
            // 
            // btnNoCancel
            // 
            this.btnNoCancel.AllowFocus = false;
            this.btnNoCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnNoCancel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnNoCancel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.btnNoCancel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnNoCancel.Appearance.Options.UseBackColor = true;
            this.btnNoCancel.Appearance.Options.UseFont = true;
            this.btnNoCancel.Appearance.Options.UseForeColor = true;
            this.btnNoCancel.Appearance.Options.UseTextOptions = true;
            this.btnNoCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNoCancel.Location = new System.Drawing.Point(285, 370);
            this.btnNoCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNoCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNoCancel.Name = "btnNoCancel";
            this.btnNoCancel.Size = new System.Drawing.Size(110, 55);
            this.btnNoCancel.TabIndex = 14;
            this.btnNoCancel.Tag = "&Cancel";
            this.btnNoCancel.Text = "취소";
            // 
            // MsgImage
            // 
            this.MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
            this.MsgImage.Location = new System.Drawing.Point(169, 88);
            this.MsgImage.Name = "MsgImage";
            this.MsgImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.MsgImage.Properties.Appearance.Options.UseBackColor = true;
            this.MsgImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MsgImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.MsgImage.Properties.SvgImageSize = new System.Drawing.Size(116, 116);
            this.MsgImage.Size = new System.Drawing.Size(116, 116);
            this.MsgImage.TabIndex = 15;
            // 
            // MsgDetailDialog
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgDetailDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MsgDetailDialog_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.txtDetail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMaion)).EndInit();
            this.panelMaion.ResumeLayout(false);
            this.panelMaion.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MsgImage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.aMemoEdit txtDetail;
        private DevExpress.XtraEditors.PanelControl panelMaion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel llblShowDetails;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private Controls.aLabel lblMessage;
        private DevExpress.XtraEditors.PictureEdit MsgImage;
        private DevExpress.XtraEditors.SimpleButton btnOKYes;
        private DevExpress.XtraEditors.SimpleButton btnYesNo;
        private DevExpress.XtraEditors.SimpleButton btnNoCancel;
    }
}