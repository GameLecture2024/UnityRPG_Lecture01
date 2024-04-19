using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public Buff buff;

    public PlayerData playerData;

    public TextMeshProUGUI[] statText;             // 0: MaxHp, 1: MaxMp, 2: AttackPower, 3: DefensePower

    // ScriptableObject�� �ڵ带 �ۼ��� �� Reference�� �����͸� �����Ű�� �ʰ� �ϱ� ���ؼ� �����͸� Instance(Copy)�ؼ� ����Ѵ�.
    private void Awake()
    {
        //playerData = Instantiate(playerData) as PlayerData;
    }
    public void OnStatChangeTest()
    {
        // �׽�Ʈ�� ���� �ڵ� �ۼ�
        foreach(var stat in playerData.stats)
        {
            if(stat.type == buff.type)
            {
                stat.value.Addmodifier(buff);
            }
        }
    }

    // �׽�Ʈ�ϰ� �����ؾ��� ����
    public bool Test = false;

    private void Update()
    {
        if (Test)
        {
            Test = false;
            OnStatChangeTest();
        }

        int tempValue = playerData.MaxHp + 100;

        UpdateStatText();
    }

    private void OnEnable()
    {
        playerData.OnChangedStats += OnChangedStats;
    }


    private void UpdateStatText()
    {
        statText[0].text = playerData.GetModifiedValue(StatType.HP).ToString();
        statText[1].text = playerData.GetModifiedValue(StatType.Mana).ToString();
        statText[2].text = playerData.GetModifiedValue(StatType.Attack).ToString();
        statText[3].text = playerData.GetModifiedValue(StatType.Defense).ToString();
    }

    public void OnChangedStats(PlayerData playerData)
    {
        UpdateStatText();
    }

}   
