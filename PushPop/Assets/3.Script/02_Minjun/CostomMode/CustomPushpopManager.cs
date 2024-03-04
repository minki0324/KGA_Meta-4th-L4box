using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPushpopManager : MonoBehaviour
{
    public static CustomPushpopManager Instance = null;


    [SerializeField] private RectTransform CustomArea;
    private Vector3 SelectPositon; //ī�޶󿡼����̴� world ������ ������ Vector
    [SerializeField] private RectTransform rectTransform; // UI�� ���콺��ġ ���ϱ����� ���� �ǳ�
    [SerializeField] private GameObject pushPop; // OverLap�˻��ϴ� Ǫ����(gameObject)
    [SerializeField] private Transform canvas; // UIǪ���� ��ȯ�ϰ� ��ӽ�ų ĵ����
    [SerializeField] private GameObject RectPushPop; //UI Ǫ����
    [SerializeField] private Button[] ColorButton;
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public bool isCanMakePush; //FramePuzzle ���� ���޹��� bool���� ��ġ�Ҷ� ��ư�� ������Ʈ�����ִ��� �Ǵ��ϱ����� ��.
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
    public int currentCreatIndex = 0;
    public GameObject decoPanel;
    [SerializeField] private PuzzleLozic puzzleLozic;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        onCustomEnd += DisableThisComponent;//Ŀ���Ҹ�� ����� ������Ʈ ����
        onCustomEnd += SetActiveCount;

    }

    private void OnDisable()
    {
        onCustomEnd -= DisableThisComponent; //Ŀ���Ҹ�� ����� ������Ʈ ����
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
    // Ŭ�� or ��ġ�� �޼ҵ��
    public void ClickDown()
    {
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //ī�޶���� ��ǥ�� �������������α��ϱ�
        //���������ǿ� push��ȯ�ϱ�(Collider �˻��ؼ� push��ư ��ġ���� Ȯ���ϱ�����)
        newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
        newPush.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f); // newRectPush������ �°� �������ּ��� �Ʒ����.
        // StackFakePops.Push(newPush);
        //UI�� ��ġ�� push��ȯ(������ ���̴� push)
        newRectPush = Instantiate(RectPushPop, Input.mousePosition, Quaternion.identity);
        tempPushPop push = newPush.GetComponent<tempPushPop>(); // collider check GameObject
        push.creatIndex = currentCreatIndex;
        currentCreatIndex++;
        push.RectPush = newRectPush;
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //��������Ʈ��ü
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = btnSprites[spriteIndex];
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = spriteIndex;
        // StackPops.Push(newRectPush);
        // pushpop Btn Parent ����
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // ������ ����� ������ Circle(�ݶ��̴��˻�) �����ϵ� �ٲ� �������� 1.3��� �ٲ��ּ��� 

        AudioManager.instance.SetAudioClip_SFX(3, false);

    }
    public void ClickUp()
    {
        if (newPush == null) return;

        newRectPush = null;
        newPush = null;

    } // ���콺Ŭ���� ������ or ��ġ�� ������  

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
            GameObject obj = StackPops.Pop(); // Queue���� ������Ʈ�� �ϳ��� ����
            Destroy(obj); // �ش� ������Ʈ�� �ı�
            PushPop.Instance.pushPopButton.Remove(obj);
        }
        while (StackFakePops.Count > 0)
        {
            GameObject objs = StackFakePops.Pop();
            Destroy(objs);
        }
    }
    public void DestroyChildren()
    {//������ �Ϸ������� �������ִ� ���� �����ϱ����� �޼ҵ�
        foreach (Transform child in puzzleBoard.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RetryCustom()
    {
        decoPanel.SetActive(true);
        enabled = true;
        isCustomMode = true;

        foreach (var btn in StackPops)
        {
            btn.GetComponent<Button>().interactable = true;
            btn.GetComponent<Image>().raycastTarget = false;

        }
    }
    public void OnPuzzleSolved()
    {
        AudioManager.instance.SetAudioClip_SFX(1, false);
        puzzleLozic.onPuzzleClear?.Invoke();
        puzzleLozic.successCount = 0;
    }
    public void onCustomEndmethod()
    {
        GameManager.Instance.GameClear();
        decoPanel.SetActive(false);
        onCustomEnd?.Invoke();
    }
    public void OnAllBubblesPopped()
    {
    }
}