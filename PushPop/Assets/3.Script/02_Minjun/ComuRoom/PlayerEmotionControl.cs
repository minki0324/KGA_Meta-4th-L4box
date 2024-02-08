using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEmotionControl : NetworkBehaviour
{

    private Animator animator;
    private NetworkIdentity networkIdentity;

    //�̸�� ���� ��������
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
        //�̸�Ƽ�� ����� player ����ȭ �۾�
        //Emotion Play �� Client EmotionPlayer ��������
        PlayerEmotionControl targetEmotionControl = targetObject.GetComponentInChildren<PlayerEmotionControl>();
        //������ �̸�� Sprite�� ��ü
        targetEmotionControl.EMotionChange(index);
        //�̸�� Play
        targetEmotionControl.animator.SetTrigger("onEmotion");
    }


}
