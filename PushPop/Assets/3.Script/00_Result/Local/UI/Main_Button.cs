using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//MainCanvas에 들어갈 스크립트
public class Main_Button : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button profileBtn;
    [SerializeField] private Button profileReturnBtn;
    [SerializeField] private Button homeBtn;        
    [SerializeField] private Button networkBtn;
    [SerializeField] private Button multiModeBtn;
    [SerializeField] private Button pushModeBtn;
    [SerializeField] private Button speedModeBtn;
    [SerializeField] private Button memoryModeBtn;

    [Header("Panel")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject networkPanel;
    [SerializeField] private GameObject timesetPanel;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject gameModePanel;
  

    [Header("모드별 캔버스")]

    [SerializeField] private Help_Canvas help_Cavas;

    #region Unity Callback
    private void OnEnable()
    {
        Enable_Button();
        help_Cavas.gameObject.SetActive(false);
        GameManager.Instance.shutdownTimer = 0;
    }
    #endregion

    #region Other Method
    public void PushPushBtn_Clicked()
    { // GameMode PushPushBtn 연동 Method
        timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(0);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void SpeedBtn_Clicked()
    { // GameMOde SpeedBtn 연동 Method
        timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(1);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void MemoryBtn_Clicked()
    { // GameMode Memory 연동 Method
        timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(2);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void BombBtn_Clicked()
    { // GameMode MultiBtn 연동 Method
        timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting((int)Mode.Bomb);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void Profile_Btn_Clicked()
    { // MainLobby ProfileIconBtn 연동 Method
        NewProfileCanvas profile = profilePanel.GetComponent<NewProfileCanvas>();
        profilePanel.SetActive(true);
        profileReturnBtn.gameObject.SetActive(true);
        ProfileManager.Instance.PrintProfileList(profile.profileParent, ProfileManager.Instance.ProfileIndex1P);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void ProfileReturnBtn_Clicked()
    { // Profile Return Btn
        profilePanel.SetActive(false);
        profileReturnBtn.gameObject.SetActive(false);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void OptionBtn_Clicked()
    { // MainLobby OptionBtn 연동 Method
        optionPanel.SetActive(true);
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }

    public void CollectionBtn_Clicked()
    { // MainLobby NetworkBtn 연동 Method
        //네트워크 씬으로 이동 + 필요한 함수 호출해주세요 :)
        
        Debug.Log("네트워크 씬으로 넘어가기");
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }
    
    public void HomeBtn_Clicked()
    { // MainLobby HomeBtn 연동 Method
        Application.OpenURL("https://www.l4box.com/");
        AudioManager.instance.SetCommonAudioClip_SFX(3);
    }


    public void Enable_Button()
    {
        optionBtn.interactable = true;
        profileBtn.interactable = true;
        homeBtn.interactable = true;
        networkBtn.interactable = true;
        multiModeBtn.interactable = true;
        pushModeBtn.interactable = true;
        speedModeBtn.interactable = true;
        memoryModeBtn.interactable = true;
    }

    public void Disable_Button()
    {
        optionBtn.interactable = false;
        profileBtn.interactable = false;
        homeBtn.interactable = false;
        networkBtn.interactable = false;
        multiModeBtn.interactable = false;
        pushModeBtn.interactable = false;
        speedModeBtn.interactable = false;
        memoryModeBtn.interactable = false;
    }
    #endregion
}
