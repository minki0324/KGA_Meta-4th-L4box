using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.U2D;

/// <summary>
/// Network Scene 통합 Manager Class
/// </summary>
public class CommunicationRoomManager : NetworkBehaviour
{
    [SerializeField] private GameObject popBtn;                         // Pushpop Prefabs
    [SerializeField] private Sprite[] btnSprite;                        // Pushpop Color Sprite
    [SerializeField] private Image[] moamoaImage;                       // PushPush Image
    [SerializeField] private SpriteAtlas atlas;                         // PushPush Image Atlas

    private List<PushPushObject> pushObjs = new List<PushPushObject>(); // PushPushObject Class List

    #region Unity Callback
    private void Start()
    {
        pushObjs = SQL_Manager.instance.SQL_SetPushPush(GameManager.Instance.ProfileIndex);
        SetMoaMoaList();
    }
    #endregion

    #region Other Method
    public void SetMoaMoaList()
    { // MoaMoa 메인화면에 출력할 6개의 컬렉션을 출력하는 Btn 연동 Method
        for (int i = 0; i < pushObjs.Count; i++)
        { // Profile에 저장된 PushpushObj의 갯수

            // 저장된 Sprite 출력
            moamoaImage[i].sprite = atlas.GetSprite(pushObjs[i].spriteName.ToString());

            // 기존 PushPush에서 사용했던 크기로 먼저 세팅
            moamoaImage[i].SetNativeSize();
            moamoaImage[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            for(int j = 0; j < pushObjs[i].childIndex; j++)
            { // PushPushObject Class에 저장되어있는 Btn의 index
                // 저장된 만큼버튼 생성 및 부모설정
                GameObject pop = Instantiate(popBtn, moamoaImage[i].transform);

                // 버튼의 색깔 Index에 맞게 Sprite 변경
                pop.GetComponent<Image>().sprite = btnSprite[pushObjs[i].childSpriteIndex[j]];

                // Scale과 Position 세팅
                pop.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                pop.transform.localPosition = pushObjs[i].childPosition[j];
            }

            // 세팅이 끝났으면 컬렉션 Bubble의 크기만큼 스케일 조정
            moamoaImage[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void GotoLobby()
    {
        //DB그대로 이동하게끔하기
        if (NetworkManager.singleton != null)
        {
            //로비이동
            NetworkManager.singleton.StopClient();
        }
        else
        {
            Debug.Log("네트워크매니저가 존재하지 않습니다.");
        }
    }
    #endregion

    #region SyncVar
    #endregion

    #region Hook Method
    #endregion

    #region Client
    #endregion

    #region Command
    #endregion

    #region ClientRPC
    #endregion

    #region Server
    #endregion




}
