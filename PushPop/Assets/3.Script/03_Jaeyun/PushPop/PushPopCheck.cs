using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPopCheck : MonoBehaviour
{ // Pos Prefabs에 참조
    [SerializeField] private Transform[] point = new Transform[4]; // position gameObject

    private void OnEnable()
    {
       // PointContains(); // 생성될 때
    }

    // Grid Position이 PushPop Board에 전부 포함되는지 확인하는 Method
    public void PointContains()
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
        else
        {
            PushPop.instance.activePos.Add(this.gameObject); // active pos add
            Debug.Log("Point Add");
        }
    }
}
