using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="DialogData", menuName = "Dialog")]
public class DialogData : ScriptableObject
{
    public string[] title;
    [TextArea] public string[] speedResult;
    [TextArea] public string[] memoryResult;
}
