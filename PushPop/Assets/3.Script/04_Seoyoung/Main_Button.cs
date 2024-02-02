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


    private void Start()
    { 
        Profile.instance.gameObject.SetActive(false);
        Profile_btn.onClick.AddListener(Profile_Btn_Clicked);
    }


    public void Profile_Btn_Clicked()
    {
        Profile.instance.profileMode = ProfileMode.Logined;
        Profile.instance.gameObject.SetActive(true);
    }

}
