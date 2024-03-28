using UnityEngine;

public class OverlabCheckPushPop : MonoBehaviour
{
    private bool isSet = false;
    private bool isTrigger = false;
    public int createIndex = 0;
    public GameObject OverlabCheckCircle;

    private void OnEnable()
    {
        PushPop.Instance.StackFakePops.Push(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.CompareTag("PushPop") /*&& !isSet*/ && !isTrigger)
        {
            isTrigger = true;
            if (createIndex > collision.GetComponent<OverlabCheckPushPop>().createIndex)
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
        GameObject lastFakeStack = PushPop.Instance.StackFakePops.Pop();
        Destroy(lastFakeStack);

        GameObject lastStack = PushPop.Instance.StackPops.Pop(); // ui gameobject
        PushPop.Instance.pushPopButton.Remove(OverlabCheckCircle);
        Destroy(lastStack);
    }
}
