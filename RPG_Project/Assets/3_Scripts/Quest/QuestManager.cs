using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : SingletonMonoBehaviour<QuestManager>
{
    public Quest[] questData;

    public string QuestJsonName = "Quest";

    public Dictionary<int, Quest> questDatabase = new Dictionary<int, Quest>();

    [Header("Quest UI")]
    public GameObject questGameObject;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptText;

    public Action<Quest> OnCompleteQuest;   // Quest�� �Ű������� ���� �̺�Ʈ

    protected override void Awake()
    {
        base.Awake();
        LoadJson();
    }

    private void Start()
    {
        questData = new Quest[questDatabase.Count];
        questDatabase.Values.CopyTo(questData, 0);

        LoadQuestUI(questDatabase[100], true);
    }

    private void LoadJson()
    {
        string jString = JSONLoader.LoadData(QuestJsonName);

        JArray jArr = JArray.Parse(jString);

        int currentId = 0;

        foreach(JObject jObj in jArr)
        {
            int id = int.Parse((string)jObj["id"]);
            int targetId = int.Parse((string)jObj["targetId"]);
            int count = int.Parse((string)jObj["count"]);
            int targetCount = int.Parse((string)jObj["targetCount"]);

            // Enum.Parse(typeof(T), string) �� �Լ��� ���׸� Ŭ������ �����Ͽ���.
            QuestStatus status = EnumUtil<QuestStatus>.Parse((string)jObj["status"]);
            QuestType type = EnumUtil<QuestType>.Parse((string)jObj["type"]);

            int rewardExp = int.Parse((string)jObj["rewardExp"]);
            int rewardGold = int.Parse((string)jObj["rewardGold"]);
            string rewardItemId = (string)jObj["rewardItemId"];

            QuestReward reward = new QuestReward(rewardExp,rewardGold, rewardItemId);

            string title = (string)jObj["title"];
            string description = (string)jObj["description"];

            Quest newQuest = new Quest(id, targetId, count, targetCount, status, type, reward, title, description);

            if(currentId != int.Parse((string)jObj["id"]))
            {
                currentId = int.Parse((string)jObj["id"]);
            }
            questDatabase[currentId] = newQuest;
        }
    }

    public void LoadQuestUI(Quest quest, bool complete)
    {
        // ����Ʈ UI â�� �������� ��� Ȱ��ȭ
        if (questGameObject.activeSelf == false) questGameObject.SetActive(true);

        titleText.text = quest.title;
        descriptText.text = quest.description;

        if (complete)
        {
            descriptText.text = "<color=red><s>" + descriptText.text + "</s></color>";
        }
    }

    public void ProcessQuest(QuestType type, int targetID)
    {
        foreach(Quest quest in questData) // QuestData : Json���� ���� �ε��� �����͸� ����
        {
            if(quest.status == QuestStatus.Accepted && quest.type == type && quest.targetId == targetID) // ���� : Accepted, type�� ����, Id ���� ���
            {
                quest.count++;   // ���� ����Ʈ �Ϸ� üũ�� ���� ����

                if(quest.count >= quest.targetCount)     // ����Ʈ �Ϸ�
                {
                    quest.status = QuestStatus.Completed;
                    // �̺�Ʈ ����
                    OnCompleteQuest?.Invoke(quest);
                }
            }
        }
    }
}
