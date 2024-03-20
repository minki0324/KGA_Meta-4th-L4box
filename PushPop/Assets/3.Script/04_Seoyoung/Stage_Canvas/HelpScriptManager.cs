using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Bg_Canvas에 들어가는 스크립트

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

    //도움말 페이지 번호
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
    { //도움말 창의 Next(다음) 버튼 눌리면 호출될 메소드
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (currentPage >= maxPage - 1)
        {
            return;
        }
        currentPage++;
        HelpWindow();
    }

    public void HelpPreviousButton()
    { //도움말 창의 Previous(이전) 버튼 눌리면 호출될 메소드
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
    { // 헬프 버튼 누르면 나오는 스크립트
        //도움말 텍스트는 json 파일에
        //도움말 이미지의 경우 리스트에 순차적으로 넣어주세요 :)
        helpImage.sprite = selectList[currentPage];
        helpDescription.text = $"{currentPage + 1}.{DataManager.Instance.helpScripts_List[(int)GameManager.Instance.GameMode].script[currentPage].content}";
        pageText.text = $"{currentPage + 1}/{maxPage}";
    }
}
