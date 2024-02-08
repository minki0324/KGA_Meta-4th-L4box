using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//MainCanvas에 들어갈 스크립트
public class Main_Button : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button option_Btn;
    [SerializeField] private Button profile_Btn;
    [SerializeField] private Button home_Btn;        
    [SerializeField] private Button collection_Btn;
    [SerializeField] private Button mode2P_Btn;
    [SerializeField] private Button pushMode_Btn;
    [SerializeField] private Button speedMode_Btn;
    [SerializeField] private Button memoryMode_Btn;

    [Header("Panel")]
    [SerializeField] private GameObject Profile_Panel;
    [SerializeField] private GameObject Option_Panel;
    [SerializeField] private GameObject Collection_Panel;
    [SerializeField] private GameObject TimeSet_Panel;


    [Header("모드별 캔버스")]
    [SerializeField] private Canvas pushMode_Canvas;
    [SerializeField] private Canvas speedMode_Canvas;
    [SerializeField] private Canvas memoryMode_Canvas;
    [SerializeField] private Canvas Background_Canvas;  //도움말 & 뒤로가기 버튼 캔버스


    #region Unity Callback
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
        TimeSet_Panel.SetActive(true);
    }


    //메모리 모드 패널(버튼) 클릭 시 호출될 함수
    public void MemoryBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.Memory;
        TimeSet_Panel.SetActive(true);
    }


    //프로필 아이콘 클릭 시 호출
    public void Profile_Btn_Clicked()
    {
        Profile_Panel.SetActive(true);
        AudioManager123.instance.SetAudioClip_SFX(0);
    }

    //환경설정 버튼 클릭 시 호출
    public void OptionBtn_Clicked()
    {
        Option_Panel.SetActive(true);
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
    #endregion
}
