using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Bifrost.Common
{
	/// <summary>
	/// Class to store settings on Form
	/// </summary>
	public class FormSettings
	{
		public FormSettings(SubSystemType subSysType)
		{

            string theme = ConfigSettings.GetStringSetting(XPropertyConverter.EnumToString(typeof(SubSystemType), subSysType));

			ErrorBackColor = ConfigSettings.GetColorSetting("ErrorBackColor");
			BorderColor = ConfigSettings.GetColorSetting("BorderColor");

            FormBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.FormBackColor", theme));
            ToolBarBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.ToolBarBackColor", theme));
            WorkAreaBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.WorkAreaBackColor", theme));
            ButtonBorderColor = ConfigSettings.GetColorSetting(string.Format("{0}.ButtonBorderColor", theme));
            ButtonBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.ButtonBackColor", theme));
            ButtonHoverColor = ConfigSettings.GetColorSetting(string.Format("{0}.ButtonHoverColor", theme));
            HeaderBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.HeaderBackColor", theme));
            GroupBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.GroupBackColor", theme));
            TDLabelBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.TDLabelBackColor", theme));
            TDControlBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.TDControlBackColor", theme));

            GridBorderColor = ConfigSettings.GetColorSetting(string.Format("{0}.GridBorderColor", theme));
            GridGroupByBoxBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.GridGroupByBoxBackColor", theme));
            GridHeaderBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.GridHeaderBackColor", theme));
            GridRowHoverBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.GridRowHoverBackColor", theme));
            GridRowAlternateBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.GridRowAlternateBackColor", theme));
            GridRowBackColor = ConfigSettings.GetColorSetting(string.Format("{0}.GridRowBackColor", theme));
		}

		private Color _FormBackColor;

		/// <summary>
		/// Form BackColor
		/// </summary>
		public Color FormBackColor
		{
			get { return _FormBackColor; }
			set { _FormBackColor = value; }
		}
	

		private Color _ToolBarBackColor;

		/// <summary>
		/// Background color
		/// </summary>
		public Color ToolBarBackColor
		{
			get { return _ToolBarBackColor; }
			set { _ToolBarBackColor = value; }
		}

		private Color _WorkAreaBackColor;

		/// <summary>
		/// Color in pnlContainer
		/// </summary>
		public Color WorkAreaBackColor
		{
			get { return _WorkAreaBackColor; }
			set { _WorkAreaBackColor = value; }
		}

		private Color _ErrorBackColor;

		/// <summary>
		/// BackColor of control to show on validation error
		/// </summary>
		public Color ErrorBackColor
		{
			get { return _ErrorBackColor; }
			set { _ErrorBackColor = value; }
		}

		private Color _BorderColor;

		/// <summary>
		/// Border Color of Panel
		/// </summary>
		public Color BorderColor
		{
			get { return _BorderColor; }
			set { _BorderColor = value; }
		}

		private Color _ButtonBorderColor;

		/// <summary>
		/// BorderColor of Button
		/// </summary>
		public Color ButtonBorderColor
		{
			get { return _ButtonBorderColor; }
			set { _ButtonBorderColor = value; }
		}

		private Color _ButtonBackColor;

		/// <summary>
		/// Normal BackColor of button
		/// </summary>
		public Color ButtonBackColor
		{
			get { return _ButtonBackColor; }
			set { _ButtonBackColor = value; }
		}

		private Color _ButtonHoverColor;

		/// <summary>
		/// BackColor of button on hovering, XButton & Toolbar buttons
		/// </summary>
		public Color ButtonHoverColor
		{
			get { return _ButtonHoverColor; }
			set { _ButtonHoverColor = value; }
		}

		private Color _HeaderBackColor;

		/// <summary>
		/// BackColor of Header Panel, XHeadePanel
		/// </summary>
		public Color HeaderBackColor
		{
			get { return _HeaderBackColor; }
			set { _HeaderBackColor = value; }
		}

		private Color _GroupBackColor;

		/// <summary>
		/// GroupPanel Back Color, XGroupPanel
		/// </summary>
		public Color GroupBackColor
		{
			get { return _GroupBackColor; }
			set { _GroupBackColor = value; }
		}

		private Color _TDLabelBackColor;

		/// <summary>
		/// BackColor of XLabel
		/// </summary>
		public Color TDLabelBackColor
		{
			get { return _TDLabelBackColor; }
			set { _TDLabelBackColor = value; }
		}


		private Color _TDControlBackColor;

		/// <summary>
		/// BackColor of TD containing controlss
		/// </summary>
		public Color TDControlBackColor
		{
			get { return _TDControlBackColor; }
			set { _TDControlBackColor = value; }
		}

		private Color _GridBorderColor;

		/// <summary>
		/// UltraGrid Border Color
		/// </summary>
		public Color GridBorderColor
		{
			get { return _GridBorderColor; }
			set { _GridBorderColor = value; }
		}

		private Color _GridGroupByBoxBackColor;

		/// <summary>
		/// BackColor of GroupByBox
		/// </summary>
		public Color GridGroupByBoxBackColor
		{
			get { return _GridGroupByBoxBackColor; }
			set { _GridGroupByBoxBackColor = value; }
		}
	

		private Color _GridHeaderBackColor;

		/// <summary>
		/// UltraGrid Header BackColor
		/// </summary>
		public Color GridHeaderBackColor
		{
			get { return _GridHeaderBackColor; }
			set { _GridHeaderBackColor = value; }
		}

		private Color _GridRowHoverBackColor;

		/// <summary>
		/// UltraGrid RowHovering color
		/// </summary>
		public Color GridRowHoverBackColor
		{
			get { return _GridRowHoverBackColor; }
			set { _GridRowHoverBackColor = value; }
		}

		private Color _GridRowAlternateBackColor;

		/// <summary>
		/// UltraGrid alternative row backcolor
		/// </summary>
		public Color GridRowAlternateBackColor
		{
			get { return _GridRowAlternateBackColor; }
			set { _GridRowAlternateBackColor = value; }
		}

		private Color _GridRowBackColor;

		/// <summary>
		/// UltraGrid Row Back Color
		/// </summary>
		public Color GridRowBackColor
		{
			get { return _GridRowBackColor; }
			set { _GridRowBackColor = value; }
		}
	
	}
}
