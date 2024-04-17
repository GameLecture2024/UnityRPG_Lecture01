using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �ϴ� ��ü�� ���� Ư��
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
