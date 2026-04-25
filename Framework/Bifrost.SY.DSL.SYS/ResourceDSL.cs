using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Bifrost.Framework;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;
using System.Xml;

namespace Bifrost.SY.DSL.SYS
{
    public class ResourceDSL : DSLBase
    {
        #region Privates
		/// <summary>
		/// 서브시스템
		/// </summary>
		private SubSystemType subType = SubSystemType.SYS;

		#endregion Privates

		public ResourceDSL():base(){}

		/// <summary>
		///	다른 Connection을 얻기 위해서 사용되는 생성자
		/// </summary>
		public ResourceDSL(SubSystemType subSystem):base(subSystem){}

        /// <summary>
        /// Load resource from 10.102 db server
        /// </summary>
        /// <param name="FirmCode"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        public DataSet LoadResources(string FirmCode, string cultureName)
        {
            TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			DataSet dsResult = null;

            try
            {
                dsResult = DBHelper.GetDataSet("AP_SYS_DD_S", new object[] { FirmCode, cultureName });
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsResult;
        }
    }
}
