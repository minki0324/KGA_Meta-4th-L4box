using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//MainCanvas에 들어갈 스크립트
public class Main_Button : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _optionBtn;
    [SerializeField] private Button _profileBtn;
    [SerializeField] private Button _homeBtn;        
    [SerializeField] private Button _collectionBtn;
    [SerializeField] private Button _mode2PBtn;
    [SerializeField] private Button _pushModeBtn;
    [SerializeField] private Button _speedModeBtn;
    [SerializeField] private Button _memoryModeBtn;

    [Header("Panel")]
    [SerializeField] private GameObject _profilePanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _collectionPanel;
    [SerializeField] private GameObject _timesetPanel;
    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private GameObject _gameModePanel;
  

    [Header("모드별 캔버스")]
    [SerializeField] private Canvas pushMode_Canvas;
    [SerializeField] private Canvas speedMode_Canvas;
    [SerializeField] private Canvas memoryMode_Canvas;
    [SerializeField] private Canvas Background_Canvas;  //도움말 & 뒤로가기 버튼 캔버스
    [SerializeField] private Help_Canvas help_Cavas;

    #region Unity Callback
    private void OnEnable()
    {
        Enable_Button();
    }
    #endregion

    #region Other Method

    //푸시푸시 모드 패널(버튼) 클릭 시 호출될 함수
    public void PushPushBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.PushPush;
        pushMode_Canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    //스피드 모드 패널(버튼) 클릭 시 호출될 함수
    public void SpeedBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.Speed;
        _timesetPanel.SetActive(true);
        //Disable_Button();
    }


    //메모리 모드 패널(버튼) 클릭 시 호출될 함수
    public void MemoryBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.Memory;
        _timesetPanel.SetActive(true);
       // Disable_Button();
    }


    //프로필 아이콘 클릭 시 호출
    public void Profile_Btn_Clicked()
    {
        _profilePanel.SetActive(true);
        _gameModePanel.SetActive(false);
        _buttonPanel.SetActive(false);
        /*AudioManager123.instance.SetAudioClip_SFX(0);*/
    }

    //환경설정 버튼 클릭 시 호출
    public void OptionBtn_Clicked()
    {
        _optionPanel.SetActive(true);
        AudioManager123.instance.SetAudioClip_SFX(0);
    }


    //소통방(네트워크) 버튼 클릭 시 호출
    public void CollectionBtn_Clicked()
    {
        //네트워크 씬으로 이동 + 필요한 함수 호출해주세요 :)
        
        Debug.Log("네트워크 씬으로 넘어가기");
        AudioManager123.instance.SetAudioClip_SFX(0);
    }

    //2인모드 버튼 클릭 시 호출
    public void Mode2PBtn_Clicked()
    {
        AudioManager123.instance.SetAudioClip_SFX(0);
    }
    
    //홈 아이콘 클릭 시 호출 - L4Box 홈페이지로 켜기
    public void HomeBtn_Clicked()
    {
        Application.OpenURL("https://www.l4box.com/");
        AudioManager123.instance.SetAudioClip_SFX(0);
    }


    public void Enable_Button()
    {
        _optionBtn.enabled = true;
        _profileBtn.enabled = true;
        _homeBtn.enabled = true;
        _collectionBtn.enabled = true;
        _mode2PBtn.enabled = true;
        _pushModeBtn.enabled = true;
        _speedModeBtn.enabled = true;
        _memoryModeBtn.enabled = true;

    }

    public void Disable_Button()
    {
        _optionBtn.enabled = false;
        _profileBtn.enabled = false;
        _homeBtn.enabled = false;
        _collectionBtn.enabled = false;
        _mode2PBtn.enabled = false;
        _pushModeBtn.enabled = false;
        _speedModeBtn.enabled = false;
        _memoryModeBtn.enabled = false;
    }
    #endregion
}
