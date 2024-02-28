using UnityEngine;
using UnityEngine.VFX;

public class TouchManager : MonoBehaviour
{
    //This is a simple script that does a raycast and plays the VFX on a prefab
    public GameObject VFXPrefab;
    VisualEffect[] visualEffects;
    void Start()
    {
        visualEffects = VFXPrefab.GetComponentsInChildren<VisualEffect>();
    }
    void Update()
    {
        ClickEffect();
        /*if (Input.anyKeyDown)
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                VFXPrefab.transform.position = hit.point;
                for (int i = 0; i < visualEffects.Length; i++)
                {
                    visualEffects[i].SendEvent("Click");
                }
            }
        }*/
    }
    private void ClickEffect()
    {
        if (Input.anyKeyDown)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            VFXPrefab.transform.position = pos;
            for (int i = 0; i < visualEffects.Length; i++)
            {
                visualEffects[i].SendEvent("Click");
            }
        }

        // android
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase.Equals(TouchPhase.Began))
                {
                    VFXPrefab.transform.position = touch.position;
                    for (int j = 0; j < visualEffects.Length; i++)
                    {
                        visualEffects[i].SendEvent("Click");
                    }
                }
            }
        }
    }
}
