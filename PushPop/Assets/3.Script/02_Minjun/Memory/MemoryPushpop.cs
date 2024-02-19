using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPushpop : MonoBehaviour
{
    private Image _myImage;
    public bool isCorrect;
    private Button button;
    private MemoryBoard memoryBoard;
    private Animator ani;
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

    void Update()
    {
        
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
    private void Correct()
    {//����޼ҵ�
        //todo �����ֱ�
        button.interactable = false; //������ư�� ��Ȱ��ȭ
        memoryBoard.CurrentCorrectCount++; //����ī��Ʈ ����
        MemoryManager.Instance.AddScore(); //���� ����
        if (memoryBoard.isStageClear())
        {
            onStageClear();
        }
    }
    private void Incorrect()
    {//����޼ҵ�
        //������ ���(MemoryManager)
        MemoryManager.Instance.Life--;
        MemoryManager.Instance.LifeRemove();
        //�ش� ��ư�� ��鸮�� ����(�ִϸ��̼�)
        //������ ��μ����� ����
        if (MemoryManager.Instance.Life == 0)
        {
            onStageFail();
        }

    }

    private void onStageFail()
    {
        //���纸�� ���ֱ�
        Destroy(memoryBoard.gameObject);
        //stage�ʱ�ȭ
        MemoryManager.Instance.currentStage = 1;
        MemoryManager.Instance.Score = 0;
        MemoryManager.Instance.ResetLife();
        //�޸� �κ�� ������
        MemoryManager.Instance.PlayStartPanel("���ɸ��ϴ�?");
    }

    private void onStageClear()
    {//��������  Ŭ����� �Ҹ��� �޼ҵ�
        Debug.Log(MemoryManager.Instance.currentStage + " : ��������Ŭ����");

        //�ڷ�ƾ���� ���ְ� �Ǹ��ؿ� ����ֱ� 2��
        StartCoroutine(Clear_co());
      
    }
    public void PlayBlink()
    {
        ani.SetTrigger("isBlink");
    }
    private IEnumerator Clear_co()
    {
        //�Ǹ��ؿ� �ִϸ��̼�
        memoryBoard.BtnAllStop();
        MemoryManager.Instance.PlayStartPanel("�Ǹ� �ؿ�!");
        yield return new WaitForSeconds(2f);

        //���纸�� ���ֱ�.
        Destroy(memoryBoard.gameObject);
        MemoryManager.Instance.currentStage++;
        MemoryManager.Instance.SetStageIndex();

        //������������?���̵�(���ο�� �����ֱ�) manager���� 
        MemoryManager.Instance.CreatBoard();
    }
}
