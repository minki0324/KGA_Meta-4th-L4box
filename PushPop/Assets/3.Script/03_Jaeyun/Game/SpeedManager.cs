using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty // Only Speed Mode
{
    Easy = 50,
    Normal = 40,
    Hard = 30
}

public class SpeedManager : MonoBehaviour, IGame
{ // speed game
    public Difficulty Difficulty = Difficulty.Easy;
    public float difficultyCount = 0;
    public Sprite SelectImage = null;

    public void GameSetting()
    {
        switch (Difficulty)
        {
            case Difficulty.Easy:
                difficultyCount = 1 / 3f;
                break;
            case Difficulty.Normal:
                difficultyCount = 1 / 4f;

                break;
            case Difficulty.Hard:
                difficultyCount = 1 / 5f;

                break;
        }
    }

    public void GameStart()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator GameStart_Co()
    {
        Difficulty = GameManager.Instance.Difficulty;
        // ready
        throw new System.NotImplementedException();
        GameStart();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }
}
