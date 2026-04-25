using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Grid
{
    public enum DataModeEnum
    {
        /// <summary>
        /// Form Load 직후 또는 추가 버튼을 누른 후
        /// </summary>
        InsertAfterModify,
        /// <summary>
        /// 조회 버튼을 눌러 프리폼 정보를 가져온 후
        /// </summary>
        SearchAfterModify
    }

    public class GridHelperArgs : EventArgs
    {
        public readonly DataRow Row;
        public readonly DataModeEnum JobMode;

        public GridHelperArgs(DataRow row, DataModeEnum jobMode)
        {
            Row = row;
            JobMode = jobMode;
        }
    }


    /// <summary>
    /// 바인딩된 각 컨트롤의 값을 사용자가 변경했을 때 발생하는 이벤트를 처리할 이벤트 핸들러의 시그너처
    /// </summary>
    /// <param name="sender">사용자가 값을 변경한 컨트롤</param>
    /// <param name="e"></param>
    public delegate void FreeBindingEventHandler(object sender, GridHelperArgs e);

}
