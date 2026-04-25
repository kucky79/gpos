namespace Bifrost
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
            this.flowLayoutPanelPrint = new System.Windows.Forms.FlowLayoutPanel();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.flowLayoutPanelPrint);
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Padding = new System.Windows.Forms.Padding(50, 20, 50, 20);
            this.panelContainer.Size = new System.Drawing.Size(478, 126);
            this.panelContainer.TabIndex = 0;
            // 
            // flowLayoutPanelPrint
            // 
            this.flowLayoutPanelPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPrint.Location = new System.Drawing.Point(50, 20);
            this.flowLayoutPanelPrint.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelPrint.Name = "flowLayoutPanelPrint";
            this.flowLayoutPanelPrint.Size = new System.Drawing.Size(378, 86);
            this.flowLayoutPanelPrint.TabIndex = 1;
            // 
            // P_POS_PRINT
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 126);
            this.IsTopVisible = false;
            this.Name = "P_POS_PRINT";
            this.Text = "P_POS_PRINT";
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPrint;
    }
}