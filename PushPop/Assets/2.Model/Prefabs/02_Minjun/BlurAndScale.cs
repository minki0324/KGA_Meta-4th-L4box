using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurAndScale : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
        Debug.Log(BlurPanel.instance);
        BlurPanel.instance.BlurParent(transform);
       
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
        BlurPanel.instance.TurnOfftheBlur();

    }
   
    
}
