using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

using System.Data;
using System.Data.SqlClient;

using NF.Framework.Common;
using NF.Framework.Win;
using NF.Framework.Win.Controls;

using Infragistics.Win;
using Infragistics.Win.UltraWinScrollBar;
using Infragistics.Win.UltraWinTree;
using Appearance = Infragistics.Win.Appearance;


namespace NF.Framework.Win.Controls
{
	public class UltraTreeHelper
	{
		#region DataBinding

		/// <summary>
		/// Bind dataset to UltraTree control
		/// </summary>
		/// <param name="treeControl"></param>
		/// <param name="dataSource">DataTable contains all nodes</param>
		/// <param name="nodeKeyField">Database field contains key data for each node</param>
		/// <param name="nodeParentField">Database field contains key data for each node's parent</param>
		/// <param name="nodeTextField">Database field contains text data for each node</param>
		/// <param name="topNodeKeyValue">nodeParentField value of top-most nodes</param>
		public static void DataBind(UltraTree treeControl,
			DataTable dataSource,
			string nodeKeyField,
			string nodeParentField,
			string nodeTextField,
			string topNodeKeyValue)
		{
			DataBind(treeControl, dataSource, nodeKeyField, nodeParentField, nodeTextField, topNodeKeyValue, string.Empty, string.Empty);
		}

		/// <summary>
		/// Bind dataset to UltraTree control
		/// </summary>
		/// <param name="treeControl"></param>
		/// <param name="dataSource">DataTable contains all nodes</param>
		/// <param name="nodeKeyField">Database field contains key data for each node</param>
		/// <param name="nodeParentField">Database field contains key data for each node's parent</param>
		/// <param name="nodeTextField">Database field contains text data for each node</param>
		/// <param name="topNodeKeyValue">nodeParentField value of top-most nodes</param>
		/// <param name="nodeSeqField">Node sequence field</param>
		/// <param name="nodeLevelField">Node level field</param>
		public static void DataBind(UltraTree treeControl,
			DataTable dataSource,
			string nodeKeyField,
			string nodeParentField,
			string nodeTextField,
			string topNodeKeyValue,
			UltraTreeNode rootNode,
			string nodeSeqField,
			string nodeLevelField)
		{
			///
			/// Begin rendering
			/// 
			treeControl.BeginUpdate();

			///
			/// Apply default style
			/// 
			ApplyDefaultStyle(treeControl);

			///
			/// Starting rendering
			/// 
			//treeControl.Nodes.Clear();			

			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(nodeLevelField))
				sb.AppendFormat("{0}", nodeLevelField);
			if (string.IsNullOrEmpty(nodeSeqField))
			{
				if (sb.ToString() != string.Empty)
					sb.AppendFormat(", {0}", nodeSeqField);
				else
					sb.AppendFormat("{0}", nodeSeqField);
			}

			DataRow[] childRows;
			string sortBy = sb.ToString();
			childRows = sortBy != string.Empty ?
				dataSource.Select(string.Format("{0}='{1}'", nodeParentField, topNodeKeyValue), sortBy) :
				dataSource.Select(string.Format("{0}='{1}'", nodeParentField, topNodeKeyValue));

			_AddTreeNodes(treeControl, nodeKeyField,
			nodeParentField,
			nodeTextField, rootNode, dataSource, childRows, sortBy, null);

			///
			/// Finish rendering
			/// 
			treeControl.EndUpdate();
		}

		/// <summary>
		/// Bind dataset to UltraTree control
		/// </summary>
		/// <param name="treeControl"></param>
		/// <param name="dataSource">DataTable contains all nodes</param>
		/// <param name="nodeKeyField">Database field contains key data for each node</param>
		/// <param name="nodeParentField">Database field contains key data for each node's parent</param>
		/// <param name="nodeTextField">Database field contains text data for each node</param>
		/// <param name="topNodeKeyValue">nodeParentField value of top-most nodes</param>
		/// <param name="nodeSeqField">Node sequence field</param>
		/// <param name="nodeLevelField">Node level field</param>
		public static void DataBind(UltraTree treeControl,
			DataTable dataSource,
			string nodeKeyField,
			string nodeParentField,
			string nodeTextField,
			string topNodeKeyValue,
			string nodeSeqField,
			string nodeLevelField)
		{
			DataBind(treeControl, dataSource,
				nodeKeyField, nodeParentField,
				nodeTextField, topNodeKeyValue,
				null, nodeSeqField, nodeLevelField);
		}


		/// <summary>
		/// Bind dataset to UltraTree control
		/// </summary>
		/// <param name="treeControl"></param>
		/// <param name="dataSource">DataTable contains all nodes</param>
		/// <param name="nodeKeyField">Database field contains key data for each node</param>
		/// <param name="nodeParentField">Database field contains key data for each node's parent</param>
		/// <param name="nodeTextField">Database field contains text data for each node</param>
		/// <param name="topNodeKeyValue">nodeParentField value of top-most nodes</param>
		/// <param name="nodeSeqField">Node sequence field</param>
		/// <param name="nodeLevelField">Node level field</param>
		public static void DataBind(UltraTree treeControl,
			DataTable dataSource,
			string nodeKeyField,
			string nodeParentField,
			string nodeTextField,
			string topNodeKeyValue,
			string nodeSeqField,
			string nodeLevelField,
			Image[] nodeLeftImages)
		{
			///
			/// Begin rendering
			/// 
			treeControl.BeginUpdate();

			///
			/// Apply default style
			/// 
			ApplyDefaultStyle(treeControl);

			///
			/// Starting rendering
			/// 
			//treeControl.Nodes.Clear();			

			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(nodeLevelField))
				sb.AppendFormat("{0}", nodeLevelField);
			if (string.IsNullOrEmpty(nodeSeqField))
			{
				if (sb.ToString() != string.Empty)
					sb.AppendFormat(", {0}", nodeSeqField);
				else
					sb.AppendFormat("{0}", nodeSeqField);
			}

			DataRow[] childRows;
			string sortBy = sb.ToString();
			childRows = sortBy != string.Empty ?
				dataSource.Select(string.Format("{0}='{1}'", nodeParentField, topNodeKeyValue), sortBy) :
				dataSource.Select(string.Format("{0}='{1}'", nodeParentField, topNodeKeyValue));

			_AddTreeNodes(treeControl, nodeKeyField,
			nodeParentField,
			nodeTextField, null, dataSource, childRows, sortBy, nodeLeftImages);

			///
			/// Finish rendering
			/// 
			treeControl.EndUpdate();
		}

		#endregion

		#region Style, Layout

		public static void ApplyDefaultStyle(UltraTree treeControl)
		{
			//treeControl.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.Standard;
			treeControl.Scrollable = Scrollbar.ShowIfNeeded;
			treeControl.Appearance.BackColor = Color.FromArgb(252, 251, 246);

			ScrollBarLook treeScrollbarLook = new ScrollBarLook();
			Appearance treeScrollAppearance = new Appearance();
			treeScrollAppearance.BackColor = System.Drawing.Color.FromArgb(245, 254, 253);
			treeScrollAppearance.BorderColor3DBase = treeScrollAppearance.BorderColor = System.Drawing.Color.FromArgb(129, 186, 239);
			treeScrollbarLook.Appearance = treeScrollAppearance;

			treeControl.ScrollBarLook = treeScrollbarLook;
		}

		#endregion

		#region Privates

		private static void _AddTreeNodes(UltraTree treeControl,
			string nodeKeyField,
			string nodeParentField,
			string nodeTextField,
			UltraTreeNode pNode, 
			DataTable allNodes, 
			DataRow[] childNodes,
			string sortBy, Image[] nodeLeftImages)
		{
			foreach (DataRow dataRow in childNodes)
			{
				UltraTreeNode childNode = new UltraTreeNode(Convert.ToString(dataRow[nodeKeyField]), Convert.ToString(dataRow[nodeTextField]));
				childNode.Tag = dataRow;

				DataRow[] childRows = sortBy == string.Empty ? allNodes.Select(string.Format("{0}='{1}'", nodeParentField, Convert.ToString(dataRow[nodeKeyField]))) :
					allNodes.Select(string.Format("{0}='{1}'", nodeParentField, Convert.ToString(dataRow[nodeKeyField])), sortBy);

				if (pNode == null)
				{
					treeControl.Nodes.Add(childNode);
				}
				else
				{
					pNode.Nodes.Add(childNode);

				}

				_AddTreeNodes(treeControl, nodeKeyField, nodeParentField, nodeTextField, childNode, allNodes, childRows, sortBy, nodeLeftImages);
			}


			if (nodeLeftImages != null && pNode != null)
			{
				if (pNode.Nodes.Count != 0)
				{
					pNode.LeftImages.Add(nodeLeftImages[0]);
				}
				else
				{
					pNode.LeftImages.Add(nodeLeftImages[1]);
				}
			}
		}

		#endregion
	}
}
