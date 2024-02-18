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
    public GameObject FrameParent;

    [Header("������ ���� ��ġ")]
    public Transform frampPos;
    [Header("�ǽ��� ���� ��ġ")] //�ӽ�
    public Transform[] piecePos;
    private float puzzleJudgmentDistance = 301111; // ���� ���� �Ÿ�.
    public List<Puzzle> puzzles = new List<Puzzle>(); //��� ���� ������ ��Ƴ��� ����Ʈ
    public int ClearCount=0; //������ϴ� ���� ����
    public int successCount= 0; //���� ����
    public Puzzle currentPuzzle; //Player�� �� ���� ����
    [SerializeField] CustomPushpopManager costom;
    public Action onPuzzleClear;
    public List<PuzzlePiece> pieceList = new List<PuzzlePiece>();
    public SpriteAtlas atlas;
    [SerializeField] private GameObject DecorationPanel;
    private void OnEnable()
    {
        onPuzzleClear += DestroyChildren; //����Ϸ�� ������ , �ǽ��� ��λ���
        onPuzzleClear += CraetBoard; //�ϼ��� ���񺸵� ����
        onPuzzleClear += ActiveCustomPanel; //Ŀ�����ǳ� Ȱ��ȭ
        onPuzzleClear += AtiveOnDecoPanel; //�����ǳ�Ȱ��ȭ
    }
    private void OnDisable()
    {
        onPuzzleClear -= DestroyChildren;
        onPuzzleClear -= CraetBoard;
        onPuzzleClear -= ActiveCustomPanel;
        onPuzzleClear -= AtiveOnDecoPanel; //�����ǳ�Ȱ��ȭ
    }
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
                // SettingPuzzle();
                break;
            }
        }
        if (currentPuzzle == null)
        {
            Debug.Log("��ġ�ϴ� ������ �����ϴ�. ScriptableObject�� �߰��� �ּ���");
        }
       
    }

    public void SettingGame()
    {
        for(int i = 0; i < pieceList.Count; i++)
        {
            pieceList[i].transform.GetComponent<Image>().raycastTarget = true;
            pieceList[i].transform.GetComponent<PieceDragAndDrop>().enabled = true;
        }
        PuzzleInstantiate(FrameObject, frampPos.position, currentPuzzle.shadow, false);
    }

    public  void SettingPuzzle()
    {//������ ���� ������,���� ����
        for (int i = 0; i < currentPuzzle.sprites.Length; i++)
        {
            GameObject piece = PuzzleInstantiate(PieceObject, piecePos[i].position, currentPuzzle.sprites[i], true);
            pieceList.Add(piece.GetComponent<PuzzlePiece>());
        }
    }
    private GameObject PuzzleInstantiate(GameObject puzzle , Vector3 position , Sprite puzzleSprite, bool _isPiece)
    {//�������
        GameObject board = null;
        board = Instantiate(puzzle, position, Quaternion.identity, FrameParent.transform);

        PuzzleSetting(board, puzzleSprite);
        //newPiecePuzle
        if(_isPiece)
        {//�����ϴ� ������ ���ߴ� �����϶�
            AlphaCalculate(puzzleSprite, board);
        }
        else
        {//�����ϴ� ������ ���� Ʋ�϶�.
            //������ �� �����麸�� ���� �������ָ鼭 ������ Ʋ�� �Ȱ��������ϱ�����.
            board.transform.SetAsFirstSibling();
        }

        return board;
    }

    private void PuzzleSetting(GameObject puzzle , Sprite sprite)
    {//UI ĵ������ ���, ������ �������� �־��ֱ� , ����ũ�� ����.
        Image frameImage = puzzle.GetComponent<Image>(); 
        frameImage.sprite = sprite; //���� �����ֱ�
        frameImage.preserveAspect =true; //���������� ����
    }
    private void DestroyChildren()
    {//������ �Ϸ������� �������ִ� ���� �����ϱ����� �޼ҵ�
        foreach (Transform child in PuzzleParent.transform.GetChild(0))
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
        //Ŀ���Ҹ�� Ȱ��ȭ
        costom.EnableThisComponent();
        costom.isCustomMode = true;
    }
    private void ActiveCustomPanel()
    {
        costom.gameObject.SetActive(true);
    }

    public void AlphaCalculate(Sprite sprite , GameObject puzzle)
    {
        Texture2D texture = sprite.texture;
        Color32[] pixels = texture.GetPixels32();

        int width = texture.width;
        int height = texture.height;

        // ���İ��� 0.1 �̻��� �ȼ��� ���� ���
        int minX = width;
        int maxX = 0;
        int minY = height;
        int maxY = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[y * width + x];
                if (pixel.a >= 25) // ���İ��� 0.1 �̻��� ���
                {
                    minX = Mathf.Min(minX, x);
                    maxX = Mathf.Max(maxX, x);
                    minY = Mathf.Min(minY, y);
                    maxY = Mathf.Max(maxY, y);
                }
            }
        }

        // ������ ũ�� �� �߽� ���
        int areaWidth = maxX - minX + 1;
        int areaHeight = maxY - minY + 1;
        Vector2 Area = new Vector2(areaWidth, areaHeight);
        int biggerArea = areaWidth > areaHeight ? areaWidth : areaHeight;

        Vector2 center = new Vector2((minX + maxX) / 2f, (minY + maxY) / 2f);

        // ��������Ʈ�� �߽��� ���
        Vector2 spriteCenter = new Vector2(sprite.rect.width / 2f, sprite.rect.height / 2f);

        // �߽��� ��ġ ���
        Vector2 finalCenter = new Vector2(center.x - spriteCenter.x, center.y - spriteCenter.y);
        PuzzleObject obj = new PuzzleObject(puzzle, sprite, Area, finalCenter);

        GameManager.Instance.puzzleClass.Add(obj);
        puzzle.GetComponent<PuzzlePiece>().puzzle = obj;
    }
    public void ClearPieceList()
    {
        pieceList.Clear();
    }
    public void AtiveOnDecoPanel()
    {
        DecorationPanel.SetActive(true);
    }
}
