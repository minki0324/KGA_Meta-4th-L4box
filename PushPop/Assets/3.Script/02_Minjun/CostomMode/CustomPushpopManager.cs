using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPushpopManager : MonoBehaviour
{

    private Vector3 SelectPositon; //ī�޶󿡼����̴� world ������ ������ Vector
    [SerializeField] private GameObject pushPop; // OverLap�˻��ϴ� Ǫ����(gameObject)
    [SerializeField] private GameObject RectPushPop; //UI Ǫ����
  
    [Header("result")]
    public GameObject result; 
    public TMP_Text resultText;
    public Image resultImage;
    [Header("PushPopObject")] 
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public Stack<GameObject> StackPops = new Stack<GameObject>(); //UI�� ���̴� ��ư��� ����
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); //OverLap �˻縦 �ϱ����� gameObject ����
    public GameObject puzzleBoard; //������ ��ư ������ִ� GameObject
    [SerializeField] private Sprite[] btnSprites; //color �ٲٱ����� �迭
    public Action onCustomEnd;
    public bool isCustomMode;
    public int spriteIndex = 0;
    public int currentCreatIndex = 0;
    
    [SerializeField] private FramePuzzle framePuzzle;
    private void Awake()
    {
        onCustomEnd += EndCustom;//Ŀ���Ҹ�� ����� ������Ʈ ����
        GameManager.Instance.pushPush.onPushPushGameEnd += BtnAllClear;

    }
    public void SetActiveCount()
    {
        GameManager.Instance.buttonActive = StackPops.Count;
    }
    #region ��ư ���� �޼ҵ�
    // Ŭ�� or ��ġ�� �޼ҵ��
    public void ClickDown()
    {
        SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //ī�޶���� ��ǥ�� �������������α��ϱ�
        //���������ǿ� push��ȯ�ϱ�(Collider �˻��ؼ� push��ư ��ġ���� Ȯ���ϱ�����)
        newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
        newPush.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f); // newRectPush������ �°� �������ּ��� �Ʒ����.
        //UI�� ��ġ�� push��ȯ(������ ���̴� push)
        newRectPush = Instantiate(RectPushPop, Input.mousePosition, Quaternion.identity);
        tempPushPop push = newPush.GetComponent<tempPushPop>(); // collider check GameObject
        //Ǫ���� overLap�˻縦 ���� ��ư������ index �ο� == ��ư�鳢���� TriggerStay���� �������� index�� ���Ͽ� �������������� �Ǵ���
        push.creatIndex = currentCreatIndex;
        currentCreatIndex++;
        push.RectPush = newRectPush;
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //��������Ʈ��ü
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = btnSprites[spriteIndex]; // ���� ������ ��������Ʈ �̹����� ����
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = spriteIndex;
        // pushpop Btn Parent ����
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // ������ ����� ������ Circle(�ݶ��̴��˻�) �����ϵ� �ٲ� �������� 1.3��� �ٲ��ּ��� 

        AudioManager.instance.SetAudioClip_SFX(3, false);

    }
    #endregion
    #region DecoPanel ��ư�޼ҵ�
    public void ReturnBtn()//��ư �ǵ�����
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
    } //��ư ��� ����� (���¹�ư���� , ���������ݹ��̺�Ʈ ����)
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
    } //�ٽ� �ٹ̱�
    public void EndCustom() //���� ���� �ݹ��̺�Ʈ �߰��޼ҵ�
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