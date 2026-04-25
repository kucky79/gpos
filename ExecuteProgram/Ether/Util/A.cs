using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Util
{
    public class FrmaeUtil
    {
        public static string GetDatePattern
        {
            get
            {
                string _GetDatePattern = @"yyyy\/MM\/dd";
                return _GetDatePattern;
            }
        }

        public static string GetString(object val)
        {
            if (val == null || val == DBNull.Value)
                return "";

            string ret = val.ToString();

            if (ret == null)
                return "";

            return ret.Trim();
        }

        public static decimal GetDecimal(object val)
        {
            if (val == null || val == DBNull.Value)
                return 0;

            decimal ret = 0;
            try
            {
                ret = Convert.ToDecimal(val);
            }
            catch
            {
            }

            return ret;
        }

        public static int GetInt(object val)
        {
            if (val == null || val == DBNull.Value)
                return 0;

            int ret = 0;
            try
            {
                ret = Convert.ToInt32(val);
            }
            catch
            {
            }

            return ret;
        }

        public static Int64 GetInt64(object val)
        {
            if (val == null || val == DBNull.Value)
                return 0;

            Int64 ret = 0;

            try
            {
                ret = Convert.ToInt64(val);
            }
            catch
            {
            }

            return ret;
        }

        public static bool IsEmpty(object val)
        {
            if (val == null || val == DBNull.Value)
                return true;

            try
            {
                if (val.GetType() == typeof(string))
                {
                    if (FrmaeUtil.GetString(val) == string.Empty) return true;
                }
                else
                {
                    if (FrmaeUtil.GetDecimal(val) == Decimal.Zero) return true;
                }

            }
            catch
            {
            }

            return false;
        }


        private static string _GetToday;
        public static string GetToday
        {
            get
            {
                _GetToday = DateTime.Now.ToString("yyyyMMdd");
                return _GetToday;
            }
        }

        public static string GetSomeMonthSomeDay(int month, int day)
        {
            DateTime date = DateTime.Now;
            date = date.AddMonths(month);
            date = date.AddDays(day);
            return date.ToString("yyyyMMdd");
        }

        private static string _GetMonthFirstDay;
        public static string GetMonthFirstDay
        {
            get
            {
                _GetMonthFirstDay = DateTime.Now.ToString("yyyyMM01");
                return _GetMonthFirstDay;
            }
        }

        private static string _GetMonthLastDay;
        public static string GetMonthLastDay
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime last_day = today.AddMonths(1).AddDays(0 - today.Day);

                _GetMonthLastDay = last_day.ToString("yyyyMMdd");
                return _GetMonthLastDay;
            }
        }

        public static string GetMACAddress()
        {
            string MacAddress = "";

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                System.Net.NetworkInformation.PhysicalAddress pa = adapter.GetPhysicalAddress();
                if (pa != null && !pa.ToString().Equals(""))
                {
                    MacAddress = pa.ToString();
                    break;
                }
            }

            return MacAddress;
        }

        public static string GetIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ClientIP = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ClientIP = host.AddressList[i].ToString();
                }
            }
            return ClientIP;
        }
    }
}
