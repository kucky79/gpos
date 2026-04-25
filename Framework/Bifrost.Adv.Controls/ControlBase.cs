using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Bifrost.Adv.Controls.PopUp
{
    public abstract class ControlBase : Control
    {
        #region Members
        private const int FIXED_HEIGHT = 24;

        protected PopUpHelper.PopUpID _PopUpID;
        protected aControlHelper.ControlEnum.ReadOnly _ReadOnly = aControlHelper.ControlEnum.ReadOnly.None;

        protected aControlHelper.PopUpParam _PopUpParam;

        protected Color _ItemBackColor = Color.White;

        protected bool _SearchCode = true;

        protected string _ChildMode = "";
        protected int _SelectCount = 0;

        // 도움창 타입이 NONE일 경우 출력메세지
        private string _NoneTypeMsg = "Please! Set Help Type!";
        private string _MasCode = "";
        private string _userHelpID;
        private string _codeValue = "";
        private string _codeName = "";
        private string _codeParam = "";

        #endregion

        #region Public Property

        public bool _UseUpperCase = true;

        [DefaultValue(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseUpperCase
        {
            get { return _UseUpperCase; }
            set { _UseUpperCase = value; }
        }

        [Category("AIMS"), Browsable(true), Description("도움창 타입을 설정합니다.")]
        public virtual PopUpHelper.PopUpID PopUpID
        {
            get { return _PopUpID; }
            set { _PopUpID = value; }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            
            }
            base.Dispose(disposing);
        }

        #endregion

        #region InitializeComponent

        private void InitializeComponent()
        {
            this.Size = new Size(250, FIXED_HEIGHT);
            this.TabStop = false;
        }

        #endregion

        #region UserHelp Properties

        public string _Code = "";

        [Category("AIMS"), 
        Description("CD_CLAS 코드"),
        DefaultValue(""),
        ]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }


        [Category("AIMS"),
        Description("사용자 정의 도움창을 사용할때 필요한 Code 컬럼을 설정합니다."),
        DefaultValue(""),
        ]
        public string UserCodeValue
        {
            get
            {
                if (string.IsNullOrEmpty(_codeValue))
                    return "";

                return IsCodeValueToUpper ? _codeValue.ToUpper() : _codeValue;
            }
            set
            {
                _codeValue = value;
            }
        }

        
        [Category("AIMS"),
        Description("사용자 정의 도움창을 사용할때 필요한 Name 컬럼을 설정합니다."),
        DefaultValue(""),
        ]
        public string UserCodeName
        {
            get
            {
                return _codeName;
            }
            set
            {
                _codeName = value;
            }
        }

        [Category("AIMS"),
        Description("사용자 정의 도움창을 사용할때 넘기는 매개변수값을 설정합니다."),
        DefaultValue(""),]
        public string UserParams
        {
            get
            {
                return _codeParam;
            }
            set
            {
                _codeParam = value;
            }
        }

        private string _UserPopUpParam = "";
        [Category("AIMS"), Description("추가 PopUp Paramater 입니다. 여러개 입력시 ;로 구분됩니다."), DefaultValue(""),]
        public virtual string UserPopUpParam
        {
            get { return _UserPopUpParam; }
            set
            {
                _UserPopUpParam = value;
            }
        }

        [Category("AIMS")]
        public bool IsCodeValueToUpper { get; set; } = true;

        public bool ShouldSerializeIsCodeValueToUpper() { return IsCodeValueToUpper != true; }
        #endregion

        public enum ReadOnlyEnum
        {
            /// <summary>
            /// 텍스트 박스와 버튼을 모두 endable 시킨다.
            /// </summary>
            None,
            /// <summary>
            /// 텍스트 박스와 버튼을 모두 ReadOnly 시킨다.
            /// </summary>
            TotalReadOnly,
            /// <summary>
            /// TextBox만 ReadOnly시킨다.(자동으로 CodeSearch가 되지 않음)
            /// </summary>
            TextBoxReadOnly,
            /// <summary>
            /// 버튼만 ReadOnly시킨다.(자동으로 도움창이 호출 되지 않음)
            /// </summary>
            ButtonReadOnly
        }

        protected DialogResult CodeSearch()
        {
            if (_PopUpID == PopUpHelper.PopUpID.POPUP_CODE)
            {
                if (_MasCode.Trim() == string.Empty)
                    throw new Exception("MAS CODE(CD_CLAS)를 설정해야 합니다.");
            }

            return DialogResult.Abort;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, FIXED_HEIGHT, specified);
        }

        public override Size MaximumSize
        {
            get
            {
                return new Size(0, FIXED_HEIGHT);
            }
            set
            {
                base.MaximumSize = value;
            }
        }
    }
}
