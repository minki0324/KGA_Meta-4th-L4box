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
    [Header("프로필 선택 패널")]
    [SerializeField] public GameObject selectProfile_Panel;

    //프로필 생성 패널
    [Header("프로필 생성 패널")]
    [SerializeField] public GameObject createName_Panel;

    [SerializeField] public GameObject createImage_Panel;


    [Header("현재 프로필 패널")]
    //현재 프로필 UI
    [SerializeField] public GameObject currnetProfile_Panel;

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



    private void OnEnable()
    {
        
      //   currnetProfile_Panel.SetActive(true);
        if (profileMode == ProfileMode.Logined)
        {
            currnetProfile_Panel.SetActive(true);
            Init_Logined();
        }
        else if (profileMode == ProfileMode.UnLogined)
        {

        }
    }

    private void Init_Logined()
    {
        Back_Btn.onClick.AddListener(() => {

            selectProfile_Panel.SetActive(false);
            currnetProfile_Panel.SetActive(false);
            createName_Panel.SetActive(false);
            createImage_Panel.SetActive(false);

            gameObject.SetActive(false); });
    }
 

    private void Init_UnLogined()
    {

    }
}
