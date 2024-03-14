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
    private Vector3 myScale;
    private Vector3 myPos;
    float tempScaleX;
    //이모션 사진 직접참조
    [SerializeField] private GameObject currentEmoji;
    void Start()
    {
        TryGetComponent(out animator);
        if (isLocalPlayer)
        {
            Emotion.instance.playerEmotion = this;
        }

    }
    public void EMotionChange(int index)
    {
        if(currentEmoji != null)
        {
            StopCoroutine(emojiCo);
            currentEmoji.SetActive(false);
        }
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
        //Emotion Play 한 Client EmotionPlayer 가져오기
        PlayerEmotionControl targetEmotionControl = targetObject.GetComponentInChildren<PlayerEmotionControl>();
        //선택한 이모션 Sprite로 교체
        targetEmotionControl.EMotionChange(index);
        emojiCo = StartCoroutine(EmojiPlay_co());
    }
    public IEnumerator EmojiPlay_co()
    {
        currentEmoji.SetActive(true);
       yield return waitTime;
        currentEmoji.SetActive(false);
    }

}
