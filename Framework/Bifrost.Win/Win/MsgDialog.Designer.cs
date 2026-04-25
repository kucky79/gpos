namespace Bifrost.Win
{
	partial class MsgDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgDialog));
            this.panMain = new Bifrost.Win.Controls.aPanel();
            this.llblShowDetails = new System.Windows.Forms.LinkLabel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblMessage = new Bifrost.Win.Controls.aLabel();
            this.btnOKYes = new DevExpress.XtraEditors.SimpleButton();
            this.btnYesNo = new DevExpress.XtraEditors.SimpleButton();
            this.btnNoCancel = new DevExpress.XtraEditors.SimpleButton();
            this.MsgImage = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MsgImage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.MsgImage);
            this.panMain.Controls.Add(this.btnOKYes);
            this.panMain.Controls.Add(this.btnYesNo);
            this.panMain.Controls.Add(this.btnNoCancel);
            this.panMain.Controls.Add(this.llblShowDetails);
            this.panMain.Controls.Add(this.lblTitle);
            this.panMain.Controls.Add(this.btnClose);
            this.panMain.Controls.Add(this.lblMessage);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.SetPanelType = Bifrost.Win.Controls.aPanel.PanelType.NONE;
            this.panMain.Size = new System.Drawing.Size(450, 450);
            this.panMain.TabIndex = 0;
            // 
            // llblShowDetails
            // 
            this.llblShowDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llblShowDetails.AutoSize = true;
            this.llblShowDetails.Location = new System.Drawing.Point(10, 427);
            this.llblShowDetails.Name = "llblShowDetails";
            this.llblShowDetails.Size = new System.Drawing.Size(75, 17);
            this.llblShowDetails.TabIndex = 10;
            this.llblShowDetails.TabStop = true;
            this.llblShowDetails.Text = "ÀÚ¼¼È÷ º¸±â...";
            this.llblShowDetails.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Ä«ÀÌ°Õ°íµñ KR Regular", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Location = new System.Drawing.Point(0, 210);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(450, 34);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Subject";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_close_icon_normal;
            this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(17, 17);
            this.btnClose.Location = new System.Drawing.Point(384, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClose.Size = new System.Drawing.Size(54, 54);
            this.btnClose.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.Appearance.Font = new System.Drawing.Font("Ä«ÀÌ°Õ°íµñ KR Regular", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Appearance.Options.UseTextOptions = true;
            this.lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMessage.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.lblMessage.Location = new System.Drawing.Point(0, 250);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(450, 112);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "Msg";
            // 
            // btnOKYes
            // 
            this.btnOKYes.AllowFocus = false;
            this.btnOKYes.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnOKYes.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnOKYes.Appearance.Font = new System.Drawing.Font("Ä«ÀÌ°Õ°íµñ KR Regular", 16F);
            this.btnOKYes.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnOKYes.Appearance.Options.UseBackColor = true;
            this.btnOKYes.Appearance.Options.UseFont = true;
            this.btnOKYes.Appearance.Options.UseForeColor = true;
            this.btnOKYes.Appearance.Options.UseTextOptions = true;
            this.btnOKYes.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOKYes.Location = new System.Drawing.Point(54, 368);
            this.btnOKYes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnOKYes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOKYes.Name = "btnOKYes";
            this.btnOKYes.Size = new System.Drawing.Size(110, 55);
            this.btnOKYes.TabIndex = 11;
            this.btnOKYes.Text = "¿¹";
            // 
            // btnYesNo
            // 
            this.btnYesNo.AllowFocus = false;
            this.btnYesNo.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnYesNo.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnYesNo.Appearance.Font = new System.Drawing.Font("Ä«ÀÌ°Õ°íµñ KR Regular", 16F);
            this.btnYesNo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnYesNo.Appearance.Options.UseBackColor = true;
            this.btnYesNo.Appearance.Options.UseFont = true;
            this.btnYesNo.Appearance.Options.UseForeColor = true;
            this.btnYesNo.Appearance.Options.UseTextOptions = true;
            this.btnYesNo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnYesNo.Location = new System.Drawing.Point(170, 368);
            this.btnYesNo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnYesNo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnYesNo.Name = "btnYesNo";
            this.btnYesNo.Size = new System.Drawing.Size(110, 55);
            this.btnYesNo.TabIndex = 12;
            this.btnYesNo.Text = "¾Æ´Ï¿À";
            // 
            // btnNoCancel
            // 
            this.btnNoCancel.AllowFocus = false;
            this.btnNoCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnNoCancel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnNoCancel.Appearance.Font = new System.Drawing.Font("Ä«ÀÌ°Õ°íµñ KR Regular", 16F);
            this.btnNoCancel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnNoCancel.Appearance.Options.UseBackColor = true;
            this.btnNoCancel.Appearance.Options.UseFont = true;
            this.btnNoCancel.Appearance.Options.UseForeColor = true;
            this.btnNoCancel.Appearance.Options.UseTextOptions = true;
            this.btnNoCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNoCancel.Location = new System.Drawing.Point(286, 368);
            this.btnNoCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNoCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNoCancel.Name = "btnNoCancel";
            this.btnNoCancel.Size = new System.Drawing.Size(110, 55);
            this.btnNoCancel.TabIndex = 13;
            this.btnNoCancel.Text = "Ãë¼Ò";
            // 
            // MsgImage
            // 
            this.MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
            this.MsgImage.Location = new System.Drawing.Point(170, 88);
            this.MsgImage.Name = "MsgImage";
            this.MsgImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.MsgImage.Properties.Appearance.Options.UseBackColor = true;
            this.MsgImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MsgImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.MsgImage.Properties.SvgImageSize = new System.Drawing.Size(116, 116);
            this.MsgImage.Size = new System.Drawing.Size(116, 116);
            this.MsgImage.TabIndex = 14;
            // 
            // MsgDialog
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.panMain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MsgDialog_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MsgImage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.aPanel panMain;
        private System.Windows.Forms.LinkLabel llblShowDetails;
        private Controls.aLabel lblMessage;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PictureEdit MsgImage;
        private DevExpress.XtraEditors.SimpleButton btnOKYes;
        private DevExpress.XtraEditors.SimpleButton btnYesNo;
        private DevExpress.XtraEditors.SimpleButton btnNoCancel;
    }
}