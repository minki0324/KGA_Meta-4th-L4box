using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{ // piece prefabs
    private PuzzleLozic puzzleLozic;
    public Image PieceImage;
    [SerializeField] private RectTransform pieceRectTrans; // 드래그 시 움직일 오브젝트 위치
    [SerializeField] private CanvasGroup canvasGroup;
    private Vector3 distance; //마우스 클릭했을 때 오브젝트가 그대로 따라오게하기위해 계산한 Vector값
    private bool isFitPuzzle = false; // 맞췄을 때 true, 아닐 때 false

    private void Awake()
    {
        puzzleLozic = PushPop.Instance.PushpushManager.puzzleManager;
    }

    private void Start()
    {
        PieceImage.alphaHitTestMinimumThreshold = 0.1f; // Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
    }

    public void OnBeginDrag(PointerEventData eventData)
    { // 퍼즐 드래그 시작 시
        AudioManager.Instance.SetAudioClip_SFX(2, false);
        distance = (Vector3)eventData.position - pieceRectTrans.position; // 클릭했을때 오브젝트 위치
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    { // 퍼즐 드래그 중
        pieceRectTrans.position = eventData.position - (Vector2)distance; // 터치 포지션 따라가기
    }

    public void OnEndDrag(PointerEventData eventData)
    { // 퍼즐 드래그 종료 시
        isFitPuzzle = puzzleLozic.CheckDistance(pieceRectTrans.position);
        if (!isFitPuzzle)
        { // fit fail
            AudioManager.Instance.SetAudioClip_SFX(0, false);
            FailToSolvePuzzle();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        else
        { // fit clear
            AudioManager.Instance.SetAudioClip_SFX(2, false);
            pieceRectTrans.position = puzzleLozic.FramePos.position; // 프레임 위치로 보정
            puzzleLozic.SuccessCount++;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;

            if (StageClear())
            { // 퍼즐을 전부 맞췄을 시
                GameManager.Instance.NextMode?.Invoke(); // custom mode로 넘어감
            }
        }
    }
    #region Puzzle Clear and Fail
    private bool StageClear()
    { // 퍼즐 맞춘지 체크
        if (puzzleLozic.SuccessCount.Equals(puzzleLozic.ClearCount))
        { // Clear
            return true;
        }
        else
        { // Fail
            return false;
        }
    }

    public void FailToSolvePuzzle()
    { // 퍼즐 위치 초기화 버블을 모두 터트렸을때도 한번 불러줌
        // 오른쪽 특정기준 랜덤값으로 이동시킵니다.
        float posX = UnityEngine.Random.Range(puzzleLozic.FailPiecePos.position.x - 100f, puzzleLozic.FailPiecePos.position.x + 100f);
        float posY = UnityEngine.Random.Range(Screen.height / 5, Screen.height - Screen.height / 5);
        pieceRectTrans.position = new Vector2(posX, posY);
    }
    #endregion
}
