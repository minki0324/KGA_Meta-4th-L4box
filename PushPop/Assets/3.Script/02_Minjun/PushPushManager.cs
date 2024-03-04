using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPushManager : MonoBehaviour
{

    public CustomPushpopManager custom;
    public PuzzleLozic puzzle;
    [SerializeField] FramePuzzle frame;
    public int pushCount; //��ư�������� + �Ǵ� ī��Ʈ pushCount == ��ư����Ʈ.Count ���ؼ� ���Ͻ� Ŭ���� ����(GameManager)

    #region Ǫ��Ǫ�ð��� ��ü �ݹ�޼ҵ� ����

    public void OnAllBubblesPopped() //������ ��� ������ ����������� �Ҹ��� �޼ҵ�
    {
        puzzle.SettingGame();//������� ����
    }
    public void OnPuzzleSolved() //������ ��� �������� �Ҹ��� �޼ҵ�
    {
        AudioManager.instance.SetAudioClip_SFX(1, false);
        puzzle.successCount = 0;
        puzzle.onPuzzleClear?.Invoke();
    }
    public void OnCustomEndmethod() //Ŀ�����ϴٰ� �������� ��ư �������� �Ҹ��� �޼ҵ�
    {//�������� ��ư�� onClick ����������.
        GameManager.Instance.GameClear();
        custom.decoPanel.SetActive(false);
        custom.onCustomEnd?.Invoke();
        frame._myImage.alphaHitTestMinimumThreshold = 0f;
    }
    public void OnButtonAllPush() //Ŀ���� �Ϸ��ϰ� ��� ��������
    {//��ư ��� �������� �ڼ��ѷ��� GameManager GameClear�� ����.
        pushCount = 0; //
    }
    public void OnPushPushEnd() //Ǫ��Ǫ�ð� ��� ������ �ʱ�ȭ �Ұ͵� ��Ƶ� �޼ҵ�
    {

    }
    #endregion



}
