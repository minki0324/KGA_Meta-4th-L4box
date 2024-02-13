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
    }

    private void Start()
    {
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
        icon_Dictionry.Add(1003, "키조개");
        icon_Dictionry.Add(1004, "키조개");
        icon_Dictionry.Add(1005, "키조개");
        icon_Dictionry.Add(1006, "키조개");
        icon_Dictionry.Add(1007, "키조개");
        icon_Dictionry.Add(1008, "키조개");
        icon_Dictionry.Add(1009, "키조개");
        icon_Dictionry.Add(1010, "키조개");
        icon_Dictionry.Add(1012, "키조개");
        icon_Dictionry.Add(1013, "키조개");
        icon_Dictionry.Add(1014, "키조개");
        icon_Dictionry.Add(1015, "키조개");
        icon_Dictionry.Add(1016, "키조개");
        icon_Dictionry.Add(1017, "키조개");
        icon_Dictionry.Add(1018, "키조개");
        icon_Dictionry.Add(1019, "키조개");
        icon_Dictionry.Add(1020, "키조개");
        icon_Dictionry.Add(1021, "키조개");
        //곤충
        icon_Dictionry.Add(1101, "키조개");
        icon_Dictionry.Add(1102, "키조개");
        icon_Dictionry.Add(1103, "키조개");
        icon_Dictionry.Add(1104, "키조개");
        icon_Dictionry.Add(1105, "키조개");
        icon_Dictionry.Add(1106, "키조개");
        icon_Dictionry.Add(1107, "키조개");
        icon_Dictionry.Add(1108, "키조개");
        icon_Dictionry.Add(1109, "키조개");
        icon_Dictionry.Add(1110, "키조개");
        icon_Dictionry.Add(1111, "키조개");
        //공룡
        icon_Dictionry.Add(1201, "키조개");
        icon_Dictionry.Add(1202, "키조개");
        icon_Dictionry.Add(1203, "키조개");
        icon_Dictionry.Add(1204, "키조개");
        icon_Dictionry.Add(1205, "키조개");
        icon_Dictionry.Add(1207, "키조개");
        icon_Dictionry.Add(1208, "키조개");
        icon_Dictionry.Add(1209, "키조개");
        icon_Dictionry.Add(1210, "키조개");
        icon_Dictionry.Add(1211, "키조개");
        icon_Dictionry.Add(1212, "키조개");
        icon_Dictionry.Add(1213, "키조개");
        icon_Dictionry.Add(1214, "키조개");
        icon_Dictionry.Add(1215, "키조개");
        icon_Dictionry.Add(1216, "키조개");
        icon_Dictionry.Add(1217, "키조개");
        icon_Dictionry.Add(1218, "키조개");
        icon_Dictionry.Add(1219, "키조개");
        icon_Dictionry.Add(1220, "키조개");
        icon_Dictionry.Add(1221, "키조개");
        icon_Dictionry.Add(1222, "키조개");
        icon_Dictionry.Add(1223, "키조개");
        icon_Dictionry.Add(1224, "키조개");
        icon_Dictionry.Add(1225, "키조개");
        icon_Dictionry.Add(1226, "키조개");
        icon_Dictionry.Add(1227, "키조개");
        icon_Dictionry.Add(1228, "키조개");
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
