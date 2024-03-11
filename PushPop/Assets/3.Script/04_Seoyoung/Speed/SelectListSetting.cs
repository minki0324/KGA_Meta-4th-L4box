using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectListSetting : MonoBehaviour
{ // category and select list 관리, speed mode에서 사용
    public Sprite BoardIcon { get; private set; }
    public int CurrentIcon = 0; // 선택한 아이콘

    [Header("Canvas")]
    [SerializeField] private SpeedCanvas speedCanvas = null;

    [Header("Prefab")]
    [SerializeField] private GameObject selectIconObject = null;

    [Header("Category Select Panel")]
    [SerializeField] private TMP_Text categorySelectTitle = null;
    [SerializeField] private GameObject selectCategoryScrollView = null;

    [Header("Ready (Selected List Panel)")]
    public GameObject Ready = null;
    [SerializeField] private TMP_Text selectCategory = null;
    [SerializeField] private Image selectIconImage = null;
    [SerializeField] private TMP_Text selectIconText = null;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Difficulty Icon List")]
    [SerializeField] private List<Sprite> easyIconList = null;
    [SerializeField] private List<Sprite> normalIconList = null;
    [SerializeField] private List<Sprite> hardIconList = null;
    private List<Button> iconButtonList = null;

    private void OnEnable()
    {
        DifficultyBoardSetting();
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
            GameObject selectIcon = Instantiate(selectIconObject, selectCategoryScrollView.transform);
            SelectIconInfo selectIconInfo = selectIcon.transform.GetComponent<SelectIconInfo>();

            iconButtonList.Add(selectIconInfo.IconButton); // button list
            selectIconInfo.IconImage.sprite = selectList[i]; // sprite change
            selectIconInfo.IconText.text = DataManager.instance.iconDict[int.Parse(selectList[i].name)]; // sprite name key dictionary
        }

        for (int i = 0; i < iconButtonList.Count; i++)
        {
            int temp = i; // ?
            iconButtonList[temp].onClick.AddListener(delegate { CategoryIconButtonClick(iconButtonList[temp].gameObject, temp); });
        }
    }
    #endregion
    #region Selected List Panel (Ready)
    public void CategoryIconButtonClick(GameObject _selectIconButton, int _selectIcon)
    { // 아이콘 눌렀을 때 호출되는 함수, Ready Active true
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectIconInfo selectIconInfo = _selectIconButton.GetComponent<SelectIconInfo>();

        CurrentIcon = _selectIcon;
        selectIconImage.sprite = selectIconInfo.IconImage.sprite;
        selectIconText.text = selectIconInfo.IconText.text;
        BoardIcon = selectIconImage.sprite;

        Ready.SetActive(true);
    }

    public void NextButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (CurrentIcon >= iconButtonList.Count - 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            previousButton.interactable = true;
            CurrentIcon++;

            SelectIconInfo selectIconInfo = iconButtonList[CurrentIcon].GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
        }
    }

    public void PreviousButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (CurrentIcon <= 0)
        {
            previousButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
            CurrentIcon--;

            SelectIconInfo selectIconInfo = iconButtonList[CurrentIcon].GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
        }
    }
    #endregion
}
