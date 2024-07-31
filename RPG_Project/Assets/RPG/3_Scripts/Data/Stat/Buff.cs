using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Buff : IModifier
{
    public StatType type;

    public int value = 10;

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

}
