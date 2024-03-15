using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPushpopManager : MonoBehaviour
{
    [Header("Custom Mode")]
    public GameObject CustomMode = null;
    private Vector3 selectPositon; //카메라에서보이는 world 포지션 저장할 Vector
    [SerializeField] private GameObject pushPop; // OverLap검사하는 푸시팝(gameObject)
    [SerializeField] private GameObject RectPushPop; //UI 푸시팝
    public TMP_Text StageTitle = null;

    [Header("Result Panel")]
    public GameObject ResultPanel = null; 
    public TMP_Text ResultText = null;
    public Image ResultImage = null;

    [Header("PushPopObject")] 
    private GameObject newPush; //현재 소환중인 푸시팝
    private GameObject newRectPush;//현재 소환중인 푸시팝
    public Stack<GameObject> StackPops = new Stack<GameObject>(); //UI상 보이는 버튼담는 스택
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); //OverLap 검사를 하기위한 gameObject 스택
    public GameObject puzzleBoard; //생성된 버튼 상속해주는 GameObject
    public Sprite[] pushPopButtonSprite; //color 바꾸기위한 배열
    public int SpriteIndex = 0;
    public int currentCreatIndex = 0;
    [SerializeField] private FramePuzzle framePuzzle;

    public void SetActiveCount()
    {
        GameManager.Instance.buttonActive = StackPops.Count;
    }

    #region 버튼 생성 메소드
    // 클릭 or 터치시 메소드들
    public void ClickDown()
    {
        selectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //카메라상의 좌표를 월드포지션으로구하기
        //월드포지션에 push소환하기(Collider 검사해서 push버튼 겹치는지 확인하기위해)
        newPush = Instantiate(pushPop, selectPositon, Quaternion.identity);
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
        popImage.sprite = pushPopButtonSprite[SpriteIndex]; // 현재 설정한 스프라이트 이미지로 설정
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = SpriteIndex;
        // pushpop Btn Parent 설정
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // 스케일 변경시 프리팹 Circle(콜라이더검사) 스케일도 바꾼 스케일의 1.3배로 바꿔주세요 

        AudioManager.Instance.SetAudioClip_SFX(3, false);

    }
    #endregion
    #region DecoPanel Button Method
    public void ReturnButton()
    { // 되돌리기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (StackFakePops.Count.Equals(0)) return;
        GameObject lastFakeStack = StackFakePops.Pop();
        Destroy(lastFakeStack);
        GameObject lastStack = StackPops.Pop();
        Destroy(lastStack);

        PushPop.Instance.pushPopButton.Remove(lastStack);
    }

    public void ResetButton()
    { // 버튼 모두 지우기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
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
    }

    public void RetryCustom()
    { // 다시 꾸미기
        CustomMode.SetActive(true);
        StageTitle.text = "내 마음대로 그림을 꾸며보자!";
        // enabled = true;
        GameManager.Instance.IsCustomMode = true;
        framePuzzle.ImageAlphaHitSet(0.1f);
        PushPop.Instance.PushCount = 0;
        foreach (var btn in StackPops)
        {
            btn.GetComponent<Button>().interactable = true;
            btn.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void EndCustom()
    { // 데코 종료
        // enabled = false;
        GameManager.Instance.IsCustomMode = false;
        // 다.꾸 버튼 Active True
        framePuzzle.ImageAlphaHitSet(0f);
        foreach (var btn in StackPops)
        {
            btn.GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
    public void DestroyChildren()
    { // 생성된 버튼 삭제
        foreach (Transform child in puzzleBoard.transform)
        {
            Destroy(child.gameObject);
        }
    }
}