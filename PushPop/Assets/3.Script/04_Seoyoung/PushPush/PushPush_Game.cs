using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PushPush_Game : MonoBehaviour
{
    [Header("�г�")]
    [SerializeField] private PushPush_Canvas pushpush_Canvas;
    [SerializeField] private Help_Canvas help_Canvas;
    [SerializeField] private GameObject selectCategory_Panel;


    [Header("�ٹ̱� �г�")]
    [SerializeField] private GameObject Colorset_Panel;
    [SerializeField] private List<Button> colorButton_List;
    [SerializeField] private List<Sprite> colorSprite_List;

    [Header("���� ������")]
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


    //�ٹ̱� �г��� �ڷΰ��� ��ư 
    public void UndoBtn_Clicked()
    {

    }

    //�ٹ̱� �г��� ���� ��ư
    public void ResetBtn_Clicked()
    {

    }

    //�ٹ̱� �г��� ���� ��ư
    public void NextBtn_Clicked()
    {

    }

    #endregion
}
