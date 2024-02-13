using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Piece", menuName = "Puzzle")]
public class Puzzle : ScriptableObject
{
    //

    [Header("���� Sprite")]
    public Sprite[] sprites;
    [Header("������� ID")]
    public int PuzzleID;


}
