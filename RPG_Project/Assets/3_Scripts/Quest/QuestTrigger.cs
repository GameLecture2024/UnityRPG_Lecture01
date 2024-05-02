using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour, IInteractable
{
    public Quest myQuest = new Quest();
    public int questId;                 // Database���� ������ ID ��

    // ��ȭ �ý��� ����
    [SerializeField] private DialogueSystem inGameDialogueSystem;
    bool nowSpeaking = false; // ���� ��ȭ ���� ���¸� true, �ƴϸ� false

    void Start()
    {
        myQuest = QuestManager.Instance.questDatabase[questId];

        QuestManager.Instance.OnCompleteQuest += OnCompleteQuest;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            QuestManager.Instance.ProcessQuest(QuestType.KillEnemy, questId);
        }
    }

    public void Interact(GameObject go)
    {
        if (nowSpeaking) return;

        // QuestDatabase���� �ڽ��� ���� �ִ� Quest Data�� TargetId�� ����, �ش� Ÿ���� Status�� None�� ��� ����

        if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.None) // ����Ʈ ����
        {
            // ����Ʈ ���� UI ���
            QuestManager.Instance.LoadQuestUI(myQuest, false);

            // ����Ʈ ��� ���
            inGameDialogueSystem.IndexNumber = 8;  // �� �� ������� : Quest Class���� quest.AcceptDialogue
            inGameDialogueSystem.Setup();
            StartCoroutine(InGameDialogue(inGameDialogueSystem, QuestStatus.Accepted));
        }
        else if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.Completed)
        {
            inGameDialogueSystem.IndexNumber = 9;
            inGameDialogueSystem.Setup();
            StartCoroutine(InGameDialogue(inGameDialogueSystem, QuestStatus.Rewarded));
        }
        else if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.Rewarded)
        {
            inGameDialogueSystem.IndexNumber = 10;
            inGameDialogueSystem.Setup();
            StartCoroutine(InGameDialogue(inGameDialogueSystem, QuestStatus.Rewarded));
        }
    }

    IEnumerator InGameDialogue(DialogueSystem text, QuestStatus status)
    {
        nowSpeaking = true;
        yield return new WaitUntil(() => text.UpdateDialog() == true);

        if (text.UpdateDialog())
        {
            QuestManager.Instance.questDatabase[myQuest.id].status = status;
            nowSpeaking = false;
        }
    }

    private void OnCompleteQuest(Quest quest)
    {
        if(quest.id == myQuest.id && quest.status == QuestStatus.Completed) // ���� ������ ���� ���� ����
        {
            QuestManager.Instance.LoadQuestUI(quest, true);
        }
    }
    

}
