using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;

using System.Data;
using System.Data.SqlClient;

using NF.Framework.Common;
using NF.A2P.Data;
using NF.Framework.Win;
using NF.Framework.Win.Controls;

using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Documents.Excel;
using Infragistics.Win.Layout;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using Infragistics.Win.UltraWinEditors;

using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;
using ColumnHeader = Infragistics.Win.UltraWinGrid.ColumnHeader;

namespace NF.Framework.Win.Controls
{
	public enum UIElementType
	{
		ColumnHeader, UltraGridRow, UltraGridCell, Other
	}

	public enum UltraGridColumnStyle
	{
        Default, Text, Numeric, Date, Time, Time2, CheckBox, SingleDropDown, SingleColumnDropDown, MultiColumnDropDown, DropDownCalendar, EditPopup, Button
	}

	#region UltraGridHelperColumn & UltraGridHelperColumCollection

	public class UltraGridHelperColumn
	{
        //TODO GlobalSettings을 사용 안하는것으로....
		//protected GlobalSettings global;

		#region Protected

		protected UltraGrid gridControl;
		protected UltraGridColumn columnObject;

		protected void ApplyColumnStyle(UltraGridColumnStyle columnStyle)
		{

			columnObject.MaskDataMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
			columnObject.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;

			switch (columnStyle)
			{
				case UltraGridColumnStyle.Default:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
					break;

				case UltraGridColumnStyle.Text:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
					break;

				case UltraGridColumnStyle.Numeric:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;

					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Right;

					// Format					
					this.DataType = typeof(System.Decimal);
					this.DecimalCount = 2;

					break;
				case UltraGridColumnStyle.Date:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;

					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;

					// Format & Mask
					this.DataType = typeof(System.DateTime);
					this.FormatString = "yyyy-MM-dd";					

					break;
				case UltraGridColumnStyle.Time:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;

					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;
					//columnObject.DataType = typeof(DateTime);

					// Format & Mask
					//this.FormatString = "HH:mm";
					this.MaskInput = "hh:mm";
					columnObject.MaskDataMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;
					break;
                case UltraGridColumnStyle.Time2:
                    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;

                    columnObject.CellAppearance.TextVAlign = VAlign.Middle;
                    columnObject.CellAppearance.TextHAlign = HAlign.Center;
                    //columnObject.DataType = typeof(DateTime);

                    // Format & Mask
                    //this.FormatString = "HH:mm";
                    this.MaskInput = "hh:mm:ss";
                    columnObject.MaskDataMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;
                    break;
				case UltraGridColumnStyle.CheckBox:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;					
					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;
					ShowHeaderCheckBox = true;

					break;

				case UltraGridColumnStyle.SingleDropDown:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;

					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;
					
					break;
				case UltraGridColumnStyle.SingleColumnDropDown:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;

					break;
				case UltraGridColumnStyle.MultiColumnDropDown:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;

					break;
				case UltraGridColumnStyle.DropDownCalendar:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownCalendar;
					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;
					break;

				case UltraGridColumnStyle.EditPopup:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
					columnObject.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;
				
					break;
				case UltraGridColumnStyle.Button:
					columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
					columnObject.CellAppearance.TextVAlign = VAlign.Middle;
					columnObject.CellAppearance.TextHAlign = HAlign.Center;

					// Format & display
					break;
				default:
					break;
			}
		}

		#endregion

		#region Properties

		private String _ColumnTitle;

		/// <summary>
		/// Header Title
		/// </summary>
		public String ColumnTitle
		{
			get { return _ColumnTitle; }
			set {
				_ColumnTitle = value;
				columnObject.Header.Caption = value;
			}
		}

		private UltraGridColumnStyle _ColumnStyle;

		/// <summary>
		/// Apply column Style: see CMAX.Framework.Win.Controls.UltraGridColumnStyle
		/// </summary>
		public UltraGridColumnStyle ColumnStyle
		{
			get { return _ColumnStyle; }
			set { 
				_ColumnStyle = value;
				ApplyColumnStyle(value);
			}
		}

		private String _ColumnField = string.Empty;

		/// <summary>
		/// Database column bound to this UltraColumn. This column belongs to DataSource of Grid control.
		/// </summary>
		public String ColumnField
		{
			get { return _ColumnField; }
			set { 
				_ColumnField = value;
				columnObject.Key = value;
			}
		}

		private bool _Editable = false;

		/// <summary>
		/// Set/Get the editing status of this column
		/// </summary>
		public bool Editable
		{
			get { return _Editable; }
			set { 
				_Editable = value;
				//columnObject.AutoEdit = value;
				columnObject.CellActivation = value ? Activation.AllowEdit : Activation.NoEdit;
				if (!value) columnObject.CellAppearance.ForeColor = ColorTranslator.FromHtml("#808080");
			}	
		}

		private bool _Hidden = false;

		/// <summary>
		/// Show/Hide this columns in Grid
		/// </summary>
		public bool Hidden
		{
			get { return _Hidden; }
			set { 
				_Hidden = value;
				columnObject.Hidden = value;
			}
		}

		private bool _AllowRowSummaries = false;

		/// <summary>
		/// Allow this column to supporting summary
		/// </summary>
		public bool AllowRowSummaries 
		{
			get { return _AllowRowSummaries ; }
			set { 
				_AllowRowSummaries  = value;
				columnObject.AllowRowSummaries = value ? Infragistics.Win.UltraWinGrid.AllowRowSummaries.True : Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
			}
		}

		private VAlign _ColumnVAlign = VAlign.Middle;

		/// <summary>
		/// Set/Get vertical alignment setting for this column.
		/// Default is VAlign.Middle
		/// </summary>
		public VAlign ColumnVAlign
		{
			get { return _ColumnVAlign; }
			set {
				_ColumnVAlign = value;
				columnObject.CellAppearance.TextVAlign = value;
			}
		}

		private HAlign _ColumnHAlign = HAlign.Center;

		/// <summary>
		/// Set/Get horizontal alignment setting for this column
		/// Default is HAlign.Left
		/// </summary>
		public HAlign ColumnHAlign
		{
			get { return _ColumnHAlign; }
			set { 
				_ColumnHAlign = value;
				columnObject.CellAppearance.TextHAlign = value;
			}
		}

		private VAlign _HeaderVAlign;

		/// <summary>
		/// Returns or sets the vertical alignment of header
		/// </summary>
		public VAlign HeaderVAlign
		{
			get { return _HeaderVAlign; }
			set { 
				_HeaderVAlign = value;
				columnObject.Header.Appearance.TextVAlign = value;
			}
		}

		private HAlign _HeaderHAlign;

		/// <summary>
		/// Returns or sets the horizontal alignment of header
		/// </summary>
		public HAlign HeaderHAlign
		{
			get { return _HeaderHAlign; }
			set { 
				_HeaderHAlign = value;
				columnObject.Header.Appearance.TextHAlign = value;
			}
		}

		private bool _CellMultiLine = false;

		/// <summary>
		/// Indicate Cell wrapping
		/// </summary>
		public bool CellMultiLine
		{
			get { return _CellMultiLine; }
			set { 
				_CellMultiLine = value;
				columnObject.CellMultiLine = value ? DefaultableBoolean.True : DefaultableBoolean.False;
			}
		}
	

		/// <summary>
		/// Set/Get datatype of this column
		/// </summary>
		public Type DataType
		{
			get { return columnObject.DataType; }
			set { 				
				try
				{
					columnObject.DataType = value;
				}
				catch{}				
			}
		}

		private object _DefaultCellValue = null;

		/// <summary>
		/// Default value of each cell in this column
		/// </summary>
		public object DefaultCellValue
		{
			get { return _DefaultCellValue; }
			set { 
				_DefaultCellValue = value;
				columnObject.DefaultCellValue = value;
			}
		}

		private int _DecimalCount = 2;
		
		/// <summary>
		/// 1200.1234 --> 
		/// default: 2
		/// </summary>
		public int DecimalCount
		{
			get { return _DecimalCount; }
			set 
			{ 
				_DecimalCount = value;
                //TODO:global 대신
                //columnObject.Format = global.FormatNumeric(value);				
                columnObject.Format = "0";
			}
		}

		private String _MaskInput;

		/// <summary>
		/// Set/Get mask string for this column
		/// </summary>
		public String MaskInput
		{
			get { return _MaskInput; }
			set { 
				_MaskInput = value;
				columnObject.MaskInput = value;
			}
		}

        private Char _PromptChar;

        /// <summary>
        /// Set/Get Prompt Char for this column
        /// </summary>
        public Char PromptChar
        {
            get { return _PromptChar; }
            set
            {
                _PromptChar = value;
                columnObject.PromptChar = value;
            }
        }

		private String _FormatString;

		/// <summary>
		/// Returns or sets the format string used to control the display of text in this column.
		/// ie: yyyy--MM-dd: date format
		/// Use UltraMaskedEditHelper for help.
		/// </summary>
		public String FormatString
		{
			get { return _FormatString; }
			set { 
				_FormatString = value;
				columnObject.Format = value;
			}
		}

		private bool _Fixed;

		/// <summary>
		/// Specifies whether this column is fixed on scrolling
		/// </summary>
		public bool Fixed
		{
			get { return _Fixed; }
			set { 
				_Fixed = value;
				columnObject.Header.Fixed = value;				
			}
		}
	
		private bool _Sortable = true;

		/// <summary>
		/// Returns or sets the sorting behavior used to control if this column supports sorting feature.
		/// Default is true.
		/// </summary>
		public bool Sortable
		{
			get { return _Sortable = true; }
			set { 
				_Sortable = value; 
				if(!gridControl.DisplayLayout.Bands[0].SortedColumns.Exists(columnObject.Key))
					gridControl.DisplayLayout.Bands[0].SortedColumns.Add(columnObject, false);
				gridControl.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
				this.SortIndicator = SortIndicator.Disabled;
			}			
		}

		private SortIndicator _SortIndicator = SortIndicator.Ascending;

		/// <summary>
		/// Returns or set the value that indicates the sort order of the column
		/// </summary>
		public SortIndicator SortIndicator
		{
			get { return _SortIndicator; }
			set { 
				_SortIndicator = value;
				columnObject.SortIndicator = value;
			}
		}
	

		private bool _AllowGroupBy = false;

		/// <summary>
		/// Enables or disables GroupBy feature of this column. Default is false.
		/// This property depends on the Grid DisplayLayout.Override.AllowGroupBy property
		/// </summary>
		public bool AllowGroupBy
		{
			get { return _AllowGroupBy; }
			set { 
				_AllowGroupBy = value;
				columnObject.AllowGroupBy = value ? DefaultableBoolean.True : DefaultableBoolean.False;
				//columnObject.GroupByMode = GroupByMode.Default;
				columnObject.SortIndicator = SortIndicator.Ascending;
			}
		}

		private int _Width;

		/// <summary>
		/// Returns or sets the width of this column
		/// </summary>
		public int Width
		{
			get { return _Width; }
			set { 
				_Width = value;
				columnObject.Width = value;
			}
		}

		private bool _AutoSize = false;

		/// <summary>
		/// Returns or sets the Autosing feature of this column.
		/// Default is false. If true, the column will be resized according to data in all rows
		/// </summary>
		public bool AutoSize
		{
			get { return _AutoSize; }
			set { 
				_AutoSize = value;
				columnObject.AutoSizeMode = value ? ColumnAutoSizeMode.AllRowsInBand : ColumnAutoSizeMode.None;
			}
		}


		private int _FieldLen = 0;

		/// <summary>
		/// Maximum character count can be entered to cell
		/// Default: unlimited
		/// </summary>
		public int FieldLen
		{
			get { return _FieldLen; }
			set { _FieldLen = value; }
		}

		private bool _ShowHeaderCheckBox = true;

		/// <summary>
		/// Show/Hide CheckBox in header of checkbox column
		/// </summary>
		public bool ShowHeaderCheckBox
		{
			get { return _ShowHeaderCheckBox; }
			set { _ShowHeaderCheckBox = value;
			this.columnObject.Tag = value;
			}
		}
		
		#endregion Properties

		#region Constructors

		public UltraGridHelperColumn(UltraGrid gridControl)
		{
			this.gridControl = gridControl;
			//global = UIHelper.GetGlobalSettings(gridControl);
		}

		public UltraGridHelperColumn(UltraGrid gridControl, string columnField)
		{
			this.gridControl = gridControl;
			//global = UIHelper.GetGlobalSettings(gridControl);

			columnObject = gridControl.DisplayLayout.Bands[0].Columns.Add(columnField);
		}

		public UltraGridHelperColumn(UltraGrid gridControl, string columnField, string columnTitle, int columnWidth, bool editable)
		{
			this.gridControl = gridControl;
			//global = UIHelper.GetGlobalSettings(gridControl);

			this.columnObject = gridControl.DisplayLayout.Bands[0].Columns.Add(columnField, columnTitle);
			this.Width = columnWidth;
			this.Editable = editable;
			ApplyColumnStyle(UltraGridColumnStyle.Text);
		}

		public UltraGridHelperColumn(UltraGrid gridControl, string columnField, string columnTitle, int columnWidth, bool editable, bool hidden)
		{
			this.gridControl = gridControl;
			//global = UIHelper.GetGlobalSettings(gridControl);

			this.columnObject = gridControl.DisplayLayout.Bands[0].Columns.Add(columnField, columnTitle);
			this.Width = columnWidth;
			this.Editable = editable;
			this.Hidden = hidden;
			ApplyColumnStyle(UltraGridColumnStyle.Text);
		}

		public UltraGridHelperColumn(UltraGrid gridControl, string columnField, string columnTitle, UltraGridColumnStyle columnStyle, int columnWidth, bool editable)
		{
			this.gridControl = gridControl;
			//global = UIHelper.GetGlobalSettings(gridControl);

			this.columnObject = gridControl.DisplayLayout.Bands[0].Columns.Add(columnField, columnTitle);
			this.Width = columnWidth;
			this.Editable = editable;
			ApplyColumnStyle(columnStyle);
		}

		public UltraGridHelperColumn(UltraGrid gridControl, string columnField, string columnTitle, UltraGridColumnStyle columnStyle, int columnWidth, bool editable, bool hidden)
		{
			this.gridControl = gridControl;
			//global = UIHelper.GetGlobalSettings(gridControl);

			this.columnObject = gridControl.DisplayLayout.Bands[0].Columns.Add(columnField, columnTitle);
			this.Width = columnWidth;
			this.Editable = editable;
			this.Hidden = hidden;
			ApplyColumnStyle(columnStyle);
		}

		#endregion

		#region Static

		/// <summary>
		/// Return a UltraGridHelperColumn object from current existing UltraGridColumn
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnStyle"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <returns></returns>
		public static UltraGridHelperColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, UltraGridColumnStyle columnStyle, 
			string columnTitle,int columnWidth, bool editable)
		{
			UltraGridHelperColumn helperColumn = new UltraGridHelperColumn(gridControl);			
			helperColumn.columnObject = gridControl.DisplayLayout.Bands[0].Columns[columnField];

			helperColumn.ColumnTitle = columnTitle;
			helperColumn.Width = columnWidth;
			helperColumn.Editable = editable;
			helperColumn.ColumnStyle = columnStyle;

			return helperColumn;
		}

		/// <summary>
		/// Return a UltraGridHelperDropDownColumn object from current existing UltraGridColumn
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="singleDropDown"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="textList"></param>
		/// <param name="valueList"></param>
		/// <returns></returns>
		public static UltraGridHelperDropDownColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle, bool singleDropDown, int columnWidth, bool editable,
			string[] textList, object[] valueList)
		{
			UltraGridHelperDropDownColumn helperColumn = new UltraGridHelperDropDownColumn(gridControl);
			helperColumn.columnObject = gridControl.DisplayLayout.Bands[0].Columns[columnField];

			helperColumn.ColumnTitle = columnTitle;
			helperColumn.Width = columnWidth;
			helperColumn.Editable = editable;
			helperColumn.ColumnStyle = singleDropDown ? UltraGridColumnStyle.SingleDropDown : UltraGridColumnStyle.SingleColumnDropDown;

			///
			/// Check validation
			/// 
			if (textList.Length != valueList.Length)
				throw new Exception("TextList & ValueList must be same in quantity");

			/// Creating ValueList
			ValueList vl = new ValueList();
			for (int i = 0; i < textList.Length; i++)
			{
				vl.ValueListItems.Add(valueList[i], textList[i]);
			}

			/// 
			/// Binding
			///
			helperColumn.columnObject.ValueList = vl;
			return helperColumn;
		}

		/// <summary>
		/// Return a UltraGridHelperDropDownColumn object from current existing UltraGridColumn
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="singleDropDown"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="textList"></param>
		/// <param name="valueList"></param>
		/// <returns></returns>
		public static UltraGridHelperDropDownColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle, 
			bool singleDropDown, int columnWidth, bool editable, DataTable dataSource, string dataTextField, string dataValueField)
		{
			UltraGridHelperDropDownColumn helperColumn = new UltraGridHelperDropDownColumn(gridControl);
			helperColumn.columnObject = gridControl.DisplayLayout.Bands[0].Columns[columnField];

			helperColumn.ColumnTitle = columnTitle;
			helperColumn.Width = columnWidth;
			helperColumn.Editable = editable;
			helperColumn.ColumnStyle = singleDropDown ? UltraGridColumnStyle.SingleDropDown : UltraGridColumnStyle.SingleColumnDropDown;

			///
			/// Check validation
			/// 
			if (dataSource == null)
				throw new Exception("TextList & ValueList must be same in quantity");

			/// Creating ValueList
			ValueList vl = new ValueList();
			foreach (DataRow dr in dataSource.Rows)
			{
				vl.ValueListItems.Add(dr[dataValueField], Convert.ToString(dr[dataTextField]));
			}
			/// 
			/// Binding
			///
			helperColumn.columnObject.ValueList = vl;
			return helperColumn;
		}

		/// <summary>
		/// Return a UltraGridHelperDropDownColumn object from current existing UltraGridColumn
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="singleColumnDropDown"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="dataSource"></param>
		/// <param name="dataTextField"></param>
		/// <param name="dataValueField"></param>
		/// <param name="dropDownColumnTitles"></param>
		/// <returns></returns>
		public static UltraGridHelperDropDownColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle, bool singleColumnDropDown, int columnWidth, bool editable,
			object dataSource, string dataTextField, string dataValueField, string[] dropDownColumnTitles)
		{
			UltraGridHelperDropDownColumn helperColumn = new UltraGridHelperDropDownColumn(gridControl);
			helperColumn.columnObject = gridControl.DisplayLayout.Bands[0].Columns[columnField];

			helperColumn.ColumnTitle = columnTitle;
			helperColumn.Width = columnWidth;
			helperColumn.Editable = editable;
			helperColumn.ColumnStyle = singleColumnDropDown ? UltraGridColumnStyle.SingleColumnDropDown : UltraGridColumnStyle.MultiColumnDropDown;

			///
			/// Create a UltraDropDown control & bind
			/// 
			if (dataSource != null)
			{
				UltraDropDown udd = new UltraDropDown();
				udd.Parent = gridControl.Parent;

				udd.DataSource = dataSource;
				udd.DisplayMember = dataTextField;
				udd.ValueMember = dataValueField;
				udd.DataBind();

				///
				/// Update grid
				/// 
				helperColumn.columnObject.ValueList = udd;
			}

			return helperColumn;	
		}

		#endregion
	}

	#region UltraGridHelperEditButtonColumn

	//ultraTextEditor1_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)

	/// <summary>
	/// Base Class for EditButton & Button Column
	/// </summary>
	public class UltraGridHelperEditButtonColumn : UltraGridHelperColumn
	{
		public UltraGridHelperEditButtonColumn(UltraGrid gridControl) : base(gridControl)
		{

		}

		/// <summary>
		/// Standard, full constructor
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="handler"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <param name="buttonImage"></param>
		public UltraGridHelperEditButtonColumn(UltraGrid gridControl, string columnField,
			string columnTitle, int columnWidth, bool editable,
			bool hidden, Image buttonImage)
			: base(gridControl, columnField, columnTitle, columnWidth, editable, hidden)
		{
			///
			/// Apply Column Style
			/// 
			ApplyColumnStyle(UltraGridColumnStyle.EditPopup);
			//columnObject.CellButtonAppearance.Image = buttonImage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="handler">Event handler of Button in cell</param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		public UltraGridHelperEditButtonColumn(UltraGrid gridControl, string columnField, 
			string columnTitle, int columnWidth, bool editable)
			: this(gridControl, columnField, columnTitle, columnWidth, editable, false, Images.btn_popup)
		{				
		}

		/// <summary>
		/// Return a HelperColumn based on a column already defined in design time
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <param name="buttonImage"></param>
		/// <returns></returns>
		public static UltraGridHelperEditButtonColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle,
			int columnWidth, bool editable, bool hidden, Image buttonImage)
		{
			UltraGridHelperEditButtonColumn helperColumn = new UltraGridHelperEditButtonColumn(gridControl);

			helperColumn.columnObject = gridControl.DisplayLayout.Bands[0].Columns[columnField];
			//helperColumn.columnObject.CellButtonAppearance.Image = buttonImage;
			helperColumn.gridControl = gridControl;
            //TODO:global
            //helperColumn.global = UIHelper.GetGlobalSettings(gridControl);

			helperColumn.ColumnTitle = columnTitle;
			helperColumn.Width = columnWidth;
			helperColumn.Editable = editable;
			helperColumn.Hidden = hidden;
			helperColumn.ApplyColumnStyle(UltraGridColumnStyle.EditPopup);

			return helperColumn;
		}

		/// <summary>
		/// Return a HelperColumn based on a column already defined in design time
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public static UltraGridHelperEditButtonColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle,
			int columnWidth, bool editable, bool hidden)			
		{
			return FromUltraGridColumn(gridControl, columnField, columnTitle, columnWidth, editable, hidden, Images.btn_popup);
		}

		/// <summary>
		/// Return a HelperColumn based on a column already defined in design time
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public static UltraGridHelperEditButtonColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle,
			int columnWidth, bool editable)
		{
            return FromUltraGridColumn(gridControl, columnField, columnTitle, columnWidth, editable, false, Images.btn_popup);
		}
	}

	#endregion

	#region UltraGridHelperButtonColumn

	//ultraTextEditor1_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)

	/// <summary>
	/// Base Class for EditButton & Button Column
	/// </summary>
	public class UltraGridHelperButtonColumn : UltraGridHelperColumn
	{
		/// <summary>
		/// Standard constructor
		/// </summary>
		/// <param name="gridControl"></param>
		public UltraGridHelperButtonColumn(UltraGrid gridControl) : base(gridControl)
		{

		}

		/// <summary>
		/// Standard, full constructor
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="handler"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <param name="buttonImage"></param>
		public UltraGridHelperButtonColumn(UltraGrid gridControl, string columnField,
			string columnTitle, int columnWidth, bool editable,
			bool hidden, Image buttonImage)
			: base(gridControl, columnField, columnTitle, columnWidth, editable, hidden)
		{
			///
			/// Apply Column Style
			/// 
			ApplyColumnStyle(UltraGridColumnStyle.EditPopup);
			//columnObject.CellButtonAppearance.Image = buttonImage;			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="handler">Event handler of Button in cell</param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		public UltraGridHelperButtonColumn(UltraGrid gridControl, string columnField,
			string columnTitle, int columnWidth, bool editable)
			: this(gridControl, columnField, columnTitle, columnWidth, editable, false,
            Images.btn_popup)
		{
		}

		/// <summary>
		/// Return a HelperColumn based on a column already defined in design time
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <param name="buttonImage"></param>
		/// <returns></returns>
		public static UltraGridHelperButtonColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle,
			int columnWidth, bool editable, bool hidden, Image buttonImage)
		{
			UltraGridHelperButtonColumn helperColumn = new UltraGridHelperButtonColumn(gridControl);

			helperColumn.columnObject = gridControl.DisplayLayout.Bands[0].Columns[columnField];
			//helperColumn.columnObject.CellButtonAppearance.Image = buttonImage;

			helperColumn.gridControl = gridControl;
			//helperColumn.global = UIHelper.GetGlobalSettings(gridControl);

			helperColumn.ColumnTitle = columnTitle;
			helperColumn.Width = columnWidth;
			helperColumn.Editable = editable;
			helperColumn.Hidden = hidden;
			helperColumn.ApplyColumnStyle(UltraGridColumnStyle.Button);

			return helperColumn;
		}

		/// <summary>
		/// Return a HelperColumn based on a column already defined in design time
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public static UltraGridHelperButtonColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle,
			int columnWidth, bool editable, bool hidden)
		{
            return FromUltraGridColumn(gridControl, columnField, columnTitle, columnWidth, editable, hidden, Images.btn_popup);
		}

		/// <summary>
		/// Return a HelperColumn based on a column already defined in design time
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnField"></param>
		/// <param name="columnTitle"></param>
		/// <param name="columnWidth"></param>
		/// <param name="editable"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public static UltraGridHelperButtonColumn FromUltraGridColumn(UltraGrid gridControl, string columnField, string columnTitle,
			int columnWidth, bool editable)
		{
            return FromUltraGridColumn(gridControl, columnField, columnTitle, columnWidth, editable, false, Images.btn_popup);
		}
	}

	#endregion

	#region UltraGridHelperDropDownColumn

	public class UltraGridHelperDropDownColumn : UltraGridHelperColumn
	{
		#region Properties

		private Object _DataSource = null;

		/// <summary>
		/// Apply datasource for this column. Use in case of DropDown column
		/// </summary>
		public Object DataSource
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}

		private String _DataTextField;

		/// <summary>
		/// Table column which data will be displayed in Grid
		/// </summary>
		public String DataTextField
		{
			get { return _DataTextField; }
			set { _DataTextField = value; }
		}

		private String _DataValueField;

		/// <summary>
		/// Table column which data will be set to DBField column. Use only for DropDown column
		/// </summary>
		public String DataValueField
		{
			get { return _DataValueField; }
			set { _DataValueField = value; }
		}	

		#endregion

		#region Constructors

		/// <summary>
		/// Create a Single DropDown column  with value & text list
		/// </summary>
		/// <param name="gridControl">The Grid control which this column belongs to</param>
		/// <param name="columnField">The database field which bound to this column in grid</param>
		/// <param name="columnTitle">Title of this column</param>
		/// <param name="singleColumnDropDown">this parameter is abosolete</param>
		/// <param name="columnWidth">Width of this column in grid</param>
		/// <param name="editable">Indicate this column is editable or not</param>
		/// <param name="textList">list of text will be displayed in dropdown</param>
		/// <param name="valueMember">list of value will be assigned to grid cell's value when selected</param>
		public UltraGridHelperDropDownColumn(UltraGrid gridControl, string columnField, string columnTitle, bool editable, int columnWidth, bool singleColumnDropDown, string[] textList, object[] valueList)
			: base(gridControl, columnField, columnTitle, singleColumnDropDown ? UltraGridColumnStyle.SingleDropDown : UltraGridColumnStyle.SingleColumnDropDown, columnWidth, editable)
		{
			
			///
			/// Check validation
			/// 
			if (textList.Length != valueList.Length)
				throw new Exception("TextList & ValueList must be same in quantity");

			/// Creating ValueList
			ValueList vl = new ValueList();
			for (int i = 0; i < textList.Length; i++)
			{
				vl.ValueListItems.Add(valueList[i], textList[i]);
			}

			/// 
			/// Binding
			///
			columnObject.ValueList = vl;
		}

		/// <summary>
		/// Create a Single DropDown column 
		/// </summary>
		/// <param name="gridControl">The Grid control which this column belongs to</param>
		/// <param name="columnField">The database field which bound to this column in grid</param>
		/// <param name="columnTitle">Title of this column</param>
		/// <param name="singleColumnDropDown">Display a comboxbox with 1 column or multi-column</param>
		/// <param name="columnWidth">Width of this column in grid</param>
		/// <param name="editable">Indicate this column is editable or not</param>
		/// <param name="dataSource">The datasource which is bound to dropdown</param>
		/// <param name="displayMember">The database field in dataSource which value is display in cell</param>
		/// <param name="valueMember">The database field in dataSource which value is set to Grid column value</param>
		public UltraGridHelperDropDownColumn(UltraGrid gridControl, string columnField, string columnTitle, int columnWidth, bool editable, 
			DataTable dataSource, string displayMember, string valueMember)
			: this (gridControl, columnField, columnTitle, false, columnWidth, editable, dataSource, displayMember, valueMember)
		{			
		}

		/// <summary>
		/// Create a Single DropDown column 
		/// </summary>
		/// <param name="gridControl">The Grid control which this column belongs to</param>
		/// <param name="columnField">The database field which bound to this column in grid</param>
		/// <param name="columnTitle">Title of this column</param>
		/// <param name="singleColumnDropDown">Display a comboxbox with 1 column or multi-column</param>
		/// <param name="columnWidth">Width of this column in grid</param>
		/// <param name="editable">Indicate this column is editable or not</param>
		/// <param name="dataSource">The datasource which is bound to dropdown</param>
		/// <param name="displayMember">The database field in dataSource which value is display in cell</param>
		/// <param name="valueMember">The database field in dataSource which value is set to Grid column value</param>
		public UltraGridHelperDropDownColumn(UltraGrid gridControl, string columnField, string columnTitle, bool singleDropDown, int columnWidth, bool editable,
			DataTable dataSource, string displayMember, string valueMember)
			: base(gridControl, columnField, columnTitle, singleDropDown ? UltraGridColumnStyle.SingleDropDown : UltraGridColumnStyle.SingleColumnDropDown, columnWidth, editable)
		{

			if (dataSource == null) return;

			/// Creating ValueList
			Infragistics.Win.ValueList vl = new Infragistics.Win.ValueList();

			foreach (DataRow dr in dataSource.Rows)
			{
				vl.ValueListItems.Add(dr[valueMember], Convert.ToString(dr[displayMember]));
			}

			/// 
			/// Binding
			///
			columnObject.ValueList = vl;
		}

		public UltraGridHelperDropDownColumn(UltraGrid gridControl) : base (gridControl)
		{			
		}

		/// <summary>
		/// Create a DropDown column with multi fields.
		/// </summary>
		/// <param name="gridControl">The Grid control which this column belongs to</param>
		/// <param name="columnField">The database field which bound to this column in grid</param>
		/// <param name="columnTitle">Title of this column</param>
		/// <param name="singleDropDown">In this case, this must be false</param>
		/// <param name="columnWidth">Width of this column in grid</param>
		/// <param name="editable">Indicate this column is editable or not</param>
		/// <param name="dataSource">The datasource which is bound to dropdown</param>
		/// <param name="dataTextField">The database field in dataSource which value is display in cell</param>
		/// <param name="dataValueField">The database field in dataSource which value is set to Grid column value</param>
		/// <param name="dropDownColumnTitles">Array of text will be displayed on DropDown</param>
		public UltraGridHelperDropDownColumn(UltraGrid gridControl, string columnField, string columnTitle, bool singleColumnDropDown, int columnWidth, bool editable,
			object dataSource, string dataTextField, string dataValueField, string[] dropDownColumnTitles)
			: base(gridControl, columnField, columnTitle, singleColumnDropDown ? UltraGridColumnStyle.SingleColumnDropDown : UltraGridColumnStyle.MultiColumnDropDown, columnWidth, editable)
		{

			if (dataSource == null) return;

			///
			/// Create a UltraDropDown control & bind
			/// 
			UltraDropDown udd = new UltraDropDown();
			udd.Parent = gridControl.Parent;

			udd.DataSource = dataSource;
			udd.DisplayMember = dataTextField;
			udd.ValueMember = dataValueField;
			for (int i = 0; i < dropDownColumnTitles.Length; i++)
			{
				udd.DisplayLayout.Bands[0].Columns[i].Header.Caption = dropDownColumnTitles[i];
			}			

			udd.DataBind();

			///
			/// Update grid
			/// 
			columnObject.ValueList = udd;
		}

		#endregion UltraGridHelperDropDownColumn

		#region DataBind

		/// <summary>
		/// Bind this column with datasource provided
		/// </summary>
		public void DataBind()
		{
			if (_DataSource == null) return;

			/// Creating ValueList
			ValueList vl = new ValueList();

			DataTable dt = (DataTable)_DataSource;

			foreach (DataRow dr in dt.Rows)
			{
				vl.ValueListItems.Add(dr[_DataValueField], Convert.ToString(dr[_DataTextField]));
			}

			/// 
			/// Binding
			///
			columnObject.ValueList = vl;
		}

		#endregion
	}
	#endregion

	public class UltraGridHelperColumnCollection : CollectionBase
	{
		public int Add(UltraGridHelperColumn column)
		{
			return List.Add(column);
		}

		public void Insert(int index, UltraGridHelperColumn column)
		{
			List.Insert(index, column);
		}

		public void Remove(UltraGridHelperColumn column)
		{
			List.Remove(column);
		}

		public bool Contains(UltraGridHelperColumn column)
		{
			return List.Contains(column);
		}

		public int IndexOf(UltraGridHelperColumn column)
		{
			return List.IndexOf(column);
		}

		public void CopyTo(UltraGridHelperColumn[] array, int index)
		{
			List.CopyTo(array, index);
		}

		public UltraGridHelperColumn this[int index]
		{
			get { return (UltraGridHelperColumn)List[index]; }
			set { List[index] = value; }
		}

		public UltraGridHelperColumn FindByName(string columnName)
		{
			foreach (Object objForm in List)
			{
				UltraGridHelperColumn column = (UltraGridHelperColumn)objForm;
				if (column.ColumnField.Equals(columnName))
				{
					return column;
				}
			}

			return null;
		}
	}	

	#endregion UltraGridHelperColumns & UltraGridHelperColumnCollection

	public class UltraGridHelper
	{		
		#region Formating & Binding

		/// <summary>
		/// Binding Grid 
		/// </summary>
		/// <param name="gridControl">UltraGrid control</param>
		/// <param name="dataSource">Prefer to use DataTable</param>
		public static void DataBind(UltraGrid gridControl, object dataSource)
		{
			//set some properties of the editor that will be reflected in the element when the column is edited
			//add an MRU list and set the max items

			/// 
			/// Start Grid Formating & Binding
			/// 
			gridControl.BeginUpdate();

			/// Grid Binding
			/// 
			//gridControl.SetDataBinding(dataSource, dataSource.TableName, false, false);
			//gridControl.DataSource = dataSource;
			_DataBind(gridControl, dataSource, "Table");

			/// 
			/// End Grid Formating & Binding
			/// 
			gridControl.EndUpdate();
		}

		/// <summary>
		/// Binding Grid 
		/// </summary>
		/// <param name="gridControl">UltraGrid control</param>
		/// <param name="dataSource">Prefer to use DataTable</param>
		public static void DataBind(UltraGrid gridControl, object dataSource, string dataMember)
		{
			//set some properties of the editor that will be reflected in the element when the column is edited
			//add an MRU list and set the max items

			/// 
			/// Start Grid Formating & Binding
			/// 
			gridControl.BeginUpdate();

			/// Grid Binding
			/// 
			//gridControl.SetDataBinding(dataSource, dataSource.TableName, false, false);
			//gridControl.DataSource = dataSource;
			_DataBind(gridControl, dataSource, dataMember);

			/// 
			/// End Grid Formating & Binding
			/// 
			gridControl.EndUpdate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl">UltraGrid control</param>
		/// <param name="dataSource">DataTable</param>
		/// <param name="allowAdd">Allow Adding rows to Grid</param>
		///	<param name="allowUpdate">Allow updating data to grid</param>
		/// <param name="allowDelete"></param>
		public static void DataBind(UltraGrid gridControl, DataTable dataSource,
			bool allowAdd,
			bool allowUpdate,
			bool allowDelete)
		{
			DataBind(gridControl, dataSource, "Table", allowAdd, allowUpdate, allowDelete);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl">UltraGrid control</param>
		/// <param name="dataSource">DataTable</param>
		/// <param name="allowAdd">Allow Adding rows to Grid</param>
		///	<param name="allowUpdate">Allow updating data to grid</param>
		/// <param name="allowDelete"></param>
		public static void DataBind(UltraGrid gridControl, object dataSource,
			bool allowAdd,
			bool allowUpdate,
			bool allowDelete)
		{
			DataBind(gridControl, dataSource, "Table", allowAdd, allowUpdate, allowDelete);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl">UltraGrid control</param>
		/// <param name="dataSource">DataTable</param>
		/// <param name="allowAdd">Allow Adding rows to Grid</param>
		///	<param name="allowUpdate">Allow updating data to grid</param>
		/// <param name="allowDelete"></param>
		public static void DataBind(UltraGrid gridControl, object dataSource, 
			string dataMember,
			bool allowAdd, 
			bool allowUpdate, 
			bool allowDelete)
		{
			/// 
			/// Start Grid Formating & Binding
			/// 
			gridControl.BeginUpdate();

			///
			/// Grid Binding
			/// 
			//gridControl.SetDataBinding(dataSource, dataSource.TableName, false, false);
			//gridControl.DataSource = dataSource;
			_DataBind(gridControl, dataSource, dataMember);

			/// 
			/// - Editable (Add, Edit, Update, Delete)	
			/// 
			gridControl.DisplayLayout.Override.AllowAddNew = allowAdd ? AllowAddNew.Yes : AllowAddNew.Yes;
			gridControl.DisplayLayout.Override.AllowDelete = allowDelete ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowUpdate = allowUpdate ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.CellClickAction = allowUpdate ? CellClickAction.EditAndSelectText : CellClickAction.RowSelect;
			//gridControl.DisplayLayout.Override.CellClickAction = allowUpdate ? CellClickAction.EditAndSelectText : CellClickAction.RowSelect;

			/// 
			/// End Grid Formating & Binding
			/// 
			gridControl.EndUpdate();

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subSysType"></param>
		/// <param name="gridControl"></param>
		/// <param name="dataSource"></param>
		/// <param name="columnTitles"></param>
		/// <param name="invisibleColumns"></param>
		public static void DataBind(SubSystemType subSysType,
			UltraGrid gridControl,
			DataTable dataSource,
			string[] columnTitles,
			string[] invisibleColumns)
		{
			/// 
			/// Start Grid Formating & Binding
			/// 
			gridControl.BeginUpdate();

			///
			/// Apply grid default format for specific sub system
			/// - Autosize
			/// - Fixed headers
			/// - Prompts to delete, update...
			/// 
			ApplyDefaultStyle(gridControl);

			///
			/// Grid Binding
			/// 
			//gridControl.SetDataBinding(dataSource, dataSource.TableName, false, false);
			gridControl.DataSource = dataSource;

			if (invisibleColumns != null)
			{
				for (int i = 0; i < invisibleColumns.Length; i++)
				{
					UltraGridColumn column = gridControl.DisplayLayout.Bands[0].Columns[invisibleColumns[i]];
					column.Hidden = true;
				}
			}

			if (columnTitles != null)
			{
				int nVisibleIdx = 0;
				for (int i = 0; i < gridControl.DisplayLayout.Bands[0].Columns.Count; i++)
				{
					UltraGridColumn column = gridControl.DisplayLayout.Bands[0].Columns[i];
					if (column.Hidden || nVisibleIdx >= columnTitles.Length)
						continue;

					column.Header.Caption = columnTitles[nVisibleIdx];
					nVisibleIdx++;
				}
			}

			/// 
			/// End Grid Formating & Binding
			/// 
			gridControl.EndUpdate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataSource">DataTable</param>
		/// <param name="columnTitles">Only visible columns need according to column sequence in the datatable</param>
		/// <param name="invisibleColumns">Array of invisible column name </param>
		public static void DataBind(SubSystemType subSysType,
			UltraGrid gridControl,
			DataTable dataSource,
			string[] columnTitles,
			string[] invisibleColumns,
			bool allowAdd,
			bool allowUpdate,
			bool allowDelete)
		{
			/// 
			/// Start Grid Formating & Binding
			/// 
			gridControl.BeginUpdate();

			///
			/// Apply grid default format for specific sub system
			/// - Autosize
			/// - Fixed headers
			/// - Prompts to delete, update...
			/// 
			ApplyDefaultStyle(gridControl);

			///
			/// Grid Binding
			/// 
			//gridControl.SetDataBinding(dataSource, dataSource.TableName, false, false);
			gridControl.DataSource = dataSource;

			/// 
			/// - Editable (Add, Edit, Update, Delete)	
			/// 
			gridControl.DisplayLayout.Override.AllowAddNew = allowAdd ? AllowAddNew.Yes : AllowAddNew.Yes;
			gridControl.DisplayLayout.Override.AllowDelete = allowDelete ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowUpdate = allowUpdate ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.CellClickAction = allowUpdate ? CellClickAction.EditAndSelectText : CellClickAction.CellSelect;

			if (invisibleColumns != null)
			{
				for (int i = 0; i < invisibleColumns.Length; i++)
				{
					UltraGridColumn column = gridControl.DisplayLayout.Bands[0].Columns[invisibleColumns[i]];
					column.Hidden = true;
				}
			}

			if (columnTitles != null)
			{
				int nVisibleIdx = 0;
				for (int i = 0; i < gridControl.DisplayLayout.Bands[0].Columns.Count; i++)
				{
					UltraGridColumn column = gridControl.DisplayLayout.Bands[0].Columns[i];
					if (column.Hidden || nVisibleIdx >= columnTitles.Length)
						continue;

					column.Header.Caption = columnTitles[nVisibleIdx];
					nVisibleIdx++;
				}
			}

			/// 
			/// End Grid Formating & Binding
			/// 
			gridControl.EndUpdate();
		}

		#endregion

		#region Grid Layout

		/// <summary>
		/// Set the default appearance of grid control.
		/// </summary>
		/// <param name="gridControl"></param>
		public static void ApplyDefaultStyle(UltraGrid gridControl)
		{
			///
			/// Get Settings from parent form
			/// 
			FormSettings settings = UIHelper.GetFormSettings(gridControl);

			//DisplayLayout
			//    Appearance
			//        BorderColor = Button Color
			//        BackColor = Transparent
			gridControl.DisplayLayout.Appearance.BorderColor = settings.GridBorderColor;

			//    AutoFitStyle = None;
			//    BorderStyle : Solid
			//    BorderStyleCaption: None
			//    CaptionVisible = False
			gridControl.DisplayLayout.AutoFitStyle = AutoFitStyle.None;;
			gridControl.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
			gridControl.DisplayLayout.BorderStyleCaption = UIElementBorderStyle.None;
            gridControl.DisplayLayout.CaptionVisible = DefaultableBoolean.False;

			//    GroupByBox
			//        Appearance
			//            BackColor : button border color
			//            BorderColor: White
			//        BandLabelAppearance
			//            ForeColor: Black
			//        BorderStyle: None
			//        Hidden: true/false
			//        Prompt: text to display
			//        PromtAppearance
			//            BackColor: transparent
			//            ForeColor: Black
			gridControl.DisplayLayout.GroupByBox.Appearance.BackColor = settings.GridGroupByBoxBackColor;
			gridControl.DisplayLayout.GroupByBox.Appearance.BorderColor = Color.White;
			gridControl.DisplayLayout.GroupByBox.BandLabelAppearance.ForeColor = Color.Black;
			gridControl.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.None;
			gridControl.DisplayLayout.GroupByBox.Hidden = true;
			gridControl.DisplayLayout.GroupByBox.PromptAppearance.BackColor = Color.Transparent;
			gridControl.DisplayLayout.GroupByBox.PromptAppearance.ForeColor = Color.Black;						
			
			//	  Splitter
			gridControl.DisplayLayout.MaxColScrollRegions = 1;
			gridControl.DisplayLayout.MaxRowScrollRegions = 1;

			//    Override
			//        ActiveCellAppearance
			//            ForeColor: ControlText
			//        ActiveRowAppearance
			//            BackColor: GridRowHoverBackColor
			//            ForeColor: ControlText
			//        BorderStyleCell: Dotted
			//        BorderStyleHEader: Solid
			//        BorderStyleRow: Dotted
			//        CellAppearance
			//            TextTrimming: EllipsisCharacter			
			gridControl.DisplayLayout.Override.ActiveCellAppearance.ForeColor = Color.Black;
			gridControl.DisplayLayout.Override.ActiveRowAppearance.BackColor = settings.GridRowHoverBackColor;
			gridControl.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Black;
			gridControl.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
			gridControl.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid;
			gridControl.DisplayLayout.Override.BorderStyleRowSelector = UIElementBorderStyle.None;
			gridControl.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
			gridControl.DisplayLayout.Override.CellAppearance.TextTrimming = TextTrimming.EllipsisCharacter;
						
			//        CellClickAction
			//        HeaderAppearance
			//            BackColor: GridHeaderBackColor
			//            TextHAlign
			//            ThemedElementAlpha: Transparent
			//        HeaderClickAction
			//        HeaderStyle: Standard
			gridControl.DisplayLayout.Override.HeaderAppearance.BackColor = settings.GridHeaderBackColor;
			gridControl.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
			gridControl.DisplayLayout.Override.HeaderAppearance.ThemedElementAlpha = Alpha.Transparent;
			gridControl.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
			//gridControl.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;

			//        RowAlternateAppearance
			//            BackColor: GridRowAlternateBackColor
			//        RowAppearance
			//            BackColor: White
			//            BorderColor:
			//            TextVAlign: Bottom
			//		  CellAppearance
			//			  BorderColor
			gridControl.DisplayLayout.Override.RowAlternateAppearance.BackColor = settings.GridRowAlternateBackColor;
			gridControl.DisplayLayout.Override.RowAppearance.BackColor = settings.GridRowBackColor;
			gridControl.DisplayLayout.Override.RowAppearance.BorderColor = settings.GridBorderColor;
			gridControl.DisplayLayout.Override.RowAppearance.TextVAlign = VAlign.Middle;
			gridControl.DisplayLayout.Override.CellAppearance.BorderColor = settings.GridBorderColor;


			//        RowSelectorAppearance
			//            BackColor: GridHeaderBackColor
			//        RowSelectorHeaderAppearance
			//            BackColor: GridHeaderBackColor
			//            BorderColor: Transparent
			//            ThemedElementAlpha: Transparent
			//        RowSelectorNumberStyle: RowIndex
			//        RowSelectors: Default/False
			//        RowSelectorStyle: Standard
			//		  RowSizing
			gridControl.DisplayLayout.Override.RowSelectorAppearance.BackColor = settings.GridHeaderBackColor;
			gridControl.DisplayLayout.Override.RowSelectorAppearance.TextVAlign = VAlign.Middle;
			gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BackColor = settings.GridHeaderBackColor;
			gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BorderColor = settings.GridBorderColor; //Color.Transparent;
			gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.ThemedElementAlpha = Alpha.Transparent;
			gridControl.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.None;

			gridControl.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
			gridControl.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.SeparateElement;
			gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BorderColor = settings.GridHeaderBackColor;
			gridControl.DisplayLayout.Override.RowSelectorWidth = 15;
			gridControl.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
			gridControl.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

			//        SelectedRowAppearance
			//            BackColor: GridRowHoverBackColor
			//            ForeColor: ControlText
			//        SummaryFooterAppearance
			//            BackColor: White
			//            ForeColor: ControlText
			//		  AllowColSizing
			//		  AllowRowFiltering
			//		  FixedHeaderIndicator

			gridControl.DisplayLayout.Override.SelectedRowAppearance.BackColor = settings.GridRowHoverBackColor;
			gridControl.DisplayLayout.Override.SelectedRowAppearance.ForeColor = Color.Black;
			gridControl.DisplayLayout.Override.SummaryFooterAppearance.BackColor = Color.White;
			gridControl.DisplayLayout.Override.SummaryFooterAppearance.ForeColor = Color.Black;
			gridControl.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
			
			gridControl.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
			gridControl.DisplayLayout.UseFixedHeaders = true;
			gridControl.DisplayLayout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

			gridControl.DisplayLayout.Override.AllowColSwapping = AllowColSwapping.NotAllowed;
			gridControl.DisplayLayout.Override.AllowColMoving = AllowColMoving.NotAllowed;

			gridControl.DisplayLayout.Override.AllowGroupBy = DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowGroupMoving = AllowGroupMoving.NotAllowed;
			gridControl.DisplayLayout.Override.AllowGroupSwapping = AllowGroupSwapping.NotAllowed;

            /// 
            /// ColumnSizingArea
            /// 
            //gridControl.DisplayLayout.Override.ColumnSizingArea = ColumnSizingArea.Default;
						
			//    ViewStyle: SingleBand
			//    ViewStyleBand: OutlookGroupBy/ Vertical
			//	  AddNewBox
			gridControl.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
			gridControl.DisplayLayout.ViewStyleBand = ViewStyleBand.Horizontal;
			gridControl.DisplayLayout.AddNewBox.Hidden = true;

			///
			/// SingleRow, SingleColumn selection
			/// 
			SetCellSelectionStyle(gridControl, SelectType.Single);
			SetColumnSelectionStyle(gridControl, SelectType.Single);
			SetRowSelectionStyle(gridControl, SelectType.Single);

			///
			/// DataError Validation
			/// 
			gridControl.DisplayLayout.Override.SupportDataErrorInfo = SupportDataErrorInfo.None;
			gridControl.DisplayLayout.Override.DataErrorCellAppearance.ForeColor = Color.Red;
			gridControl.DisplayLayout.Override.DataErrorCellAppearance.BackColor = settings.ErrorBackColor;
			gridControl.Validating += new System.ComponentModel.CancelEventHandler(OnGridValidating);
			gridControl.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(OnGridBeforeRowsDeleted);

            /// 
            /// gridControl Hover Color change
            /// 
            gridControl.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            gridControl.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
			gridControl.DisplayLayout.EmptyRowSettings.RowAppearance.BackColor = ColorTranslator.FromHtml("#E0E0E0"); //Color.FromKnownColor(KnownColor.Control);
            gridControl.DisplayLayout.EmptyRowSettings.RowSelectorAppearance.BackColor = settings.GridHeaderBackColor;			
                       
			/// 
			/// Excel-type moving
			/// 
            gridControl.KeyDown += new KeyEventHandler(OnGridKeyUpKeyDown);
			gridControl.KeyUp += new KeyEventHandler(OnGridKeyUp);
			gridControl.MouseMove += new MouseEventHandler(OnGridMouseMove);
			gridControl.MouseLeave += new EventHandler(OnGridMouseLeave);

			gridControl.AfterRowActivate += new EventHandler(OnGridControlAfterRowActivate);

			///
			/// Creation Filters
			///

			///
			///	Create Filter to display CheckBox on Header
			///	
			UltraGridCreationFilter filter = new UltraGridCreationFilter();

			// Attach an event handler for when the CheckBox in the column header is clicked.
			filter.CheckChanged += new UltraGridCreationFilter.HeaderCheckBoxClickedHandler(OnHeaderCheckBoxCheckChanged);

			// Assign the creation filter to the grid's CreationFilter property.
			gridControl.CreationFilter = filter;
		}

		/// <summary>
		/// Set the default appearance of grid control including empty rows
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="numberOfEmptyRows"></param>
		public static void ApplyDefaultStyle(UltraGrid gridControl, int numberOfEmptyRows)
		{
			ApplyDefaultStyle(gridControl);

			///
			/// Bind empty rows
			/// 
			DataTable tempDT = new DataTable();
			tempDT.TableName = "Table";
			for (int i = 0; i < gridControl.DisplayLayout.Bands[0].Columns.Count; i++)
			{
				DataColumn dc = new DataColumn(gridControl.DisplayLayout.Bands[0].Columns[i].Key);
				dc.DataType = gridControl.DisplayLayout.Bands[0].Columns[i].DataType;
				//dc.DefaultValue = string.Empty;
				tempDT.Columns.Add(dc);
			}

			for (int i = 0; i < numberOfEmptyRows; i++)
			{
				DataRow dr = tempDT.NewRow();
				tempDT.Rows.Add(dr);
				if (tempDT.HasErrors)
					tempDT.GetErrors()[0].ClearErrors();
			}

			_DataBind(gridControl, tempDT);
		}

		/// <summary>
		/// Set the default appearance of grid control including empty rows
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="dataSchema"></param>
		/// <param name="numberOfEmptyRows"></param>
		public static void ApplyDefaultStyle(UltraGrid gridControl, DataTable dataSchema, int numberOfEmptyRows)
		{
			ApplyDefaultStyle(gridControl);
			
			///
			/// Bind empty rows
			/// 
			DataTable tempDT = dataSchema.Copy();
			tempDT.Clear();

			for (int i = 0; i < numberOfEmptyRows; i++)
			{
				DataRow dr = tempDT.NewRow();
				tempDT.Rows.Add(dr);
				if (tempDT.HasErrors)
					tempDT.GetErrors()[0].ClearErrors();
			}

			_DataBind(gridControl, tempDT);
		}

		/// <summary>
		/// Enable/Disable Column Editing
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnName"></param>
		/// <param name="editable"></param>
		public static void SetColumnEditable(UltraGrid gridControl, string columnName, bool editable)
		{
			gridControl.DisplayLayout.Bands[0].Columns[columnName].CellActivation = editable ? Activation.AllowEdit : Activation.ActivateOnly;
		}


		/// <summary>
		/// To display multi-row header type
		/// </summary>
		/// <param name="groupName"></param>
		/// <param name="columnNames"></param>
		public static void SetColumnGroup(UltraGrid gridControl, string groupName, string[] columnNames)
		{
			if (gridControl.DisplayLayout.Bands[0].Groups.Contains(groupName))
			{
				throw new Exception("Group Name exists");
			}

			UltraGridGroup newGroup = new UltraGridGroup();
			newGroup.Key = groupName;

			for (int i = 0; i < columnNames.Length; i++)
			{
				newGroup.Columns.Add(gridControl.DisplayLayout.Bands[0].Columns[columnNames[i]]);
			}			
		}

						
		/// <summary>
		/// Set Grid caption
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="caption"></param>
		public static void SetGridCaption(UltraGrid gridControl, string caption)
		{
			gridControl.Text = caption;
			gridControl.DisplayLayout.CaptionVisible = DefaultableBoolean.True;
		}


		/// <summary>
		/// Set fixed columns
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnNames"></param>
		public static void SetFixedColumns(UltraGrid gridControl, string[] columnNames)
		{
			for (int i = 0; i < columnNames.Length; i++)
			{
				gridControl.DisplayLayout.Bands[0].Columns[columnNames[i]].Header.Fixed = true;
			}
		}

		/// <summary>
		/// Select multiple cells by pressing Ctrl key
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="multiple"></param>
		public static void SetCellSelectionStyle(UltraGrid gridControl, SelectType selectType)
		{
			gridControl.DisplayLayout.Override.SelectTypeCell = selectType;
		}

		/// <summary>
		/// Select multiple columns by pressing Ctrl key
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="multiple"></param>
		public static void SetColumnSelectionStyle(UltraGrid gridControl, SelectType selectType)
		{
			gridControl.DisplayLayout.Override.SelectTypeCol = selectType;
		}

		/// <summary>
		/// Select multiple rows by pressing Ctrl key
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="multiple"></param>
		public static void SetRowSelectionStyle(UltraGrid gridControl, SelectType selectType)
		{
			gridControl.DisplayLayout.Override.SelectTypeRow = selectType;
		}
		
		/// <summary>
		/// Excel-like auto filtering 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="autoFilter"></param>
		public static void SetColumnAutoFilter(UltraGrid gridControl, bool autoFilter)
		{
			gridControl.DisplayLayout.Override.AllowRowFiltering = autoFilter ? DefaultableBoolean.True : DefaultableBoolean.False;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="columnNames"></param>
		public static void SetFixedRows(UltraGrid gridControl, int[] rowIndex)
		{
			/*
			for (int i = 0; i < rowIndex.Length; i++)
			{
				gridControl.Rows[rowIndex[i]].Fixed = true;
			}
			 * */
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="allowGroupBy"></param>
		/// <param name="autoFilter"></param>
		/// <param name="allowColumnMoving"></param>
		/// <param name="allowColumnSizing"></param>
		/// <param name="allowRowSizing"></param>
		public static void SetGridOptions(UltraGrid gridControl, bool allowGroupBy, bool autoFilter,
			bool allowColMoving, bool allowColSizing)
		{
			gridControl.DisplayLayout.Override.AllowGroupBy = allowGroupBy ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.ViewStyleBand = allowGroupBy ? ViewStyleBand.OutlookGroupBy : ViewStyleBand.Vertical;
			gridControl.DisplayLayout.GroupByBox.Hidden = !allowGroupBy;
			gridControl.DisplayLayout.Override.AllowRowFiltering = autoFilter ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowColSwapping = !allowColMoving ? AllowColSwapping.NotAllowed : AllowColSwapping.Default;
			gridControl.DisplayLayout.Override.AllowColMoving = !allowColMoving ? AllowColMoving.NotAllowed : AllowColMoving.Default;
			gridControl.DisplayLayout.Override.AllowColSizing = allowColSizing ? AllowColSizing.Free : AllowColSizing.None;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="allowGroupBy"></param>
		/// <param name="autoFilter"></param>
		/// <param name="allowColumnMoving"></param>
		/// <param name="allowColumnSizing"></param>
		/// <param name="allowRowSizing"></param>
		public static void SetGridOptions(UltraGrid gridControl, bool allowGroupBy, bool autoFilter,
			bool allowColMoving, bool allowColSizing, bool showRowSelector)
		{
			gridControl.DisplayLayout.Override.AllowGroupBy = allowGroupBy ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.ViewStyleBand = allowGroupBy ? ViewStyleBand.OutlookGroupBy : ViewStyleBand.Horizontal;
            gridControl.DisplayLayout.GroupByBox.Hidden = !allowGroupBy;
			gridControl.DisplayLayout.Override.AllowRowFiltering = autoFilter ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowColSwapping = !allowColMoving ? AllowColSwapping.NotAllowed : AllowColSwapping.Default;
			gridControl.DisplayLayout.Override.AllowColMoving = !allowColMoving ? AllowColMoving.NotAllowed : AllowColMoving.Default;
			gridControl.DisplayLayout.Override.AllowColSizing = allowColSizing ? AllowColSizing.Free : AllowColSizing.None;

			if (showRowSelector)
			{
				FormSettings settings = UIHelper.GetFormSettings(gridControl);

				gridControl.DisplayLayout.Override.RowSelectorAppearance.BackColor = settings.GridHeaderBackColor;
				gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BackColor = settings.GridHeaderBackColor;
				gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BorderColor = Color.Transparent;
				gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.ThemedElementAlpha = Alpha.Transparent;
				//gridControl.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex;
				gridControl.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
				//gridControl.DisplayLayout.Override.RowSelectorStyle = HeaderStyle.Standard;
			}
			else
			{
				gridControl.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			}
		}
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="allowGroupBy"></param>
		/// <param name="autoFilter"></param>
		/// <param name="allowColumnMoving"></param>
		/// <param name="allowColumnSizing"></param>
		/// <param name="allowRowSizing"></param>
		public static void SetGridOptions(UltraGrid gridControl, bool allowGroupBy, bool autoFilter,
            bool allowColMoving, bool allowColSizing, bool showRowSelector, AutoFitStyle autoFitStyle)
		{
			gridControl.DisplayLayout.Override.AllowGroupBy = allowGroupBy ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.ViewStyleBand = allowGroupBy ? ViewStyleBand.OutlookGroupBy : ViewStyleBand.Horizontal;
            gridControl.DisplayLayout.GroupByBox.Hidden = !allowGroupBy;
			gridControl.DisplayLayout.Override.AllowRowFiltering = autoFilter ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowColSwapping = !allowColMoving ? AllowColSwapping.NotAllowed : AllowColSwapping.Default;
			gridControl.DisplayLayout.Override.AllowColMoving = !allowColMoving ? AllowColMoving.NotAllowed : AllowColMoving.Default;
			gridControl.DisplayLayout.Override.AllowColSizing = allowColSizing ? AllowColSizing.Free : AllowColSizing.None;
            gridControl.DisplayLayout.AutoFitStyle = autoFitStyle;

			if (showRowSelector)
			{
				FormSettings settings = UIHelper.GetFormSettings(gridControl);

				gridControl.DisplayLayout.Override.RowSelectorAppearance.BackColor = settings.GridHeaderBackColor;
				gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BackColor = settings.GridHeaderBackColor;
				gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.BorderColor = Color.Transparent;
				gridControl.DisplayLayout.Override.RowSelectorHeaderAppearance.ThemedElementAlpha = Alpha.Transparent;
				//gridControl.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex;
				gridControl.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
				//gridControl.DisplayLayout.Override.RowSelectorStyle = HeaderStyle.Standard;
			}
			else
			{
				gridControl.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			}
		}

		/// <summary>
		/// Set UltraGrid Editing Options
		/// </summary>
		/// <param name="allowAdd"></param>
		/// <param name="allowUpdate"></param>
		/// <param name="allowDelete"></param>
		public static void SetGridEditingOptions(UltraGrid gridControl, bool allowAdd, bool allowUpdate, bool allowDelete)
		{
			gridControl.DisplayLayout.Override.AllowAddNew = allowAdd ? AllowAddNew.Yes : AllowAddNew.Yes;
			gridControl.DisplayLayout.Override.AllowDelete = allowDelete ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.AllowUpdate = allowUpdate ? DefaultableBoolean.True : DefaultableBoolean.False;
			gridControl.DisplayLayout.Override.CellClickAction = allowUpdate ? CellClickAction.EditAndSelectText : CellClickAction.RowSelect;
		}

		/// <summary>
		/// Return type of element (Header, row, cell,...) on MouseUp event of grid control
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="point"></param>
		/// <returns></returns>
		public static UIElementType GetUIElementOnMouseUp(UltraGrid gridControl, Point point)
		{
			Infragistics.Win.UIElement aUIElement = gridControl.DisplayLayout.UIElement.ElementFromPoint(point);

			if (aUIElement is HeaderUIElement || aUIElement.Parent is HeaderUIElement)
				return UIElementType.ColumnHeader;
			else if (aUIElement is RowUIElement || aUIElement.Parent is RowUIElement)
				return UIElementType.UltraGridRow;
			else if (aUIElement is CellUIElement || aUIElement.Parent is CellUIElement)
				return UIElementType.UltraGridCell;
			else
				return UIElementType.Other;
		}

		/// <summary>
		/// Return UltraGridRow object at the point of mouse up event.
		/// Return value could be null
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="point"></param>
		/// <returns></returns>
		public static UltraGridRow GetRowOnMouseUp(UltraGrid gridControl, Point point)
		{
			Infragistics.Win.UIElement aUIElement = gridControl.DisplayLayout.UIElement.ElementFromPoint(point);

			while (aUIElement != null && (!(aUIElement is RowUIElement)))
			{
				aUIElement = aUIElement.Parent;
			}

			if (aUIElement != null)
				return aUIElement.GetContext(typeof(UltraGridRow)) as UltraGridRow;
			return null;
		}

		/// <summary>
		/// Return UltraGridCell object at the point of mouse up event.
		/// Return value could be null
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="point"></param>
		/// <returns></returns>
		public static UltraGridCell GetCellOnMouseUp(UltraGrid gridControl, Point point)
		{
			Infragistics.Win.UIElement aUIElement = gridControl.DisplayLayout.UIElement.ElementFromPoint(point);
			while (aUIElement != null && (!(aUIElement is CellUIElement)))
			{
				aUIElement = aUIElement.Parent;
			}
			if (aUIElement != null)
				return aUIElement.GetContext(typeof(UltraGridCell)) as UltraGridCell;
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		public static UltraGridRow AddNewRow(UltraGrid gridControl)
		{
			return AddNewRow(gridControl, 0, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bandIndex"></param>
		/// <param name="defaultValues"></param>
		public static UltraGridRow AddNewRow(UltraGrid gridControl, int bandIndex, object[,] requiredData)
		{
			return InsertNewRow(gridControl, requiredData, gridControl.Rows.Count, true);
		}

		/// <summary>
		/// Add a row to grid control with default values
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="defaultValues"></param>
		public static UltraGridRow AddNewRow(UltraGrid gridControl, object[,] requiredData)
		{
			return AddNewRow(gridControl, 0, requiredData);
		}

		/// <summary>
		/// Insert a row to
		/// </summary>
		/// <param name="gridControl">The grid control</param>
		/// <param name="insertAfterActiveRow"></param>
		/// <returns></returns>
		public static UltraGridRow InsertNewRow(UltraGrid gridControl, object[,] requiredData, bool insertAfterActiveRow)
		{
			int curRowIndex = -1;

			if (gridControl.ActiveRow == null)
				if(gridControl.Rows.Count > 0)
					gridControl.ActiveRow = gridControl.Rows[gridControl.Rows.Count - 1];
			if(gridControl.ActiveRow != null)
				curRowIndex = gridControl.ActiveRow.ListIndex;

			int newIndex = curRowIndex == -1 ? 0 : (insertAfterActiveRow ? curRowIndex + 1 : curRowIndex);
			return InsertNewRow(gridControl, requiredData, newIndex, insertAfterActiveRow);			
		}

		/// <summary>
		/// Insert a row to
		/// </summary>
		/// <param name="gridControl">The grid control</param>
		/// <param name="insertAfterActiveRow"></param>
		/// <returns></returns>
		public static UltraGridRow InsertNewRow(UltraGrid gridControl, bool insertAfterActiveRow)
		{
			return InsertNewRow(gridControl, null, insertAfterActiveRow);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl">The grid control</param>
		/// <param name="rowIndex"></param>
		/// <returns></returns>
		public static UltraGridRow InsertNewRow(UltraGrid gridControl, int rowIndex)
		{
			return InsertNewRow(gridControl, null, rowIndex, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl">The grid control</param>
		/// <param name="rowIndex"></param>
		/// <returns></returns>
		public static UltraGridRow InsertNewRow(UltraGrid gridControl, object[,] requiredData, int rowIndex, bool insertAfter)
		{
			if (gridControl.DisplayLayout.Override.AllowAddNew == AllowAddNew.No)
				return null;

			DataTable gridDT = _GetGridDataSource(gridControl);
			if (gridDT == null)
			{
				MessageBox.Show("Grid DataSource Is Null");
				return null;
			}

			DataRow dr = gridDT.NewRow();
			if (requiredData != null)
			{
				for (int i = 0; i < requiredData.GetLength(0); i++)
				{
					dr[Convert.ToString(requiredData.GetValue(i, 0))] = requiredData.GetValue(i, 1);
				}
			}

			/// 
			/// Check deleted rows and set to index of row in DataTable
			/// 
			int deletedCount = 0;
			DataTable deletedData = gridDT.GetChanges(DataRowState.Deleted);
			if (deletedData != null)
			{
				if (deletedData.Rows.Count != 0)
				{
					for (int i = 0; i < gridDT.Rows.Count; i++)
					{
						if (gridDT.Rows[i].RowState == DataRowState.Deleted)
						{
							deletedCount++;
						}
					}
				}
			}

			///
			/// Insert to datatable at approviate index
			/// 
			gridDT.Rows.InsertAt(dr, rowIndex + deletedCount);

			///
			/// Rebinding
			/// 
			gridControl.DataSource = gridDT;

			///
			/// Activate the newly inserted row and return
			/// 
			gridControl.ActiveRow = gridControl.Rows[rowIndex];
			gridControl.PerformAction(UltraGridAction.EnterEditMode, false, false);
			//int k=0;
			//while (k < gridControl.ActiveRow.Cells.Count)
			//{
			//    if (gridControl.ActiveRow.Cells[k].Activation != Activation.AllowEdit)
			//        continue;

			//    gridControl.ActiveRow.Cells[k].Activate();
			//    gridControl.PerformAction(UltraGridAction.EnterEditMode, false, false);
			//    break;
			//}
			return gridControl.ActiveRow;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl">The grid control</param>
		/// <param name="rowIndex"></param>
		/// <param name="insertAfter"></param>
		/// <returns></returns>
		public static UltraGridRow InsertNewRow(UltraGrid gridControl, int rowIndex, bool insertAfter)
		{
			return InsertNewRow(gridControl, null, rowIndex, insertAfter);
		}

        /// <summary>
        /// ColumnsVisible
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="ColumnIndex"></param>
        public static void ColumnsVisible(UltraGrid gridControl, int ColumnIndex, bool Visible)
        {
            gridControl.DisplayLayout.Bands[0].Columns[ColumnIndex].Hidden = Visible;
        }

        /// <summary>
        /// ColumnsVisible
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="ColumnName"></param>
        public static void ColumnsVisible(UltraGrid gridControl, string ColumnName, bool Visible)
        {
            gridControl.DisplayLayout.Bands[0].Columns[ColumnName].Hidden = Visible;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		public static void DeleteCurrentRow(UltraGrid gridControl)
		{
			DeleteCurrentRow(gridControl, true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		public static void DeleteCurrentRow(UltraGrid gridControl, bool showConfirm)
		{
			if (gridControl.DisplayLayout.Override.AllowDelete == DefaultableBoolean.False)
				return;

			if (gridControl.ActiveRow == null)
				return;

			DeleteUltraGridRow(gridControl, gridControl.ActiveRow, showConfirm);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		public static void DeleteSelectedRows(UltraGrid gridControl)
		{
			if (gridControl.DisplayLayout.Override.AllowDelete == DefaultableBoolean.False)
				return;

			DataTable gridDT = _GetGridDataSource(gridControl);
			if (gridDT == null) return;

			///
			/// Get the permission in Form that contains this grid control
			/// 
			MenuData md = (MenuData)GetObjectPropertyValue(gridControl.FindForm(), "MenuData");
			bool deletePermission = md.Permissions.AllowDelete;

			if (gridControl.Selected.Rows.Count > 0)
			{
				int firstIndex = gridControl.Selected.Rows[0].Index;

				foreach (UltraGridRow selectedRow in gridControl.Selected.Rows)
				{
					if (!deletePermission)
						if (gridDT.Rows[selectedRow.ListIndex].RowState != DataRowState.Added)
							continue;

					selectedRow.Delete(false);
				}

				///
				/// Select a row
				/// 
				if (gridControl.Rows.Count == 0)
					firstIndex = -1;
				else if (firstIndex > gridControl.Rows.Count - 1)
					firstIndex--;
				if (firstIndex != -1)
				{
					gridControl.Rows[firstIndex].Activate();
				}
			}
			else if(gridControl.ActiveRow != null)
			{
				if (deletePermission || gridDT.Rows[gridControl.ActiveRow.ListIndex].RowState == DataRowState.Added)
					DeleteUltraGridRow(gridControl, gridControl.ActiveRow, false);
			}
		}

		/// <summary>
		/// private function to delete a row in grid
		/// </summary>
		/// <param name="ultraGridRow"></param>
		private static void DeleteUltraGridRow(UltraGrid gridControl, UltraGridRow ultraGridRow, bool showConfirm)
		{
			int nSelectedIndex = ultraGridRow.Index;
			gridControl.ActiveRow.Delete(showConfirm);

			///
			/// Set Row Selected
			/// 
			if (gridControl.Rows.Count == 0)
				nSelectedIndex = -1;
			else if (nSelectedIndex > gridControl.Rows.Count - 1)
				nSelectedIndex--;
			if (nSelectedIndex != -1)
			{
				gridControl.Rows[nSelectedIndex].Activate();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="editable"></param>
		public static void SetCellEditable(UltraGridCell cell, bool editable)
		{
			cell.Activation = editable ? Activation.AllowEdit : Activation.NoEdit;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="frm"></param>
        /// <returns></returns>
        public static bool GetGridSetting(Infragistics.Win.UltraWinGrid.UltraGrid gridControl, Form frm)
        {
            bool bResult = false;
            StringBuilder fileName = new StringBuilder(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(frm.GetType()).Location));
            fileName.Append("\\DAT\\");
            fileName.Append(frm.GetType().FullName);
            fileName.Append(".");
            fileName.Append(gridControl.Name);
            fileName.Append(".dat");
            if (System.IO.File.Exists(fileName.ToString()))
            {
                gridControl.DisplayLayout.LoadFromXml(fileName.ToString(), PropertyCategories.All);
                bResult = true;
            }
            return bResult;
        }
		#endregion

		#region Excel, Clipboard functinos

		

		/*
Function compareCells(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

Dim xCellData As CellData = x
Dim yCellData As CellData = y
' compare position of cell by row and column
If xCellData.RowIndex < yCellData.RowIndex Then

Return (-1)

ElseIf xCellData.RowIndex = yCellData.RowIndex Then

If xCellData.ColIndex < yCellData.ColIndex Then

Return (-1)

ElseIf xCellData.ColIndex = yCellData.ColIndex Then

Return (0)

Else

Return (+1)

End If

Else

Return (+1)

End If

End Function

		
		 * * */


		/// <summary>
		/// Export Grid내용 TO Excel
		/// </summary>
		/// <param name="exporter"></param>
		/// <param name="grid"></param>
		/// <param name="excelFileName"></param>
		/// <returns></returns>
		public static Workbook ExportExcel(UltraGridExcelExporter exporter, UltraGrid grid, string excelFileName)
		{
			return exporter.Export(grid, excelFileName);
		}

		/// <summary>
		/// Export Grid내용 TO Excel Sheet
		/// </summary>
		/// <param name="exporter"></param>
		/// <param name="grid"></param>
		/// <param name="excelFileName"></param>
		/// <param name="workBook"></param>
		/// <param name="sheetName"></param>
		/// <returns></returns>
		public static Worksheet ExportExcel(UltraGridExcelExporter exporter, UltraGrid grid, string excelFileName, Workbook workBook, string sheetName)
		{
			// Create a workbook first
			Worksheet ws1 = workBook.Worksheets.Add(sheetName);
			exporter.Export(grid, ws1);
			BIFF8Writer.WriteWorkbookToFile(workBook, excelFileName);
			return ws1;
		}

		/// <summary>
		/// Export Grid내용 TO Excel Sheet
		/// </summary>
		/// <param name="exporter"></param>
		/// <param name="grid"></param>
		/// <param name="excelFileName"></param>
		/// <param name="workBook"></param>
		/// <param name="sheetName"></param>
		/// <param name="startRow">0부터</param>
		/// <param name="startColumn">0부터</param>
		/// <returns></returns>
		public static Worksheet ExportExcel(UltraGridExcelExporter exporter, UltraGrid grid, string excelFileName, Workbook workBook, string sheetName, int startRow, int startColumn)
		{
			Worksheet ws1 = workBook.Worksheets.Add(sheetName);
			exporter.Export(grid, ws1, startRow, startColumn);
			BIFF8Writer.WriteWorkbookToFile(workBook, excelFileName);
			return ws1;
		}

		#endregion

		#region Internal Method

		private static void OnGridMouseMove(object sender, MouseEventArgs e)
		{
			UltraGrid gridControl = (UltraGrid)sender;
			FormSettings settings = UIHelper.GetFormSettings(gridControl);

			if (gridControl.Tag != null)
			{
				if (gridControl.Tag is UltraGridRow)
				{
					UltraGridRow uRow = (UltraGridRow)gridControl.Tag;
					uRow.Appearance.BackColor = uRow.IsAlternate ?
						settings.GridRowAlternateBackColor :
						settings.GridRowBackColor;
				}
			}

			UltraGridRow curRow = GetRowOnMouseUp(gridControl, new Point(e.X, e.Y));//gridControl.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));

			if (curRow != null)
			{
				curRow.Appearance.BackColor = settings.GridRowHoverBackColor;
				gridControl.Tag = curRow;
			}
		}

		private static void OnGridMouseLeave(object sender, EventArgs e)
		{
			UltraGrid gridControl = (UltraGrid)sender;
			FormSettings settings = UIHelper.GetFormSettings(gridControl);

			if (gridControl.Tag != null)
			{
				if (gridControl.Tag is UltraGridRow)
				{
					UltraGridRow uRow = (UltraGridRow)gridControl.Tag;
					uRow.Appearance.BackColor = uRow.IsAlternate ?
						settings.GridRowAlternateBackColor :
						settings.GridRowBackColor;

					gridControl.Tag = null;
				}
			}
	
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <returns></returns>
		private static DataTable _GetGridDataSource(UltraGrid gridControl)
		{
			object dataSource = gridControl.DataSource;
			string dataMember = gridControl.DataMember == null ? string.Empty : gridControl.DataMember;

			if (dataSource is DataTable)
			{
				return (DataTable)dataSource;
			}
			else if (dataSource is DataViewManager)
			{
				if (dataMember.Contains("."))
				{
					return ((DataViewManager)dataSource).DataSet.Relations[dataMember.Substring(dataMember.LastIndexOf(".") + 1)].ChildTable;
				}
				else
				{
					return ((DataViewManager)dataSource).DataSet.Tables[dataMember];
				}
			}
			else if (dataSource is DataSet)
			{
				return ((DataSet)dataSource).Tables[dataMember];
			}
			else if (dataSource is DataView)
			{
				return ((DataView)dataSource).Table;
			}
			else
			{
				return null;
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		/// <returns></returns>
		private static DataTable _GetGridDataTable(object dataSource, string dataMember)
		{
			if (dataSource is DataTable)
			{
				return (DataTable)dataSource;				
			}
			else if (dataSource is DataViewManager)
			{
				if (dataMember.Contains("."))
				{
					return ((DataViewManager)dataSource).DataSet.Relations[dataMember.Substring(dataMember.LastIndexOf(".") + 1)].ChildTable;
				}
				else
				{
					return ((DataViewManager)dataSource).DataSet.Tables[dataMember];
				}			
			}
			else if (dataSource is DataSet)
			{
				return ((DataSet)dataSource).Tables[dataMember];
			}
			else if (dataSource is DataView)
			{
				return ((DataView)dataSource).Table;
			}
			else
			{
				return null;
			}
		}

        private static void OnGridKeyUpKeyDown(object sender, KeyEventArgs e)
        {
            //TODO:그리드 에디트모드에서 업/다운키 설정
            UltraGrid gridControl = (UltraGrid)sender;
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    if (gridControl.ActiveCell == null) break;
                    if (gridControl.ActiveCell.IsInEditMode)
                    {
                        e.SuppressKeyPress = true;
                    }
                    break;
                default:
                    break;
            }
        }

		private static void OnGridKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Handled)
				return;

			UltraGrid gridControl = (UltraGrid)sender;

			switch (e.KeyCode)
			{
				//case Keys.Up:

				//    if (gridControl.ActiveCell == null) break;

				//    switch (gridControl.ActiveCell.Column.Style)
				//    {
				//        case ColumnStyle.DropDown:
				//        case ColumnStyle.DropDownCalendar:
				//        case ColumnStyle.DropDownList:
				//        case ColumnStyle.DropDownValidate:
				//            break;

				//        default:
				//            //e.Handled = true;
				//            gridControl.PerformAction(UltraGridAction.PrevRow, false, false);
				//            //e.SuppressKeyPress = true; 
				//            break;
				//    }
					
				//    break;

				//case Keys.Down:
				//    if (gridControl.ActiveCell == null) break;

				//    switch (gridControl.ActiveCell.Column.Style)
				//    {
				//        case ColumnStyle.DropDown:
				//        case ColumnStyle.DropDownCalendar:
				//        case ColumnStyle.DropDownList:
				//        case ColumnStyle.DropDownValidate:
				//            break;
				//        default:
				//            //e.Handled = true;
				//            gridControl.PerformAction(UltraGridAction.NextRow, false, false);
				//            //e.SuppressKeyPress = true;
				//            break;
				//    }
				//    break;

				case Keys.Enter:

					///
					/// Exit  Edit mode if now is in edit mode, except multi-line column
					/// if the grid is not in edit mode, move to next right cell
					///
					if (gridControl.ActiveCell == null)
						break;


					if (!(gridControl.ActiveCell.IsInEditMode &&
						gridControl.ActiveCell.Column.CellMultiLine == DefaultableBoolean.True))
					{
						gridControl.PerformAction(UltraGridAction.NextCellByTab, false, false);
						e.Handled = true;
					}

					/*
					if (gridControl.ActiveCell.IsInEditMode)
					{						
						if (gridControl.ActiveCell.Column.CellMultiLine != DefaultableBoolean.True)
						{
							gridControl.PerformAction(UltraGridAction.ExitEditMode, false, false);
							e.Handled = true;
						}
					}
					else
					{
						if (!gridControl.ActiveRow.IsGroupByRow)
						{
							gridControl.PerformAction(UltraGridAction.NextCell, false, false);
							e.Handled = true;
						}
					}
					 * */

					break;

				case Keys.Space: 
				
					/// Enter Edit Mode or Reverse Checked if ActiveCell is a CheckBox
					if (gridControl.DisplayLayout.Override.AllowUpdate == DefaultableBoolean.False)
						return;
					
					if (gridControl.ActiveRow == null)
						break;

					if (gridControl.ActiveCell == null)
					{
						gridControl.PerformAction(UltraGridAction.FirstCellInRow, false, false);
						gridControl.PerformAction(UltraGridAction.ActivateCell, false, false);						
					}

					if (gridControl.ActiveCell == null) 
						break;

					gridControl.PerformAction(UltraGridAction.EnterEditMode, false, false);

					if (gridControl.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox)
					{
						//gridControl.ActiveCell.Value = !Convert.ToBoolean(gridControl.ActiveCell.Column.DefaultCellValue);
						gridControl.PerformAction(UltraGridAction.ToggleCheckbox, false, false);
					}

					e.Handled = true;

					break;

				case Keys.F3:

					///
					/// Click the button in EditButton, Button cell or Show Popup or show DropDown
					///

					if (gridControl.ActiveCell == null)
						break;

					//UIElement parent = gridControl.ActiveCell.GetUIElement();
					switch (gridControl.ActiveCell.Column.Style)
					{
						case ColumnStyle.DropDown:
						case ColumnStyle.DropDownCalendar:
						case ColumnStyle.DropDownList:
						case ColumnStyle.DropDownValidate:
							if (gridControl.ActiveCell.IsInEditMode)
							{
								gridControl.PerformAction(UltraGridAction.ToggleDropdown, false, false);
							}
							else
							{
								gridControl.PerformAction(UltraGridAction.EnterEditMode, false, false);
								gridControl.PerformAction(UltraGridAction.ToggleDropdown, false, false);
							}
							e.Handled = true;
							break;

						default:
							break;
					}

					break;
				
				case Keys.C:
				//    /// 
				//    /// Ctrl+C
				//    /// 
				//    if (e.Control)
				//    {
				//        DoCopyToClipBoard(gridControl);
				//    }
				//    break;
				//case Keys.V:
				//    ///
				//    /// Ctrl+V
				//    /// 
				//    if (e.Control)
				//    {
				//        DoPasteFromClipBoard(gridControl);
				//    }
				//    break;

				default:
					break;
			}		
		}

		/// <summary>
		/// Copy selected cells' values to clipboard
		/// </summary>
		/// <param name="gridControl"></param>
		private static void DoCopyToClipBoard(UltraGrid gridControl)
		{
			//create a string builder to contain the output CSV text
			StringBuilder sbCSV = new StringBuilder();


			try
			{

				if (gridControl.Selected.Cells.Count > 0)
				{
					//put cell data into an array for sorting
					CellData[] cdCells = new CellData[gridControl.Selected.Cells.Count];
					Int64 lngPtr = 0;
					Int64 lngLowColIndex = long.MaxValue;
					Int64 lngHighColIndex = 0;

					//pass and process selected cells collection
					foreach (UltraGridCell cellSelected in gridControl.Selected.Cells)
					{
						//set row and column values
						cdCells[lngPtr].RowIndex = cellSelected.Row.Index;
						cdCells[lngPtr].ColIndex = cellSelected.Column.Index;

						//retrieve text from cell based on string type
						switch (cellSelected.Column.DataType.ToString())
						{
							case "System.String":
								{
									cdCells[lngPtr].Text = cellSelected.Value.ToString();
									break;
								}
							case "System.Decimal":
								{
									decimal decValue = UIHelper.ConvertToDecimal(cellSelected.Value.ToString());
									cdCells[lngPtr].Text = decValue.ToString();
									break;
								}
						}

						//update low and high column index
						if (cdCells[lngPtr].ColIndex < lngLowColIndex)
							lngLowColIndex = cdCells[lngPtr].ColIndex;
						if (cdCells[lngPtr].ColIndex > lngHighColIndex)
							lngHighColIndex = cdCells[lngPtr].ColIndex;

						//increment array pointer
						lngPtr++;
					}

					//display cell values before sorting
					int intPtr;
					//for (intPtr = cdCells.GetLowerBound(0); intPtr <= cdCells.GetUpperBound(0); intPtr++)
					//{
					//    Console.WriteLine(cdCells[intPtr].RowIndex.ToString() + " " + cdCells[intPtr].ColIndex.ToString() + " " + cdCells[intPtr].Text);
					//}
					//Console.WriteLine();

					//sort the array on row and cell
					Array.Sort(cdCells, new CellComparer());

					////display cell values after sorting
					//for (intPtr = cdCells.GetLowerBound(0); intPtr <= cdCells.GetUpperBound(0); intPtr++)
					//{
					//    Console.WriteLine(cdCells[intPtr].RowIndex.ToString() + " " + cdCells[intPtr].ColIndex.ToString() + " " + cdCells[intPtr].Text);
					//}
					//Console.WriteLine();

					//put into CSV format
					long lngCurrentRow = cdCells[0].RowIndex;
					long lngCurrentCol = lngLowColIndex;
					for (intPtr = cdCells.GetLowerBound(0); intPtr <= cdCells.GetUpperBound(0); intPtr++)
					{
						//test for row change
						if (cdCells[intPtr].RowIndex != lngCurrentRow)
						{
							sbCSV.Remove(sbCSV.Length - 1, 1); //remove trailing comma
							sbCSV.Append(Environment.NewLine);
							lngCurrentRow = cdCells[intPtr].RowIndex;
							lngCurrentCol = lngLowColIndex;
						}

						//test for padding needed to this column
						if (lngCurrentCol < cdCells[intPtr].ColIndex)
						{
							Int64 lngPads = cdCells[intPtr].ColIndex - lngCurrentCol;
							Int64 lngPtr2;
							for (lngPtr2 = 0; lngPtr2 < lngPads; lngPtr2++)
							{
								lngCurrentCol++;
								sbCSV.Append(",");
							}
						}

						//add value for this cell
						sbCSV.Append(cdCells[intPtr].Text + ",");
						lngCurrentCol++;
					}

					//add last new line
					sbCSV.Remove(sbCSV.Length - 1, 1); //remove trailing comma
					sbCSV.Append(Environment.NewLine);
				}
				else
				{
					if (gridControl.Selected.Rows.Count > 0)
					{
						foreach (UltraGridRow aRow in gridControl.Selected.Rows)
						{
							foreach (UltraGridCell aCell in aRow.Cells)
							{
								switch (aCell.Column.DataType.ToString())
								{
									case "System.String":
										{
											sbCSV.Append(aCell.Value + ",");
											break;
										}
									case "System.Decimal":
										{
											decimal decValue = UIHelper.ConvertToDecimal(aCell.Value.ToString());
											sbCSV.Append(decValue.ToString() + ",");
											break;
										}
								}
							}

							sbCSV.Append(Environment.NewLine);
						}
					}
				}

				//add a c-string terminator for those applilcations like Excel that like that kind of thing
				sbCSV.Append((char)0);
				//Console.WriteLine(sbCSV.ToString());

				//convert string to byte array
				Byte[] byteData = Encoding.UTF8.GetBytes(sbCSV.ToString());

				//put byte array into memory stream
				MemoryStream msData = new MemoryStream(byteData);

				//put memory stream into data object as csv format
				DataObject doClipboard = new DataObject();
				doClipboard.SetData(DataFormats.CommaSeparatedValue, true, msData);
				Clipboard.SetDataObject(doClipboard, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		private static void DoPasteFromClipBoard(UltraGrid gridControl)
		{
			if (gridControl.ActiveCell == null) return;

			try
			{
				// retrieve data from clipboard
				DataObject doClipboard = Clipboard.GetDataObject() as DataObject;

				////print all available clipboard formats
				//string[] arrayOfFormats = doClipboard.GetFormats(true);
				//for (int i = 0; i < arrayOfFormats.Length; i++)
				//{
				//    Console.WriteLine(arrayOfFormats[i]);
				//}

				//test for CSV data available
				if (doClipboard.GetDataPresent(DataFormats.CommaSeparatedValue, true) == true)
				{
					MemoryStream msClipBoard = (MemoryStream)doClipboard.GetData(DataFormats.CommaSeparatedValue, true);
					//Console.WriteLine("Length=" + msClipBoard.Length.ToString());

					UltraGridCell ugcWork = gridControl.ActiveCell;

					//process each row of memory stream
					StreamReader srClipboard = new StreamReader(msClipBoard);
					string strLine = srClipboard.ReadLine();
					//Console.WriteLine(strLine);
					clsCSV csvDecoder = new clsCSV();
					while (strLine != null)
					{
						while (strLine != "")
						{
							switch (ugcWork.Column.DataType.ToString())
							{
								case "System.String":
									{
										ugcWork.Value = csvDecoder.GetNextField(ref strLine);
										break;
									}
								case "System.Decimal":
									{
										try
										{
											ugcWork.Value = UIHelper.ConvertToDecimal(csvDecoder.GetNextField(ref strLine));
										}
										catch
										{
											ugcWork.Value = 0;
										}
										break;
									}
								default:
									{
										//swallow unused field
										csvDecoder.GetNextField(ref strLine);
										break;
									}
							}

							//retrieve reference to next cell
							try
							{
								ugcWork = ugcWork.Row.Cells[ugcWork.Column.Index + 1];
							}
							catch
							{
								break;
							}
						}

						//get next row of incoming CSV data
						strLine = srClipboard.ReadLine();

						//get rid of c-string termination character
						if (char.Parse(strLine) == (char)0)
							strLine = "";

						//retrieve first cell of next row
						if (strLine != null)
						{
							if (ugcWork.Row.HasNextSibling() == true)
							{
								UltraGridRow rowNext = ugcWork.Row.GetSibling(SiblingRow.Next);
								ugcWork = rowNext.Cells[gridControl.ActiveCell.Column.Index];
							}
						}
					}
				}					
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}

		/// <summary>
		/// Call the WinFormBase's EnableToolbarButton method
		/// </summary>
		/// <param name="buttonIndex"></param>
		/// <param name="status"></param>
		private static void EnableToolBarButton(UltraGrid gridControl, WinFormToolbarButtonIndex buttonIndex, bool enable)
		{
			try
			{
				Form form = gridControl.FindForm();
				MethodInfo mi = form.GetType().GetMethod("EnableToolBarButton", BindingFlags.NonPublic | BindingFlags.Instance);
				if (mi != null)
					mi.Invoke(form, new object[] { (int)buttonIndex, enable });
			}
			catch { }
		}

		/// <summary>
		/// Get the value of an object's property
		/// </summary>
		/// <param name="theObject"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		private static object GetObjectPropertyValue(object theObject, string propertyName)
		{
			if (theObject == null) return null;

			object controlValue = null;

			PropertyInfo pi = theObject.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
			if (pi != null && pi.CanRead)
			{
				controlValue = pi.GetValue(theObject, null);
			}

			return controlValue;
		}

		/// <summary>
		/// Get the value of an object's property
		/// </summary>
		/// <param name="theObject"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		private static object GetObjectPropertyValue(object theObject, Type objectType, string propertyName)
		{
			if (theObject == null) return null;

			object controlValue = null;

			PropertyInfo pi = objectType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
			if (pi != null && pi.CanRead)
			{
				controlValue = pi.GetValue(theObject, null);
			}

			return controlValue;
		}

		/// <summary>
		/// Internal Binding method, all DataBind method use this
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="dtData"></param>
		private static void _DataBind(UltraGrid gridControl, object dtData)
		{
			_DataBind(gridControl, dtData, "Table");
		}

		/// <summary>
		/// Internal Binding method, all DataBind method use this
		/// </summary>
		/// <param name="gridControl"></param>
		/// <param name="dtData"></param>
		private static void _DataBind(UltraGrid gridControl, object dtData, string dataMember)
		{
			object[] columns = gridControl.DisplayLayout.Bands[0].Columns.All;

			Hashtable befColumns = new Hashtable();
			for (int i = 0; i < columns.Length; i++)
			{
				if (((UltraGridColumn)columns[i]).Hidden) continue;

				befColumns.Add(((UltraGridColumn)columns[i]).Key, null);
			}

			gridControl.DataSource = dtData;		
            if (!(dtData is DataTable)) gridControl.DataMember = dataMember;			
            DataTable _dtData = _GetGridDataTable(dtData, dataMember);
			//gridControl.DataSource = _dtData;

			columns = gridControl.DisplayLayout.Bands[0].Columns.All;
			for (int i = 0; i < columns.Length; i++)
			{
				UltraGridColumn column = (UltraGridColumn)columns[i];
				if (!befColumns.Contains(column.Key))
					column.Hidden = true;

				if (_dtData == null) continue;
				if (!_dtData.Columns.Contains(column.Key)) continue;
				_dtData.Columns[column.Key].AllowDBNull = true;
				_dtData.Columns[column.Key].ReadOnly = false;
				if (_dtData.Columns[column.Key].MaxLength != -1)
					column.MaxLength = _dtData.Columns[column.Key].MaxLength;
			}

			/////
			///// Clear all flags
			///// 
			//dtData.AcceptChanges();


			///
			/// Start binding
			/// 
			//gridControl.DataBind();
			 
			///
			/// Finalize
			/// 
			befColumns = null;
			columns = null;
			//boundColumns = null;			

			/// 
			/// clear checkbox
			/// 
			//ClearHeaderCheckBox(gridControl);
			///
			///	Create Filter to display CheckBox on Header
			///	
			UltraGridCreationFilter filter = new UltraGridCreationFilter();

			// Attach an event handler for when the CheckBox in the column header is clicked.
			filter.CheckChanged += new UltraGridCreationFilter.HeaderCheckBoxClickedHandler(OnHeaderCheckBoxCheckChanged);

			// Assign the creation filter to the grid's CreationFilter property.
			gridControl.CreationFilter = filter;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridControl"></param>
		public static void ClearHeaderCheckBox(UltraGrid gridControl)
		{
			/// 
			/// if has checkboxes, reset the header's status
			/// 			
			for (int i = 0; i < gridControl.DisplayLayout.Bands.Count; i++)
			{
				foreach (UltraGridColumn gridColumn in gridControl.DisplayLayout.Bands[i].Columns)
				{
					if (gridColumn.Style == ColumnStyle.CheckBox)
					{
						HeaderUIElement headerUIElement = (HeaderUIElement)gridColumn.Header.GetUIElement();
						if (headerUIElement != null)
						{
							CheckBoxUIElement checkBoxUIElement =
									headerUIElement.GetDescendant(typeof(CheckBoxUIElement))
									as CheckBoxUIElement;
							if (checkBoxUIElement != null)
							{
								checkBoxUIElement.CheckState = CheckState.Unchecked;
							}
						}
					}
				}
			}	
		}

		/// <summary>
		/// Never display delete confirmation message
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void OnGridBeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
		{
			e.DisplayPromptMsg = false;
		}

		/// <summary>
		/// Grid Validation 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void OnGridValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UltraGrid gridControl = (UltraGrid)sender;
			gridControl.DisplayLayout.Override.SupportDataErrorInfo = SupportDataErrorInfo.RowsAndCells;
		}

		/// <summary>
		/// Disable/Endable DeleteRowButton in Form
		/// </summary>
		private static void OnGridControlAfterRowActivate(object sender, EventArgs e)
		{
			UltraGrid gridControl = (UltraGrid)sender;
			if (gridControl.ActiveRow == null || gridControl.Selected.Rows == null)
				return;

			if (gridControl.ActiveRow.ListObject == null)
			{
				return;
			}

			DataTable gridDT = _GetGridDataSource(gridControl);
			if (gridDT == null) return;

			///
			/// Get the permission in Form that contains this grid control
			/// 
			Form form = gridControl.FindForm();
			MenuData md = (MenuData)GetObjectPropertyValue(form, "MenuData");
			if (md == null)
			{
				return;
			}
			
			bool deletePermission = md.Permissions.AllowDelete;

			if (!deletePermission)
			{
				// if this row is an existing row, disable the toolbar button
				if ((gridControl.DisplayLayout.Override.SelectTypeRow == SelectType.Extended ||
					gridControl.DisplayLayout.Override.SelectTypeRow == SelectType.ExtendedAutoDrag)
					&& gridControl.Selected.Rows.Count > 1)
				{
					EnableToolBarButton(gridControl, WinFormToolbarButtonIndex.DELETEROW_BUTTON, false);
				}
				else
				{
					bool enable = gridControl.ActiveRow.IsAddRow || ((DataRowView)gridControl.ActiveRow.ListObject).Row.RowState == DataRowState.Added;
					EnableToolBarButton(gridControl, WinFormToolbarButtonIndex.DELETEROW_BUTTON, enable);
				}
			}
		}

		private static void OnHeaderCheckBoxCheckChanged(object sender, UltraGridCreationFilter.HeaderCheckBoxEventArgs e)
		{
			// Loop over all of the rows in the first band and set the value of the cell in the 
			// column whose header's CheckBox was clicked.

			UltraGridRow selectedRow = e.Grid.ActiveRow;
			UltraGridCell selectedCell = e.Grid.ActiveCell;

			///
			/// Begin updating
			/// 
			e.Grid.BeginUpdate();

			foreach (UltraGridRow row in e.Grid.Rows)
			{
				CheckEditor checkEditor = e.Column.Editor as CheckEditor;				
				if (checkEditor == null)
				{
					continue;
				}

				row.Cells[e.Column.Key].Activate();
				e.Grid.PerformAction(UltraGridAction.EnterEditMode, false, false);
				checkEditor.CheckState = e.CurrentCheckState;
				e.Grid.PerformAction(UltraGridAction.ExitEditMode, false, false);
			}

			e.Grid.ActiveRow = selectedRow;
			e.Grid.ActiveCell = selectedCell;

			///
			/// Refresh grid
			/// 
			e.Grid.EndUpdate();
		}

		#endregion

	}

	struct CellData
	{
		public long RowIndex;
		public long ColIndex;
		public String Text;		
	}

	/// <summary>
	/// Summary description for clsCSV.
	/// </summary>
	class clsCSV
	{
		public clsCSV()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string GetNextField(ref string v_strLine)
		{
			// NOTE: This function does not take into account string fields containing commas
			// or Quoted string fields

			Int64 lngComma = v_strLine.IndexOf(",", 0);
			if (lngComma <= 0)
			{
				string strField = v_strLine;
				v_strLine = "";
				return strField;
			}
			else
			{
				string strField = v_strLine.Substring(0, (int)lngComma);
				v_strLine = v_strLine.Substring((int)lngComma + 1);
				return strField;
			}
		}
	}

	class CellComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			CellData xCellData = (CellData)x;
			CellData yCellData = (CellData)y;

			//compare position of cell by row and column
			if (xCellData.RowIndex < yCellData.RowIndex)
			{
				return -1;
			}
			else
			{
				if (xCellData.RowIndex == yCellData.RowIndex)
				{
					if (xCellData.ColIndex < yCellData.ColIndex)
					{
						return -1;
					}
					else
					{
						if (xCellData.ColIndex == yCellData.ColIndex)
						{
							return 0;
						}
						else
						{
							return 1;
						}
					}
				}
				else
				{
					return 1;
				}
			}
		}
	}
}

