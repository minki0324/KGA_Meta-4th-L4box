using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

public class TouchManager : MonoBehaviour
{
    //This is a simple script that does a raycast and plays the VFX on a prefab
    public GameObject VFXPrefab;
    public GameObject TouchMovePrefab;
    VisualEffect[] visualEffects;
    public Camera effectCamera;
    Coroutine touchTimer;
    public Transform particleCanvas;
    [SerializeField] private RawImage rawImage;

    private Vector2 nowPos;
    private Vector2 prePos;
    private Vector3 movePos;
    private float speed = 0.25f;
    public Transform touchTransform;

    public bool bisTouch = false;
    public bool bisDrag = false;

    void Start()
    {
        visualEffects = VFXPrefab.GetComponentsInChildren<VisualEffect>();
        rawImage.texture.width = Screen.width;
        rawImage.texture.height = Screen.height;
    }

    void Update()
    {
        //if (Input.touchCount == 1)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        prePos = touch.position = touch.deltaPosition;
        //    }
        //    else if (touch.phase == TouchPhase.Moved)
        //    {
        //        nowPos = touch.position - touch.deltaPosition;
        //        movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * speed;
        //        touchTransform.Translate(movePos);
        //        prePos = touch.position - touch.deltaPosition;
        //    }
        //}

        ClickEffect();
    }

    private void ClickEffect()
    {
        //ui�� Ŭ���� ��ǥ�� world�� �ٲ㼭 �� ��ġ�� ����

        //���콺 Ŭ���� ���¿��� ���콺 Ŭ����ġ�� ������ġ�� �ٸ��� ����Ʈ ����
        //position�� ���� ��ġ ��ġ
        //deltaposition�� �� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ��ġ�� ����, ��ġ�� �̵���

        if (Input.touchCount == 1)
        {
           
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

                bisTouch = true;

                nowPos = touch.position;
                TouchEffect();


            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                //��ġ���̸�
                float i = 0;
                i += Time.deltaTime;
                if (i > 1)
                {
                    bisDrag = true;
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                bisTouch = false;
                bisDrag = false;
            }

        }


        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.z = 1;


        //GameObject vfxEffect = Instantiate(VFXPrefab, pos, Quaternion.identity);
        //vfxEffect.transform.parent = particleCanvas;


        //visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

        //for (int i = 0; i < visualEffects.Length; i++)
        //{
        //    visualEffects[i].SendEvent("Click");
        //}
        //Destroy(vfxEffect, 1.5f);



    }
    

    public void TouchEffect()
    {
        if(bisTouch)
        {
            Vector3 worldPos = new Vector3();

            worldPos = Camera.main.ScreenToWorldPoint(nowPos);
            worldPos.z = 1;
            GameObject vfxEffect = Instantiate(VFXPrefab, worldPos, Quaternion.identity);

            vfxEffect.transform.parent = particleCanvas;
            visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

            for (int i = 0; i < visualEffects.Length; i++)
            {
                visualEffects[i].SendEvent("Click");
            }
            Destroy(vfxEffect, 1.5f);
            bisTouch = false;
        }
 
    }

    public void DragEffect()
    {
        //��ġ�� 1�� �̻� ���ӵǸ� isDrag �� true�� ����

        //    nowPos = touch.position - touch.deltaPosition;
        //    worldPos = Camera.main.ScreenToWorldPoint(nowPos);
        //    worldPos.z = 1;
        //    GameObject vfxEffect = Instantiate(TouchMovePrefab, worldPos, Quaternion.identity);

        //    vfxEffect.transform.parent = particleCanvas;
        //    visualEffects = vfxEffect.GetComponentsInChildren<VisualEffect>();

        //    for (int i = 0; i < visualEffects.Length; i++)
        //    {
        //        visualEffects[i].SendEvent("Click");
        //    }
        //    Destroy(vfxEffect, 1.5f);
    }



    private IEnumerator ClickStartTimer_Co()
    {
        effectCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        effectCamera.gameObject.SetActive(false);
    }


}
