using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bifrost.Win;

namespace Bifrost.Adv.Controls.aControlHelper
{
    public class PopUpParam
    {
        #region Member

        public bool IsRequireSearchKey = false;

        public DataRow[] MultiRow;          // 다중 선택 도움창 일 경우 필터링을 위한 Row object
        public object PARENT_CONTROL;                   // 호출한 컨트롤(버튼, 콤보 박스 같은 경우)

        public string NoneMsg = "Please! Set Help Type!";                       // 도움창 타입이 설정되지 않았을 경우의 출력 메세지

        public bool ComboCheck = true;                  // 콤보 체크 여부

        private bool _isQueryCancel = false;            // QueryBefore에서 취소했을 경우 도움창 및 코드 검사를 하지 않기 위한 bool값
        public bool IsQueryCancel
        {
            get { return _isQueryCancel; }
            set { _isQueryCancel = value; }
        }

        private string _PARENT_ID = "";         // 호출한 부모 ID
        public string PARENT_ID
        {
            get { return _PARENT_ID; }
        }

        private PopUpHelper.PopUpID _PopUpID; // 도움창 ID
        public PopUpHelper.PopUpID PopUpID
        {
            get { return _PopUpID; }
        }

        private string _userPopUpID = string.Empty;
        public string UserPopUpID
        {
            get { return _userPopUpID; }
            set { _userPopUpID = value; }
        }

        private string _codeValue = "";
        private string _codeName = "";
        #endregion

        public PopUpParam(PopUpHelper.PopUpID type)
        {
            _PopUpID = type;
        }

        public PopUpParam()
        {
        }

        private string _UserPopUpParam = "";
        public string UserPopUpParam
        {
            get { return _UserPopUpParam; }
            set { _UserPopUpParam = value; }
        }

    }
}
