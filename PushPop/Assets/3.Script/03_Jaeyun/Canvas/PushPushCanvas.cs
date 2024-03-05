using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPushCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;

    [Header("Side Panel")]
    public GameObject BackButton = null;
    public GameObject HelpButton = null;

    [Header("PushPush Game")]
    public GameObject PushpushGame = null;
    public GameObject CustomMode = null;

    [Header("Select Panel")]
    public GameObject SelectCategoryPanel = null;
    public GameObject SelectBoardPanel = null;

    [Header("Panel")]
    public GameObject ResultPanel = null;
    public GameObject WarningPanel = null;
    public GameObject GameReadyPanel = null;
    public GameObject HelpPanel = null;
}
