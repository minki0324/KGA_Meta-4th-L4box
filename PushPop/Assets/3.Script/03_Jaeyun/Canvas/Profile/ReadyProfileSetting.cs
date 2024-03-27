using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Player
{
    Player1 = 0,
    Player2
}

public class ReadyProfileSetting : MonoBehaviour
{ // Ready
    [HideInInspector] public int PlayerScore = 0; // score & time
    [HideInInspector] public bool IsSelect = false; // multi profile select 시 true

    [Header("Profile 1P Info")]
    [SerializeField] private Image profileImage1P = null;
    [SerializeField] private TMP_Text profileName1P = null;
    [SerializeField] private TMP_Text profileScore = null; // score & time

    [Header("Only Speed, Memory Mode")]
    [Header("Rank Info")] // only memory mode
    [SerializeField] private Image[] rankFrameImage = null;
    [SerializeField] private Image[] rankImage = null;
    [SerializeField] private TMP_Text[] rankProfileName = null;
    [SerializeField] private TMP_Text[] rankScoreText = null;

    [Header("Only Multi Mode")]
    [Header("Profile 2P Info")] // only multi mode
    public Image ProfileImage2P = null;
    public TMP_Text ProfileName2P = null;

    [Header("Rank Win Info")] // only multi mode
    [SerializeField] private Image[] winLankImage = null;
    [SerializeField] private TMP_Text[] winProfileName = null;
    [SerializeField] private Image[] winFrameImage = null;

    [Header("Rank Lose Info")] // only multi mode
    [SerializeField] private Image[] loseLankImage = null;
    [SerializeField] private TMP_Text[] loseProfileName = null;
    [SerializeField] private Image[] loseFrameImage = null;

    private void OnEnable()
    {
        PlayerInfoSetting();
        RankInfoSetting();
    }

    public void PlayerInfoSetting()
    { // 현재 프로필
        // 프로필 설정 1P
        profileImage1P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        profileName1P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;

        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                PlayerScore = Ranking.Instance.LoadPersonalTimer();
                float sec = PlayerScore % 60; // 60으로 나눈 나머지 = 초
                float min = PlayerScore / 60;
                profileScore.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
                break;
            case GameMode.Memory:
                PlayerScore = Ranking.Instance.LoadPersonalScore();
                profileScore.text = $"{PlayerScore}점";
                break;
        }
    }

    public void SelectPlayerInfoSetting()
    { // 2P를 선택했을 때
        ProfileImage2P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileImage;
        ProfileName2P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName;
    }

    public void RankInfoSetting()
    { // 랭킹 세팅
        List<Image[]> rankImage = new List<Image[]>(); // multi 때문에 List로 선언
        List<Image[]> framImage = new List<Image[]>();
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                rankImage.Add(this.rankImage);
                framImage.Add(rankFrameImage);
                Ranking.Instance.LoadTimer(rankScoreText, this.rankImage, rankProfileName);
                break;
            case GameMode.Memory:
                rankImage.Add(this.rankImage);
                framImage.Add(rankFrameImage);
                Ranking.Instance.LoadScore(rankScoreText, this.rankImage, rankProfileName);
                break;
            case GameMode.Multi:
                rankImage.Add(winLankImage); // frameImage 0
                rankImage.Add(loseLankImage); // frameImgae 1
                framImage.Add(winFrameImage);
                framImage.Add(loseFrameImage);
                Ranking.Instance.LoadVersusResult(winProfileName, loseProfileName, winLankImage, loseLankImage);
                break;
        }

        for (int i = 0; i < rankImage.Count; i++)
        { // multi mode count = 2, other mode = 1
            for (int j = 0; j < rankImage[i].Length; j++)
            { // 기록이 없을 때 Frame Image 꺼두기
                if (rankImage[i][j].sprite == null)
                {
                    framImage[i][j].enabled = false;
                }
                else
                {
                    framImage[i][j].enabled = true;
                }
            }
        }

    }
}
