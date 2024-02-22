using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public GameObject ResultPanel;
    public TMP_Text[] ResultText; // title, message
    public TMP_Text ScoreText;

    public string title = string.Empty;
    public string message = string.Empty;
    private Sprite pushPopImage;
    private string score;


}
