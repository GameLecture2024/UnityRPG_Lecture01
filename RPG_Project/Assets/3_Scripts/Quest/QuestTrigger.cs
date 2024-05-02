using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour, IInteractable
{
    public Quest myQuest = new Quest();
    public int questId;                 // Database에서 가져올 ID 값

    // 대화 시스템 구현
    [SerializeField] private DialogueSystem inGameDialogueSystem;
    bool nowSpeaking = false; // 현재 대화 중인 상태면 true, 아니면 false

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

        // QuestDatabase에서 자신이 갖고 있는 Quest Data와 TargetId가 같고, 해당 타겟의 Status가 None일 경우 실행

        if (QuestManager.Instance.questDatabase[myQuest.id].status == QuestStatus.None) // 퀘스트 수락
        {
            // 퀘스트 수락 UI 출력
            QuestManager.Instance.LoadQuestUI(myQuest, false);

            // 퀘스트 대사 출력
            inGameDialogueSystem.IndexNumber = 8;  // 향 후 변경사항 : Quest Class안의 quest.AcceptDialogue
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
        if(quest.id == myQuest.id && quest.status == QuestStatus.Completed) // 보상 수령을 위한 로직 구현
        {
            QuestManager.Instance.LoadQuestUI(quest, true);
        }
    }
    

}
