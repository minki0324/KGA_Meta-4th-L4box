using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour,  IPointerDownHandler 
{ //��ư��ġ�ϴ� ������ ��ũ��Ʈ.
    //������ �������� Ŭ�� or �ٿ� �ϰԲ���
    public Image FrameImage;

    void Start()
    {
        FrameImage = GetComponent<Image>();
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
        ImageAlphaHitSet(0.1f);
    }
    #region ��ư ��ġ, �̹��� ���ĺκ� ���� �޼ҵ�

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsCustomMode) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began))
        {
            PushPop.Instance.pushpushManager.customManager.ClickDown();
        }
    }

    public void ImageAlphaHitSet(float value)
    {
        FrameImage.alphaHitTestMinimumThreshold = value;
    }
    #endregion
}
