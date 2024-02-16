using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CustomPushpopManager : MonoBehaviour
{
    public static CustomPushpopManager Instance;


    [SerializeField] private RectTransform CustomArea;
    private Vector3 SelectPositon; //ī�޶󿡼����̴� world ������ ������ Vector
    [SerializeField] private RectTransform rectTransform; // UI�� ���콺��ġ ���ϱ����� ���� �ǳ�
    [SerializeField] private GameObject pushPop; // OverLap�˻��ϴ� Ǫ����(gameObject)
    [SerializeField] private Transform canvas; // UIǪ���� ��ȯ�ϰ� ��ӽ�ų ĵ����
    [SerializeField] private GameObject RectPushPop; //UI Ǫ����
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public bool isCanMakePush; //FramePuzzle ���� ���޹��� bool���� ��ġ�Ҷ� ��ư�� ������Ʈ�����ִ��� �Ǵ��ϱ����� ��.
    public bool isOnArea;
    public GameObject puzzleBoard;
    public Stack<GameObject> StackPops = new Stack<GameObject>();
    public Stack<GameObject> StackFakePops = new Stack<GameObject>();
    [SerializeField] private Sprite[] btnSprites;
    private int spriteIndex = 0;
    public GameObject result;
    public TMP_Text resultText;
    public Image resultImage;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {// �ȵ���̵�� ��������
  
    }
    private IEnumerator CheckDelay(tempPushPop push)
    {
        yield return new WaitForSeconds(0.1f);
        push.CheckOverlap();
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
    public void DisableThisComponent()
    {
        this.enabled = false;
    }

    // Ŭ�� or ��ġ�� �޼ҵ��
    public void ClickDown()
    {
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //ī�޶���� ��ǥ�� �������������α��ϱ�
                                                                             //�ǳھȿ��� ���콺Ȥ�� ��ġ��ġ�� RectTransform ���ϱ�
        //UI���� collider�˻簡 �ȵǼ� gameObject�� ���ÿ� ��ȯ�ؼ� �Ⱥ��̴� ������ ��ħ�˻�
        //���������ǿ� push��ȯ�ϱ�(Collider �˻��ؼ� push��ư ��ġ���� Ȯ���ϱ�����)
        newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
        // StackFakePops.Push(newPush);
        //UI�� ��ġ�� push��ȯ(������ ���̴� push)
        newRectPush = Instantiate(RectPushPop, Input.mousePosition, Quaternion.identity);
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //��������Ʈ��ü
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = btnSprites[spriteIndex];
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = spriteIndex;
        // StackPops.Push(newRectPush);
        // pushpop Btn Parent ����
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    } //���콺Ŭ�� or ��ġ�� �� ����
    //private void Click()
    //{
    //    if (!isOnArea)
    //    {
    //        DestroyNewPush();
    //        return;
    //    }
    //    //�巡���ϴµ��� ���콺��ġ�� ����ȭ
    //    SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPosition);

    //    newRectPush.transform.position = localPosition + new Vector2(960, 540);
    //    if (newPush != null)
    //    {
    //        newPush.transform.position = SelectPositon;
    //    }
    //} // ���콺�巡�� or ��ġ���϶�
    public void ClickUp()
    {
        if (newPush == null) return;
        tempPushPop push = newPush.GetComponent<tempPushPop>(); // collider check GameObject
        
        push.RectPush = newRectPush;
        push.GetComponent<tempPushPop>().isSet = true; // �������� �� Ȯ��

        //isCheckOverLap = ture�Ͻ� �ٸ� Ǫ���˰� ��ġ���� Ȯ����
        push.isCheckOverlap = true;
        //���콺�� ���������Ʈ ���� ������ true , �ƴϸ� false -> Ǫ���˼�ġ�� ������Ʈ���� �ϰ��ϱ�����
        //StartCoroutine(CheckDelay(push));
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
            GameObject objs = StackFakePops.Pop();
            Destroy(objs);
            GameObject obj = StackPops.Pop(); // Queue���� ������Ʈ�� �ϳ��� ����
            Destroy(obj); // �ش� ������Ʈ�� �ı�
            PushPop.Instance.pushPopButton.Remove(obj);
        }
    }

    public void GetSpriteIndex(int index)
    {
        spriteIndex = index;
    }
 
}