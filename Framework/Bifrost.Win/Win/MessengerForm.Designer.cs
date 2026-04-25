namespace NF.Framework.Win
{
    partial class MessengerForm
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
            this.panelTop = new NF.Framework.Win.Controls.XPanel();
            this.btnSetting = new NF.Framework.Win.Controls.aButtonSizeFree();
            this.btnChat = new NF.Framework.Win.Controls.aButtonSizeFree();
            this.btnOrg = new NF.Framework.Win.Controls.aButtonSizeFree();
            this.btnLogo = new NF.Framework.Win.Controls.aButtonSizeFree();
            this.btnMin = new NF.Framework.Win.Controls.aButtonSizeFree();
            this.btnClose = new NF.Framework.Win.Controls.aButtonSizeFree();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(225)))));
            this.panelTop.BorderWidth = new System.Windows.Forms.Padding(0);
            this.panelTop.Controls.Add(this.btnSetting);
            this.panelTop.Controls.Add(this.btnChat);
            this.panelTop.Controls.Add(this.btnOrg);
            this.panelTop.Controls.Add(this.btnLogo);
            this.panelTop.Controls.Add(this.btnMin);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.PanelStyle = NF.Framework.Win.Controls.XPanel.PanelStyles.Nomal;
            this.panelTop.Size = new System.Drawing.Size(360, 55);
            this.panelTop.TabIndex = 7;
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnSetting.BorderColor = System.Drawing.Color.Transparent;
            this.btnSetting.ForeColor = System.Drawing.Color.Black;
            this.btnSetting.GroupKey = null;
            this.btnSetting.HoverColor = System.Drawing.Color.Transparent;
            this.btnSetting.HoverForeColor = System.Drawing.Color.Empty;
            this.btnSetting.HoverImage = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Setting_HOver;
            this.btnSetting.Image = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Setting_Default;
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSetting.Location = new System.Drawing.Point(96, 10);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Selectable = true;
            this.btnSetting.Selected = false;
            this.btnSetting.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnSetting.Size = new System.Drawing.Size(36, 36);
            this.btnSetting.TabIndex = 7;
            this.btnSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSetting.UnSelectOtherButtons = false;
            this.btnSetting.UseDefaultImages = true;
            // 
            // btnChat
            // 
            this.btnChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnChat.BorderColor = System.Drawing.Color.Transparent;
            this.btnChat.ForeColor = System.Drawing.Color.Black;
            this.btnChat.GroupKey = null;
            this.btnChat.HoverColor = System.Drawing.Color.Transparent;
            this.btnChat.HoverForeColor = System.Drawing.Color.Empty;
            this.btnChat.HoverImage = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_ChatRoom_HOver;
            this.btnChat.Image = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_ChatRoom_Default;
            this.btnChat.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnChat.Location = new System.Drawing.Point(54, 10);
            this.btnChat.Name = "btnChat";
            this.btnChat.Selectable = true;
            this.btnChat.Selected = false;
            this.btnChat.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnChat.Size = new System.Drawing.Size(36, 36);
            this.btnChat.TabIndex = 6;
            this.btnChat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnChat.UnSelectOtherButtons = false;
            this.btnChat.UseDefaultImages = true;
            // 
            // btnOrg
            // 
            this.btnOrg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnOrg.BorderColor = System.Drawing.Color.Transparent;
            this.btnOrg.ForeColor = System.Drawing.Color.Black;
            this.btnOrg.GroupKey = null;
            this.btnOrg.HoverColor = System.Drawing.Color.Transparent;
            this.btnOrg.HoverForeColor = System.Drawing.Color.Empty;
            this.btnOrg.HoverImage = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Org_HOver;
            this.btnOrg.Image = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Org_Default;
            this.btnOrg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOrg.Location = new System.Drawing.Point(12, 10);
            this.btnOrg.Name = "btnOrg";
            this.btnOrg.Selectable = true;
            this.btnOrg.Selected = false;
            this.btnOrg.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnOrg.Size = new System.Drawing.Size(36, 36);
            this.btnOrg.TabIndex = 5;
            this.btnOrg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOrg.UnSelectOtherButtons = false;
            this.btnOrg.UseDefaultImages = true;
            // 
            // btnLogo
            // 
            this.btnLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnLogo.BorderColor = System.Drawing.Color.Transparent;
            this.btnLogo.ForeColor = System.Drawing.Color.Black;
            this.btnLogo.GroupKey = null;
            this.btnLogo.HoverColor = System.Drawing.Color.Transparent;
            this.btnLogo.HoverForeColor = System.Drawing.Color.Empty;
            this.btnLogo.Image = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_LOGO;
            this.btnLogo.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogo.Location = new System.Drawing.Point(193, 10);
            this.btnLogo.Name = "btnLogo";
            this.btnLogo.Selected = false;
            this.btnLogo.SelectedColor = System.Drawing.Color.LightGray;
            this.btnLogo.Size = new System.Drawing.Size(106, 10);
            this.btnLogo.TabIndex = 4;
            this.btnLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogo.UnSelectOtherButtons = false;
            this.btnLogo.UseDefaultImages = true;
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnMin.BorderColor = System.Drawing.Color.Transparent;
            this.btnMin.ForeColor = System.Drawing.Color.Black;
            this.btnMin.GroupKey = null;
            this.btnMin.HoverColor = System.Drawing.Color.Transparent;
            this.btnMin.HoverForeColor = System.Drawing.Color.Empty;
            this.btnMin.HoverImage = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Min_HOver;
            this.btnMin.Image = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Min_Default;
            this.btnMin.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMin.Location = new System.Drawing.Point(315, 10);
            this.btnMin.Name = "btnMin";
            this.btnMin.Selected = false;
            this.btnMin.SelectedColor = System.Drawing.Color.LightGray;
            this.btnMin.Size = new System.Drawing.Size(13, 13);
            this.btnMin.TabIndex = 3;
            this.btnMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMin.UnSelectOtherButtons = false;
            this.btnMin.UseDefaultImages = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.btnClose.BorderColor = System.Drawing.Color.Transparent;
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.GroupKey = null;
            this.btnClose.HoverColor = System.Drawing.Color.Transparent;
            this.btnClose.HoverForeColor = System.Drawing.Color.Empty;
            this.btnClose.HoverImage = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Close_HOver;
            this.btnClose.Image = global::NF.Framework.Win.Properties.Resources.Messenger_TOP_Close_Default;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.Location = new System.Drawing.Point(335, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Selected = false;
            this.btnClose.SelectedColor = System.Drawing.Color.LightGray;
            this.btnClose.Size = new System.Drawing.Size(13, 13);
            this.btnClose.TabIndex = 2;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.UnSelectOtherButtons = false;
            this.btnClose.UseDefaultImages = true;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(172)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(360, 55);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MessengerForm
            // 
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
                        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(360, 500);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(360, 500);
            this.Name = "MessengerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BorderlessForm";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.XPanel panelTop;
        private Controls.aButtonSizeFree btnMin;
        private Controls.aButtonSizeFree btnClose;
        private System.Windows.Forms.Label lblTitle;
        private Controls.aButtonSizeFree btnLogo;
        private Controls.aButtonSizeFree btnSetting;
        private Controls.aButtonSizeFree btnChat;
        private Controls.aButtonSizeFree btnOrg;
    }
}