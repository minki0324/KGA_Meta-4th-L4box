using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ClearTitle
{
    Clear = 0,
    Better,
    Fail
}

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

[System.Serializable]
public class VersusData
{
    public BombVersus[] games = new BombVersus[2];
}
[System.Serializable]
public class BombVersus
{
    public string Player1PName;
    public string Player2PName;
    public int Player1PIndex;
    public int Player2PIndex;
    public bool Result = false; // true�� 1P�� �̱� ���� false�� 2P�� �̱� ����

    public BombVersus(string player1PName, string player2PName, int player1PIndex, int player2PIndex, bool result)
    {
        Player1PName = player1PName;
        Player2PName = player2PName;
        Player1PIndex = player1PIndex;
        Player2PIndex = player2PIndex;
        Result = result;
    }
}
#endregion

/// <summary>
/// Ranking���� Class
/// </summary>
public class Ranking : MonoBehaviour
{
    public static Ranking Instance = null;

    private string rankPath = string.Empty; // Rank Json Path
    private string versusPath = string.Empty; // Bomb Versus Json Path
    private List<Rank> rankList = new List<Rank>(); // Rank List
    private VersusData versusData = new VersusData();

    // before game clear
    private int previousScore = 0;
    public DialogData ResultDialog;

    #region Unity Callback
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

        rankPath = Application.persistentDataPath + "/Rank.json";
        versusPath = Application.persistentDataPath + "/Versus.json";

        LoadRanking();
        LoadVersus();
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

    /// <summary>
    /// Multi Mode�� ����� �����ϴ� Method / Bool�� True�� ��� 1P�� �¸�, False�� ��� 2P�� �¸�
    /// </summary>
    /// <param name="_index1P"></param>
    /// <param name="_name1P"></param>
    /// <param name="_index2P"></param>
    /// <param name="_name2P"></param>
    /// <param name="_result"></param>
    public void SetMultiVersus(int _index1P, string _name1P, int _index2P, string _name2P, bool _result)
    { // ���ο� ���� ��� ����
        BombVersus newGameResult = new BombVersus(_name1P, _name2P, _index1P, _index2P, _result);

        // ���� [0]�� �̹� ���� ����� �����Ѵٸ�, �� ����� [1]���� �̵�
        // [1]�� �̹� ����� �ִٸ�, [0]�� ����� ��ü�ǰ�, [1]�� ����� ����.
        if (versusData.games[0] != null)
        {
            versusData.games[1] = versusData.games[0];
        }

        // ���ο� ���� ����� [0]�� ����
        versusData.games[0] = newGameResult;

        // ��������� ���Ͽ� �����ϴ� �޼ҵ� ȣ��
        SaveVersus();
    }
    #endregion

    #region Rank Load
    /// <summary>
    /// RankList�� ����� �ִ� Rank���� ���� 3���� ����ϵ��� ���� �� �Ű������� ���� Text, Image�� �����ʰ� �Բ� ������ִ� Method
    /// </summary>
    /// <param name="_score"></param>
    /// <param name="_image"></param>
    /// <param name="_name"></param>
    public void LoadScore(TMP_Text[] _score, Image[] _image, TMP_Text[] _name)
    {
        SQL_Manager.instance.SQL_ProfileListSet();
        LoadRanking();

        // ���ھ ���� ������ ��ũ ����Ʈ�� �����ϰ�, ���� 3���� ��ũ�� ����.
        var topRanks = rankList
            .Where(r => r.score > 0)
            .OrderByDescending(r => r.score)
            .Take(3)
            .ToList();

        for (int i = 0; i < topRanks.Count; i++)
        { // ���õ� ���� 3���� ��ũ�� ���� text �迭�� ������Ʈ.
            if (i < _score.Length)
            {
                _score[i].text = topRanks[i].score.ToString();
            }
        }

        for (int i = topRanks.Count; i < _score.Length; i++)
        { // ���� ���� 3���� ä���� ���� ���, ���� �ؽ�Ʈ ��Ҹ� ���.
            _score[i].text = "";
            _name[i].text = "";
            _image[i].sprite = null;
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // SQL�� ��ϵǾ� �ִ� Profile
            for (int j = 0; j < topRanks.Count; j++)
            { // ���ĵ� List
                if (SQL_Manager.instance.ProfileList[i].index == topRanks[j].index)
                { // Profile Index�� ���ĵ� List�� Index�� ��ġ�� �� ã�ƿ�
                    _name[j].text = SQL_Manager.instance.ProfileList[i].name;
                    SQL_Manager.instance.PrintProfileImage(_image[j], SQL_Manager.instance.ProfileList[i].imageMode, SQL_Manager.instance.ProfileList[i].index);
                }
            }
        }
    }

    public int LoadPersonalScore()
    { // memory mode score �ҷ���
        SQL_Manager.instance.SQL_ProfileListSet();
        LoadRanking();

        // ���ӸŴ����� ����� ������ Infomation�� �̿��Ͽ� rankList�� ������ ����� �ִ��� ��ȸ
        Rank userRecord = rankList.FirstOrDefault(r => r.name.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName) && r.index.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex));

        if (userRecord != null)
        { // ����� ���� ��
            return userRecord.score;
        }

        return 0;
    }

    /// <summary>
    /// RankList�� ����� �ִ� Rank�� �� ������ SpriteName�� Rank�� ã�� Clear�� ���� ������� �����Ͽ� Text�� Image�� ������ִ� Method
    /// </summary>
    /// <param name="_timer"></param>
    /// <param name="_image"></param>
    /// <param name="_name"></param>
    public void LoadTimer(TMP_Text[] _timer, Image[] _image, TMP_Text[] _name)
    {
        SQL_Manager.instance.SQL_ProfileListSet();
        LoadRanking();

        // Ư�� spriteName�� ���� ��� Ÿ�̸� ����� ã�Ƽ� �����ϰ� ���� 3���� ����.
        var topRanks = rankList
            .Where(r => r.spriteName.Contains(GameManager.Instance.CurrentIconName)) // ���� spriteName�� �����ϴ� Rank�� ���͸�
            .Select(r => new
            {
                Rank = r,
                Timer = r.timer[r.spriteName.IndexOf(GameManager.Instance.CurrentIconName)] // �ش� spriteName�� �ش��ϴ� Ÿ�̸Ӹ� ����
            })
            .Where(x => x.Rank.spriteName.Contains(GameManager.Instance.CurrentIconName))
            .OrderBy(x => x.Timer)
            .Take(3)
            .ToList();

        for (int i = 0; i < topRanks.Count; i++)
        { // ���õ� ���� 3���� ��ũ�� ���� text �迭�� ������Ʈ.
            if (i < _timer.Length)
            {
                for (int j = 0; j < topRanks[i].Rank.spriteName.Count; j++)
                {
                    if (topRanks[i].Rank.spriteName[j] == GameManager.Instance.CurrentIconName)
                    {
                        int sec = topRanks[i].Timer % 60;    //60���� ���� ������ = ��
                        int min = topRanks[i].Timer / 60;
                        _timer[i].text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
                        break;
                    }
                    else
                    {
                        _image[i].sprite = null;
                        _timer[i].text = "";
                        _name[i].text = "";
                    }
                }
            }
        }

        for (int i = topRanks.Count; i < _timer.Length; i++)
        { // ���� ���� 3���� ä���� ���� ���, ���� �ؽ�Ʈ ��Ҹ� ���.
            _image[i].sprite = null;
            _timer[i].text = "";
            _name[i].text = "";
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // SQL�� ��ϵǾ� �ִ� Profile
            for (int j = 0; j < topRanks.Count; j++)
            { // ���ĵ� List
                if (SQL_Manager.instance.ProfileList[i].index == topRanks[j].Rank.index)
                { // Profile Index�� ���ĵ� List�� Index�� ��ġ�� �� ã�ƿ�
                    _name[j].text = SQL_Manager.instance.ProfileList[i].name;
                    SQL_Manager.instance.PrintProfileImage(_image[j], SQL_Manager.instance.ProfileList[i].imageMode, SQL_Manager.instance.ProfileList[i].index);
                }
            }
        }
    }

    public int LoadPersonalTimer()
    { // speed mode timer �ҷ���
        SQL_Manager.instance.SQL_ProfileListSet();
        LoadRanking();

        Rank userRecord = rankList.FirstOrDefault(r => r.index.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex) && r.spriteName.Contains(GameManager.Instance.CurrentIconName));

        if (userRecord != null)
        { // ����� ���� ��
            for (int i = 0; i < userRecord.spriteName.Count; i++)
            {
                if (userRecord.spriteName[i].Equals(GameManager.Instance.CurrentIconName))
                {
                    return userRecord.timer[i];
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// Multi Lobby���� �ֱ� 2���� ���� ����� ����ϴ� Method
    /// </summary>
    /// <param name="_winText"></param>
    /// <param name="_loseText"></param>
    /// <param name="_winImage"></param>
    /// <param name="_loseImage"></param>
    public void LoadVersusResult(TMP_Text[] _winText, TMP_Text[] _loseText, Image[] _winImage, Image[] _loseImage)
    {
        // VersusList �ֽ�ȭ
        LoadVersus();

        // Profile Image �ֽ�ȭ
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < versusData.games.Length; i++)
        {
            BombVersus currentGame = versusData.games[i];
            if (currentGame != null)
            {
                if (currentGame.Player1PIndex == 0 && currentGame.Player2PIndex == 0)
                { // ������ ���� ����� �ִ� ���, ĭ ����
                    _winText[i].text = "";
                    _loseText[i].text = "";
                    _winImage[i].sprite = null;
                    _loseImage[i].sprite = null;

                }
                else if (currentGame.Result)
                { // 1P�� �̱� ���
                    SetGameResultText(_winText[i], _loseText[i], currentGame.Player1PName, currentGame.Player2PName);
                    SetPlayerProfileImage(_winImage[i], currentGame.Player1PIndex);
                    SetPlayerProfileImage(_loseImage[i], currentGame.Player2PIndex);
                }
                else
                { // 1P�� �� ���
                    SetGameResultText(_loseText[i], _winText[i], currentGame.Player1PName, currentGame.Player2PName);
                    SetPlayerProfileImage(_loseImage[i], currentGame.Player1PIndex);
                    SetPlayerProfileImage(_winImage[i], currentGame.Player2PIndex);
                }
            }
            else
            {
                _winText[i].text = "";
                _loseText[i].text = "";
                _winImage[i].sprite = null;
                _loseImage[i].sprite = null;
            }
        }
    }

    /// <summary>
    /// ������ ������ �ش� ���ǿ� ���� ����� ����ϴ� Method
    /// </summary>
    /// <param name="_winText"></param>
    /// <param name="_loseText"></param>
    /// <param name="_winImage"></param>
    /// <param name="_loseImage"></param>
    public void LoadVersusResult_Personal(TMP_Text _winText, TMP_Text _loseText, Image _winImage, Image _loseImage)
    {
        // VersusList �ֽ�ȭ
        LoadVersus();

        // Profile Image �ֽ�ȭ
        SQL_Manager.instance.SQL_ProfileListSet();

        // �ֱ� ������ ������
        BombVersus _currentGame = versusData.games[0];

        if (_currentGame.Result)
        { // 1P�� �̱� ���
            SetGameResultText(_winText, _loseText, _currentGame.Player1PName, _currentGame.Player2PName);
            SetPlayerProfileImage(_winImage, _currentGame.Player1PIndex);
            SetPlayerProfileImage(_loseImage, _currentGame.Player2PIndex);
        }
        else
        { // 1P�� �� ���
            SetGameResultText(_loseText, _winText, _currentGame.Player1PName, _currentGame.Player2PName);
            SetPlayerProfileImage(_loseImage, _currentGame.Player1PIndex);
            SetPlayerProfileImage(_winImage, _currentGame.Player2PIndex);
        }
    }

    public void SettingPreviousScore()
    { // old score setting, game start �� load
        int index = GameManager.Instance.CurrentIconName;
        int scoreIndex = 0;
        Rank userRecord = null;

        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                userRecord = rankList.FirstOrDefault(r => r.index.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex) && r.spriteName.Contains(index));
                if (userRecord == null)
                {
                    previousScore = 0;
                    return;
                }
                for (int i = 0; i < userRecord.spriteName.Count; i++)
                {
                    if (index.Equals(userRecord.spriteName[i]))
                    {
                        scoreIndex = i;
                        break;
                    }
                }
                previousScore = userRecord.timer[scoreIndex];
                break;
            case GameMode.Memory:
                userRecord = rankList.FirstOrDefault(r => r.index.Equals(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex));
                if (userRecord == null)
                {
                    previousScore = 0;
                    return;
                }
                previousScore = userRecord.score;
                break;
        }
    }

    public ClearTitle CompareRanking(int _currentScore)
    { // speed, memory mode clear ��
        // new score
        ClearTitle clearTitle = ClearTitle.Clear;
        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            if (_currentScore >= (int)GameManager.Instance.Difficulty)
            {
                clearTitle = ClearTitle.Fail;
            }
            else
            {
                clearTitle = previousScore > _currentScore ? ClearTitle.Better : ClearTitle.Clear;
            }
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Memory))
        {
            clearTitle = previousScore < _currentScore ? ClearTitle.Better : ClearTitle.Clear;
        }

        return clearTitle;
    }
    #endregion

    #region Json
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

    private void SaveVersus()
    { // 2�� ��� ����� ����
        string json = JsonUtility.ToJson(versusData, true);
        File.WriteAllText(versusPath, json);
    }

    public void LoadVersus()
    { // 2�� ��� ��� �ҷ�����
        if (File.Exists(versusPath))
        {
            string json = File.ReadAllText(versusPath);
            versusData = JsonUtility.FromJson<VersusData>(json);
        }
    }

    public void DeleteRankAndVersus(int _profileIndex)
    { // Rank ����
        rankList.RemoveAll(r => r.index == _profileIndex);
        SaveRanking();

        // Versus ���� �� ������ �����
        for (int i = 0; i < versusData.games.Length; i++)
        {
            if (versusData.games[i] != null)
            {
                if (versusData.games[i].Player1PIndex == _profileIndex || versusData.games[i].Player2PIndex == _profileIndex)
                { // 1P �ε����� 2P �ε����� ������ ������ �ε����� ��ġ�ϸ� �ش� ���� ����� �����ϰ� �迭�� ������ ����.
                    for (int j = i; j < versusData.games.Length - 1; j++)
                    {
                        versusData.games[j] = versusData.games[j + 1];
                    }
                    versusData.games[versusData.games.Length - 1] = null; // ������ ��Ҵ� ����
                }
            }
        }
        SaveVersus();
    }
    #endregion

    private void SetPlayerProfileImage(Image image, int playerIndex)
    {
        Profile profile = GetPlayerProfile(playerIndex);

        if (profile != null)
        {
            if (profile.imageMode)
            {
                image.sprite = ProfileManager.Instance.ProfileImages[profile.defaultImage];
            }
            else
            {
                Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(ProfileManager.Instance.UID, profile.index);
                Sprite profileSprite = ProfileManager.Instance.TextureToSprite(profileTexture);
                image.sprite = profileSprite;
            }
        }
    }

    private Profile GetPlayerProfile(int playerIndex)
    {
        foreach (Profile profile in SQL_Manager.instance.ProfileList)
        {
            if (profile.index == playerIndex)
            {
                return profile;
            }
        }
        return null;
    }

    private void SetGameResultText(TMP_Text winText, TMP_Text loseText, string winPlayerName, string losePlayerName)
    {
        winText.text = winPlayerName;
        loseText.text = losePlayerName;
    }
    #endregion
}
