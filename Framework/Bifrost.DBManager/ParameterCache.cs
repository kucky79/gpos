using Bifrost.Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost
{
    internal class ParameterInfo
    {
        public string ParameterName;
        public SqlDbType SqlDataType;
        public ParameterDirection Direction;
        public int Size;
        public byte Precision;
        public byte Scale;
    }

    internal class ParameterCache
    {
        bool isOracle = false;
        Dictionary<string, ParameterInfo[]> caches = new Dictionary<string, ParameterInfo[]>();
        ParameterCache pcache = null;

        public DbParameter[] GetParameters(ref DbCommand cmd, string spName)
        {
            string key = spName.ToUpper();

            if (!caches.ContainsKey(key))
            {
                DataTable dt = GetParametersFromServer(ref cmd, key);

                lock (this)
                {
                    CacheRefresh(key, dt);
                }
            }
            else
            {
            }

            return GetDbParameters(key);

        }

        DataTable GetParametersFromServer(ref DbCommand cmd, string spName)
        {
            DbParameter para = null;

            para = new SqlParameter("@P_SPNAME", SqlDbType.VarChar, 255);
            para.Value = spName;
            ((SqlCommand)cmd).Parameters.Add(para);

            DbDataAdapter da = null;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        DbParameter[] GetDbParameters(string key)
        {
            ParameterInfo[] items = caches[key];
            DbParameter[] arr = null;

            arr = new SqlParameter[items.Length];

            DbParameter p = null;

            for (int i = 0; i < items.Length; i++)
            {
                p = new SqlParameter();
                p.ParameterName = items[i].ParameterName;

                SqlParameter sqlp = p as SqlParameter;
                sqlp.SqlDbType = items[i].SqlDataType;
                sqlp.Precision = items[i].Precision;
                sqlp.Scale = items[i].Scale;


                p.Direction = items[i].Direction;
                p.Size = items[i].Size;

                arr[i] = p;
            }

            return arr;
        }

        void CacheRefresh(string key, DataTable dt)
        {
            if (caches.ContainsKey(key))
                caches.Remove(key);

            if (dt == null)
                return;

            List<ParameterInfo> list = new List<ParameterInfo>();

            foreach (DataRow dRow in dt.Rows)
            {
                ParameterInfo para = new ParameterInfo();

                para.ParameterName = (string)dRow["PNAME"];

                para.SqlDataType = GetSqlDataType(Convert.ToInt32(dRow["PTYPE"]));

                int size = Convert.ToInt32(dRow["PSIZE"]);

                if (!isOracle)
                {
                    if (para.SqlDataType == SqlDbType.NChar || para.SqlDataType == SqlDbType.NVarChar)
                        para.Size = size / 2;
                    else
                        para.Size = size;
                }
                else
                    para.Size = size;

                if (dt.Columns.Contains("XPRECISION") == true && dt.Columns.Contains("XSCALE"))
                {
                    para.Precision = dRow["XPRECISION"] == DBNull.Value ? (byte)0 : Convert.ToByte(dRow["XPRECISION"]);
                    para.Scale = dRow["XSCALE"] == DBNull.Value ? (byte)0 : Convert.ToByte(dRow["XSCALE"]);
                }
                else
                {
                    para.Precision = 0;
                    para.Scale = 0;
                }

                int direction = Convert.ToInt32(dRow["PDIRECTION"]);
                if (direction == 1)
                    para.Direction = ParameterDirection.Input;
                else if (direction == 2)
                    para.Direction = ParameterDirection.Output;
                else if (direction == 3)
                    para.Direction = ParameterDirection.InputOutput;

                list.Add(para);
            }

            caches.Add(key, list.ToArray());
        }
    
        public void DeleteAll()
        {
            if (caches == null)
                return;

            caches.Clear();
        }

        SqlDbType GetSqlDataType(int ptype)
        {
            switch (ptype)
            {
                case 34:
                    return SqlDbType.Image;
                case 56:
                    return SqlDbType.Int;
                case 127:
                    return SqlDbType.BigInt;
                case 52:
                    return SqlDbType.SmallInt;
                case 48:
                    return SqlDbType.TinyInt;
                case 62:
                    return SqlDbType.Float;
                case 59:
                    return SqlDbType.Real;
                case 175:
                    return SqlDbType.Char;
                case 167:
                    return SqlDbType.VarChar;
                case 239:
                    return SqlDbType.NChar;
                case 231:
                    return SqlDbType.NVarChar;
                case 241:
                    return SqlDbType.Xml;
                case 106:
                case 108:
                    return SqlDbType.Decimal;
                case 104:
                    return SqlDbType.Bit;
                case 61:
                    return SqlDbType.DateTime;
                case 58:
                    return SqlDbType.SmallDateTime;
                case 35:
                    return SqlDbType.Text;
                case 99:
                    return SqlDbType.NText;
                default:
                    return SqlDbType.Variant;
            }
        }

        //#region Cached Parameter Setting

        public void CachePrepareCommandSql(ref DbCommand cmd, DbParameter[] cacheParas, string cmdText, CommandType cmdType, object[] paraValues)
        {
            if (cmd.Parameters != null)
                cmd.Parameters.Clear();

            foreach (SqlParameter para in cacheParas)
            {
                if (para.Direction == ParameterDirection.InputOutput)
                    para.Direction = ParameterDirection.Output;
            }

            // 값셋팅
            if (paraValues != null)
            {
                int i = 0;
                int length = paraValues.Length;

                foreach (SqlParameter para in cacheParas)
                {
                    if (para.Direction == ParameterDirection.ReturnValue)
                        continue;

                    if (para.Direction == ParameterDirection.Input)
                    {
                        if (paraValues[i] == null)
                        {
                            if (para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Int || para.SqlDbType == SqlDbType.SmallInt || para.SqlDbType == SqlDbType.TinyInt || para.SqlDbType == SqlDbType.SmallMoney || para.SqlDbType == SqlDbType.BigInt || para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Float || para.SqlDbType == SqlDbType.Money || para.SqlDbType == SqlDbType.Real)
                                para.Value = 0;
                            else
                                para.Value = "";
                        }
                        else
                        {
                            para.Value = paraValues[i];
                        }

                        cmd.Parameters.Add(para);   // 커멘트 객체에 파라미터를 대입
                    }

                    i = i + 1;  // Output 파라미터가 있어도 순서대로 루프를 돌려야 하기 때문에...

                    if (i == length)
                        break;
                }
            }

            // Output 파라미터가 있는지 체크
            foreach (SqlParameter para in cacheParas)
            {
                if (para.Direction == ParameterDirection.Output)
                {
                    if (para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Int || para.SqlDbType == SqlDbType.SmallInt || para.SqlDbType == SqlDbType.TinyInt || para.SqlDbType == SqlDbType.SmallMoney || para.SqlDbType == SqlDbType.BigInt || para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Float || para.SqlDbType == SqlDbType.Money || para.SqlDbType == SqlDbType.Real)
                        para.Value = 0;
                    else
                        para.Value = "";

                    cmd.Parameters.Add(para);   // 커멘트 객체에 파라미터를 대입
                }
            }
        }

        public OutParameters CachePrepareCommandSqlForSave(ref DbCommand cmd, DbParameter[] cacheParas, string spName, DataTable dt, string[] colNames)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return null;

            OutParameters outParameters = new OutParameters();

            foreach (SqlParameter para in cacheParas)
            {
                if (para.Direction == ParameterDirection.InputOutput)   // 저장프로시저의 OUTPUT 파라미터가 이상하게 이걸로 인식되버려서
                    para.Direction = ParameterDirection.Output;
            }

            if (colNames != null)
            {
                int i = 0;
                int length = colNames.Length;

                foreach (SqlParameter para in cacheParas)
                {
                    if (para.Direction == ParameterDirection.Input)
                    {
                        cmd.Parameters.Add(para);

                        i = i + 1;

                        if (i == length)
                            break;
                    }
                }

                // Output 파라미터가 있는지 체크
                foreach (SqlParameter para in cacheParas)
                {
                    if (para.Direction == ParameterDirection.Output)
                    {
                        if (para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Int || para.SqlDbType == SqlDbType.SmallInt || para.SqlDbType == SqlDbType.TinyInt || para.SqlDbType == SqlDbType.SmallMoney || para.SqlDbType == SqlDbType.BigInt || para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Float || para.SqlDbType == SqlDbType.Money || para.SqlDbType == SqlDbType.Real)
                            para.Value = 0;
                        else
                            para.Value = "";

                        cmd.Parameters.Add(para);   // 커멘트 객체에 파라미터를 대입
                    }
                }
            }

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                if (colNames != null)
                {
                    int i = 0;
                    int length = colNames.Length;

                    foreach (SqlParameter para in cmd.Parameters)
                    {
                        if (para.Direction == ParameterDirection.Input)
                        {
                            para.Value = dt.Rows[row][colNames[i]];

                            i = i + 1;

                            if (i == length)
                                break;
                        }
                    }

                    // Output 파라미터는 한번더
                    foreach (SqlParameter para in cmd.Parameters)
                    {
                        if (para.Direction == ParameterDirection.Output)
                        {
                            if (para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Int || para.SqlDbType == SqlDbType.SmallInt || para.SqlDbType == SqlDbType.TinyInt || para.SqlDbType == SqlDbType.SmallMoney || para.SqlDbType == SqlDbType.BigInt || para.SqlDbType == SqlDbType.Decimal || para.SqlDbType == SqlDbType.Float || para.SqlDbType == SqlDbType.Money || para.SqlDbType == SqlDbType.Real)
                                para.Value = 0;
                            else
                                para.Value = "";
                        }
                    }
                }

                cmd.ExecuteNonQuery();

                foreach (SqlParameter para in cmd.Parameters)
                {
                    if (para.Direction == ParameterDirection.Output)
                    {
                        outParameters.ColumnsAdd(para.ParameterName);
                        outParameters[row, para.ParameterName] = para.Value;
                    }
                }

            }

            return outParameters;
        }

    }
}
