using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    


public class CreateProfile_Panel : MonoBehaviour
{
    [SerializeField] GameObject CreateName_Panel;
    [SerializeField] GameObject CreateImage_Panel;

    private void Start()
    {
        CreateImage_Panel.SetActive(false);
        CreateName_Panel.SetActive(false);
        gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        CreateName_Panel.SetActive(true);
    }

    //private void OnEnable()
    //{
    //    if (Profile.instance.profileMode == ProfileMode.UnLogined)
    //    {
    //        //��������϶� ��� �������� 

    //        //�α��� ���·� ����
    //        Profile.instance.profileMode = ProfileMode.Logined;
    //    }
    //    else if (Profile.instance.profileMode == ProfileMode.Logined)
    //    {
    //        //�����϶� ��� ����
    //    }
    //}



}
