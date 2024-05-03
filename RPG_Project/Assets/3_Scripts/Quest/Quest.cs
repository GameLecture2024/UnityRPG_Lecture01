using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus
{
    None, Accepted, Completed, Rewarded
}

public enum QuestType // ����Ʈ ���� ���ǿ� ���� ����
{
    KillEnemy, BringItem, Ending
}

[System.Serializable]
public class QuestReward
{
    public int rewardExp;                // ȹ�� ����ġ
    public int rewardGold;               // ȹ�� ��ȭ
    public string rewardItemId;          // ������ ���

    public QuestReward() { } // default ������

    public QuestReward(int rewardExp, int rewardGold, string rewardItemId)
    {
        this.rewardExp = rewardExp;
        this.rewardGold = rewardGold;
        this.rewardItemId = rewardItemId;
    }
}

[System.Serializable]
public class Quest
{
    public int id;             // Database�� ����� Key��
    public int targetId;       // ����Ʈ�� �����ϱ� ���� ��Ī��ų id���� ������ ����
    public int count;          // n�� ���� óġ�Ͻÿ�.. n�� ������ ����������..
    public int targetCount;    // ��ǥ ī��Ʈ ��

    public QuestStatus status; // �⺻ ����Ʈ�� ����
    public QuestType type;     // ����Ʈ�� Ÿ��
    public QuestReward reward; // ����Ʈ ����

    [Header("���")]
    public int StartIndexNumber;    // ����Ʈ ���� ��� �ε��� ��ȣ
    public int CompleteIndexNumber; // ����Ʈ �Ϸ� ��� �ε��� ��ȣ
    public int EndIndexnumber;      // ����Ʈ �Ϸ� �� �� ��� �ε��� ��ȣ

    [Header("UI")]
    public string title;
    public string description;

    public Quest() { }  // default ������

    public Quest(int id, int targetId, int count, int targetCount, QuestStatus status, 
        QuestType type, QuestReward reward, string title, string description, int startIndexNumber, int completeIndexNumber, int endIndexnumber)
    {
        this.id = id;
        this.targetId = targetId;
        this.count = count;
        this.targetCount = targetCount;
        this.status = status;
        this.type = type;
        this.reward = reward;
        this.title = title;
        this.description = description;
        this.StartIndexNumber = startIndexNumber;
        this.CompleteIndexNumber = completeIndexNumber;
        this.EndIndexnumber = endIndexnumber;
    }
}
