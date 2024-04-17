using System;
using UnityEngine;

public enum AttributeType
{
    Agility,
    Intellect,
    Stamina,
    Strength,
    Health,
    Mana,
}

[Serializable]
public class Attribute
{
    public AttributeType type;
    public Modifier value;
}

