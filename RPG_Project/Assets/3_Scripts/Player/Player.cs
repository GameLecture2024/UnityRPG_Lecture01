using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ĳ���͵��� ���������� �������� �Ӽ� �� ����� ��Ƶ� Ŭ����, PlayerManager Ŭ������ ������ ���
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
