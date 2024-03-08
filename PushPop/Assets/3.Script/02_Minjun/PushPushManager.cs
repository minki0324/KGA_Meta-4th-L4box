using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class PushPushManager : MonoBehaviour
{

    public CustomPushpopManager custom;
    public PuzzleLozic puzzle;
    [SerializeField] FramePuzzle frame;
    public GameObject decoPanel;
    [SerializeField] private TMP_Text stageTitle;
    public int pieceCount = 1; //�����͠����� �ߺ�ȣ���� �����ϱ����� �ε���
    public int pushCount; //��ư�������� + �Ǵ� ī��Ʈ pushCount == ��ư����Ʈ.Count ���ؼ� ���Ͻ� Ŭ���� ����(GameManager)
    public event Action onPushPushGameEnd;
    #region Ǫ��Ǫ�ð��� ��ü �ݹ�޼ҵ� ����

    public void OnAllBubblesPopped() //������ ��� ������ ����������� �Ҹ��� �޼ҵ�
    {
        puzzle.SettingGame();//������� ����
        TitleSet("�׸��� �°� ������ ���� ������!");
        pieceCount = 1;
    }
    public void OnPuzzleSolved() //������ ��� �������� �Ҹ��� �޼ҵ�
    {
        AudioManager.instance.SetAudioClip_SFX(1, false);
        TitleSet("�� ������� �׸��� �ٸ纸��!");
        DebugLog.instance.Adding_Message("������ ���ߴٰ�? ��� �ְﵥ?");
        puzzle.successCount = 0;
        DecoPanelSetActive(true);
        puzzle.onPuzzleClear?.Invoke();
        custom.gameObject.SetActive(true);
    }
    public void OnCustomEndmethod() //Ŀ�����ϴٰ� �������� ��ư �������� �Ҹ��� �޼ҵ�
    {//�������� ��ư�� onClick ����������.
        GameManager.Instance.GameClear();
        TitleSet("��� Ǫ������ ��������!");
        DecoPanelSetActive(false);
        custom.onCustomEnd?.Invoke();
        frame._myImage.alphaHitTestMinimumThreshold = 0f;
    }
    public void OnButtonAllPush() //Ŀ���� �Ϸ��ϰ� ��� ��������
    {//��ư ��� �������� �ڼ��ѷ��� GameManager GameClear�� ����.
        pushCount = 0; //
    }
    public void OnPushPushEnd() //Ǫ��Ǫ�ð� ��� ������ �ʱ�ȭ �Ұ͵� ��Ƶ� �޼ҵ�
    {
        GameManager.Instance.puzzleClass.Clear();
        onPushPushGameEnd?.Invoke();
        AudioManager.instance.SetCommonAudioClip_SFX(3);

    }
    #endregion

    public void DecoPanelSetActive(bool _bool)
    {
        decoPanel.SetActive(_bool);
    }
    public void TitleSet(string _string)
    {
        stageTitle.text = _string;
    }


}
