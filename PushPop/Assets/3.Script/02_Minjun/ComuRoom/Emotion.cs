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
        //각 버튼에맞는 스프라이트 , 온클릭버튼 메소드마다 자신의 인덱스 부여
        for (int i = 0; i < emojiBtns.Length; i++)
        {
            int index = i; // 클로저를 생성하여 인덱스를 복사합니다.
            emojiBtns[i].onClick.AddListener(() => onEmotion(index)); //각버튼에 0~44 매개변수할당 
            emojiBtns[i].GetComponent<Image>().sprite = sprites[i];
        }
    }
}
