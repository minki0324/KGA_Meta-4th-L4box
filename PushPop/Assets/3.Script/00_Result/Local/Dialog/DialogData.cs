using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="DialogData", menuName = "Dialog")]
public class DialogData : ScriptableObject
{
    public string[] title;
    public string[] speedResult;
    public string[] memoryResult;
}
