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

//ī�װ���
[System.Serializable]
public class CategoryDict
{
    public int number;
    public string name;
}

//���� �����ܿ�
[System.Serializable]
public class IconDict
{
    public int number;
    public string name;
}

//���򸻿�

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


//�弳���� ��
[System.Serializable]
public class BadWord
{
    public int index;
    public string badword;
}

#endregion


public class DataManager2 : MonoBehaviour
{
    public static DataManager2 instance = null;


    //��ųʸ���
    public List<CategoryDict> categoryDicts_List = new List<CategoryDict>();
    public List<Scripts> scipts_List = new List<Scripts>();
    public List<IconDict> iconDicts_List = new List<IconDict>();
    //�Ľ��� ���� ������ ��ųʸ�
    public Dictionary<int, string> categoryDict = new Dictionary<int, string>();
    public Dictionary<int, string> iconDict = new Dictionary<int, string>();


    //���� ��ũ��Ʈ��
    public List<HelpScript> helpScripts_List = new List<HelpScript>();


    //�弳������
    public BadWord[] badWord_Arr;      //vulgarsim.json ������ �Ѵ¼��� ��ǻ�Ͱ� ���缭 �ϴ� ����
    public string[] vulgarism_Arr;


    //���� �̸� (�� ģ������ Assets/StreamingAssets ������ �̸� ����� �־�� �մϴ�)
    public string categoryDict_fileName = "category.json";
    public string iconDict_fileName = "icon.json";
    public string helpScript_fileName = "help.json";
    public string badWord_fileName = "badword.json";
    public string vulgarism_fileName = "vulgarism.json";

    //���
    public string Datapath = string.Empty;      //persistentDataPath
    public string path = string.Empty;      //StreamingDataPath;


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
        path = Application.streamingAssetsPath;

        //���� ����
        if (!File.Exists(Datapath))
        {
            Directory.CreateDirectory(Datapath);
        }

        Read_HelpScript();  //���� �б�
        Read_Category();    //ī�װ� �̸� ���� -> ��ųʸ�(����,�ѱ�)
        Read_Icon();    //���� ������ �̸� ���� -> ��ųʸ�(����, �ѱ�)
        Read_BadWord();     //��Ӿ� 1
        Read_Vulgarism();   //��Ӿ� 2

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
    {//category.json���� �о�� persistentDataPath�� ���� �� json������ Dictionary�� �Ľ��ϴ� �޼ҵ�   
        //1. JSON �����͸� Category�� List�� �޾ƿ�
        //2. List�� Dictinory�� ��ȯ
        //categoryDict.Clear();

        string oriPath = Path.Combine(path, categoryDict_fileName);
        string realPath = Datapath + "/" + categoryDict_fileName;

        if(!File.Exists(realPath))
        {

            WWW reader = new WWW(oriPath);
            while (!reader.isDone)
            {

            }

            byte[] data = reader.bytes;
            string resultData = System.Text.Encoding.UTF8.GetString(data);

            //File.WriteAllBytes(realPath, reader.bytes);
            File.WriteAllText(realPath, resultData);
        }


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
        File.WriteAllText(path + "/" + categoryDict_fileName, Decode_EncodedNonASCIICharacters(jsonData.ToString()));
    }

    //icon.json �о�ͼ� Dictionary�� ��ȯ�ϴ� �޼ҵ�
    public void Read_Icon()
    {//icon.json���� �о�� persistentDataPath�� ���� �� json������ Dictionary�� �Ľ��ϴ� �޼ҵ�      
        string oriPath = Path.Combine(path, iconDict_fileName);
        string realPath = Datapath + "/" + iconDict_fileName;

        if(!File.Exists(realPath))
        {
            WWW reader = new WWW(oriPath);
            while (!reader.isDone)
            {

            }

            byte[] data = reader.bytes;
            string resultData = System.Text.Encoding.UTF8.GetString(data);

            File.WriteAllText(realPath, resultData);

        }

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
    {//helpscript.json���� �о�� persistentDataPath�� ���� �� json�����͸� �Ľ��ϴ� �޼ҵ�

        string oriPath = Path.Combine(Application.streamingAssetsPath, helpScript_fileName);
        string realPath = Datapath + "/" + helpScript_fileName;

        if(!File.Exists(realPath))
        {
            WWW reader = new WWW(oriPath);
            while (!reader.isDone)
            {
            
            }

            byte[] data = reader.bytes;
            string resultData = System.Text.Encoding.UTF8.GetString(data);

            //File.WriteAllBytes(realPath, reader.bytes);

            File.WriteAllText(realPath, resultData);
        }


        helpScripts_List.Clear();
        string JsonString = File.ReadAllText(realPath);
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
            }

            helpScripts_List.Add(helpScript);
        }


    }

    public void Read_BadWord()
    {//badword.json���� �о�� persistentDataPath�� ���� �� json������ �迭�� �Ľ��ϴ� �޼ҵ�

        string oriPath = Path.Combine(path, badWord_fileName);
        string realPath = Datapath + "/" + badWord_fileName;

        if (!File.Exists(realPath))
        {

            WWW reader = new WWW(oriPath);
            while (!reader.isDone)
            {

            }

            byte[] data = reader.bytes;
            string resultData = System.Text.Encoding.UTF8.GetString(data);

            File.WriteAllText(realPath, resultData);
        }
 

        string JsonString = File.ReadAllText(realPath);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        badWord_Arr = new BadWord[jsonData.Count];

        for (int i = 0; i < jsonData.Count; i++)
        {
            BadWord badword = new BadWord();

            badword.index = i;
            badword.badword = jsonData[i]["badword"].ToString();           
            badWord_Arr[i] = badword;
        }

    }


    public void Read_Vulgarism()
    {//vulgarism.json���� �о�� persistentDataPath�� ���� �� json�����͸� �迭�� �Ľ��ϴ� �޼ҵ�

        string oriPath = Path.Combine(path, vulgarism_fileName);
        string realPath = Datapath + "/" + vulgarism_fileName;

        if(!File.Exists(realPath))
        {

            WWW reader = new WWW(oriPath);
            while (!reader.isDone)
            {

            }
            byte[] data = reader.bytes;
            string resultData = System.Text.Encoding.UTF8.GetString(data);
            File.WriteAllText(realPath, resultData);

        }

        string JsonString = File.ReadAllText(realPath);
        JsonData jsonData = JsonMapper.ToObject(JsonString);

        string oneData = jsonData[0]["vulgarism"].ToString();
        vulgarism_Arr = oneData.Split(",");

        for (int i =0; i<vulgarism_Arr.Length; i++)
        {
            vulgarism_Arr[i].Replace(" ", ""); 
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