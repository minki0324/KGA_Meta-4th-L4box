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
    [SerializeField] private GameObject Lobby;
    [SerializeField] public GameObject ResultPanel;
    [SerializeField] private GameObject[] Heart;
    
    public MemoryBoard currentBoard;
    public int currentStage = 1;
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
        Instantiate(stages[currentStage - 1].board, SapwnPos.position, Quaternion.identity, gameObject.transform);
    }
    public MemoryStageData GetStage()
    {
        return stages[currentStage - 1];
    }
    public void PlayStartPanel(string Text)
    {
        StartPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = Text;
        StartPanel.SetTrigger("isStart");
    }
    public void SetStageIndex()
    {
        StageIndex.text = $"{currentStage} ��������";
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
        ScoreText.text = $"���� : {Score}";
    }
    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = $"���� : {Score}";
    }
    #region ������ �������� �¸��й� or ����
    //Stage ���� �Ǵ��� MemoryPushpop�� ����
    public void onStageEnd(bool isRetry =false)
    {//ResultPanel �ٽý��۹�ư  : �����ϰ� �ٽý���
        //���纸�� ���ֱ�

        Destroy(currentBoard.gameObject);
        //stage�ʱ�ȭ
        currentStage = 1;
        SetStageIndex();
        //�������ʱ�ȭ
        Life = 3;
        ResetLife();
        //���� ����ϱ� //������ �ּ�Ǯ��
        //Ranking.instance.SetScore(GameManager.Instance.ProfileName, GameManager.Instance.ProfileIndex, Score);
        //���ھ��ʱ�ȭ
        ResetScore();
       
        if (isRetry)
        {//�ٽ��ϱ� ��ư
            CreatBoard();
        }
        else
        {//������ ��ư
            StartCoroutine(ExitToLobby());
        }
    }
    public void onStageFail(bool isGiveUp)
    {//��������޼ҵ� (����) ���ӵ��� Back��ư : ����ȵǰ� ������
        //���纸�� ���ֱ�
        Destroy(currentBoard.gameObject);
        //stage�ʱ�ȭ
        currentStage = 1;
        SetStageIndex();

        //�������ʱ�ȭ
        Life = 3;
        ResetLife();
        Ranking.instance.score = Score;
        Ranking.instance.test_Set_Score();
        Ranking.instance.test_Print_Rank("Memory");
        //���ھ��ʱ�ȭ
        ResetScore();
        //�޸� �κ�� ������
        StartCoroutine(ExitToLobby());
        //SQL_Manager.instance.SQL_ProfileListSet()

    }
    #endregion

    private IEnumerator ExitToLobby()
    {
        PlayStartPanel("��������");
        yield return new WaitForSeconds(2f);
        Lobby.SetActive(true);
        gameObject.SetActive(false);
    }
}
