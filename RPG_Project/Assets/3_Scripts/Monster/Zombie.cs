using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable 오브젝트의 데이터를 받아와서 저장하기 위한 클래스
/// </summary>
public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;

    public string ZombieName;
    public int HP;
    public int Attack;
    public float AttackRange;

    public void OnLoadComponents()
    {
        ZombieName = zombieData.zombieName;
        HP = zombieData.HP;
        Attack = zombieData.Attack;
        AttackRange = zombieData.attackRange;
    }
}
