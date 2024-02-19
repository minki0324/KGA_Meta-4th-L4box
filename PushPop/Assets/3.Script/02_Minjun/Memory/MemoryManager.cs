using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;
    [SerializeField] private Animator StartPanel;
    [SerializeField] public TMP_Text StageIndex;
    [SerializeField] public TMP_Text ScoreText;
    [SerializeField] private GameObject[] Heart;
    public int currentStage =1;
    public int Life = 3;
    public int Score = 0;
    public MemoryStageData[] stages;
    public Transform SapwnPos;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        CreatBoard();
    }
    public void CreatBoard()
    {
        Instantiate(stages[currentStage-1].board, SapwnPos.position, Quaternion.identity,gameObject.transform);
    }
    public MemoryStageData GetStage()
    {
        return stages[currentStage-1];
    }
    public void PlayStartPanel(string Text)
    {
        StartPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = Text;
        StartPanel.SetTrigger("isStart");
    }
    public void SetStageIndex()
    {
        StageIndex.text = $"{currentStage} 스테이지";
    }
    public void LifeRemove()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            if (Heart[2 - i].activeSelf)
            {
                Heart[2 - i].SetActive(false);
                break;
            }
        }
    }

    public void ResetLife()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            if (!Heart[2 - i].activeSelf)
            {
                Heart[2 - i].SetActive(true);
            }
        }
    }
    public void AddScore()
    {
        Score += 100;
        ScoreText.text = $"점수 : {Score}";
    }
}
