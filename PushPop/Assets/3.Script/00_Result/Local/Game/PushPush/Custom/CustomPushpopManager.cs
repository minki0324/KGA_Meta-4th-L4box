using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPushpopManager : MonoBehaviour
{
    [Header("Custom Mode")]
    public GameObject CustomMode;
    public TMP_Text StageTitle;
    [SerializeField] private GameObject overlabCheckPushPop; // OverLap검사하는 푸시팝(gameObject)
    [SerializeField] private GameObject rectPushPop; // UI 푸시팝
    [SerializeField] private GameObject reDecoButton;
    [SerializeField] private GameObject decoPanel;
    [SerializeField] private RectTransform customAreaRectTrans;
    private float customPosX = 0f;
    private Vector3 selectPositon; //카메라에서보이는 world 포지션 저장할 Vector
    private Coroutine buttonDownCoroutine;

    [Header("Result Panel")]
    public GameObject ResultPanel = null; 
    public TMP_Text ResultText = null;
    public Image ResultImage = null;

    [Header("PushPop Object")] 
    [SerializeField] private FramePuzzle framePuzzle;
    private GameObject newPush; //현재 소환중인 푸시팝
    private GameObject newRectPush;//현재 소환중인 푸시팝
    public GameObject PuzzleBoard; //생성된 버튼 상속해주는 GameObject
    public Sprite[] PushPopButtonSprite; //color 바꾸기위한 배열
    [HideInInspector] public int SpriteIndex = 0;
    private int currentCreatIndex = 0;

    private void Awake()
    {
        customPosX = customAreaRectTrans.localPosition.x;
    }

    #region DecoPanel Button Method
    private IEnumerator ClickDown_Co()
    {
        AudioManager.Instance.SetAudioClip_SFX(3, false);

        selectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치 시 카메라 상의 좌표

        // pushpop collider setting
        newPush = Instantiate(overlabCheckPushPop, selectPositon, Quaternion.identity);
        newPush.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f); // newRectPush 비율에 맞게 설정
        // pushpop button setting
        newRectPush = Instantiate(rectPushPop, Input.mousePosition, Quaternion.identity);
        OverlabCheckPushPop push = newPush.GetComponent<OverlabCheckPushPop>(); // collider check GameObject

        // pushpop overLap 검사를 위한 버튼마다의 index 부여 index를 비교를 통해 생성 순서 판단
        push.createIndex = currentCreatIndex;
        currentCreatIndex++;
        push.OverlabCheckCircle = newRectPush;
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //스프라이트교체
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = PushPopButtonSprite[SpriteIndex]; // 현재 설정한 스프라이트 이미지로 설정
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.SpriteIndex = SpriteIndex;

        // pushpop Btn Parent 설정
        newRectPush.transform.SetParent(PuzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // 스케일 변경시 프리팹 Circle(콜라이더검사) 스케일도 바꾼 스케일의 1.3배로 바꿔주세요
        yield return new WaitForSeconds(0.3f);
        buttonDownCoroutine = null;
    }

    public void ClickDown()
    { // button 생성
        if (buttonDownCoroutine != null) return;
        if (Input.touchCount > 1)
        {
            // 터치가 1개를 초과할 경우 함수 종료
            return;
        }
        buttonDownCoroutine = StartCoroutine(ClickDown_Co());
    }

    public void ReturnButton()
    { // 되돌리기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (PushPop.Instance.StackFakePops.Count.Equals(0)) return;
        GameObject lastFakeStack = PushPop.Instance.StackFakePops.Pop();
        Destroy(lastFakeStack);
        GameObject lastStack = PushPop.Instance.StackPops.Pop();
        Destroy(lastStack);

        PushPop.Instance.pushPopButton.Remove(lastStack);
    }

    public void ResetButton()
    { // 버튼 모두 지우기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ResetStack();
    }

    public void CustomModeInit()
    {
        ResetStack();
        customAreaRectTrans.localPosition = new Vector3(customPosX, customAreaRectTrans.localPosition.y, customAreaRectTrans.localPosition.z);
        reDecoButton.SetActive(false);
        decoPanel.SetActive(true);
        CustomMode.SetActive(false);
    }

    private void ResetStack()
    { // button 삭제
        while (PushPop.Instance.StackPops.Count > 0)
        { // ui button
            GameObject button = PushPop.Instance.StackPops.Pop();
            Destroy(button);
            PushPop.Instance.pushPopButton.Remove(button);
        }
        while (PushPop.Instance.StackFakePops.Count > 0)
        { // gameobject collider
            GameObject collider = PushPop.Instance.StackFakePops.Pop();
            Destroy(collider);
        }
        if (PushPop.Instance.pushPopButton.Count > 0)
        {
            PushPop.Instance.pushPopButton.Clear();
        }
    }

    public void RetryCustom()
    { // 다시 꾸미기
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.IsCustomMode = true;
        decoPanel.SetActive(true);
        reDecoButton.SetActive(false);
        StageTitle.text = "내 마음대로 그림을 꾸며보자!";
        framePuzzle.ImageAlphaHitSet(0.1f);
        ResetStack();
        PushPop.Instance.PushCount = 0;
        customAreaRectTrans.localPosition = new Vector3(customPosX, customAreaRectTrans.localPosition.y, customAreaRectTrans.localPosition.z);

        foreach (GameObject btn in PushPop.Instance.StackPops)
        {
            btn.GetComponent<Button>().interactable = true;
            btn.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void EndCustom()
    { // 데코 종료
        GameManager.Instance.IsCustomMode = false;
        decoPanel.SetActive(false);
        reDecoButton.SetActive(true);
        framePuzzle.ImageAlphaHitSet(0f);

        customAreaRectTrans.localPosition = new Vector3(0f, customAreaRectTrans.localPosition.y, customAreaRectTrans.localPosition.z);

        foreach (GameObject btn in PushPop.Instance.StackPops)
        {
            btn.GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
}