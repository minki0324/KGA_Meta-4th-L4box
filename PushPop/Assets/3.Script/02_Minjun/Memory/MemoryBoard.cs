using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBoard : MonoBehaviour
{
    public int ClearCount; //������ϴ� ���䰹��
    public int CurrentCorrectCount; //����������䰹��
    private List<MemoryPushpop> allButton = new List<MemoryPushpop>();
    private List<MemoryPushpop> CorrectBtnList = new List<MemoryPushpop>();
    private Queue<MemoryPushpop> CorrectBtnQueue = new Queue<MemoryPushpop>();
    private MemoryPushpop currentOrderPushPop;
    private WaitForSeconds WaitTime = new WaitForSeconds(0.5f);
    public MemoryStageData stage { get; private set; }
    private bool isReplay = true;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            allButton.Add(transform.GetChild(i).GetComponent<MemoryPushpop>());
        }
        MemoryManager.Instance.currentBoard = this;
    }
    private void OnEnable()
    {
        stage = MemoryManager.Instance.GetStage();
        RandCorrectDraw(stage.CorrectCount);
        Blink(!isReplay); //true : ��Ʈ�� �ֱ����� ����� ( ���ӽ��۹��� ��ŵ �ٷ� ����) false : ���� �������� ���۽� 
       


    }

  



    private void OnDisable()
    {
        CorrectBtnList.Clear(); //�����ư ����Ʈ �ʱ�ȭ
        ClearCount = 0;
        CurrentCorrectCount = 0;
    }
    public void RandCorrectDraw(int DrawNum)
    {
        //DrawNum�� Ƚ����ŭ �����ư�� ���մϴ�.
        ClearCount = DrawNum;
        int DrawCount = 0;
        while (DrawCount < DrawNum)
        {
            int RandCorrectNum = Random.Range(1, allButton.Count);
            while (true)
            {
                if (!CorrectBtnList.Contains(allButton[RandCorrectNum]))
                {
                    break;
                }
                else
                {
                    RandCorrectNum = Random.Range(1, allButton.Count);
                }
            }
            CorrectBtnList.Add(allButton[RandCorrectNum]);
            CorrectBtnQueue.Enqueue(allButton[RandCorrectNum]);
            DrawCount ++;
        }
        //��ư���� �ʰ� �����̶�� �˷��ֱ�
        foreach (MemoryPushpop pushpop in CorrectBtnList)
        {
            pushpop.isCorrect = true;
        }
        currentOrderPushPop = CorrectBtnQueue.Dequeue();
    }
    public void BtnAllStop()
    {//��ưȰ��ȭ ���� �޼ҵ�
        for (int i = 0; i < allButton.Count; i++)
        {
            allButton[i].GetComponent<Image>().raycastTarget = false;
        }
    }
    public void BtnAllPlay()
    {//��ưȰ��ȭ Ű�� �޼ҵ�
        for (int i = 0; i < allButton.Count; i++)
        {
            allButton[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    public bool isStageClear()
    {
        if(ClearCount == CurrentCorrectCount)
        {
            return true;  
        }
        return false;
    }
    public IEnumerator ReadyGame(bool isReplay)
    {//����������¸��
        BtnAllStop();
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            //���ӽ��� �ؽ�Ʈ ����
            MemoryManager.Instance.PlayStartPanel("���� ����!");

            yield return new WaitForSeconds(2f);
        }
        //1�� �� ��¦�̱�
        CorrectBtnPlayBlink();
        yield return new WaitForSeconds(1f);
        BtnAllPlay();
    }
    private IEnumerator InOrder(bool isReplay)
    {//������� ������ ���
        BtnAllStop();
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            //���ӽ��� �ؽ�Ʈ ����
            MemoryManager.Instance.PlayStartPanel("���� ����!");
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(CorrectBtnPlayBlink_InOrder());

    }
    public void CorrectBtnPlayBlink()
    {
        for (int i = 0; i < CorrectBtnList.Count; i++)
        {
            CorrectBtnList[i].PlayBlink();
        }
    }
    public IEnumerator CorrectBtnPlayBlink_InOrder()
    {
        for (int i = 0; i < CorrectBtnList.Count; i++)
        {
            CorrectBtnList[i].PlayBlink();
            yield return WaitTime;
        }
        BtnAllPlay();
    }
    public bool IsOrder(MemoryPushpop btn)
    {
        
        if (currentOrderPushPop == btn)
        {
            if (CorrectBtnQueue.Count > 0) { 
            currentOrderPushPop = CorrectBtnQueue.Dequeue();
            }
            return true;
        }

        return false;
    }
    public void Blink(bool isReplay)
    {
        if (stage.isSpecialStage)
        {
            StartCoroutine(InOrder(isReplay));
        }
        else
        {
            StartCoroutine(ReadyGame(isReplay));
        }
    }
 
}
