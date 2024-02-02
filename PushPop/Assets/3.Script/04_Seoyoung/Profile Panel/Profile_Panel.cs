using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile_Panel : MonoBehaviour
{

    [SerializeField] Button Create_Btn;

    [SerializeField] Button Delete_Btn;

    [SerializeField] GameObject Content;


    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        Create_Btn.onClick.AddListener(CreateBtn_Clicked);
        Delete_Btn.onClick.AddListener(DeleteBtn_Clicked);
    }


    private void CreateBtn_Clicked()
    {
        //프로필 생성 함수 호출 해주세요 :)
    }

    private void DeleteBtn_Clicked()
    {
        //프로필 삭제 함수 호출 해주세요:)
    }
}
