using UnityEngine;

public class tempPushPop : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public bool isCanMakePush = false;
    public bool isSet = false;
    public bool isCheckOverlap = false;
    public bool isOverLap = false;
    private bool isTrigger = false;
    public GameObject RectPush;

    private void OnEnable()
    {
        CustomPushpopManager.Instance.StackFakePops.Push(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PushPop"))
        {
            if (!isSet && !isTrigger)
            {
                isTrigger = true;
                CheckOverlap();
            }
        }
    }

    public void CheckOverlap()
    {// °ãÄ¥ ½Ã
        Debug.Log("»èÁ¦¸Þ¼Òµå");
        CustomPushpopManager stack = FindObjectOfType<CustomPushpopManager>(); // gameobject
        GameObject lastFakeStack = stack.StackFakePops.Pop();
        Destroy(lastFakeStack);

        GameObject lastStack = stack.StackPops.Pop(); // ui gameobject
        PushPop.Instance.pushPopButton.Remove(RectPush);
        Destroy(lastStack);
        //Destroy(RectPush);
    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log(isCanMakePush);
    //    if (eventData.selectedObject.CompareTag("Puzzle"))
    //    {
    //        isCanMakePush = true;
    //        Debug.Log(isCanMakePush);
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log(isCanMakePush);
    //    if (eventData.selectedObject.CompareTag("Puzzle"))
    //    {
    //        isCanMakePush = false;
    //        Debug.Log(isCanMakePush);
    //    }
    //}
}
