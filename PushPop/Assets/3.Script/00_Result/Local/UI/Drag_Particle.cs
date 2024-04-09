using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Particle : MonoBehaviour
{
    private RectTransform rectTransform;

    [Header("드래그 이펙트 사라지는 시간")]
    private float cashing = 1.2f;   //드래그 이펙트 사라지는 시간

    [Header("드래그 이펙트 크기 간격")]
    private float sizeMin = 0.2f;       //이펙트 최소크기 (Scale값)
    private float sizeMax = 0.7f;       //이펙트 최대 크기 (Scale값)
    private float randomSize;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        //켜질때마다 사이즈 재조절
        randomSize = Random.Range(sizeMin, sizeMax);
        rectTransform.localScale = new Vector3(randomSize, randomSize, randomSize);
        rectTransform.rotation = new Quaternion(0f, 0f, Random.Range(0, 360), 1f);
        StartCoroutine(Destory_co());
    }

    private IEnumerator Destory_co()
    {//SetActive(false 코루틴)
        yield return new WaitForSeconds(cashing);
        gameObject.SetActive(false);
    }
}
