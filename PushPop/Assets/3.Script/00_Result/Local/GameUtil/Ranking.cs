using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

#region Other Class
[System.Serializable]
public class RankList
{
    public List<Rank> ranks = new List<Rank>();
}

[System.Serializable]
public class Rank
{
    public string name;
    public int index;
    public int score;
    public List<int> spriteName;
    public List<int> timer;

    public Rank(string name, int index, int score, List<int> spriteName, List<int> timer)
    {
        this.name = name;
        this.index = index;
        this.score = score;
        this.spriteName = spriteName;
        this.timer = timer;
    }
}
#endregion

/// <summary>
/// Ranking관련 Class
/// </summary>
public class Ranking : MonoBehaviour
{
    public static Ranking instance = null;

    private string rankPath = string.Empty; // Rank Json Path
    private List<Rank> rankList = new List<Rank>(); // Rank List

    #region Unity Callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        rankPath = Application.persistentDataPath + "/Rank";
        LoadRanking();
    }
    #endregion

    #region Other Method
    #region Rank Set
    /// <summary>
    /// name과 index로 RankList에 동일한 Rank를 가져와 Score를 갱신해주는 Method
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_index"></param>
    /// <param name="_score"></param>
    public void SetScore(string _name, int _index, int _score)
    {
        var existingRank = rankList.FirstOrDefault(r => r.name == _name && r.index == _index);

        if (existingRank != null)
        { // 기존 점수와 비교하여 새 점수가 더 높으면 갱신
            existingRank.score = Math.Max(_score, existingRank.score);
        }
        else
        { // 새로운 랭크 추가, 이때 timer는 빈 리스트로 초기화
            rankList.Add(new Rank(_name, _index, _score, new List<int>(), new List<int>()));
        }
        // 저장
        SaveRanking();
    }

    /// <summary>
    /// Name과 Index로 RankList에 동일한 Rank를 선택하여 SpriteName에 맞는 랭킹을 갱신해주는 Method
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_index"></param>
    /// <param name="_spriteName"></param>
    /// <param name="_Timer"></param>
    public void SetTimer(string _name, int _index, int _spriteName, int _Timer)
    { 
        var existingRank = rankList.FirstOrDefault(r => r.name == _name && r.index == _index);

        if (existingRank != null)
        { // 해당 spriteName의 인덱스.
            int spriteIndex = existingRank.spriteName.IndexOf(_spriteName);

            if (spriteIndex >= 0)
            { // spriteName이 이미 존재하며, 새로운 timer가 기존 값보다 작은 경우 갱신
                if (existingRank.timer[spriteIndex] > _Timer)
                {
                    existingRank.timer[spriteIndex] = _Timer;
                }
            }
            else
            { // 새로운 spriteName과 timer 값을 추가
                existingRank.spriteName.Add(_spriteName);
                existingRank.timer.Add(_Timer);
            }
        }
        else
        { // 새로운 랭크 추가, 이 경우 score는 초기값(예: 0)으로 설정
            rankList.Add(new Rank(_name, _index, 0, new List<int> { _spriteName }, new List<int> { _Timer }));
        }

        SaveRanking();
    }
    #endregion

    #region Rank Load
    /// <summary>
    /// RankList에 담겨져 있는 Rank들을 상위 3개만 출력하도록 정렬 후 매개변수로 받은 Text, Image에 프로필과 함께 출력해주는 Method
    /// </summary>
    /// <param name="_Score"></param>
    /// <param name="_image"></param>
    /// <param name="_name"></param>
    public void LoadScore(TMP_Text[] _Score, Image[] _image, TMP_Text[] _name)
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // 스코어가 높은 순으로 랭크 리스트를 정렬하고, 상위 3개의 랭크만 선택.
        var topRanks = rankList
            .Where(r => r.score > 0)
            .OrderByDescending(r => r.score)
            .Take(3)
            .ToList();

        for (int i = 0; i < topRanks.Count; i++)
        { // 선택된 상위 3개의 랭크에 대해 text 배열을 업데이트.
            if (i < _Score.Length)
            { 
                _Score[i].text = topRanks[i].score.ToString();
            }
        }

        for (int i = topRanks.Count; i < _Score.Length; i++)
        { // 만약 상위 3위를 채우지 못한 경우, 남은 텍스트 요소를 비움.
            _Score[i].text = "";
        }

        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL에 등록되어 있는 Profile
            for(int j = 0; j < topRanks.Count; j++)
            { // 정렬된 List
                if (SQL_Manager.instance.Profile_list[i].index == topRanks[j].index)
                { // Profile Index와 정렬된 List의 Index가 일치한 걸 찾아옴
                    _name[j].text = SQL_Manager.instance.Profile_list[i].name;
                    if(SQL_Manager.instance.Profile_list[i].imageMode)
                    { // 이미지 고르기를 선택한 플레이어
                        _image[j].sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
                    }
                    else if(!SQL_Manager.instance.Profile_list[i].imageMode)
                    { // 사진 찍기를 선택한 플레이어
                        Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, SQL_Manager.instance.Profile_list[i].index);
                        Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                        _image[j].sprite = profileSprite;
                    }
                }
            }
        }
    }

    /// <summary>
    /// RankList에 담겨져 있는 Rank들 중 동일한 SpriteName의 Rank를 찾아 Clear가 빠른 순서대로 정렬하여 Text와 Image에 출력해주는 Method
    /// </summary>
    /// <param name="_timer"></param>
    /// <param name="_image"></param>
    /// <param name="_name"></param>
    /// <param name="_spriteName"></param>
    public void LoadTimer(TMP_Text[] _timer, Image[] _image, TMP_Text[] _name, int _spriteName)
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // 특정 spriteName에 대한 모든 타이머 기록을 찾아서 정렬하고 상위 3개를 선택합니다.
        var topRanks = rankList
         .Where(r => r.spriteName.Contains(_spriteName))
         .SelectMany(r => r.timer.Select((timer, index) => new { Rank = r, Timer = timer }))
         .Where(x => x.Rank.spriteName.Contains(_spriteName))
         .OrderBy(x => x.Timer)
         .Take(3)
         .ToList();

        for(int i = 0; i < topRanks.Count; i++)
        {// 선택된 상위 3개의 랭크에 대해 text 배열을 업데이트.
            if (i < _timer.Length)
            {
                _timer[i].text = topRanks[i].Timer.ToString();
            }
        }

        for (int i = topRanks.Count; i < _timer.Length; i++)
        { // 만약 상위 3위를 채우지 못한 경우, 남은 텍스트 요소를 비움.
            _timer[i].text = "";
        }

        Debug.Log(topRanks[0].Rank.index);
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL에 등록되어 있는 Profile
            for (int j = 0; j < topRanks.Count; j++)
            { // 정렬된 List
                if (SQL_Manager.instance.Profile_list[i].index == topRanks[j].Rank.index)
                { // Profile Index와 정렬된 List의 Index가 일치한 걸 찾아옴
                    _name[j].text = SQL_Manager.instance.Profile_list[i].name;
                    if (SQL_Manager.instance.Profile_list[i].imageMode)
                    { // 이미지 고르기를 선택한 플레이어
                        _image[j].sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
                    }
                    else if (!SQL_Manager.instance.Profile_list[i].imageMode)
                    { // 사진 찍기를 선택한 플레이어
                        Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, SQL_Manager.instance.Profile_list[i].index);
                        Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                        _image[j].sprite = profileSprite;
                    }
                }
            }
        }
    }
    #endregion

    private void SaveRanking()
    { // 랭킹 데이터 JSON으로 저장
        RankList rankListWrapper = new RankList { ranks = rankList };
        string json = JsonUtility.ToJson(rankListWrapper, true);
        File.WriteAllText(rankPath, json);
    }
    
    private void LoadRanking()
    { // JSON에서 랭킹 데이터 불러오기
        if (File.Exists(rankPath))
        {
            string json = File.ReadAllText(rankPath);
            RankList rankListWrapper = JsonUtility.FromJson<RankList>(json);
            rankList = rankListWrapper.ranks;
        }
    }
    #endregion
}
