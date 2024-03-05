using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileCanvas : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainCanvas mainCanvas = null;
    [SerializeField] private MultiCanvas multiCanvas = null;

    [Header("Profile Panel")]
    public GameObject ProfilePanel = null;
    public GameObject Select = null;
    public GameObject CreateName = null;
    public GameObject CreateImage = null;
    public GameObject ProfileIconSelect = null;
    public GameObject CaptureCheck = null;
    public GameObject CurrentProfile = null;

    [Header("Exit Button")]
    public GameObject ExitButton = null;

    [Header("Delete Panel")]
    public GameObject DeletePanel = null;

    [Header("Current Profile")]
    public Image ProfileIamge = null;
    public TMP_Text ProfileText = null;

    #region Current Profile
    public void CurrentProfileSelectButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        if (!ProfileManager.Instance.isProfileSelected)
        {
            ProfileManager.Instance.isProfileSelected = true;
        }


    }

    public void CurrentProfileChangeButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);
        ProfileManager.Instance.isUpdate = true;

        CurrentProfile.SetActive(false);

        CreateName.SetActive(true);
    }
    public void CurrentProfileReturnButton()
    {
        AudioManager.instance.SetCommonAudioClip_SFX(3);

        CurrentProfile.SetActive(false);
        
        Select.SetActive(true);
        ExitButton.SetActive(true);
    }
    #endregion
}
