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
using UnityEngine.U2D;

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

#endregion


public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    public SpriteAtlas pushPopAtlas = null;

    //딕셔너리용
    public List<CategoryDict> CategoryDictsList = new List<CategoryDict>();
    public List<Scripts> SciptsList = new List<Scripts>();
    public List<IconDict> IconDictsList = new List<IconDict>();
    //파싱한 정보 저장할 딕셔너리
    public Dictionary<int, string> CategoryDict = new Dictionary<int, string>();
    public Dictionary<int, string> IconDict = new Dictionary<int, string>();


    //도움말 스크립트용
    public List<HelpScript> HelpScriptsList = new List<HelpScript>();


    //욕설방지용
    public BadWord[] BadWordArray;      //vulgarsim.json 파일이 켜는순간 컴퓨터가 멈춰서 일단 따로
    public string[] VulgarismArray;


    //파일 이름 (이 친구들은 Assets/StreamingAssets 폴더에 미리 담겨져 있어야 합니다)
    private string categoryDictFileName = "category.json"; // only pushpush
    private string iconDictFileName = "icon.json"; // pushpush, speed
    // help
    private string helpScriptFileName = "helpme.json";
    // nickname
    private string badWordFileName = "badword.json";
    private string vulgarismFileName = "vulgarism.json";

    //경로
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

        //폴더 생성
        if (!File.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        Read_HelpScript();  //도움말 읽기
        Read_Category();    //카테고리 이름 정보 -> 딕셔너리(숫자,한글)
        Read_Icon();    //몰드 아이콘 이름 정보 -> 딕셔너리(숫자, 한글)
        Read_BadWord();     //비속어 1
        Read_Vulgarism();   //비속어 2
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
    {//category.json파일 읽어와 persistentDataPath에 저장 밑 json데이터 Dictionary로 파싱하는 메소드   
        //1. JSON 데이터를 Category형 List로 받아옴
        //2. List를 Dictinory로 변환
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

    //icon.json 읽어와서 Dictionary로 변환하는 메소드
    public void Read_Icon()
    {//icon.json파일 읽어와 persistentDataPath에 저장 밑 json데이터 Dictionary로 파싱하는 메소드      
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
    {//helpscript.json파일 읽어와 persistentDataPath에 저장 밑 json데이터를 파싱하는 메소드

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

            //Debug.Log(jsonData[i]["script"]); //Json Data Array로 나옴 -> 안풀림..

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
    {//badword.json파일 읽어와 persistentDataPath에 저장 및 json데이터 배열로 파싱하는 메소드

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
    {//vulgarism.json파일 읽어와 persistentDataPath에 저장 및 json데이터를 배열로 파싱하는 메소드

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