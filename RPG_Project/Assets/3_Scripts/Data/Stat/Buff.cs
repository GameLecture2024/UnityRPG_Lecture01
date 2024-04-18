using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Data/Stats Buff")]
public class Buff : ScriptableObject, IModifier
{
    public StatType type;

    public int value = 10;

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

}
