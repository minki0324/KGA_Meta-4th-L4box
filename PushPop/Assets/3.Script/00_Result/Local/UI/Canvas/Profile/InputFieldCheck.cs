using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class InputFieldCheck : MonoBehaviour
{ // Create Name InputField Check
    [SerializeField] private TMP_InputField ProfileNameInputField = null;
    public TMP_Text WarningLog = null;

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
    { // �� ���ǵ��� ����ϸ� (��Ӿ�, �ʼ�, ���ڼ� ���� ��) ��� ������ Name���� �����ϰ� Profile Manager�� �� �÷��̾��� name ������ �߰�
        // ��Ӿ� üũ
        for (int i = 0; i < DataManager.Instance.VulgarismArray.Length; i++)
        {
            if (ProfileNameInputField.text.Contains(DataManager.Instance.VulgarismArray[i]))
            {
                if (DataManager.Instance.VulgarismArray[i] != string.Empty)
                {
                    ProfileManager.Instance.PrintErrorLog(WarningLog, "��Ӿ�� ���Խ�ų �� �����ϴ�.");
                    ProfileNameInputField.text = string.Empty;
                    return false;
                }
            }
        }
        // �ʼ� üũ
        for (int i = 0; i < ProfileNameInputField.text.Length; i++)
        {
            if (Regex.IsMatch(ProfileNameInputField.text[i].ToString(), @"[^0-9a-zA-Z��-�R]"))
            {
                ProfileManager.Instance.PrintErrorLog(WarningLog, "�ʼ��� �Է��� �Ұ����մϴ�.");
                ProfileNameInputField.text = string.Empty;
                return false;
            }
        }
        // ���ڼ� üũ
        if (ProfileNameInputField.text.Length < 2)
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "2~6 ������ �̸��� �Է����ּ���.");
            ProfileNameInputField.text = string.Empty;
            return false;
        }

        ProfileManager.Instance.TempProfileName = ProfileNameInputField.text; // profile temp name save
        return true;
    }

    private char ValidateInput(string _text, int _charIndex, char _addedChar)
    { // Profile ����, ���� �Է� ���ϵ��� ����
        if ((_addedChar >= 'a' && _addedChar <= 'z') || (_addedChar >= 'A' && _addedChar <= 'Z') || (_addedChar >= '0' && _addedChar <= '9'))
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "�ѱ۷� �Է� ���ּ���.");
            return '\0'; // �Է� ����
        }

        return _addedChar;
    }
}
