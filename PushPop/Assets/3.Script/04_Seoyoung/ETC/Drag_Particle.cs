using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Particle : MonoBehaviour
{

    float cashing = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(Destory_co());
    }


    private IEnumerator Destory_co()
    {//SetActive(false ÄÚ·çÆ¾)

        yield return new WaitForSeconds(cashing);
        gameObject.SetActive(false);
    }
}
