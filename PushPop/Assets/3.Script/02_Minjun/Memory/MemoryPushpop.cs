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
        isCorrect = false; // 정답버튼인지
        button.interactable = true; //눌렷던버튼 활성화
    }
    void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }

    void Update()
    {
        
    }
    #region onButton에 넣어주는메소드
    public void MemoryBtnClick()
    {
        if (isCorrect)
        {//정답을 눌렀을때
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
    #region 정답,오답판정메소드
    private void Correct()
    {//정답메소드
        //todo 점수주기
        button.interactable = false; //누른버튼은 비활성화
        memoryBoard.CurrentCorrectCount++; //정답카운트 증가
        MemoryManager.Instance.AddScore(); //점수 증가
        if (memoryBoard.isStageClear())
        {
            onStageClear();
        }
    }
    private void Incorrect()
    {//오답메소드
        //라이프 깎기(MemoryManager)
        MemoryManager.Instance.Life--;
        MemoryManager.Instance.LifeRemove();
        //해당 버튼이 흔들리게 설정(애니메이션)
        //라이프 모두소진시 실패
        if (MemoryManager.Instance.Life == 0)
        {
            MemoryManager.Instance.onStageFail();
        }

    }
    #endregion
    #region 스테이지 승리콜백메소드
 

    private void onStageClear()
    {//스테이지  클리어시 불리는 메소드
        Debug.Log(MemoryManager.Instance.currentStage + " : 스테이지클리어");

        //코루틴으로 텀주고 훌륭해요 띄워주기 2초
        StartCoroutine(Clear_co());

    }
    private IEnumerator Clear_co()
    {//클리어 코루틴
        //훌륭해요 애니메이션
        memoryBoard.BtnAllStop(); //버튼동작정지
        MemoryManager.Instance.PlayStartPanel("훌륭 해요!");//애니메이션 멘트재생
        yield return new WaitForSeconds(2f);

        Destroy(memoryBoard.gameObject); //현재보드 지우기
        MemoryManager.Instance.currentStage++; //스테이지 Index증가
        MemoryManager.Instance.SetStageIndex(); //스테이지 텍스트 문구변경

        //다음스테이지?로이동(새로운보드 꺼내주기) manager에서 
        MemoryManager.Instance.CreatBoard();
    }
    #endregion


    //시작할때 정답알려주는 깜빡깜빡 애니메이션 메소드
    public void PlayBlink()
    {
        ani.SetTrigger("isBlink");
    }
    #region
    #endregion
    
   
}
