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
    [SerializeField] private GameObject overlabCheckPushPop; // OverLap�˻��ϴ� Ǫ����(gameObject)
    [SerializeField] private GameObject rectPushPop; // UI Ǫ����
    [SerializeField] private GameObject reDecoButton;
    [SerializeField] private GameObject decoPanel;
    [SerializeField] private RectTransform customAreaRectTrans;
    private float customPosX = 0f;
    private Vector3 selectPositon; //ī�޶󿡼����̴� world ������ ������ Vector
    private Coroutine buttonDownCoroutine;

    [Header("Result Panel")]
    public GameObject ResultPanel = null; 
    public TMP_Text ResultText = null;
    public Image ResultImage = null;

    [Header("PushPop Object")] 
    [SerializeField] private FramePuzzle framePuzzle;
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public GameObject PuzzleBoard; //������ ��ư ������ִ� GameObject
    public Sprite[] PushPopButtonSprite; //color �ٲٱ����� �迭
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

        selectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ��ġ �� ī�޶� ���� ��ǥ

        // pushpop collider setting
        newPush = Instantiate(overlabCheckPushPop, selectPositon, Quaternion.identity);
        newPush.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f); // newRectPush ������ �°� ����
        // pushpop button setting
        newRectPush = Instantiate(rectPushPop, Input.mousePosition, Quaternion.identity);
        OverlabCheckPushPop push = newPush.GetComponent<OverlabCheckPushPop>(); // collider check GameObject

        // pushpop overLap �˻縦 ���� ��ư������ index �ο� index�� �񱳸� ���� ���� ���� �Ǵ�
        push.createIndex = currentCreatIndex;
        currentCreatIndex++;
        push.OverlabCheckCircle = newRectPush;
        PushPop.Instance.pushPopButton.Add(newRectPush);

        //��������Ʈ��ü
        Image popImage = newRectPush.GetComponent<Image>();
        popImage.sprite = PushPopButtonSprite[SpriteIndex]; // ���� ������ ��������Ʈ �̹����� ����
        PushPopButton pop = newRectPush.GetComponent<PushPopButton>();
        pop.SpriteIndex = SpriteIndex;

        // pushpop Btn Parent ����
        newRectPush.transform.SetParent(PuzzleBoard.transform);
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
        reDecoButton.SetActive(false);
        decoPanel.SetActive(true);
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
        decoPanel.SetActive(true);
        reDecoButton.SetActive(false);
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