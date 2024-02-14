using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PushPush_Game : MonoBehaviour
{
    [Header("패널")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;
    [SerializeField] private GameObject selectCategory_Panel;


    [Header("꾸미기 패널")]
    [SerializeField] private GameObject Colorset_Panel;
    [SerializeField] private List<Button> colorButton_List;
    [SerializeField] private List<Sprite> colorSprite_List;

    [Header("몰드 아이콘")]
    [SerializeField] private Image Mold_Image;

    #region Unity Callback

    private void OnEnable()
    {
        Mold_Image.sprite = pushpush_Canvas.SelectedMold;
    }


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
        help_Canvas.gameObject.SetActive(true);
        selectCategory_Panel.SetActive(true);

        pushpush_Canvas.Enable_Objects();
        gameObject.SetActive(false);
    }


    //꾸미기 패널의 뒤로가기 버튼 
    public void UndoBtn_Clicked()
    {

    }

    //꾸미기 패널의 리셋 버튼
    public void ResetBtn_Clicked()
    {

    }

    //꾸미기 패널의 다음 버튼
    public void NextBtn_Clicked()
    {

    }

    #endregion
}
