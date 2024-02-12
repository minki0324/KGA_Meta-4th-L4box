using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    #region Unity Callback
    #endregion

    #region Other Method
    public void Mode_Btn(string mode)
    {
        switch(mode)
        {
            case "PushPush":
                GameManager.instance.gameMode = GameMode.PushPush;
                break;
            case "Speed":
                GameManager.instance.gameMode = GameMode.Speed;
                break;
            case "Memory":
                GameManager.instance.gameMode = GameMode.Memory;
                break;
        }
    }

    

    public void Profile_Btn_Clicked()
    {
        _profilePanel.SetActive(true);
        _gameModePanel.SetActive(false);
        _buttonPanel.SetActive(false);
        /*AudioManager123.instance.SetAudioClip_SFX(0);*/
    }

    public void OptionBtn_Clicked()
    {
        _optionPanel.SetActive(true);
        AudioManager123.instance.SetAudioClip_SFX(0);
    }

    public void CollectionBtn_Clicked()
    {
        //네트워크 씬으로 이동 + 필요한 함수 호출해주세요 :)
        
        Debug.Log("네트워크 씬으로 넘어가기");
        AudioManager123.instance.SetAudioClip_SFX(0);
    }

    public void Mode2PBtn_Clicked()
    {
        AudioManager123.instance.SetAudioClip_SFX(0);
    }
    
    public void HomeBtn_Clicked()
    {
        Application.OpenURL("https://www.l4box.com/");
         AudioManager123.instance.SetAudioClip_SFX(0);
    }
    #endregion
}
