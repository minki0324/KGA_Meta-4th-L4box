using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CostomPushpopManager : MonoBehaviour
{

    private Vector3 SelectPositon; //ī�޶󿡼����̴� world ������ ������ Vector
    private Vector2 localPosition; //UI�� ��ġ�� ���콺��ġ ������ Vector
    [SerializeField] private RectTransform rectTransform; // UI�� ���콺��ġ ���ϱ����� ���� �ǳ�
    [SerializeField] private GameObject pushPop; // OverLap�˻��ϴ� Ǫ����(gameObject)
    [SerializeField] private Transform canvas; // UIǪ���� ��ȯ�ϰ� ��ӽ�ų ĵ����
    [SerializeField] private GameObject RectPushPop; //UI Ǫ����
    private GameObject newPush; //���� ��ȯ���� Ǫ����
    private GameObject newRectPush;//���� ��ȯ���� Ǫ����
    public bool isCanMakePush; //FramePuzzle ���� ���޹��� bool���� ��ġ�Ҷ� ��ư�� ������Ʈ�����ִ��� �Ǵ��ϱ����� ��.
    public bool isOnArea;
    public GameObject puzzleBoard;
    void Update()
    {// �ȵ���̵�� ��������
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved))
                    {

                    }
                    //��ġ�� ������������ ���� ��ġ�� �����մϴ�.(UI�� ����)
                }
            }

        }
        else
        { // window , editor
          //���콺Ŭ���ϰ� Ŭ����ġ�� UI�� �ƴҶ�
            WindowPlatform();
        }
    }
    private IEnumerator CheckDelay(tempPushPop push)
    {
        yield return new WaitForSeconds(0.1f);
        push.CheckOverlap(SelectPositon);
    }
    private void WindowPlatform()
    {
        Debug.Log(isOnArea);
      
        if (Input.GetMouseButtonDown(0) && isOnArea)
        {
            Debug.Log("���콺Ŭ��");
            SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); //ī�޶���� ��ǥ�� �������������α��ϱ�
                                                                                 //�ǳھȿ��� ���콺Ȥ�� ��ġ��ġ�� RectTransform ���ϱ�
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPosition);
            //���������� �� UI�� ��ġ ����
            Vector2 newLocalPosition = localPosition + new Vector2(960, 540);
            //UI���� collider�˻簡 �ȵǼ� gameObject�� ���ÿ� ��ȯ�ؼ� �Ⱥ��̴� ������ ��ħ�˻�
            //���������ǿ� push��ȯ�ϱ�(Collider �˻��ؼ� push��ư ��ġ���� Ȯ���ϱ�����)
            newPush = Instantiate(pushPop, SelectPositon, Quaternion.identity);
            //UI�� ��ġ�� push��ȯ(������ ���̴� push)
            newRectPush = Instantiate(RectPushPop, newLocalPosition, Quaternion.identity);
            //Canvas�� ����ؼ� ���̰��ϱ�
            newRectPush.transform.SetParent(puzzleBoard.transform);

        }
        if (newRectPush == null) return;
        if (Input.GetMouseButton(0))
        {
            if (!isOnArea)
            {
                DestroyNewPush();
                return;
            }
            //�巡���ϴµ��� ���콺��ġ�� ����ȭ
            SelectPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPosition);
           
            newRectPush.transform.position = localPosition + new Vector2(960, 540);
            if (newPush != null)
            {
                newPush.transform.position = SelectPositon;
            }
        }
        if (Input.GetMouseButtonUp(0) && isOnArea)
        {

            tempPushPop push = newPush.GetComponent<tempPushPop>();
            push.RectPush = newRectPush;
            //isCheckOverLap = ture�Ͻ� �ٸ� Ǫ���˰� ��ġ���� Ȯ����
            push.isCheckOverlap = true;
            //���콺�� ���������Ʈ ���� ������ true , �ƴϸ� false -> Ǫ���˼�ġ�� ������Ʈ���� �ϰ��ϱ�����
            push.isCanMakePush = isCanMakePush;
            //todo ��ư interactable ��ġ�Ҷ� ���ְ� ��ġ�Ϸ�Ǹ� �ٽ� ���ֱ�
            //OverLap üũ�� ��ģ�ٸ� Destroy , �ƴϸ� ��ġ ����.
            StartCoroutine(CheckDelay(push));
            newRectPush = null;
            newPush = null;
        }
    }
    public void DestroyNewPush()
    {
        if(newPush !=null && newRectPush != null)
        {
            Destroy(newPush);
            Destroy(newRectPush);
            newPush = null;
            newRectPush = null;
            return;
        }
    }
    public void DisableThisComponent()
    {
        this.enabled = false;
    }

}
