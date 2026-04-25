using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using System.ComponentModel;

namespace Bifrost.Win.Controls
{
    [ToolboxItem(true)]
    public partial class aCheckBox : CheckEdit
    {
        private const int FIXED_HEIGHT = 24;


        public aCheckBox()
        {
            InitializeComponent();
        }

        public override object EditValue
        {
            get
            {
                return base.EditValue;
            }

            set
            {
                this.IsModified = true;
                base.EditValue = value;
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
            if (this.Size.Height != FIXED_HEIGHT)
                this.Size = new Size(this.Size.Width, FIXED_HEIGHT);

        }

        private string _checkedValue = "N";
        /// <summary>
        /// Returns the checked status as a "Y" or "N" value.
        /// </summary>
        public string CheckedValue
        {
            get
            {
                _checkedValue = this.Checked == true ? "Y" : "N";
                return _checkedValue;
            }
            //set
            //{
            //    _CheckedValue = value;
            //}
        }

        bool ShouldSerializeCheckedValue()
        {
            return _checkedValue != "N";
        }

        /// <summary>
        /// Returns the checked status as a "Y" or "N" value.
        /// </summary>
        public bool IsFreeBinding { get; set; } = false;

        bool ShouldSerializeIsFreeBinding()
        {
            return IsFreeBinding != false;
        }

        //public void ResetCheckedValue()
        //{
        //    CheckedValue = "N";
        //}

        ///// <summary>
        ///// FreeBinding을 위한 메서드 입니다. DataRow에 자동으로 추가되는 이벤트를 제거한 메서드 이므로 이용도 외에 다른곳에서는 사용금지입니다.
        ///// </summary>
        //public object FreeBindingEditValue
        //{
        //    get
        //    {
        //        return this.EditValue;
        //    }
        //    set
        //    {
        //        this.EditValue = value;
        //    }
        //}
    }
}
