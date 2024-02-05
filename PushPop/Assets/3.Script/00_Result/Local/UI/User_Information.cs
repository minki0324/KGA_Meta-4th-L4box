using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ���� ȸ������, �α���, ������ �� ���� ��ũ��Ʈ
/// </summary>
public class User_Information : MonoBehaviour
{
    [Header("SignUp")]
    [SerializeField] private TMP_InputField Sign_ID;
    [SerializeField] private TMP_InputField Sign_PW;

    [Header("Login")]
    [SerializeField] private TMP_InputField Login_ID;
    [SerializeField] private TMP_InputField Login_PW;

    [Header("Profile")]
    [SerializeField] private GameObject Profile_obj;
   
    [Header("Test")]
    [SerializeField] private GameObject infomation;
    [SerializeField] private GameObject SignUp_panel;

    #region Unity Callback
    #endregion

    #region Other Method
    // ȸ������ Btn ���� Method 
    public void SignUp()
    {
        if (!string.IsNullOrWhiteSpace(Sign_ID.text) && !string.IsNullOrWhiteSpace(Sign_PW.text))
        {
            SQL_Manager.instance.SQL_SignUp(Sign_ID.text, Sign_PW.text);
        }
        else
        {
            // ���� �˻縦 ���� � �Է� �ʵ尡 ��ȿ���� ������ Ȯ���ϰ� �޽����� ���
            if (string.IsNullOrWhiteSpace(Sign_ID.text))
            {
                Debug.Log("ID�� �Է����ּ���.");
            }
            if (string.IsNullOrWhiteSpace(Sign_PW.text))
            {
                Debug.Log("Password�� �Է����ּ���.");
            }
        }
    }

    // �α��� Btn ���� Method
    public void Login()
    {
        if (!string.IsNullOrWhiteSpace(Login_ID.text) && !string.IsNullOrWhiteSpace(Login_PW.text))
        {
            if(SQL_Manager.instance.SQL_Login(Login_ID.text, Login_PW.text))
            {
                Profile_obj.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("�ùٸ� ID, PW�� �Է����ּ���.");
            }
        }
        else
        {
            // ���� �˻縦 ���� � �Է� �ʵ尡 ��ȿ���� ������ Ȯ���ϰ� �޽����� ���
            if (string.IsNullOrWhiteSpace(Login_ID.text))
            {
                Debug.Log("�ùٸ� ID�� �Է����ּ���.");
            }
            if (string.IsNullOrWhiteSpace(Login_PW.text))
            {
                Debug.Log("�ùٸ� PW�� �Է����ּ���.");
            }
        }
    }

    // Join���� �� �� �ű�� �׽�Ʈ �޼ҵ��Դϴ�.
    public void test_SignUp()
    {
        bool info = infomation.activeSelf;
        bool sign = SignUp_panel.activeSelf;
        infomation.SetActive(!info);
        SignUp_panel.SetActive(!sign);
    }
    #endregion

}
