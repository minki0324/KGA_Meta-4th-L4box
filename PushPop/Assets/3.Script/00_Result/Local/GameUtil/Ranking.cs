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
    // true면 1P가 이긴 게임 false면 2P가 이긴 게임
    public bool Result = false;

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
/// Ranking관련 Class
/// </summary>
public class Ranking : MonoBehaviour
{
    public static Ranking Instance = null;

    private string rankPath = string.Empty; // Rank Json Path
    private string versusPath = string.Empty; // Bomb Versus Json Path
    private List<Rank> rankList = new List<Rank>(); // Rank List
    private VersusData versusData = new VersusData();

    // before game clear
    [SerializeField] private int previousScore = 0;
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

    /// <summary>
    /// Multi Mode의 결과를 저장하는 Method / Bool값 True일 경우 1P의 승리, False일 경우 2P의 승리
    /// </summary>
    /// <param name="_index1P"></param>
    /// <param name="_name1P"></param>
    /// <param name="_index2P"></param>
    /// <param name="_name2P"></param>
    /// <param name="_result"></param>
    public void SetBombVersus(int _index1P, string _name1P, int _index2P, string _name2P, bool _result)
    { // 새로운 게임 결과 생성
        BombVersus newGameResult = new BombVersus(_name1P, _name2P, _index1P, _index2P, _result);

        // 만약 [0]에 이미 게임 결과가 존재한다면, 그 결과를 [1]으로 이동
        // [1]에 이미 결과가 있다면, [0]의 결과로 대체되고, [1]의 결과는 삭제.
        if (versusData.games[0] != null)
        {
            versusData.games[1] = versusData.games[0];
        }

        // 새로운 게임 결과를 [0]에 저장
        versusData.games[0] = newGameResult;

        // 변경사항을 파일에 저장하는 메소드 호출
        SaveVersus();
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
        LoadRanking();

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
            _name[i].text = "";
            _image[i].sprite = ProfileManager.Instance.NoneBackground;
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // SQL에 등록되어 있는 Profile
            for (int j = 0; j < topRanks.Count; j++)
            { // 정렬된 List
                if (SQL_Manager.instance.ProfileList[i].index == topRanks[j].index)
                { // Profile Index와 정렬된 List의 Index가 일치한 걸 찾아옴
                    _name[j].text = SQL_Manager.instance.ProfileList[i].name;
                    SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.ProfileList[i].imageMode, _image[j], SQL_Manager.instance.ProfileList[i].index);
                }
            }
        }
    }

    /// <summary>
    /// RankList에 담겨 있는 본인의 기록을 조회하고, 기록이 있다면 rankList에 담긴 본인의 기록을 출력, 없다면 공백으로 출력
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_Score"></param>
    /// <param name="_image"></param>
    public void LoadScore_Personal(TMP_Text _name, TMP_Text _Score, Image _image)
    {
        SQL_Manager.instance.SQL_ProfileListSet();
        LoadRanking();

        // 게임매니저에 저장된 프로필 Infomation을 이용하여 rankList에 본인의 기록이 있는지 조회
        var userRecord = rankList.FirstOrDefault(r => r.name == ProfileManager.Instance.ProfileName1P && r.index == ProfileManager.Instance.FirstPlayerIndex);

        if (userRecord != null)
        { // 사용자 기록이 있을 경우, 정보를 표시.
            if (userRecord.score != 0)
            {
                _name.text = userRecord.name;
                _Score.text = userRecord.score.ToString();
            }
            else if (userRecord.score == 0)
            { // 사용자 기록이 없을 경우, 공백을 표시.
                _name.text = userRecord.name;
                _Score.text = "";
            }
        }
        else
        {
            _name.text = ProfileManager.Instance.ProfileName1P;
            _Score.text = "";
        }

        _image.sprite = ProfileManager.Instance.CacheProfileImage1P;
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
        LoadRanking();

        // 특정 spriteName에 대한 모든 타이머 기록을 찾아서 정렬하고 상위 3개를 선택.
        var topRanks = rankList
            .Where(r => r.spriteName.Contains(_spriteName)) // 먼저 spriteName을 포함하는 Rank만 필터링
            .Select(r => new
            {
                Rank = r,
                Timer = r.timer[r.spriteName.IndexOf(_spriteName)] // 해당 spriteName에 해당하는 타이머만 선택
            })
            .Where(x => x.Rank.spriteName.Contains(_spriteName))
            .OrderBy(x => x.Timer)
            .Take(3)
            .ToList();

        for (int i = 0; i < topRanks.Count; i++)
        { // 선택된 상위 3개의 랭크에 대해 text 배열을 업데이트.
            if (i < _timer.Length)
            {
                for (int j = 0; j < topRanks[i].Rank.spriteName.Count; j++)
                {
                    if (topRanks[i].Rank.spriteName[j] == _spriteName)
                    {
                        int sec = topRanks[i].Timer % 60;    //60으로 나눈 나머지 = 초
                        int min = topRanks[i].Timer / 60;
                        _timer[i].text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
                        break;
                    }
                    else
                    {
                        _image[i].sprite = ProfileManager.Instance.NoneBackground;
                        _timer[i].text = "";
                        _name[i].text = "";
                    }
                }
            }
        }

        for (int i = topRanks.Count; i < _timer.Length; i++)
        { // 만약 상위 3위를 채우지 못한 경우, 남은 텍스트 요소를 비움.
            _image[i].sprite = ProfileManager.Instance.NoneBackground;
            _timer[i].text = "";
            _name[i].text = "";
        }

        for (int i = 0; i < SQL_Manager.instance.ProfileList.Count; i++)
        { // SQL에 등록되어 있는 Profile
            for (int j = 0; j < topRanks.Count; j++)
            { // 정렬된 List
                if (SQL_Manager.instance.ProfileList[i].index == topRanks[j].Rank.index)
                { // Profile Index와 정렬된 List의 Index가 일치한 걸 찾아옴
                    _name[j].text = SQL_Manager.instance.ProfileList[i].name;
                    SQL_Manager.instance.PrintProfileImage(SQL_Manager.instance.ProfileList[i].imageMode, _image[j], SQL_Manager.instance.ProfileList[i].index);
                }
            }
        }
    }

    /// <summary>
    /// RankList에 담겨 있는 본인의 기록을 조회하고, 기록이 있다면 rankList에 담긴 본인의 기록을 출력, 없다면 공백으로 출력
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_timer"></param>
    /// <param name="_image"></param>
    /// <param name="_spriteName"></param>
    public void LoadTimer_Personal(TMP_Text _name, TMP_Text _timer, Image _image, int _spriteName)
    {
        SQL_Manager.instance.SQL_ProfileListSet();
        LoadRanking();

        var userRecord = rankList.FirstOrDefault(r => r.index == ProfileManager.Instance.FirstPlayerIndex && r.spriteName.Contains(_spriteName));

        if (userRecord != null)
        {
            for (int i = 0; i < userRecord.spriteName.Count; i++)
            {
                if (userRecord.spriteName[i] == _spriteName)
                {
                    int sec = userRecord.timer[i] % 60;    //60으로 나눈 나머지 = 초
                    int min = userRecord.timer[i] / 60;
                    _name.text = ProfileManager.Instance.ProfileName1P;
                    _timer.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
                    break;
                }
                else
                {
                    _image.sprite = ProfileManager.Instance.NoneBackground;
                    _name.text = userRecord.name;
                    _timer.text = "";
                }
            }
        }
        else
        {
            _image.sprite = ProfileManager.Instance.NoneBackground;
            _name.text = ProfileManager.Instance.ProfileName1P;
            _timer.text = "";
        }
        _image.sprite = ProfileManager.Instance.CacheProfileImage1P;
    }

    /// <summary>
    /// Multi Lobby에서 최근 2개의 게임 결과를 출력하는 Method
    /// </summary>
    /// <param name="_winText"></param>
    /// <param name="_loseText"></param>
    /// <param name="_winImage"></param>
    /// <param name="_loseImage"></param>
    public void LoadVersusResult(TMP_Text[] _winText, TMP_Text[] _loseText, Image[] _winImage, Image[] _loseImage)
    {
        // VersusList 최신화
        LoadVersus();

        // Profile Image 최신화
        SQL_Manager.instance.SQL_ProfileListSet();

        for (int i = 0; i < versusData.games.Length; i++)
        {
            BombVersus currentGame = versusData.games[i];

            if (currentGame != null)
            {
                if (currentGame.Player1PIndex == 0 && currentGame.Player2PIndex == 0)
                { // 삭제된 기록이 있을 경우
                    _winText[i].text = "";
                    _loseText[i].text = "";
                    _winImage[i].sprite = ProfileManager.Instance.NoneBackground;
                    _loseImage[i].sprite = ProfileManager.Instance.NoneBackground;
                }
                else if (currentGame.Result)
                { // 1P가 이긴 경우
                    SetGameResultText(_winText[i], _loseText[i], currentGame.Player1PName, currentGame.Player2PName);
                    SetPlayerProfileImage(_winImage[i], currentGame.Player1PIndex, true);
                    SetPlayerProfileImage(_loseImage[i], currentGame.Player2PIndex, false);
                }
                else
                { // 1P가 진 경우
                    SetGameResultText(_loseText[i], _winText[i], currentGame.Player1PName, currentGame.Player2PName);
                    SetPlayerProfileImage(_loseImage[i], currentGame.Player1PIndex, true);
                    SetPlayerProfileImage(_winImage[i], currentGame.Player2PIndex, false);
                }
            }
            else
            {
                _winText[i].text = "";
                _loseText[i].text = "";
                _winImage[i].sprite = ProfileManager.Instance.NoneBackground;
                _loseImage[i].sprite = ProfileManager.Instance.NoneBackground;
            }
        }
    }

    /// <summary>
    /// 게임이 끝나고 해당 한판에 대한 결과만 출력하는 Method
    /// </summary>
    /// <param name="_winText"></param>
    /// <param name="_loseText"></param>
    /// <param name="_winImage"></param>
    /// <param name="_loseImage"></param>
    public void LoadVersusResult_Personal(TMP_Text _winText, TMP_Text _loseText, Image _winImage, Image _loseImage)
    {
        // VersusList 최신화
        LoadVersus();

        // Profile Image 최신화
        SQL_Manager.instance.SQL_ProfileListSet();

        // 최근 게임을 가져옴
        BombVersus _currentGame = versusData.games[0];

        if (_currentGame.Result)
        { // 1P가 이긴 경우
            SetGameResultText(_winText, _loseText, _currentGame.Player1PName, _currentGame.Player2PName);
            SetPlayerProfileImage(_winImage, _currentGame.Player1PIndex, true);
            SetPlayerProfileImage(_loseImage, _currentGame.Player2PIndex, false);
        }
        else
        { // 1P가 진 경우
            SetGameResultText(_loseText, _winText, _currentGame.Player1PName, _currentGame.Player2PName);
            SetPlayerProfileImage(_loseImage, _currentGame.Player1PIndex, true);
            SetPlayerProfileImage(_winImage, _currentGame.Player2PIndex, false);
        }
    }

    public void SettingPreviousScore()
    { // old score setting, game start 시 load

        int index = GameManager.Instance.boardName;
        int scoreIndex = 0;
        Rank userRecord = rankList.FirstOrDefault(r => r.index == ProfileManager.Instance.FirstPlayerIndex && r.spriteName.Contains(index));

        if (userRecord == null)
        {
            previousScore = 0;
            return;
        }
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                userRecord = rankList.FirstOrDefault(r => r.index == ProfileManager.Instance.FirstPlayerIndex && r.spriteName.Contains(index));
                for (int i = 0; i < userRecord.spriteName.Count; i++)
                {
                    if (index.Equals(userRecord.spriteName[i]))
                    {
                        scoreIndex = i;
                        break;
                    }
                }
                break;
            case GameMode.Memory:
                userRecord = rankList.FirstOrDefault(r => r.index == ProfileManager.Instance.FirstPlayerIndex);
                break;
        }


        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                previousScore = userRecord.timer[scoreIndex];
                break;
            case GameMode.Memory:
                previousScore = userRecord.score;
                break;
        }
    }

    public ClearTitle CompareRanking()
    { // speed, memory mode clear 시
        // new score
        ClearTitle clearTitle = ClearTitle.Clear;
        int currentScore = 0;

        if (GameManager.Instance.GameMode.Equals(GameMode.Speed))
        {
            currentScore = GameManager.Instance.currentTime;
            clearTitle = previousScore > currentScore ? ClearTitle.Better : ClearTitle.Clear;
        }
        else if (GameManager.Instance.GameMode.Equals(GameMode.Memory))
        {
            currentScore = MemoryManager.Instance.Score;
            clearTitle = previousScore < currentScore ? ClearTitle.Better : ClearTitle.Clear;
        }

        return clearTitle;
    }
    #endregion

    #region Json
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

    private void SaveVersus()
    { // 2인 모드 결과를 저장
        string json = JsonUtility.ToJson(versusData, true);
        File.WriteAllText(versusPath, json);
    }

    public void LoadVersus()
    { // 2인 모드 결과 불러오기
        if (File.Exists(versusPath))
        {
            string json = File.ReadAllText(versusPath);
            versusData = JsonUtility.FromJson<VersusData>(json);
        }
    }


    public void DeleteRankAndVersus(int _profileIndex)
    { // Rank 삭제
        rankList.RemoveAll(r => r.index == _profileIndex);
        SaveRanking();

        // Versus 삭제 및 앞으로 땡기기
        for (int i = 0; i < versusData.games.Length; i++)
        {
            if (versusData.games[i] != null)
            {
                if (versusData.games[i].Player1PIndex == _profileIndex || versusData.games[i].Player2PIndex == _profileIndex)
                { // 1P 인덱스나 2P 인덱스가 삭제할 프로필 인덱스와 일치하면 해당 게임 결과를 삭제하고 배열을 앞으로 땡김.
                    for (int j = i; j < versusData.games.Length - 1; j++)
                    {
                        versusData.games[j] = versusData.games[j + 1];
                    }
                    versusData.games[versusData.games.Length - 1] = null; // 마지막 요소는 삭제
                }
            }
        }
        SaveVersus();
    }
    #endregion

    private void SetPlayerProfileImage(Image image, int playerIndex, bool isWinner)
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
