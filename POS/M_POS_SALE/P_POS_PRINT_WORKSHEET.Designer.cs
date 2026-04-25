namespace POS
{
    partial class P_POS_PRINT_WORKSHEET
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
            this.btnReceit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNone = new DevExpress.XtraEditors.SimpleButton();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.btnNone);
            this.panelContainer.Controls.Add(this.btnReceit);
            this.panelContainer.Size = new System.Drawing.Size(340, 155);
            // 
            // btnReceit
            // 
            this.btnReceit.AllowFocus = false;
            this.btnReceit.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnReceit.Appearance.Options.UseFont = true;
            this.btnReceit.Location = new System.Drawing.Point(46, 36);
            this.btnReceit.Name = "btnReceit";
            this.btnReceit.Size = new System.Drawing.Size(120, 80);
            this.btnReceit.TabIndex = 5;
            this.btnReceit.Tag = "W";
            this.btnReceit.Text = "작업서";
            // 
            // btnNone
            // 
            this.btnNone.AllowFocus = false;
            this.btnNone.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F);
            this.btnNone.Appearance.Options.UseFont = true;
            this.btnNone.Location = new System.Drawing.Point(172, 36);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(120, 80);
            this.btnNone.TabIndex = 6;
            this.btnNone.Tag = "N";
            this.btnNone.Text = "미발행";
            // 
            // P_POS_PRINT_WORKSHEET
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 201);
            this.Name = "P_POS_PRINT_WORKSHEET";
            this.PopupTitle = "출력물 도움창";
            this.Text = "출력물 도움창 - Popup";
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnNone;
        private DevExpress.XtraEditors.SimpleButton btnReceit;
    }
}