using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MuseumManager : NetworkBehaviour
{
 
    public void GotoLobby()
    {
        //DB�״�� �̵��ϰԲ��ϱ�
        if (NetworkManager.singleton != null)
        {
            //�κ��̵�
            NetworkManager.singleton.StopClient();
        }
        else
        {
            Debug.Log("��Ʈ��ũ�Ŵ����� �������� �ʽ��ϴ�.");
        }
    }
}
