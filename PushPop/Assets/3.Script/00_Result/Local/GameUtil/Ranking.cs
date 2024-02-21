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
/// Ranking���� Class
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
    /// name�� index�� RankList�� ������ Rank�� ������ Score�� �������ִ� Method
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_index"></param>
    /// <param name="_score"></param>
    public void SetScore(string _name, int _index, int _score)
    {
        var existingRank = rankList.FirstOrDefault(r => r.name == _name && r.index == _index);

        if (existingRank != null)
        { // ���� ������ ���Ͽ� �� ������ �� ������ ����
            existingRank.score = Math.Max(_score, existingRank.score);
        }
        else
        { // ���ο� ��ũ �߰�, �̶� timer�� �� ����Ʈ�� �ʱ�ȭ
            rankList.Add(new Rank(_name, _index, _score, new List<int>(), new List<int>()));
        }
        // ����
        SaveRanking();
    }

    /// <summary>
    /// Name�� Index�� RankList�� ������ Rank�� �����Ͽ� SpriteName�� �´� ��ŷ�� �������ִ� Method
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_index"></param>
    /// <param name="_spriteName"></param>
    /// <param name="_Timer"></param>
    public void SetTimer(string _name, int _index, int _spriteName, int _Timer)
    { 
        var existingRank = rankList.FirstOrDefault(r => r.name == _name && r.index == _index);

        if (existingRank != null)
        { // �ش� spriteName�� �ε���.
            int spriteIndex = existingRank.spriteName.IndexOf(_spriteName);

            if (spriteIndex >= 0)
            { // spriteName�� �̹� �����ϸ�, ���ο� timer�� ���� ������ ���� ��� ����
                if (existingRank.timer[spriteIndex] > _Timer)
                {
                    existingRank.timer[spriteIndex] = _Timer;
                }
            }
            else
            { // ���ο� spriteName�� timer ���� �߰�
                existingRank.spriteName.Add(_spriteName);
                existingRank.timer.Add(_Timer);
            }
        }
        else
        { // ���ο� ��ũ �߰�, �� ��� score�� �ʱⰪ(��: 0)���� ����
            rankList.Add(new Rank(_name, _index, 0, new List<int> { _spriteName }, new List<int> { _Timer }));
        }

        SaveRanking();
    }
    #endregion

    #region Rank Load
    /// <summary>
    /// RankList�� ����� �ִ� Rank���� ���� 3���� ����ϵ��� ���� �� �Ű������� ���� Text, Image�� �����ʰ� �Բ� ������ִ� Method
    /// </summary>
    /// <param name="_Score"></param>
    /// <param name="_image"></param>
    /// <param name="_name"></param>
    public void LoadScore(TMP_Text[] _Score, Image[] _image, TMP_Text[] _name)
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // ���ھ ���� ������ ��ũ ����Ʈ�� �����ϰ�, ���� 3���� ��ũ�� ����.
        var topRanks = rankList
            .Where(r => r.score > 0)
            .OrderByDescending(r => r.score)
            .Take(3)
            .ToList();

        for (int i = 0; i < topRanks.Count; i++)
        { // ���õ� ���� 3���� ��ũ�� ���� text �迭�� ������Ʈ.
            if (i < _Score.Length)
            { 
                _Score[i].text = topRanks[i].score.ToString();
            }
        }

        for (int i = topRanks.Count; i < _Score.Length; i++)
        { // ���� ���� 3���� ä���� ���� ���, ���� �ؽ�Ʈ ��Ҹ� ���.
            _Score[i].text = "";
        }

        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL�� ��ϵǾ� �ִ� Profile
            for(int j = 0; j < topRanks.Count; j++)
            { // ���ĵ� List
                if (SQL_Manager.instance.Profile_list[i].index == topRanks[j].index)
                { // Profile Index�� ���ĵ� List�� Index�� ��ġ�� �� ã�ƿ�
                    _name[j].text = SQL_Manager.instance.Profile_list[i].name;
                    if(SQL_Manager.instance.Profile_list[i].imageMode)
                    { // �̹��� ���⸦ ������ �÷��̾�
                        _image[j].sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
                    }
                    else if(!SQL_Manager.instance.Profile_list[i].imageMode)
                    { // ���� ��⸦ ������ �÷��̾�
                        Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(GameManager.Instance.UID, SQL_Manager.instance.Profile_list[i].index);
                        Sprite profileSprite = GameManager.Instance.TextureToSprite(profileTexture);
                        _image[j].sprite = profileSprite;
                    }
                }
            }
        }
    }

    /// <summary>
    /// RankList�� ����� �ִ� Rank�� �� ������ SpriteName�� Rank�� ã�� Clear�� ���� ������� �����Ͽ� Text�� Image�� ������ִ� Method
    /// </summary>
    /// <param name="_timer"></param>
    /// <param name="_image"></param>
    /// <param name="_name"></param>
    /// <param name="_spriteName"></param>
    public void LoadTimer(TMP_Text[] _timer, Image[] _image, TMP_Text[] _name, int _spriteName)
    {
        SQL_Manager.instance.SQL_ProfileListSet();

        // Ư�� spriteName�� ���� ��� Ÿ�̸� ����� ã�Ƽ� �����ϰ� ���� 3���� �����մϴ�.
        var topRanks = rankList
         .Where(r => r.spriteName.Contains(_spriteName))
         .SelectMany(r => r.timer.Select((timer, index) => new { Rank = r, Timer = timer }))
         .Where(x => x.Rank.spriteName.Contains(_spriteName))
         .OrderBy(x => x.Timer)
         .Take(3)
         .ToList();

        for(int i = 0; i < topRanks.Count; i++)
        {// ���õ� ���� 3���� ��ũ�� ���� text �迭�� ������Ʈ.
            if (i < _timer.Length)
            {
                _timer[i].text = topRanks[i].Timer.ToString();
            }
        }

        for (int i = topRanks.Count; i < _timer.Length; i++)
        { // ���� ���� 3���� ä���� ���� ���, ���� �ؽ�Ʈ ��Ҹ� ���.
            _timer[i].text = "";
        }

        Debug.Log(topRanks[0].Rank.index);
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        { // SQL�� ��ϵǾ� �ִ� Profile
            for (int j = 0; j < topRanks.Count; j++)
            { // ���ĵ� List
                if (SQL_Manager.instance.Profile_list[i].index == topRanks[j].Rank.index)
                { // Profile Index�� ���ĵ� List�� Index�� ��ġ�� �� ã�ƿ�
                    _name[j].text = SQL_Manager.instance.Profile_list[i].name;
                    if (SQL_Manager.instance.Profile_list[i].imageMode)
                    { // �̹��� ���⸦ ������ �÷��̾�
                        _image[j].sprite = GameManager.Instance.ProfileImages[SQL_Manager.instance.Profile_list[i].defaultImage];
                    }
                    else if (!SQL_Manager.instance.Profile_list[i].imageMode)
                    { // ���� ��⸦ ������ �÷��̾�
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
    { // ��ŷ ������ JSON���� ����
        RankList rankListWrapper = new RankList { ranks = rankList };
        string json = JsonUtility.ToJson(rankListWrapper, true);
        File.WriteAllText(rankPath, json);
    }
    
    private void LoadRanking()
    { // JSON���� ��ŷ ������ �ҷ�����
        if (File.Exists(rankPath))
        {
            string json = File.ReadAllText(rankPath);
            RankList rankListWrapper = JsonUtility.FromJson<RankList>(json);
            rankList = rankListWrapper.ranks;
        }
    }
    #endregion
}
