using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Piece", menuName = "Puzzle")]
public class PuzzleData : ScriptableObject
{
    //

    [Header("���� Sprite")]
    public Sprite[] sprites;
    [Header("���� Shadow")]
    public Sprite shadow;
    [Header("���� Sprite")]
    public Sprite board;
    [Header("������� ID")]
    public int PuzzleID;


}
