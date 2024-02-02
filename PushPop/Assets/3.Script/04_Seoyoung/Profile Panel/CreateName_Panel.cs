using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateName_Panel : MonoBehaviour
{
    [Header("입력창")]
    [SerializeField] TMP_InputField name_InputField;

    [Header("버튼")]
    [SerializeField] Button Confirm_Btn;
    [SerializeField] Button Back_Btn;

    [Header("경고창")]
    [SerializeField] GameObject warning_Panel;

    [Header("사진 패널")]
    [SerializeField] GameObject createImage_Panel;

    //변경/저장할 유저네임 할당 변수(임시)
    public string username;

    //이름입력을 받았는가 체크
    public bool isGetName = false;

    private void OnEnable()
    {
        Init();
        name_InputField.ActivateInputField();
    }

    private void Init()
    {
        Confirm_Btn.onClick.AddListener(ConfirmBtn_Clicked);
        Back_Btn.onClick.AddListener(() => { gameObject.SetActive(false); });
    }



    private void ConfirmBtn_Clicked()
    {
        //username값이 2~8자인가 검사 (추후에 중복 닉네임 검사도 할 것)
        //중복검사까지 한후 isGetName 값이 true가 되도록 변경하기 :)
        username = name_InputField.text;
        if(username.Length < 2 || username.Length > 8)
        {
            isGetName = false;
            name_InputField.text = string.Empty;
            name_InputField.ActivateInputField();
        }
        else
        {
            //닉넴 생성/변경
            isGetName = true;
            createImage_Panel.SetActive(true);
            gameObject.SetActive(false);

        }
    }

}
