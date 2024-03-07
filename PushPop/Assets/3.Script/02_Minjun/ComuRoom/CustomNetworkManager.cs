using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;
using System;

public class CustomNetworkManager : NetworkManager
{
    public GameObject[] avatars;
    private NetworkConnectionToClient connc;
    // Start is called before the first frame update
    public Button selectButton;
    Func<string, string, string> func = (x, y) => x + y;
    public GameObject playerPrefab;
    List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        connc = conn;
        Debug.Log(connc.connectionId);
    }
    private void Start()
    {
        // �͸� �Լ� ���� (C#)
        Func<int, int, int> add = delegate (int x, int y) { return x + y; };
        Console.WriteLine(add(2, 3));  // ���: 5

        // �ݹ� �Լ��� ���Ǵ� �͸� �Լ�
        List<int> filteredNumbers = numbers.FindAll(delegate (int x) { return x % 2 == 0; });
        foreach (var num in filteredNumbers)
        {
            Console.WriteLine(num);  // ���: 2, 4
        }
        func("��", "ȣ����������");
    }
}

