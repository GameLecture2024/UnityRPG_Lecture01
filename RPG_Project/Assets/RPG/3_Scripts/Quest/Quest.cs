using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus
{
    None, Accepted, Completed, Rewarded
}

public enum QuestType // 퀘스트 수행 조건에 사용될 변수
{
    KillEnemy, BringItem, Ending
}

[System.Serializable]
public class QuestReward
{
    public int rewardExp;                // 획득 경험치
    public int rewardGold;               // 획득 재화
    public string rewardItemId;          // 아이템 경로

    public QuestReward() { } // default 생성자

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
    public int id;             // Database에 저장될 Key값
    public int targetId;       // 퀘스트를 수행하기 위해 매칭시킬 id값을 저장할 변수
    public int count;          // n개 몬스터 처치하시오.. n개 아이템 가져오세요..
    public int targetCount;    // 목표 카운트 수

    public QuestStatus status; // 기본 퀘스트의 상태
    public QuestType type;     // 퀘스트의 타입
    public QuestReward reward; // 퀘스트 보상

    [Header("대사")]
    public int StartIndexNumber;    // 퀘스트 시작 대사 인덱스 번호
    public int CompleteIndexNumber; // 퀘스트 완료 대사 인덱스 번호
    public int EndIndexnumber;      // 퀘스트 완료 이 후 대사 인덱스 번호

    [Header("UI")]
    public string title;
    public string description;

    public Quest() { }  // default 생성자

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
