using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Profile_ : MonoBehaviour
{
    [Header("Profile_Panel")]
    public GameObject selectProfile_Panel;
    public GameObject createName_Panel;
    public GameObject createImage_Panel;
    public GameObject currnetProfile_Panel;

    [Header("Button")]
    [SerializeField] private Button Back_Btn;
    [SerializeField] private Button Profile_Create;


    [Header("Other Object")]
    [SerializeField] private TMP_InputField Profile_name;
    [SerializeField] private GameObject Profile_Panel;
    [SerializeField] private Transform Panel_Parent;
    [SerializeField] private List<GameObject> Panel_List;
    public TMP_Text Select_Name;

    #region Unity Callback
    private void Awake()
    {
        Print_Profile();
    }
    #endregion

    #region Other Method
    // 프로필 생성 Btn 연동 Method
    public void Add_Profile()
    {
        if (!string.IsNullOrWhiteSpace(Profile_name.text))
        {
            SQL_Manager.instance.SQL_Add_Profile(Profile_name.text);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(Profile_name.text))
            {
                Debug.Log("올바른 프로필 닉네임을 입력해주세요.");
            }
        }
    }

    // 프로필 출력 Btn 연동 Method
    public void Print_Profile()
    {
        SQL_Manager.instance.SQL_Profile_ListSet();

        // 뒤로가기 버튼 등으로 이미 생성 되어있을 경우 초기화
        for (int i = 0; i < Panel_List.Count; i++)
        {
            Destroy(Panel_List[i].gameObject);
        }
        Panel_List.Clear();

        // List의 Count대로 Panel생성
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            GameObject panel = Instantiate(Profile_Panel);
            panel.transform.SetParent(Panel_Parent);
            Panel_List.Add(panel);
        }

        // Profile Index에 맞게 프로필 name 출력
        for (int i = 0; i < SQL_Manager.instance.Profile_list.Count; i++)
        {
            Profile_Information info = Panel_List[i].GetComponent<Profile_Information>();
            info.Profile_name.text = SQL_Manager.instance.Profile_list[i].name;
        }
    }

    // 프로필 수정 Btn 연동 Method
    public void Update_Profile()
    {

    }

    // 프로필 선택 후 씬 변경 Method
    public void Next_Scene()
    {
        SceneManager.LoadScene(1);
    }
    #endregion
}
