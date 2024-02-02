using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 유저 회원가입, 로그인, 프로필 등 관련 스크립트
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
    // 회원가입 Btn 연동 Method 
    public void SignUp()
    {
        if (!string.IsNullOrWhiteSpace(Sign_ID.text) && !string.IsNullOrWhiteSpace(Sign_PW.text))
        {
            SQL_Manager.instance.SQL_SignUp(Sign_ID.text, Sign_PW.text);
        }
        else
        {
            // 공백 검사를 통해 어떤 입력 필드가 유효하지 않은지 확인하고 메시지를 출력
            if (string.IsNullOrWhiteSpace(Sign_ID.text))
            {
                Debug.Log("ID를 입력해주세요.");
            }
            if (string.IsNullOrWhiteSpace(Sign_PW.text))
            {
                Debug.Log("Password를 입력해주세요.");
            }
        }
    }

    // 로그인 Btn 연동 Method
    public void Login()
    {
        if (!string.IsNullOrWhiteSpace(Login_ID.text) && !string.IsNullOrWhiteSpace(Login_PW.text))
        {
            SQL_Manager.instance.SQL_Login(Login_ID.text, Login_PW.text);
        }
        else
        {
            // 공백 검사를 통해 어떤 입력 필드가 유효하지 않은지 확인하고 메시지를 출력
            if (string.IsNullOrWhiteSpace(Sign_ID.text))
            {
                Debug.Log("올바른 ID를 입력해주세요.");
            }
            if (string.IsNullOrWhiteSpace(Sign_PW.text))
            {
                Debug.Log("올바른 PW를 입력해주세요.");
            }
        }
    }

    // 프로필 생성 Btn 연동 Method
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
                Debug.Log("올바른 프로필 닉네임을 입력해주세요.");
            }
        }
    }

    // 프로필 출력 Btn 연동 Method
    public void Print_Profile()
    {
        SQL_Manager.instance.SQL_Profile_ListSet();
        // List의 Count대로 Panel생성
        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i ++)
        {
            GameObject panel = Instantiate(Profile_Panel);
            panel.transform.SetParent(Panel_Parent);
            Panel_List.Add(panel);
        }

        // Profile Index에 맞게 프로필 name 출력
        for(int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = Panel_List[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
        }
    }

    // Join했을 때 씬 옮기는 테스트 메소드입니다.
    public void test_Change_Scene()
    {
        ranking.SetActive(true);
        infomation.SetActive(false);
    }
    #endregion

}
