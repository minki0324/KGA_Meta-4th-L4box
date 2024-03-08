using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SelectCharacter : MonoBehaviour
{
    public GameObject[] avatars;
    public GameObject selectAvatars;
    #region Unity Callback
    #endregion

    #region Other Method
    // Network Room에서 사용할 캐릭터 선택 Btn 연동 Method
    public void ChoiceCharacter(int _selectindex)
    {
        // index에 따라서 플레이어 캐릭터 다른걸로 나오도록 수정
        GameObject selectAvatars = avatars[_selectindex];
        NetworkManager.singleton.playerPrefab = selectAvatars;
    }
    #endregion
}
