using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

/// Framework 컴포넌
using NF.Framework.Common;
using NF.Framework.Data;
using NF.Framework.Win.Controls;

using Infragistics.Win.UltraWinGrid;

namespace NF.Framework.Win
{
	partial class PrintSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSettings));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrinterSettings = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPageFrom = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrintScale = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrintCopies = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbPrintFrom = new System.Windows.Forms.RadioButton();
            this.rbPrintAll = new System.Windows.Forms.RadioButton();
            this.txtPageTo = new System.Windows.Forms.TextBox();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this._GridPrintDocument = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(this.components);
            this.ultraPrintPreviewDialog1 = new Infragistics.Win.Printing.UltraPrintPreviewDialog(this.components);
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintScale)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintCopies)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPrinterSettings);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(540, 50);
            this.panel2.TabIndex = 1;
            // 
            // btnPrinterSettings
            // 
            this.btnPrinterSettings.Location = new System.Drawing.Point(439, 12);
            this.btnPrinterSettings.Name = "btnPrinterSettings";
            this.btnPrinterSettings.Size = new System.Drawing.Size(90, 26);
            this.btnPrinterSettings.TabIndex = 2;
            this.btnPrinterSettings.Text = "페이지 설정";
            this.btnPrinterSettings.UseVisualStyleBackColor = true;
            this.btnPrinterSettings.Click += new System.EventHandler(this.btnPrinterSettings_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(343, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(247, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPageFrom
            // 
            this.txtPageFrom.HideSelection = false;
            this.txtPageFrom.Location = new System.Drawing.Point(123, 54);
            this.txtPageFrom.Name = "txtPageFrom";
            this.txtPageFrom.Size = new System.Drawing.Size(74, 21);
            this.txtPageFrom.TabIndex = 2;
            this.txtPageFrom.TextChanged += new System.EventHandler(this.txtPageFrom_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(540, 110);
            this.panel3.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtPrintScale);
            this.groupBox5.Location = new System.Drawing.Point(314, 63);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(218, 41);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "label8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrintScale
            // 
            this.txtPrintScale.Location = new System.Drawing.Point(138, 14);
            this.txtPrintScale.Name = "txtPrintScale";
            this.txtPrintScale.Size = new System.Drawing.Size(74, 21);
            this.txtPrintScale.TabIndex = 0;
            this.txtPrintScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrintScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtPrintCopies);
            this.groupBox4.Location = new System.Drawing.Point(314, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.groupBox4.Size = new System.Drawing.Size(218, 41);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrintCopies
            // 
            this.txtPrintCopies.Location = new System.Drawing.Point(138, 17);
            this.txtPrintCopies.Name = "txtPrintCopies";
            this.txtPrintCopies.Size = new System.Drawing.Size(74, 21);
            this.txtPrintCopies.TabIndex = 0;
            this.txtPrintCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrintCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.rbPrintFrom);
            this.groupBox3.Controls.Add(this.txtPageFrom);
            this.groupBox3.Controls.Add(this.rbPrintAll);
            this.groupBox3.Controls.Add(this.txtPageTo);
            this.groupBox3.Location = new System.Drawing.Point(5, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 96);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(203, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbPrintFrom
            // 
            this.rbPrintFrom.AutoSize = true;
            this.rbPrintFrom.Location = new System.Drawing.Point(15, 59);
            this.rbPrintFrom.Name = "rbPrintFrom";
            this.rbPrintFrom.Size = new System.Drawing.Size(92, 16);
            this.rbPrintFrom.TabIndex = 1;
            this.rbPrintFrom.Text = "radioButton2";
            this.rbPrintFrom.UseVisualStyleBackColor = true;
            this.rbPrintFrom.CheckedChanged += new System.EventHandler(this.rbPrintFrom_CheckedChanged);
            // 
            // rbPrintAll
            // 
            this.rbPrintAll.AutoSize = true;
            this.rbPrintAll.Checked = true;
            this.rbPrintAll.Location = new System.Drawing.Point(15, 23);
            this.rbPrintAll.Name = "rbPrintAll";
            this.rbPrintAll.Size = new System.Drawing.Size(47, 16);
            this.rbPrintAll.TabIndex = 0;
            this.rbPrintAll.TabStop = true;
            this.rbPrintAll.Text = "모두";
            this.rbPrintAll.UseVisualStyleBackColor = true;
            this.rbPrintAll.CheckedChanged += new System.EventHandler(this.rbPrintAll_CheckedChanged);
            // 
            // txtPageTo
            // 
            this.txtPageTo.HideSelection = false;
            this.txtPageTo.Location = new System.Drawing.Point(223, 54);
            this.txtPageTo.Name = "txtPageTo";
            this.txtPageTo.Size = new System.Drawing.Size(74, 21);
            this.txtPageTo.TabIndex = 3;
            this.txtPageTo.TextChanged += new System.EventHandler(this.txtPageTo_TextChanged);
            // 
            // ultraPrintPreviewDialog1
            // 
            this.ultraPrintPreviewDialog1.AutoSize = true;
            this.ultraPrintPreviewDialog1.Document = this._GridPrintDocument;
            this.ultraPrintPreviewDialog1.Name = "ultraPrintPreviewDialog1";
            this.ultraPrintPreviewDialog1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.OfficeXP;
            // 
            // PrintSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
                        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(540, 162);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PrintSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.SubSystemType = NF.Framework.SubSystemType.FRAMEWORK;
            this.Text = "PrintSettings";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintSettings_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintScale)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintCopies)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtPageFrom;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtPageTo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown txtPrintCopies;
		private System.Windows.Forms.Button btnPrinterSettings;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown txtPrintScale;
		private System.Windows.Forms.RadioButton rbPrintFrom;
		private System.Windows.Forms.RadioButton rbPrintAll;
		private PageSetupDialog pageSetupDialog1;
		private UltraGridPrintDocument _GridPrintDocument;
		private Infragistics.Win.Printing.UltraPrintPreviewDialog ultraPrintPreviewDialog1;
	}
}