using UnityEngine;

public class TempPushPop : MonoBehaviour
{
    public bool isSet = false;
    public bool isTrigger = false;
    public int createIndex = 0;
    public GameObject RectPush;

    private void OnEnable()
    {
        GameManager.Instance.pushPush.customManager.StackFakePops.Push(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.CompareTag("PushPop") && !isSet && !isTrigger)
        {
            isTrigger = true;
            if (createIndex > collision.GetComponent<TempPushPop>().createIndex)
            {
                CheckOverlap();
            }
            else
            {
                isSet = true;
            }
        }
    }

    public void CheckOverlap()
    { // °ãÄ¥ ½Ã
        CustomPushpopManager stack = GameManager.Instance.pushPush.customManager;
        GameObject lastFakeStack = stack.StackFakePops.Pop();
        Destroy(lastFakeStack);

        GameObject lastStack = stack.StackPops.Pop(); // ui gameobject
        PushPop.Instance.pushPopButton.Remove(RectPush);
        Destroy(lastStack);
    }
}
