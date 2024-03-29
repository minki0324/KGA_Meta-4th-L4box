using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Emotion : NetworkBehaviour
{ //Emotion ������ ���� ���� ��ũ��Ʈ
    public PlayerEmotionControl playerEmotion; 
    private bool isExpend = false; //����ǥ�� �г��� Ȱ��ȭ ���ִ���?
    [SerializeField] private GameObject emojiPanel;

    //�̸�� ��ư�� �־��� Sprite
    // *����* PlayerEmotionControl ��ũ��Ʈ�� GameObject[] emojis;  �� ���� �����ϰ� �Ѱ�
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
    //����ǥ�� ��ư�� onClick��� ������.
    public void ExpandEmojiPanel()
    {
        //��ư������ �̸��� �г� Ȱ��ȭ or ��Ȱ��ȭ
        isExpend = !isExpend;
        emojiPanel.SetActive(isExpend);
    }
    private void InitEmojiBtn()
    {
        //�� ��ư���´� ��������Ʈ , ��Ŭ����ư �޼ҵ帶�� �ڽ��� �ε��� �ο�
        for (int i = 0; i < emojiBtns.Length; i++)
        {
            int index = i; // Ŭ������ �����Ͽ� �ε����� �����մϴ�.
            //�̸��� ���� ��ư�� onEmotion onClick�޼��� �Ҵ�
            emojiBtns[i].onClick.AddListener(() => onEmotion(index));
            //�̸���sprites �迭 ������� �̸�����ư�� sprite �־��ֱ�
            emojiBtns[i].GetComponent<Image>().sprite = sprites[i];
        }
    }
}
