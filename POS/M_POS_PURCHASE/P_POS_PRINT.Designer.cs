namespace POS
{
    partial class P_POS_PRINT
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
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnReceit = new DevExpress.XtraEditors.SimpleButton();
            this.btnWorkSheet = new DevExpress.XtraEditors.SimpleButton();
            this.btnNone = new DevExpress.XtraEditors.SimpleButton();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.btnNone);
            this.panelContainer.Controls.Add(this.btnWorkSheet);
            this.panelContainer.Controls.Add(this.btnReceit);
            this.panelContainer.Controls.Add(this.btnAll);
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Size = new System.Drawing.Size(591, 141);
            this.panelContainer.TabIndex = 0;
            // 
            // btnAll
            // 
            this.btnAll.AllowFocus = false;
            this.btnAll.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnAll.Appearance.Options.UseFont = true;
            this.btnAll.Location = new System.Drawing.Point(46, 33);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(120, 80);
            this.btnAll.TabIndex = 1;
            this.btnAll.Tag = "A";
            this.btnAll.Text = "영수증\n + 작업서";
            // 
            // btnReceit
            // 
            this.btnReceit.AllowFocus = false;
            this.btnReceit.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnReceit.Appearance.Options.UseFont = true;
            this.btnReceit.Location = new System.Drawing.Point(172, 33);
            this.btnReceit.Name = "btnReceit";
            this.btnReceit.Size = new System.Drawing.Size(120, 80);
            this.btnReceit.TabIndex = 2;
            this.btnReceit.Tag = "R";
            this.btnReceit.Text = "영수증\n출력";
            // 
            // btnWorkSheet
            // 
            this.btnWorkSheet.AllowFocus = false;
            this.btnWorkSheet.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnWorkSheet.Appearance.Options.UseFont = true;
            this.btnWorkSheet.Location = new System.Drawing.Point(298, 33);
            this.btnWorkSheet.Name = "btnWorkSheet";
            this.btnWorkSheet.Size = new System.Drawing.Size(120, 80);
            this.btnWorkSheet.TabIndex = 3;
            this.btnWorkSheet.Tag = "W";
            this.btnWorkSheet.Text = "작업서발행";
            // 
            // btnNone
            // 
            this.btnNone.AllowFocus = false;
            this.btnNone.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.btnNone.Appearance.Options.UseFont = true;
            this.btnNone.Location = new System.Drawing.Point(424, 33);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(120, 80);
            this.btnNone.TabIndex = 4;
            this.btnNone.Tag = "N";
            this.btnNone.Text = "미발행";
            // 
            // P_POS_PRINT
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 141);
            this.IsTopVisible = false;
            this.Name = "P_POS_PRINT";
            this.PopupTitle = "출력물 도움창";
            this.Text = "출력물 도움창 - Popup";
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnNone;
        private DevExpress.XtraEditors.SimpleButton btnWorkSheet;
        private DevExpress.XtraEditors.SimpleButton btnReceit;
        private DevExpress.XtraEditors.SimpleButton btnAll;
    }
}