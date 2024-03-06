using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugLog : MonoBehaviour
{
    public static DebugLog instance;

    [SerializeField] private TMP_Text[] message_Box;

    public Action<string> Message;

    string current_me = string.Empty;
    string past_me;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Message = Adding_Message;
        past_me = current_me;


    }

    public void Adding_Message(string m)
    {
        current_me = m;
        ReadText(current_me);
    }

    public void ReadText(string me)
    {
        bool isinput = false;

        foreach (TMP_Text message in message_Box)
        {
            message.gameObject.SetActive(true);
        }

        for (int i = 0; i < message_Box.Length; i++)
        {
            if (message_Box[i].text.Equals(""))
            {
                message_Box[i].text = me;
                isinput = true;
                break;
            }
        }
        if (!isinput)
        {
            for (int i = 1; i < message_Box.Length; i++)
            {
                message_Box[i - 1].text = message_Box[i].text;
                //미는 작업
            }
            message_Box[message_Box.Length - 1].text = me;
        }
    }
}
