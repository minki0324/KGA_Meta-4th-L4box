using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Button : MonoBehaviour
{
    [Header("��ư")]
    [SerializeField]
    private Button option_Btn;

    [SerializeField]
    private Button profile_Btn;

    [SerializeField]
    private Button home_Btn;        //�̰� ��������..����..���� ���� ��ư�̴��� 

    [SerializeField]
    private Button collection_Btn;

    [SerializeField]
    private Button mode2P_Btn;


    private void Start()
    { 
        Profile.instance.gameObject.SetActive(false);
        profile_Btn.onClick.AddListener(Profile_Btn_Clicked);
    }


    public void Profile_Btn_Clicked()
    {
        //Profile.instance.profileMode = ProfileMode.Logined;
        Profile.instance.gameObject.SetActive(true);
    }

}
