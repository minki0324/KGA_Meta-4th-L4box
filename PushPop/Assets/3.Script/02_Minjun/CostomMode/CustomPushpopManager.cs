using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPushpopManager : MonoBehaviour
{

    private Vector3 SelectPositon; //카메라에서보이는 world 포지션 저장할 Vector
    [SerializeField] private GameObject pushPop; // OverLap검사하는 푸시팝(gameObject)
    [SerializeField] private GameObject RectPushPop; //UI 푸시팝
  
    [Header("result")]
    public GameObject result; 
    public TMP_Text resultText;
    public Image resultImage;
    [Header("PushPopObject")] 
    private GameObject newPush; //현재 소환중인 푸시팝
    private GameObject newRectPush;//현재 소환중인 푸시팝
    public Stack<GameObject> StackPops = new Stack<GameObject>(); //UI상 보이는 버튼담는 스택
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); //OverLap 검사를 하기위한 gameObject 스택
    public GameObject puzzleBoard; //생성된 버튼 상속해주는 GameObject
    [SerializeField] private Sprite[] btnSprites; //color 바꾸기위한 배열
    public Action onCustomEnd;
    public bool isCustomMode;
    public int spriteIndex = 0;
    public int currentCreatIndex = 0;
    
    [SerializeField] private FramePuzzle framePuzzle;
    private void Awake()
    {
        onCustomEnd += EndCustom;//커스텀모드 종료시 컴포넌트 끄기
        GameManager.Instance.pushPush.onPushPushGameEnd += BtnAllClear;

    }
    public void SetActiveCount()
    {
        GameManager.Instance.buttonActive = StackPops.Count;
    }
    #region 버튼 생성 메소드
    // 클릭 or 터치시 메소드들
    public void ClickDown()
    {
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //카메라상의 좌표를 월드포지션으로구하기
        //월드포지션에 push소환하기(Collider 검사해서 push버튼 겹치는지 확인하기위해)
        newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
        newPush.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f); // newRectPush비율에 맞게 설정해주세요 아래명시.
        //UI상 위치에 push소환(실제로 보이는 push)
        newRectPush = Instantiate(RectPushPop, Input.mousePosition, Quaternion.identity);
        tempPushPop push = newPush.GetComponent<tempPushPop>(); // collider check GameObject
        //푸시팝 overLap검사를 위한 버튼마다의 index 부여 == 버튼들끼리의 TriggerStay에서 겹쳤을때 index를 비교하여 먼저생성됬음을 판단함
        push.creatIndex = currentCreatIndex;
        currentCreatIndex++;
        push.RectPush = newRectPush;
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //스프라이트교체
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = btnSprites[spriteIndex]; // 현재 설정한 스프라이트 이미지로 설정
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = spriteIndex;
        // pushpop Btn Parent 설정
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // 스케일 변경시 프리팹 Circle(콜라이더검사) 스케일도 바꾼 스케일의 1.3배로 바꿔주세요 

        AudioManager.instance.SetAudioClip_SFX(3, false);

    }
    #endregion
    #region DecoPanel 버튼메소드
    public void ReturnBtn()//버튼 되돌리기
    {
        GameObject lastFakeStack = StackFakePops.Pop();
        Destroy(lastFakeStack);
        GameObject lastStack = StackPops.Pop();
        Destroy(lastStack);
        PushPop.Instance.pushPopButton.Remove(lastStack);
    }
    public void BtnAllClear()
    {
        while (StackPops.Count > 0)
        {
            GameObject obj = StackPops.Pop(); // Queue에서 오브젝트를 하나씩 제거
            Destroy(obj); // 해당 오브젝트를 파괴
            PushPop.Instance.pushPopButton.Remove(obj);
        }
        while (StackFakePops.Count > 0)
        {
            GameObject objs = StackFakePops.Pop();
            Destroy(objs);
        }
    } //버튼 모두 지우기 (리셋버튼참조 , 게임종료콜백이벤트 구독)
    public void RetryCustom()
    {
        GameManager.Instance.pushPush.DecoPanelSetActive(true);
        enabled = true;
        isCustomMode = true;
        framePuzzle.ImageAlphaHitSet(0.1f);
        GameManager.Instance.pushPush.pushCount = 0;
        foreach (var btn in StackPops)
        {
            btn.GetComponent<Button>().interactable = true;
            btn.GetComponent<Image>().raycastTarget = false;
        }
    } //다시 꾸미기
    public void EndCustom() //데코 종료 콜백이벤트 추가메소드
    {
        GameManager.Instance.pushPush.DecoPanelSetActive(false);
        enabled = false;
        isCustomMode = false;
        framePuzzle.ImageAlphaHitSet(0f);
        foreach (var btn in StackPops)
        {
            btn.GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
    public void DestroyChildren()

    {
        foreach (Transform child in puzzleBoard.transform)
        {
            Destroy(child.gameObject);
        }

    }


}