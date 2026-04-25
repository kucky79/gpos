using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Bifrost.Helper
{
    public class ApiHelper
    {
        public static string GetPosetCode(string search, int pageNo, int rowCount, List<string> postNo, out int resultCount)
        {
            //* 공공데이터포털(http://www.data.go.kr) 오픈 API 이용
            // 현재 개발계정 상태 (2019. 05.10까지 유효, 이후 연장해야) 추후 운영계정 신청=일일 트래픽 조정
            // 서비스명 : 통합검색 5자리 우편번호 조회서비스
            // 새 우편번호(2015-08-01부터) 오픈 API 주소
            // http://openapi.epost.go.kr/postal/retrieveNewAdressAreaCdSearchAllService/retrieveNewAdressAreaCdSearchAllService/getNewAddressListAreaCdSearchAll

            // [in] search : 검색어 (도로명주소[도로명/건물명] 또는 지번주소[동/읍/면/리])
            // [in] pageNo : 읽어올 페이지(1부터), 
            //       rowCount : 한 페이지당 출력할 목록 수(최대 50까지)
            // [out] postNo[i*3 +0]=우편번호, v[i*3 +1]=도로명주소, v[i*3 +2]=지번주소, v.Count/3=표시할 목록 수
            // [out] resultCount : 검색한 전체 목록(우편번호) 개수
            // 반환값 : 에러메시지, null == OK

            resultCount = 0;

            const string key = "V9TjN8nXPltAgUqVVTiDcUuL2H4e9zbrvN/ouNyvcOKWT6MXhMPPLZJWpy3YwCrIhkhjG2LvRqoQNuHX91Xd7Q==";

            try
            {
                HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(
                    "http://openapi.epost.go.kr/postal/retrieveNewAdressAreaCdSearchAllService/retrieveNewAdressAreaCdSearchAllService/getNewAddressListAreaCdSearchAll"
                    + "?ServiceKey=" + key // 서비스키
                    + "&countPerPage=" + rowCount // 페이지당 출력될 개수를 지정(최대 50)
                    + "&currentPage=" + pageNo // 출력될 페이지 번호
                    + "&srchwrd=" + HttpUtility.UrlPathEncode(search) // 검색어
                    );
                rq.Headers = new WebHeaderCollection();
                rq.Headers.Add("Accept-language", "ko");
                bool bOk = false; // <successYN>Y</successYN> 획득 여부
                search = null; // 에러 메시지
                HttpWebResponse rp = (HttpWebResponse)rq.GetResponse();
                XmlTextReader r = new XmlTextReader(rp.GetResponseStream());
                while (r.Read())
                {
                    if (r.NodeType == XmlNodeType.Element)
                    {
                        if (bOk)
                        {
                            if (r.Name == "zipNo" || // 우편번호
                                r.Name == "lnmAdres" || // 도로명 주소
                                r.Name == "rnAdres") // 지번 주소
                            {
                                postNo.Add(r.ReadString());
                            }
                            else if (r.Name == "totalCount") // 전체 검색수
                            {
                                int.TryParse(r.ReadString(), out resultCount);
                            }
                        }
                        else
                        {
                            if (r.Name == "successYN")
                            {
                                if (r.ReadString() == "Y") bOk = true; // 검색 성공
                            }
                            else if (r.Name == "errMsg") // 에러 메시지
                            {
                                search = r.ReadString();
                                break;
                            }
                        }
                    }
                }
                r.Close();
                rp.Close();
                if (search == null)
                { 
                    if (postNo.Count < 3)
                        search = "검색결과가 없습니다.";
                }
            }
            catch (Exception ex)
            {
                search = ex.Message;
            }
            return search;
        }
    }
}
