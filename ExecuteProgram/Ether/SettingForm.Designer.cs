namespace Jarvis
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.tabSetting = new System.Windows.Forms.TabControl();
            this.tabPassword = new System.Windows.Forms.TabPage();
            this.txtPWDReenter = new DevExpress.XtraEditors.TextEdit();
            this.txtPWDNew = new DevExpress.XtraEditors.TextEdit();
            this.txtPWDCurrent = new DevExpress.XtraEditors.TextEdit();
            this.txtUserID01 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.tabUser = new System.Windows.Forms.TabPage();
            this.panelAdmin = new System.Windows.Forms.Panel();
            this.btnAdminCheck = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdminPassword = new System.Windows.Forms.TextBox();
            this.ddlLang = new System.Windows.Forms.ComboBox();
            this.lblLang = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.txtStoreCode = new DevExpress.XtraEditors.TextEdit();
            this.txtStoreName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.lblUpgrade = new DevExpress.XtraEditors.LabelControl();
            this.txtUpgradeServer = new DevExpress.XtraEditors.TextEdit();
            this.labelServerIP = new DevExpress.XtraEditors.LabelControl();
            this.txtDBServer = new DevExpress.XtraEditors.TextEdit();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.tabSetting.SuspendLayout();
            this.tabPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDReenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDCurrent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID01.Properties)).BeginInit();
            this.tabUser.SuspendLayout();
            this.panelAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStoreCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStoreName.Properties)).BeginInit();
            this.tabServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpgradeServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBServer.Properties)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 67);
            this.panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.lblTitle.Location = new System.Drawing.Point(67, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(565, 67);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "프로그램 설정";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit1.EditValue = global::Ether.Properties.Resources.bright_setting_icon_normal;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SvgImageSize = new System.Drawing.Size(30, 30);
            this.pictureEdit1.Size = new System.Drawing.Size(67, 67);
            this.pictureEdit1.TabIndex = 6;
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.tabPassword);
            this.tabSetting.Controls.Add(this.tabUser);
            this.tabSetting.Controls.Add(this.tabServer);
            this.tabSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSetting.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.tabSetting.ItemSize = new System.Drawing.Size(123, 33);
            this.tabSetting.Location = new System.Drawing.Point(0, 67);
            this.tabSetting.Margin = new System.Windows.Forms.Padding(2);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.SelectedIndex = 0;
            this.tabSetting.Size = new System.Drawing.Size(632, 363);
            this.tabSetting.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSetting.TabIndex = 6;
            // 
            // tabPassword
            // 
            this.tabPassword.Controls.Add(this.txtPWDReenter);
            this.tabPassword.Controls.Add(this.txtPWDNew);
            this.tabPassword.Controls.Add(this.txtPWDCurrent);
            this.tabPassword.Controls.Add(this.txtUserID01);
            this.tabPassword.Controls.Add(this.labelControl12);
            this.tabPassword.Controls.Add(this.labelControl13);
            this.tabPassword.Controls.Add(this.labelControl14);
            this.tabPassword.Controls.Add(this.labelControl15);
            this.tabPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.tabPassword.Location = new System.Drawing.Point(4, 37);
            this.tabPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tabPassword.Name = "tabPassword";
            this.tabPassword.Padding = new System.Windows.Forms.Padding(2);
            this.tabPassword.Size = new System.Drawing.Size(624, 322);
            this.tabPassword.TabIndex = 6;
            this.tabPassword.Text = "암호 설정";
            this.tabPassword.UseVisualStyleBackColor = true;
            // 
            // txtPWDReenter
            // 
            this.txtPWDReenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPWDReenter.Location = new System.Drawing.Point(134, 196);
            this.txtPWDReenter.Margin = new System.Windows.Forms.Padding(2);
            this.txtPWDReenter.Name = "txtPWDReenter";
            this.txtPWDReenter.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtPWDReenter.Properties.Appearance.Options.UseFont = true;
            this.txtPWDReenter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPWDReenter.Properties.PasswordChar = '●';
            this.txtPWDReenter.Size = new System.Drawing.Size(468, 34);
            this.txtPWDReenter.TabIndex = 22;
            // 
            // txtPWDNew
            // 
            this.txtPWDNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPWDNew.Location = new System.Drawing.Point(134, 158);
            this.txtPWDNew.Margin = new System.Windows.Forms.Padding(2);
            this.txtPWDNew.Name = "txtPWDNew";
            this.txtPWDNew.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtPWDNew.Properties.Appearance.Options.UseFont = true;
            this.txtPWDNew.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPWDNew.Properties.PasswordChar = '●';
            this.txtPWDNew.Size = new System.Drawing.Size(468, 34);
            this.txtPWDNew.TabIndex = 19;
            // 
            // txtPWDCurrent
            // 
            this.txtPWDCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPWDCurrent.Location = new System.Drawing.Point(134, 120);
            this.txtPWDCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.txtPWDCurrent.Name = "txtPWDCurrent";
            this.txtPWDCurrent.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtPWDCurrent.Properties.Appearance.Options.UseFont = true;
            this.txtPWDCurrent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPWDCurrent.Properties.PasswordChar = '●';
            this.txtPWDCurrent.Size = new System.Drawing.Size(468, 34);
            this.txtPWDCurrent.TabIndex = 16;
            // 
            // txtUserID01
            // 
            this.txtUserID01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserID01.Location = new System.Drawing.Point(134, 82);
            this.txtUserID01.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserID01.Name = "txtUserID01";
            this.txtUserID01.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtUserID01.Properties.Appearance.Options.UseFont = true;
            this.txtUserID01.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUserID01.Size = new System.Drawing.Size(468, 34);
            this.txtUserID01.TabIndex = 13;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.Location = new System.Drawing.Point(18, 85);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(54, 28);
            this.labelControl12.TabIndex = 12;
            this.labelControl12.Text = "아이디";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Appearance.Options.UseForeColor = true;
            this.labelControl13.Location = new System.Drawing.Point(18, 199);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(114, 28);
            this.labelControl13.TabIndex = 21;
            this.labelControl13.Text = "비밀번호 확인";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Appearance.Options.UseForeColor = true;
            this.labelControl14.Location = new System.Drawing.Point(18, 161);
            this.labelControl14.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(90, 28);
            this.labelControl14.TabIndex = 18;
            this.labelControl14.Text = "새비밀번호";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.Location = new System.Drawing.Point(18, 123);
            this.labelControl15.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(72, 28);
            this.labelControl15.TabIndex = 15;
            this.labelControl15.Text = "비밀번호";
            // 
            // tabUser
            // 
            this.tabUser.Controls.Add(this.panelAdmin);
            this.tabUser.Controls.Add(this.ddlLang);
            this.tabUser.Controls.Add(this.lblLang);
            this.tabUser.Controls.Add(this.txtUserName);
            this.tabUser.Controls.Add(this.txtUserId);
            this.tabUser.Controls.Add(this.txtStoreCode);
            this.tabUser.Controls.Add(this.txtStoreName);
            this.tabUser.Controls.Add(this.labelControl6);
            this.tabUser.Controls.Add(this.labelControl7);
            this.tabUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.tabUser.Location = new System.Drawing.Point(4, 37);
            this.tabUser.Margin = new System.Windows.Forms.Padding(2);
            this.tabUser.Name = "tabUser";
            this.tabUser.Padding = new System.Windows.Forms.Padding(2);
            this.tabUser.Size = new System.Drawing.Size(624, 322);
            this.tabUser.TabIndex = 27;
            this.tabUser.Text = "사용자 설정";
            this.tabUser.UseVisualStyleBackColor = true;
            // 
            // panelAdmin
            // 
            this.panelAdmin.Controls.Add(this.btnAdminCheck);
            this.panelAdmin.Controls.Add(this.label1);
            this.panelAdmin.Controls.Add(this.txtAdminPassword);
            this.panelAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdmin.Location = new System.Drawing.Point(2, 2);
            this.panelAdmin.Name = "panelAdmin";
            this.panelAdmin.Size = new System.Drawing.Size(620, 318);
            this.panelAdmin.TabIndex = 40;
            // 
            // btnAdminCheck
            // 
            this.btnAdminCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminCheck.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnAdminCheck.Appearance.Options.UseFont = true;
            this.btnAdminCheck.Location = new System.Drawing.Point(480, 142);
            this.btnAdminCheck.Name = "btnAdminCheck";
            this.btnAdminCheck.Size = new System.Drawing.Size(109, 35);
            this.btnAdminCheck.TabIndex = 8;
            this.btnAdminCheck.Text = "확인";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "관리자암호";
            // 
            // txtAdminPassword
            // 
            this.txtAdminPassword.Location = new System.Drawing.Point(149, 142);
            this.txtAdminPassword.Name = "txtAdminPassword";
            this.txtAdminPassword.PasswordChar = '●';
            this.txtAdminPassword.Size = new System.Drawing.Size(325, 35);
            this.txtAdminPassword.TabIndex = 6;
            // 
            // ddlLang
            // 
            this.ddlLang.BackColor = System.Drawing.Color.White;
            this.ddlLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLang.FormattingEnabled = true;
            this.ddlLang.Location = new System.Drawing.Point(153, 127);
            this.ddlLang.Margin = new System.Windows.Forms.Padding(2);
            this.ddlLang.Name = "ddlLang";
            this.ddlLang.Size = new System.Drawing.Size(181, 36);
            this.ddlLang.TabIndex = 39;
            // 
            // lblLang
            // 
            this.lblLang.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblLang.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblLang.Appearance.Options.UseFont = true;
            this.lblLang.Appearance.Options.UseForeColor = true;
            this.lblLang.Location = new System.Drawing.Point(31, 130);
            this.lblLang.Margin = new System.Windows.Forms.Padding(2);
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size(36, 28);
            this.lblLang.TabIndex = 38;
            this.lblLang.Text = "언어";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(338, 93);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtUserName.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtUserName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.txtUserName.Properties.Appearance.Options.UseBorderColor = true;
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            this.txtUserName.Properties.Appearance.Options.UseForeColor = true;
            this.txtUserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUserName.Size = new System.Drawing.Size(158, 34);
            this.txtUserName.TabIndex = 36;
            // 
            // txtUserId
            // 
            this.txtUserId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserId.Location = new System.Drawing.Point(153, 93);
            this.txtUserId.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUserId.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtUserId.Properties.Appearance.Options.UseBorderColor = true;
            this.txtUserId.Properties.Appearance.Options.UseFont = true;
            this.txtUserId.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUserId.Size = new System.Drawing.Size(181, 34);
            this.txtUserId.TabIndex = 34;
            // 
            // txtStoreCode
            // 
            this.txtStoreCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreCode.Location = new System.Drawing.Point(153, 59);
            this.txtStoreCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtStoreCode.Name = "txtStoreCode";
            this.txtStoreCode.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtStoreCode.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtStoreCode.Properties.Appearance.Options.UseBorderColor = true;
            this.txtStoreCode.Properties.Appearance.Options.UseFont = true;
            this.txtStoreCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtStoreCode.Size = new System.Drawing.Size(181, 34);
            this.txtStoreCode.TabIndex = 29;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreName.Enabled = false;
            this.txtStoreName.Location = new System.Drawing.Point(338, 59);
            this.txtStoreName.Margin = new System.Windows.Forms.Padding(2);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStoreName.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtStoreName.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtStoreName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.txtStoreName.Properties.Appearance.Options.UseBackColor = true;
            this.txtStoreName.Properties.Appearance.Options.UseBorderColor = true;
            this.txtStoreName.Properties.Appearance.Options.UseFont = true;
            this.txtStoreName.Properties.Appearance.Options.UseForeColor = true;
            this.txtStoreName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtStoreName.Size = new System.Drawing.Size(158, 34);
            this.txtStoreName.TabIndex = 31;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(31, 62);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(108, 28);
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "업장코드 / 명";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(31, 96);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(108, 28);
            this.labelControl7.TabIndex = 33;
            this.labelControl7.Text = "아이디 / 이름";
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.lblUpgrade);
            this.tabServer.Controls.Add(this.txtUpgradeServer);
            this.tabServer.Controls.Add(this.labelServerIP);
            this.tabServer.Controls.Add(this.txtDBServer);
            this.tabServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.tabServer.Location = new System.Drawing.Point(4, 37);
            this.tabServer.Margin = new System.Windows.Forms.Padding(2);
            this.tabServer.Name = "tabServer";
            this.tabServer.Padding = new System.Windows.Forms.Padding(2);
            this.tabServer.Size = new System.Drawing.Size(624, 322);
            this.tabServer.TabIndex = 43;
            this.tabServer.Text = "서버 설정";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // lblUpgrade
            // 
            this.lblUpgrade.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.lblUpgrade.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblUpgrade.Appearance.Options.UseFont = true;
            this.lblUpgrade.Appearance.Options.UseForeColor = true;
            this.lblUpgrade.Location = new System.Drawing.Point(12, 42);
            this.lblUpgrade.Margin = new System.Windows.Forms.Padding(2);
            this.lblUpgrade.Name = "lblUpgrade";
            this.lblUpgrade.Size = new System.Drawing.Size(126, 28);
            this.lblUpgrade.TabIndex = 48;
            this.lblUpgrade.Text = "업그레이드서버";
            // 
            // txtUpgradeServer
            // 
            this.txtUpgradeServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpgradeServer.EnterMoveNextControl = true;
            this.txtUpgradeServer.Location = new System.Drawing.Point(154, 39);
            this.txtUpgradeServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpgradeServer.Name = "txtUpgradeServer";
            this.txtUpgradeServer.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUpgradeServer.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtUpgradeServer.Properties.Appearance.Options.UseBorderColor = true;
            this.txtUpgradeServer.Properties.Appearance.Options.UseFont = true;
            this.txtUpgradeServer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUpgradeServer.Size = new System.Drawing.Size(454, 34);
            this.txtUpgradeServer.TabIndex = 49;
            // 
            // labelServerIP
            // 
            this.labelServerIP.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.labelServerIP.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labelServerIP.Appearance.Options.UseFont = true;
            this.labelServerIP.Appearance.Options.UseForeColor = true;
            this.labelServerIP.Location = new System.Drawing.Point(12, 79);
            this.labelServerIP.Margin = new System.Windows.Forms.Padding(2);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(67, 28);
            this.labelServerIP.TabIndex = 52;
            this.labelServerIP.Text = "DB 서버";
            // 
            // txtDBServer
            // 
            this.txtDBServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBServer.EnterMoveNextControl = true;
            this.txtDBServer.Location = new System.Drawing.Point(154, 73);
            this.txtDBServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtDBServer.Name = "txtDBServer";
            this.txtDBServer.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDBServer.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtDBServer.Properties.Appearance.Options.UseBorderColor = true;
            this.txtDBServer.Properties.Appearance.Options.UseFont = true;
            this.txtDBServer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDBServer.Size = new System.Drawing.Size(454, 34);
            this.txtDBServer.TabIndex = 53;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 430);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(2);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(632, 96);
            this.panelBottom.TabIndex = 24;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 18F);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(419, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 60);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 18F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(525, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 60);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 526);
            this.Controls.Add(this.tabSetting);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.Text = "Setting";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.tabSetting.ResumeLayout(false);
            this.tabPassword.ResumeLayout(false);
            this.tabPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDReenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDCurrent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID01.Properties)).EndInit();
            this.tabUser.ResumeLayout(false);
            this.tabUser.PerformLayout();
            this.panelAdmin.ResumeLayout(false);
            this.panelAdmin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStoreCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStoreName.Properties)).EndInit();
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpgradeServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBServer.Properties)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabSetting;
        private System.Windows.Forms.TabPage tabPassword;
        private System.Windows.Forms.Panel panelBottom;
        private DevExpress.XtraEditors.TextEdit txtPWDReenter;
        private DevExpress.XtraEditors.TextEdit txtPWDNew;
        private DevExpress.XtraEditors.TextEdit txtPWDCurrent;
        private DevExpress.XtraEditors.TextEdit txtUserID01;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private System.Windows.Forms.TabPage tabUser;
        private System.Windows.Forms.ComboBox ddlLang;
        private DevExpress.XtraEditors.LabelControl lblLang;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtUserId;
        private DevExpress.XtraEditors.TextEdit txtStoreCode;
        private DevExpress.XtraEditors.TextEdit txtStoreName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.TabPage tabServer;
        private DevExpress.XtraEditors.LabelControl lblUpgrade;
        private DevExpress.XtraEditors.TextEdit txtUpgradeServer;
        private DevExpress.XtraEditors.LabelControl labelServerIP;
        private DevExpress.XtraEditors.TextEdit txtDBServer;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel panelAdmin;
        private DevExpress.XtraEditors.SimpleButton btnAdminCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdminPassword;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
    }
}