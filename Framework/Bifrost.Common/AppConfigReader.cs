using System;
using System.Collections.Generic;
using System.Text;

namespace Bifrost.Common
{
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	public sealed partial class AppConfigReader : global::System.Configuration.ApplicationSettingsBase
	{

		private static AppConfigReader defaultInstance = new AppConfigReader();

		public static AppConfigReader Default
		{
			get
			{
				return defaultInstance;
			}
		}
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="settingKey"></param>
		/// <returns></returns>
		public string GetStringSetting(string settingKey)
		{
			return ((string)(this[settingKey]));
		}

        /// <summary>
        /// AS WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/AS/NF.AS.WSL.ASWebService/ASWebService.asmx")]
        public string NFASWebService
        {
            get
            {
                return ((string)(this["NFASWebService"]));
            }
        }

        /// <summary>
        /// CL WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/CL/NF.CL.WSL.CLWebService/CLWebService.asmx")]
        public string NFCLWebService
        {
            get
            {
                return ((string)(this["NFCLWebService"]));
            }
        }

        /// <summary>
        /// AS WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/ISYWebService/CM/NF.CM.WSL.CMWebService/CMWebService.asmx")]
        public string NFCMWebService
        {
            get
            {
                return ((string)(this["NFCMWebService"]));
            }
        }

        /// <summary>
        /// CO WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/CO/NF.CO.WSL.COWebService/COWebService.asmx")]
        public string NFCOWebService
        {
            get
            {
                return ((string)(this["NFCOWebService"]));
            }
        }

		/// <summary>
		/// FD WebService URL
		/// </summary>
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
		[global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/FD/NF.FD.WSL.FDWebService/FDWebService.asmx")]
		public string NFFDWebService
		{
			get
			{
				return ((string)(this["ISYWebService"]));
			}
		}
        
        /// <summary>
        /// FI WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/FI/NF.FI.WSL.FIWebService/FIWebService.asmx")]
        public string NFFIWebService
        {
            get
            {
                return ((string)(this["NFFIWebService"]));
            }
        }

        /// <summary>
        /// HR WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/HR/NF.HR.WSL.HRWebService/HRWebService.asmx")]
        public string NFHRWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }

        /// <summary>
        /// MM WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/MM/NF.MM.WSL.MMWebService/MMWebService.asmx")]
        public string NFMMWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }

        /// <summary>
        /// PM WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/PM/NF.PM.WSL.PMWebService/PMWebService.asmx")]
        public string NFPMWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }

        /// <summary>
        /// PP WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/PP/NF.PP.WSL.PPWebService/PPWebService.asmx")]
        public string NFPPWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }


		/// <summary>
		/// QC WebService URL
		/// </summary>
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
		[global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/QC/NF.QC.WSL.QCWebService/QCWebService.asmx")]
		public string NFQCWebService
		{
			get
			{
				return ((string)(this["ISYWebService"]));
			}
		}


        /// <summary>
        /// SC WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/SC/NF.SC.WSL.SCWebService/SCWebService.asmx")]
        public string NFSCWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }

        /// <summary>
        /// SD WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/SD/NF.SD.WSL.SDWebService/SDWebService.asmx")]
        public string NFSDWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }

        /// <summary>
        /// SD WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/ISYWebService/SY/NF.SY.WSL.SYWebService/SYWebService.asmx")]
        public string NFSYWebService
        {
            get
            {
                return ((string)(this["NFSYWebService"]));
            }
        }

        /// <summary>
        /// TE WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/TE/NF.TE.WSL.TEWebService/TEWebService.asmx")]
        public string NFTEWebService
        {
            get
            {
                return ((string)(this["ISYWebService"]));
            }
        }

        /// <summary>
        /// TI WebService URL
        /// </summary>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/TI/NF.TI.WSL.TIWebService/TIWebService.asmx")]
        public string NFTIWebService
        {
            get
            {
                return ((string)(this["NFTIWebService"]));
            }
        }

        

        /// <summary>
		/// NF File Manager Service URL
		/// </summary>
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
		[global::System.Configuration.DefaultSettingValueAttribute("http://localhost/NFWebService/CO/NF.CO.WSL.COWebService/FMService.asmx")]
		public string NFFileManagerWebService
		{
			get
			{
				return ((string)(this["NFFileManagerWebService"]));
			}
		}

		/// <summary>
		/// Resource Location Flag
		/// </summary>
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
		[global::System.Configuration.DefaultSettingValueAttribute("False")]
		public bool UseDBResource
		{
			get
			{
				return ((bool)(this["UseDBResource"]));
			}
		}
	}
}
