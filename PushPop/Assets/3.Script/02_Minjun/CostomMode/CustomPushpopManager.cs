using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPushpopManager : MonoBehaviour
{
    [Header("Custom Mode")]
    public GameObject CustomMode = null;
    private Vector3 selectPositon; //ī�޶󿡼����̴� world ������ ������ Vector
    [SerializeField] private GameObject pushPop; // OverLap�˻��ϴ� Ǫ����(gameObject)
    [SerializeField] private GameObject RectPushPop; //UI Ǫ����
    public TMP_Text StageTitle = null;

    [Header("Result Panel")]
    public GameObject ResultPanel = null; 
    public TMP_Text ResultText = null;
    public Image ResultImage = null;

    [Header("PushPopObject")] 
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public Stack<GameObject> StackPops = new Stack<GameObject>(); //UI�� ���̴� ��ư��� ����
    public Stack<GameObject> StackFakePops = new Stack<GameObject>(); //OverLap �˻縦 �ϱ����� gameObject ����
    public GameObject puzzleBoard; //������ ��ư ������ִ� GameObject
    public Sprite[] pushPopButtonSprite; //color �ٲٱ����� �迭
    public int SpriteIndex = 0;
    public int currentCreatIndex = 0;
    [SerializeField] private FramePuzzle framePuzzle;

    public void SetActiveCount()
    {
        GameManager.Instance.buttonActive = StackPops.Count;
    }

    #region ��ư ���� �޼ҵ�
    // Ŭ�� or ��ġ�� �޼ҵ��
    public void ClickDown()
    {
        selectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //ī�޶���� ��ǥ�� �������������α��ϱ�
        //���������ǿ� push��ȯ�ϱ�(Collider �˻��ؼ� push��ư ��ġ���� Ȯ���ϱ�����)
        newPush = Instantiate(pushPop, selectPositon, Quaternion.identity);
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
        popImage.sprite = pushPopButtonSprite[SpriteIndex]; // ���� ������ ��������Ʈ �̹����� ����
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.spriteIndex = SpriteIndex;
        // pushpop Btn Parent ����
        newRectPush.transform.SetParent(puzzleBoard.transform);
        newRectPush.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // ������ ����� ������ Circle(�ݶ��̴��˻�) �����ϵ� �ٲ� �������� 1.3��� �ٲ��ּ��� 

        AudioManager.Instance.SetAudioClip_SFX(3, false);

    }
    #endregion
    #region DecoPanel Button Method
    public void ReturnButton()
    { // �ǵ�����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (StackFakePops.Count.Equals(0)) return;
        GameObject lastFakeStack = StackFakePops.Pop();
        Destroy(lastFakeStack);
        GameObject lastStack = StackPops.Pop();
        Destroy(lastStack);

        PushPop.Instance.pushPopButton.Remove(lastStack);
    }

    public void ResetButton()
    { // ��ư ��� �����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
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

    public void RetryCustom()
    { // �ٽ� �ٹ̱�
        CustomMode.SetActive(true);
        StageTitle.text = "�� ������� �׸��� �ٸ纸��!";
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
    { // ���� ����
        // enabled = false;
        GameManager.Instance.IsCustomMode = false;
        // ��.�� ��ư Active True
        framePuzzle.ImageAlphaHitSet(0f);
        foreach (var btn in StackPops)
        {
            btn.GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
    public void DestroyChildren()
    { // ������ ��ư ����
        foreach (Transform child in puzzleBoard.transform)
        {
            Destroy(child.gameObject);
        }
    }
}