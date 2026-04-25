using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Bifrost.CommonFunction;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Printing;
using DevExpress.XtraEditors;

using Bifrost.Win.Controls;
using Bifrost.Adv.Controls;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;

namespace Bifrost.Helper
{
    public class CH
    {
        string GetStringYearMonth { get; }

        public static Dictionary<string, DataTable> _cacheCode = new Dictionary<string, DataTable>();

        #region GetCode

        public static DataTable GetCode(string key)
        {
            if (!_cacheCode.ContainsKey(key))
            {
                DataTable dt = null;
                string query = string.Empty;

                string FirmCode = Global.FirmCode;

                switch (key)
                {
                    case "MAS_BIZ":
                        query = "SELECT CD_BIZ CODE, CASE WHEN CD_BIZ = 'TYO' THEN 'JPN' ELSE CD_BIZ END NAME FROM MAS_BIZ WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_BIZ";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_BIZNM":
                        query = "SELECT CD_BIZ CODE, NM_BIZ NAME FROM MAS_BIZ WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_BIZ";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_FIRM02":
                        query = "SELECT CD_FIRM CODE, NM_FIRM NAME FROM MAS_FIRM WHERE CD_FIRM <> '" + FirmCode + "'  ORDER BY CD_FIRM";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_EMP":
                        query = "SELECT CD_EMP CODE, NM_EMP NAME FROM MAS_EMP WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_EMP";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_DEPT":
                        query = "SELECT CD_DEPT CODE, NM_DEPT NAME FROM MAS_DEPT WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_DEPT";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_UNIT":
                        query = "SELECT CD_UNIT CODE, NM_UNIT NAME, DC_RMK FROM MAS_UNIT WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_UNIT";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_FREIGHT":
                        query = "SELECT CD_CATEGORY, CD_FREIGHT CODE, NM_FREIGHT NAME, RT_TAX NAME1 FROM MAS_FREIGHT WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_FREIGHT";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_TSS":
                        query = "SELECT CD_TSS CODE, NM_TITLE NAME FROM MAS_TSS WHERE CD_FIRM = '" + FirmCode + "' AND CD_BIZ = '" + Global.BizCode + "' ORDER BY CD_TSS";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_CONTAINER":
                        query = "SELECT CD_CONTAINER CODE, CD_CONTAINER NAME, CD_UNIT_VOL FROM MAS_CONTAINER WHERE CD_FIRM = '" + FirmCode + "' ORDER BY CD_CONTAINER";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_BANK":
                        query = "SELECT CD_BANK CODE, NM_BANK NAME FROM MAS_BANK WHERE CD_FIRM = '" + FirmCode + "' ORDER BY NM_BANK";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_GL":
                        query = "SELECT CD_GL CODE, DC_RMK NAME FROM MAS_GL WHERE CD_FIRM = '" + FirmCode + "'";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    default:
                        query = @"SELECT
                                    CD_FLAG AS CODE,        
                                    NM_FLAG AS NAME,    
                                    CD_CLAS,
                                    NO_SEQ,    
                                    DC_REF1
                                FROM MAS_CODEL
                                WHERE CD_FIRM = '" + FirmCode + @"'
                                    AND CD_CLAS = '" + key + @"'
                                    AND YN_USE = 'Y'
                                ORDER BY NO_SEQ";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                }

                _cacheCode.Add(key, dt);
            }

            return _cacheCode[key].Copy();
        }

        public static DataTable GetCode(string[] keys)
        {
            DataTable dt = new DataTable();

            foreach (string key in keys)
            {
                DataTable dt_add = GetCode(key).Copy();
                dt.Merge(dt_add);
            }

            dt.AcceptChanges();

            return dt;
        }

        public static DataTable GetCode(string[] keys, bool addEmptyLine)
        {
            DataTable dt = new DataTable();

            foreach (string key in keys)
            {
                DataTable dt_add = GetCode(key).Copy();
                dt.Merge(dt_add);
            }

            if (!addEmptyLine) return dt;

            DataRow newrow = dt.NewRow();
            newrow["CODE"] = "";
            newrow["NAME"] = "";
            dt.Rows.InsertAt(newrow, 0);
            dt.AcceptChanges();

            return dt;
        }

        public static DataTable GetCode(string key, bool addEmptyLine)
        {
            DataTable dt = GetCode(key).Copy();
            if (!addEmptyLine) return dt;

            DataRow newrow = dt.NewRow();
            newrow["CODE"] = "";
            newrow["NAME"] = "";
            dt.Rows.InsertAt(newrow, 0);
            dt.AcceptChanges();

            return dt;
        }

        public static DataTable GetCode(string[] codeParam, string[] valueParam, bool addEmptyLine)
        {
            if(valueParam.Length != codeParam.Length)
                throw new Exception("코드(Code)와 명칭(Name)의 수가 다릅니다.\n갯수를 맞추어 주세요");

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("NAME", typeof(string)));

            for (int i = 0; i < codeParam.Length; i++)
            {
                DataRow newrow = dt.NewRow();
                newrow["CODE"] = codeParam[i];
                newrow["NAME"] = valueParam[i];
                dt.Rows.Add(newrow);
            }
            dt.AcceptChanges();

            if (addEmptyLine)
            {
                DataRow newrow = dt.NewRow();
                newrow["CODE"] = "";
                newrow["NAME"] = "";
                dt.Rows.InsertAt(newrow, 0);
                dt.AcceptChanges();
            }

            return dt;
        }

        public static DataTable GetCode(Dictionary<string, string> CodeDictionary, bool addEmptyLine)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("NAME", typeof(string)));
        
            foreach (var item in CodeDictionary)
            {
                DataRow newrow = dt.NewRow();
                newrow["CODE"] = item.Key;
                newrow["NAME"] = item.Value;
                dt.Rows.Add(newrow);
            }

            dt.AcceptChanges();

            if (addEmptyLine)
            {
                DataRow newrow = dt.NewRow();
                newrow["CODE"] = "";
                newrow["NAME"] = "";
                dt.Rows.InsertAt(newrow, 0);
                dt.AcceptChanges();
            }

            return dt;
        }

        public static DataTable GetCode(Dictionary<string, string> CodeDictionary)
        {
            return GetCode(CodeDictionary, false);
        }

        #endregion

        #region GetPOSCode

        public static DataTable GetPOSCode(string key)
        {
            if (!_cacheCode.ContainsKey(key))
            {
                DataTable dt = null;
                string query = string.Empty;

                string FirmCode = POSGlobal.StoreCode;

                switch (key)
                {
                    case "MAS_BIZ":
                        query = "SELECT CD_BIZ AS CODE, NM_BIZ AS NAME FROM MAS_BIZ WHERE CD_STORE = '" + FirmCode + "' ORDER BY CD_BIZ";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_EMP":
                        query = "SELECT CD_EMP CODE, NM_EMP NAME FROM MAS_EMP WHERE CD_STORE = '" + FirmCode + "' ORDER BY CD_EMP";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_DEPT":
                        query = "SELECT CD_DEPT CODE, NM_DEPT NAME FROM MAS_DEPT WHERE CD_STORE = '" + FirmCode + "' ORDER BY CD_DEPT";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_UNIT":
                        query = "SELECT CD_UNIT CODE, NM_UNIT NAME, DC_RMK FROM MAS_UNIT WHERE CD_STORE = '" + FirmCode + "' ORDER BY CD_UNIT";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    case "MAS_BANK":
                        query = "SELECT CD_BANK CODE, NM_BANK NAME FROM MAS_BANK WHERE CD_STORE = '" + FirmCode + "' ORDER BY NM_BANK";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                    default:
                        query = @"SELECT
                                    CD_FLAG AS CODE,        
                                    NM_FLAG AS NAME,    
                                    CD_CLAS,
                                    NO_SEQ,    
                                    DC_REF1
                                FROM POS_CODEL
                                WHERE CD_STORE = '" + FirmCode + @"'
                                    AND CD_CLAS = '" + key + @"'
                                    AND YN_USE = 'Y'
                                ORDER BY NO_SEQ";
                        dt = DBHelper.GetDataTable(query, false);
                        break;
                }

                _cacheCode.Add(key, dt);
            }

            return _cacheCode[key].Copy();
        }


        public static DataTable GetPOSCode(string[] keys)
        {
            DataTable dt = new DataTable();

            foreach (string key in keys)
            {
                DataTable dt_add = GetPOSCode(key).Copy();
                dt.Merge(dt_add);
            }

            dt.AcceptChanges();

            return dt;
        }

        public static DataTable GetPOSCode(string[] keys, bool addEmptyLine)
        {
            DataTable dt = new DataTable();

            foreach (string key in keys)
            {
                DataTable dt_add = GetPOSCode(key).Copy();
                dt.Merge(dt_add);
            }

            if (!addEmptyLine) return dt;

            DataRow newrow = dt.NewRow();
            newrow["CODE"] = "";
            newrow["NAME"] = "";
            dt.Rows.InsertAt(newrow, 0);
            dt.AcceptChanges();

            return dt;
        }


        public static DataTable GetPOSCode(string key, bool addEmptyLine)
        {
            DataTable dt = GetPOSCode(key).Copy();
            if (!addEmptyLine) return dt;

            DataRow newrow = dt.NewRow();
            newrow["CODE"] = "";
            newrow["NAME"] = "";
            dt.Rows.InsertAt(newrow, 0);
            dt.AcceptChanges();

            return dt;
        }

        public static DataTable GetPOSCode(string[] codeParam, string[] valueParam, bool addEmptyLine)
        {
            if (valueParam.Length != codeParam.Length)
                throw new Exception("코드(Code)와 명칭(Name)의 수가 다릅니다.\n갯수를 맞추어 주세요");

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("NAME", typeof(string)));

            for (int i = 0; i < codeParam.Length; i++)
            {
                DataRow newrow = dt.NewRow();
                newrow["CODE"] = codeParam[i];
                newrow["NAME"] = valueParam[i];
                dt.Rows.Add(newrow);
            }
            dt.AcceptChanges();

            if (addEmptyLine)
            {
                DataRow newrow = dt.NewRow();
                newrow["CODE"] = "";
                newrow["NAME"] = "";
                dt.Rows.InsertAt(newrow, 0);
                dt.AcceptChanges();
            }

            return dt;
        }

        #endregion

        public static string GetTokenString(TokenEdit ctr)
        {
            string result = string.Empty;
            if (ctr.EditValue == null) return result;

            foreach (string item in ctr.EditValue as BindingList<string>)
            {
                result += item + "|";
            }

            if(result != string.Empty)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        public static void SetCombobox(aLookUpEdit ctr, DataTable dt, bool codeView)
        {
            ctr.Properties.ValueMember = "CODE";
            ctr.Properties.DisplayMember = "NAME";
            ctr.Properties.DataSource = dt;
            ctr.Properties.ShowFooter = false;
            ctr.Properties.ShowHeader = false;
            //ctr.Properties.BestFitRowCount = 5;
            //ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;


            ctr.Properties.NullText = string.Empty;
            ctr.Properties.ShowNullValuePromptWhenFocused = true;
            ctr.Properties.AutoHeight = false;
            ctr.Properties.ShowLines = false;
            ctr.Properties.PopupSizeable = true;
            ctr.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize;
            ctr.Properties.PopupFormMinSize = new System.Drawing.Size(50, 50);
            ctr.Properties.ShowFooter = false;
            ctr.Properties.UseDropDownRowsAsMaxCount = true;
            ctr.Properties.DropDownRows = 15;
            ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            if (dt.Rows.Count > 0)
                ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }

        /// <summary>
        /// 날짜를 원하는 형태로 반환해주는 함수
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inputFormat"></param>
        /// <param name="outputFormat"></param>
        /// <returns></returns>
        public static string stringToDateTime(string date, string inputFormat, string outputFormat)
        {
            string _resultString = string.Empty;

            // 일단 string을 datetime으로 변환.
            DateTime parsedDate = new DateTime();
            bool parsedSuccessfully = false;
            DateTime TmpDate = new DateTime();
            if(date.Length > 10)
            {
                TmpDate = Convert.ToDateTime(date);
                date = TmpDate.ToString(inputFormat);
            }

            if (date.Trim() != string.Empty)
            {
                parsedSuccessfully = DateTime.TryParseExact(date, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            }

            if (parsedSuccessfully)
                _resultString = parsedDate.Date.ToString(outputFormat);

            return _resultString;
        }
        /// <summar>
        /// 날짜를  무조건 yyyyMMdd 형태로 반환해주는 함수
        /// </summary>
        /// <param name="inputFormat"></param>
        /// <param name="outputFormat"></param>
        /// <returns></returns>
        public static string stringToDateTime(string date, string inputFormat)
        {
            return stringToDateTime(date, inputFormat, _outPattern);
        }

        public static string[] SplitString(string args, string delimiter)
        {
            string[] resultString = Regex.Split(args, delimiter);

            return resultString;
        }

        public static object[] GetCodebyArray(string key)
        {
            DataTable dt = GetCode(key).Copy();
            object[] obj_code;
            if (dt == null)
            {
                return null;
            }
            obj_code = new object[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                obj_code[i] = dt.Rows[i]["CODE"];
            }
            return obj_code;
        }

        private static string _outPattern = "yyyyMMdd";

        private static Color _colorMain = Color.FromArgb(170, 203, 239);
        private static Color _colorMainPress = Color.FromArgb(31, 85, 153);
        private static Color _colorSub = Color.FromArgb(199, 225, 239);
        private static Color _colorSubPress = Color.FromArgb(31, 85, 153);
        private static Color _colorDefault = Color.FromArgb(232, 232, 232);
        private static Color _colorDefaultPress = Color.FromArgb(31, 85, 153);
        private static Font _fontDefault = new Font("카이겐고딕 KR Regular", 20F);

        private static Color _colorMainPurchase = Color.FromArgb(112, 173, 71);
        private static Color _colorMainPurchasePress = Color.FromArgb(24, 114, 66);
        private static Color _colorSubPurchase = Color.FromArgb(198, 224, 180);
        private static Color _colorSubPurchasePress = Color.FromArgb(24, 114, 66);

        public static void SetButtonApperanceMain(SimpleButton btn)
        {
            SetButtonApperance(btn, _fontDefault, _fontDefault, Color.Empty, Color.White, _colorMain, _colorMainPress);
        }

        public static void SetButtonApperanceMain(SimpleButton btn, bool IsSales)
        {
            if (IsSales)
                SetButtonApperanceMain(btn);
            else
                SetButtonApperance(btn, _fontDefault, _fontDefault, Color.Empty, Color.White, _colorMainPurchase, _colorMainPurchasePress);
        }

        public static void SetButtonApperanceSub(SimpleButton btn)
        {
            SetButtonApperance(btn, _fontDefault, _fontDefault, Color.Empty, Color.White, _colorSub, _colorSubPress);
        }

        public static void SetButtonApperanceSub(SimpleButton btn, bool IsSales)
        {
            if (IsSales)
                SetButtonApperanceSub(btn);
            else
                SetButtonApperance(btn, _fontDefault, _fontDefault, Color.Empty, Color.White, _colorSubPurchase, _colorSubPurchasePress);

        }

        public static void SetButtonApperanceDefault(SimpleButton btn)
        {
            SetButtonApperance(btn, _fontDefault, _fontDefault, Color.Empty, Color.White, _colorDefault, _colorDefaultPress);
        }

        public static void SetButtonApperance(SimpleButton btn, Font defaultFont, Font pressFont, Color defaultFontColor, Color pressFontColor, Color defaultColor, Color pressColor)
        {

            btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            btn.LookAndFeel.UseDefaultLookAndFeel = false;
            btn.Appearance.BackColor = defaultColor;
            btn.Appearance.BorderColor = defaultColor;

            btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;

            btn.AllowFocus = false;
            btn.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;

            //btn.AllowFocus = false;

            //btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            //btn.LookAndFeel.UseDefaultLookAndFeel = false;
            //btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //btn.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;

            //btn.Appearance.Options.UseBorderColor = true;
            //btn.Appearance.BorderColor = Color.AliceBlue;

            //btn.Appearance.BackColor = defaultColor;
            //btn.Appearance.Options.UseBackColor = true;

            //btn.Appearance.Options.UseFont = true;
            //btn.Appearance.Font = defaultFont;
            //btn.Appearance.ForeColor = defaultFontColor;

            //btn.AppearancePressed.BackColor = pressColor;
            //btn.AppearancePressed.Font = pressFont;
            //btn.AppearancePressed.ForeColor = pressFontColor;
            //btn.AppearancePressed.Options.UseBackColor = true;
            //btn.AppearancePressed.Options.UseFont = true;
            //btn.AppearancePressed.Options.UseForeColor = true;
        }

        public static void SetDateEditFont(aDateEdit aDateEdit, float fontSize)
        {
            aDateEdit.Properties.AllowAnimatedContentChange = false;
            aDateEdit.Properties.AllowFocused = false;
            aDateEdit.Properties.BorderStyle = BorderStyles.Default;
            //aDateEdit.Properties.AutoHeight = true;  


            aDateEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            aDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            aDateEdit.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.Appearance.Options.UseFont = true;
            aDateEdit.Properties.AppearanceDropDown.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceDropDown.Options.UseFont = true;
            aDateEdit.Properties.CalendarTimeProperties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.CalendarTimeProperties.Appearance.Options.UseFont = true;
            aDateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
            aDateEdit.Properties.DisplayFormat.FormatString = "yyyy\\-MM\\-dd";
            aDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            aDateEdit.Properties.EditFormat.FormatString = "yyyy\\-MM\\-dd";
            aDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            aDateEdit.Properties.Appearance.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.Appearance.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.Button.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.Button.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.ButtonHighlighted.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.ButtonHighlighted.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.ButtonPressed.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.ButtonPressed.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.CalendarHeader.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.CalendarHeader.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCell.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCell.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellDisabled.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellDisabled.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellHoliday.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellHoliday.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellInactive.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellInactive.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellPressed.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellPressed.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellSelected.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellSelected.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecial.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecial.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecialPressed.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecialPressed.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecialSelected.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellSpecialSelected.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.DayCellToday.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.DayCellToday.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.HeaderPressed.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.HeaderPressed.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.WeekDay.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.WeekDay.Options.UseFont = true;
            aDateEdit.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            aDateEdit.Properties.AppearanceDropDown.Font = new System.Drawing.Font("카이겐고딕 KR Regular", fontSize);
            aDateEdit.Properties.AppearanceDropDown.Options.UseFont = true;
            aDateEdit.Properties.CellSize = new Size(70, 60);
        }
        public static void SetDateEditFont(aDateEdit aDateEdit)
        {
            SetDateEditFont(aDateEdit, 20F);
        }

    }

    public class SetControl
    {
        public void ClearCombobox(aLookUpEdit ctr)
        {
            ctr.Properties.Columns.Clear();
        }

        public void SetCombobox(aLookUpEdit ctr, DataTable dt, bool codeView)
        {
            ctr.Properties.ValueMember = "CODE";
            ctr.Properties.DisplayMember = "NAME";
            ctr.Properties.DataSource = dt;
            ctr.Properties.ShowFooter = false;
            ctr.Properties.ShowHeader = false;
            //ctr.Properties.BestFitRowCount = 5;
            //ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;


            ctr.Properties.NullText = string.Empty;
            ctr.Properties.ShowNullValuePromptWhenFocused = true;
            ctr.Properties.AutoHeight = false;
            ctr.Properties.ShowLines = false;
            ctr.Properties.PopupSizeable = true;
            ctr.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize;
            ctr.Properties.PopupFormMinSize = new System.Drawing.Size(50, 50);
            ctr.Properties.ShowFooter = false;
            ctr.Properties.UseDropDownRowsAsMaxCount = true;
            ctr.Properties.DropDownRows = 15;
            ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            //ctr.Popup += Ctr_Popup;
            if (codeView)
            {
                LookUpColumnInfo colCode = new LookUpColumnInfo("CODE", "코드", 50);
                LookUpColumnInfo colName = new LookUpColumnInfo("NAME", "코드명", 50);
                ctr.Properties.Columns.AddRange(new LookUpColumnInfo[] { colCode, colName });
            }
            else
            {
                LookUpColumnInfo colName = new LookUpColumnInfo("NAME", "코드명", 50);
                ctr.Properties.Columns.AddRange(new LookUpColumnInfo[] { colName });
            }

            if(dt.Rows.Count > 0)
                ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }

        //private void Ctr_Popup1(object sender, EventArgs e)
        //{
        //    LookUpEdit edit = sender as LookUpEdit;
        //    int popupWidth = (edit as IPopupControl).PopupWindow.Width;
        //    PopupLookUpEditForm f = (edit as IPopupControl).PopupWindow as PopupLookUpEditForm;
        //    if (edit.Width > popupWidth)
        //    {
        //        f.Width = edit.Width;
        //    }
        //}

        //private void Ctr_Popup(object sender, EventArgs e)
        //{
        //    aLookUpEdit edit = sender as aLookUpEdit;
        //    edit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
        //}

        /// <summary>
        /// aLookUpEdit에 DataTable을 바인딩하여 원하는 컬럼을 보여줍니다. 
        /// 최소 2개 이상의 값을 넘겨줘야 합니다.
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="dt"></param>
        /// <param name="objValue"></param>
        /// <param name="objName"></param>
        /// <returns></returns>
        public void SetCombobox(aLookUpEdit ctr, DataTable dt, bool codeView, bool edit)
        {
            ctr.Properties.ValueMember = "CODE";
            ctr.Properties.DisplayMember = "NAME";
            ctr.Properties.DataSource = dt;
            ctr.Properties.ShowFooter = false;
            ctr.Properties.ShowHeader = false;
            //ctr.Properties.BestFitRowCount = 5;
            //ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;


            ctr.Properties.NullText = string.Empty;
            ctr.Properties.ShowNullValuePromptWhenFocused = true;
            ctr.Properties.AutoHeight = false;
            ctr.Properties.ShowLines = false;
            ctr.Properties.PopupSizeable = true;
            ctr.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize;
            ctr.Properties.PopupFormMinSize = new System.Drawing.Size(50, 50);
            ctr.Properties.ShowFooter = false;
            ctr.Properties.UseDropDownRowsAsMaxCount = true;
            ctr.Properties.DropDownRows = 15;
            ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            //ctr.Popup += Ctr_Popup;
            if (codeView)
            {
                LookUpColumnInfo colCode = new LookUpColumnInfo("CODE", "코드", 50);
                LookUpColumnInfo colName = new LookUpColumnInfo("NAME", "코드명", 50);
                ctr.Properties.Columns.AddRange(new LookUpColumnInfo[] { colCode, colName });
            }
            else
            {
                LookUpColumnInfo colName = new LookUpColumnInfo("NAME", "코드명", 50);
                ctr.Properties.Columns.AddRange(new LookUpColumnInfo[] { colName });
            }
            
            if (dt.Rows.Count > 0 && edit)
                ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }

       
        public void SetCombobox(aLookUpEdit ctr, DataTable dt, string[] objValue, string[] objName)
        {
            if(objValue.Length != objName.Length)
            {
                throw new Exception("코드(Code)와 명칭(Name)의 수가 다릅니다.\n갯수를 맞추어 주세요");
            }

            ctr.Properties.ValueMember = objValue[0];
            ctr.Properties.DisplayMember = objValue[1];
            ctr.Properties.DataSource = dt;
            ctr.Properties.ShowHeader = true;
            ctr.Properties.ShowFooter = true;
            ctr.Properties.PopupWidth = objValue.Length * 80;
            //ctr.Properties.PopupResizeMode = ResizeMode.LiveResize;

            for (int i = 0; i < objValue.Length; i++)
            {
                ctr.Properties.Columns.AddRange(new LookUpColumnInfo[] { new LookUpColumnInfo(objValue[i], objName[i]) });
            }
            if (dt.Rows.Count > 0)
                ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }

        /// <summary>
        /// aLookUpEdit에 DataTable을 바인딩하여 원하는 컬럼을 보여줍니다. 
        /// 최소 2개 이상의 값을 넘겨줘야 합니다.
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="dt"></param>
        /// <param name="objValue"></param>
        /// <param name="objName"></param>
        /// <param name="addEmptyLine"></param>
        /// <returns></returns>
        public void SetCombobox(aLookUpEdit ctr, DataTable dt, string[] objValue, string[] objName, bool addEmptyLine)
        {
            if (objValue.Length != objName.Length)
            {
                throw new Exception("코드(Code)와 명칭(Name)의 수가 다릅니다.\n갯수를 맞추어 주세요");
            }

            DataRow nr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                nr[i] = DBNull.Value;
                //if (dt.Columns[i].DataType == typeof(String))
                //{
                //    nr[i] = "";
                //}
                //else if (dt.Columns[i].DataType == typeof(DateTime))
                //{
                //    nr[i] = DBNull.Value;
                //}
                //else if (dt.Columns[i].DataType == typeof(Decimal))
                //{
                //    nr[i] = 0;
                //}
            }
            dt.Rows.InsertAt(nr, 0);
            dt.AcceptChanges();

            ctr.Properties.ValueMember = objValue[0];
            ctr.Properties.DisplayMember = objValue[1];
            ctr.Properties.DataSource = dt;
            ctr.Properties.ShowHeader = true;
            ctr.Properties.ShowFooter = true;
            ctr.Properties.PopupWidth = objValue.Length * 80;
            //ctr.Properties.PopupResizeMode = ResizeMode.LiveResize;

            for (int i = 0; i < objValue.Length; i++)
            {
                ctr.Properties.Columns.AddRange(new LookUpColumnInfo[] { new LookUpColumnInfo(objValue[i], objName[i]) });
            }
            if (dt.Rows.Count > 0)
                ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }

        public void SetCombobox(aLookUpEdit ctr, object dt)
        {
            SetCombobox(ctr, dt);
        }

        public void SetCombobox(aLookUpEdit ctr, DataTable dt)
        {
            SetCombobox(ctr, dt, false);
        }

        public void SetCombobox(RepositoryItemLookUpEdit ctr, object dt, bool codeView)
        {
            SetCombobox(ctr, dt, codeView);
        }

        public void SetCombobox(RepositoryItemLookUpEdit ctr, DataTable dt) 
        {
            ctr.ValueMember = "CODE";
            ctr.DisplayMember = "NAME";
            ctr.DataSource = dt;

        }

        public void SetCombobox(RepositoryItemLookUpEdit ctr, object dt)
        {
            SetCombobox(ctr, dt);
        }

        public void SetCombobox(RepositoryItemLookUpEdit ctr, DataTable dt, bool codeView)
        {

            SetCombobox(ctr, dt);

            if (!codeView)
            {
                LookUpColumnInfo colName = new LookUpColumnInfo("NAME", "코드명");
                ctr.Columns.AddRange(new LookUpColumnInfo[] { colName });
            }
            else
            {
                LookUpColumnInfo colName = new LookUpColumnInfo("NAME", "코드명");
                LookUpColumnInfo colCode = new LookUpColumnInfo("CODE", "코드");
                ctr.Columns.AddRange(new LookUpColumnInfo[] { colCode, colName });
            }

        }

        public void SetCombobox(aLookUpEdit ctr, object dt, bool codeView)
        {
            SetCombobox(ctr, dt, codeView);
        }

        public static DataTable GetUserCode(string[] Code, string[] Name)
        {
            if (Code.Length != Name.Length) throw new Exception("코드(Code)와 명칭(Name)의 수가 다릅니다./n갯수를 맞추어 주세요");

            DataTable dt = new DataTable();
            dt.Columns.Add("CODE", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            for (int i = 0; i < Code.Length; i++)
            {
                DataRow nr = dt.NewRow();
                nr["CODE"] = Code[i];
                nr["NAME"] = Name[i];
                dt.Rows.Add(nr);
            }
            dt.AcceptChanges();

            return dt;
        }

        public static DataTable GetUserCode(string[] Code, string[] Name, bool addEmptyLine)
        {
            DataTable dt = GetUserCode(Code, Name);
            if (!addEmptyLine) return dt;

            DataRow nr = dt.NewRow();
            nr["CODE"] = "";
            nr["NAME"] = "";
            dt.Rows.InsertAt(nr, 0);
            dt.AcceptChanges();

            return dt;
        }

        public void ControlClear(Control.ControlCollection Controls)
        {
            foreach (Control ctr in Controls)
            {
                switch (ctr.GetType().Name)
                {
                    case "aTextEdit":
                        ((aTextEdit)ctr).Text = string.Empty;
                        break;
                    case "aNumericText":
                        ((aNumericText)ctr).DecimalValue = 0m;
                        break;
                    case "aMemoEdit":
                        ((aMemoEdit)ctr).Text = string.Empty;
                        break;
                    case "aDateEdit":
                        ((aDateEdit)ctr).Text = string.Empty;
                        break;
                    case "aPeriodEdit":
                        ((aPeriodEdit)ctr).DtStart = string.Empty;
                        ((aPeriodEdit)ctr).DtEnd = string.Empty;
                        break;
                    case "aLookUpEdit":
                        ((aLookUpEdit)ctr).EditValue = string.Empty;
                        break;
                    case "aCodeNText":
                        ((aCodeNText)ctr).SetCodeNameNValue(string.Empty, string.Empty);
                        break;
                    case "aCodeText":
                        ((aCodeText)ctr).SetCodeNameNValue(string.Empty, string.Empty);
                        break;
                    case "aCodeTextNonPopup":
                        ((aCodeTextNonPopup)ctr).SetCodeNameNValue(string.Empty, string.Empty);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class A
    {
        public static string GetDatePatternRegex
        {
            get
            {
                string YYYY = "([0-9][0-9][0-9][0-9])";
                string MM = "(0[0-9]|1[0-2])";
                string DD = "(0[1-9]|[1-2][0-9]|3[0-1])";

                return GetDatePattern.Replace("yyyy", YYYY).Replace("MM", MM).Replace("dd", DD);
            }
        }
        #region 쿼리작성시 필요한 매개변수를 "'" 붙여서 리턴

        public static string I(object strValue)
        {
            string strTemp = "";
            if (strValue == null)
                strTemp = "";
            else
            {
                strTemp = strValue.ToString().Trim();
                strTemp = strTemp.Replace("'", "''");
            }
            if (strTemp == "")
                strTemp = "NULL";
            else
                strTemp = "N'" + strTemp + "'";

            return strTemp;
        }
        public static string I2(object strValue)
        {
            string strTemp = "";
            if (strValue == null)
                strTemp = "";
            else
            {
                strTemp = strValue.ToString().Trim();
                strTemp = strTemp.Replace("'", "''");
            }
            if (strTemp == "")
                strTemp = "NULL";
            else
                strTemp = "N' " + strTemp + "'";

            return strTemp;
        }

        public static string II(object strValue)
        {
            string strTemp = strValue.ToString().Trim();
            strTemp = strTemp.Replace("'", "''");

            if (strTemp == "")
                strTemp = "N''";
            else
                strTemp = "N'" + strTemp + "'";

            return strTemp;
        }

        public static string I3(object strValue)
        {
            string strTemp = "";

            if (strValue == null)
                strTemp = "";

            strTemp = strValue.ToString();

            return strTemp;
        }

        /// <summary>
        /// decimal/datetime/integer 인경우.. Null 리턴.. 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string D(object strValue)
        {
            string strTemp = strValue.ToString().Trim().Replace(",", "");

            if (strTemp == "")
                return "NULL";
            else
                return strTemp;
        }

        
        public static object DN(object objValue)
        {
            if (objValue == null)
                return System.DBNull.Value;
            else if (objValue.ToString().Trim() == "")
                return System.DBNull.Value;
            else
                return objValue;
        }

      	
        public static decimal DNZero(object objValue)
        {
            if (objValue == null || objValue.ToString().Trim() == "" || objValue.ToString().Trim() == "NaN")
                return 0m;
            else
                return decimal.Parse(objValue.ToString());
        }


        
        public static string U(object strValue)
        {
            string strTemp = strValue.ToString().Trim();
            strTemp = strTemp.Replace("'", "''");

            if (strTemp == "")
                strTemp = "NULL";
            else
                strTemp = "N'" + strTemp.ToUpper() + "'";

            return strTemp;
        }


        #endregion

        public static string GetDatePattern
        {
            get
            {
                Bifrost.Helper.POSConfig _configDateFormat = POSConfigHelper.GetConfig("SYS100");

                if (_configDateFormat == null)
                {
                    _configDateFormat = new Bifrost.Helper.POSConfig();
                    _configDateFormat.ConfigCode = "SYS100";
                    _configDateFormat.ConfigName = "날짜형식";
                    _configDateFormat.ConfigValue = @"yyyy\-MM\-dd";
                    _configDateFormat.ConfigDescript = "날짜형식을 설정합니다.";
                    POSConfigHelper.SetConfig(_configDateFormat, "SYS");
                }

                string _GetDatePattern = _configDateFormat.ConfigValue;// @"MM\/dd\/yyyy"; //
                return _GetDatePattern;
            }
        }

        public static int GetByteLength(string val)
        {
            return Encoding.Default.GetBytes(val).Length;
        }

        public static string SetString(string targetString, int Lenth, char CharSet)
        {
            return targetString.PadLeft(Lenth, CharSet);
        }

        public static string GetString(object val)
        {
            return GetString(val, false);
        }

        public static string GetString(object val, bool isUpper)
        {
            if (val == null || val == DBNull.Value)
                return "";

            string ret = string.Empty;
            if (isUpper)
                ret = val.ToString().ToUpper();
            else
                ret = val.ToString();

            if (ret == null)
                return "";

            return ret.Trim();
        }

        public static string GetNumericString(decimal val)
        {
            if (val == 0)
                return "0";

            string ret = string.Empty;
            ret = string.Format("{0:##,##0.###}", val);

            if (ret == null)
                return "0";

            return ret.Trim();
        }

        public static string GetNumericString(object val)
        {
            if(val.ToString() == string.Empty) return string.Empty;

            decimal tmp = decimal.Parse(val.ToString());

            string ret = string.Empty;
            ret = string.Format("{0:##,##0.###}", tmp);

            if (ret == null)
                return "0";

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

        public static float GetFloat(object val)
        {
            if (val == null || val == DBNull.Value)
                return 0F;

            float ret = 0F;
            try
            {
                ret = Convert.ToSingle(val);
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
                    if (A.GetString(val) == string.Empty) return true;
                }
                else
                {
                    if (A.GetDecimal(val) == Decimal.Zero) return true;
                }

            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// ip주소를 리턴합니다.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// MAC 주소를 리턴합니다.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 외부ip주소를 리턴합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetRealIPAddress()
        {
            string PublicIP = string.Empty;

            try
            {
                PublicIP = new WebClient().DownloadString("https://api.ipify.org/");
            }
            catch
            {

            }
            return PublicIP;
        }

        public static string GetToday
        {
            get { return DateTime.Now.ToString("yyyyMMdd"); }
        }

        /// <summary>
        /// 오늘은 기준으로 MONTH, DAY 더해서 반환(day가 0 = 해당월 1일, 32 = 해당월 말일)
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetSomeMonthSomeDay (int month, int day)
        {
            string result = string.Empty;

            DateTime date = DateTime.Now;
            date = date.AddMonths(month);
            if(day==0)
            {
                date = date.AddDays(1 - date.Day);
            }
            else if (day==32)
            {
                date = date.AddMonths(1).AddDays(0 - date.Day);
            }
            else
            {
                date = date.AddDays(day);
            }

            return date.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 입력받은 날짜 기준으로 MONTH, DAY 더해서 반환(day가 0 = 해당월 1일, 32 = 해당월 말일)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetSomeMonthSomeDay(string date, int month, int day)
        {
            string result = string.Empty;

            DateTime _date = DateTime.ParseExact(A.GetString(date), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            _date = _date.AddMonths(month);
            if (day == 0)
            {
                _date = _date.AddDays(1 - _date.Day);
            }
            else if (day == 32)
            {
                _date = _date.AddMonths(1).AddDays(0 - _date.Day);
            }
            else
            {
                _date = _date.AddDays(day);
            }

            return _date.ToString("yyyyMMdd");
        }

        public static string GetMonthFirstDay
        {
            get { return DateTime.Now.ToString("yyyyMM01"); }
        }

        public static string GetMonthLastDay
        {
            get { return DateTime.Today.ToString("yyyyMM") + DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month).ToString(); }
        }

        public static string GetSlipNo(string ModuleCode, int MenuId)
        {
            try
            {
                return GetSlipNo(ModuleCode, MenuId, GetToday);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GetSlipNo(string ModuleCode, int MenuId, string DtSlip)
        {
            try
            {
                object[] outs;
                DBHelper.ExecuteNonQuery("USP_SYS_SLIPCOUNT", new object[] { Global.FirmCode, DtSlip.Replace("-", "").Replace("/", ""), ModuleCode, MenuId }, out outs);
                return outs[0].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GetPOSSlipNo(string StoreCode, string DtSlip, string PreFix, int SerialQty)
        {
            try
            {
                object[] outs;
                DBHelper.ExecuteNonQuery("USP_POS_SLIPCOUNT_CUSTOM", new object[] { StoreCode, DtSlip.Replace("-", "").Replace("/", ""), PreFix, SerialQty }, out outs);
                return outs[0].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Prefix를 DB에 없어도 마음대로 만들어 넣을수 있다
        /// </summary>
        /// <param name="ModuleCode"></param>
        /// <param name="MenuId"></param>
        /// <param name="PreFix"></param>
        /// <returns></returns>
        public static string GetSlipNoCommon(string ModuleCode, int MenuId, string PreFix)
        {
            try
            {
                return GetSlipNoCommon(ModuleCode, MenuId, PreFix, GetToday );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// Prefix를 DB에 없어도 마음대로 만들어 넣을수 있다
        /// </summary>
        /// <param name="ModuleCode"></param>
        /// <param name="MenuId"></param>
        /// <param name="DtSlip"></param>
        /// <param name="PreFix"></param>
        /// <returns></returns>
        public static string GetSlipNoCommon(string ModuleCode, int MenuId, string PreFix, string DtSlip)
        {
            try
            {
                object[] outs;
                DBHelper.ExecuteNonQuery("AP_SYS_AUTOCOUNT", new object[] { Global.FirmCode, ModuleCode, MenuId, PreFix, DtSlip.Replace("-", "").Replace("/", "") }, out outs);
                return outs[0].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GetSlipNoNew(string cdBiz, string ModuleCode, int MenuId)
        {
            try
            {
                return GetSlipNoNew(cdBiz, ModuleCode, MenuId, GetToday);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GetSlipNoNew(string cdBiz, string ModuleCode, int MenuId, string DtSlip)
        {
            try
            {
                object[] outs;
                DBHelper.ExecuteNonQuery("AP_SYS_SLIPCOUNT_NEW", new object[] { Global.FirmCode, cdBiz, DtSlip.Replace("-", "").Replace("/", ""), ModuleCode, MenuId }, out outs);
                return outs[0].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GetDatatableToString(object dt)
        {

            string result = string.Empty;

            if (dt == null) return result;

            DataTable dtResult = dt as DataTable;

            if (dtResult.Rows.Count == 0) return result;

            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                result += dtResult.Rows[i][0].ToString() + "|";
            }

            return result.Substring(0, result.Length - 1);
        }
        //public static string GetCdform(string cdmenu, string code)
        //{
        //    try
        //    {
        //        DataTable Dt = DBHelper.GetDataTable("AP_GRW_APPLINK_DOC_S", new object[] { Global.FirmCode, cdmenu, code });
        //        string Rerturn = "";
        //        if (Dt.Rows.Count > 0)
        //        {
        //            Rerturn = GetString(Dt.Rows[0]["CD_FORM"]);
        //        }
        //        return Rerturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}

        public static string GetDummyString
        {
            get { return "!@#$"; }
        }

        /// <summary>
        /// 임시 전표 번호 생성 (yyyyMMddHHmmss)
        /// </summary>
        public static string GetDummySlipNo
        {
            get { return "TMP" + DateTime.Now.ToString("yyyyMMddHHmmss"); }
        }

        public static string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }

        public static decimal GetCeiling(decimal v)
        {
            try
            {
                return GetCeiling(v, 0);
                //return Math.Ceiling(v);// 올림
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetCeiling(decimal v, int point)
        {
            try
            {
                decimal result = v / Decimal.Parse(Math.Pow(10, point).ToString());
                return Math.Ceiling(result) * Decimal.Parse(Math.Pow(10, point).ToString());//올림
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetRound(decimal v)
        {
            try
            {
                return Math.Round(v);// 반올림
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetRound(decimal v, int point)
        {
            try
            {
                return Math.Round(v, point, MidpointRounding.AwayFromZero);// 반올림
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTruncate(decimal v)
        {
            try
            {
                return Math.Truncate(v);// 버림    
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTruncate(decimal v, int point)
        {
            try
            {

                decimal result = v / Decimal.Parse(Math.Pow(10, point).ToString());
                return Math.Truncate(result) * Decimal.Parse(Math.Pow(10, point).ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetVatRate(string VatType)
        {
            try
            {
                decimal RT_VAT = 0;
                DataTable _dt_VAT = DBHelper.GetDataTable("SELECT CD_FLAG AS FG_VAT, NM_FLAG AS NM_VAT, ISNULL(CONVERT(NUMERIC(5,0), DC_REF1), 0) AS RT_VAT FROM POS_CODEL WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND CD_CLAS = 'SYS013'");

                DataRow[] dr = _dt_VAT.Select("FG_VAT = '" + VatType + "'");
                if (dr.Length > 0)
                {
                    RT_VAT = A.GetDecimal(dr[0]["RT_VAT"]);
                }
                return RT_VAT;
            }
            catch
            {
                return 0;
            }
        }

        public static Form GetForm(string formName)
        {
            foreach (Form frm in Application.OpenForms)
                if (frm.GetType().Name == formName)
                    return frm;

            return null;
        }
    }

    public static class FindByNameUtil
    {
        public static T FindByName<T>(this object targetClass, string name) where T : class
        {
            System.Reflection.FieldInfo fi = targetClass.GetType().GetField(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return fi.GetValue(targetClass) as T;
        }

        public static T FindByName<T>(this string name, object targetClass) where T : class
        {
            System.Reflection.FieldInfo fi = targetClass.GetType().GetField(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return fi.GetValue(targetClass) as T;
        }
        public static System.Windows.Forms.Label Is_Label_From(this string name, object targetClass)
        {
            return FindByName<System.Windows.Forms.Label>(targetClass, name);
        }
        public static System.Windows.Forms.TextBox Is_TextBox_From(this string name, object targetClass)
        {
            return FindByName<System.Windows.Forms.TextBox>(targetClass, name);
        }
        public static System.Windows.Forms.TextBox TextBox(string name, object targetClass)
        {
            return FindByName<System.Windows.Forms.TextBox>(targetClass, name);
        }
        public static System.Windows.Forms.Label Label(string name, object targetClass)
        {
            return FindByName<System.Windows.Forms.Label>(targetClass, name);
        }
    }
}
