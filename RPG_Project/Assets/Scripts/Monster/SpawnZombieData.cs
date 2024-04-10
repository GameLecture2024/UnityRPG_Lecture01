using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ZombieType { small, meduim, big}

public class Zombie
{
    public int HP;
    public int Attack;
    public float attackRange;
    public Zombie(ZombieData zombieData)
    {
        HP = zombieData.HP;
        Attack = zombieData.Attack;
        attackRange = zombieData.attackRange;
    }
}

public class SpawnZombieData : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    public ZombieType zombieType;


    private void Update()
    {
        // 시간 제어... 몇 초마다 좀비가 나올지..
        SpawnRandomZombie(zombieData);
    }

    public void SpawnRandomZombie(ZombieData zombieData)
    {
        Zombie newZombie = new Zombie(zombieData);

        Debug.Log("좀비의 체력:" + newZombie.HP);
        Debug.Log("좀비의 공격력:" + newZombie.Attack);
        Debug.Log("좀비의 공격 범위:" + newZombie.attackRange);
    }
}
