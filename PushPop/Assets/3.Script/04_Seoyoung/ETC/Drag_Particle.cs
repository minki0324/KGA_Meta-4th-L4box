using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Particle : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartCoroutine(Destory_co());
    }


    private IEnumerator Destory_co()
    {//SetActive(false ÄÚ·çÆ¾)

        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
