using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEmotionControl : NetworkBehaviour
{

    private Animator animator;
    [SerializeField]private NetworkIdentity networkIdentity;
    private WaitForSeconds waitTime = new WaitForSeconds(2);
    [SerializeField] private GameObject[] emojis; 
    private Coroutine emojiCo;
    //이모션 사진 직접참조
    [SerializeField] private GameObject currentEmoji;
    void Start()
    {
        TryGetComponent(out animator);
        if (isLocalPlayer)
        {
            //자신의 씬에 있는 EmotionManager 에 자신의 이모션 스크립트 할당
            FindObjectOfType<Emotion>().playerEmotion = this;
            //Emotion.instance.playerEmotion = this;
        }

    }
    public void EMotionChange(int index)
    {
        if(currentEmoji != null && currentEmoji.activeSelf)
        {//현재 이모션이 실행중이라면?

            //실행중인 코루틴 스탑, 켜져있는 이모지 false
            StopCoroutine(emojiCo);
            currentEmoji.SetActive(false);
        }
        //실행할 이모지 변경
        currentEmoji = emojis[index];
      
       
    }
    [Client]
    public void PlayEmotion(int index)
    {
        CMD_EMotion(networkIdentity , index);
    }
    [Command]
    public void CMD_EMotion(NetworkIdentity targetObject , int index)
    {
        RPC_Emotion(targetObject , index);
    }
    [ClientRpc]
    public void RPC_Emotion(NetworkIdentity targetObject , int index)
    {
        //이모티콘 사용한 player 동기화 작업
        //각 Client들마다 Emotion Play 한 Client EmotionPlayer 가져오기
        PlayerEmotionControl targetEmotionControl = targetObject.GetComponentInChildren<PlayerEmotionControl>();
        //선택한 이모션 Sprite로 교체
        targetEmotionControl.EMotionChange(index);
        //이모지 실행
        emojiCo = StartCoroutine(EmojiPlay_co());
    }
    public IEnumerator EmojiPlay_co()
    {
        //이모지실행
        currentEmoji.SetActive(true);
       yield return waitTime;
        //이모지끄기
        currentEmoji.SetActive(false);
    }

}
