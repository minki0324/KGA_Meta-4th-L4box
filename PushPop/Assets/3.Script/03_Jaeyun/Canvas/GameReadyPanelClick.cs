using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameReadyPanelClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject onPanel = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPanel.activeSelf)
        {
            Time.timeScale = 1f;
            AudioManager.Instance.Pause_SFX(false);
            gameObject.SetActive(false);
        }
    }
}
