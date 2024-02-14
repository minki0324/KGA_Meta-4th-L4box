using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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
    [SerializeField] CostomPushpopManager costom;
    public Action onClear;

    public SpriteAtlas atlas;

    private void OnEnable()
    {
        onClear += DestroyChildren;
        onClear += CraetBoard;
        onClear += ActiveCostomPanel;
    }
    private void OnDisable()
    {
        onClear -= DestroyChildren;
        onClear -= CraetBoard;
        onClear -= ActiveCostomPanel;
    }
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
        GameObject board = Instantiate(puzzle, position, Quaternion.identity);
        PuzzleSetting(board, puzzleSprite);
        //newPiecePuzle
    }

    private void PuzzleSetting(GameObject puzzle , Sprite sprite)
    {//UI ĵ������ ���, ������ �������� �־��ֱ� , ����ũ�� ����.
        puzzle.transform.SetParent(PuzzleParent.transform); //ĵ�����ȿ� ���
        Image frameImage = puzzle.GetComponent<Image>(); 
        frameImage.sprite = sprite; //���� �����ֱ�
        frameImage.preserveAspect =true; //���������� ����
    }
    private void DestroyChildren()
    {//������ �Ϸ������� �������ִ� ���� �����ϱ����� �޼ҵ�
        foreach (Transform child in PuzzleParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void CraetBoard()
    {//����Ϸ��ϰ� ���� ���� ������Ʈ �������ֱ�
        Image frameImage = costom.puzzleBoard.GetComponent<Image>();
        frameImage.sprite = atlas.GetSprite(currentPuzzle.PuzzleID.ToString()); //���� �����ֱ�
        frameImage.SetNativeSize();
        frameImage.alphaHitTestMinimumThreshold = 0.1f;
    }
    private void ActiveCostomPanel()
    {
        costom.gameObject.SetActive(true);
    }
}
