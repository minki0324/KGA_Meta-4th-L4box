using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Main_Button : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField]
    private Button option_Btn;

    [SerializeField]
    private Button profile_Btn;

    [SerializeField]
    private Button home_Btn;        //이게 뭐엿더라..ㅎㅎ..어디로 가는 버튼이더라 

    [SerializeField]
    private Button collection_Btn;

    [SerializeField]
    private Button mode2P_Btn;

    [Header("패널")]
    [SerializeField]
    private GameObject collection_Panel;



    private void Start()
    { 
        Profile.instance.gameObject.SetActive(false);
        profile_Btn.onClick.AddListener(Profile_Btn_Clicked);
        option_Btn.onClick.AddListener(OptionBtn_Clicked);
        collection_Btn.onClick.AddListener(CollectionBtn_Clicked);
        mode2P_Btn.onClick.AddListener(Mode2PBtn_Clicked);
        home_Btn.onClick.AddListener(HomeBtn_Clicked);
    }


    public void Profile_Btn_Clicked()
    {
        //Profile.instance.profileMode = ProfileMode.Logined;
        Profile.instance.gameObject.SetActive(true);
    }

    public void OptionBtn_Clicked()
    {
        Option_Panel.instance.gameObject.SetActive(true);
    }

    public void CollectionBtn_Clicked()
    {
        collection_Panel.SetActive(true);
    }

    public void Mode2PBtn_Clicked()
    {
       
    }
    
    public void HomeBtn_Clicked()
    {
        Application.OpenURL("https://www.l4box.com/");
    }

}
