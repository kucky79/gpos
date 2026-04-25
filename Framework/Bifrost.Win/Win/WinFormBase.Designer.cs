using Bifrost.Win.Controls;

namespace Bifrost.Win
{
    partial class WinFormBase
    {

        // <summary>
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


        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinFormBase));
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlInBound = new System.Windows.Forms.Panel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlSubTitle = new System.Windows.Forms.Panel();
            this.lblMenuPath = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.picTitleIcon = new System.Windows.Forms.PictureBox();
            this.pnlBottom.SuspendLayout();
            this.pnlInBound.SuspendLayout();
            this.pnlSubTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitleIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlInBound);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlBottom.Size = new System.Drawing.Size(994, 708);
            this.pnlBottom.TabIndex = 0;
            // 
            // pnlInBound
            // 
            this.pnlInBound.Controls.Add(this.pnlContainer);
            this.pnlInBound.Controls.Add(this.pnlSubTitle);
            this.pnlInBound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInBound.Location = new System.Drawing.Point(12, 10);
            this.pnlInBound.Name = "pnlInBound";
            this.pnlInBound.Size = new System.Drawing.Size(970, 688);
            this.pnlInBound.TabIndex = 1;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 34);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(970, 654);
            this.pnlContainer.TabIndex = 6;
            // 
            // pnlSubTitle
            // 
            this.pnlSubTitle.Controls.Add(this.lblMenuPath);
            this.pnlSubTitle.Controls.Add(this.lblSubTitle);
            this.pnlSubTitle.Controls.Add(this.picTitleIcon);
            this.pnlSubTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlSubTitle.Name = "pnlSubTitle";
            this.pnlSubTitle.Size = new System.Drawing.Size(970, 34);
            this.pnlSubTitle.TabIndex = 2;
            // 
            // lblMenuPath
            // 
            this.lblMenuPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMenuPath.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMenuPath.Location = new System.Drawing.Point(416, 0);
            this.lblMenuPath.Name = "lblMenuPath";
            this.lblMenuPath.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblMenuPath.Size = new System.Drawing.Size(554, 34);
            this.lblMenuPath.TabIndex = 4;
            this.lblMenuPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSubTitle.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 9F, System.Drawing.FontStyle.Regular);
            this.lblSubTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(416, 34);
            this.lblSubTitle.TabIndex = 3;
            this.lblSubTitle.Text = "label1";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picTitleIcon
            // 
            this.picTitleIcon.Location = new System.Drawing.Point(50, 25);
            this.picTitleIcon.Name = "picTitleIcon";
            this.picTitleIcon.Size = new System.Drawing.Size(100, 50);
            this.picTitleIcon.TabIndex = 5;
            this.picTitleIcon.TabStop = false;
            // 
            // WinFormBase
            // 
            this.Appearance.Options.UseFont = true;
            this.ClientSize = new System.Drawing.Size(994, 708);
            this.Controls.Add(this.pnlBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WinFormBase";
            this.Text = "WinFormBase";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinFormBase_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WinFormBase_KeyDown);
            this.pnlBottom.ResumeLayout(false);
            this.pnlInBound.ResumeLayout(false);
            this.pnlSubTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitleIcon)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlBottom;
        protected System.Windows.Forms.Panel pnlContainer;

        #endregion
    }
}