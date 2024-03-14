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
    //�̸�� ���� ��������
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
        //�̸�Ƽ�� ����� player ����ȭ �۾�
        //Emotion Play �� Client EmotionPlayer ��������
        PlayerEmotionControl targetEmotionControl = targetObject.GetComponentInChildren<PlayerEmotionControl>();
        //������ �̸�� Sprite�� ��ü
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
