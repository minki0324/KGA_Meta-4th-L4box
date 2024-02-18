using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PuzzleLozic : MonoBehaviour
{
    [Header("맞추는 퍼즐 오브젝트")]
    public GameObject PieceObject;
    [Header("프레임 오브젝트")]
    public GameObject FrameObject;
    [Header("퍼즐상속시킬 오브젝트")] 
    public GameObject PuzzleParent;
    public GameObject FrameParent;

    [Header("프레임 설정 위치")]
    public Transform frampPos;
    [Header("피스들 설정 위치")] //임시
    public Transform[] piecePos;
    private float puzzleJudgmentDistance = 301111; // 퍼즐 판정 거리.
    public List<Puzzle> puzzles = new List<Puzzle>(); //모든 퍼즐 종류를 담아놓는 리스트
    public int ClearCount=0; //맞춰야하는 퍼즐 갯수
    public int successCount= 0; //맞춘 갯수
    public Puzzle currentPuzzle; //Player가 고른 퍼즐 종류
    [SerializeField] CustomPushpopManager costom;
    public Action onPuzzleClear;
    public List<PuzzlePiece> pieceList = new List<PuzzlePiece>();
    public SpriteAtlas atlas;
    [SerializeField] private GameObject DecorationPanel;
    private void OnEnable()
    {
        onPuzzleClear += DestroyChildren; //퍼즐완료시 프레임 , 피스들 모두삭제
        onPuzzleClear += CraetBoard; //완성된 퍼즐보드 생산
        onPuzzleClear += ActiveCustomPanel; //커스텀판넬 활성화
        onPuzzleClear += AtiveOnDecoPanel; //데코판넬활성화
    }
    private void OnDisable()
    {
        onPuzzleClear -= DestroyChildren;
        onPuzzleClear -= CraetBoard;
        onPuzzleClear -= ActiveCustomPanel;
        onPuzzleClear -= AtiveOnDecoPanel; //데코판넬활성화
    }
    public bool checkdistance(Vector3 currentPosition )
    {//퍼즐을 놓았을때 맞춰야하는 위치와 현재위치 비교
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
    {//버튼참조 메소드
        foreach (var Kind in puzzles)
        {
            //매개변수로받은 String과 List에 들어있는 퍼즐들중 Enum.toString()과 같은 퍼즐 찾기
            if (Kind.PuzzleID == PuzzleIDIndex)
            {
                currentPuzzle = Kind;
                //todo 게임매니저에 현재퍼즐 보내줘...


                //클리어카운트는 퍼즐갯수 (Sprite)
                ClearCount = currentPuzzle.sprites.Length;
                // SettingPuzzle();
                break;
            }
        }
        if (currentPuzzle == null)
        {
            Debug.Log("일치하는 퍼즐이 없습니다. ScriptableObject를 추가해 주세요");
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
    {//정해진 퍼즐 프레임,퍼즐 생성
        for (int i = 0; i < currentPuzzle.sprites.Length; i++)
        {
            GameObject piece = PuzzleInstantiate(PieceObject, piecePos[i].position, currentPuzzle.sprites[i], true);
            pieceList.Add(piece.GetComponent<PuzzlePiece>());
        }
    }
    private GameObject PuzzleInstantiate(GameObject puzzle , Vector3 position , Sprite puzzleSprite, bool _isPiece)
    {//퍼즐생성
        GameObject board = null;
        board = Instantiate(puzzle, position, Quaternion.identity, FrameParent.transform);

        PuzzleSetting(board, puzzleSprite);
        //newPiecePuzle
        if(_isPiece)
        {//생성하는 퍼즐이 맞추는 조각일때
            AlphaCalculate(puzzleSprite, board);
        }
        else
        {//생성하는 퍼즐이 퍼즐 틀일때.
            //생성할 시 조각들보다 위로 세팅해주면서 조각이 틀에 안가려지게하기위함.
            board.transform.SetAsFirstSibling();
        }

        return board;
    }

    private void PuzzleSetting(GameObject puzzle , Sprite sprite)
    {//UI 캔버스에 상속, 정해진 사진으로 넣어주기 , 사진크기 세팅.
        Image frameImage = puzzle.GetComponent<Image>(); 
        frameImage.sprite = sprite; //퍼즐 사진넣기
        frameImage.preserveAspect =true; //사진사이즈 세팅
    }
    private void DestroyChildren()
    {//퍼즐을 완료했을때 생성되있던 퍼즐 삭제하기위한 메소드
        foreach (Transform child in PuzzleParent.transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
    }
    private void CraetBoard()
    {//퍼즐완료하고 퍼즐 원본 오브젝트 생성해주기
        Image frameImage = costom.puzzleBoard.GetComponent<Image>();
        frameImage.sprite = atlas.GetSprite(currentPuzzle.PuzzleID.ToString()); //퍼즐 사진넣기
        frameImage.SetNativeSize();
        frameImage.alphaHitTestMinimumThreshold = 0.1f;
        //커스텀모드 활성화
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

        // 알파값이 0.1 이상인 픽셀의 영역 계산
        int minX = width;
        int maxX = 0;
        int minY = height;
        int maxY = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[y * width + x];
                if (pixel.a >= 25) // 알파값이 0.1 이상인 경우
                {
                    minX = Mathf.Min(minX, x);
                    maxX = Mathf.Max(maxX, x);
                    minY = Mathf.Min(minY, y);
                    maxY = Mathf.Max(maxY, y);
                }
            }
        }

        // 영역의 크기 및 중심 계산
        int areaWidth = maxX - minX + 1;
        int areaHeight = maxY - minY + 1;
        Vector2 Area = new Vector2(areaWidth, areaHeight);
        int biggerArea = areaWidth > areaHeight ? areaWidth : areaHeight;

        Vector2 center = new Vector2((minX + maxX) / 2f, (minY + maxY) / 2f);

        // 스프라이트의 중심점 계산
        Vector2 spriteCenter = new Vector2(sprite.rect.width / 2f, sprite.rect.height / 2f);

        // 중심점 위치 계산
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
