using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    //게임매니저에 넣을 것
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
                //시간 초기화하기
                //Main창 켜기
                //게임 종료 알림 띄우기

                yield break;
            }
            t -= 1;

            yield return new WaitForSeconds(1f);
        }
    }
    #endregion

}
