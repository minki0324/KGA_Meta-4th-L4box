using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject Bubbles;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Loading_Bubble[] bubble_Array;

    [SerializeField] private Image background;
    private RectTransform background_Rect;

    public int maxBubble = 150;
    public float upSpeed = 12f;

    public float colorTime = 100f;
 
    public bool bisLoaded = false;

    #region Unity Callback
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        bisLoaded = false;
        for (int i = 0; i < maxBubble; i++)
        {
            bubble_Array[i].moveMode = MoveMode.Loading;
            bubble_Array[i].transform.position = new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(0f, 450f), 0f);
            bubble_Array[i].gameObject.SetActive(true);
        }

        background.color = new Color(255f, 255f, 255f, 1f);
        StartCoroutine(Background_co());
    }

    private void Update()
    {
        if(!bisLoaded)
        {
            for (int i = 0; i < maxBubble; i++)
            {
                if (!bubble_Array[i].gameObject.activeSelf)
                {
                    bubble_Array[i].gameObject.SetActive(true);
                    bubble_Array[i].transform.position = new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), -100f, 0f);
                }
            }
        }
  
    }
    #endregion

    #region Other Method

    private void Init()
    {
        bubble_Array = new Loading_Bubble[maxBubble];
        background_Rect = background.GetComponent<RectTransform>();

        for (int i = 0; i < maxBubble; i++)
        {
            GameObject bub = Instantiate(bubblePrefab, new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(0f, 450f), 0f), Quaternion.identity);
            bub.transform.parent = Bubbles.transform;
            bub.SetActive(false);
            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
        }
    }


    private IEnumerator Background_co()
    {
        Color color = background.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / colorTime;
            background.color = color;
           //background_Rect.transform.position += new Vector3(0f, upSpeed, 0f);

            yield return null;
        }

        bisLoaded = true;
    }
    #endregion
}
