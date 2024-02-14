using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _myImage;
    private CostomPushpopManager costomPushpop;
    // Start is called before the first frame update
    void Start()

    {
        costomPushpop = FindObjectOfType<CostomPushpopManager>();
        _myImage = GetComponent<Image>();
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //�������� ���콺�� ������ True�� �����ϱ�
        costomPushpop.isCanMakePush = true;
        costomPushpop.isOnArea = true;
        Debug.Log("���콺����" + costomPushpop.isCanMakePush);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        costomPushpop.isCanMakePush = false;
        Debug.Log("���콺����" + costomPushpop.isCanMakePush);
    }
    public void PushButtonActiveOn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // i��° �ڽ� ����� Image ������Ʈ�� raycastTarget�� true�� �����մϴ�.
            transform.GetChild(i).GetComponent<Image>().raycastTarget = true;
        }
    }
}