using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public PlayerManager player;

    [SerializeField] private List<TutorialBase> tutorials; // �ν����� â���� Tutorial Class�� �޾ƿͼ� ������ List

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    private void Start()
    {
        SetNextTutorial();
        player.canMove = false;
        player.isPerformingAction = true;
    }

    private void Update()
    {
        currentTutorial?.Execute(this);    // Ʃ�丮�� ������ ������ Update���� �����϶�.
    }

    public void SetNextTutorial() // currentTutorial �ִ� ����� �����϶�.
    {
        if (currentTutorial != null) currentTutorial.Exit(); // ���� ���� Ʃ�丮���� ������ �����ϰ�

        if(currentIndex >= tutorials.Count - 1)                   // Ʃ�丮���� ������ �˻�
        {
            CompleteTutorial(); // Ʃ�丮�� ���� ������ ����
            return;
        }

        // ���� Ʃ�丮�� ����
        currentIndex++;
        Debug.Log($"���� �ε��� ��ȣ{currentIndex}");
        currentTutorial = tutorials[currentIndex];

        currentTutorial.Enter();                            // ���ο� Ʃ�丮���� �����Ѵ�.
    }

    void CompleteTutorial()
    {
        Debug.Log("Ʃ�丮�� ��");
        currentTutorial = null;
        LoadingUI.LoadScene("BattleScene");                // �ε� ������ �̵� ��, �ε��� ������ nextScene���� �̵�
    }
}
