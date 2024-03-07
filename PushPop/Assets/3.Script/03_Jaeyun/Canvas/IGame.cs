using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame
{
    void Init(); // OnEnable(), check list: coroutine, list, array, variables �ʱ�ȭ ����
    void GameSetting(); // OnDisable(), bubble size, board size, pushpopbutton size, pushpop percentage, etc. setting ����
    void GameStart(); // gameready coroutine -> gamestart ���� ���� ����
}
