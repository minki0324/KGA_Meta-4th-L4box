using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Piece", menuName = "Puzzle")]
public class PuzzleData : ScriptableObject
{
    //

    [Header("∆€¡Ò Sprite")]
    public Sprite[] sprites;
    [Header("∆€¡Ò Shadow")]
    public Sprite shadow;
    [Header("∆€¡Ò Sprite")]
    public Sprite board;
    [Header("∆€¡Ò∞Ì¿Ø ID")]
    public int PuzzleID;


}
