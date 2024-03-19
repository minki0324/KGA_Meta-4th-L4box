using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameReadyPanelClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MultiCanvas multiCanvas = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (multiCanvas.Ready.activeSelf)
        {
            multiCanvas.StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}
