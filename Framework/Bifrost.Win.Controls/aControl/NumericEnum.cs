using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Win.Controls
{
    public enum SetNumericType
    {
        /// <summary>
        /// 기본값
        /// </summary>
        NONE = 0,
        /// <summary>
        /// 반내림
        /// </summary>
        ROUNDDOWN = 1,
        /// <summary>
        /// 반올림
        /// </summary>
        ROUNDUP = 2,
        /// <summary>
        /// 버림
        /// </summary>
        FLOOR = 3,
        /// <summary>
        /// 올림
        /// </summary>
        CEIL = 4
    }

    public enum ButtonDesignType
    {
        NONE = 0,
        ADD = 1,
        DELETE = 2,
        REFERENCE = 3,
        ADD_LABEL = 4,
        DEL_LABEL = 5,
        TOP_LV1 = 6,
        TOP_LV2 = 7,
        POP_SEARCH = 20,
        ETC = 99
    }
}
