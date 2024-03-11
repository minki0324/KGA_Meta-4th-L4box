using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MoveMode
{
    Main = 0, 
    Loading
}

public class TitlePanel : MonoBehaviour
{
    [SerializeField] private GameObject Bubbles;       //�θ� ������Ʈ
    [SerializeField] private GameObject bubblePrefab;   //���� ������
    [SerializeField] private GameObject[] bubble_Array; //������Ʈ Ǯ����

    [SerializeField] private TMP_Text gameStartText;

    public int maxBubble = 5;
    #region Unity Callback

    private void Start()
    {
        Init();
    }


    private void Update()
    {
        for(int i =0; i<bubble_Array.Length; i++)
        {
            if(!bubble_Array[i].activeSelf)
            {
                bubble_Array[i].SetActive(true);
            }
        }
    }

    #endregion

    #region Other Method

    private void Init()
    {
        bubble_Array = new GameObject[maxBubble];
        for (int i = 0; i < maxBubble; i++)
        {
            GameObject bub = Instantiate(bubblePrefab, new Vector3(Random.Range(100, Camera.main.pixelWidth - 100), Random.Range(100, Camera.main.pixelHeight - 100)), Quaternion.identity);
            bub.transform.parent = Bubbles.transform;
            bub.SetActive(true);
            bubble_Array[i] = bub;
        }
    }


    public void StartGame()
    {
        //�ε��г� �ѱ�
        gameObject.SetActive(false);
    }

    #endregion
}
