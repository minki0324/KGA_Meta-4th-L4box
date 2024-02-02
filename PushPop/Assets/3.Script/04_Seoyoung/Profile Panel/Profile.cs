using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ProfileMode
{
    UnLogined = 0, //�α��� ���°� �ƴ� ��, ó�� �������� �� ������ UI��
    Logined,   //�α��� ������ ��
}

public class Profile : MonoBehaviour
{
    public static Profile instance = null;

    public ProfileMode profileMode;

    //ó�� ���� �� ���̴� �г�
    [SerializeField] public GameObject selectProfile_Panel;

    //������ ���� �г�
    [SerializeField] public GameObject createProfile_Panel;

    //[SerializeField] GameObject CreateName_panel;

    //[SerializeField] GameObject CreateImage_panel;

    //���� ������ UI
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
