using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBubble
{
    void PushPushMode(Vector2 _bubblePosition, Vector2 _touchPosition);
    void SpeedMode(Vector2 _bubblePosition, Vector2 _touchPosition);
    void MemoryMode();
    void BombMode();
}