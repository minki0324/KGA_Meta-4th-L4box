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
    { // °¢ Á¶°ÇµéÀ» Åë°úÇÏ¸é (ºñ¼Ó¾î, ÃÊ¼º, ±ÛÀÚ¼ö Á¦ÇÑ µî) µî·Ï °¡´ÉÇÑ NameÀ¸·Î ÆÇÁ¤ÇÏ°í Profile ManagerÀÇ °¢ ÇÃ·¹ÀÌ¾îÀÇ name º¯¼ö¿¡ Ãß°¡
        // ºñ¼Ó¾î Ã¼Å©
        for (int i = 0; i < DataManager.Instance.VulgarismArray.Length; i++)
        {
            if (ProfileNameInputField.text.Contains(DataManager.Instance.VulgarismArray[i]))
            {
                if (DataManager.Instance.VulgarismArray[i] != string.Empty)
                {
                    ProfileManager.Instance.PrintErrorLog(WarningLog, "ºñ¼Ó¾î´Â Æ÷ÇÔ½ÃÅ³ ¼ö ¾ø½À´Ï´Ù.");
                    ProfileNameInputField.text = string.Empty;
                    return false;
                }
            }
        }
        // ÃÊ¼º Ã¼Å©
        for (int i = 0; i < ProfileNameInputField.text.Length; i++)
        {
            if (Regex.IsMatch(ProfileNameInputField.text[i].ToString(), @"[^0-9a-zA-Z°¡-ÆR]"))
            {
                ProfileManager.Instance.PrintErrorLog(WarningLog, "ÃÊ¼ºÀº ÀÔ·ÂÀÌ ºÒ°¡´ÉÇÕ´Ï´Ù.");
                ProfileNameInputField.text = string.Empty;
                return false;
            }
        }
        // ±ÛÀÚ¼ö Ã¼Å©
        if (ProfileNameInputField.text.Length < 2)
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "2~6 ±ÛÀÚÀÇ ÀÌ¸§À» ÀÔ·ÂÇØÁÖ¼¼¿ä.");
            ProfileNameInputField.text = string.Empty;
            return false;
        }

        ProfileManager.Instance.TempProfileName = ProfileNameInputField.text; // profile temp name save
        return true;
    }

    private char ValidateInput(string _text, int _charIndex, char _addedChar)
    { // Profile ¿µ¾î, ¼ýÀÚ ÀÔ·Â ¸øÇÏµµ·Ï ¼³Á¤
        if ((_addedChar >= 'a' && _addedChar <= 'z') || (_addedChar >= 'A' && _addedChar <= 'Z') || (_addedChar >= '0' && _addedChar <= '9'))
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "ÇÑ±Û·Î ÀÔ·Â ÇØÁÖ¼¼¿ä.");
            return '\0'; // ÀÔ·Â ¸·À½
        }

        return _addedChar;
    }
}
