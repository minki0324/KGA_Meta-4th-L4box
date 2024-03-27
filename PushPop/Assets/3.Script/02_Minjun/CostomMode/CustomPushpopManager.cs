using System;
using System.Collections;
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
    [SerializeField] private GameObject RectPushPop; // UI Ǫ����
    public TMP_Text StageTitle = null;
    public GameObject ReDecoButton = null;
    public GameObject DecoPanel = null;
    [SerializeField] private RectTransform customAreaRectTrans = null;
    private float customPosX = 0f;
    [SerializeField] private bool buttonDownDelay = false;
    private Coroutine buttonDownCoroutine;

    [Header("Result Panel")]
    public GameObject ResultPanel = null; 
    public TMP_Text ResultText = null;
    public Image ResultImage = null;

    [Header("PushPop Object")] 
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public GameObject puzzleBoard; //������ ��ư ������ִ� GameObject
    public Sprite[] pushPopButtonSprite; //color �ٲٱ����� �迭
    public int SpriteIndex = 0;
    public int currentCreatIndex = 0;
    [SerializeField] private FramePuzzle framePuzzle;

    private void Awake()
    {
        customPosX = customAreaRectTrans.localPosition.x;
    }

    #region DecoPanel Button Method
    private IEnumerator ClickDown_Co()
    {
        AudioManager.Instance.SetAudioClip_SFX(3, false);

        selectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ��ġ �� ī�޶� ���� ��ǥ

        // pushpop collider setting
        newPush = Instantiate(pushPop, selectPositon, Quaternion.identity);
        newPush.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f); // newRectPush ������ �°� ����
        // pushpop button setting
        newRectPush = Instantiate(RectPushPop, Input.mousePosition, Quaternion.identity);
        TempPushPop push = newPush.GetComponent<TempPushPop>(); // collider check GameObject

        // pushpop overLap �˻縦 ���� ��ư������ index �ο� index�� �񱳸� ���� ���� ���� �Ǵ�
        push.createIndex = currentCreatIndex;
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
        yield return new WaitForSeconds(0.3f);
        buttonDownCoroutine = null;
    }

    public void ClickDown()
    { // button ����
        if (buttonDownCoroutine != null) return;
        if (Input.touchCount > 1)
        {
            // ��ġ�� 1���� �ʰ��� ��� �Լ� ����
            return;
        }
        buttonDownCoroutine = StartCoroutine(ClickDown_Co());
    }

    public void ReturnButton()
    { // �ǵ�����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        if (PushPop.Instance.StackFakePops.Count.Equals(0)) return;
        GameObject lastFakeStack = PushPop.Instance.StackFakePops.Pop();
        Destroy(lastFakeStack);
        GameObject lastStack = PushPop.Instance.StackPops.Pop();
        Destroy(lastStack);

        PushPop.Instance.pushPopButton.Remove(lastStack);
    }

    public void ResetButton()
    { // ��ư ��� �����
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        ResetStack();
    }

    public void CustomModeInit()
    {
        ResetStack();
        customAreaRectTrans.localPosition = new Vector3(customPosX, customAreaRectTrans.localPosition.y, customAreaRectTrans.localPosition.z);
        ReDecoButton.SetActive(false);
        DecoPanel.SetActive(true);
        CustomMode.SetActive(false);
    }

    private void ResetStack()
    { // button ����
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
    { // �ٽ� �ٹ̱�
        AudioManager.Instance.SetCommonAudioClip_SFX(3);
        GameManager.Instance.IsCustomMode = true;
        DecoPanel.SetActive(true);
        ReDecoButton.SetActive(false);
        StageTitle.text = "�� ������� �׸��� �ٸ纸��!";
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
    { // ���� ����
        GameManager.Instance.IsCustomMode = false;
        DecoPanel.SetActive(false);
        ReDecoButton.SetActive(true);
        framePuzzle.ImageAlphaHitSet(0f);

        customAreaRectTrans.localPosition = new Vector3(0f, customAreaRectTrans.localPosition.y, customAreaRectTrans.localPosition.z);

        foreach (GameObject btn in PushPop.Instance.StackPops)
        {
            btn.GetComponent<Image>().raycastTarget = true;
        }
    }
    #endregion
}