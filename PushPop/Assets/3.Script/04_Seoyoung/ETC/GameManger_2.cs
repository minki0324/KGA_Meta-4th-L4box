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


    public IEnumerator Timer_co()
    {
        int t = TimerTime;
        while(true)
        {
            if (t <= 0)
            {
                //�ð� �ʱ�ȭ�ϱ�
                //Mainâ �ѱ�
                //���� ���� �˸� ����

                yield break;
            }
            t -= 1;

            yield return new WaitForSeconds(1f);
        }
    }

}
