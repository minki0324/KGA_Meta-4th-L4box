using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Player
{
    Player1 = 0,
    Player2
}

public class ReadyProfileSetting : MonoBehaviour
{ // Ready
    [Header("Game")]
    public int PlayerScore = 0; // score & time

    [Header("Profile 1P Info")]
    [SerializeField] private Image profileImage1P = null;
    [SerializeField] private TMP_Text profileName1P = null;
    [SerializeField] private TMP_Text profileScore = null; // score & time

    [Header("Profile 2P Info")] // only multi mode
    public Image ProfileImage2P = null;
    public TMP_Text ProfileName2P = null;

    [Header("Rank Info")]


    [Header("Rank Win Info")]
    [SerializeField] private Image[] winLankImage = null;
    [SerializeField] private TMP_Text[] winProfileName = null;
    [SerializeField] private Image[] winFrameImage = null;

    [Header("Rank Lose Info")]
    [SerializeField] private Image[] loseLankImage = null;
    [SerializeField] private TMP_Text[] loseProfileName = null;
    [SerializeField] private Image[] loseFrameImage = null;

    [Header("Score Info")]
    [SerializeField] private TMP_Text[] rankProfileScore = null; // speed, memeory mode

    [HideInInspector] public bool IsSelect = false; // multi profile select 시 true

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
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                break;
            case GameMode.Memory:

                break;
            case GameMode.Multi:
                Ranking.Instance.LoadVersusResult(winProfileName, loseProfileName, winLankImage, loseLankImage);
                for (int i = 0; i < winProfileName.Length; i++)
                {
                    // 기록이 없을 때 Frame Image 꺼두기
                    if (winProfileName[i].text.Equals(""))
                    {
                        winFrameImage[i].enabled = false;
                        loseFrameImage[i].enabled = false;
                    }
                    else
                    {
                        winFrameImage[i].enabled = true;
                        loseFrameImage[i].enabled = true;
                    }
                }
                break;
        }
    }
}
