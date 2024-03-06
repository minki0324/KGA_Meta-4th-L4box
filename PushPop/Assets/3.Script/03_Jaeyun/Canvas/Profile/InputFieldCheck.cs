using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class InputFieldCheck : MonoBehaviour
{ // Create Name InputField Check
    public TMP_InputField profileNameInputField = null;
    public TMP_Text WarningLog = null;

    private void OnEnable()
    {
        profileNameInputField.text = string.Empty;
        profileNameInputField.onValidateInput += ValidateInput;
    }

    private void OnDisable()
    {
        profileNameInputField.onValidateInput -= ValidateInput;
    }

    public bool ProfileNameCheck()
    { // �� ���ǵ��� ����ϸ� (��Ӿ�, �ʼ�, ���ڼ� ���� ��) ��� ������ Name���� �����ϰ� Profile Manager�� �� �÷��̾��� name ������ �߰�
        // ��Ӿ� üũ
        for (int i = 0; i < DataManager2.instance.vulgarism_Arr.Length; i++)
        {
            if (profileNameInputField.text.Contains(DataManager2.instance.vulgarism_Arr[i]))
            {
                if (DataManager2.instance.vulgarism_Arr[i] != string.Empty)
                {
                    ProfileManager.Instance.PrintErrorLog(WarningLog, "��Ӿ�� ���Խ�ų �� �����ϴ�.");
                    profileNameInputField.text = string.Empty;
                    return false;
                }
            }
        }
        // �ʼ� üũ
        for (int i = 0; i < profileNameInputField.text.Length; i++)
        {
            if (Regex.IsMatch(profileNameInputField.text[i].ToString(), @"[^0-9a-zA-Z��-�R]"))
            {
                ProfileManager.Instance.PrintErrorLog(WarningLog, "�ʼ��� �Է��� �Ұ����մϴ�.");
                profileNameInputField.text = string.Empty;
                return false;
            }
        }
        // ���ڼ� üũ
        if (profileNameInputField.text.Length < 2)
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "2~6 ������ �̸��� �Է����ּ���.");
            profileNameInputField.text = string.Empty;
            return false;
        }

        ProfileManager.Instance.TempProfileName = profileNameInputField.text; // profile temp name save
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
