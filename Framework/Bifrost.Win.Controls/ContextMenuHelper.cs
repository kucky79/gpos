using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

using System.Data;
using System.Data.SqlClient;

using CMAX.Framework.Common;
using CMAX.Framework.Win;
using CMAX.Framework.Win.Controls.CreationFilters;

namespace CMAX.Framework.Win.Controls
{
	public class ContextMenuHelper
	{
		#region DataBinding

		/// <summary>
		/// Bind dataset to ContextMenuStrip control
		/// </summary>
		/// <param name="treeControl"></param>
		/// <param name="dataSource">DataTable contains all menus</param>
		/// <param name="menuKeyField">Database field contains key data for each menu</param>
		/// <param name="menuParentField">Database field contains key data for each menu's parent</param>
		/// <param name="menuTextField">Database field contains text data for each menu</param>
		/// <param name="topMenuKeyValue">menuParentField value of top-most menus</param>
		/// <param name="menuSeqField">Menu sequence field</param>
		/// <param name="menuLevelField">Menu level field</param>
		public static void DataBind(ContextMenu menuControl,
			DataTable dataSource,
			string menuKeyField,
			string menuParentField,
			string menuTextField,
			string topMenuKeyValue,
			string menuSeqField,
			string menuLevelField)
		{
			///
			/// Apply default style
			/// 
			//ApplyDefaultStyle(treeControl);

			///
			/// Starting rendering
			/// 
			//treeControl.Menus.Clear();			
			menuControl.MenuItems.Clear();

			StringBuilder sb = new StringBuilder();
			if (menuSeqField != string.Empty)
				sb.AppendFormat("{0}", menuLevelField);
			if (menuLevelField != string.Empty)
			{
				if (sb.ToString() != string.Empty)
					sb.AppendFormat(", {0}", menuSeqField);
				else
					sb.AppendFormat("{0}", menuSeqField);
			}

			DataRow[] childRows;
			string sortBy = sb.ToString();
			childRows = sortBy != string.Empty ?
				dataSource.Select(string.Format("{0}='{1}'", menuParentField, topMenuKeyValue), sortBy) :
				dataSource.Select(string.Format("{0}='{1}'", menuParentField, topMenuKeyValue));

			_AddMenus(menuControl, menuKeyField,
			menuParentField,
			menuTextField, null, dataSource, childRows, sortBy);

		}


		#region Privates

		private static void _AddMenus(ContextMenu menuControl,
			string menuKeyField,
			string menuParentField,
			string menuTextField,
			MenuItem pMenu,
			DataTable allMenus,
			DataRow[] childItems,
			string sortBy)
		{			
			foreach (DataRow dataRow in childItems)
			{
				MenuItem childItem = new MenuItem(Convert.ToString(dataRow[menuTextField]));
				childItem.Name = Convert.ToString(dataRow[menuKeyField]);
				childItem.Tag = dataRow;

				DataRow[] childRows = sortBy == string.Empty ? allMenus.Select(string.Format("{0}='{1}'", menuParentField, Convert.ToString(dataRow[menuKeyField]))) :
					allMenus.Select(string.Format("{0}='{1}'", menuParentField, Convert.ToString(dataRow[menuKeyField])), sortBy);

				if (pMenu == null)
				{
					menuControl.MenuItems.Add(childItem);
				}
				else
				{
					pMenu.MenuItems.Add(childItem);

				}

				_AddMenus(menuControl, menuKeyField, menuParentField, menuTextField, childItem, allMenus, childRows, sortBy);
			}			
		}

		#endregion DataBinding
		#endregion
	}
}
