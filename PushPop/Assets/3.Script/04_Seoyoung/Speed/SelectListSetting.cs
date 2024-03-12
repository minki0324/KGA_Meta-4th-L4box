using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectListSetting : MonoBehaviour
{ // category and select list 관리, pushpush, speed mode에서 사용
    public Sprite BoardIcon { get; private set; }
    public int CurrentIcon = 0; // 선택한 아이콘

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
    [SerializeField] private Button previousButton = null;
    [SerializeField] private Button nextButton = null;

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
    private void CategoryIconSetting()
    {

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
            selectIconInfo.IconText.text = DataManager.instance.iconDict[int.Parse(selectList[i].name)]; // sprite name key dictionary
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

        CurrentIcon = _selectIcon;
        selectIconImage.sprite = selectIconInfo.IconImage.sprite;
        selectIconText.text = selectIconInfo.IconText.text;
        BoardIcon = selectIconImage.sprite;

        Ready.SetActive(true);
    }

    public void NextButton()
    { // 다음 버튼
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        if (CurrentIcon < iconButtonList.Count - 1)
        {
            CurrentIcon++;

            SelectIconInfo selectIconInfo = iconButtonList[CurrentIcon].transform.parent.GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
        }
    }

    public void PreviousButton()
    { // 이전 버튼
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        if (CurrentIcon > 0)
        {
            CurrentIcon--;

            SelectIconInfo selectIconInfo = iconButtonList[CurrentIcon].transform.parent.GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
        }
    }
    #endregion
}
