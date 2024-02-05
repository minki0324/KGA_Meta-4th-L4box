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

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameMode gameMode;
    public int TimerTime;


    [Header("User Infomation")]
    public int UID;
    public string Profile_name;
    public int Profile_Index;

    #region Unity Callback
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region Other Method
    public IEnumerator Timer_co()
    {
        int t = TimerTime;
        while (true)
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
    #endregion

}
