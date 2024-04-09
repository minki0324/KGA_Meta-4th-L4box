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

    [Header("Profile 1P Info")]
    [SerializeField] private Image profileImage1P;
    [SerializeField] private TMP_Text profileName1P;
    [SerializeField] private TMP_Text profileScore; // score & time

    [Header("Only Speed, Memory Mode")]
    [Header("Rank Info")] // only memory mode
    [SerializeField] private Image[] rankFrameImage;
    [SerializeField] private Image[] rankImage;
    [SerializeField] private TMP_Text[] rankProfileName;
    [SerializeField] private TMP_Text[] rankScoreText;

    [Header("Only Multi Mode")]
    [Header("Profile 2P Info")] // only multi mode
    public Image ProfileImage2P;
    public TMP_Text ProfileName2P;

    [Header("Rank Win Info")] // only multi mode
    [SerializeField] private Image[] winLankImage;
    [SerializeField] private TMP_Text[] winProfileName;
    [SerializeField] private Image[] winFrameImage;

    [Header("Rank Lose Info")] // only multi mode
    [SerializeField] private Image[] loseLankImage;
    [SerializeField] private TMP_Text[] loseProfileName;
    [SerializeField] private Image[] loseFrameImage;

    private void OnEnable()
    {
        PlayerInfoSetting();
        RankInfoSetting();
    }

    public void PlayerInfoSetting()
    { // ���� ������
        // ������ ���� 1P
        profileImage1P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        profileName1P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;

        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                PlayerScore = Ranking.Instance.LoadPersonalTimer();
                float sec = PlayerScore % 60; // 60���� ���� ������ = ��
                float min = PlayerScore / 60;
                profileScore.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
                break;
            case GameMode.Memory:
                PlayerScore = Ranking.Instance.LoadPersonalScore();
                profileScore.text = $"{PlayerScore}��";
                break;
        }
    }

    public void SelectPlayerInfoSetting()
    { // 2P�� �������� ��
        ProfileImage2P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileImage;
        ProfileName2P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player2].profileName;
    }

    public void RankInfoSetting()
    { // ��ŷ ����
        List<Image[]> rankImage = new List<Image[]>(); // multi ������ List�� ����
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
            { // ����� ���� �� Frame Image ���α�
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
