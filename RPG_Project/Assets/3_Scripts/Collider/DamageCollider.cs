using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")] // ������ Ÿ�� ����
    public int meleeDamage;
    public int magicDamage;
    public int trueDamage;

    [Header("Contact Point")]   // ���� ���� ��ġ �� ����, ����Ʈ ���� ��ġ
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
