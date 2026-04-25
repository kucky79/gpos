using Bifrost.Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bifrost.Common
{
    public class DB
    {
        private static DB instance = null;

        private DB()
        {
        }

        public static DB Getinstance()
        {
            if (instance == null)
                instance = new DB();

            return instance;
        }

        public static void ReSet()
        {
            DbAgent.ReSet();
        }

        public static void ReSet(string connectString)
        {
            DbAgent.ReSet(connectString);
        }


        public ResultData ExecSp(string spname, object[] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData result = agent.FillResultTable(spname, System.Data.CommandType.StoredProcedure, parameters);
            agent.CommitTransaction();
            return result;
        }

        public object ExecuteScalar(SpInfo si)
        {
            DbAgent agent = DbAgent.GetInstance();

            string spName = si.SpNameSelect;
            object[] paras = si.SpParamsSelect;

            return agent.ExecuteScalar(spName, CommandType.StoredProcedure, paras);
        }

   
        public object ExecuteScalar(string query)
        {
            DbAgent agent = DbAgent.GetInstance();

            return agent.ExecuteScalar(query, CommandType.Text, null);
        }

        public DataSet FillDataSet(SpInfoCollection spCollection)
        {
            DbAgent agent = DbAgent.GetInstance();

            return agent.FillDataSet(spCollection);
        }

        public DataSet FillDataSet(string query)
        {
            DbAgent agent = DbAgent.GetInstance();

            return agent.FillDataSet(query);
        }

        public ResultData FillResultSet(string spName, object[] paras)
        {
            DbAgent agent = DbAgent.GetInstance();

            return agent.FillResultSet(spName, paras);
        }

        public ResultData FillResultTable(SpInfo si)
        {
            if (si == null)
                return null;

            string spName = si.SpNameSelect;
            object[] paras = si.SpParamsSelect;

            DbAgent agent = DbAgent.GetInstance();

            return agent.FillResultTable(spName, System.Data.CommandType.StoredProcedure, paras);
        }

        public DataTable FillDataTable(string query)
        {
            DbAgent agent = DbAgent.GetInstance();
            return agent.FillDataTable(query, CommandType.Text, null);
        }

        public DataTable FillDataTable(string query, bool remainLog)
        {
            DbAgent agent = DbAgent.GetInstance();
            return agent.FillDataTable(query, CommandType.Text, null);
        }

        public ResultData Save(SpInfo si)
        {
            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData retValue = agent.Save(si);
            agent.CommitTransaction();
            return retValue;
        }

        public ResultData[] Save(SpInfoCollection spCollection)
        {
            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData[] retValue = agent.Save(spCollection);
            agent.CommitTransaction();
            return retValue;
        }
    }
}
