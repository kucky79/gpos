using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using DevExpress.XtraScheduler;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors;

namespace Bifrost.Win.Controls
{
    public partial class aPeriodMonthEdit : Control
    {
        private bool _ReadOnly;
        private const int FIXED_HEIGHT = 24;

        private Color _BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));

        string _format = @"yyyy\-MM";
        string _outFormat = @"yyyyMM";

        public aPeriodMonthEdit()
        {
            InitializeComponent();

            txtDtFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            txtDtFrom.Properties.Mask.EditMask = _format;
            txtDtFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtDtFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.dateNavigatorFrom.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            txtDtTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            txtDtTo.Properties.Mask.EditMask = _format;
            txtDtTo.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtDtTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.dateNavigatorTo.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            //this.dateNavigatorFrom.DateTime = DateTime.Now;
            //this.dateNavigatorTo.DateTime = DateTime.Now;

            InitEvent();

            //공백 세팅해주기 위해서 이벤트를 잠시 죽였다가 살림. 안그러면 FreeBinding시 isModified 가 되어 문제생김
            txtDtFrom.EditValueChanged -= txtDtFrom_EditValueChanged;
            txtDtTo.EditValueChanged -= txtDtTo_EditValueChanged; ;

            txtDtFrom.EditValue = string.Empty;// dateNavigatorFrom.DateTime.ToShortDateString();
            txtDtTo.EditValue = string.Empty;//dateNavigatorTo.DateTime.ToShortDateString();

            txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;
            txtDtTo.EditValueChanged += txtDtTo_EditValueChanged;

        }

        private void InitEvent()
        {
            this.KeyDown += APeriodEdit_KeyDown;
            this.Enter += APeriodEdit_Enter;

            dateNavigatorFrom.EditValueChanged += dateNavigator_EditDateModified;
            dateNavigatorTo.EditValueChanged += dateNavigator_EditDateModified;
            dateNavigatorFrom.MouseDown += DateNavigatorFrom_MouseDown;
            dateNavigatorTo.MouseDown += DateNavigatorTo_MouseDown;

            dateNavigatorFrom.MouseDoubleClick += DateNavigatorFrom_MouseDoubleClick;

            PickerEdit.QueryCloseUp += PcikerEdit_QueryCloseUp;
            PickerEdit.QueryPopUp += PickerEdit_QueryPopUp;

            txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;
            txtDtTo.EditValueChanged += txtDtTo_EditValueChanged; ;
            txtDtFrom.KeyDown += TxtDtFrom_KeyDown;
            txtDtTo.KeyDown += TxtDtTo_KeyDown;
        }

        private void DateNavigatorFrom_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CalendarHitInfo hitInfo = dateNavigatorFrom.GetHitInfo(e);
            if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
            {
                DtStart = hitInfo.HitDate.ToString(_outFormat);
                //To값이 세팅되면 무조건 창 닫음
                PickerControl.OwnerEdit.ClosePopup();
                txtDtFrom.Focus();
            }
            dateNavigatorTo.Refresh();

        }

        private void PickerEdit_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (txtDtFrom.Text == string.Empty)
                dateNavigatorFrom.DateTime = DateTime.Now;
            if (txtDtTo.Text == string.Empty)
                dateNavigatorTo.DateTime = DateTime.Now;
        }

        //private void BtnMonth_Click(object sender, EventArgs e)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime dt_From;
        //    DateTime dt_To;

        //    int Month = 0;
        //    string ControlName = ((aButtonSizeFree)sender).Name;
        //    switch (ControlName)
        //    {
        //        case "btnMonth1":
        //            Month = -1;
        //        break;
        //        case "btnMonth3":
        //            Month = -3;
        //            break;
        //        case "btnMonth6":
        //            Month = -6;
        //            break;
        //        case "btnYear":
        //            Month = -12;
        //            //dt_From = DateTime.ParseExact(now.Year + "0101", outFormat, null);
        //            //dt_To = DateTime.ParseExact(now.Year + "1231", outFormat, null);

        //            //txtDtFrom.Text = dt_From.ToString(format);
        //            //txtDtTo.Text = dt_To.ToString(format);

        //            //dateNavigatorFrom.DateTime = dt_From;
        //            //dateNavigatorTo.DateTime = dt_To;
        //            break;
        //        case "btnMonthThis":
        //            dt_From = DateTime.ParseExact(now.ToString("yyyy") + now.ToString("MM") + "01", _outFormat, null);
        //            dt_To = DateTime.ParseExact(GetMonthLastDay(now), _outFormat, null);

        //            txtDtFrom.Text = dt_From.ToString(_format);
        //            txtDtTo.Text = dt_To.ToString(_format);

        //            dateNavigatorFrom.DateTime = dt_From;
        //            dateNavigatorTo.DateTime = dt_To;
        //            break;
        //        case "btnMonthLast":
        //            now = now.AddMonths(-1);
        //            dt_From = DateTime.ParseExact(now.ToString("yyyy") + now.ToString("MM") + "01", _outFormat, null);
        //            dt_To = DateTime.ParseExact(GetMonthLastDay(now), _outFormat, null);

        //            txtDtFrom.Text = dt_From.ToString(_format);
        //            txtDtTo.Text = dt_To.ToString(_format);

        //            dateNavigatorFrom.DateTime = dt_From;
        //            dateNavigatorTo.DateTime = dt_To;
        //            break;
        //    }

        //    if (ControlName == "btnMonth1" || ControlName == "btnMonth3" || ControlName == "btnMonth6" || ControlName == "btnYear")
        //    {
        //        string strResult = string.Empty;
        //        if (txtDtTo.Text != string.Empty)
        //        {
        //            DateTime setText = DateTime.Now;
        //            //DateTime setText = DateTime.ParseExact(txtDtTo.Text, format, null);
        //            DateTime CalcDate = setText.AddMonths(Month);
        //            strResult = CalcDate.ToString(_format);
        //            txtDtFrom.Text = strResult;
        //            txtDtTo.Text = setText.ToString(_format);

        //            dateNavigatorFrom.DateTime = CalcDate;
        //            dateNavigatorTo.DateTime = setText;

        //        }
        //        else
        //        {
        //            DateTime setText = DateTime.Now;
        //            DateTime CalcDate = setText.AddMonths(Month);

        //            txtDtFrom.Text = CalcDate.ToString(_format);
        //            txtDtTo.Text = setText.ToString(_format);

        //            dateNavigatorFrom.DateTime = CalcDate;
        //            dateNavigatorTo.DateTime = setText;

        //        }
        //    }

        //    //To값이 세팅되면 무조건 창 닫음
        //    if (PickerControl.OwnerEdit == null)
        //        return;
        //    PickerControl.OwnerEdit.ClosePopup();
        //    txtDtTo.Focus();
        //}

        private string _GetMonthLastDay;
        private string GetMonthLastDay(DateTime day)
        {
            
                DateTime today = day;
                DateTime last_day = today.AddMonths(1).AddDays(0 - today.Day);

                _GetMonthLastDay = last_day.ToString("yyyyMMdd");
                return _GetMonthLastDay;
        }

        public string DateFormat
        {
            get { return _format; }
            set
            {
                _format = value;

                txtDtFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                txtDtFrom.Properties.Mask.EditMask = _format;
                txtDtFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtDtFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

                txtDtTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                txtDtTo.Properties.Mask.EditMask = _format;
                txtDtTo.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtDtTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

            }
        }


        private void TxtDtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtDtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtDtTo.Focus();
            }
        }

        private void APeriodEdit_Enter(object sender, EventArgs e)
        {
            txtDtFrom.Focus();
        }

        private void APeriodEdit_KeyDown(object sender, KeyEventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void DateNavigatorTo_MouseDown(object sender, MouseEventArgs e)
        {
            CalendarHitInfo hitInfo = dateNavigatorTo.GetHitInfo(e);

            if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
            {
                DtEnd = hitInfo.HitDate.ToString(_outFormat);
                //To값이 세팅되면 무조건 창 닫음
                PickerControl.OwnerEdit.ClosePopup();
                txtDtTo.Focus();
            }
            dateNavigatorFrom.Refresh();
        }

        private void DateNavigatorFrom_MouseDown(object sender, MouseEventArgs e)
        {
            CalendarHitInfo hitInfo = dateNavigatorFrom.GetHitInfo(e);
            if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
            {
                DtStart = hitInfo.HitDate.ToString(_outFormat);
            }
            dateNavigatorTo.Refresh();
        }

        private void txtDtFrom_EditValueChanged(object sender, EventArgs e)
        {
            string strResult = string.Empty;

            if (txtDtFrom.Text != string.Empty)
            {
                DateTime setText = DateTime.ParseExact(txtDtFrom.Text, _format, null);
                strResult = setText.ToString(_format);
                dateNavigatorFrom.DateTime = setText;
            }
            
        }

        private void txtDtTo_EditValueChanged(object sender, EventArgs e)
        {
            string strResult = string.Empty;

            if (txtDtTo.Text != string.Empty)
            {
                DateTime setText = DateTime.ParseExact(txtDtTo.Text, _format, null);
                strResult = setText.ToString(_format);
                dateNavigatorTo.DateTime = setText;
            }

        }

        private void PcikerEdit_QueryCloseUp(object sender, CancelEventArgs e)
        {
            txtDtTo.Focus();
        }

        private void dateNavigator_EditDateModified(object sender, EventArgs e)
        {
            txtDtFrom.EditValueChanged -= txtDtFrom_EditValueChanged;
            txtDtTo.EditValueChanged -= txtDtTo_EditValueChanged; ;

            if(txtDtTo.Text == string.Empty)
            {
                txtDtTo.Text = txtDtFrom.Text;
            }

            if (txtDtFrom.Text == string.Empty)
            {
                DtStart = dateNavigatorFrom.DateTime.ToString(_outFormat);//.ToShortDateString().Replace("-", "");
            }

            //DtEnd = dateNavigatorTo.DateTime.ToString(outFormat);// ToShortDateString().Replace("-", "");

            txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;
            txtDtTo.EditValueChanged += txtDtTo_EditValueChanged;

            if (((DateNavigator)sender) == dateNavigatorTo)
            {
                if (PickerControl.OwnerEdit == null)
                    return;
                ////To값이 세팅되면 무조건 창 닫음
                //PickerControl.OwnerEdit.ClosePopup();
                //txtDtTo.Focus();
            }
        }

        //protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        //{
        //    // Set a fixed height for the control.
        //    int fixHeight = 20;
        //    base.SetBoundsCore(x, y, width, fixHeight, specified);
        //}

        [Browsable(true), Description("ReadOnly를 설정합니다."), DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;
                this.txtDtFrom.ReadOnly = value;
                this.txtDtFrom.ReadOnly = value;
            }
        }

        [Browsable(true), Description("시작일을 입력합니다. 형식은 yyyyMM 입니다.")]
        public string DtStart
        {
            get
            {
                string _strResult = string.Empty;
                if (txtDtFrom.Text != string.Empty)
                {
                    DateTime _dtStart = DateTime.ParseExact(txtDtFrom.Text, _format, null);
                    _strResult = _dtStart.ToString(_outFormat);
                }
                return _strResult;

            }//.ToShortDateString().Replace("-", ""); }
            set
            {
                txtDtFrom.EditValueChanged -= txtDtFrom_EditValueChanged;


                string _strResult = string.Empty;
                if (value != _strResult)
                {
                    DateTime parsedDate;
                    bool parsedSuccessfully = DateTime.TryParseExact(value, _outFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                    if (parsedSuccessfully)
                    {
                        dateNavigatorFrom.DateTime = DateTime.ParseExact(value, _outFormat, null);
                        _strResult = dateNavigatorFrom.DateTime.ToString(_format);
                    }
                }

                txtDtFrom.Text = _strResult;
                txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;

            }
        }

        [Browsable(true), Description("종료일을 입력합니다. 형식은 yyyyMM 입니다.")]
        public string DtEnd
        {
            get
            {
                string _strResult = string.Empty;
                if (txtDtTo.Text != string.Empty)
                {
                    DateTime _dtEnd = DateTime.ParseExact(txtDtTo.Text, _format, null);
                    _strResult = _dtEnd.ToString(_outFormat);
                }
                return _strResult;
            }//.ToShortDateString().Replace("-", ""); }
            set
            {
                txtDtTo.EditValueChanged -= txtDtTo_EditValueChanged; ;


                string _strResult = string.Empty;
                if (value != _strResult)
                {
                    DateTime parsedDate;
                    bool parsedSuccessfully = DateTime.TryParseExact(value, _outFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                    if (parsedSuccessfully)
                    {
                        dateNavigatorTo.DateTime = DateTime.ParseExact(value, _outFormat, null);
                        _strResult = dateNavigatorTo.DateTime.ToString(_format);
                    }
                }
                txtDtTo.Text = _strResult;
                txtDtTo.EditValueChanged += txtDtTo_EditValueChanged; ;

            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
           
            base.OnLayout(levent);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Pen penBorder = new Pen(Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(248)))), ((int)(((byte)(251))))));
            //Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //e.Graphics.DrawRectangle(penBorder, rectBorder);

            //Rectangle textRec = new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //TextRenderer.DrawText(e.Graphics, Text, this._CodeBox.Font, textRec, this._CodeBox.ForeColor, this._CodeBox.BackColor, TextFormatFlags.Default);

            this.txtDtFrom.Properties.AutoHeight = false;
            this.txtDtFrom.Properties.Appearance.BorderColor = _BorderColor;
            this.txtDtFrom.Properties.Appearance.Options.UseBorderColor = true;
            this.txtDtFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            this.txtDtTo.Properties.AutoHeight = false;
            this.txtDtTo.Properties.Appearance.BorderColor = _BorderColor;
            this.txtDtTo.Properties.Appearance.Options.UseBorderColor = true;
            this.txtDtTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;


            this.txtDtFrom.Height = FIXED_HEIGHT;
            this.txtDtTo.Height = FIXED_HEIGHT;

            if (this.Size.Height != FIXED_HEIGHT)
                this.Size = new Size(this.Size.Width, FIXED_HEIGHT);



            int X = this.Width;
            int Y = this.Height;

            int calendarBtnSize = 23;

            if (X >= 185)
            {
                PickerEdit.Width = X;

                if (txtDtFrom != null)
                {
                    txtDtFrom.Location = new Point(0, 0);
                    txtDtFrom.Width = (X - Label.Width - calendarBtnSize) / 2;
                }

                if (Label != null)
                {
                    Label.Location = new Point(txtDtFrom.Width, 0);
                }

                if (txtDtTo != null)
                {
                    txtDtTo.Location = new Point(Label.Width + txtDtFrom.Width, 0);
                    txtDtTo.Width = txtDtFrom.Width;
                }
            }


        }

    }
}   
