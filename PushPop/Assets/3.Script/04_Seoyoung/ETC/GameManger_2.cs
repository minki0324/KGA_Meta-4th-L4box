using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameMode
{
    //���ӸŴ����� ���� ��
    None = 0,
    PushPush,
    Speed,
    Memory,
    Multi
}



public class GameManger_2 : MonoBehaviour
{
    public static GameManger_2 instance = null;
   
    public GameMode gameMode;

    public int TimerTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameMode = GameMode.None;
    }

}
