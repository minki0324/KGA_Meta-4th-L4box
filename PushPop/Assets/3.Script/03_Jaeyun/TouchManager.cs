using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TouchManager : MonoBehaviour
{
    //This is a simple script that does a raycast and plays the VFX on a prefab
    public GameObject VFXPrefab;
    VisualEffect[] visualEffects;
    public Camera effectCamera;
    Coroutine touchTimer;
    public Transform particleCanvas;
    [SerializeField] private RawImage rawImage;


    void Start()
    {
        visualEffects = VFXPrefab.GetComponentsInChildren<VisualEffect>();
        rawImage.texture.width = Screen.width;
        rawImage.texture.height = Screen.height;
    }

    void Update()
    {
        ClickEffect();
    }

    private void ClickEffect()
    {       
        //ui�� Ŭ���� ��ǥ�� world�� �ٲ㼭 �� ��ġ�� ����
        if (Input.anyKeyDown)
        {
            if (touchTimer != null)
            {
                StopCoroutine(touchTimer);
            }
         
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 1;


            GameObject vfxEffect = Instantiate(VFXPrefab, pos, Quaternion.identity);
            vfxEffect.transform.parent = particleCanvas;

         
            visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

            for (int i = 0; i < visualEffects.Length; i++)
            {
                visualEffects[i].SendEvent("Click");
            }
            Destroy(vfxEffect, 1.5f);
        }
        /*if (Input.touchCount > 0)
        {
            if (touchTimer != null)
            {
                StopCoroutine(touchTimer);
            }
            touchTimer = StartCoroutine(ClickStartTimer_Co());

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase.Equals(TouchPhase.Began))
                {
                    GameObject vfxEffect = Instantiate(VFXPrefab, particleCanvas);
                    vfxEffect.transform.position = touch.position;
                    visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

                    for (int j = 0; j < visualEffects.Length; i++)
                    {
                        visualEffects[i].SendEvent("Click");
                    }
                    Destroy(vfxEffect, 1.5f);
                }
            }
        }*/
    }
    
    private IEnumerator ClickStartTimer_Co()
    {
        effectCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        effectCamera.gameObject.SetActive(false);
    }
}
