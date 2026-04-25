using System;
using System.Data;
using System.Data.OracleClient;

using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Globalization;
using System.Diagnostics;

namespace Bifrost.Common.Util
{
    #region ActionState Enum
    [Serializable]
    public enum ActionState
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        Select = 4
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

    #region coDbException
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class coDbException : System.ApplicationException
    {
        #region Private Variable
        /// <summary>
        /// Error Number
        /// </summary>
        private int m_DbErrorNo;
        /// <summary>
        /// Error Message
        /// </summary>
        private string m_DbDummyStr;
        /// <summary>
        /// Method Name occured Error
        /// </summary>
        private string m_MethodName;
        /// <summary>
        /// Null Name
        /// </summary>
        private string m_NullName;
        /// <summary>
        /// PrimaryKey Name
        /// </summary>
        private string m_PrimaryKeyName;
        /// <summary>
        /// Custom Name
        /// </summary>
        private string m_customName;
        #endregion

        #region Constuctor
        /// <summary>
        /// 
        /// </summary>
        public coDbException() : base()
        {
            m_DbErrorNo = 0;
            m_DbDummyStr = String.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public coDbException(string message) : base(message)
        {
            m_DbErrorNo = 0;
            m_DbDummyStr = String.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public coDbException(string message, Exception inner) : base(message, inner)
        {
            m_DbErrorNo = 0;
            m_DbDummyStr = String.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public coDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            m_DbErrorNo = info.GetInt32("m_DbErrorNo");
            m_DbDummyStr = info.GetString("m_DbDummyStr");
        }
        #endregion

        #region override Method
        /// <summary>
        /// 현재 시스템 클립보드에 있는 데이터를 검색합니다.
        /// 클립보드에서 반환되는 개체의 데이터 형식이 서로 다를 수 있으므로 이 메서드는 IDataObject에 있는 데이터를 반환합니다. 
        /// 그런 다음 IDataObject 인터페이스의 메서드를 사용하여 적절한 데이터 형식의 데이터를 추출합니다.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("m_DbErrorNo", m_DbErrorNo);
            info.AddValue("m_DbDummyStr", m_DbDummyStr);

            Type t = this.GetType();
            info.AddValue("TypeObj", t);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Error 번호
        /// </summary>
        public int DbErrorNo
        {
            get { return m_DbErrorNo; }
            set { m_DbErrorNo = value; }
        }

        /// <summary>
        /// HelpLink 변화 문자
        /// </summary>
        public string DbDummyStr
        {
            get { return m_DbDummyStr; }
            set { m_DbDummyStr = "[" + value + "]\n"; }
        }

        /// <summary>
        /// 출력을 위한 Method Name 
        /// </summary>
        public string MethodName
        {
            get { return this.m_MethodName; }
            set { this.m_MethodName += "[" + value + "]\n"; }
        }

        /// <summary>
        /// 널에러 변환문자
        /// </summary>
        public string NullName
        {
            get { return this.m_NullName; }
            set { this.m_NullName = value; }
        }

        /// <summary>
        /// 중복값 변환 문자
        /// </summary>
        public string PrimaryKeyName
        {
            get { return this.m_PrimaryKeyName; }
            set { this.m_PrimaryKeyName = value; }
        }


        /// <summary>
        /// 100000번 에러일 때의 변환 문자
        /// </summary>
        public string CustomName
        {
            get { return this.m_customName; }
            set { this.m_customName = value; }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// null 에러 변환 문자
        /// </summary>
        /// <param name="param"></param>
        public void SetNullName(params string[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                if (i == param.Length - 1)
                    this.m_NullName += param[i] + " ";
                else
                    this.m_NullName += param[i] + ", ";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public void SetPrimaryKeyName(params string[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                if (i == param.Length - 1)
                    this.m_PrimaryKeyName += param[i] + " ";
                else
                    this.m_PrimaryKeyName += param[i] + ", ";
            }
        }
        #endregion
    }
    #endregion

    #region coDBParameter

    /// <summary>
    /// SqlParameter, OleDbParameter 대신 사용할 class
    /// </summary>
    [Serializable]
    public class coDBParameter : ISerializable
    {
        #region Private Variable

        private SqlDbType m_SDbType;
        private OracleType m_ODbType;


        /// <summary>
        /// Value 속성을 나타내는 데 사용되는 최대 자릿수를 가져오거나 설정합니다.
        /// </summary>
        private ParameterDirection m_Direction;
        /// <summary>
        /// Parameter의 이름을 가져오거나 설정합니다.
        /// </summary>
        private string m_ParameterName;
        /// <summary>
        /// Value 속성을 나타내는 데 사용되는 최대 자릿수를 가져오거나 설정합니다.
        /// </summary>
        private byte m_Precision;
        /// <summary>
        /// Value를 확인하는 소수 자릿수의 수를 가져오거나 설정합니다.
        /// </summary>
        private byte m_Scale;
        /// <summary>
        /// 열 내부에 있는 데이터의 최대 크기를 바이트 단위로 가져오거나 설정합니다.
        /// </summary>
        private int m_Size;
        /// <summary>
        /// DataSet에 매핑되고 Value를 로드하거나 반환하는 소스 열의 이름을 가져오거나 설정합니다.
        /// </summary>
        private string m_SourceColumn;
        /// <summary>
        /// Value를 로드할 때 사용할 DataRowVersion을 가져오거나 설정합니다.
        /// </summary>
        private DataRowVersion m_SourceVersion;
        /// <summary>
        /// 매개 변수의 값을 가져오거나 설정합니다.
        /// </summary>
        private object m_Value;
        #endregion

        #region Constructor
        public coDBParameter()
        {
        }

        public coDBParameter(SerializationInfo info, StreamingContext context)
        {
            m_SDbType = (SqlDbType)info.GetValue("m_SDbType", typeof(SqlDbType));
            m_ODbType = (OracleType)info.GetValue("m_ODbType", typeof(OracleType));
            m_Direction = (ParameterDirection)info.GetValue("m_Direction", typeof(ParameterDirection));
            m_ParameterName = (string)info.GetValue("m_ParameterName", typeof(string));
            m_Precision = (byte)info.GetValue("m_Precision", typeof(byte));
            m_Scale = (byte)info.GetValue("m_Scale", typeof(byte));
            m_Size = (int)info.GetValue("m_Size", typeof(int));
            m_SourceColumn = (string)info.GetValue("m_SourceColumn", typeof(string));
            m_SourceVersion = (DataRowVersion)info.GetValue("m_SourceVersion", typeof(DataRowVersion));
            m_Value = (object)info.GetValue("m_Value", typeof(object));
        }


        public coDBParameter(string parameterName, SqlDbType sdbType, OracleType odbType, int size)
        {
            m_ParameterName = parameterName;
            m_SDbType = sdbType;
            m_ODbType = odbType;
            m_Size = size;
        }

        public coDBParameter(string parameterName, SqlDbType sdbType, OracleType odbType, int size, object values)
            : this(parameterName, sdbType, odbType, size)
        {
            //			m_ParameterName = parameterName;
            //			m_SDbType		= sdbType;
            //			m_ODbType		= odbType;
            //			m_Size			= size;
            m_Value = values;
        }

        public coDBParameter(string parameterName, SqlDbType sdbType, OracleType odbType, int size, object values, ParameterDirection direction)
            : this(parameterName, sdbType, odbType, size, values)
        {
            //			m_ParameterName = parameterName;
            //			m_SDbType		= sdbType;
            //			m_ODbType		= odbType;
            //			m_Size			= size;
            //			m_Value			= values;
            m_Direction = direction;
        }
        #endregion

        #region Properties

        public SqlDbType SDbType
        {
            get { return m_SDbType; }
            set { m_SDbType = value; }
        }

        public OracleType ODbType
        {
            get { return m_ODbType; }
            set { m_ODbType = value; }
        }

        public ParameterDirection Direction
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }

        public string ParameterName
        {
            get { return m_ParameterName; }
            set { m_ParameterName = value; }
        }

        public byte Precision
        {
            get { return m_Precision; }
            set { m_Precision = value; }
        }

        public byte Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }

        public int Size
        {
            get { return m_Size; }
            set { m_Size = value; }
        }

        public string SourceColumn
        {
            get { return m_SourceColumn; }
            set { m_SourceColumn = value; }
        }

        public DataRowVersion SourceVersion
        {
            get { return m_SourceVersion; }
            set { m_SourceVersion = value; }
        }

        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        #endregion

        #region Public Method
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("m_SDbType", m_SDbType);
            info.AddValue("m_ODbType", m_ODbType);
            info.AddValue("m_Direction", m_Direction);
            info.AddValue("m_ParameterName", m_ParameterName);
            info.AddValue("m_Precision", m_Precision);
            info.AddValue("m_Scale", m_Scale);
            info.AddValue("m_Size", m_Size);
            info.AddValue("m_SourceColumn", m_SourceColumn);
            info.AddValue("m_SourceVersion", m_SourceVersion);
            info.AddValue("m_Value", m_Value);
        }
        #endregion
    }

    #endregion

    #region coDBParameterCollection

    public class coDBParametrCollection : IEnumerable, ISerializable
    {
        private ArrayList _list = null;
        public coDBParametrCollection()
        {

        }

        public coDBParametrCollection(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {

        }

        public void Add(string parameterName, object value)
        {
            if (_list == null)
                _list = new ArrayList();
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return null;
        }


    }

    #endregion

    #region SpInfo Class
    [Serializable]
    public class SpInfo : ISerializable/*, IDisposable*/
    {
        #region Private Variables
        /// <summary>
        /// 페이지ID
        /// </summary>
        private string m_pageID = string.Empty;
        /// <summary>
        /// Insert SP Name
        /// </summary>
        private string m_spNameInsert = string.Empty;
        /// <summary>
        /// Update SP Name
        /// </summary>
        private string m_spNameUpdate = string.Empty;
        /// <summary>
        /// Delete SP Name
        /// </summary>
        private string m_spNameDelete = string.Empty;
        /// <summary>
        /// Select SP Name
        /// </summary>
        private string m_spNameSelect = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string m_spNameNonQuery = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private object m_dataValue = null;
        /// <summary>
        /// 
        /// </summary>
        private string[] m_spParamsInsert = null;
        /// <summary>
        /// 
        /// </summary>
        private string[] m_spParamsUpdate = null;
        /// <summary>
        /// 
        /// </summary>
        private string[] m_spParamsDelete = null;

        /// <summary>
        /// 
        /// </summary>
        private object[] m_spParamsNonQuery = null;

        /// <summary>
        /// 
        /// </summary>
        private object[] m_spParamsSelect = null;

        /// <summary>
        /// 
        /// </summary>
        private string m_companyID = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string m_userID = string.Empty;


        private string m_detailToday = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private SingleValueCollection m_singleValues = null;//new SingleValueCollection();
        /// <summary>
        /// 
        /// </summary>
        private DataValueState m_dataValueState = DataValueState.NoAccept;

        #endregion

        #region Constructor
        public SpInfo() { }

        public SpInfo(string pageId, string spInsert, string spUpdate, string spDelete, string spSelect,
            object data, string[] insertParams, string[] updateParams, string[] deleteParams, object[] selectParams, string companyId, string userId)
        {
            this.m_pageID = pageId;
            this.m_spNameInsert = spInsert;
            this.m_spNameUpdate = spUpdate;
            this.m_spNameDelete = spDelete;
            this.m_spNameSelect = spSelect;
            this.m_dataValue = data;
            this.m_spParamsInsert = insertParams;
            this.m_spParamsUpdate = updateParams;
            this.m_spParamsDelete = deleteParams;
            this.m_spParamsSelect = selectParams;
            this.m_companyID = companyId;
            this.m_userID = userId;
        }

        public SpInfo(string pageId, string spInsert, string spUpdate, string spDelete, object data, string[] insertParams, string[] updateParams, string[] deleteParams,
            string companyId, string userId)
            : this(pageId, spInsert, spUpdate, spDelete, null, data, insertParams, updateParams, deleteParams, null, companyId, userId) { }

        public SpInfo(string pageId, string spSelect, object[] selectParams, string companyId, string userId)
            : this(pageId, null, null, null, spSelect, null, null, null, null, selectParams, companyId, userId) { }

        public SpInfo(SerializationInfo info, StreamingContext context)
        {

            try
            {
                m_pageID = (string)info.GetValue("m_pageID", typeof(String));
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
                m_companyID = (string)info.GetValue("m_companyID", typeof(string));
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
                info.AddValue("m_pageID", m_pageID);
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
                info.AddValue("m_companyID", m_companyID);
                info.AddValue("m_userID", m_userID);
                info.AddValue("m_detailToday", m_detailToday);
                info.AddValue("m_singleValues", m_singleValues);
                info.AddValue("m_dataValueState", m_dataValueState);

            }
            catch (Exception ex)
            {
                Trace.WriteLine(String.Format("SpInfo GetObjectData : {0}", ex));
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
        public string CompanyID
        {
            get { return this.m_companyID; }
            set { this.m_companyID = value; }
        }

        /// <summary>
        /// 사용자코드를 가져오거나 설정합니다.
        /// </summary>
        public string UserID
        {
            get { return this.m_userID; }
            set { this.m_userID = value; }
        }

        /// <summary>
        /// Page ID
        /// </summary>
        public string PageID
        {
            get { return m_pageID; }
            set { m_pageID = value; }
        }

        #endregion

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
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
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

        public int Add(SpInfo obj)
        {
            return this.Add((object)obj);
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

        public SingleValue[] this[ActionState acs]
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
                    Trace.WriteLine(String.Format("{0}", ex));

                }
                return obj;
            }
        }

        public SingleValue this[ActionState acs, string columnName]
        {
            get
            {
                SingleValue sValue = null;
                if (m_singleValueList != null && m_singleValueList.Count > 0)
                {
                    for (int i = 0; i < m_singleValueList.Count; i++)
                    {
                        sValue = (SingleValue)m_singleValueList[i];
                        if (sValue.Action == acs && String.Compare(sValue.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
                            return sValue;
                    }
                }
                return sValue;
            }
        }
        #endregion

        #region Public Method

        //		public object SingleValueFind(ActionState acs, string columnName)
        //		{
        //			object obj = null;
        //			try
        //			{
        //				for(int i=0; i < m_singleValueList.Count; i++)
        //				{
        //					SingleValue sValue = (SingleValue)m_singleValueList[i];
        //					if(sValue.Action == acs && String.Compare(sValue.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
        //						return sValue.DefaultValue;
        //				}
        //			}
        //			catch(Exception ex)
        //			{
        //				throw ex;
        //			}
        //			return obj;
        //		}

        public int Add(SingleValue sv)
        {
            return m_singleValueList.Add((object)sv);
        }
        /*
                /// <summary>
                /// SingleValueCollection에 ActionState에 따른 컬럼이 존재하는지 여부를 리턴합니다.
                /// </summary>
                /// <param name="acs"></param>
                /// <param name="columnName"></param>
                /// <returns></returns>
                public bool Contains(ActionState acs, string columnName)
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
        public int Add(ActionState acs, string columnName, object defaultValue)
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

    #region SingleValue
    [Serializable]
    public class SingleValue : ISerializable
    {
        private ActionState m_acs;
        private string m_columnName;
        private object m_defaultValue;

        public SingleValue() { }
        public SingleValue(ActionState acs, string columnName, object defaultValue)
        {
            m_acs = acs;
            m_columnName = columnName;
            m_defaultValue = defaultValue;
        }

        public SingleValue(SerializationInfo info, StreamingContext context)
        {
            m_acs = (ActionState)info.GetValue("m_acs", typeof(ActionState));
            m_columnName = (string)info.GetValue("m_columnName", typeof(string));
            m_defaultValue = (object)info.GetValue("m_defaultValue", typeof(object));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_acs", m_acs);
            info.AddValue("m_columnName", m_columnName);
            info.AddValue("m_defaultValue", m_defaultValue);
        }

        public ActionState Action
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

    #region ResultData

    #region ResultData
    [Serializable]
    public sealed class ResultData : ISerializable
    {
        #region Private Variables
        /// <summary>
        /// 최종 결과
        /// </summary>
        private bool result = false;
        /// <summary>
        /// 결과 집합셋
        /// </summary>
        private object dataValue = null;
        private OutParameters outParamsInsert = null;
        private OutParameters outParamsUpdate = null;
        private OutParameters outParamsDelete = null;
        private OutParameters outParamsSelect = null;
        #endregion

        #region Constructor
        public ResultData() { }
        public ResultData(SerializationInfo info, StreamingContext context)
        {
            result = (bool)info.GetValue("result", typeof(bool));
            dataValue = (object)info.GetValue("dataValue", typeof(object));
            outParamsInsert = (OutParameters)info.GetValue("outParamsInsert", typeof(OutParameters));
            outParamsUpdate = (OutParameters)info.GetValue("outParamsUpdate", typeof(OutParameters));
            outParamsDelete = (OutParameters)info.GetValue("outParamsDelete", typeof(OutParameters));
            outParamsSelect = (OutParameters)info.GetValue("outParamsSelect", typeof(OutParameters));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("result", result);
            info.AddValue("dataValue", dataValue);
            info.AddValue("outParamsInsert", outParamsInsert);
            info.AddValue("outParamsUpdate", outParamsUpdate);
            info.AddValue("outParamsDelete", outParamsDelete);
            info.AddValue("outParamsSelect", outParamsSelect);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// OutParameters -> DataTable
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(OutParameters parameters)
        {
            DataTable dt_temp = new DataTable();
            try
            {
                //데이터컬럼만들기
                for (int colIndex = 0; colIndex < parameters.ColCount; colIndex++)
                {
                    string columnName = parameters.GetColumnName(colIndex);//"column" + colIndex.ToString();
                    System.Data.DataColumn column = new DataColumn(columnName);
                    dt_temp.Columns.Add(column);
                }

                //데이터 집어넣기
                DataRow row;
                object[] tempObj;
                for (int rowIndex = 0; rowIndex < parameters.RowCount; rowIndex++)
                {
                    dt_temp.BeginLoadData();
                    tempObj = parameters.GetDataRow(rowIndex);
                    row = dt_temp.LoadDataRow(tempObj, true);
                    dt_temp.EndLoadData();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("GetDataTable()", ex.InnerException);
            }
            return dt_temp;
        }

        /// <summary>
        /// DataTable -> OutParameters
        /// </summary>
        /// <param name="dt_temp"></param>
        /// <returns></returns>
        public OutParameters GetOutParameters(DataTable dt_temp)
        {
            if (dt_temp == null)
                return null;

            OutParameters outParameters = null;
            try
            {
                outParameters = new OutParameters();

                //데이터컬럼 넣기
                for (int i = 0; i < dt_temp.Columns.Count; i++)
                    outParameters.ColumnsAdd(dt_temp.Columns[i].ColumnName);

                for (int rowIndex = 0; rowIndex < dt_temp.Rows.Count; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < dt_temp.Columns.Count; colIndex++)
                        outParameters[rowIndex, colIndex] = dt_temp.Rows[rowIndex][colIndex];
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("GetOutParameters()", ex.InnerException);
            }
            return outParameters;
        }

        #endregion

        #region Public Property
        public bool Result
        {
            get { return result; }
            set { result = value; }
        }

        public object DataValue
        {
            get { return dataValue; }
            set { dataValue = value; }
        }

        public OutParameters OutParamsInsert
        {
            get
            {
                if (outParamsInsert == null)
                    outParamsInsert = new OutParameters();
                return outParamsInsert;
            }
            set { outParamsInsert = value; }
        }

        public OutParameters OutParamsUpdate
        {
            get
            {
                if (outParamsUpdate == null)
                    outParamsUpdate = new OutParameters();
                return outParamsUpdate;
            }
            set { outParamsUpdate = value; }
        }

        public OutParameters OutParamsDelete
        {
            get
            {
                if (outParamsDelete == null)
                    outParamsDelete = new OutParameters();
                return outParamsDelete;
            }
            set { outParamsDelete = value; }
        }

        public OutParameters OutParamsSelect
        {
            get
            {
                if (outParamsSelect == null)
                    outParamsSelect = new OutParameters();
                return outParamsSelect;
            }
            set { outParamsSelect = value; }
        }

        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		public OutParameters OutParamsNonQuery
        //		{
        //			get
        //			{
        //				if(outParamsSelect == null)
        //					outParamsNonQuery = new OutParameters();
        //				return outParamsNonQuery;
        //			}
        //			set{ outParamsNonQuery = value; }
        //		}
        #endregion
    }
    #endregion

    #region OutParameters
    [Serializable()]
    public class OutParameters : ISerializable
    { 
        #region Private Variables

        private int rowCount = 0;
        private int colCount = 0;
        private ArrayListExt rows = null;
        private string packName = string.Empty;

        public string PackName
        {
            get { return packName; }
            set { packName = value; }
        }
        private ArrayListExt cols = null;

        public ArrayListExt Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        #endregion

        #region Constructor
        public OutParameters() { }
        public OutParameters(SerializationInfo info, StreamingContext context)
        {
            rowCount = (int)info.GetValue("RowCount", typeof(int));
            colCount = (int)info.GetValue("ColCount", typeof(int));
            rows = (ArrayListExt)info.GetValue("Rows", typeof(ArrayListExt));
            cols = (ArrayListExt)info.GetValue("Cols", typeof(ArrayListExt));
        }

        #endregion

        #region Interface Implement

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RowCount", rowCount);
            info.AddValue("ColCount", colCount);
            info.AddValue("Rows", rows);
            info.AddValue("Cols", cols);
        }

        #endregion

        #region IEnumerable
        //		public System.Collections.IEnumerator GetEnumerator()
        //		{
        //			return rows.GetEnumerator();
        //		}
        #endregion

        #endregion

        #region Property

        public ArrayListExt Rows
        {
            get
            {
                if (rows == null)
                    rows = CreateRowsCollection();
                return rows;
            }
        }

        public int RowCount
        {
            get
            {
                if (Rows == null)
                    return 0;
                return Rows.Count;
            }
        }

        public int ColCount
        {
            get
            {
                if (Cols == null)
                    return 0;
                return Cols.Count;
            }
        }

        #region Indexer

        public object this[int rowIndex, string columnName]
        {
            get
            {

                int colIndex = -1;
                for (int i = 0; i < cols.Count; i++)
                {
                    Column c = (Column)cols[i];
                    if (string.Compare(c.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
                    {
                        colIndex = i;
                        break;
                    }
                }
                if (colIndex < 0)
                    throw new ArgumentOutOfRangeException("ColIndex");

                return this[rowIndex, colIndex];
            }
            set
            {
                int colIndex = -1;
                for (int i = 0; i < cols.Count; i++)
                {
                    Column c = (Column)cols[i];
                    if (string.Compare(c.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
                    {
                        colIndex = i;
                        break;
                    }
                }
                if (colIndex == -1)
                    throw new ArgumentOutOfRangeException("ColIndex");

                this[rowIndex, colIndex] = value; ;
            }
        }

        public object this[int rowIndex, int colIndex]
        {
            get
            {
                if (rowIndex >= rowCount || rowIndex < 0
                    || colIndex >= colCount || colIndex < 0)
                    return null;

                ArrayListExt row = null;
                if (rowIndex < Rows.Count)
                    row = Rows[rowIndex] as ArrayListExt;
                if (row == null)
                    return null;
                else
                    return row[colIndex];
            }

            set
            {
                if (rowIndex < 0 || colIndex < 0)
                    throw new ArgumentOutOfRangeException("Index");

                if (rowIndex >= rowCount)
                    this.IncrementRowCount(++rowCount);
                ArrayListExt row = Rows[rowIndex] as ArrayListExt;
                if (row == null)
                {
                    if (value == null)
                        return;
                    else
                        Rows[rowIndex] = row = CreateRowsCollection();
                }
                row[colIndex] = value;
            }
        }
        #endregion

        #endregion

        #region Public Method


        /// <summary>
        /// 해당 로우의 데이터를 리턴한다.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public object[] GetDataRow(int rowIndex)
        {
            if (this.colCount <= 0)
                return null;
            object[] tempObj = new object[colCount];
            ArrayListExt temp = (ArrayListExt)Rows[rowIndex];

            for (int i = 0; i < temp.Count; i++)
                tempObj[i] = temp[i];
            return tempObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetColumnName(int index)
        {
            if (index < 0 || index >= colCount)
                throw new ArgumentOutOfRangeException("Index");

            Column c = (Column)cols[index];
            return c.ColumnName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetColumnName()
        {
            string[] colNames = null;
            if (cols != null && cols.Count > 0)
            {
                colNames = new string[cols.Count];
                for (int i = 0; i < cols.Count; i++)
                    colNames[i] = GetColumnName(i);
            }
            return colNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        public void ColumnsAdd(string columnName)
        {
            if (cols == null)
                cols = CreateRowsCollection();

            if (CheckColumnNameDuplicate(columnName))
            {
                Column c = new Column(columnName);
                int count = cols.Add(c);
                this.colCount++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnNames"></param>
        public void ColumnsAdd(string[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
                ColumnsAdd(columnNames[i]);
        }

        /// <summary>
        ///  모든 항목을 삭제합니다.
        /// </summary>
        public void Clear()
        {
            rowCount = 0;
            colCount = 0;
            rows = null;
        }
        #endregion

        #region Private Method

        /// <summary>
        /// columnName의 중복여부를 체크한다.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool CheckColumnNameDuplicate(string columnName)
        {
            bool bRet = false;
            for (int i = 0; i < cols.Count; i++)
            {
                Column c = (Column)cols[i];
                if (string.Compare(c.ColumnName, columnName, true, CultureInfo.InvariantCulture) == 0)
                    return false;
            }
            bRet = true;
            return bRet;
        }

        /// <summary>
        /// column count를 중가시키는 메소드
        /// </summary>
        /// <param name="value"></param>
        private void IncrementColCount(int value)
        {
            if (value < colCount)
                RemoveCols(value, colCount - value);
            else if (value > colCount)
                InsertCols(colCount, value - colCount);
        }

        /// <summary>
        /// row count를 증가시키는 메소드
        /// </summary>
        /// <param name="value"></param>
        private void IncrementRowCount(int value)
        {
            if (value < rowCount)
                RemoveRows(value, rowCount - value);
            else if (value > rowCount)
                InsertRows(rowCount, value - rowCount);
        }

        private ArrayListExt CreateRowsCollection()
        {
            return new ArrayListExt();
        }

        private bool InsertRows(int rowIndex, int count)
        {
            if (rowIndex > rowCount + 1)
                throw new ArgumentOutOfRangeException("rowIndex");
            Rows.InsertRange(rowIndex, count);
            rowCount = Math.Max(rowIndex, rowCount) + count;
            return true;
        }

        private bool InsertCols(int colIndex, int count)
        {
            if (colIndex > colCount + 1)
                throw new ArgumentOutOfRangeException("colIndex");
            foreach (object obj in Rows)
            {
                ArrayListExt row = obj as ArrayListExt;
                if (row != null)
                    row.InsertRange(colIndex, count);
            }
            colCount = Math.Max(colIndex, colCount) + count;
            return true;
        }

        private bool RemoveRows(int rowIndex, int count)
        {
            if (rowIndex >= rowCount || rowIndex < 0)
                throw new ArgumentOutOfRangeException("rowIndex");
            if (rowIndex + count > rowCount)
                throw new ArgumentOutOfRangeException("count");
            Rows.RemoveRange(rowIndex, count);
            rowCount -= count;
            return true;
        }

        private bool RemoveCols(int colIndex, int count)
        {
            if (colIndex >= colCount || colIndex < 0)
                throw new ArgumentOutOfRangeException("colIndex");
            if (colIndex + count > colCount)
                throw new ArgumentOutOfRangeException("count");

            foreach (object obj in Rows)
            {
                ArrayListExt row = obj as ArrayListExt;
                if (row != null)
                    row.RemoveRange(colIndex, count);
            }
            colCount -= count;
            return true;
        }

        #endregion

    }

    #region Column

    [Serializable]
    public class Column
    {
        #region Private Variables
        /// <summary>
        /// DataColumn 이름
        /// </summary>
        private string m_columnName = string.Empty;

        #endregion

        #region Constructor
        public Column() { }
        public Column(string columnName)
        {
            this.m_columnName = columnName;
        }
        #endregion

        #region Property
        public string ColumnName
        {
            get { return this.m_columnName; }
            set { this.m_columnName = value; }
        }
        #endregion

    }
    #endregion

    #region ArrayListExt
    [Serializable]
    public class ArrayListExt : ArrayList
    {

        public ArrayListExt() : base() { }
        public ArrayListExt(ICollection c) : base(c) { }
        public ArrayListExt(int capacity) : base(capacity) { }

        private void EnsureCount(int value)
        {
            if (this.Count < value)
                this.AddRange(new object[value - Count]);
        }

        public override void RemoveRange(int index, int count)
        {
            if (index < this.Count)
                base.RemoveRange(index, Math.Min(count, this.Count - index));
        }

        public void InsertRange(int index, int count)
        {
            if (index < this.Count)
                this.InsertRange(index, new object[count]);
        }

        public override object this[int index]
        {
            get
            {
                if (index >= this.Count)
                    return null;
                return base[index];
            }
            set
            {
                this.EnsureCount(index + 1);
                base[index] = value;
            }
        }
    }
    #endregion

    #endregion

    #endregion
}
