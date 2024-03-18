using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PuzzleLozic : MonoBehaviour
{ // puzzle Prefab
    [Header("Prefab")]
    [SerializeField] private GameObject frameObject = null;
    [SerializeField] private GameObject pieceObject = null;

    [Header("Puzzle Mode Info")] 
    [SerializeField] private List<Puzzle> puzzles = new List<Puzzle>(); // puzzle scriptableObject list
    [SerializeField] private Transform puzzleTrans = null; // puzzle이 생성되는 곳
    public Transform framePos;
    public Transform failPiecePos;
    private Vector2 piecePos = Vector2.zero;

    public Puzzle CurrentPuzzle; // Player가 고른 퍼즐 종류
    private List<GameObject> pieceList = new List<GameObject>(); // 생성된 조각
    private float puzzleJudgmentDistance = 60f; // 퍼즐 판정 거리.
    public int ClearCount=0; //맞춰야하는 퍼즐 갯수
    public int SuccessCount= 0; //맞춘 갯수

    //public Action onPuzzleClear; //퍼즐을 모두 맞췄을때 부르는 콜백이벤트
    // public SpriteAtlas atlas;
    public List<PuzzleObject> puzzleList = new List<PuzzleObject>(); // 생성된 puzzle

    public bool CheckDistance(Vector3 _currentPosition)
    { // 퍼즐을 놓았을때 맞춰야하는 위치와 현재위치 비교
        if (Vector3.Distance(_currentPosition, framePos.position) < puzzleJudgmentDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Puzzle Mode
    public void SettingPuzzleInfo()
    { // scriptableObject setting, 선택한 카테고리에 맞는 퍼즐찾기
        foreach (Puzzle kind in puzzles)
        {
            if (kind.PuzzleID.Equals(GameManager.Instance.CurrentIconName))
            { // 선택한 Board
                CurrentPuzzle = kind;
                ClearCount = CurrentPuzzle.sprites.Length; // clear count = puzzle count
                break;
            }
        }
    }

    public void SettingPuzzlePos()
    { // 퍼즐기준 랜덤 위치에 bubble puzzle 생성
        for (int i = 0; i < CurrentPuzzle.sprites.Length; i++)
        {
            Texture2D puzzleTexture = CurrentPuzzle.sprites[i].texture;
            float bigger = puzzleTexture.width > puzzleTexture.height ? puzzleTexture.width : puzzleTexture.height;
            //퍼즐위치 랜덤한 위치에 생성
            float X = UnityEngine.Random.Range(bigger * 1.2f, Screen.width - bigger * 1.2f);
            float Y = UnityEngine.Random.Range(bigger * 1.2f, Screen.height - bigger * 1.2f);
            piecePos = new Vector2(X, Y);
            PuzzleInstantiate(pieceObject, piecePos, CurrentPuzzle.sprites[i], true); // puzzle 생성
        }

        for (int i = 0; i < puzzleList.Count; i++)
        { // bubble 생성
            GameManager.Instance.CreateBubble(puzzleList[i].puzzleArea, puzzleList[i].puzzleCenter, puzzleList[i].puzzleObject);
            GameManager.Instance.LiveBubbleCount++;
        }
    }

    private void PuzzleInstantiate(GameObject _puzzle, Vector3 _position, Sprite _puzzleSprite, bool _isPiece)
    { // 퍼즐 생성
        GameObject board = Instantiate(_puzzle, _position, Quaternion.identity, puzzleTrans);
        PuzzleSpriteSetting(board, _puzzleSprite);
        pieceList.Add(board); // piceList 추가
        if (_isPiece)
        { // 생성하는 퍼즐이 맞추는 조각일때
            AlphaCalculate(_puzzleSprite, board);
        }
        else
        { // 생성하는 퍼즐이 퍼즐 프레임일 때
            //생성할 시 조각들보다 위로 세팅해주면서 조각이 틀에 안가려지게하기위함.
            board.transform.SetAsFirstSibling();
        }
    }
    
    private void PuzzleSpriteSetting(GameObject _puzzle, Sprite _sprite)
    { // sprite setting
        Image frameImage = _puzzle.GetComponent<Image>();
        frameImage.sprite = _sprite;
        frameImage.preserveAspect = true; // sprite 비율 세팅
    }

    public void SettingGame()
    { // puzzle mode 진입
        for (int i = 0; i < pieceList.Count; i++)
        { // 막은 클릭 초기화
            PieceDragAndDrop dragAndDrop = pieceList[i].GetComponent<PieceDragAndDrop>();
            dragAndDrop.enabled = true;
            dragAndDrop.PieceImage.raycastTarget = true;
            dragAndDrop.FailToSolvePuzzle();
        }

        PuzzleInstantiate(frameObject, framePos.position, CurrentPuzzle.shadow, false);
    }
    #endregion
    #region 퍼즐완성 콜백 메소드들
    public void DestroyChildren()
    { // 퍼즐을 완료했을때 생성되있던 퍼즐,프레임 삭제하기위한 메소드
        if (puzzleTrans.childCount > 0)
        {
            for (int i = 0; i < puzzleTrans.childCount; i++)
            {
                Destroy(puzzleTrans.GetChild(i).gameObject);
            }
        }
    }

    public void CraetBoard()
    { // 퍼즐 완료하고 퍼즐 원본 오브젝트 생성
        Image frameImage = GameManager.Instance.pushPush.customManager.puzzleBoard.GetComponent<Image>();
        frameImage.sprite = DataManager.Instance.pushPopAtlas.GetSprite(CurrentPuzzle.PuzzleID.ToString()); //퍼즐 사진넣기
        frameImage.SetNativeSize();
        frameImage.alphaHitTestMinimumThreshold = 0.1f;
        GameManager.Instance.IsCustomMode = true;
    }
    #endregion

    public void AlphaCalculate(Sprite sprite , GameObject puzzle)
    { // alpha 값 계산해서 퍼즐 센터 찾기
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
        PuzzleObject puzzleObj = new PuzzleObject(puzzle, sprite, Area, finalCenter);

        puzzleList.Add(puzzleObj);
        puzzle.GetComponent<PuzzlePiece>().Puzzle = puzzleObj;
    }

    public void PuzzleModeInit()
    {
        pieceList.Clear();
        puzzleList.Clear();
        SuccessCount = 0;
        DestroyChildren();
    }
}
