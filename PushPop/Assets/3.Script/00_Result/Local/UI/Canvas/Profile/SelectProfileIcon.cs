using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectProfileIcon : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text WarningLog;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null && eventData.pointerPress.GetComponent<Button>() != null)
        {
            return;
        }
        else
        {
            ProfileManager.Instance.isImageSelect = false;
        }
    }

    public void SelectImage(int _index)
    { // Default profile icon index ¿˙¿Â
        ProfileManager.Instance.TempImageIndex = _index;
        ProfileManager.Instance.isImageSelect = true;
    }
}
