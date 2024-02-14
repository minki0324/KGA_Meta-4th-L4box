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

        category_Dictionary.Add(10, "�ػ깰");
        category_Dictionary.Add(11, "����");
        category_Dictionary.Add(12, "����");
        category_Dictionary.Add(13, "����");
        category_Dictionary.Add(14, "����");
        category_Dictionary.Add(15, "����");
        category_Dictionary.Add(16, "��");
        category_Dictionary.Add(17, "��");
        category_Dictionary.Add(18, "ä��");
        category_Dictionary.Add(19, "��&�����");
    }

    public void IconDictionary()
    {
        icon_Dictionry = new Dictionary<int, string>();

        icon_Dictionry.Add(1001, "����");
        icon_Dictionry.Add(1002, "Ű����");
        icon_Dictionry.Add(1003, "���");
        icon_Dictionry.Add(1004, "ĥ��");
        icon_Dictionry.Add(1005, "���");
        icon_Dictionry.Add(1006, "���ָŹ����");
        icon_Dictionry.Add(1007, "��");
        icon_Dictionry.Add(1008, "�󰡸�");
        icon_Dictionry.Add(1009, "¯�վ�");
        icon_Dictionry.Add(1010, "�Ҷ��");
        icon_Dictionry.Add(1011, "���Ҹ��վ�");
        icon_Dictionry.Add(1012, "������");
        icon_Dictionry.Add(1013, "�����");
        icon_Dictionry.Add(1014, "����");
        icon_Dictionry.Add(1015, "��������������");
        icon_Dictionry.Add(1016, "����");
        icon_Dictionry.Add(1017, "�����Ӹ�������");
        icon_Dictionry.Add(1018, "������");
        icon_Dictionry.Add(1019, "�ǻ԰�");
        icon_Dictionry.Add(1020, "ū�����췷��");
        icon_Dictionry.Add(1021, "���Ұ��縮");
        //����
        icon_Dictionry.Add(1101, "�ܹ�");
        icon_Dictionry.Add(1102, "����");
        icon_Dictionry.Add(1103, "�޶ѱ�");
        icon_Dictionry.Add(1104, "ǳ����");
        icon_Dictionry.Add(1105, "������");
        icon_Dictionry.Add(1106, "��ġ");
        icon_Dictionry.Add(1107, "�Ź�");
        icon_Dictionry.Add(1108, "�������");
        icon_Dictionry.Add(1109, "�ֹ���");
        icon_Dictionry.Add(1110, "���ڸ�");
        icon_Dictionry.Add(1111, "����");
        //����
        icon_Dictionry.Add(1201, "�˷λ�츣��");
        icon_Dictionry.Add(1202, "�췹���츣��");
        icon_Dictionry.Add(1203, "��������");
        icon_Dictionry.Add(1204, "����̹���");
        icon_Dictionry.Add(1205, "Ű����");
        icon_Dictionry.Add(1207, "�����׳뵷");
        icon_Dictionry.Add(1208, "�����ұ׳�����");
        icon_Dictionry.Add(1209, "���Ű����츣��");
        icon_Dictionry.Add(1210, "���÷ε���");
        icon_Dictionry.Add(1211, "��Ű���ȷλ�츣��");
        icon_Dictionry.Add(1212, "���װ��츣��");
        icon_Dictionry.Add(1213, "Ƽ����츣��");
        icon_Dictionry.Add(1214, "���󽺸��츣��");
        icon_Dictionry.Add(1215, "�������츣��");
        icon_Dictionry.Add(1216, "�̱��Ƴ뵷");
        icon_Dictionry.Add(1217, "ũ�γ��츣��");
        icon_Dictionry.Add(1218, "�ö��׿���츣��");
        icon_Dictionry.Add(1219, "���ϳ��츣��");
        icon_Dictionry.Add(1220, "����츣��");
        icon_Dictionry.Add(1221, "����Ű���丣");
        icon_Dictionry.Add(1222, "Ʈ���ɶ��齺");
        icon_Dictionry.Add(1223, "��������츣��");
        icon_Dictionry.Add(1224, "�Ķ���ѷ�Ǫ��");
        icon_Dictionry.Add(1225, "�����ڸ��Ͻ�");
        icon_Dictionry.Add(1226, "��ų�λ�츣��");
        icon_Dictionry.Add(1227, "Ű����");
        icon_Dictionry.Add(1228, "�����������̷罺");
        //����
        icon_Dictionry.Add(1301, "���ξ���");
        icon_Dictionry.Add(1302, "����");
        icon_Dictionry.Add(1303, "�ٳ���");
        icon_Dictionry.Add(1304, "���");
        icon_Dictionry.Add(1305, "��");
        icon_Dictionry.Add(1306, "����");
        icon_Dictionry.Add(1307, "����");
        //���� 
        icon_Dictionry.Add(1401, "ġŸ");
        icon_Dictionry.Add(1402, "�糪��");
        icon_Dictionry.Add(1403, "�̾�Ĺ");
        icon_Dictionry.Add(1404, "�Ǿ�");
        icon_Dictionry.Add(1405, "�⸰");
        icon_Dictionry.Add(1406, "����");
        icon_Dictionry.Add(1407, "����");
        icon_Dictionry.Add(1408, "�����ú�");
        icon_Dictionry.Add(1409, "�Ǵ�");
        icon_Dictionry.Add(1410, "��");
        icon_Dictionry.Add(1411, "�ϸ�");
        icon_Dictionry.Add(1412, "����");
        icon_Dictionry.Add(1413, "ġŸ");
        icon_Dictionry.Add(1414, "�ڳ���");
        icon_Dictionry.Add(1415, "�罿");
        icon_Dictionry.Add(1416, "����");
        icon_Dictionry.Add(1417, "��踻");
        icon_Dictionry.Add(1418, "�ڻԼ�");
        icon_Dictionry.Add(1419, "Ļ�ŷ�");
        icon_Dictionry.Add(1420, "�ھ˶�");
        icon_Dictionry.Add(1421, "�ٶ���");
        icon_Dictionry.Add(1422, "�����ʱ���");
        icon_Dictionry.Add(1423, "ȣ����");
        icon_Dictionry.Add(1424, "����");
        icon_Dictionry.Add(1425, "��ī");
        //����
        icon_Dictionry.Add(1501, "�κ�");
        icon_Dictionry.Add(1502, "������");
        //��
        icon_Dictionry.Add(1601, "����");
        icon_Dictionry.Add(1602, "����");
        icon_Dictionry.Add(1603, "��������");
        icon_Dictionry.Add(1604, "������");
        icon_Dictionry.Add(1605, "��ġ");
        icon_Dictionry.Add(1606, "����");
        icon_Dictionry.Add(1607, "�ξ���");
        icon_Dictionry.Add(1608, "���ۻ�");
        icon_Dictionry.Add(1609, "��");
        icon_Dictionry.Add(1610, "����");
        //Ż��
        icon_Dictionry.Add(1701, "����Ʈ��");
        icon_Dictionry.Add(1702, "2������");
        icon_Dictionry.Add(1703, "������");
        icon_Dictionry.Add(1704, "�ҹ���");
        icon_Dictionry.Add(1705, "�ҵ���");
        icon_Dictionry.Add(1706, "������");
        icon_Dictionry.Add(1707, "�ù���");
        icon_Dictionry.Add(1708, "ũ����");
        icon_Dictionry.Add(1709, "�ڵ���");
        icon_Dictionry.Add(1710, "�������");
        icon_Dictionry.Add(1711, "������");
        icon_Dictionry.Add(1712, "û����");
        icon_Dictionry.Add(1713, "�ý�");
        icon_Dictionry.Add(1714, "ķ��ī");
        icon_Dictionry.Add(1715, "Ʈ��");
        icon_Dictionry.Add(1716, "����");
        icon_Dictionry.Add(1717, "������");
        icon_Dictionry.Add(1718, "����ö");
        icon_Dictionry.Add(1719, "����");
        //ä��
        icon_Dictionry.Add(1801, "ȣ��");
        icon_Dictionry.Add(1802, "����ݸ�");
        icon_Dictionry.Add(1803, "����");
        icon_Dictionry.Add(1804, "����");
        icon_Dictionry.Add(1805, "����");
        icon_Dictionry.Add(1806, "���");
        //�ذ�
        icon_Dictionry.Add(1901, "���ܹ�");
        icon_Dictionry.Add(1902, "��Ʈ");
        icon_Dictionry.Add(1903, "��Ʈ");
        icon_Dictionry.Add(1904, "������");
        icon_Dictionry.Add(1905, "�����");
        icon_Dictionry.Add(1906, "������");
        icon_Dictionry.Add(1907, "�����");
        icon_Dictionry.Add(1908, "�︮����");
        icon_Dictionry.Add(1909, "���ⱸ");

    }




}
