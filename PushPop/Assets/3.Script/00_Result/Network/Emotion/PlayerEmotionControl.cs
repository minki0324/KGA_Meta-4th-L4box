using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerEmotionControl : NetworkBehaviour
{
    [SerializeField] private NetworkIdentity networkIdentity;
    [SerializeField] private GameObject[] emojis; 
    private Animator animator;
    private Coroutine emojiCoroutine;
    private WaitForSeconds coroutineCasheDelay = new WaitForSeconds(2f);

    //�̸�� ���� ��������
    [SerializeField] private GameObject currentEmoji;

    void Start()
    {
        TryGetComponent(out animator);
        if (isLocalPlayer)
        {
            //�ڽ��� ���� �ִ� EmotionManager �� �ڽ��� �̸�� ��ũ��Ʈ �Ҵ�
            FindObjectOfType<Emotion>().playerEmotion = this;
        }
    }

    public void EMotionChange(int index)
    {
        if(currentEmoji != null && currentEmoji.activeSelf)
        {//���� �̸���� �������̶��?

            //�������� �ڷ�ƾ ��ž, �����ִ� �̸��� false
            StopCoroutine(emojiCoroutine);
            currentEmoji.SetActive(false);
        }

        //������ �̸��� ����
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
        //�� Client�鸶�� Emotion Play �� Client EmotionPlayer ��������
        PlayerEmotionControl targetEmotionControl = targetObject.GetComponentInChildren<PlayerEmotionControl>();
        //������ �̸�� Sprite�� ��ü
        targetEmotionControl.EMotionChange(index);
        //�̸��� ����
        emojiCoroutine = StartCoroutine(EmojiPlay_co());
    }

    public IEnumerator EmojiPlay_co()
    {
        //�̸�������
        currentEmoji.SetActive(true);
       yield return coroutineCasheDelay;
        //�̸�������
        currentEmoji.SetActive(false);
    }

}
