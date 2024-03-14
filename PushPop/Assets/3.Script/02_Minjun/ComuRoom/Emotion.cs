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
    private bool isExpend = false;
    [SerializeField] private GameObject emojiPanel;

    public Sprite[] sprites;
    public Button[] emojiBtns;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
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
        isExpend = !isExpend;
        emojiPanel.SetActive(isExpend);
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
