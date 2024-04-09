using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Emotion : NetworkBehaviour
{ //Emotion 관리와 실행 관련 스크립트
    public PlayerEmotionControl playerEmotion; 
    private bool isExpend = false; //감정표현 패널이 활성화 되있는지?
    [SerializeField] private GameObject emojiPanel;

    //이모션 버튼에 넣어줄 Sprite
    // *주의* PlayerEmotionControl 스크립트의 GameObject[] emojis;  와 순서 동일하게 둘것
    public Sprite[] sprites;
    public Button[] emojiBtns;

    private void Start()
    {
        InitEmojiBtn();
    }

    [Client]
    public void onEmotion(int index)
    {
        AudioManager.Instance.SetTalkTalkAudioClic_SFX(0, false);
        playerEmotion.PlayEmotion(index);
    }
    //감정표현 버튼에 onClick등록 되있음.
    public void ExpandEmojiPanel()
    {
        //버튼누를때 이모지 패널 활성화 or 비활성화
        isExpend = !isExpend;
        emojiPanel.SetActive(isExpend);
    }
    private void InitEmojiBtn()
    {
        //각 버튼에맞는 스프라이트 , 온클릭버튼 메소드마다 자신의 인덱스 부여
        for (int i = 0; i < emojiBtns.Length; i++)
        {
            int index = i; // 클로저를 생성하여 인덱스를 복사합니다.
            //이모지 실행 버튼에 onEmotion onClick메서드 할당
            emojiBtns[i].onClick.AddListener(() => onEmotion(index));
            //이모지sprites 배열 순서대로 이모지버튼에 sprite 넣어주기
            emojiBtns[i].GetComponent<Image>().sprite = sprites[i];
        }
    }
}
