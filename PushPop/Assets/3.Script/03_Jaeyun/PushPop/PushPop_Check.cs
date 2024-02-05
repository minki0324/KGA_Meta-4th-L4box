using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPop_Check : MonoBehaviour
{
    [SerializeField] private Transform[] point = new Transform[4];
    private void Update()
    {
        PointContains();
    }

    private void PointContains()
    {
        int contains = 0;
        for (int i = 0; i < point.Length; i++)
        {
            Collider2D collider = Physics2D.OverlapPoint(point[i].position);
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
    }
}
