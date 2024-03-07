using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame
{
    void Init(); // OnEnable(), check list: coroutine, list, array, variables 초기화 관련
    void GameSetting(); // OnDisable(), bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting 관련
    void GameStart(); // gameready coroutine -> gamestart 게임 시작 관련
}
