namespace Bifrost
{
    /// <summary>
    /// SystemBase 클래스
    /// </summary>
    public class SystemBase
    {
        public SystemBase()
        {
        }
        /// <summary>
        /// 서브시스템 타입을 리턴
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static Bifrost.SubSystemType ReturnSubType(string sub)
        {
            Bifrost.SubSystemType oSub = Bifrost.SubSystemType.FRAMEWORK;

            if (sub != null && sub.Length == 0)
            {
                oSub = Bifrost.SubSystemType.FRAMEWORK;
            }
            else
            {
                switch (sub.ToUpper())
                {
                    case "SYS":
                        oSub = Bifrost.SubSystemType.SYS;
                        break;
                    case "MAS":
                        oSub = Bifrost.SubSystemType.MAS;
                        break;
                    case "HRS":
                        oSub = Bifrost.SubSystemType.HRS;
                        break;
                    case "GRW":
                        oSub = Bifrost.SubSystemType.GRW;
                        break;
                    case "EIS":
                        oSub = Bifrost.SubSystemType.EIS;
                        break;
                    case "ETC":
                        oSub = Bifrost.SubSystemType.ETC;
                        break;
                    case "POS":
                        oSub = Bifrost.SubSystemType.ETC;
                        break;
                    default:
                        oSub = Bifrost.SubSystemType.FRAMEWORK;
                        break;
                }
            }

            return oSub;
        }

        /// <summary>
        /// 파일이름을 리턴
        /// </summary>
        /// <param name="sub">서브시스템 이름을 Return</param>
        /// <returns></returns>
        public static string ReturnFileName(SubSystemType sub)
        {
            return sub.ToString("f");
        }
        /// <summary>
        /// 레이어 이름을 리턴
        /// </summary>
        /// <param name="layer">레이어 타입</param>
        /// <returns></returns>
        public static string GetLayerString(LayerType layer)
        {

            return layer.ToString("f");
        }
    }
}
