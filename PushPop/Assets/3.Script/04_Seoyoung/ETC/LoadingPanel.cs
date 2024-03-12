using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject Bubbles;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Loading_Bubble[] bubble_Array;


    //밑에서부터 FADE용 배경
    [SerializeField] private Image FadeBackground;

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
            bubble_Array[i].transform.position = new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(-200f, 450f), 0f);
            bubble_Array[i].gameObject.SetActive(true);
        }

      
        //StartCoroutine(Background_co());
        StartCoroutine(BackgroundFadeOut_co());
    }

    private void Update()
    {
        if (!bisLoaded)
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

        for (int i = 0; i < maxBubble; i++)
        {
            GameObject bub = Instantiate(bubblePrefab, new Vector3(Random.Range(0, Camera.main.pixelWidth - 100), Random.Range(0f, 30f), 0f), Quaternion.identity);
            bub.transform.parent = Bubbles.transform;
            bub.SetActive(false);
            bubble_Array[i] = bub.GetComponent<Loading_Bubble>();
        }

        
    }


    private IEnumerator BackgroundFadeOut_co()
    {
        yield return new WaitForSeconds(0.5f);
        float visibility = 0.1f;
        float cashing = 0.1f;
        while(true)
        {

            if (visibility <= 0.35f)
            {
                visibility += 0.03f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                // yield return null;
                yield return new WaitForSeconds(cashing);
            }
            else if (visibility > 0.3f && visibility < 5f)
            {
                visibility += 0.07f;
                FadeBackground.material.SetFloat("_Visibility", visibility);
                yield return null;
            }
            else
            {
                FadeBackground.material.SetFloat("_Visibility", 7f);
                bisLoaded = true;
                yield break;
            }
           
            
        }
    }

    private IEnumerator BackgroundFade_co()
    {
        //아래서부터 올라오는 쉐이더가 적용된 배경화면 Fade 코루틴
        float visibility = 15f;

        while (true)
        {
            if (visibility <= 0)
            {
                bisLoaded = true;
                yield break;
            }

            FadeBackground.material.SetFloat("_Visibility", visibility);

            visibility -= 0.1f;
            //visibility -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
