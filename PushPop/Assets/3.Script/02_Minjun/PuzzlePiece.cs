using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        //button = GetComponent<Button>();
        /*button.*/GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }












    //private Puzzle puzzle;
    //private Vector2 StartPuzzlePos;
    //private Vector2 FramePos; //�̽�ũ��Ʈ�� �ǽ��� ������ϴ� ��������ġ
    //private float PuzzleJudgmentDistance = 0.1f; // ������ �������� ��Ȯ�� 
    //private Vector2 _offSet;
    //private void Awake()
    //{
    //    StartPuzzlePos = transform.position;
    //    puzzle = transform.root.GetComponent<Puzzle>();
    //}
    //void Start()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        puzzle.Piece.Add(transform.GetChild(i).gameObject);
    //    }

    //}
    //public void ResultPos()
    //{
    //    if(FramePos == null)
    //    {
    //        foreach (GameObject frame in puzzle.Frame)
    //        {
    //            if(frame.name == gameObject.name)
    //            {
    //                FramePos = frame.transform.position;
    //            }
    //        }

    //    }

    //    CheckDistance();
    //}

    //public void CheckDistance()
    //{
    //    if (Vector2.Distance(transform.position, FramePos) < PuzzleJudgmentDistance)
    //    {
    //        //���������� ������ ��ġ ��Ȯ�ϰ� ����
    //        transform.position = FramePos;
    //    }
    //    else
    //    {
    //        //������������ ���� ������ ������ġ�� �ٽ� �̵�
    //        Vector2.MoveTowards(transform.position, StartPuzzlePos, 20 * Time.deltaTime);
    //    }
    //}
    //private void OnMouseDrag()
    //{
    //    Debug.Log("11");
    //    _offSet = (Vector2)transform.position - mousePositon();
    //    transform.position +=(Vector3) _offSet;

    //}
    //private Vector2 mousePositon()
    //{
    //    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}
}
