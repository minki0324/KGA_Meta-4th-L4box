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
            onStageFail();
        }

    }

    private void onStageFail()
    {
        //현재보드 꺼주기
        Destroy(memoryBoard.gameObject);
        //stage초기화
        MemoryManager.Instance.currentStage = 1;
        MemoryManager.Instance.Score = 0;
        MemoryManager.Instance.ResetLife();
        //메모리 로비로 나가기
        MemoryManager.Instance.PlayStartPanel("왤케못하니?");
    }

    private void onStageClear()
    {//스테이지  클리어시 불리는 메소드
        Debug.Log(MemoryManager.Instance.currentStage + " : 스테이지클리어");

        //코루틴으로 텀주고 훌륭해요 띄워주기 2초
        StartCoroutine(Clear_co());
      
    }
    public void PlayBlink()
    {
        ani.SetTrigger("isBlink");
    }
    private IEnumerator Clear_co()
    {
        //훌륭해요 애니메이션
        memoryBoard.BtnAllStop();
        MemoryManager.Instance.PlayStartPanel("훌륭 해요!");
        yield return new WaitForSeconds(2f);

        //현재보드 꺼주기.
        Destroy(memoryBoard.gameObject);
        MemoryManager.Instance.currentStage++;
        MemoryManager.Instance.SetStageIndex();

        //다음스테이지?로이동(새로운보드 꺼내주기) manager에서 
        MemoryManager.Instance.CreatBoard();
    }
}
