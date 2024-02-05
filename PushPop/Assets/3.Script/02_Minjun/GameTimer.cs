using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    //게임시작하기전 타이머 설정 및 시작을 담당하는 스크립트
    //기본 5분 세팅
    private float gameTime = 300f;

    //버튼누를시 30초추가
    public void AddTime30()
    {
        gameTime += 30f;
        Mathf.Clamp(gameTime, 30f, 600f);
    }
    //버튼누를시 30초제거
    public void RemoveTime30()
    {
        gameTime -= 30f;
        Mathf.Clamp(gameTime, 30f, 600f);
    }
    public void StartTimer()
    {
        gameTime -= Time.deltaTime;
    }
}
