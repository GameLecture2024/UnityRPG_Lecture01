using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour, IAttackable
{
    public Enemy enemy;

    public ManualCollision attackCollision;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        attackCollision = GetComponentInChildren<ManualCollision>();
    }

    public void AttackTrigger()
    {
        Collider[] colliders = attackCollision.CheckOverlapBox();

        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<PlayerManager>() != null)
            {
                PlayerManager player = hit.GetComponentInParent<PlayerManager>();
                player.TakeDamage(enemy.AttackPower, player.transform.position);
                player.playerEffectManager.PlayFlashFX();
            }
        }   
    }
}
