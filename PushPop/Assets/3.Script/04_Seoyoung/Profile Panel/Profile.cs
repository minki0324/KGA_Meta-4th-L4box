using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ProfileMode
{
    UnLogined = 0, //로그인 상태가 아닐 때, 처음 시작했을 때 나오는 UI들
    Logined,   //로그인 상태일 때
}

public class Profile : MonoBehaviour
{
    public static Profile instance = null;

    public ProfileMode profileMode;

    //처음 접속 시 보이는 패널
    [SerializeField] public GameObject selectProfile_Panel;

    //프로필 생성 패널
    [SerializeField] public GameObject createProfile_Panel;

    //[SerializeField] GameObject CreateName_panel;

    //[SerializeField] GameObject CreateImage_panel;

    //현재 프로필 UI
    [SerializeField] GameObject currnetProfile_Panel;

    [SerializeField] Button Back_Btn;

   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        profileMode = ProfileMode.Logined;
    }


    private void OnEnable()
    {
        Init();
        currnetProfile_Panel.SetActive(true);
        //if (profileMode == ProfileMode.Logined)
        //{
        //    currnetProfile_Panel.SetActive(true);
        //}
        //else if(profileMode == ProfileMode.UnLogined)
        //{

        //}
    }

    private void Init()
    {
        Back_Btn.onClick.AddListener(() => {

            selectProfile_Panel.SetActive(false);
            createProfile_Panel.SetActive(false);
            currnetProfile_Panel.SetActive(false);

            gameObject.SetActive(false); });
    }
}
