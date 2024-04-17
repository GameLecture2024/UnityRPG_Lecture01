using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public int HP;
    public int Stamina;
    public int Attack;
}
