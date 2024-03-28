using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBoard : MonoBehaviour
{ // stage �����ϴ� board prefabs�� ����
    public MemoryManager memoryManager = null;
    public MemoryStageData Stage { get; private set; }
    private MemoryPushpop currentOrderPushPop = null;
    private List<MemoryPushpop> memoryPopButtonList = new List<MemoryPushpop>(); // �������� �� ������ ��ư
    private List<MemoryPushpop> correctButtonList = new List<MemoryPushpop>(); // �Ϲ� �������� ���� ��ư
    private Queue<MemoryPushpop> correctButtonQueue = new Queue<MemoryPushpop>(); // ����� �������� ���� ��ư queue
    private int clearCount = 0; // ������ϴ� ���� ����
    public int CurrentCorrectCount = 0; // ���� ���� ���� ����
    private bool isReplay = true; // ��Ʈ ��ư�� ������ �� true

    private void Awake()
    {
        memoryManager = FindObjectOfType<MemoryManager>();
    }

    private void OnEnable()
    {
        BoardSetting();
    }
    #region Memory Game Setting
    private void BoardSetting()
    { // Stage ���۸��� Memory Board ���� �� Setting
        for (int i = 0; i < transform.childCount; i++)
        {
            memoryPopButtonList.Add(transform.GetChild(i).GetComponent<MemoryPushpop>());
        }
        memoryManager.CurrentBoard = this;

        // ������������ ���ο� ���� ����
        Stage = memoryManager.GetStage(); // ���� �������� ��������
        clearCount = Stage.CorrectCount; // stage ���� ���� setting
        RandCorrectDraw();
        Blink(!isReplay);
    }

    private void Init()
    { // �������� ������ ��, ���� �� �ı��Ǵ��� �ʱ�ȭ �� �ʿ䰡 ���� ��
        correctButtonList.Clear(); // ���� ��ư ����Ʈ �ʱ�ȭ
        clearCount = 0;
        CurrentCorrectCount = 0;
    }
    #endregion
    #region Memory Pop Button Setting
    public void ButtonAllStop()
    { // Memory pop button Ŭ�� ���ϰ� ����
        memoryManager.BackButton.GetComponent<Button>().interactable = false;
        memoryManager.Hintbutton.interactable = false;
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = false;
        }
    }

    public void ButtonAllPlay()
    { // Memory pop button Ŭ�� �����ϰ� ����
        memoryManager.BackButton.GetComponent<Button>().interactable = true;
        memoryManager.HintButtonActive();
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    private void RandCorrectDraw()
    { // ���������� ���� ������ŭ ���� ��ư �������� ����
        int drawCount = 0; // 
        while (drawCount < clearCount)
        { // random button ����
            int randCorrectNum = Random.Range(1, memoryPopButtonList.Count);
            while (true)
            {
                if (!correctButtonList.Contains(memoryPopButtonList[randCorrectNum]))
                { // ���� ��ư�� ���Ե��� ���� index�� ��
                    break;
                }
                randCorrectNum = Random.Range(1, memoryPopButtonList.Count);
            }
            // ���� index ���� �ƴ� �� list, queue�� �־���
            correctButtonList.Add(memoryPopButtonList[randCorrectNum]);
            correctButtonQueue.Enqueue(memoryPopButtonList[randCorrectNum]);
            drawCount++;
        }

        foreach (MemoryPushpop pushpop in correctButtonList)
        { // ���� ��ư ����
            pushpop.IsCorrect = true;
        }

        currentOrderPushPop = correctButtonQueue.Dequeue();
    }

    public bool IsStageClear()
    { // �Ϲ� �������� Ŭ���� ���� Ȯ��
        if (clearCount.Equals(CurrentCorrectCount))
        {
            return true;
        }

        return false;
    }

    public bool IsOrder(MemoryPushpop _memoryPopButton)
    { // ����� ��������, ������� �������� Ȯ��
        // Queue�� ������ ���ʴ�� ���
        // �ϳ��� ������ ���� �������� ������ ���� ��ư�� ��
        if (currentOrderPushPop.Equals(_memoryPopButton))
        {
            if (correctButtonQueue.Count > 0)
            {
                currentOrderPushPop = correctButtonQueue.Dequeue();
            }
            return true;
        }

        return false;
    }
    #endregion
    #region Stage Start Text
    public IEnumerator StageStart_Co(bool isReplay)
    { // �Ϲ� ��������
        ButtonAllStop();
        int randindex = Random.Range(1, 4);
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            switch (randindex)
            {
                case 1:
                    memoryManager.PlayStartPanel("�����غ�����!");
                    break;
                case 2:
                    memoryManager.PlayStartPanel("������ ã�ƶ�!");
                    break;
                case 3:
                    memoryManager.PlayStartPanel("�غ� �Ƴ���?");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }

        CorrectButtnPlayBlink();

        yield return new WaitForSeconds(1f);
        
        ButtonAllPlay();
    }

    private IEnumerator SpecialStageStart_Co(bool isReplay)
    { // ����� ��������
        ButtonAllStop();
        if (!isReplay)
        { // ��Ʈ�� ���� ��Ʈ ����
            yield return new WaitForSeconds(1f);

            int randindex = Random.Range(1, 3);
            switch (randindex)
            {
                case 1:
                    memoryManager.PlayStartPanel("����� ��������!");
                    break;
                case 2:
                    memoryManager.PlayStartPanel("������� ������!");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(SpecialCorrectButtonPlayBlink_Co());
    }
    #endregion
    #region Memory Button Blink
    public IEnumerator SpecialCorrectButtonPlayBlink_Co()
    { // ����� �������� ���� Blink
        for (int i = 0; i < correctButtonList.Count; i++)
        {
            correctButtonList[i].PlayBlink();
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        ButtonAllPlay();
    }

    public void CorrectButtnPlayBlink()
    { // �Ϲ� �������� Blink
        for (int i = 0; i < correctButtonList.Count; i++)
        {
            correctButtonList[i].PlayBlink();
        }
    }

    public void Blink(bool isReplay)
    { // �������ϴ� ��ư ��¦��
        if (Stage.IsSpecialStage)
        {
            StartCoroutine(SpecialStageStart_Co(isReplay));
        }
        else
        {
            StartCoroutine(StageStart_Co(isReplay));
        }
    }
    #endregion
}
