using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public Stat[] stats;

    public int MaxHp;
    public int MaxMp;

    public int AttackPower;
    public int DefensePower;

    public Action<PlayerData> OnChangedStats; // BaseValue(PlayerData)에 영향을 주는 함수에 콜백해주는 델리게이트

    public int GetBaseValue(StatType type)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                return stat.value.BaseValue;
            }
        }

        Debug.LogError("지정한 StstType이 존재하지 않습니다.");
        return -1;
    }

    public int GetModifiedValue(StatType type)
    {
        foreach (Stat stat in stats)
        {
            if (stat.type == type)
            {
                return stat.value.ModifiedValue;
            }
        }

        Debug.LogError("지정한 StstType이 존재하지 않습니다.");
        return -1;
    }

    public void SetBaseValue(StatType type, int value)
    {
        foreach(Stat stat in stats)
        {
            if(stat.type == type)
            {
                stat.value.BaseValue = value;
            }
        }
    }

    private void OnEnable()
    {
        InitalizeStats();
    }

    private void InitalizeStats()
    {

        foreach(Stat stat in stats)
        {
            stat.value = new Modifier(OnModifiedValue);

        }

        // 모든 기본 스탯 baseValue로 초기화
        SetBaseValue(StatType.HP, MaxHp);
        SetBaseValue(StatType.Mana, MaxMp);
        SetBaseValue(StatType.Attack, AttackPower);
        SetBaseValue(StatType.Defense, DefensePower);

        // 초기에 modify 계산을 해줘야 하는 스탯 초기화
        MaxHp = GetModifiedValue(StatType.HP);
        MaxMp = GetModifiedValue(StatType.Mana);
        AttackPower = GetModifiedValue(StatType.Attack);
        DefensePower = GetModifiedValue(StatType.Defense);
    }

    private void OnModifiedValue(Modifier value)
    {
        OnChangedStats?.Invoke(this);
    }

}
