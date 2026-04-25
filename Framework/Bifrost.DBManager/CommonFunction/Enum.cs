using System;
using System.Collections.Generic;
using System.Text;

namespace Bifrost.CommonFunction
{

    #region SpState Enum
    [Serializable]
    public enum SpState
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        Select = 4
    }
    #endregion

    public enum FormatType
    {
        /// <summary>
        /// 수량
        /// </summary>
        QTY = 1,			// 001 => 수량
        /// <summary>
        /// 단가
        /// </summary>
        UNIT_COST = 2,			// 002 => 단가
        /// <summary>
        /// 금액
        /// </summary>
        AMOUNT = 3,				// 003 => 금액
        /// <summary>
        /// 외화단가
        /// </summary>
        FOREIGN_UNIT_COST = 4,	// 004 => 외화단가
        /// <summary>
        /// 외화금액
        /// </summary>
        FOREIGN_AMOUNT = 5,		// 005 => 외화금액
        /// <summary>
        /// 환율
        /// </summary>
        EXCHANGE_RATE = 6,		// 006 => 환율

    }

    public enum MaskType
    {
        /// <summary>
        /// 날짜
        /// </summary>
        DATE = 1,
        /// <summary>
        /// 시간
        /// </summary>
        TIME = 2,
        /// <summary>
        /// 숫자
        /// </summary>
        NUMERIC = 3,
        YYMM = 4
    }

    
}
