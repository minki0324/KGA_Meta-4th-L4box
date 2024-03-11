using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "Stage", menuName = "StageData")]
public class MemoryStageData : ScriptableObject
{
    public GameObject board; // board Prefabs
    public int CorrectCount; // stage 정답 개수
    public bool isSpecialStage;
}
