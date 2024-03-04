using UnityEngine;

public class tempPushPop : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    private bool isSet = false;
    public bool isTrigger = false;
    public int creatIndex = 0;
    public GameObject RectPush;

    private void OnEnable()
    {
        CustomPushpopManager.Instance.StackFakePops.Push(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.CompareTag("PushPop") && !isSet && !isTrigger)
        {
            isTrigger = true;
            if (creatIndex > collision.GetComponent<tempPushPop>().creatIndex )
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
    {// ��ĥ ��

        CustomPushpopManager stack = CustomPushpopManager.Instance;
        GameObject lastFakeStack = stack.StackFakePops.Pop();
        
        Destroy(lastFakeStack);

        GameObject lastStack = stack.StackPops.Pop(); // ui gameobject
        PushPop.Instance.pushPopButton.Remove(RectPush);
        Destroy(lastStack);
            //Destroy(RectPush);
        
      
    }
  
}
