using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    //���ӸŴ����� ���� ��
    None = 0,
    PushPush,
    Speed,
    Memory,
    Multi
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameMode gameMode;
    public int TimerTime;

    [Header("User Infomation")]
    public int UID;
    public string Profile_name;
    public int Profile_Index;
    public int DefaultImage;
    public Sprite[] ProfileImages;
    public bool _isImageMode = true; // false = �������, true = �̹��� ����

    #region Unity Callback
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region Other Method
    //���� ���۵Ǹ� ȣ��
    public IEnumerator Timer_co()
    {
        int t = TimerTime;
        while (true)
        {
            if (t <= 0)
            {
                //�ð� �ʱ�ȭ�ϱ�
                //Mainâ �ѱ�
                //���� ���� �˸� ����

                yield break;
            }
            t -= 1;

            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// ������ gameobject�� �Ű������� �޾Ƽ� �� ���� Image Component�� ���� ������ �̹����� ���
    /// </summary>
    /// <param name="printobj"></param>
    public void ProfileImagePrint(GameObject printobj)
    {
        Image image = printobj.GetComponent<Image>();

        if (_isImageMode) // �̹��� ���ø��
        {   // ����� Index�� �̹����� ������ Sprite�� �־���
            image.sprite = ProfileImages[DefaultImage];
        }
        else if (!_isImageMode) // ������� ���
        {
            Texture2D profileTexture = SQL_Manager.instance.SQL_LoadProfileImage(UID, Profile_Index);
            Sprite profileSprite = TextureToSprite(profileTexture);
            image.sprite = profileSprite;
        }
    }

    /// <summary>
    /// SQL_Manager���� Texture2D�� ��ȯ�� �̹��������� Sprite�� �ѹ� �� ��ȯ�ϴ� Method
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public Sprite TextureToSprite(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        return sprite;
    }
    #endregion

}
