namespace POS
{
    partial class P_POS_PRINT_PAY
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
            this.flowLayoutPanelPrint = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNone = new DevExpress.XtraEditors.SimpleButton();
            this.btnReceit = new DevExpress.XtraEditors.SimpleButton();
            this.panelContainer.SuspendLayout();
            this.flowLayoutPanelPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.flowLayoutPanelPrint);
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Padding = new System.Windows.Forms.Padding(50, 20, 50, 20);
            this.panelContainer.Size = new System.Drawing.Size(352, 126);
            this.panelContainer.TabIndex = 4;
            // 
            // flowLayoutPanelPrint
            // 
            this.flowLayoutPanelPrint.Controls.Add(this.btnReceit);
            this.flowLayoutPanelPrint.Controls.Add(this.btnNone);
            this.flowLayoutPanelPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPrint.Location = new System.Drawing.Point(50, 20);
            this.flowLayoutPanelPrint.Name = "flowLayoutPanelPrint";
            this.flowLayoutPanelPrint.Size = new System.Drawing.Size(252, 86);
            this.flowLayoutPanelPrint.TabIndex = 7;
            // 
            // btnNone
            // 
            this.btnNone.AllowFocus = false;
            this.btnNone.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.btnNone.Appearance.Options.UseFont = true;
            this.btnNone.Location = new System.Drawing.Point(129, 3);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(120, 80);
            this.btnNone.TabIndex = 8;
            this.btnNone.Tag = "N";
            this.btnNone.Text = "미발행";
            // 
            // btnReceit
            // 
            this.btnReceit.AllowFocus = false;
            this.btnReceit.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 20F);
            this.btnReceit.Appearance.Options.UseFont = true;
            this.btnReceit.Location = new System.Drawing.Point(3, 3);
            this.btnReceit.Name = "btnReceit";
            this.btnReceit.Size = new System.Drawing.Size(120, 80);
            this.btnReceit.TabIndex = 7;
            this.btnReceit.Tag = "P";
            this.btnReceit.Text = "입금표 발행";
            // 
            // P_POS_PRINT_PAY
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 126);
            this.IsTopVisible = false;
            this.Name = "P_POS_PRINT_PAY";
            this.PopupTitle = "출력물 도움창";
            this.Text = "출력물 도움창 - Popup";
            this.panelContainer.ResumeLayout(false);
            this.flowLayoutPanelPrint.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPrint;
        private DevExpress.XtraEditors.SimpleButton btnNone;
        private DevExpress.XtraEditors.SimpleButton btnReceit;
    }
}