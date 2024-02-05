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
    [SerializeField] private TMP_InputField Profile_name;
    [SerializeField] private GameObject Profile_Panel;
    [SerializeField] private Transform Panel_Parent;
    [SerializeField] private List<GameObject> Panel_List;

    [Header("Test")]
    [SerializeField] private GameObject infomation;
    [SerializeField] private GameObject ranking;

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
            SQL_Manager.instance.SQL_Login(Login_ID.text, Login_PW.text);
        }
        else
        {
            // ���� �˻縦 ���� � �Է� �ʵ尡 ��ȿ���� ������ Ȯ���ϰ� �޽����� ���
            if (string.IsNullOrWhiteSpace(Sign_ID.text))
            {
                Debug.Log("�ùٸ� ID�� �Է����ּ���.");
            }
            if (string.IsNullOrWhiteSpace(Sign_PW.text))
            {
                Debug.Log("�ùٸ� PW�� �Է����ּ���.");
            }
        }
    }

    // ������ ���� Btn ���� Method
    public void Add_Profile()
    {
        if(!string.IsNullOrWhiteSpace(Profile_name.text))
        {
            SQL_Manager.instance.SQL_Add_Profile(Profile_name.text);
        }
        else
        {
            if(string.IsNullOrWhiteSpace(Profile_name.text))
            {
                Debug.Log("�ùٸ� ������ �г����� �Է����ּ���.");
            }
        }
    }

    // ������ ��� Btn ���� Method
    public void Print_Profile()
    {
        SQL_Manager.instance.SQL_Profile_ListSet();
        // List�� Count��� Panel����
        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i ++)
        {
            GameObject panel = Instantiate(Profile_Panel);
            panel.transform.SetParent(Panel_Parent);
            Panel_List.Add(panel);
        }

        // Profile Index�� �°� ������ name ���
        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = Panel_List[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
        }
    }

    // Join���� �� �� �ű�� �׽�Ʈ �޼ҵ��Դϴ�.
    public void test_Change_Scene()
    {
        ranking.SetActive(true);
        infomation.SetActive(false);
    }
    #endregion

}
