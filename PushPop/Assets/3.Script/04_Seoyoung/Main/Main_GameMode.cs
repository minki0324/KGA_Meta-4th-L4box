using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_GameMode : MonoBehaviour
{
    [Header("게임 모드 패널(버튼)")]
    [SerializeField]
    private Button pushMode_Btn;

    [SerializeField]
    private Button speedMode_Btn;

    [SerializeField]
    private Button memoryMode_Btn;


    [Header("시간 설정 패널")]
    [SerializeField]
    private GameObject timeSet_Panel;



    private void Start()
    {

        Init();
        timeSet_Panel.SetActive(false);
    }

    private void Init()
    {
        pushMode_Btn.onClick.AddListener(PushModeBtn_Clicked);
        speedMode_Btn.onClick.AddListener(SpeedModeBtn_Clicked);
        memoryMode_Btn.onClick.AddListener(MemoryModeBtn_Clicked);
    }

    private void PushModeBtn_Clicked()
    {
        Debug.Log("ㅎㅎ ㅅㅄㅄㅂ");
        GameManger_2.instance.gameMode = GameMode.PushPush;
        timeSet_Panel.gameObject.SetActive(true);
    }

    private void SpeedModeBtn_Clicked()
    {
        GameManger_2.instance.gameMode = GameMode.Speed;
        timeSet_Panel.gameObject.SetActive(true);
    }

    private void MemoryModeBtn_Clicked()
    {
        GameManger_2.instance.gameMode = GameMode.Memory;
        timeSet_Panel.gameObject.SetActive(true);
    }


}
