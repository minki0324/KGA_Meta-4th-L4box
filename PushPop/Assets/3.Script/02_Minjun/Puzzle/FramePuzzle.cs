using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour,  IPointerDownHandler 
{//��ư��ġ�ϴ� ������ ��ũ��Ʈ.
 //������ �������� Ŭ�� or �ٿ� �ϰԲ���

    public Image _myImage;

    #region UnityCallback
    void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
        ImageAlphaHitSet(0.1f);
    }
    #endregion

    #region ��ư��ġ , �̹��� ���ĺκ� ���� �޼ҵ�

    public void OnPointerDown(PointerEventData eventData)
    { //
        if (!GameManager.Instance.pushPush.custom.isCustomMode) return;
        GameManager.Instance.pushPush.custom.ClickDown();
    }
    public void ImageAlphaHitSet(float value)
    {
        _myImage.alphaHitTestMinimumThreshold = value;
    }
    #endregion

}
