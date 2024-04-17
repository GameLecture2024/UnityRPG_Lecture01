using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격을 하는 객체의 공통 특성
/// </summary>
public class Entity : MonoBehaviour, IDamagable
{
    [Header("Status")]
    public int HP;
    public int MaxHP;
    public int AttackPower;
    public float AttackRange;
    public float ViewRange;

    public bool IsAlive => HP > 0;

    public Animator anim;

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
        anim = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage, Vector3 contactPos, GameObject hitEffectPrefabs = null)
    {
        HP -= damage;
    }
       
}
