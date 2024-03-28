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
using UnityEngine.U2D;

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


public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    public SpriteAtlas pushPopAtlas = null;

    //��ųʸ���
    public List<CategoryDict> CategoryDictsList = new List<CategoryDict>();
    public List<Scripts> SciptsList = new List<Scripts>();
    public List<IconDict> IconDictsList = new List<IconDict>();
    //�Ľ��� ���� ������ ��ųʸ�
    public Dictionary<int, string> CategoryDict = new Dictionary<int, string>();
    public Dictionary<int, string> IconDict = new Dictionary<int, string>();


    //���� ��ũ��Ʈ��
    public List<HelpScript> HelpScriptsList = new List<HelpScript>();


    //�弳������
    public BadWord[] BadWordArray;      //vulgarsim.json ������ �Ѵ¼��� ��ǻ�Ͱ� ���缭 �ϴ� ����
    public string[] VulgarismArray;


    //���� �̸� (�� ģ������ Assets/StreamingAssets ������ �̸� ����� �־�� �մϴ�)
    private string categoryDictFileName = "category.json"; // only pushpush
    private string iconDictFileName = "icon.json"; // pushpush, speed
    // help
    private string helpScriptFileName = "helpme.json";
    // nickname
    private string badWordFileName = "badword.json";
    private string vulgarismFileName = "vulgarism.json";

    //���
    private string dataPath = string.Empty;      //persistentDataPath
    private string path = string.Empty;      //StreamingDataPath;


    #region Unity Callback

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }


        dataPath = Application.persistentDataPath + "/gameData";
        path = Application.streamingAssetsPath;

        //���� ����
        if (!File.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
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

        string oriPath = Path.Combine(path, categoryDictFileName);
        string realPath = dataPath + "/" + categoryDictFileName;

        if(!File.Exists(realPath))
        {
#pragma warning disable 0618
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
            CategoryDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString()); ;

        }




    }

    //icon.json �о�ͼ� Dictionary�� ��ȯ�ϴ� �޼ҵ�
    public void Read_Icon()
    {//icon.json���� �о�� persistentDataPath�� ���� �� json������ Dictionary�� �Ľ��ϴ� �޼ҵ�      
        string oriPath = Path.Combine(path, iconDictFileName);
        string realPath = dataPath + "/" + iconDictFileName;

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
            IconDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString());
        }

    }

    public void Save_HelpScript()
    {
        string oriPath = Path.Combine(Application.streamingAssetsPath, helpScriptFileName);

        //UnityWebRequest reader = new UnityWebRequest()

        WWW reader = new WWW(oriPath);
        while (!reader.isDone)
        {
            ;
        }

        string realPath = dataPath + "/" + helpScriptFileName;

        byte[] data = reader.bytes;
        string resultData = System.Text.Encoding.UTF8.GetString(data);

        //File.WriteAllBytes(realPath, reader.bytes);

        File.WriteAllText(realPath, resultData);
    }

    public void Read_HelpScript()
    {//helpscript.json���� �о�� persistentDataPath�� ���� �� json�����͸� �Ľ��ϴ� �޼ҵ�

        string oriPath = Path.Combine(Application.streamingAssetsPath, helpScriptFileName);
        string realPath = dataPath + "/" + helpScriptFileName;

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


        HelpScriptsList.Clear();
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

            HelpScriptsList.Add(helpScript);
        }


    }

    public void Read_BadWord()
    {//badword.json���� �о�� persistentDataPath�� ���� �� json������ �迭�� �Ľ��ϴ� �޼ҵ�

        string oriPath = Path.Combine(path, badWordFileName);
        string realPath = dataPath + "/" + badWordFileName;

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

        BadWordArray = new BadWord[jsonData.Count];

        for (int i = 0; i < jsonData.Count; i++)
        {
            BadWord badword = new BadWord();

            badword.index = i;
            badword.badword = jsonData[i]["badword"].ToString();           
            BadWordArray[i] = badword;
        }

    }


    public void Read_Vulgarism()
    {//vulgarism.json���� �о�� persistentDataPath�� ���� �� json�����͸� �迭�� �Ľ��ϴ� �޼ҵ�

        string oriPath = Path.Combine(path, vulgarismFileName);
        string realPath = dataPath + "/" + vulgarismFileName;

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
        VulgarismArray = oneData.Split(",");

        for (int i =0; i<VulgarismArray.Length; i++)
        {
            VulgarismArray[i].Replace(" ", ""); 
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