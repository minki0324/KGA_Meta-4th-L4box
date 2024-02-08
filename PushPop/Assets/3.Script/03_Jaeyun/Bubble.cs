using UnityEngine;
using UnityEngine.EventSystems;

public enum Mode
{
    PushPush = 0,
    Speed,
    Memory,
    Bomb,
}

public class PushBubble : IBubble
{
    public virtual void BubbleTouch(GameObject bubble)
    {
        bubble = EventSystem.current.currentSelectedGameObject; // select한 object

    }

    public virtual void BubbleMovement()
    {

    }
}

public class SpeedBubble : IBubble
{

}

public class MemoryBubble : IBubble
{

}

public class BombBubble : IBubble
{

}

public class Bubble : MonoBehaviour
{ // bubble prefab마다 달아줄 script
    protected Mode mode; // game mode

}
