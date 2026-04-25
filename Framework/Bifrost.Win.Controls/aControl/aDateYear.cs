using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{

    public class aDateYear : DateEdit
    {
        private const int FIXED_HEIGHT = 24;
        string _format = "yyyy";
        string _outFormat = @"yyyy";
        DateTime _minDay = DateTime.MinValue;

        static aDateYear()
        {
            RepositoryItemaDateYear.RegisteraDateEdit();
        }

        public aDateYear() : base()
        {
            RepositoryItemaDateYear.RegisteraDateEdit();

            this.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Text = string.Empty;
            this.EditValue = null;
            this.Properties.Mask.EditMask = "yyyy";
            this.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.Properties.ShowClear = true;

        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemaDateYear.aDateEditName;
            }
        }
        protected override void InitLayout()
        {

            this.Properties.NullDate = DateTime.MinValue;
            this.Properties.NullText = String.Empty;
            base.InitLayout();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemaDateYear Properties
        {
            get
            {
                return (RepositoryItemaDateYear)base.Properties;
            }
        }

        public override string Text
        {
            get
            {
                string strResult = string.Empty;
                if (base.Text != string.Empty)
                {
                    strResult = this.DateTime.ToShortDateString().Replace("-", "").Substring(0, 4);
                }

                return strResult;
            }
            set
            {
                string resultString = string.Empty;
                if (value != string.Empty)
                {
                    if (value != "        ")
                    {
                        DateTime setText = new DateTime();
                        //yyyyMM일 경우는 
                        if (value.Length == 4)
                        {
                            setText = DateTime.ParseExact(value + "0101", "yyyyMMdd", null);
                        }
                        //그외에 구분자(-,/,.공백 등)은 아래 메서드 이용
                        else
                        {
                            DateTime.TryParse(value + "01", null, DateTimeStyles.AssumeLocal, out setText);
                        }
                        resultString = setText.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    resultString = value;
                }
                base.Text = resultString;

                this.IsModified = true;
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            this.Properties.AutoHeight = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.ReadOnly == true)
            {
                this.Properties.Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
                this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
                this.Properties.Appearance.Options.UseBackColor = true;
                this.Properties.Appearance.Options.UseBorderColor = true;
                this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            else
            {
                this.Properties.Appearance.BackColor = System.Drawing.Color.White;
                this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
                this.Properties.Appearance.Options.UseBackColor = true;
                this.Properties.Appearance.Options.UseBorderColor = true;
                this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }

            if (this.Size.Height != FIXED_HEIGHT)
                this.Size = new Size(this.Size.Width, FIXED_HEIGHT);

        }

    }
}
