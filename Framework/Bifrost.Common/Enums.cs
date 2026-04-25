using System;
using System.Collections.Generic;
using System.Text;

namespace Bifrost.Common
{
	#region Common Enumerations

	public enum ResourceType
	{
		Resource, Message
	}

	public enum MessageType
	{
		Question, YesNoCancel, Error, Warning, Information
	}

    public enum MessageCommon
    {
        _의값이중복되었습니다,
        _이가존재하지않습니다,
        입력형식이올바르지않습니다,
        _은는필수입력항목입니다,
        이미진행중인작업이라_할수없습니다,
        _보다커야합니다,
        시작일자보다종료일자가클수없습니다,
        _은는_보다큽니다,
        _와_은는같을수없습니다,
        자료저장중오류가발생하였습니다,
        작업을정상적으로처리하지못했습니다,
        변경된사항이있습니다저장하시겠습니까,
        자료를삭제하시겠습니까,
        기존에등록된자료가있습니다삭제후다시작업하시겠습니까,
        자료가정상적으로저장되었습니다,
        자료가정상적으로삭제되었습니다,
        조건에해당하는내용이없습니다,
        _작업을완료하였습니다,
        이미등록된자료가있습니다,
        선택된자료가없습니다,
        등록되지않은자료입니다,
        변경된내용이없습니다,
        _자료가선택되어있지않습니다,
        _와_은같아야합니다,
        _은_보다작아야합니다,
        _은_보다작거나같아야합니다,
        _은_보다커야합니다,
        _은_보다크거나같아야합니다,
        _자료가선택되었습니다
    }

    public enum LanguageType
    {
        KO,JP,CH,EN
    }

	public enum WinFormToolbarButtonIndex
	{
		INSERT_BUTTON = 0,
		SAVE_BUTTON = 1,
		VIEW_BUTTON = 2,
		DELETE_BUTTON = 3,
		ADDROW_BUTTON = 4,
		DELETEROW_BUTTON = 5,
		CANCEL_BUTTON = 6,
		EXCELEXPORT_BUTTON = 7,
		PRINTPREVIEW_BUTTON = 8,
		PRINT_BUTTON = 9,
		CLOSE_BUTTON = 10
	}

	#endregion Common Enumerations
}
