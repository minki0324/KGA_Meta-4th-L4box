using UnityEngine;

public class InfinityManager : MonoBehaviour
{
    [SerializeField] private Transform boardTrans;
    [SerializeField] private Transform buttonTrans;

    private void OnEnable()
    {
        GameSetting();
    }

    private void OnDisable()
    {
        Init();
    }

    public void Init()
    {
        
    }

    public void GameSetting()
    {

    }

    public void GameStart()
    {
        PushPop.Instance.BoardPos = boardTrans.position;
        PushPop.Instance.CreatePushPopBoard(boardTrans);
        PushPop.Instance.CreateGrid();
        PushPop.Instance.PushPopButtonSetting(buttonTrans);
    }
}
