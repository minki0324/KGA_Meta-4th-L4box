using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    #region Unity Callback
    #endregion

    #region Other Method
    public void Mode_Btn(string mode)
    {
        switch(mode)
        {
            case "PushPush":
                GameManager.instance.gameMode = GameMode.PushPush;
                TimeSet_Panel.gameObject.SetActive(true);
                break;
            case "Speed":
                GameManager.instance.gameMode = GameMode.Speed;
                TimeSet_Panel.gameObject.SetActive(true);
                break;
            case "Memory":
                GameManager.instance.gameMode = GameMode.Memory;
                TimeSet_Panel.gameObject.SetActive(true);
                break;
        }
    }

    public void Profile_Btn_Clicked()
    {
        Profile_Panel.SetActive(true);
    }

    public void OptionBtn_Clicked()
    {
        Option_Panel.SetActive(true);
    }

    public void CollectionBtn_Clicked()
    {
        //네트워크 씬으로 이동 + 필요한 함수 호출해주세요 :)
        
        Debug.Log("네트워크 씬으로 넘어가기");
    }

    public void Mode2PBtn_Clicked()
    {
       
    }
    
    public void HomeBtn_Clicked()
    {
        Application.OpenURL("https://www.l4box.com/");
    }
    #endregion
}
