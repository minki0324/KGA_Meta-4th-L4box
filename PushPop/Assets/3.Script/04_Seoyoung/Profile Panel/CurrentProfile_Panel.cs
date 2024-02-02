using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentProfile_Panel : MonoBehaviour
{
    //������ ����â���� �Ѿ�� ��ư
    [SerializeField] Button Select_Btn;

    //������ �������� �Ѿ�� ��ư
    [SerializeField] Button Change_Btn;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        Select_Btn.onClick.AddListener(SelectBtn_Clicked);
        Change_Btn.onClick.AddListener(ChangeBtn_Clicked);
    }

    private void SelectBtn_Clicked()
    {
        Profile.instance.profileMode = ProfileMode.Logined;
        gameObject.SetActive(false);
        Profile.instance.selectProfile_Panel.SetActive(true);
      
        
    }

    private void ChangeBtn_Clicked()
    {
        Profile.instance.profileMode = ProfileMode.Logined;
        gameObject.SetActive(false);
        Profile.instance.createProfile_Panel.SetActive(true);
    }
}
