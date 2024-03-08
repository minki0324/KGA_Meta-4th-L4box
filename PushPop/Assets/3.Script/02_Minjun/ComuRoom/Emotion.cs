using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Emotion : NetworkBehaviour
{
    //단지 EMotion 들 저장하고 있기 위한 스크립트
    public static Emotion instance;
    //이모션 종류 인스펙터에서 추가하기
    public Canvas canvas;
    public Transform spawnPoint;
    public PlayerEmotionControl playerEmotion;
    private bool isExpend = true;
    [SerializeField] private GameObject emojiPanel;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Button ExpendBtn;
    private RectTransform emojiPanelRect;
    private Vector3 DirUp = new Vector3(0, 0, -90);
    private Vector3 DirDown = new Vector3(0, 0, 90);
    private Vector2 BasicSize = new Vector2(0, -15f);

    public Sprite[] sprites;
    public Button[] emojiBtns;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        emojiPanelRect =emojiPanel.GetComponent<RectTransform>();
        InitEmojiBtn();
    }
    [Client]
    public void onEmotion(int index)
    {
        //spriterenderer.sprite = emotions[index];
        playerEmotion.PlayEmotion(index);
    }

    public void ExpandEmojiPanel()
    {

        //        Vertical
        //Fixed Column Count / 4
        //veiwport 늘려주기 500
        //화살표 아래방향
        if (isExpend)
        {
            float size = 500f;
            emojiPanelRect.sizeDelta = new Vector2(emojiPanelRect.sizeDelta.x, size);
            ExpendBtn.transform.rotation = Quaternion.Euler(DirDown);
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 4;
            //grid.transform.position = Vector2.zero;
            grid.transform.localPosition = Vector2.zero;
            Debug.Log(grid.transform.localPosition);
        }
        else
        {
            //        Horizontal
            // Fixed Row Count / 1
            //vewport는 평소 125
            //화살표 위방향

            float size = 125f;
            //RectTransform temp = emojiPanelRect.rect;
            //temp.height = size;
            //grid.transform.position = Vector2.zero;
            emojiPanelRect.sizeDelta = new Vector2(emojiPanelRect.sizeDelta.x, size);
            ExpendBtn.transform.rotation = Quaternion.Euler(DirUp);
            scrollRect.horizontal = true;
            scrollRect.vertical = false;
            grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.constraintCount = 1;
            grid.transform.localPosition = BasicSize;
            Debug.Log(grid.transform.localPosition);
        }
        isExpend = !isExpend;
    }
    private void InitEmojiBtn()
    {
        //각 버튼에맞는 스프라이트 , 온클릭버튼 메소드마다 자신의 인덱스 부여
        for (int i = 0; i < emojiBtns.Length; i++)
        {
            int index = i; // 클로저를 생성하여 인덱스를 복사합니다.
            emojiBtns[i].onClick.AddListener(() => onEmotion(index)); //각버튼에 0~44 매개변수할당 
            emojiBtns[i].GetComponent<Image>().sprite = sprites[i];
        }
    }
}
