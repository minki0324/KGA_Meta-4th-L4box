using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Particle : MonoBehaviour
{
    RectTransform rectTransform;
    [Header("�巡�� ����Ʈ ������� �ð�")]
    public float cashing = 1.2f;

    [Header("�巡�� ����Ʈ ũ�� ����")]
    public float sizeMin = 0.2f;
    public float sizeMax = 0.7f;
    private float randomSize;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {

        randomSize = Random.Range(sizeMin, sizeMax);
        rectTransform.localScale = new Vector3(randomSize, randomSize, randomSize);
        rectTransform.rotation = new Quaternion(0f, 0f, Random.Range(0, 360), 1f);
        StartCoroutine(Destory_co());
    }

    private IEnumerator Destory_co()
    {//SetActive(false �ڷ�ƾ)

        yield return new WaitForSeconds(cashing);
        gameObject.SetActive(false);
    }

}
