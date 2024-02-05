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
    [Header("������ ���� �г�")]
    [SerializeField] public GameObject selectProfile_Panel;

    //������ ���� �г�
    [Header("������ ���� �г�")]
    [SerializeField] public GameObject createName_Panel;

    [SerializeField] public GameObject createImage_Panel;


    [Header("���� ������ �г�")]
    //���� ������ UI
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
