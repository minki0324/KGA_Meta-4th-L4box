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
    [HideInInspector] public bool IsSelect = false; // multi profile select �� true

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

    private void PlayerInfoSetting()
    { // ���� ������
        // ������ ���� 1P
        profileImage1P.sprite = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileImage;
        profileName1P.text = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName;


        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                PlayerScore = Ranking.Instance.LoadPersonalTimer(0); // todo.. �ٲ��ٰ�
                int min = PlayerScore % 60;
                int sec = PlayerScore / 60;
                profileScore.text = $"{string.Format("{0:00}", min)}:{ string.Format("{0:00}", sec)}";
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

    private void RankInfoSetting()
    { // ��ŷ ����
        List<Image[]> frameImage = new List<Image[]>(); // multi ������ List�� ����
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Speed:
                break;
            case GameMode.Memory:
                frameImage.Add(rankImage);
                Ranking.Instance.LoadScore(rankScoreText, rankImage, rankProfileName);
                break;
            case GameMode.Multi:
                frameImage.Add(winLankImage); // frameImage 0
                frameImage.Add(loseLankImage); // frameImgae 1
                Ranking.Instance.LoadVersusResult(winProfileName, loseProfileName, winLankImage, loseLankImage);
                break;
        }

        for (int i = 0; i < frameImage.Count; i++)
        { // multi mode count = 2, other mode = 1
            for (int j = 0; j < frameImage[i].Length; j++)
            { // ����� ���� �� Frame Image ���α�
                if (frameImage[i][j].sprite.Equals(ProfileManager.Instance.NoneBackground))
                {
                    frameImage[i][j].enabled = false;
                }
                else
                {
                    frameImage[i][j].enabled = true;
                }
            }
        }

    }
}
