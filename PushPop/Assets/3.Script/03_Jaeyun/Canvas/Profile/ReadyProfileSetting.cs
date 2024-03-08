using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    [Header("Only Speed Mode")]
    [Header("High Score Info")] // only speed mode
    [SerializeField] private Image highRankFrameImage = null;
    [SerializeField] private Image highRankProfileImage = null;
    [SerializeField] private TMP_Text hightRankProfileNameText = null;
    [SerializeField] private TMP_Text hightRankScoreText = null;

    [Header("Only Memory Mode")]
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

    private void PlayerInfoSetting()
    { // 현재 프로필
        // 프로필 설정 1P
        profileImage1P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        profileName1P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;

        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                profileScore.text = $"{PlayerScore}";
                break;
            case GameMode.Memory:
                profileScore.text = $"{PlayerScore}";
                break;
        }
    }

    public void SelectPlayerInfoSetting()
    { // 2P를 선택했을 때
        ProfileImage2P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileImage;
        ProfileName2P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName;
    }

    private void RankInfoSetting()
    { // 랭킹 세팅
        List<Image[]> frameImage = new List<Image[]>(); // multi 때문에 List로 선언
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                break;
            case GameMode.Memory:
                frameImage.Add(rankImage);
                Ranking.Instance.LoadScore(rankScoreText, rankImage, rankProfileName);
                break;
            case GameMode.Multi:
                frameImage.Add(winLankImage);
                frameImage.Add(loseLankImage);
                Ranking.Instance.LoadVersusResult(winProfileName, loseProfileName, winLankImage, loseLankImage);
                break;
        }

        for (int i = 0; i < frameImage[(int)Player.Player1].Length; i++)
        { // 기록이 없을 때 Frame Image 꺼두기
            if (frameImage[(int)Player.Player1][i].sprite.Equals(ProfileManager.Instance.NoneBackground))
            {
                frameImage[(int)Player.Player1][i].enabled = false;
                frameImage[(int)Player.Player2][i].enabled = false;
            }
            else
            {
                frameImage[(int)Player.Player1][i].enabled = true;
                frameImage[(int)Player.Player2][i].enabled = true;
            }
        }
    }
}
