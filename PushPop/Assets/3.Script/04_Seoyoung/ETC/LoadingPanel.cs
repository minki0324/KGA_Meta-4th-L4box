using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject Bubbles;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private GameObject[] bubble_Array;

    public int maxBubble = 100;

    #region Unity Callback
    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        for (int i = 0; i < maxBubble; i++)
        {

        }
    }
    #endregion

    #region Other Method

    private void Init()
    {
        bubble_Array = new GameObject[maxBubble];
     
        for (int i = 0; i < maxBubble; i++)
        {
            GameObject bub = Instantiate(bubblePrefab, new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), 0), Quaternion.identity);
            bub.transform.parent = Bubbles.transform;
            bub.SetActive(true);
            bubble_Array[i] = bub;
        }
    }

    #endregion
}
