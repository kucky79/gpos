namespace SAL
{
    partial class POPUP_SMS
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POPUP_SMS));
            this.aPanel2 = new Bifrost.Win.Controls.aPanel();
            this.aGrid1 = new Bifrost.Grid.aGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.textBox_msg = new System.Windows.Forms.TextBox();
            this.aTextEdit_send = new Bifrost.Win.Controls.aTextEdit();
            this.buttonEx_send = new NF.Framework.Adv.Controls.ButtonEx();
            this.buttonEx_rsv = new NF.Framework.Adv.Controls.ButtonEx();
            this.buttonEx_reset = new NF.Framework.Adv.Controls.ButtonEx();
            this.textEdit_rcv = new DevExpress.XtraEditors.TextEdit();
            this.pnlBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aPanel2)).BeginInit();
            this.aPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_send.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_rcv.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBound
            // 
            this.pnlBound.Controls.Add(this.aPanel2);
            this.pnlBound.Size = new System.Drawing.Size(294, 521);
            this.pnlBound.TabIndex = 6;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(1, 609);
            this.pnlBottom.Size = new System.Drawing.Size(334, 46);
            this.pnlBottom.TabIndex = 28;
            // 
            // aPanel2
            // 
            this.aPanel2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel2.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.aPanel2.Appearance.Options.UseBackColor = true;
            this.aPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aPanel2.ContentImage = global::Bifrost.CommonPopup.Properties.Resources.sms;
            this.aPanel2.Controls.Add(this.textEdit_rcv);
            this.aPanel2.Controls.Add(this.buttonEx_reset);
            this.aPanel2.Controls.Add(this.buttonEx_rsv);
            this.aPanel2.Controls.Add(this.buttonEx_send);
            this.aPanel2.Controls.Add(this.aTextEdit_send);
            this.aPanel2.Controls.Add(this.aGrid1);
            this.aPanel2.Controls.Add(this.textBox_msg);
            this.aPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aPanel2.Location = new System.Drawing.Point(0, 0);
            this.aPanel2.Name = "aPanel2";
            this.aPanel2.SetPanelType = Bifrost.Win.Controls.aPanel.PanelType.NONE;
            this.aPanel2.Size = new System.Drawing.Size(294, 521);
            this.aPanel2.TabIndex = 7;
            // 
            // aGrid1
            // 
            this.aGrid1.AddNewRowLastColumn = false;
            this.aGrid1.disabledEditingColumns = null;
            this.aGrid1.isSaveLayout = false;
            this.aGrid1.isUpper = false;
            this.aGrid1.LayoutVersion = "";
            this.aGrid1.Location = new System.Drawing.Point(63, 327);
            this.aGrid1.MainView = this.gridView1;
            this.aGrid1.Name = "aGrid1";
            this.aGrid1.SetBindingEvnet = true;
            this.aGrid1.Size = new System.Drawing.Size(167, 76);
            this.aGrid1.TabIndex = 8;
            this.aGrid1.VerifyNotNull = null;
            this.aGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridView1.GridControl = this.aGrid1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PH";
            this.gridColumn1.FieldName = "PH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // textBox_msg
            // 
            this.textBox_msg.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_msg.Location = new System.Drawing.Point(62, 66);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_msg.Size = new System.Drawing.Size(169, 164);
            this.textBox_msg.TabIndex = 25;
            // 
            // aTextEdit_send
            // 
            this.aTextEdit_send.Location = new System.Drawing.Point(131, 409);
            this.aTextEdit_send.Name = "aTextEdit_send";
            this.aTextEdit_send.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.aTextEdit_send.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.aTextEdit_send.Properties.Appearance.Options.UseBackColor = true;
            this.aTextEdit_send.Properties.Appearance.Options.UseBorderColor = true;
            this.aTextEdit_send.Properties.AutoHeight = false;
            this.aTextEdit_send.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.aTextEdit_send.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions2)});
            this.aTextEdit_send.Properties.LookAndFeel.SkinName = "AIMS_SUB";
            this.aTextEdit_send.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.aTextEdit_send.Size = new System.Drawing.Size(102, 24);
            this.aTextEdit_send.TabIndex = 26;
            // 
            // buttonEx_send
            // 
            this.buttonEx_send.BackColor = System.Drawing.Color.Empty;
            this.buttonEx_send.BorderColor = System.Drawing.Color.Empty;
            this.buttonEx_send.ForeColor = System.Drawing.Color.Empty;
            this.buttonEx_send.HoverColor = System.Drawing.Color.Transparent;
            this.buttonEx_send.HoverForeColor = System.Drawing.Color.Empty;
            this.buttonEx_send.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEx_send.Location = new System.Drawing.Point(120, 449);
            this.buttonEx_send.Name = "buttonEx_send";
            this.buttonEx_send.SelectedColor = System.Drawing.Color.LightGray;
            this.buttonEx_send.Size = new System.Drawing.Size(65, 23);
            this.buttonEx_send.TabIndex = 27;
            this.buttonEx_send.Text = "보내기";
            this.buttonEx_send.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonEx_send.UseDefaultImages = true;
            this.buttonEx_send.UseVisualStyleBackColor = true;
            // 
            // buttonEx_rsv
            // 
            this.buttonEx_rsv.BackColor = System.Drawing.Color.Empty;
            this.buttonEx_rsv.BorderColor = System.Drawing.Color.Empty;
            this.buttonEx_rsv.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEx_rsv.ForeColor = System.Drawing.Color.Empty;
            this.buttonEx_rsv.HoverColor = System.Drawing.Color.Transparent;
            this.buttonEx_rsv.HoverForeColor = System.Drawing.Color.Empty;
            this.buttonEx_rsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEx_rsv.Location = new System.Drawing.Point(55, 449);
            this.buttonEx_rsv.Name = "buttonEx_rsv";
            this.buttonEx_rsv.SelectedColor = System.Drawing.Color.LightGray;
            this.buttonEx_rsv.Size = new System.Drawing.Size(63, 23);
            this.buttonEx_rsv.TabIndex = 28;
            this.buttonEx_rsv.Text = "예약전송";
            this.buttonEx_rsv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonEx_rsv.UseDefaultImages = true;
            this.buttonEx_rsv.UseVisualStyleBackColor = true;
            // 
            // buttonEx_reset
            // 
            this.buttonEx_reset.BackColor = System.Drawing.Color.Empty;
            this.buttonEx_reset.BorderColor = System.Drawing.Color.Empty;
            this.buttonEx_reset.ForeColor = System.Drawing.Color.Empty;
            this.buttonEx_reset.HoverColor = System.Drawing.Color.Transparent;
            this.buttonEx_reset.HoverForeColor = System.Drawing.Color.Empty;
            this.buttonEx_reset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEx_reset.Location = new System.Drawing.Point(187, 449);
            this.buttonEx_reset.Name = "buttonEx_reset";
            this.buttonEx_reset.SelectedColor = System.Drawing.Color.LightGray;
            this.buttonEx_reset.Size = new System.Drawing.Size(51, 23);
            this.buttonEx_reset.TabIndex = 29;
            this.buttonEx_reset.Text = "리셋";
            this.buttonEx_reset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonEx_reset.UseDefaultImages = true;
            this.buttonEx_reset.UseVisualStyleBackColor = true;
            // 
            // textEdit_rcv
            // 
            this.textEdit_rcv.Location = new System.Drawing.Point(63, 305);
            this.textEdit_rcv.Name = "textEdit_rcv";
            this.textEdit_rcv.Size = new System.Drawing.Size(142, 22);
            this.textEdit_rcv.TabIndex = 30;
            // 
            // POPUP_SMS
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 656);
            this.Name = "POPUP_SMS";
            this.Text = "POPUP_SMS";
            this.pnlBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.aPanel2)).EndInit();
            this.aPanel2.ResumeLayout(false);
            this.aPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTextEdit_send.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_rcv.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bifrost.Win.Controls.aPanel aPanel2;
        private System.Windows.Forms.TextBox textBox_msg;
        private Bifrost.Grid.aGrid aGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.TextEdit textEdit_rcv;
        private NF.Framework.Adv.Controls.ButtonEx buttonEx_reset;
        private NF.Framework.Adv.Controls.ButtonEx buttonEx_rsv;
        private NF.Framework.Adv.Controls.ButtonEx buttonEx_send;
        private Bifrost.Win.Controls.aTextEdit aTextEdit_send;
    }
}