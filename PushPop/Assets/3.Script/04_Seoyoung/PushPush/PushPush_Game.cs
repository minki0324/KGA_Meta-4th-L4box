using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PushPush_Game : MonoBehaviour
{

    [Header("꾸미기 패널")]
    [SerializeField] private GameObject Colorset_Panel;
    [SerializeField] private List<Button> colorButton_List;
    [SerializeField] private List<Sprite> colorSprite_List;



    private void Start()
    {
       
    }

    public void Init()
    {

        for (int i = 0; i < Colorset_Panel.transform.childCount; i++)
        {
            colorButton_List.Add(Colorset_Panel.transform.GetChild(i).GetComponent<Button>());
        }
    }



    public void BackBtn_Clicked()
    {

    }


    public void Deco_BackBtn_Clicked()
    {

    }

    public void ResetBtn_Clicked()
    {

    }

    public void NextBtn_Clicked()
    {

    }

}
