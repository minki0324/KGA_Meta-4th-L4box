using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateName_Panel : MonoBehaviour
{
    [Header("�Է�â")]
    [SerializeField] TMP_InputField name_InputField;

    [Header("��ư")]
    [SerializeField] Button Confirm_Btn;
    [SerializeField] Button Back_Btn;

    [Header("���â")]
    [SerializeField] GameObject warning_Panel;

    [Header("���� �г�")]
    [SerializeField] GameObject createImage_Panel;

    //����/������ �������� �Ҵ� ����(�ӽ�)
    public string username;

    //�̸��Է��� �޾Ҵ°� üũ
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
        //username���� 2~8���ΰ� �˻� (���Ŀ� �ߺ� �г��� �˻絵 �� ��)
        //�ߺ��˻���� ���� isGetName ���� true�� �ǵ��� �����ϱ� :)
        username = name_InputField.text;
        if(username.Length < 2 || username.Length > 8)
        {
            isGetName = false;
            name_InputField.text = string.Empty;
            name_InputField.ActivateInputField();
        }
        else
        {
            //�г� ����/����
            isGetName = true;
            createImage_Panel.SetActive(true);
            gameObject.SetActive(false);

        }
    }

}
