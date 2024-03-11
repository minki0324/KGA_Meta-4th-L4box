using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBoard : MonoBehaviour
{ // stage 관리하는 board prefabs에 참조
    public int ClearCount; //맞춰야하는 정답갯수
    public int CurrentCorrectCount; //현재맞춘정답갯수
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

    private void BoardSetting()
    { // Stage 시작마다 Memory Board 생성 시 Setting
        for (int i = 0; i < transform.childCount; i++)
        {
            memoryPopButtonList.Add(transform.GetChild(i).GetComponent<MemoryPushpop>());
        }
        MemoryManager.Instance.CurrentBoard = this;

        // 스테이지마다 새로운 보드를 생성
        stage = MemoryManager.Instance.GetStage(); //현재스테이지 가져오기
        RandCorrectDraw(stage.CorrectCount); // 스테이지의 정답갯수만큼 정답버튼 랜덤으로 고르기
        Blink(!isReplay); //고른 정답버튼을 깜빡여주기 true : 힌트버튼 false : 처음시작할때 
    }



    #region 버튼활성화 비활성화
    public void BtnAllStop()
    {//버튼활성화 끄는 메소드
        MemoryManager.Instance.hintbuttonIamge.raycastTarget = false;
        MemoryManager.Instance.Backbutton.enabled = false;
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = false;
        }
    }
    public void BtnAllPlay()
    {//버튼활성화 키는 메소드
        MemoryManager.Instance.hintbuttonIamge.raycastTarget = true;
        MemoryManager.Instance.Backbutton.enabled = true;
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
    #region 버튼 정답 관련( 클리어 유무 , 정답버튼 랜덤지정)
    private void RandCorrectDraw(int DrawNum)
    {
        //DrawNum의 횟수만큼 정답버튼을 정합니다.
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
        //버튼에게 너가 정답이라고 알려주기
        foreach (MemoryPushpop pushpop in CorrectBtnList)
        {
            pushpop.isCorrect = true;
        }
        currentOrderPushPop = CorrectBtnQueue.Dequeue();
    }//랜덤 정답 버튼 정하는곳
    public bool isStageClear()
    {//일반 스테이지 클리어 유무확인 
        //정답 누른 갯수 = 정답갯수 동일시 클리어
        if (ClearCount == CurrentCorrectCount)
        {
            return true;
        }
        return false;
    }
    public bool IsOrder(MemoryPushpop btn)
    {//스페셜 스테이지전용 순서대로 누르는지 확인하는 곳
        //Queue에 정답을 차례대로 담고
        //하나씩 꺼내서 현재 정답으로 지정후 누른 버튼과 비교함.
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
    #region 스테이지 시작 or 텍스트
    public IEnumerator ReadyGame(bool isReplay)
    { // 순서 상관 없는 모드
        // BtnAllStop();
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            //게임시작 텍스트 띄우기
            int randindex = Random.Range(1, 4);
            switch (randindex)
            {
                case 1:
                    MemoryManager.Instance.PlayStartPanel("집중해보세요!");
                    break;
                case 2:
                    MemoryManager.Instance.PlayStartPanel("정답을 찾아라!");
                    break;
                case 3:
                    MemoryManager.Instance.PlayStartPanel("준비 됐나요?");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }

        // 1초 뒤 반짝이기
        yield return new WaitForSeconds(1f);
        CorrectBtnPlayBlink();
        // BtnAllPlay();
    }
    private IEnumerator InOrder(bool isReplay)
    { // 순서대로 누르는 모드
        // BtnAllStop();
        if (!isReplay)
        { // 힌트일 때는 멘트 생략
            yield return new WaitForSeconds(1f);

            int randindex = Random.Range(1, 3);
            switch (randindex)
            {
                case 1:
                    MemoryManager.Instance.PlayStartPanel("스페셜 스테이지!");
                    break;
                case 2:
                    MemoryManager.Instance.PlayStartPanel("순서대로 눌러라!");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }
        
        StartCoroutine(CorrectBtnPlayBlink_InOrder());
    }
    #endregion
    #region Memory Button Blink
    public IEnumerator CorrectBtnPlayBlink_InOrder()
    { // 스페셜 스테이지 전용 Blink
        for (int i = 0; i < CorrectBtnList.Count; i++)
        {
            CorrectBtnList[i].PlayBlink();
            yield return WaitTime;
        }
        // BtnAllPlay();
    }

    public void CorrectBtnPlayBlink()
    { // 일반 스테이지 Blink
        for (int i = 0; i < CorrectBtnList.Count; i++)
        {
            CorrectBtnList[i].PlayBlink();
        }
    }

    public void Blink(bool isReplay)
    { // 눌러야하는 버튼 반짝임
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
