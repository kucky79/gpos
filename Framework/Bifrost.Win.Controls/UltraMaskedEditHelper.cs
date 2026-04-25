using System;
using System.Collections.Generic;
using System.Text;

namespace CMAX.Framework.Win.Controls
{
	public class UltraMaskedEditHelper
	{
		public const string FORMAT_MASK_CURRENCY = "-nnn,nnn,nnn.nn";

		public const string FORMAT_MASK_SHORT_TIME = "{hh-mm}";
		public const string FORMAT_MASK_SHORT_DATE = "{date}";
		public const string FORMAT_MASK_NORMAL_DATE = "{yyyy-MM-dd}";
		public const string FORMAT_MASK_LONG_DATE = ">?<&&&&&&&&& 9#, 99##";
		public const string FORMAT_MASK_YEAR_MONTH = "{yyyy-MM}";
		public const string FORMAT_MASK_SHORT_YEAR_MONTH = "{yy-MM}";
		public const string FORMAT_MASK_TELEPHONE_1 = "###-###-####";
		public const string FORMAT_MASK_TELEPHONE_2 = "(###) ###-####";
		public const string FORMAT_MASK_SOCIAL_SECURITY = "###-##-####";
		public const string FORMAT_MASK_ZIP_CODE = "#####-9999";
		public const string FORMAT_MASK_CUSTOM = "Custom...";

		public const string FORMAT_VALUE_CURRENCY = "#,##0.##";
		public static readonly DateTime FORMAT_VALUE_SHORT_DATE = new DateTime(2005, 1, 1);
		public const string FORMAT_VALUE_NORMAL_DATE = "2004-01-01";
		public const string FORMAT_VALUE_LONG_DATE = "January 1, 2004";
		public const string FORMAT_VALUE_TELEPHONE = "8005551212";
		public const string FORMAT_VALUE_SOCIAL_SECURITY = "123456789";
		public const string FORMAT_VALUE_ZIP_CODE = "123459876";
		public const string FORMAT_VALUE_CUSTOM = "";

	}
}
