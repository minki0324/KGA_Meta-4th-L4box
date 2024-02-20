using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;	// ���ڵ�
using LitJson;

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
    public string helpScript_fileName = "asdf.json";
    //public string helpScript_fileName = "help.json";      //�ѱ��� �����Ƿ� ���ڵ� �ڵ� �ְ� ����..�ϴ� asdf ��

    private string path = string.Empty;

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
    }

    void Start()
    {
        path = Application.streamingAssetsPath;
        Read_HelpScript();

        //Save_Category();
        //Save_Icon();

       

        //Read_Category();
        //Read_Icon();
    }

    #endregion

    #region Other Method

    //category.json �о�ͼ� Dictionary�� ��ȯ�ϴ� �޼ҵ�
    public void Read_Category()
    {
        //1. JSON �����͸� Category�� List�� �޾ƿ�
        //2. List�� Dictinory�� ��ȯ
        //categoryDict.Clear();

        string JsonString = File.ReadAllText(path + "/" + categoryDict_fileName);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++)
        {
            categoryDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString()); ;
            Debug.Log(jsonData[i]["number"] + "," + jsonData[i]["name"]);
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
            Debug.Log(ca.number + "," + ca.name);
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
        File.WriteAllText(path + "/" + categoryDict_fileName, jsonData.ToString());
    }

    //icon.json �о�ͼ� Dictionary�� ��ȯ�ϴ� �޼ҵ�
    public void Read_Icon()
    {
        //iconDict.Clear();

        string JsonString = File.ReadAllText(path + "/" + iconDict_fileName);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++)
        {
            iconDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString());
            Debug.Log(jsonData[i]["number"] + "," + jsonData[i]["name"]);
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
            Debug.Log(ic.number + "," + ic.name);
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
        File.WriteAllText(path + "/" + iconDict_fileName, jsonData.ToString());
    }


    public void Read_HelpScript()
    {
        //���� ��ũ��Ʈ �о���� �Լ�

        helpScripts_List.Clear();

        string JsonString = File.ReadAllText(path + "/" + helpScript_fileName);
        JsonData jsonData = JsonMapper.ToObject(JsonString);
       // byte[] encoding = Encoding.UTF8.GetBytes(File.ReadAllText(JsonString));
        

       


        for (int i = 0; i < jsonData.Count; i++)
        {
            HelpScript helpScript = new HelpScript();

            helpScript.type = (string)jsonData[i]["type"];

            //Debug.Log(jsonData[i]["script"]); //Json Data Array�� ���� -> ��Ǯ��..

       

            for (int j = 0; j < jsonData[i]["script"].Count; j++)
            {
                JsonData  item = JsonMapper.ToObject<JsonData>(jsonData[i]["script"].ToJson());
                Scripts script = new Scripts();
                script.pageNum = int.Parse(item[j]["page"].ToString());
                script.content = item[j]["content"].ToString();


                helpScript.script.Add(script);
                //Debug.Log(script.pageNum + "," + script.content);
                
            }

             helpScripts_List.Add(helpScript);

        }

    }


    #endregion
}