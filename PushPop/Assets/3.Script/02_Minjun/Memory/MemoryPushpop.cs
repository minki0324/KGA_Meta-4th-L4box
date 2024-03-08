using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryPushpop : MonoBehaviour
{
    private Image _myImage;
    public bool isCorrect;
    private Button button;
    private MemoryBoard memoryBoard;
    private Animator ani;
    private int clearMessage;
    [SerializeField] private TMP_Text resultText = null;

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
    #region onButton에 넣어주는메소드
    public void onBtnClick()
    {
        if (memoryBoard.stage.isSpecialStage)
        {
            InOrderBtn();
        }
        else
        {
            MemoryBtnClick();
        }
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
        AudioManager.instance.SetAudioClip_SFX(3,false);

        //todo 점수주기
        button.interactable = false; //누른버튼은 비활성화
        memoryBoard.CurrentCorrectCount++; //정답카운트 증가
        MemoryManager.Instance.AddScore(100); //점수 증가
        if (memoryBoard.isStageClear())
        {
            onStageClear();
        }
    }
    private void Incorrect()
    {//오답메소드
        AudioManager.instance.SetAudioClip_SFX(0, false);
        PlayShakePush();
        //라이프 깎기(MemoryManager)
        MemoryManager.Instance.Life--;
        MemoryManager.Instance.LifeRemove();
        //해당 버튼이 흔들리게 설정(애니메이션)
        //라이프 모두소진시 실패
        if (MemoryManager.Instance.Life == 0)
        {//결과창호출
            //모든라이프가 소진해서 패배
            MemoryManager.Instance.Result();
        }

    }


    #endregion
    #region 스테이지 승리콜백메소드
    private void onStageClear()
    {//스테이지  클리어시 불리는 메소드
        //코루틴으로 텀주고 훌륭해요 띄워주기 2초
        StartCoroutine(Clear_co());

    }
    private IEnumerator Clear_co()
    {//클리어 코루틴
        //훌륭해요 애니메이션
        memoryBoard.BtnAllStop(); //버튼동작정지
                
        AudioManager.instance.SetAudioClip_SFX(4, false);
        MemoryManager.Instance.PlayStartPanel("훌륭 해요!");//애니메이션 멘트재생
        yield return new WaitForSeconds(2f);
        MemoryManager.Instance.currentStage++; //스테이지 Index증가
        Debug.Log(MemoryManager.Instance.currentStage);
        //준비된 스테이지 < 현재스테이지
         if(MemoryManager.Instance.endStageIndex < MemoryManager.Instance.currentStage)
        {
            //결과창호출
            //모든스테이지 클리어 했을때
            MemoryManager.Instance.Result();
            yield break;
        }
        Destroy(memoryBoard.gameObject); //현재보드 지우기
        MemoryManager.Instance.SetStageIndex(); //스테이지 텍스트 문구변경

        //다음스테이지?로이동(새로운보드 꺼내주기) manager에서 
        MemoryManager.Instance.CreatBoard();
    }


    #endregion

    #region 버튼클릭애니메이션
    #endregion
    //본인이 정답인지 깜빡이는 메소드
    public void PlayBlink()
    { //게임시작, 혹은 힌트버튼누를때 정답 버튼을 알려주는 메소드
        ani.SetTrigger("isBlink");

        if (MemoryManager.Instance.currentStage % 5 != 0)
        {
            AudioManager.instance.SetAudioClip_SFX(2, false);
        }
        else
        {
            AudioManager.instance.SetAudioClip_SFX(1, false);
        }

    }
    private void PlayShakePush()
    {//버튼이 틀렸을때 흔들리는 애니메이션
        ani.SetTrigger("isShake");
        //흔들리는 동안 터치 안되게
        _myImage.raycastTarget = false;
    }
    public void ShakeEndAfter()
    {//애니메이션 Event로 추가되있음
        // 흔들림이 끝나고 다시 터치 가능하게 만듬
        _myImage.raycastTarget = true;
        Debug.Log("11");
    }
    #region
    #endregion


}
