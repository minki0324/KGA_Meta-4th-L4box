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
    public bool IsCorrect = false; // �����̸� true �ƴϸ� false

    private void OnEnable()
    {
        popButtonImage.alphaHitTestMinimumThreshold = 0.1f; // Sprite���� Alpha ���� 0.1 ���� �Ͻ� �ν����� �ʰ���
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
    { // �Ϲ� �������� ��ư
        if (IsCorrect)
        { // ������ ��������
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    public void SpecialStage()
    { // ����� �������� ��ư
        if (memoryBoard.IsOrder(this))
        { // ������� ������ ��
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
    { // ���� ��
        AudioManager.Instance.SetAudioClip_SFX(3, false);
        // ���� �ֱ�
        popButton.interactable = false; // ���� ��ư�� ��Ȱ��ȭ
        memoryBoard.CurrentCorrectCount++; // ���� ī��Ʈ ����
        memoryManager.AddScore(100); // ���� ����
        if (memoryBoard.IsStageClear())
        {
            StartCoroutine(StageClear_Co());
        }
    }
    private void Incorrect()
    { // ���� ��
        AudioManager.Instance.SetAudioClip_SFX(0, false);
        PlayShakePush(); // ani
        memoryManager.Life--;
        memoryManager.LifeRemove();

        if (memoryManager.Life.Equals(0))
        { // ������ ��� ���� �� ���� ����
            AudioManager.Instance.SetAudioClip_SFX(5, false);
            GameManager.Instance.GameEnd?.Invoke();
        }
    }
    #endregion
    #region Stage Clear Method
    private IEnumerator StageClear_Co()
    { // Stage Clear
        AudioManager.Instance.SetAudioClip_SFX(4, false);
        memoryBoard.ButtonAllStop(); // ��ư ���� ����
        memoryManager.PlayStartPanel("�Ǹ��ؿ�!");

        yield return new WaitForSeconds(2f);

        memoryManager.CurrentStage++; //�������� Index ����
        if (memoryManager.EndStageIndex < memoryManager.CurrentStage)
        { // �غ�� �������� < ���� ��������, ��� �������� Ŭ���� ��
            GameManager.Instance.GameEnd?.Invoke();
            yield break;
        }

        Destroy(memoryBoard.gameObject); // ���� �������� ���� �����
        memoryManager.StageText.text = $"{memoryManager.CurrentStage} �ܰ�";

        // ���� ���������� (���ο� ���� ����) 
        memoryManager.CreatBoard();
    }
    #endregion
    #region Button Animation
    //������ �������� �����̴� �޼ҵ�
    public void PlayBlink()
    { // ���ӽ���, Ȥ�� ��Ʈ ��ư Ŭ�� �� ���� ��ư�� �˷��ִ� �޼ҵ�
        popButtonAnimation.SetTrigger("isBlink");
        if (memoryBoard.Stage.IsSpecialStage)
        { // ����� ���������� ��
            AudioManager.Instance.SetAudioClip_SFX(1, false);
        }
        else
        { // ����� ���������� �ƴ� ��
            AudioManager.Instance.SetAudioClip_SFX(2, false);
        }
    }

    private void PlayShakePush()
    { // ���� ��ư ���� �� ��鸮�� �ִϸ��̼�
        popButtonAnimation.SetTrigger("isShake");
        popButtonImage.raycastTarget = false;
    }

    public void ShakeEndAfter()
    { // �ִϸ��̼� Event�� �߰��Ǿ� ����
        popButtonImage.raycastTarget = true;
    }
    #endregion
}
