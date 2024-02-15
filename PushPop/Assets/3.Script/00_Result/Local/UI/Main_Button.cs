using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//MainCanvas�� �� ��ũ��Ʈ
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
  

    [Header("��庰 ĵ����")]
    [SerializeField] private Canvas pushMode_Canvas;
    [SerializeField] private Canvas speedMode_Canvas;
    [SerializeField] private Canvas memoryMode_Canvas;
    [SerializeField] private Canvas Background_Canvas;  //���� & �ڷΰ��� ��ư ĵ����
    [SerializeField] private Help_Canvas help_Cavas;

    #region Unity Callback
    private void OnEnable()
    {
        Enable_Button();
    }
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
        _timesetPanel.SetActive(true);
        //Disable_Button();
    }


    //�޸� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void MemoryBtn_Clicked()
    {
        GameManager.instance.gameMode = GameMode.Memory;
        _timesetPanel.SetActive(true);
       // Disable_Button();
    }


    //������ ������ Ŭ�� �� ȣ��
    public void Profile_Btn_Clicked()
    {
        _profilePanel.SetActive(true);
        _gameModePanel.SetActive(false);
        _buttonPanel.SetActive(false);
        /*AudioManager123.instance.SetAudioClip_SFX(0);*/
    }

    //ȯ�漳�� ��ư Ŭ�� �� ȣ��
    public void OptionBtn_Clicked()
    {
        _optionPanel.SetActive(true);
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
