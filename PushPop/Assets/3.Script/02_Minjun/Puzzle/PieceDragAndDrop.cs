using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _myImage;
    private RectTransform _rect; //드래그할때 움직일 오브젝트 위치
    private Vector3 _distance; //마우스 클릭했을 때 오브젝트가 그대로 따라오게하기위해 계산한 Vector값

    private CanvasGroup canvasGroup;
    private bool isFitPuzzle; //퍼즐을 놓았을 때 맞췄는지 확인하는 bool
    private PuzzleLozic puzzleLozic;


    #region UnityCallback
    private void Awake()
    {
        TryGetComponent(out _rect);
        TryGetComponent(out canvasGroup);
        puzzleLozic = GameManager.Instance.pushPush.puzzle;

    }
    private void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }
    #endregion
    #region 마우스 드래그앤 드랍 로직
    public void OnBeginDrag(PointerEventData eventData)
    {//현재 오브젝트 드래그를 시작할때 1회 호출

        AudioManager.instance.SetAudioClip_SFX(2, false);
        //클릭했을때 오브젝트 위치 그대로 포지션을 옮기기위한 계산
        _distance = (Vector3)eventData.position - _rect.position;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {//현재오브젝트 드래그 중일때 프레임단위 호출
        //현재위치 그대로 마우스이동 따라가기
        _rect.position = eventData.position - (Vector2)_distance;
    }
    public void OnEndDrag(PointerEventData eventData)
    {//현재오브젝트 드래그를 종료할 때 1회 호출

        //퍼즐을 맞췄는지 확인하는 bool값
        isFitPuzzle = puzzleLozic.checkdistance(_rect.position);
        if (!isFitPuzzle)
        {//퍼즐을 맞추지 못했을때
            AudioManager.instance.SetAudioClip_SFX(0, false);
            //퍼즐위치는 초기위치로 초기화
            FailToSolvePuzzle();
            //_rect.position = _startPostion;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            //퍼즐을 맞췄을때
            AudioManager.instance.SetAudioClip_SFX(2, false);
            //프레임위치로 정확하게 보정
            _rect.position = puzzleLozic.frampPos.position;
            // 알파값 초기화
            canvasGroup.alpha = 1f;
            // 성공한 퍼즐은 클릭 안되게 블록
            canvasGroup.blocksRaycasts = false;
            //맞출때마다 성공카운트 올려주기
            puzzleLozic.successCount++;
            if (StageClear())
            {//퍼즐모두 맞췄을때
                GameManager.Instance.pushPush.OnPuzzleSolved();
            }
        }
    }
    #endregion
    #region 퍼즐을 놓았을때 메소드 ( 맞췄을때 , 못맞췄을때)
    public void FailToSolvePuzzle() //퍼즐을 못맞췄을때 오른쪽으로 재위치 시키는 메소드 (버블을 모두 터트렸을때도 한번 불러줌)
    {
        // 오른쪽 특정기준 랜덤값으로 이동시킵니다.
        float X = UnityEngine.Random.Range(puzzleLozic.failPiecePos.position.x - 100f, puzzleLozic.failPiecePos.position.x + 100f);
        float Y = UnityEngine.Random.Range(Screen.height / 5, Screen.height - Screen.height / 5);
        _rect.position = new Vector2(X, Y);
    }

    private bool StageClear() //퍼즐을 맞출때마다 모두 맞췄는데 확인하고 bool값을 반환함.
    {// 성공카운트 == 퍼즐 갯수 (ClearCount) 일때 클리어 bool반환

        //ClearCount = 퍼즐 피스(Sprite)의 갯수
        if (puzzleLozic.successCount == puzzleLozic.ClearCount)
        {//클리어
            return true;
        }
        else
        {//클리어x
            return false;
        }
    }
    #endregion


}
