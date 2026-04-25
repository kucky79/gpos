namespace Bifrost.Win
{
    partial class DialogBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogBase));
            this.pnlContainer = new Bifrost.Win.Controls.aPanel();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            //this.pnlContainer.BorderColor = System.Drawing.Color.Empty;
            //this.pnlContainer.BorderWidth = new System.Windows.Forms.Padding(0);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(5, 5);
            this.pnlContainer.Name = "pnlContainer";
            //this.pnlContainer.PanelStyle = Bifrost.Win.Controls.aPanel.PanelStyles.Nomal;
            this.pnlContainer.Size = new System.Drawing.Size(649, 481);
            this.pnlContainer.TabIndex = 0;
            // 
            // DialogBase
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(659, 491);
            this.Controls.Add(this.pnlContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DialogBase";
            this.ResumeLayout(false);

        }

        #endregion

        protected Bifrost.Win.Controls.aPanel pnlContainer;





    }
}