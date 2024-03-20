using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class AsyncLoading : MonoBehaviour
{
    //public AsyncLoading instance = null;
    //[SerializeField] private NetworkManager manager;

    public bool bisLoading { get; private set; } = true;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(instance);
        //}

        //manager = GetComponent<NetworkManager>();

    }



    private void OnEnable()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        StartCoroutine(LoadAsyncScene_co("01_Network Minki"));
    }

    private IEnumerator LoadAsyncScene_co(string _name)
    {
        bisLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(_name);  // 비동기 Scene 로딩 ( 로딩할 Scene 이름 )
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        bisLoading = false;
        operation.allowSceneActivation = true;
        


    }
}
