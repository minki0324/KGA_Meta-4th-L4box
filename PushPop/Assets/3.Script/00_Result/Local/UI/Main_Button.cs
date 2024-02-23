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

    [SerializeField] private Help_Canvas help_Cavas;

    #region Unity Callback
    private void OnEnable()
    {
        Enable_Button();
        help_Cavas.gameObject.SetActive(false);
    }
    #endregion

    #region Other Method

    //Ǫ��Ǫ�� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void PushPushBtn_Clicked()
    {
        _timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(0);
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
      
    }


    //���ǵ� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void SpeedBtn_Clicked()
    {
        _timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(1);
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
        
    }


    //�޸� ��� �г�(��ư) Ŭ�� �� ȣ��� �Լ�
    public void MemoryBtn_Clicked()
    {
        _timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(2);
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
     
    }

    //2�θ�� ��ư Ŭ�� �� ȣ��
    public void BombBtn_Clicked()
    {
        _timesetPanel.SetActive(true);
        GameManager.Instance.GameModeSetting(0);
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
    }


    //������ ������ Ŭ�� �� ȣ��
    public void Profile_Btn_Clicked()
    {
        _profilePanel.SetActive(true);
        _gameModePanel.SetActive(false);
        _buttonPanel.SetActive(false);
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
    }

    //ȯ�漳�� ��ư Ŭ�� �� ȣ��
    public void OptionBtn_Clicked()
    {
        _optionPanel.SetActive(true);
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
    }


    //�����(��Ʈ��ũ) ��ư Ŭ�� �� ȣ��
    public void CollectionBtn_Clicked()
    {
        //��Ʈ��ũ ������ �̵� + �ʿ��� �Լ� ȣ�����ּ��� :)
        
        Debug.Log("��Ʈ��ũ ������ �Ѿ��");
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
    }


    
    //Ȩ ������ Ŭ�� �� ȣ�� - L4Box Ȩ�������� �ѱ�
    public void HomeBtn_Clicked()
    {
        Application.OpenURL("https://www.l4box.com/");
        AudioManager123.instance.SetCommonAudioClip_SFX(3);
    }


    public void Enable_Button()
    {
        //_optionBtn.enabled = true;
        //_profileBtn.enabled = true;
        //_homeBtn.enabled = true;
        //_collectionBtn.enabled = true;
        //_mode2PBtn.enabled = true;
        //_pushModeBtn.enabled = true;
        //_speedModeBtn.enabled = true;
        //_memoryModeBtn.enabled = true;

        _optionBtn.interactable = true;
        _profileBtn.interactable = true;
        _homeBtn.interactable = true;
        _collectionBtn.interactable = true;
        _mode2PBtn.interactable = true;
        _pushModeBtn.interactable = true;
        _speedModeBtn.interactable = true;
        _memoryModeBtn.interactable = true;

    }

    public void Disable_Button()
    {
        //_optionBtn.enabled = false;
        //_profileBtn.enabled = false;
        //_homeBtn.enabled = false;
        //_collectionBtn.enabled = false;
        //_mode2PBtn.enabled = false;
        //_pushModeBtn.enabled = false;
        //_speedModeBtn.enabled = false;
        //_memoryModeBtn.enabled = false;


        _optionBtn.interactable = false;
        _profileBtn.interactable = false;
        _homeBtn.interactable = false;
        _collectionBtn.interactable = false;
        _mode2PBtn.interactable = false;
        _pushModeBtn.interactable = false;
        _speedModeBtn.interactable = false;
        _memoryModeBtn.interactable = false;
    }
    #endregion
}
