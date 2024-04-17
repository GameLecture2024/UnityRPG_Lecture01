using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터들이 공통적으로 가져야할 속성 및 기능을 모아둔 클래스, PlayerManager 클래스로 데이터 사용
/// </summary>
public class Player : Entity, IDamagable
{
    public ManualCollision attackCollision;
    public LayerMask attackTarget;

    public Transform hitSpawnPos;


    [Header("Player Stats")]
    public PlayerStats playerStats;
    public PlayerData playerData;

    public int Stamina;
    public int MaxStmaina;

    protected override void Start()
    {
        base.Start();

    }

    public override void OnLoadComponents()
    {
        base.OnLoadComponents();
        playerStats = GetComponent<PlayerStats>();
    }

    public override void TakeDamage(int damage, Vector3 contactPos,GameObject hitEffectPrefabs = null)
    {
        base.TakeDamage(damage, contactPos, hitEffectPrefabs);
    }
}
