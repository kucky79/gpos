using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Bifrost.Win
{
	/// <summary>
	/// This class helps to solve the MS .NET bug with PageSetupDialog control
	/// http://support.microsoft.com/?id=814355
	/// Until this bug is solved, CMAX has to use this class
	/// </summary>
	public class PageSetupDialogHelper
	{
		//'<summary>
		//' Invokes the ShowDialog method by first ensuring the page margins are set correctly
		//' regardless of the current user's measurement system. This fixes the conversion
		//' bug inside the PageSetupDialog class.
		//'</summary>
		//'<param name="dlg">The dialog whose ShowDialog method will be called.</param>
		//'<param name="owner">Optional owner window of the dialog.</param>
		//'<returns>Returns the dlg.ShowDialog return value.</returns>
		public static DialogResult ShowDialog(PageSetupDialog dlg, IWin32Window owner)
		{
			if (dlg == null)
			{
				throw new ArgumentNullException("PageSetupDialog control must be set");
			}
			if (dlg.Document == null)
			{
				throw new ArgumentNullException("Document control must be set");
			}

			Margins savedMargins = dlg.Document.DefaultPageSettings.Margins;
			if (PageSetupDialogHelper.IsMetric)
			{
				dlg.Document.DefaultPageSettings.Margins = PrinterUnitConvert.Convert(dlg.Document.DefaultPageSettings.Margins, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
			}

			DialogResult result = dlg.ShowDialog(owner);
			if (result != DialogResult.OK)
			{
				dlg.Document.DefaultPageSettings.Margins = savedMargins;
			}
			else
			{
				dlg.Document.DefaultPageSettings = dlg.PageSettings;
				dlg.Document.PrinterSettings = dlg.PrinterSettings;
			}
			return result;
		}

		public static DialogResult ShowDialog(PageSetupDialog dlg)
		{
			return ShowDialog(dlg, null);
		}

		//'<summary>
		//' Returns True is the user is currently working in metric measurement system. Returns False
		//' for US system. Takes into account the current "Measurement system" setting in the Regional Options
		//' CP applet.
		//'</summary>
		//'<returns>True if metric system is set, False for US system.</returns>
		public static bool IsMetric
		{
			get 
			{ 				
				// Get the "real" measurement system for the current user. We have to go through
				// API here, because the System.Globalization.RegionInfo.CurrentRegion.IsMetric returns
				// a value for the "whole" region, but the user could specificaly set measurement
				// system to other than the default for the given region.
				int lcid = PageSetupDialogHelper.GetUserDefaultLCID();
				StringBuilder info = new StringBuilder(10);
				int result = PageSetupDialogHelper.GetLocaleInfo(lcid, LOCALE_IMEASURE, info, info.Capacity);
				if (result == 0)
				{
					// The call failed; there isn't much we can do here, so we'll default to the RegionInfo's value.
					return System.Globalization.RegionInfo.CurrentRegion.IsMetric;
				}
				else
				{
					return info.ToString() == "0";
				}
            }
		}

		#region Win32 APIs
		[System.Runtime.InteropServices.DllImport("Kernel32")]
		private static extern int GetUserDefaultLCID();

		private const int LOCALE_IMEASURE = 0xD;
		//[System.Runtime.InteropServices.DllImport("Kernel32", CharSet=System.Runtime.InteropServices.CharSet.Auto,SetLastError=true)]
		[System.Runtime.InteropServices.DllImport("Kernel32")]
		private static extern int GetLocaleInfo(int lcid, int lctype, System.Text.StringBuilder lcdata, int cchData);
		#endregion
	}
}
