using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FramePuzzle : MonoBehaviour,  IPointerDownHandler 
{ //버튼설치하는 보드판 스크립트.
    //보드판 위에서만 클릭 or 다운 하게끔함
    public Image FrameImage;

    void Start()
    {
        FrameImage = GetComponent<Image>();
        //Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        ImageAlphaHitSet(0.1f);
    }
    #region 버튼 설치, 이미지 알파부분 무시 메소드

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
