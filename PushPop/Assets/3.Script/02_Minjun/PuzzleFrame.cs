using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFrame : MonoBehaviour
{
    private Puzzle puzzle;
    private void Awake()
    {
        puzzle = transform.root.GetComponent<Puzzle>();
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            puzzle.Frame.Add(transform.GetChild(i).gameObject);
        }

    }

}
