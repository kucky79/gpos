namespace POS
{
    partial class P_SORT
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.txtSeq = new Bifrost.Win.Controls.aNumericText();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.txtSeq);
            this.panelContainer.Controls.Add(this.btnCancel);
            this.panelContainer.Controls.Add(this.btnDone);
            this.panelContainer.Size = new System.Drawing.Size(411, 70);
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(292, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 50);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "닫기";
            // 
            // btnDone
            // 
            this.btnDone.AllowFocus = false;
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Regular);
            this.btnDone.Appearance.Options.UseFont = true;
            this.btnDone.Location = new System.Drawing.Point(180, 10);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(106, 50);
            this.btnDone.TabIndex = 36;
            this.btnDone.Text = "확인";
            // 
            // txtSeq
            // 
            this.txtSeq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSeq.DecimalPoint = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSeq.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSeq.EnterMoveNextControl = true;
            this.txtSeq.Location = new System.Drawing.Point(12, 18);
            this.txtSeq.Modified = false;
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", 15F, System.Drawing.FontStyle.Regular);
            this.txtSeq.Properties.Appearance.Options.UseFont = true;
            this.txtSeq.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSeq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSeq.Properties.AutoHeight = false;
            this.txtSeq.Properties.NullText = "0";
            this.txtSeq.Size = new System.Drawing.Size(143, 36);
            this.txtSeq.TabIndex = 38;
            // 
            // P_SORT
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 118);
            this.Name = "P_SORT";
            this.Text = "P_SEARCH_INITIAL";
            this.panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private Bifrost.Win.Controls.aNumericText txtSeq;
    }
}