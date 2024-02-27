using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBoard : MonoBehaviour
{
    public int ClearCount; //맞춰야하는 정답갯수
    public int CurrentCorrectCount; //현재맞춘정답갯수
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
    {//스테이지마다 새로운 보드를 생성해줌


        stage = MemoryManager.Instance.GetStage(); //현재스테이지 가져오기
        RandCorrectDraw(stage.CorrectCount); // 스테이지의 정답갯수만큼 정답버튼 랜덤으로 고르기
        Blink(!isReplay); //고른 정답버튼을 깜빡여주기 true : 힌트버튼 false : 처음시작할때 
       


    }

  



    private void OnDisable()
    {
        CorrectBtnList.Clear(); //정답버튼 리스트 초기화
        ClearCount = 0;
        CurrentCorrectCount = 0;
    }
    public void RandCorrectDraw(int DrawNum)
    {
        //DrawNum의 횟수만큼 정답버튼을 정합니다.
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
        //버튼에게 너가 정답이라고 알려주기
        foreach (MemoryPushpop pushpop in CorrectBtnList)
        {
            pushpop.isCorrect = true;
        }
        currentOrderPushPop = CorrectBtnQueue.Dequeue();
    }
    public void BtnAllStop()
    {//버튼활성화 끄는 메소드
        MemoryManager.Instance.Hintbutton.enabled = false;
        MemoryManager.Instance.Backbutton.enabled = false;
        for (int i = 0; i < allButton.Count; i++)
        {
            allButton[i].GetComponent<Image>().raycastTarget = false;
        }
    }
    public void BtnAllPlay()
    {//버튼활성화 키는 메소드
        MemoryManager.Instance.Hintbutton.enabled = true;
        MemoryManager.Instance.Backbutton.enabled = true;
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
    {//순서상관없는모드
        BtnAllStop();
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            //게임시작 텍스트 띄우기
            int randindex = Random.Range(1, 4);
            switch (randindex) {
                case 1:
                    MemoryManager.Instance.PlayStartPanel("집중해보세요!");
                    break;
                case 2:
                    MemoryManager.Instance.PlayStartPanel($"정답을 찾아라!");
                    break;
                case 3:
                    MemoryManager.Instance.PlayStartPanel("준비 됐나요?");
                    break;
            }

          

            yield return new WaitForSeconds(2f);
        }
        //1초 뒤 반짝이기
        CorrectBtnPlayBlink();
        yield return new WaitForSeconds(1f);
        BtnAllPlay();
    }
    private IEnumerator InOrder(bool isReplay)
    {//순서대로 누르는 모드
        BtnAllStop();
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            //게임시작 텍스트 띄우기
            int randindex = Random.Range(1, 3);
            switch (randindex)
            {
                case 1:
                    MemoryManager.Instance.PlayStartPanel("스페셜 스테이지!");
                    break;
                case 2:
                    MemoryManager.Instance.PlayStartPanel($"순서대로 눌러라!");
                    break;

            }
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
