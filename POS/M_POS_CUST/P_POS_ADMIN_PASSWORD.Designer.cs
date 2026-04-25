namespace POS
{
    partial class P_POS_ADMIN_PASSWORD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_POS_ADMIN_PASSWORD));
            this.btnAdminCheck = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdminPassword = new System.Windows.Forms.TextBox();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.btnAdminCheck);
            this.panelContainer.Controls.Add(this.label1);
            this.panelContainer.Controls.Add(this.txtAdminPassword);
            this.panelContainer.Size = new System.Drawing.Size(660, 159);
            // 
            // btnAdminCheck
            // 
            this.btnAdminCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminCheck.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnAdminCheck.Appearance.Options.UseFont = true;
            this.btnAdminCheck.Location = new System.Drawing.Point(495, 62);
            this.btnAdminCheck.Name = "btnAdminCheck";
            this.btnAdminCheck.Size = new System.Drawing.Size(109, 35);
            this.btnAdminCheck.TabIndex = 14;
            this.btnAdminCheck.Text = "확인";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.label1.Location = new System.Drawing.Point(56, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 28);
            this.label1.TabIndex = 13;
            this.label1.Text = "관리자암호";
            // 
            // txtAdminPassword
            // 
            this.txtAdminPassword.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.txtAdminPassword.Location = new System.Drawing.Point(164, 62);
            this.txtAdminPassword.Name = "txtAdminPassword";
            this.txtAdminPassword.PasswordChar = '●';
            this.txtAdminPassword.Size = new System.Drawing.Size(325, 35);
            this.txtAdminPassword.TabIndex = 12;
            // 
            // P_POS_ADMIN_PASSWORD
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 205);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("P_POS_ADMIN_PASSWORD.IconOptions.Icon")));
            this.Name = "P_POS_ADMIN_PASSWORD";
            this.PopupTitle = "암호 입력";
            this.Text = "암호 입력 - Popup";
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAdminCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdminPassword;
    }
}