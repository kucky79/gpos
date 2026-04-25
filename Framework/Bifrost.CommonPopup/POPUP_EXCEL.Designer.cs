namespace NF.A2P.CommonPopup
{
    partial class POPUP_EXCEL
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
            this.radioGroup_FgGroup = new DevExpress.XtraEditors.RadioGroup();
            this.aPanel1 = new NF.Framework.Win.Controls.aPanel();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup_FgGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).BeginInit();
            this.aPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aPanel1);
            this.pnlBound.Size = new System.Drawing.Size(347, 91);
            this.pnlBound.TabIndex = 6;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 179);
            this.pnlBottom.Size = new System.Drawing.Size(387, 46);
            this.pnlBottom.TabIndex = 8;
            // 
            // radioGroup_FgGroup
            // 
            this.radioGroup_FgGroup.Location = new System.Drawing.Point(3, 3);
            this.radioGroup_FgGroup.Name = "radioGroup_FgGroup";
            this.radioGroup_FgGroup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.radioGroup_FgGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup_FgGroup.Properties.Columns = 2;
            this.radioGroup_FgGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("U", "UPLOAD", true, ""),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("D", "DOWNLOAD", true, "")});
            this.radioGroup_FgGroup.Size = new System.Drawing.Size(290, 25);
            this.radioGroup_FgGroup.TabIndex = 7;
            // 
            // aPanel1
            // 
            this.aPanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel1.Appearance.Options.UseBackColor = true;
            this.aPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aPanel1.Controls.Add(this.radioGroup_FgGroup);
            this.aPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aPanel1.Location = new System.Drawing.Point(0, 0);
            this.aPanel1.Name = "aPanel1";
            this.aPanel1.SetPanelType = NF.Framework.Win.Controls.aPanel.PanelType.NONE;
            this.aPanel1.Size = new System.Drawing.Size(347, 91);
            this.aPanel1.TabIndex = 8;
            // 
            // POPUP_EXCEL
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 226);
            this.Name = "POPUP_EXCEL";
            this.Text = "POPUP_EXCEL";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup_FgGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel1)).EndInit();
            this.aPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioGroup_FgGroup;
        private Framework.Win.Controls.aPanel aPanel1;
    }
}