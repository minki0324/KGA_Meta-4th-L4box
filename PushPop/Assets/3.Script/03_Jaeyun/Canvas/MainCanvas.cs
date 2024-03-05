using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private ProfileCanvas profileCanvas = null;
    [SerializeField] private MultiCanvas multiCanvas = null;

    [Header("Side Panel")]
    public GameObject titleText = null;
    public GameObject optionButton = null;
    public GameObject profileButton = null;
    public GameObject homeButton = null;

    [Header("GameMode Panel")]
    public GameObject pushpushButton = null;
    public GameObject speedButton = null;
    public GameObject memoryButton = null;
    public GameObject multiButton = null;
    public GameObject networkButton = null;

    [Header("Panel")]
    [SerializeField] private GameObject timeSettingPanel = null;
    [SerializeField] private GameObject optionPanel = null;

    public void GameModeButton()
    { // Time Set Start

    }
    public void ProfileButton()
    { // Profile Canvas Active

    }
    public void OptionButton()
    {

    }
    public void HomeButton()
    {

    }
}
