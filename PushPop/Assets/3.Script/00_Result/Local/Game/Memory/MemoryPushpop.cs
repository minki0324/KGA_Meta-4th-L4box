using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPushpop : MonoBehaviour
{ // Memeory PushPop Button Prefabs
    private MemoryManager memoryManager = null;
    private MemoryBoard memoryBoard;
    [SerializeField] private Image popButtonImage;
    [SerializeField] private Button popButton;
    [SerializeField] private Animator popButtonAnimation;
    public bool IsCorrect = false; // 정답이면 true 아니면 false

    private void OnEnable()
    {
        popButtonImage.alphaHitTestMinimumThreshold = 0.1f; // Sprite에서 Alpha 값이 0.1 이하 일시 인식하지 않게함
        memoryBoard = transform.parent.GetComponent<MemoryBoard>();
        memoryManager = memoryBoard.memoryManager;
    }

    #region OnClick Method
    public void MemoryPopButtonClick()
    { // prefab onclick method
        if (memoryBoard.Stage.IsSpecialStage)
        {
            SpecialStage();
        }
        else
        {
            DefaultStage();
        }
    }

    public void DefaultStage()
    { // 일반 스테이지 버튼
        if (IsCorrect)
        { // 정답을 눌렀을때
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    public void SpecialStage()
    { // 스페셜 스테이지 버튼
        if (memoryBoard.IsOrder(this))
        { // 순서대로 눌렀을 때
            Correct();
        }
        else
        {
            Incorrect();
        }
    }
    #endregion
    #region Correct, Incorrect
    private void Correct()
    { // 정답 시
        AudioManager.Instance.SetAudioClip_SFX(3, false);
        // 점수 주기
        popButton.interactable = false; // 누른 버튼은 비활성화
        memoryBoard.CurrentCorrectCount++; // 정답 카운트 증가
        memoryManager.AddScore(100); // 점수 증가
        if (memoryBoard.IsStageClear())
        {
            StartCoroutine(StageClear_Co());
        }
    }
    private void Incorrect()
    { // 오답 시
        AudioManager.Instance.SetAudioClip_SFX(0, false);
        PlayShakePush(); // ani
        memoryManager.Life--;
        memoryManager.LifeRemove();

        if (memoryManager.Life.Equals(0))
        { // 라이프 모두 소진 시 게임 종료
            AudioManager.Instance.SetAudioClip_SFX(5, false);
            GameManager.Instance.GameEnd?.Invoke();
        }
    }
    #endregion
    #region Stage Clear Method
    private IEnumerator StageClear_Co()
    { // Stage Clear
        AudioManager.Instance.SetAudioClip_SFX(4, false);
        memoryBoard.ButtonAllStop(); // 버튼 동작 정지
        memoryManager.PlayStartPanel("훌륭해요!");

        yield return new WaitForSeconds(2f);

        memoryManager.CurrentStage++; //스테이지 Index 증가
        if (memoryManager.EndStageIndex < memoryManager.CurrentStage)
        { // 준비된 스테이지 < 현재 스테이지, 모든 스테이지 클리어 시
            GameManager.Instance.GameEnd?.Invoke();
            yield break;
        }

        Destroy(memoryBoard.gameObject); // 현재 스테이지 보드 지우기
        memoryManager.StageText.text = $"{memoryManager.CurrentStage} 단계";

        // 다음 스테이지로 (새로운 보드 생성) 
        memoryManager.CreatBoard();
    }
    #endregion
    #region Button Animation
    //본인이 정답인지 깜빡이는 메소드
    public void PlayBlink()
    { // 게임시작, 혹은 힌트 버튼 클릭 시 정답 버튼을 알려주는 메소드
        popButtonAnimation.SetTrigger("isBlink");
        if (memoryBoard.Stage.IsSpecialStage)
        { // 스페셜 스테이지일 때
            AudioManager.Instance.SetAudioClip_SFX(1, false);
        }
        else
        { // 스페셜 스테이지가 아닐 때
            AudioManager.Instance.SetAudioClip_SFX(2, false);
        }
    }

    private void PlayShakePush()
    { // 오답 버튼 선택 시 흔들리는 애니메이션
        popButtonAnimation.SetTrigger("isShake");
        popButtonImage.raycastTarget = false;
    }

    public void ShakeEndAfter()
    { // 애니메이션 Event로 추가되어 있음
        popButtonImage.raycastTarget = true;
    }
    #endregion
}
