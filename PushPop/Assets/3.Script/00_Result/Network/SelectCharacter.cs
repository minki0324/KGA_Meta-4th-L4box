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
    // Network Room���� ����� ĳ���� ���� Btn ���� Method
    public void ChoiceCharacter(int _selectindex)
    {
        // index�� ���� �÷��̾� ĳ���� �ٸ��ɷ� �������� ����
        GameObject selectAvatars = avatars[_selectindex];
        NetworkManager.singleton.playerPrefab = selectAvatars;
    }
    #endregion
}
