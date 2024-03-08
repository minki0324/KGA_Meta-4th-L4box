using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryPushpop : MonoBehaviour
{
    private Image _myImage;
    public bool isCorrect;
    private Button button;
    private MemoryBoard memoryBoard;
    private Animator ani;
    private int clearMessage;
    [SerializeField] private TMP_Text resultText = null;

    private void Awake()
    {
        TryGetComponent(out button);
        //TryGetComponent(out ani);
         ani =GetComponent<Animator>();

        memoryBoard = transform.parent.GetComponent<MemoryBoard>();
    }
    private void OnDisable()
    {
        isCorrect = false; // �����ư����
        button.interactable = true; //���Ǵ���ư Ȱ��ȭ
    }
    void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }
    #region onButton�� �־��ִ¸޼ҵ�
    public void onBtnClick()
    {
        if (memoryBoard.stage.isSpecialStage)
        {
            InOrderBtn();
        }
        else
        {
            MemoryBtnClick();
        }
    }
    public void MemoryBtnClick()
    {
        if (isCorrect)
        {//������ ��������
            Correct();
        }
        else
        {
            Incorrect();
        }
    }
    public void InOrderBtn()
    {
        if (memoryBoard.IsOrder(this))
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }
    #endregion
    #region ����,���������޼ҵ�
    private void Correct()
    {//����޼ҵ�
        AudioManager.instance.SetAudioClip_SFX(3,false);

        //todo �����ֱ�
        button.interactable = false; //������ư�� ��Ȱ��ȭ
        memoryBoard.CurrentCorrectCount++; //����ī��Ʈ ����
        MemoryManager.Instance.AddScore(100); //���� ����
        if (memoryBoard.isStageClear())
        {
            onStageClear();
        }
    }
    private void Incorrect()
    {//����޼ҵ�
        AudioManager.instance.SetAudioClip_SFX(0, false);
        PlayShakePush();
        //������ ���(MemoryManager)
        MemoryManager.Instance.Life--;
        MemoryManager.Instance.LifeRemove();
        //�ش� ��ư�� ��鸮�� ����(�ִϸ��̼�)
        //������ ��μ����� ����
        if (MemoryManager.Instance.Life == 0)
        {//���âȣ��
            //���������� �����ؼ� �й�
            MemoryManager.Instance.Result();
        }

    }


    #endregion
    #region �������� �¸��ݹ�޼ҵ�
    private void onStageClear()
    {//��������  Ŭ����� �Ҹ��� �޼ҵ�
        //�ڷ�ƾ���� ���ְ� �Ǹ��ؿ� ����ֱ� 2��
        StartCoroutine(Clear_co());

    }
    private IEnumerator Clear_co()
    {//Ŭ���� �ڷ�ƾ
        //�Ǹ��ؿ� �ִϸ��̼�
        memoryBoard.BtnAllStop(); //��ư��������
                
        AudioManager.instance.SetAudioClip_SFX(4, false);
        MemoryManager.Instance.PlayStartPanel("�Ǹ� �ؿ�!");//�ִϸ��̼� ��Ʈ���
        yield return new WaitForSeconds(2f);
        MemoryManager.Instance.currentStage++; //�������� Index����
        Debug.Log(MemoryManager.Instance.currentStage);
        //�غ�� �������� < ���罺������
         if(MemoryManager.Instance.endStageIndex < MemoryManager.Instance.currentStage)
        {
            //���âȣ��
            //��罺������ Ŭ���� ������
            MemoryManager.Instance.Result();
            yield break;
        }
        Destroy(memoryBoard.gameObject); //���纸�� �����
        MemoryManager.Instance.SetStageIndex(); //�������� �ؽ�Ʈ ��������

        //������������?���̵�(���ο�� �����ֱ�) manager���� 
        MemoryManager.Instance.CreatBoard();
    }


    #endregion

    #region ��ưŬ���ִϸ��̼�
    #endregion
    //������ �������� �����̴� �޼ҵ�
    public void PlayBlink()
    { //���ӽ���, Ȥ�� ��Ʈ��ư������ ���� ��ư�� �˷��ִ� �޼ҵ�
        ani.SetTrigger("isBlink");

        if (MemoryManager.Instance.currentStage % 5 != 0)
        {
            AudioManager.instance.SetAudioClip_SFX(2, false);
        }
        else
        {
            AudioManager.instance.SetAudioClip_SFX(1, false);
        }

    }
    private void PlayShakePush()
    {//��ư�� Ʋ������ ��鸮�� �ִϸ��̼�
        ani.SetTrigger("isShake");
        //��鸮�� ���� ��ġ �ȵǰ�
        _myImage.raycastTarget = false;
    }
    public void ShakeEndAfter()
    {//�ִϸ��̼� Event�� �߰�������
        // ��鸲�� ������ �ٽ� ��ġ �����ϰ� ����
        _myImage.raycastTarget = true;
        Debug.Log("11");
    }
    #region
    #endregion


}
