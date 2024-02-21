using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;
    [SerializeField] private Animator StartPanel; //���ӽ��� ,�Ǹ��ؿ� ������ִ� �ǳ� Ani
    [SerializeField] public TMP_Text StageIndex; //ȭ��� ǥ���ϴ� ��������
    [SerializeField] public TMP_Text ScoreText; //�����ؽ�Ʈ
    [SerializeField] private GameObject Lobby; //Ǫ��Ǫ�� ���ǵ� �޸� ����â
    [SerializeField] public GameObject ResultPanel; //���������� , AllClear�� �ߴ� ���â
    [SerializeField] private GameObject[] Heart; //�����Ÿ���� ��Ʈ������Ʈ �迭
    
    public MemoryBoard currentBoard; //���� ��ȯ���ִ� Ǫ���˺�����
    public int currentStage = 1; //���罺������
    public int Life = 3; //���������
    public int Score = 0; //���罺�ھ�
    public MemoryStageData[] stages; //�������� ScriptableObject �迭 ���罺������������ �����̴ٸ� / ������,���䰹��, ����Ƚ�����������
    public Transform SapwnPos; //Ǫ���˺����� ��ȯ��ġ
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        //ó�� Gameplay�ǳ� ���۽� �����Ǽ�ȯ(���ӽ���) 
        CreatBoard();
    }
    public void CreatBoard()
    {//���� ���������� �´� ������ ��ȯ
        Instantiate(stages[currentStage - 1].board, SapwnPos.position, Quaternion.identity, gameObject.transform);
    }
    public MemoryStageData GetStage()
    {//�ٸ������� ���罺������ ��������
        return stages[currentStage - 1];
    }
    public void PlayStartPanel(string Text)
    { //�־��� ��
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
    {//�κ񳪰���
        PlayStartPanel("��������");
        yield return new WaitForSeconds(2f);
        Lobby.SetActive(true);
        gameObject.SetActive(false);
    }
}
