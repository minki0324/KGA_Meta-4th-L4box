using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Emotion : NetworkBehaviour
{
    //���� EMotion �� �����ϰ� �ֱ� ���� ��ũ��Ʈ
    public static Emotion instance;
    //�̸�� ���� �ν����Ϳ��� �߰��ϱ�
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
        //veiwport �÷��ֱ� 500
        //ȭ��ǥ �Ʒ�����
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
            //vewport�� ��� 125
            //ȭ��ǥ ������

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
        //�� ��ư���´� ��������Ʈ , ��Ŭ����ư �޼ҵ帶�� �ڽ��� �ε��� �ο�
        for (int i = 0; i < emojiBtns.Length; i++)
        {
            int index = i; // Ŭ������ �����Ͽ� �ε����� �����մϴ�.
            emojiBtns[i].onClick.AddListener(() => onEmotion(index)); //����ư�� 0~44 �Ű������Ҵ� 
            emojiBtns[i].GetComponent<Image>().sprite = sprites[i];
        }
    }
}
