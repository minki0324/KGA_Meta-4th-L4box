using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPushManager : MonoBehaviour
{

    public CustomPushpopManager custom;
    public PuzzleLozic puzzle;
    [SerializeField] FramePuzzle frame;
    public int pushCount; //버튼눌렀을때 + 되는 카운트 pushCount == 버튼리스트.Count 비교해서 동일시 클리어 판정(GameManager)

    #region 푸시푸시게임 전체 콜백메소드 모음

    public void OnAllBubblesPopped() //버블이 모두 터지고 땅에닿았을때 불리는 메소드
    {
        puzzle.SettingGame();//퍼즐게임 시작
    }
    public void OnPuzzleSolved() //퍼즐을 모두 맞췄을때 불리는 메소드
    {
        AudioManager.instance.SetAudioClip_SFX(1, false);
        puzzle.successCount = 0;
        puzzle.onPuzzleClear?.Invoke();
    }
    public void OnCustomEndmethod() //커스텀하다가 다음으로 버튼 눌렀을때 불리는 메소드
    {//다음으로 버튼에 onClick 참조되있음.
        GameManager.Instance.GameClear();
        custom.decoPanel.SetActive(false);
        custom.onCustomEnd?.Invoke();
        frame._myImage.alphaHitTestMinimumThreshold = 0f;
    }
    public void OnButtonAllPush() //커스텀 완료하고 모두 눌렀을때
    {//버튼 모두 눌렀을때 자세한로직 GameManager GameClear에 있음.
        pushCount = 0; //
    }
    public void OnPushPushEnd() //푸시푸시가 모두 끝나고 초기화 할것들 모아둔 메소드
    {

    }
    #endregion



}
