using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "Piece" , menuName ="Puzzle")]
public class Puzzle : ScriptableObject
{
    //
    public enum PuzzleKind
    {
        Grape = 0,
        Apple,
        Banana
    }
    [Header("���� Sprite")]
    public Sprite[] sprites;
    
    [Header("�������� ex.Grape , Apple ...")]
    public PuzzleKind puzzleKind;
    

}
