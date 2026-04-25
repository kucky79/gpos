namespace Bifrost
{
    public enum SubSystemType
    {
        /// <summary>
        /// 프레임웍
        /// </summary>
        FRAMEWORK = 0,
        /// <summary>
        /// 시스템
        /// </summary>
        SYS = 1,
        /// <summary>
        /// 마스터
        /// </summary>
        MAS = 2,
        /// <summary>
        /// 고객관련
        /// </summary>
        SAL = 3,
        /// <summary>
        /// 항공
        /// </summary>
        AIR = 4,
        /// <summary>
        /// 해상
        /// </summary>
        SEA = 5,
        /// <summary>
        /// 회계
        /// </summary>
        FIN = 6,
        /// <summary>
        /// 인사
        /// </summary>
        HRS = 7,
        /// <summary>
        /// 그룹웨어
        /// </summary>
        GRW = 8,
        /// <summary>
        /// 통계분석
        /// </summary>
        EIS = 9 ,
        /// <summary>
        /// 정산
        /// </summary>
        INT = 10,
        /// <summary>
        /// 통합수출입
        /// </summary>
        ACC = 11,
        /// <summary>
        /// 생산
        /// </summary>
        PRD = 12,
        /// <summary>기타업무</summary>
        ETC = 99

    };

    public enum LayerType
    {
        None = 0,		// 
        WIN = 10,		// WinForm 프로그램
        WEB = 20,		// Web 페이지
        WSL = 30,		// WebService        
        BSL = 40,		// BSL 컴포넌트
        DSL = 50,		// DSL 컴포넌트        
        USL = 60,		// User Control        
        FWK = 100,		// 공통 프레임웍
    };

    public enum SystemType
    {
        ERP = 0, Groupware = 1, BI = 2, SFA = 3, CRM = 4
    };

    public enum SubFolder { Item, Customer, Employee, Die, Equipment, NCR, Inspection, Board }

    public enum MessageItem
    {
        SubSystemType = 0,
        MessageID = 1,
        MessageType = 2,
        DisplayMessage = 3,
        SummaryMessage = 4,
    };

    public enum FileLenUnit
    {
        Gigabyte,
        Megabyte,
        Killobyte,
    }

    public enum Language
    {
        /// <summary>
        /// 한글
        /// </summary>
        KR = 0,
        /// <summary>
        /// 영어
        /// </summary>
        EN = 1,
        /// <summary>
        /// 일어
        /// </summary>
        JP = 2,
        /// <summary>
        /// 중국어(만달린)
        /// </summary>
        CH = 3,
    }

    public enum UserLoginStatus
    {
        LogInSuccess, //성공
        InvalidCredentials,//사용자ID를 확인하세요
        WrongPassword, //Password오류
        UsageExpired, //사용여부
        UnknownError  //알수없는 오류
    }
}
