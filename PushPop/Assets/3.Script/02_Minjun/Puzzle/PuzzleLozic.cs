using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PuzzleLozic : MonoBehaviour
{
    [Header("맞추는 퍼즐 오브젝트")]
    public GameObject PieceObject;
    [Header("프레임 오브젝트")]
    public GameObject FrameObject;
    [Header("프레임상속시킬 오브젝트")] 
    public GameObject FrameParent;
    [Header("피스상속시킬 오브젝트")]
    public GameObject PieceParent;
    [Header("프레임 설정 위치")]
    public Transform frampPos;
    [Header("피스들 설정 위치")] //임시
    public Transform[] piecePos;
    private float puzzleJudgmentDistance = 30; // 퍼즐 판정 거리.
    public List<Puzzle> puzzles = new List<Puzzle>(); //모든 퍼즐 종류를 담아놓는 리스트
    public int ClearCount=0; //맞춰야하는 퍼즐 갯수
    public int successCount= 0; //맞춘 갯수
    private Puzzle currentPuzzle; //Player가 고른 퍼즐 종류
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

    public void SelectPuzzleButton(string name)
    {//버튼참조 메소드
        foreach (var Kind in puzzles)
        {
            //매개변수로받은 String과 List에 들어있는 퍼즐들중 Enum.toString()과 같은 퍼즐 찾기
            if (Kind.puzzleKind.ToString() == name)
            {
                currentPuzzle = Kind;
                //클리어카운트는 퍼즐갯수 (Sprite)
                ClearCount = currentPuzzle.sprites.Length;
                SettingPuzzle();
                break;
            }
        }
        if (currentPuzzle == null)
        {
            Debug.Log("일치하는 퍼즐이 없습니다. ScriptableObject를 추가해 주세요");
        }
       
    }
    private void SettingPuzzle()
    {//퍼즐을 골랐을때 정해둔 위치로 퍼즐 생성 , Sprite 끼우기 , SetParent로 에디터 정리

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
