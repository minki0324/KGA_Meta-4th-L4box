using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("Ready")]
    public GameObject Ready = null;

    [Header("Memory Game")]
    public GameObject MemoryGame = null;

    [Header("Panel")]
    public GameObject HintGuidePanel = null;
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;
}
