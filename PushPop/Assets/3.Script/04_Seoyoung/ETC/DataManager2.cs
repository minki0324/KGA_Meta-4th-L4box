using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;	// 인코딩
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


    //딕셔너리용
    public List<CategoryDict> categoryDicts_List = new List<CategoryDict>();
    public List<Scripts> scipts_List = new List<Scripts>();
    public List<IconDict> iconDicts_List = new List<IconDict>();


    //일단 임시로 쓰는 딕셔너리..나중에 Mold_Dictionary의 딕셔너리로 변경
    public Dictionary<int, string> categoryDict = new Dictionary<int, string>();
    public Dictionary<int, string> iconDict = new Dictionary<int, string>();

    //도움말 스크립트용
    public List<HelpScript> helpScripts_List = new List<HelpScript>();

    public string categoryDict_fileName = "category.json";
    public string iconDict_fileName = "icon.json";
    public string helpScript_fileName = "asdf.json";
    //public string helpScript_fileName = "help.json";      //한글이 꺠지므로 인코딩 코드 넣고 쓰기..일단 asdf 씀

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

    //category.json 읽어와서 Dictionary로 변환하는 메소드
    public void Read_Category()
    {
        //1. JSON 데이터를 Category형 List로 받아옴
        //2. List를 Dictinory로 변환
        //categoryDict.Clear();

        string JsonString = File.ReadAllText(path + "/" + categoryDict_fileName);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++)
        {
            categoryDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString()); ;
            Debug.Log(jsonData[i]["number"] + "," + jsonData[i]["name"]);
        }




    }

    //Dictionary의 값을 category.json에 저장하는 메소드
    public void Save_Category()
    {
        //1. Dictinory의 num(스프라이트명)/name(한글이름)을 받아서 리스트를 만듬
        //2. 리스트를 JSON 데이터로 변경하여 저장

        categoryDicts_List.Clear();

        foreach (KeyValuePair<int, string> item in Mold_Dictionary.instance.category_Dictionary)
        {
            CategoryDict ca = new CategoryDict();
            ca.number = item.Key;
            ca.name = item.Value;
            categoryDicts_List.Add(ca);
            Debug.Log(ca.number + "," + ca.name);
        }

        #region 파일/폴더 체크
        if (path.Equals(string.Empty))
        {
            path = Application.persistentDataPath;
        }

        if (!File.Exists(path))
        {
            //폴더검사
            //만약 License폴더가 없다면 만들기
            Directory.CreateDirectory(path);
        }

        #endregion

        JsonData jsonData = JsonMapper.ToJson(categoryDicts_List);
        File.WriteAllText(path + "/" + categoryDict_fileName, jsonData.ToString());
    }

    //icon.json 읽어와서 Dictionary로 변환하는 메소드
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

    //Dictionary의 값을 category.json에 저장하는 메소드
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

        #region 파일/폴더 체크
        if (path.Equals(string.Empty))
        {
            path = Application.persistentDataPath;
        }

        if (!File.Exists(path))
        {
            //폴더검사
            //만약 License폴더가 없다면 만들기
            Directory.CreateDirectory(path);
        }

        #endregion

        JsonData jsonData = JsonMapper.ToJson(iconDicts_List);
        File.WriteAllText(path + "/" + iconDict_fileName, jsonData.ToString());
    }


    public void Read_HelpScript()
    {
        //도움말 스크립트 읽어오는 함수

        helpScripts_List.Clear();

        string JsonString = File.ReadAllText(path + "/" + helpScript_fileName);
        JsonData jsonData = JsonMapper.ToObject(JsonString);
       // byte[] encoding = Encoding.UTF8.GetBytes(File.ReadAllText(JsonString));
        

       


        for (int i = 0; i < jsonData.Count; i++)
        {
            HelpScript helpScript = new HelpScript();

            helpScript.type = (string)jsonData[i]["type"];

            //Debug.Log(jsonData[i]["script"]); //Json Data Array로 나옴 -> 안풀림..

       

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