using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PushPush_Game : MonoBehaviour
{
    [Header("패널")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private HelpScriptManager help_Canvas;
    [SerializeField] private GameObject selectCategory_Panel;
    [SerializeField] private PuzzleLozic puzzleLozic_Panel;
    [SerializeField] private GameObject success_Panel;
    [SerializeField] private GameObject backBtn;

    [Header("꾸미기 패널")]
    [SerializeField] private GameObject Colorset_Panel;
    [SerializeField] private List<Button> colorButton_List;
    [SerializeField] private List<Sprite> colorSprite_List;

    [Header("몰드 아이콘")]
    [SerializeField] private Image Mold_Image;

    #region Unity Callback

    #endregion

    #region Other Method
    public void Init()
    {

        for (int i = 0; i < Colorset_Panel.transform.childCount; i++)
        {
            colorButton_List.Add(Colorset_Panel.transform.GetChild(i).GetComponent<Button>());
        }
    }



    public void BackBtn_Clicked()
    {
        AudioManager.Instance.SetCommonAudioClip_SFX(3);

        help_Canvas.gameObject.SetActive(true);
        selectCategory_Panel.SetActive(true);

        pushpush_Canvas.Enable_Objects();
        gameObject.SetActive(false);
    }
    public void TimeScalezero()
    {
        Time.timeScale = 0;
    }
    public void TimeScaleOwn()
    {
        Time.timeScale = 1;
    }

    public void Success_ActiveChange()
    {   
        pushpush_Canvas.Enable_Objects();
        success_Panel.SetActive(false);
        selectCategory_Panel.SetActive(true);
        gameObject.SetActive(false);
        help_Canvas.gameObject.SetActive(true);
        backBtn.SetActive(false);
    }

    #endregion
}
