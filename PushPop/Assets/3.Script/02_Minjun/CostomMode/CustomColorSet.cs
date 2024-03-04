using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColorSet : MonoBehaviour
{
    public GameObject[] ring;
  public void ChildActive(int index)
    {
        for (int i = 0; i < ring.Length; i++)
        {
            ring[i].SetActive(false);
        }
        ring[index].SetActive(true);
        CustomPushpopManager.Instance.spriteIndex = index;
    }
    
}
