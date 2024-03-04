using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image _myImage;
    private RectTransform _rect; //�巡���Ҷ� ������ ������Ʈ ��ġ
    private Vector3 _distance; //���콺 Ŭ������ �� ������Ʈ�� �״�� ��������ϱ����� ����� Vector��

    private CanvasGroup canvasGroup;
    private bool isFitPuzzle; //������ ������ �� ������� Ȯ���ϴ� bool
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
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }
    #endregion
    #region ���콺 �巡�׾� ��� ����
    public void OnBeginDrag(PointerEventData eventData)
    {//���� ������Ʈ �巡�׸� �����Ҷ� 1ȸ ȣ��

        AudioManager.instance.SetAudioClip_SFX(2, false);
        //Ŭ�������� ������Ʈ ��ġ �״�� �������� �ű������ ���
        _distance = (Vector3)eventData.position - _rect.position;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {//���������Ʈ �巡�� ���϶� �����Ӵ��� ȣ��
        //������ġ �״�� ���콺�̵� ���󰡱�
        _rect.position = eventData.position - (Vector2)_distance;
    }
    public void OnEndDrag(PointerEventData eventData)
    {//���������Ʈ �巡�׸� ������ �� 1ȸ ȣ��

        //������ ������� Ȯ���ϴ� bool��
        isFitPuzzle = puzzleLozic.checkdistance(_rect.position);
        if (!isFitPuzzle)
        {//������ ������ ��������
            AudioManager.instance.SetAudioClip_SFX(0, false);
            //������ġ�� �ʱ���ġ�� �ʱ�ȭ
            FailToSolvePuzzle();
            //_rect.position = _startPostion;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            //������ ��������
            AudioManager.instance.SetAudioClip_SFX(2, false);
            //��������ġ�� ��Ȯ�ϰ� ����
            _rect.position = puzzleLozic.frampPos.position;
            // ���İ� �ʱ�ȭ
            canvasGroup.alpha = 1f;
            // ������ ������ Ŭ�� �ȵǰ� ���
            canvasGroup.blocksRaycasts = false;
            //���⶧���� ����ī��Ʈ �÷��ֱ�
            puzzleLozic.successCount++;
            if (StageClear())
            {//������ ��������
                GameManager.Instance.pushPush.OnPuzzleSolved();
            }
        }
    }
    #endregion
    #region ������ �������� �޼ҵ� ( �������� , ����������)
    public void FailToSolvePuzzle() //������ ���������� ���������� ����ġ ��Ű�� �޼ҵ� (������ ��� ��Ʈ�������� �ѹ� �ҷ���)
    {
        // ������ Ư������ ���������� �̵���ŵ�ϴ�.
        float X = UnityEngine.Random.Range(puzzleLozic.failPiecePos.position.x - 100f, puzzleLozic.failPiecePos.position.x + 100f);
        float Y = UnityEngine.Random.Range(Screen.height / 5, Screen.height - Screen.height / 5);
        _rect.position = new Vector2(X, Y);
    }

    private bool StageClear() //������ ���⶧���� ��� ����µ� Ȯ���ϰ� bool���� ��ȯ��.
    {// ����ī��Ʈ == ���� ���� (ClearCount) �϶� Ŭ���� bool��ȯ

        //ClearCount = ���� �ǽ�(Sprite)�� ����
        if (puzzleLozic.successCount == puzzleLozic.ClearCount)
        {//Ŭ����
            return true;
        }
        else
        {//Ŭ����x
            return false;
        }
    }
    #endregion


}
