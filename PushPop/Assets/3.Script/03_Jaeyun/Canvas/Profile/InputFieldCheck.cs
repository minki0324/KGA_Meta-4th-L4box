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
    { // °¢ Á¶°ÇµéÀ» Åë°úÇÏ¸é (ºñ¼Ó¾î, ÃÊ¼º, ±ÛÀÚ¼ö Á¦ÇÑ µî) µî·Ï °¡´ÉÇÑ NameÀ¸·Î ÆÇÁ¤ÇÏ°í Profile ManagerÀÇ °¢ ÇÃ·¹ÀÌ¾îÀÇ name º¯¼ö¿¡ Ãß°¡
        // ºñ¼Ó¾î Ã¼Å©
        for (int i = 0; i < DataManager2.instance.vulgarism_Arr.Length; i++)
        {
            if (profileNameInputField.text.Contains(DataManager2.instance.vulgarism_Arr[i]))
            {
                if (DataManager2.instance.vulgarism_Arr[i] != string.Empty)
                {
                    ProfileManager.Instance.PrintErrorLog(WarningLog, "ºñ¼Ó¾î´Â Æ÷ÇÔ½ÃÅ³ ¼ö ¾ø½À´Ï´Ù.");
                    profileNameInputField.text = string.Empty;
                    return false;
                }
            }
        }
        // ÃÊ¼º Ã¼Å©
        for (int i = 0; i < profileNameInputField.text.Length; i++)
        {
            if (Regex.IsMatch(profileNameInputField.text[i].ToString(), @"[^0-9a-zA-Z°¡-ÆR]"))
            {
                ProfileManager.Instance.PrintErrorLog(WarningLog, "ÃÊ¼ºÀº ÀÔ·ÂÀÌ ºÒ°¡´ÉÇÕ´Ï´Ù.");
                profileNameInputField.text = string.Empty;
                return false;
            }
        }
        // ±ÛÀÚ¼ö Ã¼Å©
        if (profileNameInputField.text.Length < 2)
        {
            ProfileManager.Instance.PrintErrorLog(WarningLog, "2~6 ±ÛÀÚÀÇ ÀÌ¸§À» ÀÔ·ÂÇØÁÖ¼¼¿ä.");
            profileNameInputField.text = string.Empty;
            return false;
        }

        ProfileManager.Instance.TempProfileName = profileNameInputField.text; // profile temp name save
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
