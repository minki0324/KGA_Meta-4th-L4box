using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBoard : MonoBehaviour
{ // stage �����ϴ� board prefabs�� ����
    public int ClearCount; //������ϴ� ���䰹��
    public int CurrentCorrectCount; //����������䰹��
    private List<MemoryPushpop> memoryPopButtonList = new List<MemoryPushpop>();
    private List<MemoryPushpop> CorrectBtnList = new List<MemoryPushpop>();
    private Queue<MemoryPushpop> CorrectBtnQueue = new Queue<MemoryPushpop>();
    private MemoryPushpop currentOrderPushPop;
    private WaitForSeconds WaitTime = new WaitForSeconds(0.5f);
    public MemoryStageData stage { get; private set; }
    private bool isReplay = true;

    public Coroutine SpecialStageCoroutine = null;
    public Coroutine DefaultStageCoroutine = null;
    public Coroutine SpecialStageBlinkCoroutine = null;

    private void Awake()
    {
        BoardSetting();
    }
    private void OnEnable()
    {//������������ ���ο� ���带 ��������
        stage = MemoryManager.Instance.GetStage(); //���罺������ ��������
        RandCorrectDraw(stage.CorrectCount); // ���������� ���䰹����ŭ �����ư �������� ����
        Blink(!isReplay); //�� �����ư�� �������ֱ� true : ��Ʈ��ư false : ó�������Ҷ� 
    }
    private void OnDisable()
    {
        CorrectBtnList.Clear(); //�����ư ����Ʈ �ʱ�ȭ
        ClearCount = 0;
        CurrentCorrectCount = 0;
    }

    private void BoardSetting()
    { // Stage ���۸��� Memory Board ���� �� Setting
        for (int i = 0; i < transform.childCount; i++)
        {
            memoryPopButtonList.Add(transform.GetChild(i).GetComponent<MemoryPushpop>());
        }
        MemoryManager.Instance.CurrentBoard = this;

        // ������������ ���ο� ���带 ����
        stage = MemoryManager.Instance.GetStage(); //���罺������ ��������
        RandCorrectDraw(stage.CorrectCount); // ���������� ���䰹����ŭ �����ư �������� ����
        Blink(!isReplay); //�� �����ư�� �������ֱ� true : ��Ʈ��ư false : ó�������Ҷ� 
    }



    #region ��ưȰ��ȭ ��Ȱ��ȭ
    public void BtnAllStop()
    {//��ưȰ��ȭ ���� �޼ҵ�
        MemoryManager.Instance.hintbuttonIamge.raycastTarget = false;
        MemoryManager.Instance.Backbutton.enabled = false;
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = false;
        }
    }
    public void BtnAllPlay()
    {//��ưȰ��ȭ Ű�� �޼ҵ�
        MemoryManager.Instance.hintbuttonIamge.raycastTarget = true;
        MemoryManager.Instance.Backbutton.enabled = true;
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
    #region ��ư ���� ����( Ŭ���� ���� , �����ư ��������)
    private void RandCorrectDraw(int DrawNum)
    {
        //DrawNum�� Ƚ����ŭ �����ư�� ���մϴ�.
        ClearCount = DrawNum;
        int DrawCount = 0;
        while (DrawCount < DrawNum)
        {
            int RandCorrectNum = Random.Range(1, memoryPopButtonList.Count);
            while (true)
            {
                if (!CorrectBtnList.Contains(memoryPopButtonList[RandCorrectNum]))
                {
                    break;
                }
                else
                {
                    RandCorrectNum = Random.Range(1, memoryPopButtonList.Count);
                }
            }
            CorrectBtnList.Add(memoryPopButtonList[RandCorrectNum]);
            CorrectBtnQueue.Enqueue(memoryPopButtonList[RandCorrectNum]);
            DrawCount++;
        }
        //��ư���� �ʰ� �����̶�� �˷��ֱ�
        foreach (MemoryPushpop pushpop in CorrectBtnList)
        {
            pushpop.isCorrect = true;
        }
        currentOrderPushPop = CorrectBtnQueue.Dequeue();
    }//���� ���� ��ư ���ϴ°�
    public bool isStageClear()
    {//�Ϲ� �������� Ŭ���� ����Ȯ�� 
        //���� ���� ���� = ���䰹�� ���Ͻ� Ŭ����
        if (ClearCount == CurrentCorrectCount)
        {
            return true;
        }
        return false;
    }
    public bool IsOrder(MemoryPushpop btn)
    {//����� ������������ ������� �������� Ȯ���ϴ� ��
        //Queue�� ������ ���ʴ�� ���
        //�ϳ��� ������ ���� �������� ������ ���� ��ư�� ����.
        if (currentOrderPushPop == btn)
        {
            if (CorrectBtnQueue.Count > 0)
            {
                currentOrderPushPop = CorrectBtnQueue.Dequeue();
            }
            return true;
        }

        return false;
    }
    #endregion
    #region �������� ���� or �ؽ�Ʈ
    public IEnumerator ReadyGame(bool isReplay)
    { // ���� ��� ���� ���
        // BtnAllStop();
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            //���ӽ��� �ؽ�Ʈ ����
            int randindex = Random.Range(1, 4);
            switch (randindex)
            {
                case 1:
                    MemoryManager.Instance.PlayStartPanel("�����غ�����!");
                    break;
                case 2:
                    MemoryManager.Instance.PlayStartPanel("������ ã�ƶ�!");
                    break;
                case 3:
                    MemoryManager.Instance.PlayStartPanel("�غ� �Ƴ���?");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }

        // 1�� �� ��¦�̱�
        yield return new WaitForSeconds(1f);
        CorrectBtnPlayBlink();
        // BtnAllPlay();
    }
    private IEnumerator InOrder(bool isReplay)
    { // ������� ������ ���
        // BtnAllStop();
        if (!isReplay)
        { // ��Ʈ�� ���� ��Ʈ ����
            yield return new WaitForSeconds(1f);

            int randindex = Random.Range(1, 3);
            switch (randindex)
            {
                case 1:
                    MemoryManager.Instance.PlayStartPanel("����� ��������!");
                    break;
                case 2:
                    MemoryManager.Instance.PlayStartPanel("������� ������!");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }
        
        StartCoroutine(CorrectBtnPlayBlink_InOrder());
    }
    #endregion
    #region Memory Button Blink
    public IEnumerator CorrectBtnPlayBlink_InOrder()
    { // ����� �������� ���� Blink
        for (int i = 0; i < CorrectBtnList.Count; i++)
        {
            CorrectBtnList[i].PlayBlink();
            yield return WaitTime;
        }
        // BtnAllPlay();
    }

    public void CorrectBtnPlayBlink()
    { // �Ϲ� �������� Blink
        for (int i = 0; i < CorrectBtnList.Count; i++)
        {
            CorrectBtnList[i].PlayBlink();
        }
    }

    public void Blink(bool isReplay)
    { // �������ϴ� ��ư ��¦��
        if (stage.isSpecialStage)
        {
            StartCoroutine(InOrder(isReplay));
        }
        else
        {
            StartCoroutine(ReadyGame(isReplay));
        }
    }
    #endregion


}
