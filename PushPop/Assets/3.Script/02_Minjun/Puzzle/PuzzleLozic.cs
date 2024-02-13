using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PuzzleLozic : MonoBehaviour
{
    [Header("���ߴ� ���� ������Ʈ")]
    public GameObject PieceObject;
    [Header("������ ������Ʈ")]
    public GameObject FrameObject;
    [Header("�����ӻ�ӽ�ų ������Ʈ")] 
    public GameObject FrameParent;
    [Header("�ǽ���ӽ�ų ������Ʈ")]
    public GameObject PieceParent;
    [Header("������ ���� ��ġ")]
    public Transform frampPos;
    [Header("�ǽ��� ���� ��ġ")] //�ӽ�
    public Transform[] piecePos;
    private float puzzleJudgmentDistance = 30; // ���� ���� �Ÿ�.
    public List<Puzzle> puzzles = new List<Puzzle>(); //��� ���� ������ ��Ƴ��� ����Ʈ
    public int ClearCount=0; //������ϴ� ���� ����
    public int successCount= 0; //���� ����
    private Puzzle currentPuzzle; //Player�� �� ���� ����
    public bool checkdistance(Vector3 currentPosition )
    {//������ �������� ������ϴ� ��ġ�� ������ġ ��
        if (Vector3.Distance(currentPosition, frampPos.position) < puzzleJudgmentDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectPuzzleButton(string name)
    {//��ư���� �޼ҵ�
        foreach (var Kind in puzzles)
        {
            //�Ű������ι��� String�� List�� ����ִ� ������� Enum.toString()�� ���� ���� ã��
            if (Kind.puzzleKind.ToString() == name)
            {
                currentPuzzle = Kind;
                //Ŭ����ī��Ʈ�� ���񰹼� (Sprite)
                ClearCount = currentPuzzle.sprites.Length;
                SettingPuzzle();
                break;
            }
        }
        if (currentPuzzle == null)
        {
            Debug.Log("��ġ�ϴ� ������ �����ϴ�. ScriptableObject�� �߰��� �ּ���");
        }
       
    }
    private void SettingPuzzle()
    {//������ ������� ���ص� ��ġ�� ���� ���� , Sprite ����� , SetParent�� ������ ����

        for (int i = 0; i < currentPuzzle.sprites.Length; i++)
        {
            GameObject newFramePuzzle =  Instantiate(FrameObject, frampPos.position ,Quaternion.identity);
            newFramePuzzle.transform.SetParent(FrameParent.transform);
            newFramePuzzle.GetComponent<Image>().sprite = currentPuzzle.sprites[i];
        }
        for (int i = 0; i < currentPuzzle.sprites.Length; i++)
        {
            GameObject newPiecePuzzle = Instantiate(PieceObject, piecePos[i].position, Quaternion.identity);
            newPiecePuzzle.transform.SetParent(PieceParent.transform);
            newPiecePuzzle.GetComponent<Image>().sprite = currentPuzzle.sprites[i];
        }
    }

}
