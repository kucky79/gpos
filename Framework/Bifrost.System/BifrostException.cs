using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace Bifrost
{
    public class BifrostException : Base
    {
        #region HandleBSLException
        /// <summary>
        /// Business Service Layer 예외 처리 공통 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        public static void HandleBSLException(SubSystemType sub, Exception ex, Type ObjType)
        {
            HandleException(sub, LayerType.BSL, "#", ex, ObjType);
        }

        /// <summary>
        /// Business Service Layer 예외 처리 공통 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        /// <param name="errorCode">에러코드</param>
        public static void HandleBSLException(SubSystemType sub, Exception ex, Type ObjType, string errorCode)
        {
            HandleException(sub, LayerType.BSL, errorCode, ex, ObjType);
        }
        #endregion

        #region HandleWSLException
        /// <summary>
        /// Web Service Layer 예외 처리 공통 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        public static void HandleWSLException(SubSystemType sub, System.Exception ex, Type ObjType)
        {
            HandleException(sub, LayerType.WSL, "#", ex, ObjType);
        }

        /// <summary>
        /// Web Service Layer 예외 처리 공통 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        /// <param name="errorCode">에러코드</param>
        public static void HandleWSLException(SubSystemType sub, System.Exception ex, Type ObjType, string errorCode)
        {
            HandleException(sub, LayerType.WSL, errorCode, ex, ObjType);
        }
        #endregion

        #region HandleDSLException
        /// <summary>
        /// Data Service Layer 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        public static void HandleDSLException(SubSystemType sub, Exception ex, Type ObjType)
        {
            HandleException(sub, LayerType.DSL, "#", ex, ObjType);
        }

        /// <summary>
        /// Data Service Layer 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">서브시스템타입</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        /// <param name="errorCode">에러코드</param>
        public static void HandleDSLException(SubSystemType sub, Exception ex, Type ObjType, string errorCode)
        {
            HandleException(sub, LayerType.DSL, errorCode, ex, ObjType);
        }
        #endregion

        #region HandleFWKException
        /// <summary>
        /// Framework 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        public static void HandleFWKException(SubSystemType sub, Exception ex, Type ObjType)
        {
            HandleException(sub, LayerType.FWK, "#", ex, ObjType);
        }

        /// <summary>
        /// Framework 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">서브시스템타입</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        /// <param name="errorCode">에러코드</param>
        public static void HandleFWKException(SubSystemType sub, Exception ex, Type ObjType, string errorCode)
        {
            HandleException(sub, LayerType.FWK, errorCode, ex, ObjType);
        }
        #endregion

        #region HandleUSLException
        /// <summary>
        /// User Service Layer 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        public static void HandleUSLException(SubSystemType sub, Exception ex, Type ObjType)
        {
            HandleException(sub, LayerType.USL, "#", ex, ObjType);
        }

        /// <summary>
        /// User Service Layer 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">서브시스템타입</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        /// <param name="errorCode">에러코드</param>
        public static void HandleUSLException(SubSystemType sub, Exception ex, Type ObjType, string errorCode)
        {
            HandleException(sub, LayerType.USL, errorCode, ex, ObjType);
        }
        #endregion

        #region HandleWEBException
        /// <summary>
        /// WEB PRESENTATION Layer 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">업무구분</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        public static void HandleWEBException(SubSystemType sub, Exception ex, Type ObjType)
        {
            HandleException(sub, LayerType.WEB, "#", ex, ObjType);
        }

        /// <summary>
        /// WEB PRESENTATION Layer 예외 처리를 위한 메소드
        /// </summary>
        /// <param name="sub">서브시스템타입</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">오브젝트타입</param>
        /// <param name="errorCode">에러코드</param>
        public static void HandleWEBException(SubSystemType sub, Exception ex, Type ObjType, string errorCode)
        {
            HandleException(sub, LayerType.WEB, errorCode, ex, ObjType);
        }
        #endregion

        #region WriteLog
        /// <summary>
        /// Exception에 대한 로그를 기록한다.
        /// </summary>
        /// <param name="systemName">서브시스템이름</param>
        /// <param name="methodIBifrosto">Method IBifrosto</param>
        /// <param name="layer">Layer타입</param>
        /// <param name="ex">예외개체</param>
        protected static Exception WriteLog(string systemName, string methodIBifrosto, string layer, Exception ex)
        {
            return LogHandler.Publish(systemName, methodIBifrosto, layer, ex);
        }
        #endregion

        #region HandleException Exception처리공통
        /// <summary>
        /// 예외 일반 또는 UserException 처리를 위한 Protected 메소드
        /// </summary>
        /// <param name="sub">서브시스템타입</param>
        /// <param name="layer">Layer타입</param>
        /// <param name="errCode">에러코드</param>
        /// <param name="ex">예외개체</param>
        /// <param name="ObjType">클래스타입</param>
        protected static void HandleException(SubSystemType sub, LayerType layer, string errCode, Exception ex, Type ObjType)
        {
            string subSystemName = sub.ToString();
            string layerName = GetLayerString(layer);
            string strTemp = ex.ToString();
            int nTemp = strTemp.IndexOf("Bifrost System Error Tracing");
            if (nTemp == -1)
                throw WriteLog(subSystemName, errCode, layerName, ex);
            else
                throw ex;
        }
        #endregion

        #region 메소드명 뜯어오는 지역클래스
        /// <summary>
        /// System.Type에서 메소드을 가져오기 위한 클래스
        /// </summary>
        public class ClsType
        {
            /// <summary>
            /// 생성자
            /// </summary>
            public ClsType() { }

            /// <summary>
            /// 목적 : Type을 통하여 현재의 메소드명 가져오기
            /// </summary>
            /// <param name="aType">Type명</param>
            /// <returns>메소드명</returns>
            public string GetMethodName(Type aType)
            {
                try
                {
                    MethodInfo[] mIBifrosto = aType.GetMethods();
                    for (int i = 0; i < mIBifrosto.Length; i++)
                    {
                        if (mIBifrosto[i].DeclaringType == aType && !mIBifrosto[i].IsSpecialName && (this.GetType().ToString() != aType.ToString()))
                        {
                            return mIBifrosto[i].Name.ToString();
                        }
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
