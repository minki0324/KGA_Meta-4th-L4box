using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection_Panel : MonoBehaviour
{

    [SerializeField]
    private Button Back_Btn;

    private void Start()
    {
        gameObject.SetActive(false);

        Back_Btn.onClick.AddListener(() => { gameObject.SetActive(false); });
    }
}
