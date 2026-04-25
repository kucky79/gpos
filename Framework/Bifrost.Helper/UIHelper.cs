using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

using System.Data;

namespace NF.A2P.Helper
{
    #region UIHelper

    public class UIHelper
	{
        #region DataBinding

        /// <summary>
        /// Binding master-detail combobox, listbox, dropdownbox
        /// </summary>
        /// <param name="dataSource">DataSet contains 2 datatable</param>
        /// <param name="mstTBName">Master DataTable Name, example: ds.Tables[0].TableName</param>
        /// <param name="dtlTBName">Master DataTable Name, example: ds.Tables[1].TableName</param>
        /// <param name="mstColumnName">Master DataTableî¼?Column to link, CustomerID</param>
        /// <param name="dtlColumnName">Detail DataTableî¼?Column to link, CustomerID</param>
        /// <param name="mstLstCtrl">Master ListControl, combobox or listbox is fine</param>
        /// <param name="mstLstCtrlDisplayMember">DisplayMember for Master List Control</param>
        /// <param name="mstLstCtrlValueMember">ValueMember for Master List Control</param>
        /// <param name="dtlLstCtrl">Detail ListControl, combobox or listbox is fine</param>
        /// <param name="dtlLstCtrlDisplayMember">DisplayMember for Master List Control</param>
        /// <param name="dtlLstCtrlValueMember">ValueMember for Master List Control</param>
        public static void BindMasterDetail(DataSet dataSource,
            string mstTBName, string dtlTBName,
            string mstColumnName, string dtlColumnName,
            ListControl mstLstCtrl, string mstLstCtrlDisplayMember, string mstLstCtrlValueMember,
            ListControl dtlLstCtrl, string dtlLstCtrlDisplayMember, string dtlLstCtrlValueMember)
        {
            DataRelation dr = new DataRelation("master_detail",
                dataSource.Tables[mstTBName].Columns[mstColumnName],
                dataSource.Tables[dtlTBName].Columns["dtlColumnName"]);
            dataSource.Relations.Add(dr);

            mstLstCtrl.DataSource = dataSource;
            mstLstCtrl.DisplayMember = string.Format("{0}.{1}", mstTBName, mstLstCtrlDisplayMember);
            mstLstCtrl.ValueMember = string.Format("{0}.{1}", mstTBName, mstLstCtrlValueMember);

            dtlLstCtrl.DataSource = dataSource;
            dtlLstCtrl.DisplayMember = string.Format("{0}.master_detail.{1}", mstTBName, dtlLstCtrlDisplayMember);
            dtlLstCtrl.ValueMember = string.Format("{0}.master_detail.{1}", mstTBName, dtlLstCtrlValueMember);
        }


        /// <summary>
        /// DataTable   ComboBox Binding
        /// </summary>
        /// <param name="pCboID"> ComboBox ID</param>
        /// <param name="pValue">DisplayMember, ValueMember</param>
        public static void ComboBoxDataBinding(ComboBox pTargetCbx, string[,] pValue)
        {
            DataTable tempDT = new DataTable();
            tempDT.Columns.Add(new DataColumn("Value"));
            tempDT.Columns.Add(new DataColumn("Code"));

            for (int i = 0; i < pValue.GetLength(0); i++)
            {
                string[] tempArr = null;
                for (int j = 0; j < pValue.GetLength(1) - 1; j++)
                {
                    tempArr = new string[pValue.GetLength(1)];
                    tempArr[j] = pValue[i, j];
                    tempArr[j + 1] = pValue[i, j + 1];
                    tempDT.Rows.Add(tempArr);
                }
            }
            // DataBinding
            pTargetCbx.DataSource = tempDT;
            pTargetCbx.DisplayMember = "Value";
            pTargetCbx.ValueMember = "Code";
        }

        /// <summary>
        /// ComboxBox, ListBox databinding
        /// </summary>
        /// <param name="listCtrl">ComboxBox or ListBox</param>
        /// <param name="dataSource"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        public static void ControlDataBinding(ListControl listCtrl, DataTable dataSource, string displayMember, string valueMember)
        {
            listCtrl.DataSource = dataSource;
            listCtrl.DisplayMember = displayMember;
            listCtrl.ValueMember = valueMember;
        }

        /// <summary>
        /// Columns in source and target datarow must be identical.
        /// </summary>
        /// <param name="fromDR"></param>
        /// <param name="toDR"></param>
        public static void CopyDataRow(DataRow fromDR, DataRow toDR)
        {
            for (int i = 0; i < fromDR.Table.Columns.Count; i++)
            {
                toDR[i] = fromDR[i];
            }
        }


        #endregion

        #region Reset Controls

        /// <summary>
        /// Parameter  Form Control  ÃÊ±âÈ­.
        /// 1.TextBox  : Text = ""
        /// 2.CheckBox : Checked = false
        /// 3.ComboBox : SelectedIndex = 0
        /// 4.RadioButton : Checked = false
        /// </summary>
        /// <param name="pTarget">Form Control ID</param>
        public static void ResetControl(Control pTarget)
        {
            if(pTarget.HasChildren)		
            {
                foreach(Control ctl in pTarget.Controls)
                {
                    ResetControl(ctl);
                }
            }
            else
            {
                switch(pTarget.GetType().FullName)
                {
                    case "System.Windows.Forms.TextBox" :
                        ((System.Windows.Forms.TextBox)pTarget).ResetText();
                        break;
                    case "System.Windows.Forms.CheckBox" :
                        ((System.Windows.Forms.CheckBox)pTarget).Checked = false;
                        break;
                    case "System.Windows.Forms.ComboBox":
                        if (((System.Windows.Forms.ComboBox)pTarget).SelectedIndex != -1)
                        {
                            ((System.Windows.Forms.ComboBox)pTarget).SelectedIndex = 0;
                        }
                        break;
                    case "System.Windows.Forms.RadioButton" :
                        ((System.Windows.Forms.RadioButton)pTarget).Checked = false;
                        break;
                    case "Infragistics.Win.UltraWinEditors.UltraDateTimeEditor":
                        ((Infragistics.Win.UltraWinEditors.UltraDateTimeEditor)pTarget).Value = null;
                        break;
                    case "Infragistics.Win.UltraWinEditors.UltraNumericEditor":
                        ((Infragistics.Win.UltraWinEditors.UltraNumericEditor)pTarget).Value = null;
                        break;
                }
            }
			
        }

			
        #endregion

        #region DataTable Type Parameter 

        /// <summary>
        /// DataTable Type Parameter 
        /// </summary>
        /// <param name="pColumnName">ColumnName</param>
        /// <param name="pRowValue">Data( Column,  Row)</param>
        /// <returns> DataTable</returns>
        public static DataTable CreateDataTable(string[] pColumnName, string[,] pRowValue)
        {
            DataTable tempDT = new DataTable();

            // DataColumn 
            for (int i = 0; i < pColumnName.Length; i++)
            {
                tempDT.Columns.Add(new DataColumn(pColumnName[i]));
            }

            // DataRow 
            for (int i = 0; i < pRowValue.GetLength(0); i++)
            {
                DataRow tempDR = tempDT.NewRow();
                for (int j = 0; j < pRowValue.GetLength(1); j++)
                {
                    tempDR[j] = pRowValue[i, j];
                }
                tempDT.Rows.Add(tempDR);
            }

            return tempDT;
        }

        /// <summary>
        /// ComboBox Binding  DataTable 
        /// </summary>
        /// <param name="pCode">Code </param>
        /// <param name="pValue">Value </param>
        /// <returns>Datatble</returns>
        public static DataTable GetComboDataTable(string[] pCode, string[] pValue)
        {
            DataTable oDataTable = null;

            oDataTable = new DataTable();
            DataRow oDataRow = null;

            DataColumn[] oDataColumn = new DataColumn[2];

            oDataColumn[0] = new DataColumn("Code");
            oDataColumn[1] = new DataColumn("Value");

            oDataTable.Columns.AddRange(oDataColumn);

            for (int i = 0; i < pCode.Length; i++)
            {
                oDataRow = oDataTable.NewRow();
                oDataRow["Code"] = pCode[i];
                oDataRow["Value"] = pValue[i];
                oDataTable.Rows.Add(oDataRow);
            }

            return oDataTable;
        }

        #endregion



		#region Internal

		internal static object GetControlPropertyValue(Control control, string propertyName)
		{
			try
			{
				object controlValue = null;
				PropertyInfo pi = control.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (pi != null && pi.CanRead)
				{
					controlValue = pi.GetValue(control, null);
				}

				return controlValue;
			}
			catch
			{
				return null;
			}
		}

		internal static object GetControlPropertyValue(Control control, Type controlType, string propertyName)
		{
			try
			{
				object controlValue = null;
				PropertyInfo pi = controlType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
				if (pi != null && pi.CanRead)
				{
					controlValue = pi.GetValue(control, null);
				}

				return controlValue;
			}
			catch
			{
				return null;
			}
		}

		internal static string GetCurrentSubSystemType(Control control)
		{
			Form f = control.FindForm();
			if (f == null) return "CM";

			try
			{
				return XPropertyConverter.EnumToString(typeof(SubSystemType), 
					UIHelper.GetControlPropertyValue(f, f.GetType().BaseType, "SubSystemType"));
			}
			catch (Exception)
			{				
				return "CM";
			}				
		}

		internal static FormSettings GetFormSettings(Control control)
		{
			Form f = control.FindForm();
			if (f == null) return new FormSettings(SubSystemType.CM);

			FormSettings fs;
			
			try
            {
                fs = (FormSettings)GetControlPropertyValue(f, f.GetType().BaseType, "formSettings");
				
				if (fs == null) {
					string[]  sep = f.GetType().Name.Split('.');
					string subSystem = sep.Length > 1 ? sep[1] : "CM";
					fs = new FormSettings((SubSystemType)XPropertyConverter.EnumFromString(typeof(SubSystemType), subSystem));
				}
			}
			catch
			{
				fs = new FormSettings(SubSystemType.CM);
			}

			return fs;
		}

        internal static decimal ConvertToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        //internal static GlobalSettings GetGlobalSettings(Control control)
        //{
        //    Form f = control.FindForm();
        //    if (f == null) return new GlobalSettings();

        //    GlobalSettings global;

        //    try
        //    {
        //        global = (GlobalSettings)GetControlPropertyValue(f, f.GetType().BaseType, "GlobalSettings");
        //    }
        //    catch
        //    {
        //        global = new GlobalSettings();
        //    }

        //    return global;
        //}	

		#endregion
	}

	#endregion
    
	#region DummyDataCreator

	/// <summary>
	/// Summary description for DummyDataCreator.
	/// </summary>
	public class DummyDataCreator
	{
		#region Private Members
		private static Random rnd = new Random();
		#endregion Pricate Members

		#region Enums
		public enum Element
		{
			Air,
			Earth,
			Fire,
			Water,
			Energy,
		}
		#endregion Enums

		#region GetData

		public static DataTable GetData(int rows, Type[] types)
		{
			DataTable ReturnValue = new DataTable();
			System.Collections.Hashtable typeCounts = new System.Collections.Hashtable();
			DataColumn keyColumn = null;

			keyColumn = ReturnValue.Columns.Add("Key", typeof(int));
			keyColumn.AutoIncrement = true;
			keyColumn.ReadOnly = true;

			foreach (Type type in types)
			{
				if (typeCounts.ContainsKey(type.Name))
					typeCounts[type.Name] = (int)typeCounts[type.Name] + 1;
				else
					typeCounts.Add(type.Name, 1);

				ReturnValue.Columns.Add(type.Name + " " + typeCounts[type.Name].ToString(), type);
			}

			for (int i = 0; i < rows; i++)
			{
				DataRow newRow = ReturnValue.NewRow();
				newRow["Key"] = i;
				foreach (DataColumn dc in ReturnValue.Columns)
				{
					if (dc.DataType == typeof(string))
						newRow[dc.ColumnName] = RandomString();
					else if (dc.DataType == typeof(int)
						&& dc.ColumnName != "Key")
						newRow[dc.ColumnName] = RandomInt();
					else if (dc.DataType == typeof(bool))
						newRow[dc.ColumnName] = RandomBool();
					else if (dc.DataType == typeof(double))
						newRow[dc.ColumnName] = RandomDouble();
					else if (dc.DataType == typeof(Element))
						newRow[dc.ColumnName] = RandomElement();
					else if (dc.DataType == typeof(DateTime))
						newRow[dc.ColumnName] = RandomDate();
					else if (dc.DataType == typeof(decimal))
						newRow[dc.ColumnName] = RandomDecimal();
					else if (dc.DataType == typeof(Color))
						newRow[dc.ColumnName] = RandomColor();
					else if (dc.DataType == typeof(Image))
						newRow[dc.ColumnName] = GetTextBitmap(ReturnValue.Rows.Count.ToString());
				}
				ReturnValue.Rows.Add(newRow);
			}

			ReturnValue.AcceptChanges();
			return ReturnValue;
		}

		public static DataTable GetData(int rows, string[] columnNames, Type[] types)
		{
			DataTable ReturnValue = new DataTable();
			System.Collections.Hashtable typeCounts = new System.Collections.Hashtable();
			for (int i = 0; i < columnNames.Length; i++)
			{
				ReturnValue.Columns.Add(columnNames[i], types[i]);
			}

			for (int i = 0; i < rows; i++)
			{
				DataRow newRow = ReturnValue.NewRow();
				foreach (DataColumn dc in ReturnValue.Columns)
				{
					if (dc.DataType == typeof(string))
						newRow[dc.ColumnName] = RandomString();
					else if (dc.DataType == typeof(int))
						newRow[dc.ColumnName] = RandomInt();
					else if (dc.DataType == typeof(bool))
						newRow[dc.ColumnName] = RandomBool();
					else if (dc.DataType == typeof(double))
						newRow[dc.ColumnName] = RandomDouble();
					else if (dc.DataType == typeof(Element))
						newRow[dc.ColumnName] = RandomElement();
					else if (dc.DataType == typeof(DateTime))
						newRow[dc.ColumnName] = RandomDate();
					else if (dc.DataType == typeof(decimal))
						newRow[dc.ColumnName] = RandomDecimal();
					else if (dc.DataType == typeof(Color))
						newRow[dc.ColumnName] = RandomColor();
					else if (dc.DataType == typeof(Image))
						newRow[dc.ColumnName] = GetTextBitmap(ReturnValue.Rows.Count.ToString());
				}
				ReturnValue.Rows.Add(newRow);
			}

			ReturnValue.AcceptChanges();
			return ReturnValue;
		}



		public static DataTable GetData(int rows)
		{
			System.Type[] types = new Type[] {typeof(string), 
												 typeof(string),
												 typeof(bool), 
												 typeof(bool), 
												 typeof(int), 
												 typeof(int), 
												 typeof(double), 
												 typeof(double), 
												 typeof(decimal), 
												 typeof(decimal), 
												 typeof(DateTime), 
												 typeof(DateTime), 
												 typeof(Element), 
												 typeof(Element),
												 typeof(Image), 
												 typeof(Image),   
											 };
			return GetData(rows, types);
		}


		public static DataTable GetData()
		{
			return GetData(100);
		}


		#endregion GetData

		#region RandomString

		public static string RandomString(int characterCount)
		{
			if (characterCount < 1)
				characterCount = rnd.Next(1, 51);

			System.Text.StringBuilder SB = new System.Text.StringBuilder();

			for (int i = 1; i < characterCount; i++)
			{
				SB.Append(RandomCharacter());
			}

			return SB.ToString();
		}


		public static string RandomString()
		{
			return RandomString(-1);
		}
		#endregion RandomString

		#region RandomCharacter
		public static char RandomCharacter()
		{
			char ReturnValue;
			ReturnValue = (char)(rnd.Next(65, 91));

			if (RandomBool())
				ReturnValue = char.ToLower(ReturnValue);

			return ReturnValue;
		}
		#endregion RandomCharacter

		#region RandomBool
		public static bool RandomBool()
		{

			if (RandomInt(0, 1) == 1)
				return true;

			return false;
		}
		#endregion RandomBool

		#region RandomInt
		public static int RandomInt(int Min, int Max)
		{
			return rnd.Next(Min, Max + 1);
		}
		public static int RandomInt()
		{
			return RandomInt(-100, 100);
		}
		#endregion RandomInt

		#region RandomDouble
		public static Double RandomDouble()
		{
			return rnd.NextDouble() * 100;
		}
		#endregion RandomDouble

		#region RandomDecimal
		public static decimal RandomDecimal()
		{
			return (decimal)rnd.NextDouble() * 100;
		}
		#endregion RandomDecimal

		#region RandomDate
		public static DateTime RandomDate()
		{
			DateTime ReturnValue = System.DateTime.Today;
			ReturnValue = ReturnValue.AddYears(RandomInt());
			ReturnValue = ReturnValue.AddMonths(RandomInt());
			ReturnValue = ReturnValue.AddDays(RandomInt());
			ReturnValue = ReturnValue.AddHours(RandomInt());
			ReturnValue = ReturnValue.AddSeconds(RandomInt());
			ReturnValue = ReturnValue.AddMinutes(RandomInt());
			return ReturnValue;
		}
		#endregion RandomDate

		#region RandomElement
		public static Enum RandomElement()
		{
			return (Element)rnd.Next(0, System.Enum.GetValues(typeof(Element)).Length);
		}
		#endregion RandomElement

		#region RandomColor
		public static Color RandomColor()
		{
			int R, G, B;
			R = RandomInt(0, 255);
			G = RandomInt(0, 255);
			B = RandomInt(0, 255);

			Color ReturnValue = Color.FromArgb(R, G, B);
			return ReturnValue;
		}
		#endregion RandomColor

		#region  Images
		public static Bitmap GetThumnailImage(Bitmap OriginalImage, int NewWidth, int NewHeight)
		{
			return (Bitmap)OriginalImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
		}

		public static Bitmap GetThumnailImage(Bitmap OriginalImage, int MaxExtent)
		{
			float AspectRatio = OriginalImage.Width / OriginalImage.Height;
			float SizeRatio;
			int NewWidth, NewHeight;

			if (AspectRatio == 0)
				return (Bitmap)OriginalImage.GetThumbnailImage(MaxExtent, MaxExtent, null, IntPtr.Zero);
			else if (AspectRatio > 1)
			{
				if (OriginalImage.Width < MaxExtent)
					return (Bitmap)OriginalImage.Clone();

				SizeRatio = MaxExtent / OriginalImage.Width;
			}
			else //AspectRation >1
			{
				if (OriginalImage.Height < MaxExtent)
					return (Bitmap)OriginalImage.Clone();

				SizeRatio = MaxExtent / OriginalImage.Height;
			}

			NewWidth = (int)((float)OriginalImage.Width * SizeRatio);
			NewHeight = (int)((float)OriginalImage.Height * SizeRatio);
			return GetThumnailImage(OriginalImage, NewWidth, NewHeight);
		}

		public enum Shape
		{
			Ellipse,
			Rectangle
		}

		public static Bitmap GetTextBitmap(string Text)
		{
			return GetTextBitmap(Text, Color.Yellow, Color.Black, Color.Black, Shape.Ellipse, -1, -1);
		}
		public static Bitmap GetTextBitmap(string Text, Color BackColor, Color ForeColor)
		{
			return GetTextBitmap(Text, BackColor, ForeColor, ForeColor, Shape.Ellipse, -1, -1);
		}
		public static Bitmap GetTextBitmap(string Text, Color BackColor, Color ForeColor, Color BorderColor)
		{
			return GetTextBitmap(Text, BackColor, ForeColor, BorderColor, Shape.Ellipse, -1, -1);
		}
		public static Bitmap GetTextBitmap(string Text, Color BackColor, Color ForeColor, Shape BorderShape)
		{
			return GetTextBitmap(Text, BackColor, ForeColor, ForeColor, BorderShape, -1, -1);
		}
		public static Bitmap GetTextBitmap(string Text, Color BackColor, Color ForeColor, Color bordercolor, Shape BorderShape)
		{
			return GetTextBitmap(Text, BackColor, ForeColor, bordercolor, BorderShape, -1, -1);
		}
		public static Bitmap GetTextBitmap(string Text, Color BackColor, Color ForeColor, Color OutlineColor, Shape BorderShape, int Height, int Width)
		{
			Bitmap BMP = new Bitmap(1, 1);
			Graphics g = Graphics.FromImage(BMP);
			if (Width == -1 || Height == -1)
			{
				SizeF CalculatedAutoSize;
				CalculatedAutoSize = g.MeasureString(Text, Control.DefaultFont);
				if (Width == -1)
					Width = (int)(CalculatedAutoSize.Width + 3);
				if (Height == -1)
					Height = (int)(CalculatedAutoSize.Height + 1);
			}

			BMP = new Bitmap(Width, Height);
			g = Graphics.FromImage(BMP);
			g.Clear(Color.Transparent);
			Rectangle R = new Rectangle(0, 0, Width - 1, Height - 1);
			System.Drawing.SolidBrush BackColorBrush = new System.Drawing.SolidBrush(BackColor);
			System.Drawing.Pen ForeColorPen = new System.Drawing.Pen(ForeColor);
			System.Drawing.Pen OutlineColorPen = new System.Drawing.Pen(OutlineColor);
			switch (BorderShape)
			{
				case Shape.Ellipse:
					g.FillEllipse(BackColorBrush, R);
					g.DrawEllipse(OutlineColorPen, R);
					break;
				case Shape.Rectangle:
					g.FillRectangle(BackColorBrush, R);
					g.DrawRectangle(OutlineColorPen, R);
					break;
			}

			StringFormat SF = new StringFormat(StringFormatFlags.NoWrap);

			SF.Alignment = StringAlignment.Center;
			SF.LineAlignment = StringAlignment.Center;
			SF.Trimming = StringTrimming.None;

			System.Drawing.SolidBrush ForeColorBrush = new System.Drawing.SolidBrush(ForeColor);

			RectangleF RF = new RectangleF(0, 0, Width, Height);

			g.DrawString(Text, Control.DefaultFont, ForeColorBrush, RF, SF);
			SF.Dispose();
			ForeColorBrush.Dispose();
			BackColorBrush.Dispose();
			ForeColorPen.Dispose();
			OutlineColorPen.Dispose();
			g.Dispose();
			return BMP;
		}
		#endregion
	}

	#endregion
     
}
