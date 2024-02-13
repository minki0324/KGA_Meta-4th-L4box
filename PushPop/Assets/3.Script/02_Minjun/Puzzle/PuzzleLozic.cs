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
    [Header("�����ӽ�ų ������Ʈ")] 
    public GameObject PuzzleParent;

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
            Debug.Log(frampPos.position);
            return true;
        }
        else
        {
            Debug.Log(frampPos.position);
            return false;
        }
    }

    public void SelectPuzzleButton(int PuzzleIDIndex)
    {//��ư���� �޼ҵ�
        foreach (var Kind in puzzles)
        {
            //�Ű������ι��� String�� List�� ����ִ� ������� Enum.toString()�� ���� ���� ã��
            if (Kind.PuzzleID == PuzzleIDIndex)
            {
                currentPuzzle = Kind;
                //todo ���ӸŴ����� �������� ������...


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
    {//������ ���� ������,���� ����
        PuzzleInstantiate(FrameObject, frampPos.position, currentPuzzle.shadow);
        for (int i = 0; i < currentPuzzle.sprites.Length; i++)
        {
            PuzzleInstantiate(PieceObject, piecePos[i].position, currentPuzzle.sprites[i]);
        }
    }
    private void PuzzleInstantiate(GameObject puzzle , Vector3 position , Sprite puzzleSprite)
    {//�������
        GameObject newPiecePuzzle = Instantiate(puzzle, position, Quaternion.identity);
        PuzzleSetting(newPiecePuzzle, puzzleSprite);
    }

    private void PuzzleSetting(GameObject puzzle , Sprite sprite)
    {//UI ĵ������ ���, ������ �������� �־��ֱ� , ����ũ�� ����.
        puzzle.transform.SetParent(PuzzleParent.transform); //ĵ�����ȿ� ���
        Image frameImage = puzzle.GetComponent<Image>(); 
        frameImage.sprite = sprite; //���� �����ֱ�
        frameImage.preserveAspect =true; //���������� ����
    }
    public void DestroyChildren()
    {//������ �Ϸ������� �������ִ� ���� �����ϱ����� �޼ҵ�
        foreach (Transform child in PuzzleParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void CraetBoard()
    {//����Ϸ��ϰ� ���� ���� ������Ʈ �������ֱ�
        PuzzleInstantiate(FrameObject, frampPos.position, currentPuzzle.board);
    }
}
