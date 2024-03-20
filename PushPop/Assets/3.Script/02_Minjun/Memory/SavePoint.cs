using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
[System.Serializable]
public class PlayersStage
{
   public List<CompletedStage> stages = new List<CompletedStage>();
}
[System.Serializable]
public class CompletedStage
{
    public string profileName;
    public int profileIndex;
    public int saveStageIndex;
    public CompletedStage(string name , int ID , int stageindex)
    {
        profileName = name;
        profileIndex = ID;
        saveStageIndex = stageindex;
    }
}

public class SavePoint : MonoBehaviour
{

    public static SavePoint Instance = null;

    public string savePath = string.Empty;
    public int saveStage;
    public List<CompletedStage> completedStageList = new List<CompletedStage>();
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        savePath = Application.persistentDataPath + "/MemorySave.json";
        LoadStage();
     
    }

    public void SaveStage()
    {
        PlayersStage rankListWrapper = new PlayersStage { stages = completedStageList };
        string json = JsonUtility.ToJson(rankListWrapper, true);
        File.WriteAllText(savePath, json);
    }
    private void LoadStage()
    { // JSON에서 랭킹 데이터 불러오기
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayersStage rankListWrapper = JsonUtility.FromJson<PlayersStage>(json);
            completedStageList = rankListWrapper.stages;
        }
    }

    public void SetStage(string _name, int _index, int _stage)
    {
        GameManager.Instance.myMeomoryStageInfo = completedStageList.FirstOrDefault(stage => stage.profileName == ProfileManager.Instance.myProfile.name);

        if (GameManager.Instance.myMeomoryStageInfo != null)
        { // 기존 점수와 비교하여 새 점수가 더 높으면 갱신
            GameManager.Instance.myMeomoryStageInfo.saveStageIndex = Mathf.Max(_stage, GameManager.Instance.myMeomoryStageInfo.saveStageIndex);
        }
        else
        { // 새로운 랭크 추가, 이때 timer는 빈 리스트로 초기화
            completedStageList.Add(new CompletedStage(_name, _index, _stage));
            GameManager.Instance.myMeomoryStageInfo = completedStageList.FirstOrDefault(stage => stage.profileName == ProfileManager.Instance.myProfile.name);

        }
        // 저장
        SaveStage();
    }
}
