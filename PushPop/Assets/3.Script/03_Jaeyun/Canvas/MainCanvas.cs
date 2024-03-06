using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private ProfileCanvas profileCanvas = null;
    [SerializeField] private MultiCanvas multiCanvas = null;

    [Header("Side Panel")]
    public GameObject TitleText = null;
    public GameObject OptionButton = null;
    public GameObject ProfileButton = null;
    public GameObject HomeButton = null;

    [Header("Profile")]
    public Image CaptureImage = null;

    [Header("GameMode Panel")]
    public GameObject PushpushButton = null;
    public GameObject SpeedButton = null;
    public GameObject MemoryButton = null;
    public GameObject MultiButton = null;
    public GameObject NetworkButton = null;

    [Header("Panel")]
    [SerializeField] private GameObject timeSettingPanel = null;
    [SerializeField] private GameObject optionPanel = null;

    public void GameModeButton()
    { // Time Set Start

    }
    public void ProfileImageButton()
    { // Profile 이미지 클릭 시 프로필 선택으로 돌아감
        profileCanvas.gameObject.SetActive(true);
        profileCanvas.Select.SetActive(true);

        TitleText.SetActive(false);
        OptionButton.SetActive(false);
        ProfileButton.SetActive(false);
        HomeButton.SetActive(false);
        PushpushButton.SetActive(false);
        SpeedButton.SetActive(false);
        MemoryButton.SetActive(false);
        MultiButton.SetActive(false);
        NetworkButton.SetActive(false);
    }
    public void MusicOptionButton()
    { // Music Option 버튼, 소리 조절
        optionPanel.SetActive(true);
    }
    public void HomeSiteButton()
    {

    }
}
