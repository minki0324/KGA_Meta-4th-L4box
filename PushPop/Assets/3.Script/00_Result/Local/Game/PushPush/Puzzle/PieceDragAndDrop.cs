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
    [SerializeField] private RectTransform pieceRectTrans; // �巡�� �� ������ ������Ʈ ��ġ
    [SerializeField] private CanvasGroup canvasGroup;
    private Vector3 distance; //���콺 Ŭ������ �� ������Ʈ�� �״�� ��������ϱ����� ����� Vector��
    private bool isFitPuzzle = false; // ������ �� true, �ƴ� �� false

    private void Awake()
    {
        puzzleLozic = PushPop.Instance.PushpushManager.puzzleManager;
    }

    private void Start()
    {
        PieceImage.alphaHitTestMinimumThreshold = 0.1f; // Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
    }

    public void OnBeginDrag(PointerEventData eventData)
    { // ���� �巡�� ���� ��
        AudioManager.Instance.SetAudioClip_SFX(2, false);
        distance = (Vector3)eventData.position - pieceRectTrans.position; // Ŭ�������� ������Ʈ ��ġ
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    { // ���� �巡�� ��
        pieceRectTrans.position = eventData.position - (Vector2)distance; // ��ġ ������ ���󰡱�
    }

    public void OnEndDrag(PointerEventData eventData)
    { // ���� �巡�� ���� ��
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
            pieceRectTrans.position = puzzleLozic.FramePos.position; // ������ ��ġ�� ����
            puzzleLozic.SuccessCount++;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;

            if (StageClear())
            { // ������ ���� ������ ��
                GameManager.Instance.NextMode?.Invoke(); // custom mode�� �Ѿ
            }
        }
    }
    #region Puzzle Clear and Fail
    private bool StageClear()
    { // ���� ������ üũ
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
    { // ���� ��ġ �ʱ�ȭ ������ ��� ��Ʈ�������� �ѹ� �ҷ���
        // ������ Ư������ ���������� �̵���ŵ�ϴ�.
        float posX = UnityEngine.Random.Range(puzzleLozic.FailPiecePos.position.x - 100f, puzzleLozic.FailPiecePos.position.x + 100f);
        float posY = UnityEngine.Random.Range(Screen.height / 5, Screen.height - Screen.height / 5);
        pieceRectTrans.position = new Vector2(posX, posY);
    }
    #endregion
}
