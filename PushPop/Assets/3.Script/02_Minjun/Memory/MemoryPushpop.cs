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
    #region onButton�� �־��ִ¸޼ҵ�
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
            MemoryManager.Instance.onStageFail();
        }

    }
    #endregion
    #region �������� �¸��ݹ�޼ҵ�
 

    private void onStageClear()
    {//��������  Ŭ����� �Ҹ��� �޼ҵ�
        Debug.Log(MemoryManager.Instance.currentStage + " : ��������Ŭ����");

        //�ڷ�ƾ���� ���ְ� �Ǹ��ؿ� ����ֱ� 2��
        StartCoroutine(Clear_co());

    }
    private IEnumerator Clear_co()
    {//Ŭ���� �ڷ�ƾ
        //�Ǹ��ؿ� �ִϸ��̼�
        memoryBoard.BtnAllStop(); //��ư��������
        MemoryManager.Instance.PlayStartPanel("�Ǹ� �ؿ�!");//�ִϸ��̼� ��Ʈ���
        yield return new WaitForSeconds(2f);

        Destroy(memoryBoard.gameObject); //���纸�� �����
        MemoryManager.Instance.currentStage++; //�������� Index����
        MemoryManager.Instance.SetStageIndex(); //�������� �ؽ�Ʈ ��������

        //������������?���̵�(���ο�� �����ֱ�) manager���� 
        MemoryManager.Instance.CreatBoard();
    }
    #endregion


    //�����Ҷ� ����˷��ִ� �������� �ִϸ��̼� �޼ҵ�
    public void PlayBlink()
    {
        ani.SetTrigger("isBlink");
    }
    #region
    #endregion
    
   
}
