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
    [SerializeField] private ScrollRect selectRect;

    [Header("Ready (Selected List Panel)")]
    public GameObject Ready = null;
    [SerializeField] private ReadyProfileSetting readyProfileSetting = null;
    [SerializeField] private TMP_Text selectCategory = null;
    [SerializeField] private Image selectIconImage = null;
    [SerializeField] private TMP_Text selectIconText = null;

    [Header("PushPush Icon List")] // sprite atlas�� �ٲ� �� �ִٸ�... todo
    private int maxPage = 0; // category list count
    private int currentPage = 1; // category list current count
    [SerializeField] private TMP_Text pageText = null;
    [SerializeField] private List<Sprite> categoryIconList = null;
    private List<Sprite> selectIcon = null;
    public List<Sprite> IconList10 = null;
    public List<Sprite> IconList11 = null;
    public List<Sprite> IconList12 = null;
    public List<Sprite> IconList13 = null;
    public List<Sprite> IconList14 = null;
    public List<Sprite> IconList15 = null;
    public List<Sprite> IconList16 = null;
    public List<Sprite> IconList17 = null;
    public List<Sprite> IconList18 = null;
    public List<Sprite> IconList19 = null;

    [Header("Speed Icon List")]
    [SerializeField] private List<Sprite> easyIconList = null;
    [SerializeField] private List<Sprite> normalIconList = null;
    [SerializeField] private List<Sprite> hardIconList = null;

    private List<Button> iconButtonList = null; // select�� icon list

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
    { // list �ʱ�ȭ
        selectRect.normalizedPosition = new Vector2(1f, 1f);
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

        List<Sprite> selectList = categoryIconList;
        iconButtonList = new List<Button>();

        for (int i = 0; i < selectList.Count; i++)
        {
            GameObject selectIcon = Instantiate(selectIconObject, selectCategoryScrollView.transform); // category prefab
            SelectIconInfo selectIconInfo = selectIcon.transform.GetComponent<SelectIconInfo>();

            iconButtonList.Add(selectIconInfo.IconButton); // button list
            selectIconInfo.IconImage.sprite = selectList[i]; // sprite change
            selectIconInfo.IconText.text = DataManager.Instance.CategoryDict[int.Parse(selectList[i].name)]; // sprite name key dictionary, �ѱ� �ؽ�Ʈ �������
        }

        for (int i = 0; i < iconButtonList.Count; i++)
        {
            int index = i;
            Sprite sprite = iconButtonList[i].transform.GetComponent<Image>().sprite;
            iconButtonList[i].onClick.AddListener(delegate { CategoryIconButtonClick(int.Parse(sprite.name)); });
        }
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
            selectIconInfo.IconText.text = DataManager.Instance.IconDict[int.Parse(selectList[i].name)]; // sprite name key dictionary, �ѱ� �ؽ�Ʈ �������
        }

        for (int i = 0; i < iconButtonList.Count; i++)
        {
            int index = i;
            iconButtonList[i].onClick.AddListener(delegate { CategoryIconButtonClick(iconButtonList[index].gameObject, index); });
        }
    }
    #endregion
    #region Selected List Panel (Ready)
    private void CategoryIconButtonClick(int _selectList)
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        selectIcon = this.GetType().GetField($"IconList{_selectList}").GetValue(this) as List<Sprite>;
        categorySelectTitle.text = DataManager.Instance.CategoryDict[_selectList];
        selectIconImage.sprite = selectIcon[0];
        selectIconText.text = DataManager.Instance.IconDict[int.Parse(selectIcon[0].name)];
        BoardIcon = selectIconImage.sprite;
        GameManager.Instance.CurrentIcon = 0;
        GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);

        currentPage = 1;
        maxPage = selectIcon.Count;
        pageText.text = $"{currentPage}/{maxPage}";

        Ready.SetActive(true);
    }

    private void CategoryIconButtonClick(GameObject _selectIconButton, int _selectIcon)
    { // ������ ������ �� ȣ��Ǵ� �Լ�, Ready Active true
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
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
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        switch (GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
                if (GameManager.Instance.CurrentIcon < selectIcon.Count - 1)
                {
                    GameManager.Instance.CurrentIcon++;
                    currentPage++;
                    pageText.text = $"{currentPage}/{maxPage}";

                    selectIconImage.sprite = selectIcon[GameManager.Instance.CurrentIcon];
                    selectIconText.text = DataManager.Instance.IconDict[int.Parse(selectIcon[GameManager.Instance.CurrentIcon].name)];
                    BoardIcon = selectIconImage.sprite;

                    GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);
                }
                break;
            case GameMode.Speed:
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
                break;
        }
    }

    public void PreviousButton()
    { // ���� ��ư
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        switch (GameManager.Instance.GameMode)
        {
            case GameMode.PushPush:
                if (GameManager.Instance.CurrentIcon > 0)
                {
                    GameManager.Instance.CurrentIcon--;
                    currentPage--;
                    pageText.text = $"{currentPage}/{maxPage}";

                    selectIconImage.sprite = selectIcon[GameManager.Instance.CurrentIcon];
                    selectIconText.text = DataManager.Instance.IconDict[int.Parse(selectIcon[GameManager.Instance.CurrentIcon].name)];
                    BoardIcon = selectIconImage.sprite;

                    GameManager.Instance.CurrentIconName = int.Parse(BoardIcon.name);
                }
                break;
            case GameMode.Speed:
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
                break;
        }
    }
    #endregion
}
