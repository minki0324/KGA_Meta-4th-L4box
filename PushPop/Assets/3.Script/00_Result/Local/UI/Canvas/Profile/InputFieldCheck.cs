using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InputFieldCheck : MonoBehaviour
{ // Create Name InputField Check
    [SerializeField] private TMP_InputField ProfileNameInputField;
    public TMP_Text WarningLog;

    private void OnEnable()
    {
        ProfileNameInputField.text = string.Empty;
        ProfileNameInputField.onValidateInput += ValidateInput;
    }

    private void OnDisable()
    {
        ProfileNameInputField.onValidateInput -= ValidateInput;
    }

    public bool ProfileNameCheck()
    { // 각 조건들을 통과하면 (비속어, 초성, 글자수 제한 등) 등록 가능한 Name으로 판정하고 Profile Manager의 각 플레이어의 name 변수에 추가
        // 비속어 체크
        for (int i = 0; i < DataManager.Instance.VulgarismArray.Length; i++)
        {
            if (ProfileNameInputField.text.Contains(DataManager.Instance.VulgarismArray[i]))
            {
                if (DataManager.Instance.VulgarismArray[i] != string.Empty)
                {
                    ProfileManager.Instance.PrintErrorLog(WarningLog, "비속어는 포함시킬 수 없습니다.");
                    ProfileNameInputField.text = string.Empty;
                    return false;
                }
            }
        }
        // 초성 체크
        for (int i = 0; i < ProfileNameInputField.text.Length; i++)
        {
            if (Regex.IsMatch(ProfileNameInputField.text[i].ToString(), @"[^0-9a-zA-Z가-힣]"))
            {
                ProfileManager.Instance.PrintErrorLog(WarningLog, "초성은 입력이 불가능합니다.");
                ProfileNameInputField.text = string.Empty;
                return false;
            }
        }
        // 글자수 체크
        if (ProfileNameInputField.text.Length < 2)
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "2~6 글자의 이름을 입력해주세요.");
            ProfileNameInputField.text = string.Empty;
            return false;
        }

        ProfileManager.Instance.TempProfileName = ProfileNameInputField.text; // profile temp name save
        return true;
    }

    private char ValidateInput(string _text, int _charIndex, char _addedChar)
    { // Profile 영어, 숫자 입력 못하도록 설정
        if ((_addedChar >= 'a' && _addedChar <= 'z') || (_addedChar >= 'A' && _addedChar <= 'Z') || (_addedChar >= '0' && _addedChar <= '9'))
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "한글로 입력 해주세요.");
            return '\0'; // 입력 막음
        }

        return _addedChar;
    }
}
