using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectListSetting : MonoBehaviour
{ // category and select list 관리, pushpush, speed mode에서 사용
    public Sprite BoardIcon { get; private set; }

    [Header("Prefab")]
    [SerializeField] private GameObject selectIconObject = null;

    [Header("Category Select Panel")]
    [SerializeField] private TMP_Text categorySelectTitle = null;
    [SerializeField] private GameObject selectCategoryScrollView = null;

    [Header("Ready (Selected List Panel)")]
    public GameObject Ready = null;
    [SerializeField] ReadyProfileSetting readyProfileSetting = null;
    [SerializeField] private TMP_Text selectCategory = null;
    [SerializeField] private Image selectIconImage = null;
    [SerializeField] private TMP_Text selectIconText = null;
    [SerializeField] private Button previousButton = null;
    [SerializeField] private Button nextButton = null;

    [Header("Difficulty Icon List")] // spriteAtlas에서 가져올 것
    [SerializeField] private List<Sprite> easyIconList = null;
    [SerializeField] private List<Sprite> normalIconList = null;
    [SerializeField] private List<Sprite> hardIconList = null;
    private List<Button> iconButtonList = null;

    [Header("Only PushPush Mode")]
    [SerializeField] private TMP_Text pageText = null;
    private int maxPage = 0; // category list count
    private int currentPage = 1; // category list current count

    private void OnEnable()
    {
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
                CategoryIconSetting();
                break;
            case GameMode.Speed:
                DifficultyBoardSetting();
                break;
        }
    }
    private void OnDisable()
    {
        Init();
    }

    private void Init()
    { // list 초기화
        for (int i = 0; i < selectCategoryScrollView.transform.childCount; i++)
        {
            Destroy(selectCategoryScrollView.transform.GetChild(i).gameObject);
        }
        iconButtonList.Clear();
    }

    #region Difficulty Select, Category Select Panel
    private void CategoryIconSetting()
    {
        maxPage = 1; 
        pageText.text = $"{currentPage}/{maxPage}";

        List<Sprite> selectList = null;
        iconButtonList = new List<Button>();

        for (int i = 0; i < selectList.Count; i++)
        {
            GameObject selectIcon = Instantiate(selectIconObject, selectCategoryScrollView.transform); // category prefab
            SelectIconInfo selectIconInfo = selectIcon.transform.GetComponent<SelectIconInfo>();

            iconButtonList.Add(selectIconInfo.IconButton); // button list
            selectIconInfo.IconImage.sprite = selectList[i]; // sprite change
            selectIconInfo.IconText.text = DataManager.instance.categoryDict[int.Parse(selectList[i].name)]; // sprite name key dictionary, 한글 텍스트 갖고오기
        }

        for (int i = 0; i < iconButtonList.Count; i++)
        {
            int index = i;
            //iconButtonList[i].onClick.AddListener(delegate { CategoryIconButtonClick(iconButtonList[index].gameObject, index); });
        }
    }

    private void DifficultyBoardSetting()
    { // Category Setting
        List<Sprite> selectList = null;
        iconButtonList = new List<Button>();

        switch (GameManager.Instance.Difficulty)
        {
            case Difficulty.Easy:
                categorySelectTitle.text = "쉬움";
                selectCategory.text = "쉬움";
                selectList = easyIconList;
                break;
            case Difficulty.Normal:
                categorySelectTitle.text = "보통";
                selectCategory.text = "보통";
                selectList = normalIconList;
                break;
            case Difficulty.Hard:
                categorySelectTitle.text = "어려움";
                selectCategory.text = "어려움";
                selectList = hardIconList;
                break;
        }

        for (int i = 0; i < selectList.Count; i++)
        {
            GameObject selectIcon = Instantiate(selectIconObject, selectCategoryScrollView.transform); // category prefab
            SelectIconInfo selectIconInfo = selectIcon.transform.GetComponent<SelectIconInfo>();

            iconButtonList.Add(selectIconInfo.IconButton); // button list
            selectIconInfo.IconImage.sprite = selectList[i]; // sprite change
            selectIconInfo.IconText.text = DataManager.instance.iconDict[int.Parse(selectList[i].name)]; // sprite name key dictionary, 한글 텍스트 갖고오기
        }

        for (int i = 0; i < iconButtonList.Count; i++)
        {
            int index = i;
            iconButtonList[i].onClick.AddListener(delegate { CategoryIconButtonClick(iconButtonList[index].gameObject, index); });
        }
    }
    #endregion
    #region Selected List Panel (Ready)
    private void CategoryIconButtonClick(GameObject _selectIconButton, int _selectIcon)
    { // 아이콘 눌렀을 때 호출되는 함수, Ready Active true
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectIconInfo selectIconInfo = _selectIconButton.transform.parent.GetComponent<SelectIconInfo>();

        selectIconImage.sprite = selectIconInfo.IconImage.sprite;
        selectIconText.text = selectIconInfo.IconText.text;
        BoardIcon = selectIconImage.sprite;
        GameManager.Instance.CurrentIcon = _selectIcon;
        GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

        if (GameManager.Instance.GameMode.Equals(GameMode.PushPush))
        {
            currentPage = 1;
            pageText.text = $"{currentPage}/{maxPage}";
        }

        Ready.SetActive(true);
    }

    public void NextButton()
    { // 다음 버튼
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        if (GameManager.Instance.CurrentIcon < iconButtonList.Count - 1)
        {
            GameManager.Instance.CurrentIcon++;

            SelectIconInfo selectIconInfo = iconButtonList[GameManager.Instance.CurrentIcon].transform.parent.GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
            GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

            switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    currentPage++;
                    pageText.text = $"{currentPage}/{maxPage}";
                    break;
                case GameMode.Speed:
                    readyProfileSetting.PlayerInfoSetting();
                    readyProfileSetting.RankInfoSetting();
                    break;
            }
        }
    }

    public void PreviousButton()
    { // 이전 버튼
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        if (GameManager.Instance.CurrentIcon > 0)
        {
            GameManager.Instance.CurrentIcon--;

            SelectIconInfo selectIconInfo = iconButtonList[GameManager.Instance.CurrentIcon].transform.parent.GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
            GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

            switch (GameManager.Instance.GameMode)
            {
                case GameMode.PushPush:
                    currentPage--;
                    pageText.text = $"{currentPage}/{maxPage}";
                    break;
                case GameMode.Speed:
                    readyProfileSetting.PlayerInfoSetting();
                    readyProfileSetting.RankInfoSetting();
                    break;
            }
        }
    }
    #endregion
}
