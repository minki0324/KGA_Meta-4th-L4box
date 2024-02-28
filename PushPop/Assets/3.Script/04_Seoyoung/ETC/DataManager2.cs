using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;   // ���ڵ�
using System.Text.RegularExpressions;
using System.Globalization;
using LitJson;
using UnityEngine.Networking;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

#region ObjectClass
[System.Serializable]
public class CategoryDict
{
    public int number;
    public string name;
}

[System.Serializable]
public class IconDict
{
    public int number;
    public string name;
}


[System.Serializable]
public class Scripts
{
    public int pageNum;
    public string content;
}


[System.Serializable]
public class HelpScript
{
    public string type;
    public List<Scripts> script = new List<Scripts>();
}
#endregion


public class DataManager2 : MonoBehaviour
{
    public static DataManager2 instance = null;


    //��ųʸ���
    public List<CategoryDict> categoryDicts_List = new List<CategoryDict>();
    public List<Scripts> scipts_List = new List<Scripts>();
    public List<IconDict> iconDicts_List = new List<IconDict>();


    //�ϴ� �ӽ÷� ���� ��ųʸ�..���߿� Mold_Dictionary�� ��ųʸ��� ����
    public Dictionary<int, string> categoryDict = new Dictionary<int, string>();
    public Dictionary<int, string> iconDict = new Dictionary<int, string>();

    //���� ��ũ��Ʈ��
    public List<HelpScript> helpScripts_List = new List<HelpScript>();

    public string categoryDict_fileName = "category.json";
    public string iconDict_fileName = "icon.json";
    public string helpScript_fileName = "help.json";
    public string Datapath = string.Empty;

    private string path = Application.streamingAssetsPath;


    #region Unity Callback

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }



        Datapath = Application.persistentDataPath + "/gameData";
        if (!File.Exists(Datapath)) // ��� Ž��
        {
            Directory.CreateDirectory(Datapath);
        }

        Save_HelpScript();
        Read_HelpScript();

        Read_Category();
        Read_Icon();


    }

    private void Start()
    {

    }
    #endregion


    #region Other Method

    public void Encoding_file(string dataPath)
    {
        string data = File.ReadAllText(dataPath);
        byte[] encodeing = Encoding.UTF8.GetBytes(data);
        FileStream file = File.Open(dataPath, FileMode.Open);
        file.Read(encodeing, 0, encodeing.Length);
        file.Close();
    }


    //category.json �о�ͼ� Dictionary�� ��ȯ�ϴ� �޼ҵ�
    public void Read_Category()
    {
        //1. JSON �����͸� Category�� List�� �޾ƿ�
        //2. List�� Dictinory�� ��ȯ
        //categoryDict.Clear();

        string oriPath = Path.Combine(path, categoryDict_fileName);

        WWW reader = new WWW(oriPath);
        while (!reader.isDone)
        {

        }

        string realPath = Datapath + "/category.json";


        byte[] data = reader.bytes;
        string resultData = System.Text.Encoding.UTF8.GetString(data);

        //File.WriteAllBytes(realPath, reader.bytes);
        File.WriteAllText(realPath, resultData);

        string JsonString = File.ReadAllText(realPath);
        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++)
        {
            categoryDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString()); ;

        }




    }

    //Dictionary�� ���� category.json�� �����ϴ� �޼ҵ�
    public void Save_Category()
    {
        //1. Dictinory�� num(��������Ʈ��)/name(�ѱ��̸�)�� �޾Ƽ� ����Ʈ�� ����
        //2. ����Ʈ�� JSON �����ͷ� �����Ͽ� ����

        categoryDicts_List.Clear();

        foreach (KeyValuePair<int, string> item in Mold_Dictionary.instance.category_Dictionary)
        {
            CategoryDict ca = new CategoryDict();
            ca.number = item.Key;
            ca.name = item.Value;
            categoryDicts_List.Add(ca);

        }

        #region ����/���� üũ
        if (path.Equals(string.Empty))
        {
            path = Application.persistentDataPath;
        }

        if (!File.Exists(path))
        {
            //�����˻�
            //���� License������ ���ٸ� �����
            Directory.CreateDirectory(path);
        }

        #endregion




        JsonData jsonData = JsonMapper.ToJson(categoryDicts_List);

        //byte[] b = System.Text.UTF8Encoding.UTF8.GetBytes(jsonData.ToString());

        //File.WriteAllBytes(path + "/" + categoryDict_fileName, b);
        File.WriteAllText(path + "/" + categoryDict_fileName, Decode_EncodedNonASCIICharacters(jsonData.ToString()));
    }

    //icon.json �о�ͼ� Dictionary�� ��ȯ�ϴ� �޼ҵ�
    public void Read_Icon()
    {
        //iconDict.Clear();

        string oriPath = Path.Combine(path, iconDict_fileName);

        WWW reader = new WWW(oriPath);
        while (!reader.isDone)
        {

        }

        string realPath = Datapath + "/" + iconDict_fileName;

        byte[] data = reader.bytes;
        string resultData = System.Text.Encoding.UTF8.GetString(data);


        //File.WriteAllBytes(realPath, reader.bytes);
        File.WriteAllText(realPath, resultData);


        string JsonString = File.ReadAllText(realPath);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++)
        {
            iconDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString());

        }

    }

    //Dictionary�� ���� category.json�� �����ϴ� �޼ҵ�
    public void Save_Icon()
    {

        iconDicts_List.Clear();

        foreach (KeyValuePair<int, string> item in Mold_Dictionary.instance.icon_Dictionry)
        {
            IconDict ic = new IconDict();
            ic.number = item.Key;
            ic.name = item.Value;
            iconDicts_List.Add(ic);

        }

        #region ����/���� üũ
        if (path.Equals(string.Empty))
        {
            path = Application.persistentDataPath;
        }

        if (!File.Exists(path))
        {
            //�����˻�
            //���� License������ ���ٸ� �����
            Directory.CreateDirectory(path);
        }

        #endregion

        JsonData jsonData = JsonMapper.ToJson(iconDicts_List);
        File.WriteAllText(path + "/" + iconDict_fileName, Decode_EncodedNonASCIICharacters(jsonData.ToString()));
    }



    public void Save_HelpScript()
    {
        string oriPath = Path.Combine(Application.streamingAssetsPath, helpScript_fileName);

        //UnityWebRequest reader = new UnityWebRequest()

        WWW reader = new WWW(oriPath);
        while (!reader.isDone)
        {
            ;
        }

        string realPath = Datapath + "/help.json";

        byte[] data = reader.bytes;
        string resultData = System.Text.Encoding.UTF8.GetString(data);

        //File.WriteAllBytes(realPath, reader.bytes);

        File.WriteAllText(realPath, resultData);
    }

    public void Read_HelpScript()
    {
        //���� ��ũ��Ʈ �о���� �Լ�


        if (File.Exists(Datapath + "/help.json"))
        {
            Debug.Log("Read_HelpScript ���� ����");
            helpScripts_List.Clear();
            string JsonString = File.ReadAllText(Datapath + "/help.json");
            var jsonData = JsonMapper.ToObject(JsonString);

            for (int i = 0; i < jsonData.Count; i++)
            {
                HelpScript helpScript = new HelpScript();

                helpScript.type = (string)jsonData[i]["type"];

                //Debug.Log(jsonData[i]["script"]); //Json Data Array�� ���� -> ��Ǯ��..

                for (int j = 0; j < jsonData[i]["script"].Count; j++)
                {
                    JsonData item = JsonMapper.ToObject<JsonData>(jsonData[i]["script"].ToJson());
                    Scripts script = new Scripts();
                    script.pageNum = int.Parse(item[j]["page"].ToString());
                    script.content = item[j]["content"].ToString();

                    helpScript.script.Add(script);
                    //Debug.Log(script.pageNum + "," + script.content);
                }

                helpScripts_List.Add(helpScript);
            }
        }



    }

    //�����ڵ� -> �ѱ� ��ȯ �޼ҵ�
    public string Decode_EncodedNonASCIICharacters(string value)
    {
        return Regex.Replace(
               value,
               @"\\u(?<Value>[a-zA-Z0-9]{4})",
               m =>
               {
                   return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
               });
    }

    #endregion
}