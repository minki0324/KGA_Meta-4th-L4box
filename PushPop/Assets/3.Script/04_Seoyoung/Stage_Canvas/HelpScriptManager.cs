using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Bg_Canvas�� ���� ��ũ��Ʈ

public class HelpScriptManager : MonoBehaviour
{ // Help Panel Script
    [SerializeField] private Image helpImage;
    [SerializeField] private TMP_Text helpDescription;
    [SerializeField] private TMP_Text pageText;

    [SerializeField] private List<Sprite> pushpushList;
    [SerializeField] private List<Sprite> speedList;
    [SerializeField] private List<Sprite> memoryList;
    [SerializeField] private List<Sprite> multiList;
    private List<Sprite> selectList = null;

    //���� ������ ��ȣ
    private int maxPage = 0;
    private int currentPage = 0;

    private void OnEnable()
    {
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
                selectList = pushpushList;
                break;
            case GameMode.Speed:
                selectList = speedList;
                break;
            case GameMode.Memory:
                selectList = memoryList;
                break;
            case GameMode.Multi:
                selectList = multiList;
                break;
        }
        HelpSetting();
        HelpWindow();
    }

    private void OnDisable()
    {
        Init();
    }

    public void HelpNextButton()
    { //���� â�� Next(����) ��ư ������ ȣ��� �޼ҵ�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (currentPage >= maxPage - 1)
        {
            return;
        }
        currentPage++;
        HelpWindow();
    }

    public void HelpPreviousButton()
    { //���� â�� Previous(����) ��ư ������ ȣ��� �޼ҵ�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (currentPage <= 0)
        {
            return;
        }
        currentPage--;
        HelpWindow();
    }

    private void Init()
    {
        currentPage = 0;
    }

    private void HelpSetting()
    {
        maxPage = DataManager.Instance.helpScripts_List[(int)GameManager.Instance.GameMode].script.Count;
        for (int i = 0; i < DataManager.Instance.helpScripts_List[(int)GameManager.Instance.GameMode].script.Count; i++)
        {
            if (currentPage.Equals(DataManager.Instance.helpScripts_List[(int)GameManager.Instance.GameMode].script[i].pageNum))
            {
                helpDescription.text = $"{currentPage + 1}.{DataManager.Instance.helpScripts_List[(int)GameManager.Instance.GameMode].script[i].content}";
                helpImage.sprite = selectList[i];
            }
        }
    }

    private void HelpWindow()
    { // ���� ��ư ������ ������ ��ũ��Ʈ
        //���� �ؽ�Ʈ�� json ���Ͽ�
        //���� �̹����� ��� ����Ʈ�� ���������� �־��ּ��� :)
        helpImage.sprite = selectList[currentPage];
        helpDescription.text = $"{currentPage + 1}.{DataManager.Instance.helpScripts_List[(int)GameManager.Instance.GameMode].script[currentPage].content}";
        pageText.text = $"{currentPage + 1}/{maxPage}";
    }
}
