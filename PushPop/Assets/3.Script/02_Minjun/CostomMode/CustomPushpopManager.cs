using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CustomPushpopManager : MonoBehaviour
{
    public static CustomPushpopManager Instance = null;


    [SerializeField] private RectTransform CustomArea;
    private Vector3 SelectPositon; //카메라에서보이는 world 포지션 저장할 Vector
    [SerializeField] private RectTransform rectTransform; // UI상 마우스위치 구하기위한 기준 판넬
    [SerializeField] private GameObject pushPop; // OverLap검사하는 푸시팝(gameObject)
    [SerializeField] private Transform canvas; // UI푸시팝 소환하고 상속시킬 캔버스
    [SerializeField] private GameObject RectPushPop; //UI 푸시팝
    [SerializeField] private Button[] ColorButton;
    private GameObject newPush; //현재 소환중인 푸시팝
    private GameObject newRectPush;//현재 소환중인 푸시팝
    public bool isCanMakePush; //FramePuzzle 에서 전달받은 bool값을 설치할때 버튼이 오브젝트위에있는지 판단하기위해 씀.
    public bool isOnArea;
    public GameObject puzzleBoard;
    public Stack<GameObject> StackPops = new Stack<GameObject>();
    public Stack<GameObject> StackFakePops = new Stack<GameObject>();
    [SerializeField] private Sprite[] btnSprites;
    public int spriteIndex = 0;
    public GameObject result;
    public TMP_Text resultText;
    public Image resultImage;
    public bool isCustomMode;
    public Action onCustomEnd;
    public bool isCool = false;
    Coroutine cool;

    public GameObject decoPanel;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        onCustomEnd += DisableThisComponent;//커스텀모드 종료시 컴포넌트 끄기
        onCustomEnd += SetActiveCount;
    }
    
    private void OnDisable()
    {
        onCustomEnd -= DisableThisComponent; //커스텀모드 종료시 컴포넌트 끄기
        onCustomEnd -= SetActiveCount;
    }
  
    public void DestroyNewPush()
    {
        if (newPush != null && newRectPush != null)
        {
            Destroy(newPush);
            PushPop.Instance.pushPopButton.Remove(newRectPush);
            GameObject lastStack = StackPops.Pop();
            Destroy(lastStack);
            newPush = null;
            newRectPush = null;
            return;
        }
    }
    public void SetActiveCount()
    {
        GameManager.Instance.buttonActive = StackPops.Count;
    }

    public void DisableThisComponent()
    {
        this.enabled = false;
    }
    public void EnableThisComponent()
    {
        this.enabled = true;
    }
    // 클릭 or 터치시 메소드들
    public void ClickDown()
    {
        if (isCool) return;
        if (cool != null)
        {
            return;
        }
        cool = StartCoroutine(Cooltime());
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //카메라상의 좌표를 월드포지션으로구하기
                                                                             //판넬안에서 마우스혹은 터치위치의 RectTransform 구하기
        //UI에선 collider검사가 안되서 gameObject를 동시에 소환해서 안보이는 곳에서 겹침검사
        //월드포지션에 push소환하기(Collider 검사해서 push버튼 겹치는지 확인하기위해)
        newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
        // StackFakePops.Push(newPush);
        //UI상 위치에 push소환(실제로 보이는 push)
        newRectPush = Instantiate(RectPushPop, Input.mousePosition, Quaternion.identity);
        tempPushPop push = newPush.GetComponent<tempPushPop>(); // collider check GameObject

        push.RectPush = newRectPush;
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //스프라이트교체
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = btnSprites[spriteIndex];
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = spriteIndex;
        // StackPops.Push(newRectPush);
        // pushpop Btn Parent 설정
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        AudioManager.instance.SetAudioClip_SFX(3, false);

    } 
    public void ClickUp()
    {
        if (newPush == null) return;
        tempPushPop push = newPush.GetComponent<tempPushPop>(); // collider check GameObject
        
        //push.RectPush = newRectPush;
        push.GetComponent<tempPushPop>().isSet = true; // 놓아졌는 지 확인

        //isCheckOverLap = ture일시 다른 푸시팝과 겹치는지 확인함
        push.isCheckOverlap = true;
        //마우스가 퍼즐오브젝트 위에 있을시 true , 아니면 false -> 푸시팝설치를 오브젝트에만 하게하기위해
        //StartCoroutine(CheckDelay(push));
        newRectPush = null;
        newPush = null;
      
    } // 마우스클릭을 뗏을때 or 터치를 뗏을때  

    public void ReturnBtn()
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
    }

    //public void GetSpriteIndex(int index)
    //{
    //    ColorButton[spriteIndex].interactable = true;
    //    spriteIndex = index;
    //    ColorButton[spriteIndex].interactable = false;
    //}
    public void onCustomEndmethod()
    {
        if (StackPops.Count == 0) return;
        decoPanel.SetActive(false);
        onCustomEnd?.Invoke();
    }
    public void DestroyChildren()
    {//퍼즐을 완료했을때 생성되있던 퍼즐 삭제하기위한 메소드
        foreach (Transform child in puzzleBoard.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator Cooltime()
    {
        isCool = true;
        yield return new WaitForSeconds(0.2f);
        isCool = false;
        cool = null;
    }

    public void SetMoaMoaList()
    { // MoaMoa 메인화면에 출력할 6개의 컬렉션을 출력하는 Btn 연동 Method
        
    }
}