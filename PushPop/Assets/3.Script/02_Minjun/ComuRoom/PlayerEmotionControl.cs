using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEmotionControl : NetworkBehaviour
{

    private Animator animator;
    private NetworkIdentity networkIdentity;

    //이모션 사진 직접참조
    [SerializeField] private SpriteRenderer spriterenderer;
    void Start()
    {
        TryGetComponent(out animator);
        transform.root.TryGetComponent(out networkIdentity);
    }
    
    public void EMotionChange(int index)
    {
        spriterenderer.sprite = Emotion.instance.emotions[index];
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
        //이모션 Play
        targetEmotionControl.animator.SetTrigger("onEmotion");
    }


}
