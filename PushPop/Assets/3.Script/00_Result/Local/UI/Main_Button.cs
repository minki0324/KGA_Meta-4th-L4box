using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//MainCanvas�� �� ��ũ��Ʈ
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


    [Header("��庰 ĵ����")]
    [SerializeField] private Canvas pushMode_Canvas;
    [SerializeField] private Canvas speedMode_Canvas;
    [SerializeField] private Canvas memoryMode_Canvas;
    [SerializeField] private Canvas Background_Canvas;  //���� & �ڷΰ��� ��ư ĵ����


    #region Unity Callback
    #endregion

    #region Other Method

    //Ǫ��Ǫ�� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void PushPushBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.PushPush;
        pushMode_Canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    //���ǵ� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void SpeedBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.Speed;
        TimeSet_Panel.SetActive(true);
    }


    //�޸� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void MemoryBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.Memory;
        TimeSet_Panel.SetActive(true);
    }


    //������ ������ Ŭ�� �� ȣ��
    public void Profile_Btn_Clicked()
    {
        Profile_Panel.SetActive(true);
        AudioManager123.instance.SetAudioClip_SFX(0);
    }

    //ȯ�漳�� ��ư Ŭ�� �� ȣ��
    public void OptionBtn_Clicked()
    {
        Option_Panel.SetActive(true);
        AudioManager123.instance.SetAudioClip_SFX(0);
    }


    //�����(��Ʈ��ũ) ��ư Ŭ�� �� ȣ��
    public void CollectionBtn_Clicked()
    {
        //��Ʈ��ũ ������ �̵� + �ʿ��� �Լ� ȣ�����ּ��� :)
        
        Debug.Log("��Ʈ��ũ ������ �Ѿ��");
        AudioManager123.instance.SetAudioClip_SFX(0);
    }

    //2�θ�� ��ư Ŭ�� �� ȣ��
    public void Mode2PBtn_Clicked()
    {
        AudioManager123.instance.SetAudioClip_SFX(0);
    }
    
    //Ȩ ������ Ŭ�� �� ȣ�� - L4Box Ȩ�������� �ѱ�
    public void HomeBtn_Clicked()
    {
        Application.OpenURL("https://www.l4box.com/");
         AudioManager123.instance.SetAudioClip_SFX(0);
    }
    #endregion
}
