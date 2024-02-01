using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Button : MonoBehaviour
{
    [SerializeField]
    private Button Option_btn;

    [SerializeField]
    private Button Profile_btn;

    [SerializeField]
    private Button Home_btn;

    [SerializeField]
    private Button Collection_btn;

    [SerializeField]
    private Button Mode2P_btn;

    [SerializeField]
    GameObject Profile_Panel;


    private void Start()
    {
        Profile_Panel.ins
    }


    public void Profile_Btn_Clicked()
    {
        Profile_Panel.SetActive(true);
    }

}
