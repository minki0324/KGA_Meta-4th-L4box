using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectListSetting : MonoBehaviour
{ // category and select list ����, pushpush, speed mode���� ���
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
    { // list �ʱ�ȭ
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
                categorySelectTitle.text = "����";
                selectCategory.text = "����";
                selectList = easyIconList;
                break;
            case Difficulty.Normal:
                categorySelectTitle.text = "����";
                selectCategory.text = "����";
                selectList = normalIconList;
                break;
            case Difficulty.Hard:
                categorySelectTitle.text = "�����";
                selectCategory.text = "�����";
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
    { // ������ ������ �� ȣ��Ǵ� �Լ�, Ready Active true
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        SelectIconInfo selectIconInfo = _selectIconButton.transform.parent.GetComponent<SelectIconInfo>();

        selectIconImage.sprite = selectIconInfo.IconImage.sprite;
        selectIconText.text = selectIconInfo.IconText.text;
        BoardIcon = selectIconImage.sprite;
        GameManager.Instance.CurrentIcon = _selectIcon;
        GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

        Ready.SetActive(true);
    }

    public void NextButton()
    { // ���� ��ư
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        if (GameManager.Instance.CurrentIcon < iconButtonList.Count - 1)
        {
            GameManager.Instance.CurrentIcon++;

            SelectIconInfo selectIconInfo = iconButtonList[GameManager.Instance.CurrentIcon].transform.parent.GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
            GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

            readyProfileSetting.PlayerInfoSetting();
            readyProfileSetting.RankInfoSetting();
        }
    }

    public void PreviousButton()
    { // ���� ��ư
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        
        if (GameManager.Instance.CurrentIcon > 0)
        {
            GameManager.Instance.CurrentIcon--;

            SelectIconInfo selectIconInfo = iconButtonList[GameManager.Instance.CurrentIcon].transform.parent.GetComponent<SelectIconInfo>();
            selectIconImage.sprite = selectIconInfo.IconImage.sprite;
            selectIconText.text = selectIconInfo.IconText.text;
            BoardIcon = selectIconImage.sprite;
            GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

            readyProfileSetting.PlayerInfoSetting();
            readyProfileSetting.RankInfoSetting();
        }
    }
    #endregion
}
