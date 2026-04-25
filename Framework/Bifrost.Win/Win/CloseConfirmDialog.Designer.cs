using DevExpress.XtraEditors;

namespace Bifrost.Win
{
    partial class CloseConfirmDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseConfirmDialog));
            this.listBoxForm = new System.Windows.Forms.ListBox();
            this.panelMain = new DevExpress.XtraEditors.PanelControl();
            this.btnYes = new DevExpress.XtraEditors.SimpleButton();
            this.btnNo = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.MsgImage = new DevExpress.XtraEditors.PictureEdit();
            this.lblMessage = new Bifrost.Win.Controls.aLabel();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MsgImage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxForm
            // 
            this.listBoxForm.BackColor = System.Drawing.Color.White;
            this.listBoxForm.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 9F);
            this.listBoxForm.FormattingEnabled = true;
            this.listBoxForm.ItemHeight = 17;
            this.listBoxForm.Location = new System.Drawing.Point(60, 293);
            this.listBoxForm.Name = "listBoxForm";
            this.listBoxForm.Size = new System.Drawing.Size(325, 38);
            this.listBoxForm.TabIndex = 8;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnYes);
            this.panelMain.Controls.Add(this.btnNo);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.MsgImage);
            this.panelMain.Controls.Add(this.lblMessage);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(1, 1);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(398, 398);
            this.panelMain.TabIndex = 14;
            // 
            // btnYes
            // 
            this.btnYes.AllowFocus = false;
            this.btnYes.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnYes.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnYes.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.btnYes.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnYes.Appearance.Options.UseBackColor = true;
            this.btnYes.Appearance.Options.UseFont = true;
            this.btnYes.Appearance.Options.UseForeColor = true;
            this.btnYes.Appearance.Options.UseTextOptions = true;
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnYes.Location = new System.Drawing.Point(86, 317);
            this.btnYes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnYes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(110, 55);
            this.btnYes.TabIndex = 23;
            this.btnYes.Text = "예";
            // 
            // btnNo
            // 
            this.btnNo.AllowFocus = false;
            this.btnNo.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnNo.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.btnNo.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 16F);
            this.btnNo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.btnNo.Appearance.Options.UseBackColor = true;
            this.btnNo.Appearance.Options.UseFont = true;
            this.btnNo.Appearance.Options.UseForeColor = true;
            this.btnNo.Appearance.Options.UseTextOptions = true;
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnNo.Location = new System.Drawing.Point(202, 317);
            this.btnNo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnNo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(110, 55);
            this.btnNo.TabIndex = 24;
            this.btnNo.Text = "아니오";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(89)))), ((int)(((byte)(65)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Location = new System.Drawing.Point(-1, 210);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(397, 34);
            this.lblTitle.TabIndex = 17;
            this.lblTitle.Text = "로그아웃";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.SvgImage = global::Bifrost.Win.Properties.Resources.bright_close_icon_normal;
            this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(17, 17);
            this.btnClose.Location = new System.Drawing.Point(333, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClose.Size = new System.Drawing.Size(54, 54);
            this.btnClose.TabIndex = 15;
            // 
            // MsgImage
            // 
            this.MsgImage.EditValue = global::Bifrost.Win.Properties.Resources.popup_question_icon;
            this.MsgImage.Location = new System.Drawing.Point(141, 61);
            this.MsgImage.Name = "MsgImage";
            this.MsgImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.MsgImage.Properties.Appearance.Options.UseBackColor = true;
            this.MsgImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MsgImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.MsgImage.Properties.SvgImageSize = new System.Drawing.Size(116, 116);
            this.MsgImage.Size = new System.Drawing.Size(116, 116);
            this.MsgImage.TabIndex = 16;
            // 
            // lblMessage
            // 
            this.lblMessage.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMessage.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(86)))), ((int)(((byte)(90)))));
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Appearance.Options.UseForeColor = true;
            this.lblMessage.Appearance.Options.UseTextOptions = true;
            this.lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblMessage.LabelType = Bifrost.Win.Controls.LabelType.NONE;
            this.lblMessage.Location = new System.Drawing.Point(-1, 250);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(400, 32);
            this.lblMessage.TabIndex = 18;
            this.lblMessage.Text = "MSG";
            // 
            // CloseConfirmDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.listBoxForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloseConfirmDialog";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.CloseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MsgImage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxForm;
        private PanelControl panelMain;
        private SimpleButton btnYes;
        private SimpleButton btnNo;
        private LabelControl lblTitle;
        private SimpleButton btnClose;
        private PictureEdit MsgImage;
        private Controls.aLabel lblMessage;
    }
}