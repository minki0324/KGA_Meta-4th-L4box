using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;   // 인코딩
using System.Text.RegularExpressions;
using System.Globalization;
using LitJson;
using UnityEngine.Networking;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

#region ObjectClass

//카테고리용
[System.Serializable]
public class CategoryDict
{
    public int number;
    public string name;
}

//몰드 아이콘용
[System.Serializable]
public class IconDict
{
    public int number;
    public string name;
}

//도움말용

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


//욕설방지 용
[System.Serializable]
public class BadWord
{
    public int index;
    public string badword;
}


[System.Serializable]
public class Vulgarism
{
    public int index;
    public string vulgarism;
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



    //욕설방지용
    public BadWord[] badWord_Arr;
    public string[] vulgarism_Arr;



    //파일 이름
    public string categoryDict_fileName = "category.json";
    public string iconDict_fileName = "icon.json";
    public string helpScript_fileName = "help.json";
    public string badWord_fileName = "badword.json";
    public string vulgarism_fileName = "vulgarism.json";

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
        if (!File.Exists(Datapath)) // 경로 탐색
        {
            Directory.CreateDirectory(Datapath);
        }

        Save_HelpScript();
        Read_HelpScript();

        Read_Category();
        Read_Icon();
        Read_BadWord();
        Read_Vulgarism();

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


    //category.json 읽어와서 Dictionary로 변환하는 메소드
    public void Read_Category()
    {
        //1. JSON 데이터를 Category형 List로 받아옴
        //2. List를 Dictinory로 변환
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

        //byte[] b = System.Text.UTF8Encoding.UTF8.GetBytes(jsonData.ToString());

        //File.WriteAllBytes(path + "/" + categoryDict_fileName, b);
        File.WriteAllText(path + "/" + categoryDict_fileName, Decode_EncodedNonASCIICharacters(jsonData.ToString()));
    }

    //icon.json 읽어와서 Dictionary로 변환하는 메소드
    public void Read_Icon()
    {
        //iconDict.Clear();

        #region 이부분을 처음 깔때만 실행하면 참 좋을텐데
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

        #endregion

        string JsonString = File.ReadAllText(realPath);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++)
        {
            iconDict.Add(int.Parse(jsonData[i]["number"].ToString()), jsonData[i]["name"].ToString());

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
        //도움말 스크립트 읽어오는 함수


        if (File.Exists(Datapath + "/help.json"))
        {
            Debug.Log("Read_HelpScript 파일 존재");
            helpScripts_List.Clear();
            string JsonString = File.ReadAllText(Datapath + "/help.json");
            var jsonData = JsonMapper.ToObject(JsonString);

            for (int i = 0; i < jsonData.Count; i++)
            {
                HelpScript helpScript = new HelpScript();

                helpScript.type = (string)jsonData[i]["type"];

                //Debug.Log(jsonData[i]["script"]); //Json Data Array로 나옴 -> 안풀림..

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

    public void Read_BadWord()
    {
        string oriPath = Path.Combine(path, badWord_fileName);

        WWW reader = new WWW(oriPath);
        while (!reader.isDone)
        {

        }

        string realPath = Datapath + "/" + badWord_fileName;

        byte[] data = reader.bytes;
        string resultData = System.Text.Encoding.UTF8.GetString(data);


        File.WriteAllText(realPath, resultData);


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
    {
        string oriPath = Path.Combine(path, vulgarism_fileName);

        WWW reader = new WWW(oriPath);
        while (!reader.isDone)
        {

        }

        string realPath = Datapath + "/" + vulgarism_fileName;

        byte[] data = reader.bytes;
        string resultData = System.Text.Encoding.UTF8.GetString(data);

        File.WriteAllText(realPath, resultData);

        string JsonString = File.ReadAllText(realPath);

        JsonData jsonData = JsonMapper.ToObject(JsonString);

        //vulgarisms_List


        string oneData = jsonData[0]["vulgarism"].ToString();

        vulgarism_Arr = oneData.Split(",");

    }



    //유니코드 -> 한글 변환 메소드
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