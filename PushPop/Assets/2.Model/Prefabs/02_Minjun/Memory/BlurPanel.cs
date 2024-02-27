using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurPanel : MonoBehaviour
{
    public static BlurPanel instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameObject.SetActive(false);
    }
    public void BlurParent(Transform parent)
    {
        transform.parent.SetParent(parent);
        gameObject.SetActive(true);
        transform.SetAsFirstSibling();
    }
    public void TurnOfftheBlur()
    {
        gameObject.SetActive(false);
    }
}
