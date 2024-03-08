using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Select Panel")]
    public GameObject SelectDifficultyPanel = null;
    public GameObject SelectCategoryPanel = null;

    [Header("Ready")]
    public GameObject Ready = null;

    [Header("Speed Game")]
    public GameObject SpeedGame = null;
    public GameObject Timer = null;
    public GameObject CountSlider = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;
}
