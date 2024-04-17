using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터만 상속받을 것이 아니라 플레이어도 상속할 수 있게.
/// </summary>
public class Entity : MonoBehaviour, IDamagable
{
    [Header("Status")]
    public int HP;
    public int AttackPower;

    public Animator animator;

    public bool IsAlive => HP > 0;

    protected virtual void Awake()
    {
        OnLoadComponents();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void OnLoadComponents()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        Debug.Log($"{damage}만큼 피해를 입엇다.");
        HP -= damage;
    }
}
