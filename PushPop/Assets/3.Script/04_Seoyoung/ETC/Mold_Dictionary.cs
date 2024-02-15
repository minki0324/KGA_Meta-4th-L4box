using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mold_Dictionary : MonoBehaviour
{
    public static Mold_Dictionary instance = null;


    public Dictionary<int, string> category_Dictionary = new Dictionary<int, string>();
    public Dictionary<int, string> icon_Dictionry = new Dictionary<int, string>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CategoryDictionary();
        IconDictionary();
    }



    public void CategoryDictionary()
    {
        category_Dictionary = new Dictionary<int, string>();

        category_Dictionary.Add(10, "해산물");
        category_Dictionary.Add(11, "곤충");
        category_Dictionary.Add(12, "공룡");
        category_Dictionary.Add(13, "과일");
        category_Dictionary.Add(14, "동물");
        category_Dictionary.Add(15, "인형");
        category_Dictionary.Add(16, "새");
        category_Dictionary.Add(17, "차");
        category_Dictionary.Add(18, "채소");
        category_Dictionary.Add(19, "배&비행기");
    }

    public void IconDictionary()
    {
        icon_Dictionry = new Dictionary<int, string>();

        icon_Dictionry.Add(1001, "문어");
        icon_Dictionry.Add(1002, "키조개");
        icon_Dictionry.Add(1003, "농게");
        icon_Dictionry.Add(1004, "칠게");
        icon_Dictionry.Add(1005, "밤게");
        icon_Dictionry.Add(1006, "명주매물고둥");
        icon_Dictionry.Add(1007, "쏙");
        icon_Dictionry.Add(1008, "댕가리");
        icon_Dictionry.Add(1009, "짱둥어");
        icon_Dictionry.Add(1010, "소라게");
        icon_Dictionry.Add(1011, "말뚝망둥어");
        icon_Dictionry.Add(1012, "바지락");
        icon_Dictionry.Add(1013, "저어새");
        icon_Dictionry.Add(1014, "동죽");
        icon_Dictionry.Add(1015, "말락꼬리마도요");
        icon_Dictionry.Add(1016, "꼬막");
        icon_Dictionry.Add(1017, "검은머리물떼새");
        icon_Dictionry.Add(1018, "가리비");
        icon_Dictionry.Add(1019, "피뿔고동");
        icon_Dictionry.Add(1020, "큰구슬우렁이");
        icon_Dictionry.Add(1021, "별불가사리");
        //곤충
        icon_Dictionry.Add(1101, "꿀벌");
        icon_Dictionry.Add(1102, "나비");
        icon_Dictionry.Add(1103, "메뚜기");
        icon_Dictionry.Add(1104, "풍뎅이");
        icon_Dictionry.Add(1105, "달팽이");
        icon_Dictionry.Add(1106, "여치");
        icon_Dictionry.Add(1107, "매미");
        icon_Dictionry.Add(1108, "무당벌레");
        icon_Dictionry.Add(1109, "애벌레");
        icon_Dictionry.Add(1110, "잠자리");
        icon_Dictionry.Add(1111, "개미");
        //공룡
        icon_Dictionry.Add(1201, "알로사우르스");
        icon_Dictionry.Add(1202, "헤레라사우르스");
        icon_Dictionry.Add(1203, "람포링쿠스");
        icon_Dictionry.Add(1204, "갈라미무스");
        icon_Dictionry.Add(1205, "키조개");
        icon_Dictionry.Add(1207, "프라테노돈");
        icon_Dictionry.Add(1208, "콤프소그나투스");
        icon_Dictionry.Add(1209, "브라키오사우르스");
        icon_Dictionry.Add(1210, "디플로도쿠스");
        icon_Dictionry.Add(1211, "파키케팔로사우르스");
        icon_Dictionry.Add(1212, "스테고사우르스");
        icon_Dictionry.Add(1213, "티라노사우르스");
        icon_Dictionry.Add(1214, "엘라스모사우르스");
        icon_Dictionry.Add(1215, "아파토사우르스");
        icon_Dictionry.Add(1216, "이구아노돈");
        icon_Dictionry.Add(1217, "크로노사우르스");
        icon_Dictionry.Add(1218, "플라테오사우르스");
        icon_Dictionry.Add(1219, "스니노사우르스");
        icon_Dictionry.Add(1220, "모사사우르스");
        icon_Dictionry.Add(1221, "벨라키랍토르");
        icon_Dictionry.Add(1222, "트리케라톱스");
        icon_Dictionry.Add(1223, "딜로포사우르스");
        icon_Dictionry.Add(1224, "파라사우롤로푸스");
        icon_Dictionry.Add(1225, "돌리코린촙스");
        icon_Dictionry.Add(1226, "안킬로사우르스");
        icon_Dictionry.Add(1227, "키조개");
        icon_Dictionry.Add(1228, "오르니토케이루스");
        //과일
        icon_Dictionry.Add(1301, "파인애플");
        icon_Dictionry.Add(1302, "포도");
        icon_Dictionry.Add(1303, "바나나");
        icon_Dictionry.Add(1304, "사과");
        icon_Dictionry.Add(1305, "배");
        icon_Dictionry.Add(1306, "수박");
        icon_Dictionry.Add(1307, "레몬");
        //동물 
        icon_Dictionry.Add(1401, "치타");
        icon_Dictionry.Add(1402, "당나귀");
        icon_Dictionry.Add(1403, "미어캣");
        icon_Dictionry.Add(1404, "악어");
        icon_Dictionry.Add(1405, "기린");
        icon_Dictionry.Add(1406, "돼지");
        icon_Dictionry.Add(1407, "젖소");
        icon_Dictionry.Add(1408, "나무늘보");
        icon_Dictionry.Add(1409, "판다");
        icon_Dictionry.Add(1410, "곰");
        icon_Dictionry.Add(1411, "하마");
        icon_Dictionry.Add(1412, "사자");
        icon_Dictionry.Add(1413, "치타");
        icon_Dictionry.Add(1414, "코끼리");
        icon_Dictionry.Add(1415, "사슴");
        icon_Dictionry.Add(1416, "여우");
        icon_Dictionry.Add(1417, "얼룩말");
        icon_Dictionry.Add(1418, "코뿔소");
        icon_Dictionry.Add(1419, "캥거루");
        icon_Dictionry.Add(1420, "코알라");
        icon_Dictionry.Add(1421, "다람쥐");
        icon_Dictionry.Add(1422, "오리너구리");
        icon_Dictionry.Add(1423, "호랑이");
        icon_Dictionry.Add(1424, "염소");
        icon_Dictionry.Add(1425, "쿼카");
        //인형
        icon_Dictionry.Add(1501, "로봇");
        icon_Dictionry.Add(1502, "곰인형");
        //새
        icon_Dictionry.Add(1601, "오리");
        icon_Dictionry.Add(1602, "참새");
        icon_Dictionry.Add(1603, "딱따구리");
        icon_Dictionry.Add(1604, "독수리");
        icon_Dictionry.Add(1605, "까치");
        icon_Dictionry.Add(1606, "제비");
        icon_Dictionry.Add(1607, "부엉이");
        icon_Dictionry.Add(1608, "공작새");
        icon_Dictionry.Add(1609, "닭");
        icon_Dictionry.Add(1610, "오리");
        //탈것
        icon_Dictionry.Add(1701, "덤프트럭");
        icon_Dictionry.Add(1702, "2층버스");
        icon_Dictionry.Add(1703, "구급차");
        icon_Dictionry.Add(1704, "소방차");
        icon_Dictionry.Add(1705, "불도저");
        icon_Dictionry.Add(1706, "굴착기");
        icon_Dictionry.Add(1707, "택배차");
        icon_Dictionry.Add(1708, "크레인");
        icon_Dictionry.Add(1709, "자동차");
        icon_Dictionry.Add(1710, "오토바이");
        icon_Dictionry.Add(1711, "레미콘");
        icon_Dictionry.Add(1712, "청소차");
        icon_Dictionry.Add(1713, "택시");
        icon_Dictionry.Add(1714, "캠핑카");
        icon_Dictionry.Add(1715, "트럭");
        icon_Dictionry.Add(1716, "버스");
        icon_Dictionry.Add(1717, "경찰차");
        icon_Dictionry.Add(1718, "지하철");
        icon_Dictionry.Add(1719, "기차");
        //채소
        icon_Dictionry.Add(1801, "호박");
        icon_Dictionry.Add(1802, "브로콜리");
        icon_Dictionry.Add(1803, "고구마");
        icon_Dictionry.Add(1804, "감자");
        icon_Dictionry.Add(1805, "가지");
        icon_Dictionry.Add(1806, "당근");
        //해공
        icon_Dictionry.Add(1901, "돛단배");
        icon_Dictionry.Add(1902, "보트");
        icon_Dictionry.Add(1903, "요트");
        icon_Dictionry.Add(1904, "여객선");
        icon_Dictionry.Add(1905, "잠수함");
        icon_Dictionry.Add(1906, "유람선");
        icon_Dictionry.Add(1907, "비행기");
        icon_Dictionry.Add(1908, "헬리콥터");
        icon_Dictionry.Add(1909, "열기구");

    }




}
