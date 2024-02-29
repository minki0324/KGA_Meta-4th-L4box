using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPopCheck : MonoBehaviour
{ // Pos Prefabs에 참조
    private Transform[] point = new Transform[4]; // position gameObject
    public int spriteIndex;

    private void Awake()
    {
        for (int i = 0; i < point.Length; i++)
        {
            point[i] = transform.GetChild(i).transform;
        }
    }

    // Grid Position이 PushPop Board에 전부 포함되는지 확인하는 Method
    public void PointContains()
    {
        int contains = 0;
        for (int i = 0; i < point.Length; i++)
        {
            Collider2D collider = Physics2D.OverlapPoint(point[i].position); // position check
            if (collider == null) continue;
            if (collider.CompareTag("PushPop"))
            {
                contains++;
            }
        }

        if (!contains.Equals(4))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            PushPop.Instance.activePos.Add(this.gameObject); // active pos add
        }
    }
}
