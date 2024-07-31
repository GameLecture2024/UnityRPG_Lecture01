using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    HP, Mana, Attack, Defense
}

[System.Serializable]
public class Stat  
{
    public StatType type;
    public Modifier value;
}

[System.Serializable]
public class Modifier
{
    [NonSerialized] private int baseValue;                     // 기본 데이터
    [SerializeField] private int modifiedValue;                // 추가될 데이터

    private event Action<Modifier> OnModifyValue;
    private List<IModifier> modifiers = new List<IModifier>(); // modifier로 지정된 인터페이스 벨류를 전부 보관했다가 baseValue에 적용시켜주는 리스트

    public int BaseValue
    {
        get => baseValue;
        set
        {
            baseValue = value;
            UpdateModifiedValue();
        }
    }

    public int ModifiedValue
    {
        get => modifiedValue;
        set => modifiedValue = value;
    }

    public Modifier(Action<Modifier> method = null)
    {
        ModifiedValue = baseValue;
        // modifieValue를 변경시키는 이벤트를 등록해주는 함수 호출
        RegisterModifierEvent(method);
    }

    public void RegisterModifierEvent(Action<Modifier> method)
    {
        if(method!= null)
        {
            OnModifyValue += method;
        }
    }

    public void UnRegisterModifierEvent(Action<Modifier> method)
    {
        if(method!= null)
        {
            OnModifyValue -= method;
        }
    }

    /// <summary>
    /// 인터페이스 Modifier를 상속하고 있는 데이터가 있다면, 해당 데이터 벨류를 Stat에 변경해준 후 갱신시켜준다.
    /// </summary>
    private void UpdateModifiedValue()
    {
        int valueToAdd = 0;

        foreach(IModifier modifier in modifiers)
        {
            modifier.AddValue(ref valueToAdd);
        }

        ModifiedValue = baseValue + valueToAdd;

        OnModifyValue?.Invoke(this);
    }

    /// <summary>
    /// 아이템 장착, 버프 획득 시 Stat을 갱신 해준다
    /// </summary>
    public void Addmodifier(IModifier modifier)
    {
        modifiers.Add(modifier);
        UpdateModifiedValue();
    }

    /// <summary>
    /// 아이템 해제, 버프 종료 시 Stat 갱신
    /// </summary>
    public void RemoveModifier(IModifier modifier)
    {
        modifiers.Remove(modifier);
        UpdateModifiedValue();
    }
}
