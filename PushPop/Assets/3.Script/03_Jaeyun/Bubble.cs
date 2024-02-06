using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    PUSHPUSH = 0,
    SPEED,
    BOOM,
}

public class Bubble : MonoBehaviour, IBubble
{
    protected Mode mode; // game mode

    public void BubbleMovement()
    {

    }
}
