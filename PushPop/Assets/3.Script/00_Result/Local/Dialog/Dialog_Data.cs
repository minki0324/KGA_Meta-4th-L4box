using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="Dialog_Data", menuName = "Dialog")]
public class Dialog_Data : ScriptableObject
{
    [TextArea]
    public string[] dialog_Script;
}
