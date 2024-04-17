using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour, IAttackable
{
    public ManualCollider attackCollider;

    void Awake()
    {
        attackCollider = GetComponentInChildren<ManualCollider>();  
    }

    public void AttackTrigger()
    {
        Collider[] colliders = attackCollider.GetColliderObject();
        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<PlayerManager>() != null)
            {
                // TakeDamage
                PlayerManager player = hit.GetComponentInParent<PlayerManager>();
                player.TakeDamage(player.AttackPower, player.transform.position);

            }
        }
    }
}
