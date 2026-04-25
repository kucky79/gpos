using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

/// Framework 컴포넌

using Infragistics.Win.UltraWinGrid;

namespace NF.Framework.Win
{
    public partial class PrintSettings : FormBase
	{
		public delegate void BeforePrintEventHandler2(UltraGrid gridControl);
		public event BeforePrintEventHandler2 BeforePrint;
		private void RaiseBeforePrint(UltraGrid gridControl)
		{
			if (BeforePrint != null)
			{
				BeforePrint(gridControl);
			}			
		}

		private UltraGrid _GridControl;
		private bool _Preview = false;

		public int PrintScale
		{
			get { return (int)txtPrintScale.Value; }
			set { txtPrintScale.Value = value; }
		}

		public int FromPage
		{
			get { return ParseInt(txtPageFrom.Text); }
			set { txtPageFrom.Text = value.ToString(); }
		}

		public int ToPage
		{
			get { return ParseInt(txtPageTo.Text); }
			set { txtPageTo.Text = value.ToString(); }
		}

		public int Copies
		{
			get { return (int)txtPrintCopies.Value; }
			set { txtPrintCopies.Value = value; }
		}

		/// <summary>
		/// true: 가로
		/// false: 세로
		/// </summary>
		[Description("true: 가로, false: 세로")]
		public bool Landscape
		{
			get { return _GridPrintDocument.DefaultPageSettings.Landscape; }
			set { _GridPrintDocument.DefaultPageSettings.Landscape = value; }
		}
		
		public PrintSettings(UltraGrid gridControl, UltraGridPrintDocument printDocument, bool preview)
		{
			InitializeComponent();

			_GridControl = gridControl;
			_GridPrintDocument = printDocument;
			_Preview = preview;

			ultraPrintPreviewDialog1.Document = printDocument;
			ultraPrintPreviewDialog1.Width = this.MdiForm.Width;
			ultraPrintPreviewDialog1.Height = this.MdiForm.Height;
			ultraPrintPreviewDialog1.Left = (this.Width - ultraPrintPreviewDialog1.Width) / 2 + 20;
			ultraPrintPreviewDialog1.Top = (this.Height - ultraPrintPreviewDialog1.Height)/ 2;

			FormInitialize();
		}

		/// <summary>
		/// 
		/// </summary>
		private void FormInitialize()
		{
            groupBox3.Text = this.ResReader.GetString("R05304");//인쇄 범위
            rbPrintAll.Text = this.ResReader.GetString("R05305");//모두
            rbPrintFrom.Text = this.ResReader.GetString("R05306");//인쇄할 페이지

            groupBox4.Text = this.ResReader.GetString("R05307");//인쇄 매수
            label7.Text = this.ResReader.GetString("R05307");//인쇄 매수

            groupBox5.Text = this.ResReader.GetString("R05308");//배율
            label8.Text = this.ResReader.GetString("R05309");//확대/축소 배율 (%)

            btnOK.Text = this.ResReader.GetString("R04938");//확인
            btnCancel.Text = this.ResReader.GetString("R00010");//취소
            btnPrinterSettings.Text = this.ResReader.GetString("R05310");//페이지 설정

            this.Text = _Preview ? this.ResReader.GetString("R00012") : this.ResReader.GetString("R00013");//Print Preview:Print

			rbPrintAll.Select();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		private UltraGridLayout GetPrintLayout(UltraGrid gridControl, decimal scale)
		{
			UltraGridLayout printLayout = new UltraGridLayout();
			printLayout.CopyFrom(gridControl.DisplayLayout);

			printLayout.Override.HeaderAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.Override.SummaryFooterAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;

			printLayout.Override.GroupByColumnAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.Override.GroupByColumnHeaderAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;

			printLayout.Override.GroupByRowAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;

			printLayout.Override.RowSelectorAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.Override.RowSelectorHeaderAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			
			printLayout.Override.RowAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.Override.RowAlternateAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.Override.SelectedRowAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.Override.ActiveRowAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;

			printLayout.Override.CellAppearance.FontData.SizeInPoints = this.Font.SizeInPoints * (float)scale;
			printLayout.AutoFitStyle = AutoFitStyle.None;

			for (int i = 0; i < printLayout.Bands.Count; i++)
			{
				foreach (UltraGridColumn column in printLayout.Bands[i].Columns)
				{
					column.Width = Convert.ToInt32(column.Width * scale);
					column.RowLayoutColumnInfo.PreferredCellSize = new Size(column.Width, 0);
				}
			}

			return printLayout;
		}


		private int ParseInt(string textValue)
		{
			int intValue = 0;
			Int32.TryParse(textValue, out intValue);
			return intValue;
		}

		private float ParseFloat(string textValue)
		{
			float floatValue = 0;
			float.TryParse(textValue, out floatValue);
			return floatValue;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{	
			_GridPrintDocument.PrinterSettings.Copies = (short)txtPrintCopies.Value;

			if (rbPrintAll.Checked)
			{
				_GridPrintDocument.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
			}
			else
			{
				_GridPrintDocument.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
				_GridPrintDocument.PrinterSettings.FromPage = ParseInt(txtPageFrom.Text);
				_GridPrintDocument.PrinterSettings.ToPage = ParseInt(txtPageTo.Text);
			}

			if (_GridPrintDocument.Grid == null)
			{
				_GridPrintDocument.Grid = _GridControl;
			}

			UltraGridLayout originallayout = new UltraGridLayout();
			originallayout.CopyFrom(_GridControl.DisplayLayout);	
			_GridControl.DisplayLayout.Load(GetPrintLayout(_GridControl, txtPrintScale.Value / 100), PropertyCategories.All);

			/// 
			/// Print하기전에 그리드컨트롤의 속성을 따로 처리 하는 이벤트
			/// 
			RaiseBeforePrint(_GridControl);

			if (_Preview)
			{
				//ultraPrintPreviewDialog1.SetDesktopLocation(this.MdiForm.Left, this.MdiForm.Top);
				ultraPrintPreviewDialog1.ShowDialog(this.MdiForm);				
			}
			else
			{
				_GridPrintDocument.Print();
			}

			_GridControl.DisplayLayout.Load(originallayout, PropertyCategories.All);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{

		}

		private void btnPrinterSettings_Click(object sender, EventArgs e)
		{
			this.pageSetupDialog1.Document = _GridPrintDocument;
			PageSetupDialogHelper.ShowDialog(pageSetupDialog1, this);
		}

		private void rbPrintFrom_CheckedChanged(object sender, EventArgs e)
		{
			if (rbPrintFrom.Checked)
			{
				txtPageFrom.Enabled = txtPageTo.Enabled = true;

				if (txtPageFrom.Text == string.Empty)
					txtPageFrom.Text = "1";
				txtPageFrom.Focus();
			}
		}

		private void rbPrintAll_CheckedChanged(object sender, EventArgs e)
		{
			txtPageFrom.Enabled = txtPageTo.Enabled = false;
		}

		private void txtPageFrom_TextChanged(object sender, EventArgs e)
		{
			if (!rbPrintFrom.Checked)
				rbPrintFrom.Checked = true;
		}

		private void txtPageTo_TextChanged(object sender, EventArgs e)
		{
			if (!rbPrintFrom.Checked)
				rbPrintFrom.Checked = true;
		}

		private void PrintSettings_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}
	}
}