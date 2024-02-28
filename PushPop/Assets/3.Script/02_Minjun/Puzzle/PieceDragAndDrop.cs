using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private PuzzleLozic puzzleLozic;

    private Image _myImage;
    private RectTransform _rect; //드래그할때 움직일 오브젝트 위치
    private Vector3 _startPostion; //프레임에 맞지 않았을때 최초 위치로 돌아가게하기위한 위치
    private Vector3 _distance; //마우스 클릭했을 때 오브젝트가 그대로 따라오게하기위해 계산한 Vector값

    private CanvasGroup canvasGroup;
    private bool isFitPuzzle; //퍼즐을 놓았을 때 맞췄는지 확인하는 bool


    private void Awake()
    {
        TryGetComponent(out _rect);
        TryGetComponent(out canvasGroup);
        _startPostion = _rect.position;
    }
    private void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {//현재 오브젝트 드래그를 시작할때 1회 호출

        if (puzzleLozic == null)
        {
            puzzleLozic = FindObjectOfType<PuzzleLozic>();
        }

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
            {
                Debug.Log("퍼즐을 모두 맞췄어요! 잘했어요!!");


                AudioManager.instance.SetAudioClip_SFX(1, false);
                puzzleLozic.onPuzzleClear?.Invoke();
                puzzleLozic.successCount = 0;
            }
        }
    }

    private void FailToSolvePuzzle()
    {
        float X = UnityEngine.Random.Range(puzzleLozic.failPiecePos.position.x - 100f, puzzleLozic.failPiecePos.position.x + 100f);
        float Y = UnityEngine.Random.Range(Screen.height / 5, Screen.height - Screen.height / 5);
        //Vector2 movePos = new Vector2(X, Y);
        _rect.position = new Vector2(X, Y);
        //_rect.position =Vector2.MoveTowards(_rect.position, movePos, 10 * Time.deltaTime);
    }

    private bool StageClear()
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
   
}
