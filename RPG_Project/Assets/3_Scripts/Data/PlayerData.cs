using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public int MaxHp;
    public int MaxMp;

    public int AttackPower;
}
