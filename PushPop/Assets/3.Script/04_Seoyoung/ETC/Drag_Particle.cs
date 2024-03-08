using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class Drag_Particle : MonoBehaviour
{
    //public int cashing = 1500;
    public float cashing = 1.2f;

    private void OnEnable()
    {
         StartCoroutine(Destory_co());
        //  Task.Run(() => Destroy_async());
        //Destroy_async();


      
    }

    private IEnumerator Destory_co()
    {//SetActive(false ÄÚ·çÆ¾)

        yield return new WaitForSeconds(cashing);
        gameObject.SetActive(false);
    }

    //async void Destroy_async()
    //{
    //    await Task.Run(() =>
    //    {
    //        Thread.Sleep(cashing);

    //        Test_s();
    //    });
    //}

    //public void Test_s()
    //{
    //    gameObject.SetActive(false);
    //}
}
