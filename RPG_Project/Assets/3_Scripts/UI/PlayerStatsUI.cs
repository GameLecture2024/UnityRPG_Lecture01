using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public Buff buff;

    public PlayerData playerData;

    public TextMeshProUGUI[] statText;             // 0: MaxHp, 1: MaxMp, 2: AttackPower, 3: DefensePower

    // ScriptableObject를 코드를 작성할 때 Reference의 데이터를 변경시키지 않게 하기 위해서 데이터를 Instance(Copy)해서 사용한다.
    private void Awake()
    {
        //playerData = Instantiate(playerData) as PlayerData;
    }
    public void OnStatChangeTest()
    {
        // 테스트를 위한 코드 작성
        foreach(var stat in playerData.stats)
        {
            if(stat.type == buff.type)
            {
                stat.value.Addmodifier(buff);
            }
        }
    }

    // 테스트하고 삭제해야할 변수
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
