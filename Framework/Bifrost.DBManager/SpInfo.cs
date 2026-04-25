using Bifrost.CommonFunction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Bifrost
{
    #region SpInfo Class
    [Serializable]
    public class SpInfo : ISerializable 
    {
        #region Private Variables
        private string m_MenuID = string.Empty;
        private string m_spNameInsert = string.Empty;
        private string m_spNameUpdate = string.Empty;
        private string m_spNameDelete = string.Empty;
        private string m_spNameSelect = string.Empty;
        private string m_spNameNonQuery = string.Empty;
        private object m_dataValue = null;
        private string[] m_spParamsInsert = null;
        private string[] m_spParamsUpdate = null;
        private string[] m_spParamsDelete = null;
        private object[] m_spParamsNonQuery = null;
        private object[] m_spParamsSelect = null;
        private string m_FirmCode = string.Empty;
        private string m_userID = string.Empty;
        private string m_detailToday = string.Empty;
        private SingleValueCollection m_singleValues = null;
        private DataValueState m_dataValueState = DataValueState.NoAccept;
        #endregion

        #region Constructor
        public SpInfo() { }

        public SpInfo(string MenuID, string spInsert, string spUpdate, string spDelete, string spSelect, object data, string[] insertParams, string[] updateParams, string[] deleteParams, object[] selectParams, string FirmCode, string userId)
        {
            this.m_MenuID = MenuID;
            this.m_spNameInsert = spInsert;
            this.m_spNameUpdate = spUpdate;
            this.m_spNameDelete = spDelete;
            this.m_spNameSelect = spSelect;
            this.m_dataValue = data;
            this.m_spParamsInsert = insertParams;
            this.m_spParamsUpdate = updateParams;
            this.m_spParamsDelete = deleteParams;
            this.m_spParamsSelect = selectParams;
            this.m_FirmCode = FirmCode;
            this.m_userID = userId;
        }

        public SpInfo(string MenuID, string spInsert, string spUpdate, string spDelete, object data, string[] insertParams, string[] updateParams, string[] deleteParams, string FirmCode, string userId) 
            : this(MenuID, spInsert, spUpdate, spDelete, null, data, insertParams, updateParams, deleteParams, null, FirmCode, userId) { }

        public SpInfo(string MenuID, string spSelect, object[] selectParams, string FirmCode, string userId) 
            : this(MenuID, null, null, null, spSelect, null, null, null, null, selectParams, FirmCode, userId) { }

        public SpInfo(SerializationInfo info, StreamingContext context)
        {
            try
            {
                m_MenuID = (string)info.GetValue("m_MenuID", typeof(String));
                m_spNameInsert = (string)info.GetValue("m_spNameInsert", typeof(String));
                m_spNameUpdate = (string)info.GetValue("m_spNameUpdate", typeof(String));
                m_spNameDelete = (string)info.GetValue("m_spNameDelete", typeof(String));
                m_spNameSelect = (string)info.GetValue("m_spNameSelect", typeof(String));
                m_spNameNonQuery = (string)info.GetValue("m_spNameNonQuery", typeof(String));
                m_spParamsInsert = (string[])info.GetValue("m_spParamsInsert", typeof(string[]));
                m_spParamsUpdate = (string[])info.GetValue("m_spParamsUpdate", typeof(string[]));
                m_spParamsDelete = (string[])info.GetValue("m_spParamsDelete", typeof(string[]));
                m_spParamsSelect = (object[])info.GetValue("m_spParamsSelect", typeof(object[]));
                m_spParamsNonQuery = (object[])info.GetValue("m_spParamsNonQuery", typeof(object[]));
                m_dataValue = (object)info.GetValue("m_dataValue", typeof(object));
                m_FirmCode = (string)info.GetValue("m_FirmCode", typeof(string));
                m_userID = (string)info.GetValue("m_userID", typeof(string));
                m_detailToday = (string)info.GetValue("m_detailToday", typeof(string));

                this.m_singleValues = (SingleValueCollection)info.GetValue("m_singleValues", typeof(SingleValueCollection));
                this.m_dataValueState = (DataValueState)info.GetValue("m_dataValueState", typeof(DataValueState));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("SpInfo를 Serialiaze하는 도중 오류가 발생하였습니다.", ex);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("m_MenuID", m_MenuID);
                info.AddValue("m_spNameInsert", m_spNameInsert);
                info.AddValue("m_spNameUpdate", m_spNameUpdate);
                info.AddValue("m_spNameDelete", m_spNameDelete);
                info.AddValue("m_spNameSelect", m_spNameSelect);
                info.AddValue("m_spNameNonQuery", m_spNameNonQuery);
                info.AddValue("m_spParamsInsert", m_spParamsInsert);
                info.AddValue("m_spParamsUpdate", m_spParamsUpdate);
                info.AddValue("m_spParamsDelete", m_spParamsDelete);
                info.AddValue("m_spParamsSelect", m_spParamsSelect);
                info.AddValue("m_spParamsNonQuery", m_spParamsNonQuery);
                info.AddValue("m_dataValue", m_dataValue);
                info.AddValue("m_FirmCode", m_FirmCode);
                info.AddValue("m_userID", m_userID);
                info.AddValue("m_detailToday", m_detailToday);
                info.AddValue("m_singleValues", m_singleValues);
                info.AddValue("m_dataValueState", m_dataValueState);

            }
            catch (Exception ex)
            {
                //Trace.WriteLine(String.Format("SpInfo GetObjectData : {0}", ex));
                //	throw ex;
            }
        }
        #endregion

        #region Property
        /// <summary>
        /// 
        /// </summary>
        public SingleValueCollection SpParamsValues
        {
            get
            {
                if (this.m_singleValues == null)
                    m_singleValues = new SingleValueCollection();
                return m_singleValues;
            }
        }

        /// <summary>
        /// 데이터 추가 저장 프로시저 이름을 가져오거나 설정합니다.
        /// </summary>
        public string SpNameInsert
        {
            get { return this.m_spNameInsert; }
            set { this.m_spNameInsert = value; }
        }

        /// <summary>
        /// 데이터 수정 저장 프로시저 이름을 가져오거나 설정합니다.
        /// </summary>
        public string SpNameUpdate
        {
            get { return this.m_spNameUpdate; }
            set { this.m_spNameUpdate = value; }
        }

        /// <summary>
        /// 데이터 삭제 저장프로시저 이름을 가져오거나 설정합니다.
        /// </summary>
        public string SpNameDelete
        {
            get { return this.m_spNameDelete; }
            set { this.m_spNameDelete = value; }
        }

        /// <summary>
        /// 데이터 조회 저장프로시저 이름을 가져오거나 설정합니다.
        /// </summary>
        public string SpNameSelect
        {
            get { return this.m_spNameSelect; }
            set { this.m_spNameSelect = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SpNameNonQuery
        {
            get { return this.m_spNameNonQuery; }
            set { this.m_spNameNonQuery = value; }
        }

        public DataValueState DataState
        {
            get { return this.m_dataValueState; }
            set { this.m_dataValueState = value; }
        }

        /// <summary>
        /// 파라미터 객체에 전달될 데이터를 저장하고 있는 자료구조를 가져오거나 설정합니다.
        /// </summary>
        public object DataValue
        {
            get { return this.m_dataValue; }
            set { this.m_dataValue = value; }
        }

        /// <summary>
        /// 데이터를 추가할 데이터컬럼들을 가져오거나 설정합니다.
        /// </summary>
        public string[] SpParamsInsert
        {
            get { return this.m_spParamsInsert; }
            set { this.m_spParamsInsert = value; }
        }

        /// <summary>
        /// 데이터를 수정할 데이터컬럼들을 가져오거나 설정합니다.
        /// </summary>
        public string[] SpParamsUpdate
        {
            get { return this.m_spParamsUpdate; }
            set { this.m_spParamsUpdate = value; }
        }

        /// <summary>
        /// 데이터를 삭제할 데이터컬럼들을 가져오거나 설정합니다.
        /// </summary>
        public string[] SpParamsDelete
        {
            get { return this.m_spParamsDelete; }
            set { this.m_spParamsDelete = value; }
        }

        public object[] SpParamsNonQuery
        {
            get { return this.m_spParamsNonQuery; }
            set { this.m_spParamsNonQuery = value; }
        }

        /// <summary>
        /// 조회시 파라미터객체에 전달될 Value값을 가져오가나 설정합니다.
        /// </summary>
        public object[] SpParamsSelect
        {
            get { return this.m_spParamsSelect; }
            set { this.m_spParamsSelect = value; }
        }

        public string DetailToday
        {
            get { return this.m_detailToday; }
            set { this.m_detailToday = value; }
        }

        /// <summary>
        /// 회사코드를 가져오거나 설정합니다.
        /// </summary>
        /// 
        [Description("해당 회사의 아이디를 설정하거나 가져옵니다")]
        public string FirmCode
        {
            get { return this.m_FirmCode; }
            set { this.m_FirmCode = value; }
        }

        /// <summary>
        /// 사용자코드를 가져오거나 설정합니다.
        /// </summary>


        [Description("User아이디를 설정하거나 가져옵니다")]
        public string UserID
        {
            get { return this.m_userID; }
            set { this.m_userID = value; }
        }

        /// <summary>
        /// Page ID
        /// </summary>
        [Description("Menu아이디를 설정하거나 가져옵니다")]
        public string MenuID
        {
            get { return m_MenuID; }
            set { m_MenuID = value; }
        }

        #endregion

    }

    #endregion

    #region SingleValueCollection
    [Serializable]
    public class SingleValueCollection : IEnumerable, ISerializable
    {

        private ArrayList m_singleValueList = null;

        #region Constructor
        public SingleValueCollection() { }

        public SingleValueCollection(SerializationInfo info, StreamingContext context)
        {
            m_singleValueList = (ArrayList)info.GetValue("m_singleValueList", typeof(ArrayList));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_singleValueList", m_singleValueList);
        }
        #endregion

        #region Public Property
        public ArrayList List
        {
            get { return m_singleValueList; }
        }

        public int Count
        {
            get
            {
                if (m_singleValueList != null)
                    return m_singleValueList.Count;
                return -1;
            }
        }
        #endregion

        #region Indexer
        public SingleValue this[int index]
        {
            get
            {
                if (index < m_singleValueList.Count)
                    return (SingleValue)m_singleValueList[index];
                return null;
            }
        }

        public SingleValue[] this[SpState acs]
        {
            get
            {
                SingleValue[] obj = null;
                try
                {
                    ArrayList tempList = new ArrayList();
                    if (m_singleValueList != null && m_singleValueList.Count > 0)
                    {
                        for (int i = 0; i < m_singleValueList.Count; i++)
                        {
                            SingleValue sv = (SingleValue)m_singleValueList[i];
                            if (sv.Action == acs)
                                tempList.Add((object)sv);
                        }

                        obj = new SingleValue[tempList.Count];
                        for (int k = 0; k < tempList.Count; k++)
                        {
                            obj[k] = (SingleValue)tempList[k];
                        }
                        //Trace.WriteLine(String.Format("Indexer : {0} {1}", acs, obj.Length)); 
                    }
                }
                catch (Exception ex)
                {
                    //Trace.WriteLine(String.Format("{0}", ex));

                }
                return obj;
            }
        }

        //public SingleValue this[SpState acs, string columnName]
        //{
        //    get
        //    {
        //        SingleValue sValue = null;
        //        if (m_singleValueList != null && m_singleValueList.Count > 0)
        //        {
        //            for (int i = 0; i < m_singleValueList.Count; i++)
        //            {
        //                sValue = (SingleValue)m_singleValueList[i];
        //                if (sValue.Action == acs && String.Compare(sValue.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
        //                    return sValue;
        //            }
        //        }
        //        return sValue;
        //    }
        //}
        #endregion

        #region Public Method

        //public object SingleValueFind(SpState acs, string columnName)
        //{
        //    object obj = null;
        //    try
        //    {
        //        for (int i = 0; i < m_singleValueList.Count; i++)
        //        {
        //            SingleValue sValue = (SingleValue)m_singleValueList[i];
        //            if (sValue.Action == acs && String.Compare(sValue.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
        //                return sValue.DefaultValue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obj;
        //}

        public int Add(SingleValue sv)
        {
            return m_singleValueList.Add(sv);
        }
        /*
                /// <summary>
                /// SingleValueCollection에 SpState에 따른 컬럼이 존재하는지 여부를 리턴합니다.
                /// </summary>
                /// <param name="acs"></param>
                /// <param name="columnName"></param>
                /// <returns></returns>
                public bool Contains(SpState acs, string columnName)
                {
                    bool bResult = false;
                    SingleValue sValue = null;
                    if(m_singleValueList != null && m_singleValueList.Count >0 )
                    {
                        for(int i=0; i < m_singleValueList.Count; i++)
                        {
                            sValue = (SingleValue)m_singleValueList[i];
                            if((sValue == acs) && (String.Compare(sValue.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0))
                            {
                                bResult = true;
                                break;
                            }
                        }			
                    }
                    return bResult;
                }
        */
        public int Add(SpState acs, string columnName, object defaultValue)
        {
            int iRet = -1;
            try
            {
                if (m_singleValueList == null)
                    m_singleValueList = new ArrayList();
                SingleValue sv = new SingleValue(acs, columnName, defaultValue);
                iRet = Add(sv);
                m_singleValueList.TrimToSize();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRet;
        }
        #endregion

        public System.Collections.IEnumerator GetEnumerator()
        {
            return m_singleValueList.GetEnumerator();
        }

    }
    #endregion

    #region DataValueState Enum

    [Serializable]
    public enum DataValueState
    {
        Added = 1,
        Deleted = 2,
        Modified = 3,
        NoAccept = 4
    }
    #endregion

    #region SingleValue
    [Serializable]
    public class SingleValue : ISerializable
    {
        private SpState m_acs;
        private string m_columnName;
        private object m_defaultValue;

        public SingleValue() { }
        public SingleValue(SpState acs, string columnName, object defaultValue)
        {
            m_acs = acs;
            m_columnName = columnName;
            m_defaultValue = defaultValue;
        }

        public SingleValue(SerializationInfo info, StreamingContext context)
        {
            m_acs = (SpState)info.GetValue("m_acs", typeof(SpState));
            m_columnName = (string)info.GetValue("m_columnName", typeof(string));
            m_defaultValue = (object)info.GetValue("m_defaultValue", typeof(object));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_acs", m_acs);
            info.AddValue("m_columnName", m_columnName);
            info.AddValue("m_defaultValue", m_defaultValue);
        }

        public SpState Action
        {
            get { return m_acs; }
            set { m_acs = value; }
        }
        public string ColumnName
        {
            get { return this.m_columnName; }
            set { this.m_columnName = value; }
        }

        public object DefaultValue
        {
            get { return this.m_defaultValue; }
            set { this.m_defaultValue = value; }
        }
    }
    #endregion

    #region SpInfoCollection Class
    [Serializable]
    public class SpInfoCollection : IEnumerable, ISerializable
    {
        private ArrayList _list = null;

        public ArrayList List
        {
            get { return _list; }
            set { _list = value; }
        }

        public SpInfoCollection() { }

        public SpInfoCollection(SerializationInfo info, StreamingContext context)
        {
            _list = (ArrayList)info.GetValue("_list", typeof(ArrayList));
        }

        private void CreateSpInfoCollection()
        {
            if (_list == null)
                _list = new ArrayList();
        }

        #region Override Method
        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_list", _list);
        }
        #endregion

        #region Indexer

        public SpInfo this[int index]
        {
            get
            {
                if (index < -1 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("Index", string.Format("{0} 번째 인덱스는 유효한 범위를 벗어났습니다.", index));
                return (SpInfo)_list[index];
            }
            set
            {
                _list[index] = (SpInfo)value;
            }
        }
        #endregion

        public int Count
        {
            get { return _list.Count; }
        }

        public int Capacity
        {
            get { return _list.Capacity; }
            set { _list.Capacity = value; }
        }

        public int Add(object obj)
        {
            if (_list == null)
                CreateSpInfoCollection();
            int ret;
            ret = _list.Add(obj);
            _list.TrimToSize();
            return ret;
        }

        public void Insert(int index, object obj)
        {
            _list.Insert(index, obj);
        }

        public int IndexOf(object obj)
        {
            return _list.IndexOf(obj);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public void Remove(object obj)
        {
            _list.Remove(obj);
        }
    }


    #endregion
}
