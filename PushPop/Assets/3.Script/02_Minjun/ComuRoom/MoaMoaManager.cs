using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using Mirror;

/// <summary>
/// Network Scene 통합 Manager Class
/// </summary>
public class MoaMoaManager : MonoBehaviour
{
    [Header("Current MoaMoa Profile Info")] [Space(5)]
    public int CurrentProfileIndex; // MoaMoa 띄워주는 Profile Index
    public string CurrentProfileName;
    [SerializeField] private TMP_Text profileNameText;

    [Header("Heart And Like")] [Space(5)]
    [SerializeField] private TMP_Text likeCount;
    [SerializeField] private TMP_Text heartCount;
    [SerializeField] private TMP_Text errorLog;
    private Coroutine errorLogCoroutine;

    [Header("MoaMoa Object")] [Space(5)]
    [SerializeField] private GameObject popBtn;                         // Pushpop Prefabs
    [SerializeField] private Sprite[] btnSprite;                        // Pushpop Color Sprite
    [SerializeField] private Image[] moamoaImage;                       // PushPush Image
    [SerializeField] private SpriteAtlas atlas;                         // PushPush Image Atlas
    public List<PushPushObject> pushObjs = new List<PushPushObject>(); // PushPushObject Class List

    [Header("Other Component")] [Space(5)]
    [SerializeField] private GameObject MoaMoaPanel;
    private NetworkManager manager;

    #region Unity Callback
    private void Awake()
    {
        manager = FindObjectOfType<NetworkManager>();
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

    public void MoaMoaPanelActive()
    {
        // 화면 상단 이름 설정
        profileNameText.text = CurrentProfileName;

        // 모아모아 리스트 세팅
        pushObjs = SQL_Manager.instance.SQL_SetPushPush(CurrentProfileIndex);
        SetMoaMoaList();

        // 켜고 끄기
        bool active = MoaMoaPanel.activeSelf;
        MoaMoaPanel.SetActive(!active);
    }

    public void LikeAndHeartButton(bool _isLikeButton)
    { // 매개변수 Bool값이 True면 좋아요 버튼 False면 싫어요 버튼
        AudioManager.Instance.SetTalkTalkAudioClic_SFX(1, false);
        HeartAndLike heartAndLikeList = SQL_Manager.instance.SQL_HeartAndLikeListSet(CurrentProfileIndex);

        // 신규 등록 혹은 리스트 초기화 로직
        if (heartAndLikeList == null)
        {
            heartAndLikeList = new HeartAndLike(new List<int>(), new List<int>());
        }

        // 누른 버튼에 따라 (bool값) 어떤 list를 갱신해야 하는지 targetlist 설정
        List<int> targetList = _isLikeButton ? heartAndLikeList.PressLikeProfileIndex : heartAndLikeList.PressHeartProfileIndex;
        if (targetList == null)
        { // 기존에 좋아요나 하트 리스트가 없었다면 새로 생성
            targetList = new List<int>();
        }

        // 프로필 인덱스 추가 로직
        if (!targetList.Contains(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex))
        { // 타겟 리스트 안에 일치하는 인덱스가 없을 경우 새로 추가하고 DB에 저장
            targetList.Add(ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex);

            // 데이터베이스 업데이트 호출
            SQL_Manager.instance.SQL_UpdateHeartAndLikeCount(CurrentProfileIndex, _isLikeButton);
            SQL_Manager.instance.SQL_UpdateHeartAndLikeList(heartAndLikeList, CurrentProfileIndex);
            UpdateLikeAndHeartCount();
        }
        else
        { // 이미 좋아요 혹은 하트를 누른 경우에 대한 처리
            if(errorLogCoroutine != null)
            {
                StopCoroutine(errorLogCoroutine);
            }
            errorLogCoroutine = StartCoroutine(ErrorLog_Co(_isLikeButton));
        }
    }

    public void MoaMoaPanelTalkTalkButton()
    { // 모아모아 패널에 있는 토크토크 버튼 (모아모아에서 토크토크로 나갈때)
        pushObjs.Clear();
        for(int i = 0; i < moamoaImage.Length; i++)
        {
            moamoaImage[i].sprite = ProfileManager.Instance.NoneBackground;
            for(int j = 0; j < moamoaImage[i].transform.childCount; j++)
            {
                Destroy(moamoaImage[i].transform.GetChild(j).gameObject);
            }
        }
    }

    public void TalktalkPanelMoaMoaButton()
    { // 토크토크 패널에 있는 모아모아 버튼 (토크토크에서 모아모아로 들어갈 때)
        CurrentProfileIndex = ProfileManager.Instance.PlayerInfo[(int)Player.Player1].playerIndex;
        CurrentProfileName = $"{ProfileManager.Instance.PlayerInfo[(int)Player.Player1].profileName}#{CurrentProfileIndex}";
    }

    public void UpdateLikeAndHeartCount()
    { // 좋아요 혹은 하트 숫자 갱신 메소드
        int[] heartandlikearr = SQL_Manager.instance.SQL_HeartAndLikeCount(CurrentProfileIndex);
        likeCount.text = heartandlikearr[0].ToString();
        heartCount.text = heartandlikearr[1].ToString();
    }

    public void GotoLobby()
    {
        //DB그대로 이동하게끔하기
        if (manager != null)
        {
            //로비이동
            manager.StopClient();
        }
        else
        {
            Debug.Log("네트워크매니저가 존재하지 않습니다.");
        }
    }

    private IEnumerator ErrorLog_Co(bool _isLikeButton)
    { // 좋아요나 하트를 이미 누른 유저에게 다시 버튼 눌렀을 때 출력되는 error log
        errorLog.gameObject.SetActive(true);
        errorLog.text = _isLikeButton ? "이미 좋아요를 누른 유저입니다." : "이미 하트를 누른 유저입니다.";
        yield return new WaitForSeconds(2f);
        errorLog.gameObject.SetActive(false);
    }
    #endregion
}
