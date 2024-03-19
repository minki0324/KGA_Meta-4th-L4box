using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class AsyncLoading : MonoBehaviour
{
    public AsyncLoading instance = null;

    public bool bisLoadingEnd { get; private set; }
    [SerializeField] private NetworkManager manager;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }

        manager = GetComponent<NetworkManager>();

    }



    private void OnEnable()
    {
        bisLoadingEnd = false;
        StartCoroutine(LoadAsyncScene_co("01_Network"));
    }

    public IEnumerator LoadAsyncScene_co(string _name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_name);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            yield return null;
        }

        operation.allowSceneActivation = true;
        bisLoadingEnd = true;


    }
}
