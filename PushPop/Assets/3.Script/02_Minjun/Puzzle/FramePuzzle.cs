using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour,  IPointerDownHandler 
{//버튼설치하는 보드판 스크립트.
 //보드판 위에서만 클릭 or 다운 하게끔함

    public Image _myImage;

    #region UnityCallback
    void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        ImageAlphaHitSet(0.1f);
    }
    #endregion

    #region 버튼설치 , 이미지 알파부분 무시 메소드

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
