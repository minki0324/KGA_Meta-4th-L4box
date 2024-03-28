using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBoard : MonoBehaviour
{ // stage 관리하는 board prefabs에 참조
    public MemoryManager memoryManager = null;
    public MemoryStageData Stage { get; private set; }
    private MemoryPushpop currentOrderPushPop = null;
    private List<MemoryPushpop> memoryPopButtonList = new List<MemoryPushpop>(); // 스테이지 당 생성된 버튼
    private List<MemoryPushpop> correctButtonList = new List<MemoryPushpop>(); // 일반 스테이지 정답 버튼
    private Queue<MemoryPushpop> correctButtonQueue = new Queue<MemoryPushpop>(); // 스페셜 스테이지 정답 버튼 queue
    private int clearCount = 0; // 맞춰야하는 정답 갯수
    public int CurrentCorrectCount = 0; // 현재 맞춘 정답 갯수
    private bool isReplay = true; // 힌트 버튼을 눌렀을 때 true

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
    { // Stage 시작마다 Memory Board 생성 시 Setting
        for (int i = 0; i < transform.childCount; i++)
        {
            memoryPopButtonList.Add(transform.GetChild(i).GetComponent<MemoryPushpop>());
        }
        memoryManager.CurrentBoard = this;

        // 스테이지마다 새로운 보드 생성
        Stage = memoryManager.GetStage(); // 현재 스테이지 가져오기
        clearCount = Stage.CorrectCount; // stage 정답 개수 setting
        RandCorrectDraw();
        Blink(!isReplay);
    }

    private void Init()
    { // 스테이지 생성될 때, 끝날 때 파괴되느라 초기화 할 필요가 없을 듯
        correctButtonList.Clear(); // 정답 버튼 리스트 초기화
        clearCount = 0;
        CurrentCorrectCount = 0;
    }
    #endregion
    #region Memory Pop Button Setting
    public void ButtonAllStop()
    { // Memory pop button 클릭 못하게 만듦
        memoryManager.BackButton.GetComponent<Button>().interactable = false;
        memoryManager.Hintbutton.interactable = false;
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = false;
        }
    }

    public void ButtonAllPlay()
    { // Memory pop button 클릭 가능하게 만듦
        memoryManager.BackButton.GetComponent<Button>().interactable = true;
        memoryManager.HintButtonActive();
        for (int i = 0; i < memoryPopButtonList.Count; i++)
        {
            memoryPopButtonList[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    private void RandCorrectDraw()
    { // 스테이지의 정답 갯수만큼 정답 버튼 랜덤으로 고르기
        int drawCount = 0; // 
        while (drawCount < clearCount)
        { // random button 설정
            int randCorrectNum = Random.Range(1, memoryPopButtonList.Count);
            while (true)
            {
                if (!correctButtonList.Contains(memoryPopButtonList[randCorrectNum]))
                { // 정답 버튼에 포함되지 않은 index일 때
                    break;
                }
                randCorrectNum = Random.Range(1, memoryPopButtonList.Count);
            }
            // 정답 index 포함 아닐 시 list, queue에 넣어줌
            correctButtonList.Add(memoryPopButtonList[randCorrectNum]);
            correctButtonQueue.Enqueue(memoryPopButtonList[randCorrectNum]);
            drawCount++;
        }

        foreach (MemoryPushpop pushpop in correctButtonList)
        { // 정답 버튼 지정
            pushpop.IsCorrect = true;
        }

        currentOrderPushPop = correctButtonQueue.Dequeue();
    }

    public bool IsStageClear()
    { // 일반 스테이지 클리어 유무 확인
        if (clearCount.Equals(CurrentCorrectCount))
        {
            return true;
        }

        return false;
    }

    public bool IsOrder(MemoryPushpop _memoryPopButton)
    { // 스페셜 스테이지, 순서대로 누르는지 확인
        // Queue에 정답을 차례대로 담고
        // 하나씩 꺼내서 현재 정답으로 지정후 누른 버튼과 비교
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
    { // 일반 스테이지
        ButtonAllStop();
        int randindex = Random.Range(1, 4);
        if (!isReplay)
        {
            yield return new WaitForSeconds(1f);
            switch (randindex)
            {
                case 1:
                    memoryManager.PlayStartPanel("집중해보세요!");
                    break;
                case 2:
                    memoryManager.PlayStartPanel("정답을 찾아라!");
                    break;
                case 3:
                    memoryManager.PlayStartPanel("준비 됐나요?");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }

        CorrectButtnPlayBlink();

        yield return new WaitForSeconds(1f);
        
        ButtonAllPlay();
    }

    private IEnumerator SpecialStageStart_Co(bool isReplay)
    { // 스페셜 스테이지
        ButtonAllStop();
        if (!isReplay)
        { // 힌트일 때는 멘트 생략
            yield return new WaitForSeconds(1f);

            int randindex = Random.Range(1, 3);
            switch (randindex)
            {
                case 1:
                    memoryManager.PlayStartPanel("스페셜 스테이지!");
                    break;
                case 2:
                    memoryManager.PlayStartPanel("순서대로 눌러라!");
                    break;
            }

            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(SpecialCorrectButtonPlayBlink_Co());
    }
    #endregion
    #region Memory Button Blink
    public IEnumerator SpecialCorrectButtonPlayBlink_Co()
    { // 스페셜 스테이지 전용 Blink
        for (int i = 0; i < correctButtonList.Count; i++)
        {
            correctButtonList[i].PlayBlink();
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        ButtonAllPlay();
    }

    public void CorrectButtnPlayBlink()
    { // 일반 스테이지 Blink
        for (int i = 0; i < correctButtonList.Count; i++)
        {
            correctButtonList[i].PlayBlink();
        }
    }

    public void Blink(bool isReplay)
    { // 눌러야하는 버튼 반짝임
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
