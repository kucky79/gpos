using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Adv.Controls.aControlHelper
{
    public class ControlEnum
    {
        public enum ReadOnly
        {
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
            ButtonReadOnly,
            /// <summary>
            /// 전부 ReadOnly를 푼다
            /// </summary>
            TotalNotReadOnly
        }

        public enum PeriodType
        {
            YearMonthDay,
            YearMonth
        }
    }
}
