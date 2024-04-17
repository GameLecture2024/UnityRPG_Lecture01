using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")] // 데미지 타입 지정
    public int meleeDamage;
    public int magicDamage;
    public int trueDamage;

    [Header("Contact Point")]   // 공격 받은 위치 값 저장, 이펙트 생성 위치
    private Vector3 contactPoint;

    [Header("Damaged notify")]
    protected List<Entity> entityDamaged = new List<Entity>();

    GameObject effectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager entity = other.GetComponent<PlayerManager>();

            if(entity != null)
            {
                CalcDamage();
                contactPoint = other.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                entity.playerEffectManager.PlayFlashFX();
                entity.TakeDamage(trueDamage, contactPoint);
            }
        }

        if (other.CompareTag("Opponent"))
        {

        }
    }

    protected virtual void CalcDamage()
    {
        trueDamage = transform.GetComponentInParent<Entity>().AttackPower;
    }
}
