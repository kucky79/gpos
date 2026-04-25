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

        //protected aControlHelper.ControlEnum.ReadOnly _ReadOnly = aControlHelper.ControlEnum.ReadOnly.None;

        //protected aControlHelper.PopUpParam _PopUpParam;

        protected Color _ItemBackColor = Color.White;

        protected bool _SearchCode = true;

        protected string _ChildMode = "";
        protected int _SelectCount = 0;

        #endregion

        #region Public Property

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
