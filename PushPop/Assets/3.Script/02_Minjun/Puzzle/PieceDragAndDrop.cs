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
    private RectTransform _rect; //�巡���Ҷ� ������ ������Ʈ ��ġ
    private Vector3 _startPostion; //�����ӿ� ���� �ʾ����� ���� ��ġ�� ���ư����ϱ����� ��ġ
    private Vector3 _distance; //���콺 Ŭ������ �� ������Ʈ�� �״�� ��������ϱ����� ����� Vector��

    private CanvasGroup canvasGroup;
    private bool isFitPuzzle; //������ ������ �� ������� Ȯ���ϴ� bool


    private void Awake()
    {
        TryGetComponent(out _rect);
        TryGetComponent(out canvasGroup);
        _startPostion = _rect.position;
    }
    private void Start()
    {
        _myImage = GetComponent<Image>();
        //Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
        _myImage.alphaHitTestMinimumThreshold = 0.1f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {//���� ������Ʈ �巡�׸� �����Ҷ� 1ȸ ȣ��

        if (puzzleLozic == null)
        {
            transform.root.TryGetComponent(out puzzleLozic);
        }
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

            //������ġ�� �ʱ���ġ�� �ʱ�ȭ
            _rect.position = _startPostion;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

        }
        else
        {
            //������ ��������
            //��������ġ�� ��Ȯ�ϰ� ����
            _rect.position = puzzleLozic.frampPos.position;
            // ���İ� �ʱ�ȭ
            canvasGroup.alpha = 1f;
            // ������ ������ Ŭ�� �ȵǰ� ����
            canvasGroup.blocksRaycasts = false;
            //���⶧���� ����ī��Ʈ �÷��ֱ�
            puzzleLozic.successCount++;
            if (StageClear())
            {
                Debug.Log("���������� Ŭ���� �߽��ϴ�! ���߾��!!");
            }
        }
    }
    private bool StageClear()
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
}